using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Main
{
    public partial class Amatrix_Sever_Client_Lite
    {
        public ArrayList al_ips = new ArrayList();
        public string IP = "127.0.0.1";
        public int PORT = 5632;
        public BackgroundWorker bkk_ip = new BackgroundWorker();
        byte[] dataRcvBuf = new byte[1024];

        Scorpion.Form1 fm1;
        public Amatrix_Sever_Client_Lite(Scorpion.Form1 f1)
        {
            fm1 = f1;
            bkk_ip.WorkerSupportsCancellation = true;
            return;
        }

        public void start_client(string Scorp_Line_Exec)
        {
            //(*IP,*PORT,*NAME)
            ArrayList al = fm1.readr.lib_SCR.cut_variables(Scorp_Line_Exec);


            IP = fm1.readr.lib_SCR.var_get(al[0].ToString()).ToString();
            PORT = Convert.ToInt32(fm1.readr.lib_SCR.var_get(al[1].ToString()));

            fm1.write_to_cui(IP + " " + PORT);

            bkk_ip = new BackgroundWorker();
            bkk_ip.DoWork += new DoWorkEventHandler(bkk_ip_DoWork);
            bkk_ip.RunWorkerAsync(al);

            Scorp_Line_Exec = null;

            return;
        }

        public void stop_client(ref string Scorp_Line)
        {
            //(*NAME)
            ArrayList al = fm1.readr.lib_SCR.cut_variables(Scorp_Line);

            ((Socket)fm1.AL_SOCK[fm1.AL_SOCK_REF.IndexOf(al[0].ToString())]).Disconnect(false);
            ((Socket)fm1.AL_SOCK[fm1.AL_SOCK_REF.IndexOf(al[0].ToString())]).Close();
            ((Socket)fm1.AL_SOCK[fm1.AL_SOCK_REF.IndexOf(al[0].ToString())]).Dispose();
            //senderSock.Disconnect(false);
            //senderSock.Close();
            //senderSock.Dispose();

            bkk_ip.CancelAsync();
            display_address(fm1.readr.lib_SCR.var_get(al[0].ToString()).ToString());

            fm1.readr.lib_SCR.var_arraylist_dispose(ref al);
            Scorp_Line = null;

            return;
        }

        delegate void del_addr(); System.Threading.Thread th_addr;
        public void address_ip()
        {
           // fm1.Invoke(new del_addr(display_address));
        }

        public void display_address(String Name)
        {
            try
            {
                if (((Socket)fm1.AL_SOCK[fm1.AL_SOCK_REF.IndexOf(Name)]).Connected)
                {
                    fm1.write_to_cui("IP: " + ((Socket)fm1.AL_SOCK[fm1.AL_SOCK_REF.IndexOf(Name)]).LocalEndPoint.ToString() + " @ Port: " + PORT);
                }
                else
                {
                    fm1.write_to_cui("IP: " + fm1.AL_MESSAGE[0] + " and " + fm1.AL_MESSAGE[1]);
                }
            }
            catch
            {
                fm1.write_to_cui("IP: " + fm1.AL_MESSAGE[2] + " @ Port: " + fm1.AL_MESSAGE[2]);
            }
            return;
        }

        //server code
        byte[] b; ArrayList al_socks = new ArrayList();
        //Socket senderSock;
        public void create_socket(string ip_address, ArrayList al)
        {
            try
            {
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, ip_address, SocketPermission.AllPorts);

                IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(ip_address));
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipep = new IPEndPoint(ipAddr, PORT);

                Socket senderSock = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                senderSock.Connect(ipep);
                senderSock.BeginReceive(this.dataRcvBuf, 0, this.dataRcvBuf.Length, SocketFlags.None, new AsyncCallback(this.OnBytesReceived), senderSock);

                fm1.AL_SOCK.Add(senderSock);
                fm1.AL_SOCK_REF.Add(fm1.readr.lib_SCR.var_get(al[2].ToString()));
                fm1.AL_SESSION.Add(new ArrayList());
                //al_socks.Add(senderSock);
            }
            catch { }
            th_addr = new System.Threading.Thread(new System.Threading.ThreadStart(address_ip));
            th_addr.Start();

            ip_address = null;

            return;
        }

        string s;
        private Encoding encoding = Encoding.UTF8;
        protected void OnBytesReceived(IAsyncResult result)
        {
            //4 values [from,obj,scorpline,sessionid]
            try
            {
                Socket s_tmp = (Socket)result.AsyncState;
                int nBytesRec = s_tmp.EndReceive(result);
                if (nBytesRec <= 0)
                {
                    s_tmp.Close();
                    return;
                }
                string strReceived = this.encoding.GetString(this.dataRcvBuf, 0, nBytesRec);
                s = strReceived;

                s_tmp.BeginReceive(this.dataRcvBuf, 0, this.dataRcvBuf.Length, SocketFlags.None, new AsyncCallback(this.OnBytesReceived), s_tmp);

                handle((ArrayList)ByteArrayToObject(dataRcvBuf));
            }
            catch { }
        }

        private void handle(ArrayList al_hptr)
        {
            //(*destination,*commandtype,*sock_name,*objects.../*commands)
            //commandtype=(True=Command/False=Objects)

            if(fm1.readr.lib_SCR.var_get(al_hptr[1].ToString()).ToString() == fm1.types.S_No)
            {
                //Set Object to Session

            }
            else
            {
                //Execute Command
                for (int i = 3; i < al_hptr.Count; i++)
                {
                    fm1.readr.lib_SCR.work_(al_hptr[i].ToString());
                }
            }
            
            /*if (al_hptr[2].ToString().StartsWith("STM["))
            {
                fm1.Access_Work("net.st(" + al_hptr[2].ToString() + ")");
            }
            else if (al_hptr[2].ToString().StartsWith("REC["))
            {

            }*/

            return;
            //fm1.Access_Work(al_hptr[2].ToString());
        }

        public void broadcast(ref ArrayList al/*string Reciever, object Object, string socket*/)
        {
            //(*destination,*commandtype,*sock_name,*objects.../*commands)
            //commandtype=(True=Command/False=Objects)

            //()
            //ArrayList al = new ArrayList(); al.Add(Reciever); al.Add(fm1.readr.lib_SCR.var_get(Object.ToString())); al.Add(socket);

            //Send Structure

            if (fm1.readr.lib_SCR.var_get(al[1].ToString()).ToString() == fm1.types.S_No)
            {
                for(int i = 3; i < al.Count; i++)
                {
                    al[i] = fm1.readr.lib_SCR.var_get_full(al[i].ToString());
                }
            }

            int byteSend = ((Socket)fm1.AL_SOCK[fm1.AL_SOCK_REF.IndexOf(fm1.readr.lib_SCR.var_get(al[2].ToString()))]).Send(ObjectToByteArray((object)al));

            fm1.readr.lib_SCR.var_arraylist_dispose(ref al);

            //Reciever = null;
            //socket = null;
            //Object = null;

            return;
        }

        private Object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }

        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        private void get_string(Socket s)
        {
            byte[] b = new byte[1024];
            s.BeginReceive(b, 0, 0, SocketFlags.None, out_, this);
        }

        private void out_(IAsyncResult iar)
        {
        }

        private void Amatrix_Sever_Client_Lite_Load(object sender, EventArgs e)
        {

        }

        private void bkk_ip_DoWork(object sender, DoWorkEventArgs e)
        {
            create_socket(IP, (ArrayList)e.Argument);
        }

        private void bkk_ip_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*if (e.Cancelled == false)
            {
                create_socket(IP);
            }*/
        }
    }
}

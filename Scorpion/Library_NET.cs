/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2016>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scorpion
{
    partial class Librarian
    {
        //Connected to the Main Scorpion Foundation Research Center Server
        /*public void NET(ref string Scorp_Line_)
        {
            //FTP ONLY SYSTEM
            if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[121] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.ftp_serv.auth_table(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[122] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.ftp_serv.upload();
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[123] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.ftp_serv.download(); 
            }

            //CLIENT
            if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[78] + Do_on.AL_ACC[3].ToString()))
            {
                send_tcp_message(Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[88] + Do_on.AL_ACC[3].ToString()))
            {
                //(*IP,*PORT,*NAME)
                Do_on.amcl.start_client(Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[89] + Do_on.AL_ACC[3].ToString()))
            {
                //(*NAME)
                Do_on.amcl.stop_client(ref Scorp_Line_);
            }

            //SERVER
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[132] + Do_on.AL_ACC[3].ToString()))
            {
                //(*IP, *PORT)
                start_server(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[133] + Do_on.AL_ACC[3].ToString()))
            {
                //(*IP, *PORT)
                stop_server(ref Scorp_Line_);
            }*/


            //DEPRECIATED, ONLY TWO FACTOR FTP SYSTEM USED NOW
            /*else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[79] + Do_on.AL_ACC[3].ToString()))
            {
                set_term(Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[92] + Do_on.AL_ACC[3].ToString()))
            {
                //(*NAME)
                Start_Interprocess_Pipe(Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[93] + Do_on.AL_ACC[3].ToString()))
            {
                //(*NAME)
                Stop_Interprocess_Pipe(Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[98] + Do_on.AL_ACC[3].ToString()))
            {
                //(*URL,*CODE)
                HTTP_post(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().StartsWith(Do_on.AL_ACC_SUP[2] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[99] + Do_on.AL_ACC[3].ToString()))
            {
                //(*URL,*CODE)
                HTTP_get(ref Scorp_Line_);
            }*/
            //Node.js

            //else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {" + Do_on.AL_ACC_SUP[2] + "} in line {" + Scorp_Line_ + "}"); }



            //Clean
            /*Scorp_Line_ = null;
            return;
        }*/

        public void start_server(ref string Scorp_Line)
        {
            //(*IP,*PORT)
            ArrayList al = cut_variables(ref Scorp_Line);
            Do_on.serv.Start(var_get(al[0].ToString()).ToString(), Convert.ToInt32(var_get(al[1].ToString()).ToString()));

            return;
        }

        public void stop_server(ref string Scorp_Line)
        {
            //()
            Do_on.serv.stop();
            return;
        }


        
        public void HTTPpost(ref string Scorp_Line_Exec)
        {
            //(*URL,*CODE)
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            byte[] buffer = Encoding.ASCII.GetBytes(var_get(al[1].ToString()).ToString());
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(var_get(al[0].ToString()).ToString());
            webreq.Method = "POST";
            webreq.ContentType = "application/x-www-form-urlencoded";
            webreq.ContentLength = buffer.Length;
            Stream set_dat = webreq.GetRequestStream();
            set_dat.Write(buffer, 0, buffer.Length);
            set_dat.Close();

            HttpWebResponse rsvp = (HttpWebResponse)webreq.GetResponse();

            Do_on.write_to_cui(rsvp.StatusCode.ToString());
            Do_on.write_to_cui(rsvp.Server);

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }

        public void HTTPget(ref string Scorp_Line_Exec)
        {
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(string.Format("{0}{1}", var_get(al[0].ToString()), var_get(al[1].ToString())));
            webreq.Method = "GET";
            HttpWebResponse webresp = (HttpWebResponse)webreq.GetResponse();

            Do_on.write_to_cui(webresp.ToString());
            Do_on.write_to_cui(webresp.Server);

            Stream Answer = webresp.GetResponseStream();
            StreamReader ans = new StreamReader(Answer);

            Do_on.write_to_cui(ans.ReadToEnd());

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }/*
    
        public void set_term(string Scorp_Line_Exec)
        {
            Do_on.session = Convert.ToInt64(cut_custom(ref Scorp_Line_Exec, "st(", ")"));

            //CLEAN
            Scorp_Line_Exec = null;

            return;
        }*/

        public void send_tcp_message(string Scorp_Line_Exec)
        {
            //(*destination,*commandtype,*sock_name,*objects.../*commands)
            //commandtype=(True=Command/False=Objects)
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            if (var_get(al[1].ToString()).ToString() == Do_on.types.S_Yes)
            {
                al = cut_commands(ref Scorp_Line_Exec, ref al, 3);
            }
        
            Do_on.amcl.broadcast(ref al);

            //Clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        //InterP pipes
        /*
        private void Start_Interprocess_Pipe(string Scorp_Line_Exec)
        {
            Do_on.AL_PIPES.Add(new Scorpion_Interprocess_Network_Admin().start_pipe_server(cut_custom(Scorp_Line_Exec.Clone().ToString(), "name(", ")"), this));
            Do_on.AL_PIPES_REF.Add(cut_custom(Scorp_Line_Exec.Clone().ToString(), "name(", ")"));

            return;
        }

        private void Stop_Interprocess_Pipe(string Scorp_Line_Exec)
        {
            ((Scorpion_Interprocess_Network_Admin)Do_on.AL_PIPES[Do_on.AL_PIPES_REF.IndexOf(cut_custom(ref Scorp_Line_Exec, "name(", ")"))]).stop_pipe_server();
            return;
        }

        private void Send_Interprocess_Pipe(string Scorp_Line_Exec)
        {
            //net.pipe.send(name(sys_rn_SCP), message(app.ext))
            
            ((Scorpion_Interprocess_Network_Admin)Do_on.AL_PIPES[Do_on.AL_PIPES_REF.IndexOf(cut_custom(Scorp_Line_Exec, "name(", ")"))]).broadcast_local(cut_custom(Scorp_Line_Exec, "message(", ")"));
            return;
        }*/

        //TCP/IP
        private void connect(string Scorp_Line_Exec)
        {
            //(ip(*),port(*))
            cut_variables(ref Scorp_Line_Exec);
            Start(ref Scorp_Line_Exec);

            //clean
            Scorp_Line_Exec = null;
            return;
        }

        //AMATRIX SERVER FOR CLIENT USE
        private Socket sock;
        private int port = 0000;
        private IPAddress addr = IPAddress.Any;
        private ArrayList al_IPs = new ArrayList();

        // This is the method that starts the server listening.
        public void Start(ref string Scorp_Line_Exec)
        {
            //(ip(*),port(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            IPHostEntry host;
            string localIP = al[0].ToString();
            port = Convert.ToInt32(al[1]);
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }

            //Scorpion.Form1.IP = localIP;
            aqcuire_ip(localIP, port);

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);
            localIP = null;

            return;
        }

        public void Stop_TCPIP(ref string Scorp_Line_Exec)
        {


            //clean
            Scorp_Line_Exec = null;

            return;
        }

        private void aqcuire_ip(string IP, int PORT)
        {
            SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, Scorpion.Form1.IP, SocketPermission.AllPorts);

            IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(IP));
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipep = new IPEndPoint(ipAddr, PORT);

            this.sock = new Socket(
                ipAddr.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            sock.Bind(new IPEndPoint(ipAddr, this.port));
            this.sock.Listen(10);
            this.sock.BeginAccept(this.OnConnectRequest, sock);

            Do_on.AL_AMCS.Add(sock);
            Do_on.AL_AMCS_REF.Add(IP + ":" + PORT.ToString());

            //clean
            IP = null;

            return;
        }

        private void OnConnectRequest(IAsyncResult result)
        {
            try
            {
                Socket sock = (Socket)result.AsyncState;
                Connection newConn = new Connection(sock.EndAccept(result));
                sock.BeginAccept(this.OnConnectRequest, sock);
            }
            catch { }
        }

        public void end()
        {
            sock.Close();
        }

        public class Connection// : Form1
        {
            private Socket sock;
            private Encoding encoding = Encoding.UTF8;
            byte[] dataRcvBuf = new byte[1024];

            public Connection(Socket s)
            {
                this.sock = s;
                this.BeginReceive();
            }
            private void BeginReceive()
            {
                this.sock.BeginReceive(this.dataRcvBuf, 0,this.dataRcvBuf.Length,SocketFlags.None,new AsyncCallback(this.OnBytesReceived),this);
            }

            string s; TypeConverter tc = new TypeConverter();
            protected void OnBytesReceived(IAsyncResult result)
            {
                try
                {
                    int nBytesRec = this.sock.EndReceive(result);
                    if (nBytesRec <= 0)
                    {
                        this.sock.Close();
                        return;
                    }
                    string strReceived = this.encoding.GetString(this.dataRcvBuf, 0, nBytesRec);
                    s = strReceived;

                    this.sock.BeginReceive(
                        this.dataRcvBuf, 0,
                        this.dataRcvBuf.Length,
                        SocketFlags.None,
                        new AsyncCallback(this.OnBytesReceived),
                        this);

                    handle((ArrayList)ByteArrayToObject(dataRcvBuf));
                    //string g = UnicodeEncoding.Unicode.GetString(this.dataRcvBuf);
                }
                catch { }
            }

            private void handle(ArrayList al_hptr)
            {
                //if command or object>>>
                //(*destination,*commandtype,*sock_name,*objects.../*commands)
                //commandtype=(True=Command/False=Objects)
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

            public void broadcast(string Reciever, object Object, string Scorp_Line_Exec)
            {
                ArrayList al = new ArrayList(); al.Add(Reciever); al.Add(Object); al.Add(Scorp_Line_Exec);
                byte[] msg = ObjectToByteArray((object)al);

                int byteSend = sock.Send(msg);
            }
        }
    }
}
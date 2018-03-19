using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace Main
{
    public partial class Amatrix_Server_Lite : Form
    {
        public Amatrix_Server_Lite()
        {
            InitializeComponent();
            this.Show();
            this.Visible = false;
            BackgroundWorker bkk = new BackgroundWorker();
            bkk.DoWork += new DoWorkEventHandler(bkk_thrd_DoWork);
            bkk.RunWorkerAsync();
        }

        private Thread th_strt;
        private delegate void del_strt_();
        private void bkk_thrd_DoWork(object sender, DoWorkEventArgs e)
        {
            th_strt = new Thread(new ThreadStart(del_strt_init_));
            th_strt.IsBackground = true;
            th_strt.Start();
        }

        private void del_strt_init_()
        {
            this.Invoke(new del_strt_(Start));
        }

        private void Amatrix_Server_Lite_Load(object sender, EventArgs e)
        {

        }

        //server
        private Socket sock;
        private int port = 5632;
        private IPAddress addr = IPAddress.Any;
        private ArrayList al_IPs = new ArrayList();

        // This is the method that starts the server listening.
        public void Start()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }

            Scorpion.Form1.IP = localIP;
            bkk_aqcuire.RunWorkerAsync();
        }

        Thread th_aqc;
        delegate void del_aqc();
        private void bkk_aqcuire_DoWork(object sender, DoWorkEventArgs e)
        {
            th_aqc = new Thread(new ThreadStart(del_aqc_st));
            th_aqc.IsBackground = true;
            th_aqc.Start();
        }

        private void del_aqc_st()
        {
            this.Invoke(new del_aqc(aqcuire_ip));
        }

        private void aqcuire_ip()
        {
            for (int i = 0; i <= 3; i++)
            {
                // Create the new socket on which we'll be listening.
                try
                {
                    if (i > 0) { Scorpion.Form1.IP = "127.0.0." + i.ToString(); }

                    SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, Scorpion.Form1.IP, SocketPermission.AllPorts);

                    IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(Scorpion.Form1.IP));
                    IPAddress ipAddr = ipHost.AddressList[0];
                    IPEndPoint ipep = new IPEndPoint(ipAddr, 5632);

                    this.sock = new Socket(
                        ipAddr.AddressFamily,
                        SocketType.Stream,
                        ProtocolType.Tcp);
                    sock.Bind(new IPEndPoint(ipAddr, this.port));
                    this.sock.Listen(10);
                    this.sock.BeginAccept(this.OnConnectRequest, sock);

                    break;
                }
                catch { }
            }
        }

        private void OnConnectRequest(IAsyncResult result)
        {
            try
            {
                Socket sock = (Socket)result.AsyncState;
                Connection newConn = new Connection(sock.EndAccept(result), this);
                sock.BeginAccept(this.OnConnectRequest, sock);
            }
            catch (Exception erty) { }
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

            Amatrix_Server_Lite amsl_;
            public Connection(Socket s, Amatrix_Server_Lite amsl)
            {
                amsl_ = amsl;
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
                catch (Exception erty) { }
            }

            private void handle(ArrayList al_hptr)
            {

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
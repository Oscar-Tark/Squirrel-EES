using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Amatrix_Server_1._1
{
    public partial class Form1 : Form
    {
        Scorpion.Form1 Do_on;
        public Form1(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            IntPtr Hicon = global::Scorpion.Properties.Resources.server.GetHicon();
            this.Icon = Icon.FromHandle(Hicon);
            this.Show();
            return;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop();
            return;
        }

        ~Form1()
        {
            stop();
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Socket sock;
        // You'll probably want to initialize the port and address in the
        // constructor, or via accessors, but to start your server listening
        // on port 8080 and on any IP address available on the machine...
        private int port = 5632;
        private IPAddress addr = IPAddress.Any; Connection newConn;

        // This is the method that starts the server listening.
        bool socket_started = false;
        public void start_socket(IPAddress ipAddr)
        {
            sock = new Socket(
                ipAddr.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            socket_started = true;
            return;
        }

        public void Start(string ip, int port)
        {
            port = Convert.ToInt32(port_chooser.Value);
            try
            {
                // Create the new socket on which we'll be listening.
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, ip, port);
                IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(ip));
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipep = new IPEndPoint(ipAddr, port);

                if (!socket_started)
                {
                    start_socket(ipAddr);
                }
                else { sock.Connect(ipep); }
                // Bind the socket to the address and port.
                sock.Bind(new IPEndPoint(ipAddr, port));
                // Start listening.
                this.sock.Listen(10);
                // Set up the callback to be notified when somebody requests
                // a new connection.
                this.sock.BeginAccept(this.OnConnectRequest, sock);
               
                al_messages.Add("Connected to [" + ipAddr + "]");
                Do_on.write_to_cui("New Server @ [" + ipAddr + ":" + port.ToString() + "]");
                //treeView1.Nodes.Find("Node1", true)[0].Nodes.Add(ipAddr.ToString() + ":" + this.port);
            }
            catch (Exception erty) { al_messages.Add("Error: " + erty.Message); erty = null; }
            return;
        }

        public void stop()
        {
            try
            {
                foreach (Connection n in al_conn)
                {
                    n.Close_Connection();
                }
            }
            catch { }
            try
            {
                al_messages.Add("Closing Connection [" + sock.LocalEndPoint.AddressFamily.ToString() + "]");
                Do_on.write_to_cui("Closing Connection @ [" + sock.LocalEndPoint.AddressFamily.ToString() + "]");
            }
            catch { }
            try
            {
                sock.Close();
            }
            catch { }

            return;
        }

        public ArrayList al_messages = new ArrayList();
        public void write_to_cui(string To_Write)
        {
            al_messages.Add("\n" + To_Write);
            //rtb.Text = rtb.Text + "\n" + To_Write;
            return;
        }

        // This is the method that is called when the socket recives a request
        // for a new connection.
        private void OnConnectRequest(IAsyncResult result)
        {
            try
            {
                // Get the socket (which should be this listener's socket) from
                // the argument.
                Socket sock = (Socket)result.AsyncState;
                // Create a new client connection, using the primary socket to
                // spawn a new socket.
                newConn = new Connection(sock.EndAccept(result), this, Do_on);
                al_conn.Add(newConn);
                //sock.EnableBroadcast = true;
                // Tell the listener socket to start listening again.
                sock.BeginAccept(this.OnConnectRequest, sock);
                al_messages.Add("Connection Request From: " + sock.LocalEndPoint.ToString());
            }
            catch(Exception erty) { al_messages.Add(erty.Message); }
        }
        ArrayList al_conn = new ArrayList();

        private void button1_Click(object sender, EventArgs e)
        {
            sock.Close();
            al_messages.Add("Closing Server..");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Start(textBox1.Text, (int)port_chooser.Value);
        }

        private void write_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < al_messages.Count; i++)
            {
                rtb.Text = rtb.Text + al_messages[i] + "\n";
                al_messages.RemoveAt(i);
            }

            al_messages.TrimToSize();
            return;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            stop();
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    public class Connection// : Form1
    {
        private Socket sock;
        // Pick whatever encoding works best for you.  Just make sure the remote 
        // host is using the same encoding.
        private Encoding encoding = Encoding.UTF8;
        byte[] dataRcvBuf = new byte[1024];
        Form1 fm1;
        Scorpion.Form1 Do_on;

        public void Close_Connection()
        {
            sock.Shutdown(SocketShutdown.Both);
            sock.Close();

            return;
        }

        public Connection(Socket s, Form1 fm, Scorpion.Form1 fm12)
        {
            fm1 = fm;
            Do_on = fm12;
            this.sock = s;
            // Start listening for incoming data.  (If you want a multi-
            // threaded service, you can start this method up in a separate
            // thread.)
            this.BeginReceive();
        }

        // Call this method to set this connection's socket up to receive data.
        private void BeginReceive()
        {
            fm1.al_messages.Add("Listening to: " + sock.LocalEndPoint.ToString());
            this.sock.BeginReceive(
                    this.dataRcvBuf, 0,
                    this.dataRcvBuf.Length,
                    SocketFlags.None,
                    new AsyncCallback(this.OnBytesReceived),
                    this);
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

        // This is the method that is called whenever the socket receives
        // incoming bytes.
        string s;
        protected void OnBytesReceived(IAsyncResult result)
        {
            fm1.write_to_cui("Recieved data from TCP/IP");
            try
            {
                // End the data receiving that the socket has done and get
                // the number of bytes read.
                int nBytesRec = this.sock.EndReceive(result);
                // If no bytes were received, the connection is closed (at
                // least as far as we're concerned).
                if (nBytesRec <= 0)
                {
                    this.sock.Close();
                    return;
                }
                // Convert the data we have to a string.
                //string strReceived = this.encoding.GetString(this.dataRcvBuf, 0, nBytesRec);

                this.sock.BeginReceive(
                    this.dataRcvBuf, 0,
                    this.dataRcvBuf.Length,
                    SocketFlags.None,
                    new AsyncCallback(this.OnBytesReceived),
                    this);

                //string g = UnicodeEncoding.Unicode.GetString(this.dataRcvBuf);
                //MessageBox.Show(g);

                System.Collections.ArrayList al = (System.Collections.ArrayList)ByteArrayToObject(this.dataRcvBuf);
                Do_on.readr.access_library(al[0].ToString());
                try
                {
                    //int o = sock.Send(dataRcvBuf);
                    /*foreach (object s in al)
                    {
                        fm1.write_to_cui("Recieved:" + s.ToString());
                    }*/
                }
                catch { fm1.write_to_cui(UnicodeEncoding.Unicode.GetString(this.dataRcvBuf)); }
            }
            catch { fm1.write_to_cui("CONNTERM: A client/command exited without closing the connection properly."); }
            return;
        }
    }
}
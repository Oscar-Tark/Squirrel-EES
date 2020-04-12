using System;
using System.ComponentModel;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class Librarian
    {
        public void socket(string Scorp_Line_Exec, ArrayList objects)
        {
            /*
             * NAME,           
             * IP,
             * PORT           
            */
            ConnectionFunctions _cf = new ConnectionFunctions(Do_on);
            string name = (string)Do_on.readr.lib_SCR.var_get((string)objects[0]);
            string ip_address = (string)Do_on.readr.lib_SCR.var_get((string)objects[1]);
            Int16 port = Convert.ToInt16(Do_on.readr.lib_SCR.var_get((string)objects[2]));
            try
            {
                SocketPermission permission = new SocketPermission(NetworkAccess.Connect, TransportType.Tcp, ip_address, SocketPermission.AllPorts);

                IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse(ip_address));
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipep = new IPEndPoint(ipAddr, port);

                Socket SOCK = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                SOCK.Bind(ipep);
                SOCK.Listen(10);
                SOCK.BeginAccept(_cf.Accept, SOCK);

                Do_on.AL_SOCK.Add(SOCK);
                Do_on.AL_SOCK_REF.Add(name);
                Do_on.AL_SOCK_SESSION.Add(new ArrayList());
            }
            catch(Exception eryy) { Do_on.write_to_cui(eryy.Message); }
            return;
        }

        public void socketclose(string Scorp_Line_Exec, ArrayList objects)
        {
            /*
             * NAME           
            */
            try
            {
                ((Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf((string)var_get((string)objects[0]))]).Shutdown(SocketShutdown.Both);
                ((Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf((string)var_get((string)objects[0]))]).Close();

                Do_on.AL_SOCK.RemoveAt(Do_on.AL_SOCK_REF.IndexOf((string)var_get((string)objects[0])));
                Do_on.AL_SOCK_SESSION.RemoveAt(Do_on.AL_SOCK_REF.IndexOf((string)var_get((string)objects[0])));
                Do_on.AL_SOCK_REF.RemoveAt(Do_on.AL_SOCK_REF.IndexOf((string)var_get((string)objects[0])));

                Do_on.write_to_cui("Socket " + (string)var_get((string)objects[0]) + " closed");
            }
            catch(Exception erty) { Do_on.write_to_cui(erty.Message); }
            return;
        }

        public void listsockets(string Scorp_Line_Exec, ArrayList objects)
        {
            foreach(string s in Do_on.AL_SOCK_REF)
            {
                Do_on.write_to_cui(s);
            }
            return;
        }
    }

    class ConnectionFunctions
    {
        Form1 fm1;
         
        public ConnectionFunctions(Form1 fm1_)
        { fm1 = fm1_; return; }

        public void Accept(IAsyncResult result)
        {
            fm1.write_to_cui("ACCEPTED A REMOTE CONNECTION");
            Socket SOCK = (Socket)result.AsyncState;
            Socket END_SOCK = SOCK.EndAccept(result);

            StateObject _so = new StateObject();
            _so.workSocket = END_SOCK;

            END_SOCK.BeginReceive(_so.buffer, 0, StateObject.BufferSize, 0, Read, _so);
            return;
        }

        public void Read(IAsyncResult result)
        {
            String scorpion = String.Empty;
            String HTTP = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)result.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.
            int bytesRead = handler.EndReceive(result);

            if (bytesRead > 0)
            {
                HTTP = Encoding.ASCII.GetString(state.buffer, 0, bytesRead);

                if (HTTP.IndexOf("<EOF>") > -1)
                {
                    fm1.write_to_cui("REMOTE DATA RECIEVED: \n" + HTTP);
                    //scorpion = HTTP.Remove(0, HTTP.IndexOf("scorpion",StringComparison.CurrentCulture));
                    //fm1.write_to_cui("COMMAND: " + scorpion);
                    Send(handler, "HTTP 1.0 OK::SCORPION OK\r\n");
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(Read), state);
                    Send(handler, "HTTP 1.0 OK::SCORPION OK\r\n");
                }
            }
            return;
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }

    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
}

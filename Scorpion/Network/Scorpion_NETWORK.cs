/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

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
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security;

namespace Scorpion
{
    public partial class Librarian
    {
        public string getselfip(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable<<::
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            // Get the IP  
            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);

            return var_create_return(ref IP, true);
        }

        //UNENCRYPTED SERVER
        public void serverstartphpapi(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //                ![DEBUG]
            //::*name, *port, *rsaprivatekeyfilepath, *rsapublickeyfilepath
            //::*name, *port, *rsaprivatekey, *rsapublickey
            //Check and add RSA key, if cannot do not continue
            string name = (string)var_get(objects[0]);
            int port = Convert.ToInt32(var_get(objects[1]));

            SimpleTCP.SimpleTcpServer sctl = new SimpleTCP.SimpleTcpServer();
            sctl.ClientConnected += Sctl_ClientConnected;
            sctl.ClientDisconnected += Sctl_ClientDisconnected;
            sctl.DataReceived += Sctl_DataReceived_phpapi;
            sctl.Start(port, true);
            Do_on.AL_TCP.Add(sctl);
            Do_on.AL_TCP_REF.Add(name + "_phpapi");
            Do_on.write_warning("Scorpion PHP API server started. You do like to live dangerously :o. Non RSA servers can be read by MIM attacks and other sniffing techniques");
            write_to_console("TCP server started");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref name);
            var_arraylist_dispose(ref objects);
            return;
        }

        //ENCRYPTED SERVER
        public void serverstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //                ![DEBUG]
            //::*name, *port, *rsaprivatekeyfilepath, *rsapublickeyfilepath
            //::*name, *port, *rsaprivatekey, *rsapublickey
            //Check and add RSA key, if cannot do not continue
            string name = (string)var_get(objects[0]);
            int port = Convert.ToInt32(var_get(objects[1]));
            string RSA_private_path = (string)var_get(objects[2]);
            string RSA_public_path = (string)var_get(objects[3]);

            if (File.Exists(RSA_private_path) && File.Exists(RSA_public_path))
            {
                SimpleTCP.SimpleTcpServer sctl = new SimpleTCP.SimpleTcpServer();
                sctl.ClientConnected += Sctl_ClientConnected;
                sctl.ClientDisconnected += Sctl_ClientDisconnected;
                sctl.DataReceived += Sctl_DataReceived;
                sctl.Start(port, true);
                Do_on.AL_TCP.Add(sctl);
                Do_on.AL_TCP_REF.Add(name);
                Do_on.add_tcp_key_path(RSA_private_path, RSA_public_path);
                write_to_console("TCP server started");
            }
            else
                Do_on.write_error("Unable to start TCP server. The supplied path for a private RSA key is not valid");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref name);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void serverstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            int index = Do_on.AL_TCP_REF.IndexOf(var_get(objects[0]));
            ((SimpleTCP.SimpleTcpServer)Do_on.AL_TCP[index]).Stop();
            ((SimpleTCP.SimpleTcpServer)Do_on.AL_TCP[index]).DataReceived -= Sctl_DataReceived;
            Do_on.AL_TCP_REF.RemoveAt(index);
            Do_on.remove_tcp_key_path(ref index);
            write_to_console("TCP server stopped");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void serversend(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *data
            byte[] data = null;
            try
            {
                int server_index = Do_on.AL_TCP_REF.IndexOf(var_get(objects[0]));
                ((SimpleTCP.SimpleTcpServer)Do_on.AL_TCP[server_index]).StringEncoder = Encoding.UTF8;
                ((SimpleTCP.SimpleTcpServer)Do_on.AL_TCP[server_index]).Delimiter = 0x13;

                Do_on.write_debug(Do_on.get_tcp_key_paths(server_index)[1]);

                data = Scorpion_RSA.Scorpion_RSA.encrypt_data((string)var_get(objects[1]), Do_on.get_tcp_key_paths(server_index)[1]);
                ((SimpleTCP.SimpleTcpServer)Do_on.AL_TCP[server_index]).Broadcast(data);
                Do_on.write_success("Data sent");
            }
            catch (Exception e) { Do_on.write_error(e.Message); }

            var_dispose_internal(ref data);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void listservers(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::
            foreach (string server in Do_on.AL_TCP_REF)
                Do_on.write_to_cui(server);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //SERVER
        //RSA
        void Sctl_DataReceived(object sender, SimpleTCP.Message e)
        {
            //get private key and decrypt
            int server_index = Do_on.AL_TCP.IndexOf(sender);
            byte[] data = Scorpion_RSA.Scorpion_RSA.decrypt_data(e.Data, Scorpion_RSA.Scorpion_RSA.get_private_key_file(Do_on.get_tcp_key_paths(server_index)[0]));
            string s_data = Do_on.crypto.To_String(data);

            //Removes delimiter 0x13 and executes
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(ef__.replace_telnet(s_data));
            scorpioniee(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));
            ef__ = null;
            return;
        }

        void Sctl_ClientDisconnected(object sender, TcpClient e)
        {
            write_to_console("Client " + (IPEndPoint)e.Client.RemoteEndPoint + " disconnected");
            return;
        }

        void Sctl_ClientConnected(object sender, TcpClient e)
        {
            write_to_console("Client " + (IPEndPoint)e.Client.RemoteEndPoint + " connected");
            return;
        }

        //NO RSA
        void Sctl_DataReceived_phpapi(object sender, SimpleTCP.Message e)
        {
            int server_index = Do_on.AL_TCP.IndexOf(sender);
            //Removes delimiter 0x13 and executes
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(ef__.replace_telnet(e.MessageString));
            command = ef__.replace_phpapi(command);

            if (e.MessageString.Contains("GET /"))
            {
                e.ReplyLine("HTTP / 1.1 200 OK\n\n" + (string)var_get((string)command));
                e.TcpClient.Client.Disconnect(true);
                return;
            }
            scorpioniee(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));

            ef__ = null;
            return;
        }
        //<--

        public void clientstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *ip, *port, *private, *publicrsakey
            SimpleTCP.SimpleTcpClient sctl = new SimpleTCP.SimpleTcpClient();
            sctl.Connect((string)var_get(objects[1]), Convert.ToInt32(var_get(objects[2])));
            sctl.DataReceived += Sctl_clientDataReceived;

            string client = (string)var_get(objects[0]);
            string public_key = (string)var_get(objects[4]);
            string private_key = (string)var_get(objects[3]);

            Do_on.write_success("Client " + client + " connected to " + var_get(objects[1]) + ":" + var_get(objects[2]));
            Do_on.mem.add_tcpclient(ref client, ref sctl, ref private_key, ref public_key);

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        //CLIENT
        void Sctl_clientDataReceived(object sender, SimpleTCP.Message e)
        {
            //get private key and decrypt
            int client = Do_on.mem.get_index_tcpclient(sender);
            string key_path = Do_on.mem.get_tcpclient_key_paths(client)[0];
            SecureString key = Scorpion_RSA.Scorpion_RSA.get_private_key_file(key_path);
            byte[] data = Scorpion_RSA.Scorpion_RSA.decrypt_data(e.Data, key);
            string s_data = Do_on.crypto.To_String(data);

            //Removes delimiter 0x13 and executes
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(ef__.replace_telnet(s_data));
            scorpioniee(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));
            ef__ = null;
            return;
        }

        public void clientsend(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *data
            byte[] data = null;
            try
            {
                int client_ndx = Do_on.mem.get_index_tcpclient((string)var_get(objects[0]));
                SimpleTCP.SimpleTcpClient tcl = Do_on.mem.get_tcpclient(client_ndx);
                tcl.StringEncoder = Encoding.UTF8;
                tcl.Delimiter = 0x13;
                Do_on.write_debug(Do_on.mem.get_tcpclient_key_paths(client_ndx)[1]);

                data = Scorpion_RSA.Scorpion_RSA.encrypt_data((string)var_get(objects[1]), Do_on.mem.get_tcpclient_key_paths(client_ndx)[1]);
                tcl.Write(data);
                Do_on.write_success("Data sent");
            }
            catch (Exception e) { Do_on.write_error(e.Message); };

            var_dispose_internal(ref data);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void clientstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            Do_on.mem.remove_tcpclient(Do_on.mem.get_index_tcpclient((string)var_get(objects[0])));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        //LEGACY SOCKETS
        /*public void socket(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*name, *ip, *port
            /*
             * NAME,           
             * IP,
             * PORT           
            
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
            //::*name
            try
            {
                ((Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0]))]).Shutdown(SocketShutdown.Receive);
                //((Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0]))]).Disconnect(false);
                ((Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0]))]).Close();

                Do_on.AL_SOCK.RemoveAt(Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0])));
                Do_on.AL_SOCK_SESSION.RemoveAt(Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0])));
                Do_on.AL_SOCK_REF.RemoveAt(Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0])));

                Do_on.write_to_cui("Socket " + (string)var_get((string)objects[0]) + " closed");
            }
            catch (Exception erty) { Do_on.write_to_cui(erty.Message); }
            return;
        }

        public void socketsend(ref string Scorp_Line_Exec, ArrayList objects)
        {
            ((Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf((string)var_get(objects[0]))]).Send(new byte[] { 0x64, 0x64, 0x00 });

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);

            return;
        }

        public void listsockets(string Scorp_Line_Exec, ArrayList objects)
        {
            foreach(string s in Do_on.AL_SOCK_REF)
            {
                Do_on.write_to_cui(s);
            }
            return;
        }*/
    }

    class ConnectionFunctions
    {
        Scorp fm1;
        public ConnectionFunctions(Scorp fm1_)
        { fm1 = fm1_; return; }

        public void Accept(IAsyncResult result)
        {
            Socket SOCK = (Socket)result.AsyncState;
            Socket END_SOCK = SOCK.EndAccept(result);
            fm1.write_to_cui("Connection accepted provenent from " + ((IPEndPoint)END_SOCK.RemoteEndPoint).Address + ":" + ((IPEndPoint)END_SOCK.RemoteEndPoint).Port);

            StateObject _so = new StateObject();
            _so.workSocket = END_SOCK;

            END_SOCK.BeginReceive(_so.buffer, 0, StateObject.BufferSize, 0, Read, _so);
            return;
        }

        public void Read(IAsyncResult result)
        {
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
                Send(handler, "HTTP 1.0 OK::SCORPION OK\r\n");
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(Read), state);

                HTTP = HTTP.Replace("\r", "");
                HTTP = HTTP.Replace("\n", "");
                fm1.readr.lib_SCR.scorpioniee(HTTP);
                fm1.write_to_cui("Remote execute from " + ((IPEndPoint)handler.RemoteEndPoint).Address + ":" + ((IPEndPoint)handler.RemoteEndPoint).Port + ": \n" + HTTP);
            }
            return;
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
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

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();
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

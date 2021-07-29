using System;
using System.Net;
using System.Net.Sockets;
using System.Security;

namespace Scorpion
{
    public class SESSION_DEPENDENT_HANDLERS
    {
        Scorp HANDLE;
        public SESSION_DEPENDENT_HANDLERS(Scorp HANDLE_PASS)
        {
            HANDLE = HANDLE_PASS;
            return;
        }

        /*TCP server*/
        public void add_tcpserver(string reference, int port, string RSA_private_path, string RSA_public_path)
        {
            SimpleTCP.SimpleTcpServer sctl = new SimpleTCP.SimpleTcpServer();
            sctl.ClientConnected += Sctl_ClientConnected;
            sctl.ClientDisconnected += Sctl_ClientDisconnected;
            lock (HANDLE.mem.AL_TCP) lock (HANDLE.mem.AL_TCP_REF)
                {
                    HANDLE.mem.AL_TCP.Add(sctl);
                    HANDLE.mem.AL_TCP_REF.Add(reference);
                    if (RSA_private_path == null && RSA_public_path == null)
                    {
                        HANDLE.mem.add_tcp_key_path(null, null);
                        sctl.DataReceived += Sctl_DataReceived_noencrypt;
                    }
                    else
                    {
                        HANDLE.mem.add_tcp_key_path(RSA_private_path, RSA_public_path);
                        sctl.DataReceived += Sctl_DataReceived;
                    }
                    sctl.Start(port, true);
                }
            return;
        }

        /*TCP server : Events*/
        void Sctl_ClientConnected(object sender, TcpClient e)
        {
            HANDLE.write_to_cui("Client " + (IPEndPoint)e.Client.RemoteEndPoint + " connected");
            return;
        }

        void Sctl_ClientDisconnected(object sender, TcpClient e)
        {
            HANDLE.write_to_cui("Client " + (IPEndPoint)e.Client.RemoteEndPoint + " disconnected");
            return;
        }

        public void Sctl_DataReceived(object sender, SimpleTCP.Message e)
        {
            //get private key and decrypt
            int server_index = HANDLE.mem.AL_TCP.IndexOf(sender);
            byte[] data = Scorpion_RSA.Scorpion_RSA.decrypt_data(e.Data, Scorpion_RSA.Scorpion_RSA.get_private_key_file(HANDLE.mem.get_tcp_key_paths(server_index)[0]));
            string s_data = HANDLE.crypto.To_String(data);

            //Removes delimiter 0x13 and executes
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(ef__.replace_telnet(s_data));
            HANDLE.readr.access_library(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));
            ef__ = null;
            return;
        }

        void Sctl_DataReceived_noencrypt(object sender, SimpleTCP.Message e)
        {
            //Add RSA support
            //Removes delimiter 0x13 and executes
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(ef__.replace_telnet(e.MessageString));
            command = ef__.replace_phpapi(command);
            if (e.MessageString.Contains("GET /"))
            {
                //Not getting according to seesion
                e.ReplyLine("HTTP / 1.1 200 OK\n\n" + (string)HANDLE.readr.lib_SCR.var_get(ref command));
                e.TcpClient.Client.Disconnect(true);
                return;
            }
            command = command.TrimEnd(new char[] { Convert.ToChar(0x13) });
            string[] commands = command.Split(new char[] { '\n' });
            foreach (string s_dat in commands)
                HANDLE.readr.access_library(s_dat);
            e.ReplyLine("HTTP / 1.1 200 OK\n\n COMMANDS EXECUTED [Commands can fail on networked connections without warning!]");
            //e.TcpClient.Client.Disconnect(true);
            ef__ = null;
            return;
        }

        //TCP CLIENT
        public SimpleTCP.SimpleTcpClient get_tcpclient(int ndx)
        {
            return (SimpleTCP.SimpleTcpClient)HANDLE.mem.AL_TCP_CLIENTS[ndx];
        }

        public int get_index_tcpclient(object client)
        {
            return HANDLE.mem.AL_TCP_CLIENTS.IndexOf(client);
        }

        public int get_index_tcpclient(string client)
        {
            return HANDLE.mem.AL_TCP_CLIENTS_REF.IndexOf(client);
        }

        public string[] get_tcpclient_key_paths(int ndx)
        {
            return (string[])HANDLE.mem.AL_TCP_CLIENTS_KY[ndx];
        }

        public void remove_tcpclient(int ndx)
        {
            lock (HANDLE.mem.AL_TCP_CLIENTS) lock (HANDLE.mem.AL_TCP_CLIENTS_REF) lock (HANDLE.mem.AL_TCP_CLIENTS_KY)
                    {
                        HANDLE.mem.AL_TCP_CLIENTS.RemoveAt(ndx);
                        HANDLE.mem.AL_TCP_CLIENTS_KY.RemoveAt(ndx);
                        HANDLE.mem.AL_TCP_CLIENTS_REF.RemoveAt(ndx);
                    }
            return;
        }

        //TCP client
        //FIX DISCREPANCIES
        public void add_tcpclient(string reference, string ip, int port, string private_key_path, string public_key_path)
        {
            //::*name, *ip, *port, *private, *publicrsakey
            SimpleTCP.SimpleTcpClient sctl = new SimpleTCP.SimpleTcpClient();
            sctl.Connect(ip, port);
            sctl.DataReceived += Sctl_clientDataReceived;
            lock (HANDLE.mem.AL_TCP_CLIENTS) lock (HANDLE.mem.AL_TCP_CLIENTS_REF) lock (HANDLE.mem.AL_TCP_CLIENTS_KY)
                    {
                        HANDLE.mem.AL_TCP_CLIENTS.Add(sctl);
                        HANDLE.mem.AL_TCP_CLIENTS_REF.Add(reference);
                        HANDLE.mem.AL_TCP_CLIENTS_KY.Add(new string[] { private_key_path, public_key_path });
                    }
            HANDLE.write_success("Client " + reference + " connected to " + ip + ":" + port);
            return;
        }

        //CLIENT
        void Sctl_clientDataReceived(object sender, SimpleTCP.Message e)
        {
            //get private key and decrypt
            int client = get_index_tcpclient(sender);
            string key_path = get_tcpclient_key_paths(client)[0];
            SecureString key = Scorpion_RSA.Scorpion_RSA.get_private_key_file(key_path);
            byte[] data = Scorpion_RSA.Scorpion_RSA.decrypt_data(e.Data, key);
            string s_data = HANDLE.crypto.To_String(data);

            //Removes delimiter 0x13 and executes
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(ef__.replace_telnet(s_data));
            HANDLE.readr.access_library(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));
            ef__ = null;
            return;
        }
    }
}
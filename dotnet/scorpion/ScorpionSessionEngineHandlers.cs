/*  <Scorpion Server>
    Copyright (C) <2022+>  <Oscar Arjun Singh Tark>

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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;

namespace Scorpion
{
    public class SessionDependentNetworkHandlers
    {
        private Scorp HANDLE;
        public SessionDependentNetworkHandlers(Scorp HANDLE_PASS)
        {
            HANDLE = HANDLE_PASS;
            return;
        }

        /*TCP server*/
        public void AddTcpServer(string reference, string ip, int port, string RSA_private_path, string RSA_public_path)
        {
            //To be depreciated soon with scorpion_P2P use [DEPRECIATED] ontop of the function definition when done.

            SimpleTCP.SimpleTcpServer sctl = new SimpleTCP.SimpleTcpServer();
            sctl.ClientConnected += Sctl_ClientConnected;
            sctl.ClientDisconnected += Sctl_ClientDisconnected;
            lock (HANDLE.mem.AL_TCP) lock (HANDLE.mem.AL_TCP_REF)
                {
                    HANDLE.mem.AL_TCP.Add(sctl);
                    HANDLE.mem.AL_TCP_REF.Add(reference);
                    HANDLE.mem.AddTcpPath(RSA_private_path, RSA_public_path);

                    if(RSA_public_path == null || RSA_private_path == null)
                        HANDLE.write_warning("Scorpion server started. No RSA keys have been assigned to this server. Non RSA servers can be read by MITM attacks and other sniffing techniques");

                    sctl.DataReceived += Sctl_DataReceived;
                    IPAddress ipa = IPAddress.Parse(ip == HANDLE.types.S_NULL ? "127.0.0.1" : ip);
                    sctl.Start(ipa, port);//(port, true);
                }
            return;
        }

        /*TCP server : Events*/
        void Sctl_ClientConnected(object sender, TcpClient e)
        {
            HANDLE.write_to_cui("TCP >> Client " + (IPEndPoint)e.Client.RemoteEndPoint + " connected");
            return;
        }

        void Sctl_ClientDisconnected(object sender, TcpClient e)
        {
            HANDLE.write_to_cui("TCP >> Client " + (IPEndPoint)e.Client.RemoteEndPoint + " disconnected");
            return;
        }

        public void Sctl_DataReceived(object sender, SimpleTCP.Message e)
        {
            //Add RSA support
            //Removes delimiter 0x13 and executes
            /*
            {&scorpion}{&type}type{&/type}{&database}db{&/database}{&tag}tag{&/tag}{&subtag}subtag{&/subtag}{&session}session{&/session}{&/scorpion}
            tag:project_folder*/
            
            Enginefunctions ef__ = new Enginefunctions();
            NetworkEngineFunctions nef__ = new NetworkEngineFunctions();
            int server_index = HANDLE.mem.AL_TCP.IndexOf(sender);
            byte[] data = e.Data;
            //string session = HANDLE.types.S_NULL;
            string reply = null;
            IPEndPoint end_point = (IPEndPoint)e.TcpClient.Client.RemoteEndPoint;

            //Check if RSA
            if (HANDLE.mem.GetTcpKeyPath(server_index)[0] != null)
                data = Scorpion_RSA.Scorpion_RSA.decrypt_data(e.Data, Scorpion_RSA.Scorpion_RSA.get_private_key_file(HANDLE.mem.GetTcpKeyPath(server_index)[0]));

            //Btyte->string, Parse string
            string s_data = HANDLE.crypto.To_String(data);
            string command = ef__.replace_fakes(nef__.replace_telnet(s_data));
            Dictionary<string, string> processed = nef__.replace_api(command);

            //Get session from parsed elements
            string session = processed["session"];
            string destroyable = "\0";
            string db_page = HANDLE.types.S_NULL;

            try
            {
                if (processed != null)
                {
                    //Is there a session? if not create a session dictionary to contain session variables for the user, only GET regularly needs a user variable
                    if(!HANDLE.readr.lib_SCR.varCheck(session))
                    {
                        ArrayList temp = new ArrayList(){ session };
                        HANDLE.readr.lib_SCR.vardictionary(ref destroyable, ref temp);
                        temp = new ArrayList(){ session, "\'session\'", $"\'{session}\'" };
                        HANDLE.readr.lib_SCR.vardictionaryappend(ref destroyable, ref temp);
                        temp = new ArrayList(){ session, "\'project\'", "\'" + processed["tag"] + "\'" };
                        HANDLE.readr.lib_SCR.vardictionaryappend(ref destroyable, ref temp);
                    }

                    if (processed["type"] == nef__.api_requests["get"])
                    {
                        //Get formattable page from XMLDB
                        ArrayList query_result = HANDLE.vds.doDBSelectiveNoThread(processed["db"], null, processed["tag"], processed["subtag"], HANDLE.vds.OPCODE_GET);

                        if (query_result.Count > 0)
                        {
                            db_page = (string)query_result[0];

                            //Session allows us to return the page with user loaded session data
                            if(HANDLE.mem.AL_CURR_VAR_REF.Contains(session))
                                reply = nef__.build_api((string)HANDLE.readr.lib_SCR.varGetCustomFormattedOnlyDictionary(ref db_page, ref session), session, false);
                            else
                                reply = nef__.build_api((string)db_page, HANDLE.types.S_NULL, false);
                        }
                        else
                            reply = nef__.build_api("Query resulted in 0 elements returned", session, true);
                    }
                    else if (processed["type"] == nef__.api_requests["set"])
                    {
                        command = command.TrimEnd(new char[] { Convert.ToChar(0x13) });
                        string[] commands = command.Split(new char[] { '\n' });
                        foreach (string s_dat in commands)
                            HANDLE.readr.access_library(s_dat);
                        reply = nef__.build_api("Command executed", session, false);
                    }
                    else if (processed["type"] == nef__.api_requests["delete"])
                    {

                        //Delete a session on request from the HTTP server (Time based)
                    }
                }
                else
                    reply = nef__.build_api("Command error. Incorrect syntax", "", true);

                //RSA then encrypt and send
                if (reply != null && HANDLE.mem.GetTcpKeyPath(server_index)[0] != null)
                    e.Reply(Scorpion_RSA.Scorpion_RSA.encrypt_data(reply, Scorpion_RSA.Scorpion_RSA.get_public_key_file(HANDLE.mem.GetTcpKeyPath(server_index)[0])));

                //No RSA then just send
                if (reply != null && HANDLE.mem.GetTcpKeyPath(server_index)[0] == null)
                    e.Reply(reply);
            }
            catch(Exception er){ HANDLE.write_error(string.Format("Fatal Error for {0}. Closing connection", end_point)); HANDLE.write_error(er.StackTrace); HANDLE.write_error(er.Message); }
            finally
            {
                e.TcpClient.Client.Disconnect(true);
            }

            ef__ = null;
            nef__ = null;
            return;
        }

        //TCP CLIENT
        public SimpleTCP.SimpleTcpClient GetClient(int ndx)
        {
            return (SimpleTCP.SimpleTcpClient)HANDLE.mem.AL_TCP_CLIENTS[ndx];
        }

        public int GetClientIndex(object client)
        {
            return HANDLE.mem.AL_TCP_CLIENTS.IndexOf(client);
        }

        public int GetIndexTcpClient(string client)
        {
            return HANDLE.mem.AL_TCP_CLIENTS_REF.IndexOf(client);
        }

        public string[] GetClientKeyPaths(int ndx)
        {
            return (string[])HANDLE.mem.AL_TCP_CLIENTS_KY[ndx];
        }

        public void RemoveTcpClient(int ndx)
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
        public void AddTcpClient(string reference, string ip, int port, string private_key_path, string public_key_path)
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
            int client = GetClientIndex(sender);
            string key_path = GetClientKeyPaths(client)[0];
            SecureString key = Scorpion_RSA.Scorpion_RSA.get_private_key_file(key_path);
            byte[] data = Scorpion_RSA.Scorpion_RSA.decrypt_data(e.Data, key);
            string s_data = HANDLE.crypto.To_String(data);

            //Removes delimiter 0x13 and executes
            NetworkEngineFunctions nef__ = new NetworkEngineFunctions();
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(nef__.replace_telnet(s_data));
            HANDLE.readr.access_library(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));
            ef__ = null;
            nef__ = null;
            return;
        }
    }
}
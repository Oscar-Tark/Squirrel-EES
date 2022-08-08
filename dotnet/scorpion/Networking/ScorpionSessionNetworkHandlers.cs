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

//If sessions are allowed. this class allows functionality not to be shared between sessions that are crucial in being seperate from the main partial Librarian class

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Threading;
using System.Text;

namespace Scorpion
{
    public class SessionDependentNetworkHandlers
    {
        /*TCP server*/
        public void AddTcpServer(string reference, string ip, int port, string RSA_private_path, string RSA_public_path)
        {
            //To be depreciated soon with scorpion_P2P use [DEPRECIATED] ontop of the function definition when done.

            SimpleTCP.SimpleTcpServer sctl = new SimpleTCP.SimpleTcpServer();
            sctl.ClientConnected += Sctl_ClientConnected;
            sctl.ClientDisconnected += Sctl_ClientDisconnected;
            lock (Types.HANDLE.mem.AL_TCP) lock (Types.HANDLE.mem.AL_TCP_REF)
                {
                    Types.HANDLE.mem.AL_TCP.Add(sctl);
                    Types.HANDLE.mem.AL_TCP_REF.Add(reference);
                    Types.HANDLE.mem.AddTcpPath(RSA_private_path, RSA_public_path);

                    if(RSA_public_path == null || RSA_private_path == null)
                        ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("Scorpion server started. No RSA keys have been assigned to this server. Non RSA servers can be read by MITM attacks and other sniffing techniques");

                    sctl.DataReceived += Sctl_DataReceived;
                    IPAddress ipa = IPAddress.Parse(ip == Types.S_NULL ? "127.0.0.1" : ip);
                    sctl.Start(ipa, port);//(port, true);
                }
            return;
        }

        /*TCP server : Events*/
        void Sctl_ClientConnected(object sender, TcpClient e)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("TCP >> Client " + (IPEndPoint)e.Client.RemoteEndPoint + " connected");
            return;
        }

        void Sctl_ClientDisconnected(object sender, TcpClient e)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("TCP >> Client " + (IPEndPoint)e.Client.RemoteEndPoint + " disconnected");
            return;
        }

        public void Sctl_DataReceived(object sender, SimpleTCP.Message e)
        {
            Thread ths = new Thread(new ParameterizedThreadStart(processServerRequest));
            ths.IsBackground = true;
            ths.Start(new object[2]{ sender, e });
        }

        private void processServerRequest(object param_thread_object)
        {
            //Add RSA support
            //Removes delimiter 0x13 and executes
            /*
            {&scorpion}{&type}type{&/type}{&database}db{&/database}{&tag}tag{&/tag}{&subtag}subtag{&/subtag}{&session}session{&/session}{&/scorpion}
            tag:project_folder*/

            object sender = ((object[])param_thread_object)[0];
            SimpleTCP.Message e = (SimpleTCP.Message)((object[])param_thread_object)[1];
            
            Enginefunctions ef__ = new Enginefunctions();
            NetworkEngineFunctions nef__ = new NetworkEngineFunctions();
            int server_index = Types.HANDLE.mem.AL_TCP.IndexOf(sender);
            byte[] data = e.Data;
            //string session = Types.S_NULL;
            string reply = null;
            IPEndPoint end_point = (IPEndPoint)e.TcpClient.Client.RemoteEndPoint;

            //Check if RSA
            if (Types.HANDLE.mem.GetTcpKeyPath(server_index)[0] != null)
                data = Scorpion_RSA.ScorpionRSAMin.decrypt(e.Data, Types.HANDLE.mem.GetTcpKeyPath(server_index)[0]);

            //Btyte->string, Parse string
            string s_data = Types.HANDLE.crypto.To_String(data);
            string command = ef__.replace_fakes(nef__.replace_telnet(s_data));
            Dictionary<string, string> processed = nef__.replace_api(command);

            //Get session from parsed elements
            string session = processed["session"];
            string destroyable = "\0";
            string db_page = Types.S_NULL;

            try
            {
                if (processed != null)
                {
                    //Is there a session? if not create a session dictionary to contain session variables for the user, only GET regularly needs a user variable
                    if(!Types.HANDLE.librarian_instance.librarian.varCheck(session))
                    {
                        ArrayList temp = new ArrayList(){ session };
                        Types.HANDLE.librarian_instance.librarian.vardictionary(ref destroyable, ref temp);
                        temp = new ArrayList(){ session, "\'session\'", $"\'{session}\'" };
                        Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
                        temp = new ArrayList(){ session, "\'project\'", "\'" + processed["tag"] + "\'" };
                        Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
                    }

                    if (processed["type"] == nef__.api_requests["get"])
                    {
                        //Get formattable page from XMLDB
                        ArrayList query_result = Types.HANDLE.vds.doDBSelectiveNoThread(processed["db"], null, processed["tag"], processed["subtag"], Types.HANDLE.vds.OPCODE_GET);

                        if (query_result.Count > 0)
                        {
                            db_page = (string)query_result[0];

                            //Get mysql dat
                            

                            //Session allows us to return the page with user loaded session data
                            if(Types.HANDLE.mem.AL_CURR_VAR_REF.Contains(session))
                                reply = nef__.build_api((string)Types.HANDLE.librarian_instance.librarian.varGetCustomFormattedOnlyDictionary(ref db_page, ref session), session, false);
                            else
                                reply = nef__.build_api((string)db_page, Types.S_NULL, false);
                        }
                        else
                            reply = nef__.build_api("Query resulted in 0 elements returned", session, true);
                    }
                    else if (processed["type"] == nef__.api_requests["set"])
                    {
                        command = command.TrimEnd(new char[] { Convert.ToChar(0x13) });
                        string[] commands = command.Split(new char[] { '\n' });
                        foreach (string s_dat in commands)
                            Types.HANDLE.librarian_instance.librarian.scorpioniee(s_dat);
                        reply = nef__.build_api("Command executed", session, false);
                    }
                    else if (processed["type"] == nef__.api_requests["delete"])
                    {

                        //Delete a session on request from the HTTP server (Time based)
                    }
                }
                else
                    reply = nef__.build_api("Command error. Incorrect syntax", "", true);

                //RSA then encrypt and send as byte[]... CHANGE TO AES
                if (reply != null && Types.HANDLE.mem.GetTcpKeyPath(server_index)[0] != null)
                    e.Reply(ScorpionAES.ScorpionAES.encryptData(reply, "test"));
                    //e.Reply(Scorpion_RSA.ScorpionRSAMin.encrypt(Encoding.ASCII.GetBytes(reply), Types.HANDLE.mem.GetTcpKeyPath(server_index)[1]));

                //No RSA then just send as string
                if (reply != null && Types.HANDLE.mem.GetTcpKeyPath(server_index)[0] == null)
                    e.Reply(reply);
            }
            catch(Exception er){ ScorpionConsoleReadWrite.ConsoleWrite.writeError(string.Format("Fatal Error for {0}. Closing connection", end_point)); ScorpionConsoleReadWrite.ConsoleWrite.writeError(er.StackTrace); ScorpionConsoleReadWrite.ConsoleWrite.writeError(er.Message); }
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
            return (SimpleTCP.SimpleTcpClient)Types.HANDLE.mem.AL_TCP_CLIENTS[ndx];
        }

        public int GetClientIndex(object client)
        {
            return Types.HANDLE.mem.AL_TCP_CLIENTS.IndexOf(client);
        }

        public int GetIndexTcpClient(string client)
        {
            return Types.HANDLE.mem.AL_TCP_CLIENTS_REF.IndexOf(client);
        }

        public string[] GetClientKeyPaths(int ndx)
        {
            return (string[])Types.HANDLE.mem.AL_TCP_CLIENTS_KY[ndx];
        }

        public void RemoveTcpClient(int ndx)
        {
            lock (Types.HANDLE.mem.AL_TCP_CLIENTS) lock (Types.HANDLE.mem.AL_TCP_CLIENTS_REF) lock (Types.HANDLE.mem.AL_TCP_CLIENTS_KY)
                    {
                        Types.HANDLE.mem.AL_TCP_CLIENTS.RemoveAt(ndx);
                        Types.HANDLE.mem.AL_TCP_CLIENTS_KY.RemoveAt(ndx);
                        Types.HANDLE.mem.AL_TCP_CLIENTS_REF.RemoveAt(ndx);
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
            lock (Types.HANDLE.mem.AL_TCP_CLIENTS) lock (Types.HANDLE.mem.AL_TCP_CLIENTS_REF) lock (Types.HANDLE.mem.AL_TCP_CLIENTS_KY)
                    {
                        Types.HANDLE.mem.AL_TCP_CLIENTS.Add(sctl);
                        Types.HANDLE.mem.AL_TCP_CLIENTS_REF.Add(reference);
                        Types.HANDLE.mem.AL_TCP_CLIENTS_KY.Add(new string[] { private_key_path, public_key_path });
                    }
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Client " + reference + " connected to " + ip + ":" + port);
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
            string s_data = Types.HANDLE.crypto.To_String(data);

            //Removes delimiter 0x13 and executes
            NetworkEngineFunctions nef__ = new NetworkEngineFunctions();
            Enginefunctions ef__ = new Enginefunctions();
            string command = ef__.replace_fakes(nef__.replace_telnet(s_data));
            Types.HANDLE.librarian_instance.librarian.scorpioniee(command.TrimEnd(new char[] { Convert.ToChar(0x13) }));
            
            ef__ = null;
            nef__ = null;
            return;
        }
    }
}
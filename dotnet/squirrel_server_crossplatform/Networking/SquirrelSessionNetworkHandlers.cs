

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
using System.Security.Cryptography;
using System.IO;

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
            string command = Enginefunctions.replace_fakes(NetworkEngineFunctions.replace_telnet(s_data));
            Dictionary<string, string> processed = NetworkEngineFunctions.replace_api(command);

            //Get session from parsed elements
            string session = processed["session"];
            string destroyable = "\0";
            string db_page = Types.S_NULL;

            try
            {
                if (processed != null)
                {
                    //Is there a session? if not create a session dictionary to contain session variables for the user, only GET regularly needs a user variable
                    if(!MemoryCore.varCheck(session))
                    {
                        ArrayList temp = new ArrayList(){ session };
                        Types.HANDLE.librarian_instance.librarian.vardictionary(ref destroyable, ref temp);
                        temp = new ArrayList(){ session, "\'session\'", $"\'{session}\'" };
                        Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
                        temp = new ArrayList(){ session, "\'project\'", "\'" + processed["tag"] + "\'" };
                        Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
                    }

                    if (processed["type"] == NetworkEngineFunctions.api_requests["get"])
                    {//Get formattable page from XMLDB
                        ArrayList query_result = Types.HANDLE.vds.doDBSelectiveNoThread(processed["db"], null, processed["tag"], processed["subtag"], Types.HANDLE.vds.OPCODE_GET);

                        if (query_result.Count > 0)
                        {
                            db_page = (string)((ArrayList)query_result[0])[0];

                            //Get mysql dat
                            

                            //Session allows us to return the page with user loaded session data
                            if(Types.HANDLE.mem.AL_CURR_VAR_REF.Contains(session))
                                reply = NetworkEngineFunctions.build_api((string)MemoryCore.varGetCustomFormattedOnlyDictionary(ref db_page, ref session), session, false);
                            else
                                reply = NetworkEngineFunctions.build_api((string)db_page, Types.S_NULL, false);
                        }
                        else
                            reply = NetworkEngineFunctions.build_api("Query resulted in 0 elements returned", session, true);
                    }
                    else if (processed["type"] == NetworkEngineFunctions.api_requests["set"])
                    {
                        command = command.TrimEnd(new char[] { Convert.ToChar(0x13) });
                        string[] commands = command.Split(new char[] { '\n' });
                        foreach (string s_dat in commands)
                            Types.HANDLE.librarian_instance.librarian.scorpioniee(s_dat);
                        reply = NetworkEngineFunctions.build_api("Command executed", session, false);
                    }
                    else if (processed["type"] == NetworkEngineFunctions.api_requests["delete"])
                    {
                        //Delete a session on request from the HTTP server (Time based)
                    }
                }
                else
                    reply = NetworkEngineFunctions.build_api("Command error. Incorrect syntax", "", true);

                //RSA then encrypt and send as byte[]... CHANGE TO AES
                if (reply != null && Types.HANDLE.mem.GetTcpKeyPath(server_index)[0] != null)
                {
                    using (Aes myAes = Aes.Create())
                    {
                        //Must be a 16 byte key
                        byte[] key = ScorpionAES.ScorpionAESInHouse.importKey(Types.main_user_aes_path_file);
                        myAes.Key = key;
                        e.Reply(ScorpionAES.ScorpionAESInHouse.encrypt(reply, myAes.Key, myAes.IV));
                    }
                }

                //No RSA then just send as string
                if (reply != null && Types.HANDLE.mem.GetTcpKeyPath(server_index)[0] == null)
                    e.Reply(reply);
            }
            catch(Exception er){ ScorpionConsoleReadWrite.ConsoleWrite.writeError(string.Format("Fatal Error for {0}. Closing connection", end_point)); ScorpionConsoleReadWrite.ConsoleWrite.writeError(er.StackTrace); ScorpionConsoleReadWrite.ConsoleWrite.writeError(er.Message); }
            finally
            {
                e.TcpClient.Client.Disconnect(true);
            }
            return;
        }
    }
}
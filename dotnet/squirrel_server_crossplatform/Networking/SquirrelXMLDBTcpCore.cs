//If sessions are allowed. this class allows functionality not to be shared between sessions that are crucial in being seperate from the main partial Librarian class

using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using ScorpionMySql;
using SquirrelDefaultPaths;

namespace Scorpion
{
    public partial class SessionDependentNetworkHandlers
    {
        public void AddTcpServer(string reference, string ip, int port, string maria_db_connection_string)
        {
            SimpleTCP.SimpleTcpServer server = new SimpleTCP.SimpleTcpServer();
            server.AutoTrimStrings = true;
            server.ClientConnected += ServerClientConnectedEvent;
            server.ClientDisconnected += ServerClientDisconnectedEvent;

            lock (Types.HANDLE.mem.AL_TCP) lock (Types.HANDLE.mem.AL_TCP_REF) lock(Types.HANDLE.mem.AL_TCP_CONNECTION_STRING)
                {
                    Types.HANDLE.mem.AL_TCP.Add(server);
                    Types.HANDLE.mem.AL_TCP_REF.Add(reference);
                    Types.HANDLE.mem.AL_TCP_CONNECTION_STRING.Add(maria_db_connection_string);

                    server.DataReceived += ServerDataReceivedEvent;
                    IPAddress ipa = IPAddress.Parse(ip == Types.S_NULL ? "127.0.0.1" : ip);
                    server.Start(ipa, port);
                }
            return;
        }

        /*TCP server : Events*/
        void ServerClientConnectedEvent(object sender, TcpClient e)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("TCP >> Client " + (IPEndPoint)e.Client.RemoteEndPoint + " connected");
            return;
        }

        void ServerClientDisconnectedEvent(object sender, TcpClient e)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("TCP >> Client " + (IPEndPoint)e.Client.RemoteEndPoint + " disconnected");
            return;
        }

        public void ServerDataReceivedEvent(object sender, SimpleTCP.Message e)
        {
            Thread ths = new Thread(new ParameterizedThreadStart(XMLDBProcessTcpRequest));
            ths.IsBackground = false;
            ths.Priority = 0;
            ths.Start(new object[2]{ sender, e });
        }
    }

    public partial class SessionDependentNetworkHandlers
    {
        private void XMLDBProcessTcpRequest(object param_thread_object)
        {
            //Add RSA support
            //Removes delimiter 0x13 and executes
            /*
            {&scorpion}{&type}type{&/type}{&database}db{&/database}{&tag}tag{&/tag}{&subtag}subtag{&/subtag}{&session}session{&/session}{&/scorpion}
            tag:project_folder*/

            //Get server related objects
            XMLDBGetClientEndPointData(param_thread_object, out SimpleTCP.Message message, out int server_index, out IPEndPoint end_point);

            //Inbound and outbound data
            string reply = null;
            string s_data = string.Empty;

            //Mariadb
            string maria_db_connection_string = (string)Types.HANDLE.mem.AL_TCP_CONNECTION_STRING[server_index];
            using (Aes myAes = Aes.Create())
            {
                //Must be a 16 byte key
                byte[] key = ScorpionAES.ScorpionAESInHouse.importKey(SquirrelPaths.main_user_aes_path_file);
                try
                {
                    myAes.Key = key;
                    s_data = ScorpionAES.ScorpionAESInHouse.decrypt(message.Data, myAes.Key, myAes.IV);
                }
                catch { Console.WriteLine("Unable to decrypt for: {0}", message); }
            }

            //Replace TELNET and API elements
            string command = Enginefunctions.replace_fakes(ScorpionNetworkDriver.NetworkEngineFunctions.replaceTelnet(s_data));
            Dictionary<string, string> processed = ScorpionNetworkDriver.NetworkEngineFunctions.replaceApi(command);

            //Get the requests session
            string session = processed["session"];
            bool includedata = Convert.ToBoolean(processed["includedata"]);
            string XMLDB_result = Types.S_NULL;

            try
            {
                    if (processed != null)
                    {
                        //Is there a session? if not create a session dictionary to contain session variables for the user, only GET regularly needs a user variable.
                        if(!MemoryCore.varCheck(session))
                            sessionMemory(session, processed["tag"], "");

                        //FUTURE!: Check if the session belongs to said project
                        //string passable = String.Empty;
                        //ArrayList to_get_dict = new ArrayList(){ session, "*project", "*true" };
                        //if(processed["tag"] == Types.HANDLE.librarian_instance.librarian.vardictionaryget())

                        //Process a GET scorpion request
                        if (processed["type"] == ScorpionNetworkDriver.NetworkEngineFunctions.api_requests["get"])
                            reply = XMLDBProcessTcpGetRequest(ref session, ref processed, ref maria_db_connection_string, ref includedata);
                        else if (processed["type"] == ScorpionNetworkDriver.NetworkEngineFunctions.api_requests["set"])
                            reply = XMLDBProcessTcpSetRequest(ref session, ref processed, ref maria_db_connection_string);
                        else if (processed["type"] == ScorpionNetworkDriver.NetworkEngineFunctions.api_requests["login"])
                            reply = XMLDBProcessTcpLoginRequest(ref session, ref processed, ref maria_db_connection_string);
                        else if (processed["type"] == ScorpionNetworkDriver.NetworkEngineFunctions.api_requests["delete"])
                        {
                            //Delete a session on request from the HTTP server (Time based!)
                        }
                    }
                    else
                        reply = ScorpionNetworkDriver.NetworkEngineFunctions.buildApiResponse("Command error. Incorrect syntax", "", true);

                //RSA then encrypt and send as byte[]... CHANGE TO AES
                if (reply != null)
                {
                    using (Aes myAes = Aes.Create())
                    {
                        //Must be a 16 byte key
                        byte[] key = ScorpionAES.ScorpionAESInHouse.importKey(Types.main_user_aes_path_file);
                        myAes.Key = key;
                        message.Reply(ScorpionAES.ScorpionAESInHouse.encrypt(reply, myAes.Key, myAes.IV));
                    }
                }
                else
                    message.Reply(new byte[] { 0x00 });
            }
            catch(Exception er){ ScorpionConsoleReadWrite.ConsoleWrite.writeError(string.Format("Fatal Error for {0}. Closing connection", end_point)); ScorpionConsoleReadWrite.ConsoleWrite.writeError(er.StackTrace); ScorpionConsoleReadWrite.ConsoleWrite.writeError(er.Message); }
            finally
            {
                message.TcpClient.Client.Disconnect(true);
            }
            return;
        }

        private string XMLDBProcessTcpGetRequest(ref string session, ref Dictionary<string, string> processed, ref string maria_db_connection_string, ref bool includedata)
        {
            string reply = Types.S_NULL;
            string db_page = Types.S_NULL;
            
            //Query XMLDB for Static content
            Scorpion_MDB.ScorpionMicroDB.XMLDBResult query_result = Types.HANDLE.vds.doDBSelectiveNoThread(processed["db"], null, processed["tag"], processed["subtag"], Types.HANDLE.vds.OPCODE_GET);

            if(query_result.Length() == 0)
            {
                //If XMLDB returns no results read from file directly
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("No XMLDB result found. Accessing content file instead if available..");

                string path = string.Format("{0}/{1}/src/{2}", SquirrelDefaultPaths.SquirrelPaths.main_user_projects_path, processed["tag"], processed["subtag"]);
                
                //If no resort to finding the file on the filesytem
                if(File.Exists(path))
                    db_page = File.ReadAllText(path);
            }
            else
            {
                //XMLDB get result
                db_page = query_result.getFirstAsString();
            }

            if (db_page != Types.S_NULL)
            {
                //Get mysql data for specified page if data has been requested embedded in the page
                if(includedata)
                {
                    using(ScorpionSql sql = new ScorpionSql())
                    {
                        ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Getting MariaDB data..");
                        sql.scfmtSqlGet(maria_db_connection_string, processed["tag"], processed["subtag"], "", "", session, out Dictionary<string, string> mysql_result);              
                        MemoryCore.var_manipulate(session, mysql_result, false, true, MemoryCore.OPCODE_MERGE);
                        ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Retrieved MariaDB data");
                    }
                }

                //Session allows us to return the page with user loaded session data
                if(Types.HANDLE.mem.AL_CURR_VAR_REF.Contains(session))
                    reply = ScorpionNetworkDriver.NetworkEngineFunctions.buildApiResponse((string)MemoryCore.varGetCustomFormattedOnlyDictionary(ref db_page, ref session), session, false);
                else
                    reply = ScorpionNetworkDriver.NetworkEngineFunctions.buildApiResponse((string)db_page, Types.S_NULL, false);
            }
            else
                reply = ScorpionNetworkDriver.NetworkEngineFunctions.buildApiResponse("Internal retrieval error: 500", session, true);

                //Clear out all MariaDB memory used to populate the page and set to defaults.
                if(MemoryCore.varCheck(session))
                    sessionMemory(session, processed["tag"], "");
            return reply;
        }

        private string XMLDBProcessTcpSetRequest(ref string session, ref Dictionary<string, string> processed, ref string maria_db_connection_string)
        {
            //Set recieved data to mysql
            using(ScorpionSql sql = new ScorpionSql())
            {
                sql.scfmtSqlSet(maria_db_connection_string, processed["db"], processed["tag"], processed["subtag"], processed["data"], session);
            }
            return ScorpionNetworkDriver.NetworkEngineFunctions.types["none"];
        }

        private string XMLDBProcessTcpLoginRequest(ref string session, ref Dictionary<string, string> processed, ref string maria_db_connection_string)
        {
            string uname = processed["user"];
            string password = processed["password"];
            Dictionary<string, string> result = new Dictionary<string, string>();

            //Check if exists in Mysql
            using(ScorpionSql sql = new ScorpionSql())
            {
                sql.scfmtSqlGet(maria_db_connection_string, "users", processed["tag"], uname, Types.S_NULL, Types.S_NULL, out result);
            }

            return "";
        }

        private void XMLDBGetClientEndPointData(object tcp_client_objects, out SimpleTCP.Message message, out int server_index, out IPEndPoint end_point)
        {
            object sender = ((object[])tcp_client_objects)[0];
            message = (SimpleTCP.Message)((object[])tcp_client_objects)[1];
            server_index = Types.HANDLE.mem.AL_TCP.IndexOf(sender);
            end_point = (IPEndPoint)message.TcpClient.Client.RemoteEndPoint;
        }

        private void sessionMemory(string session, string project, string user)
        {
            string destroyable = "\0";
            ArrayList temp = new ArrayList(){ session, false };
            Types.HANDLE.librarian_instance.librarian.vardictionary(ref destroyable, ref temp);
            temp = new ArrayList(){ session, "\'session\'", $"\'{session}\'" };
            Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
            temp = new ArrayList(){ session, "\'project\'", $"\'{project}\'" };
            Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
            temp = new ArrayList(){ session, "\'user\'", $"\'{user}\'" };
            Types.HANDLE.librarian_instance.librarian.vardictionaryappend(ref destroyable, ref temp);
            return;
        }
    }
}
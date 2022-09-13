using System.Text;
using System.Collections;
using System.Net;
using System.ComponentModel;
using ScorpionConsoleReadWrite;

namespace Scorpion
{
    public sealed partial class Librarian
    {
        public string getownip(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable<<::
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            // Get the IP
            string IP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);

            return var_create_return(ref IP, true);
        }

        public void xmldbtcpserverstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *port, *[rsaprivatekeyfilepath||*null], *[rsapublickeyfilepath||*null], *api
            //No rsa then pass *false for rsa parameters
            string name = (string)MemoryCore.varGet(objects[0]);
            string ip = (string)MemoryCore.varGet(objects[1]);
            int port = Convert.ToInt32(MemoryCore.varGet(objects[2]));
            string RSA_private_path = (string)MemoryCore.varGet(objects[3]);
            string RSA_public_path = (string)MemoryCore.varGet(objects[4]);
            string maria_db_connection_string = (string)MemoryCore.varGet(objects[5]);

            //Check if the AES encryption key exists
            if(!File.Exists(Types.main_user_aes_path_file))
            {
                ConsoleWrite.writeError("No AES key found at: ", Types.main_user_aes_path_file, ". Use the command 'generateaeskey' to generate one");
                return;
            }

            //Check if the RSA encryption keys exist
            if(!File.Exists(RSA_private_path) || !File.Exists(RSA_public_path))
            {
                ConsoleWrite.writeError("The provided RSA public key: ", RSA_public_path, ", or private key: ", RSA_private_path, " could not be found");
                return;
            }

            Types.HANDLE.sdh.AddTcpServer(name, ip, port, RSA_private_path == Types.S_NULL ? null : RSA_private_path, RSA_public_path == Types.S_NULL ? null : RSA_public_path, maria_db_connection_string);
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("TCP server started, Please remember to configure your firewall appropriately");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref name);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void xmldbtcpserverstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            lock (Types.HANDLE.mem.AL_TCP) lock (Types.HANDLE.mem.AL_TCP_REF)
                {
                    int index = Types.HANDLE.mem.AL_TCP_REF.IndexOf(MemoryCore.varGet(objects[0]));
                    ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[index]).Stop();
                    ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[index]).DataReceived -= Types.HANDLE.sdh.Sctl_DataReceived;
                    Types.HANDLE.mem.AL_TCP_REF.RemoveAt(index);
                    Types.HANDLE.mem.RemoveTcpPath(ref index);
                    Types.HANDLE.mem.AL_TCP.RemoveAt(index);
                    ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("TCP server stopped");
                }
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void listservers(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::
            int n = 0;
            foreach (string server in Types.HANDLE.mem.AL_TCP_REF)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(server + ":\n");
                foreach (IPAddress ip in ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[n]).GetListeningIPs())
                    ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(ip.ToString());
                n++;
            }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void ls(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            listservers(ref Scorp_Line_Exec, ref objects);
            return;
        }
    }

    public sealed partial class Librarian
    {
        internal async Task DownloadFile(string url, string local_path)
        {
            using(HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(@url));
                using (var fs = File.Create(local_path))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }
            return;
        }

        internal static void DownloadFileDone(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("FAILED");
            if (e.Error != null)
                ScorpionConsoleReadWrite.ConsoleWrite.writeError(e.Error.ToString());
            return;
        }
    }
}

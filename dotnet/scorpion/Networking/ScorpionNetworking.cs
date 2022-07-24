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
using System.ComponentModel;

namespace Scorpion
{
    public sealed partial class Librarian
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
        public void serverstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *port, *[rsaprivatekeyfilepath||*null], *[rsapublickeyfilepath||*null], *api
            //No rsa then pass *false for rsa parameters
            string name = (string)var_get(objects[0]);
            string ip = (string)var_get(objects[1]);
            int port = Convert.ToInt32(var_get(objects[2]));
            string RSA_private_path = (string)var_get(objects[3]);
            string RSA_public_path = (string)var_get(objects[4]);

            Types.HANDLE.sdh.AddTcpServer(name, ip, port, RSA_private_path == Types.S_NULL ? null : RSA_private_path, RSA_public_path == Types.S_NULL ? null : RSA_public_path);
            write_to_console("TCP server started, Please remember to configure your firewall appropriately");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref name);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void serverstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            lock (Types.HANDLE.mem.AL_TCP) lock (Types.HANDLE.mem.AL_TCP_REF)
                {
                    int index = Types.HANDLE.mem.AL_TCP_REF.IndexOf(var_get(objects[0]));
                    ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[index]).Stop();
                    ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[index]).DataReceived -= Types.HANDLE.sdh.Sctl_DataReceived;
                    Types.HANDLE.mem.AL_TCP_REF.RemoveAt(index);
                    Types.HANDLE.mem.RemoveTcpPath(ref index);
                    Types.HANDLE.mem.AL_TCP.RemoveAt(index);
                    write_to_console("TCP server stopped");
                }
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
                int server_index = Types.HANDLE.mem.AL_TCP_REF.IndexOf(var_get(objects[0]));
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[server_index]).StringEncoder = Encoding.UTF8;
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[server_index]).Delimiter = 0x13;

                //If there are no RSA keys, skip encryption
                if (Types.HANDLE.mem.GetTcpKeyPath(server_index)[1] != null)
                    data = Scorpion_RSA.Scorpion_RSA.encrypt_data((string)var_get(objects[1]), Types.HANDLE.mem.GetTcpKeyPath(server_index)[1]);
                else
                    data = Types.HANDLE.crypto.To_Byte((string)var_get(objects[1]));

                //Broadcast the data
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[server_index]).Broadcast(data);
                ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Data sent");
            }
            catch (Exception e) { ScorpionConsoleReadWrite.ConsoleWrite.writeError(e.Message); }
            //var_dispose_internal(ref data);
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

        public void tcpclientstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //A function that allows you to start an RSA encryption supported TCP client or one with no encryption
            //::*name, *ip, *port, [*privatekey||BOOLEAN *false||*null], [*publicrsakey||BOOLEAN *false||*null]
            if((string)var_get(objects[3]) != Types.S_No && (string)var_get(objects[3]) != Types.S_NULL && (string)var_get(objects[4]) != Types.S_No && (string)var_get(objects[4]) != Types.S_NULL)
                Types.HANDLE.sdh.AddTcpClient((string)var_get(objects[0]), (string)var_get(objects[1]), Convert.ToInt32(var_get(objects[2])), (string)var_get(objects[3]), (string)var_get(objects[4]));
            else
                Types.HANDLE.sdh.AddTcpClient((string)var_get(objects[0]), (string)var_get(objects[1]), Convert.ToInt32(var_get(objects[2])), null, null);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void tcpclientsend(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *data
            byte[] data = null;
            try
            {
                int client_ndx = Types.HANDLE.sdh.GetIndexTcpClient((string)var_get(objects[0]));
                SimpleTCP.SimpleTcpClient tcl = Types.HANDLE.sdh.GetClient(client_ndx);
                tcl.StringEncoder = Encoding.UTF8;
                tcl.Delimiter = 0x13;

                //If there are no RSA keys, skip encryption
                if (Types.HANDLE.sdh.GetClientKeyPaths(client_ndx)[1] != null)
                    data = Scorpion_RSA.Scorpion_RSA.encrypt_data((string)var_get(objects[1]), Types.HANDLE.sdh.GetClientKeyPaths(client_ndx)[1]);
                else
                    data = Types.HANDLE.crypto.To_Byte((string)var_get(objects[1]));

                tcl.Write(data);
                ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Data sent");
                //Types.HANDLE.sdh.remove_tcpclient(client_ndx);
            }
            catch (Exception e) { ScorpionConsoleReadWrite.ConsoleWrite.writeError(e.Message); };

            var_dispose_internal(ref data);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void tcpclientstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            Types.HANDLE.sdh.RemoveTcpClient(Types.HANDLE.sdh.GetIndexTcpClient((string)var_get(objects[0])));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
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

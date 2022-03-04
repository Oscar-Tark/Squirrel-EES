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

            Do_on.sdh.add_tcpserver(name, ip, port, RSA_private_path == Do_on.types.S_NULL ? null : RSA_private_path, RSA_public_path == Do_on.types.S_NULL ? null : RSA_public_path);
            write_to_console("TCP server started, Please remember to configure your firewall appropriately");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref name);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void serverstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            lock (Do_on.mem.AL_TCP) lock (Do_on.mem.AL_TCP_REF)
                {
                    int index = Do_on.mem.AL_TCP_REF.IndexOf(var_get(objects[0]));
                    ((SimpleTCP.SimpleTcpServer)Do_on.mem.AL_TCP[index]).Stop();
                    ((SimpleTCP.SimpleTcpServer)Do_on.mem.AL_TCP[index]).DataReceived -= Do_on.sdh.Sctl_DataReceived;
                    Do_on.mem.AL_TCP_REF.RemoveAt(index);
                    Do_on.mem.remove_tcp_key_path(ref index);
                    Do_on.mem.AL_TCP.RemoveAt(index);
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
                int server_index = Do_on.mem.AL_TCP_REF.IndexOf(var_get(objects[0]));
                ((SimpleTCP.SimpleTcpServer)Do_on.mem.AL_TCP[server_index]).StringEncoder = Encoding.UTF8;
                ((SimpleTCP.SimpleTcpServer)Do_on.mem.AL_TCP[server_index]).Delimiter = 0x13;

                //If there are no RSA keys, skip encryption
                if (Do_on.mem.get_tcp_key_paths(server_index)[1] != null)
                    data = Scorpion_RSA.Scorpion_RSA.encrypt_data((string)var_get(objects[1]), Do_on.mem.get_tcp_key_paths(server_index)[1]);
                else
                    data = Do_on.crypto.To_Byte((string)var_get(objects[1]));

                //Broadcast the data
                ((SimpleTCP.SimpleTcpServer)Do_on.mem.AL_TCP[server_index]).Broadcast(data);
                Do_on.write_success("Data sent");
            }
            catch (Exception e) { Do_on.write_error(e.Message); }
            //var_dispose_internal(ref data);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void listservers(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::
            int n = 0;
            foreach (string server in Do_on.mem.AL_TCP_REF)
            {
                Do_on.write_to_cui(server + ":\n");
                foreach (IPAddress ip in ((SimpleTCP.SimpleTcpServer)Do_on.mem.AL_TCP[n]).GetListeningIPs())
                    Do_on.write_to_cui(ip.ToString());
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
            if((string)var_get(objects[3]) != Do_on.types.S_No && (string)var_get(objects[3]) != Do_on.types.S_NULL && (string)var_get(objects[4]) != Do_on.types.S_No && (string)var_get(objects[4]) != Do_on.types.S_NULL)
                Do_on.sdh.add_tcpclient((string)var_get(objects[0]), (string)var_get(objects[1]), Convert.ToInt32(var_get(objects[2])), (string)var_get(objects[3]), (string)var_get(objects[4]));
            else
                Do_on.sdh.add_tcpclient((string)var_get(objects[0]), (string)var_get(objects[1]), Convert.ToInt32(var_get(objects[2])), null, null);
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
                int client_ndx = Do_on.sdh.get_index_tcpclient((string)var_get(objects[0]));
                SimpleTCP.SimpleTcpClient tcl = Do_on.sdh.get_tcpclient(client_ndx);
                tcl.StringEncoder = Encoding.UTF8;
                tcl.Delimiter = 0x13;

                //If there are no RSA keys, skip encryption
                if (Do_on.sdh.get_tcpclient_key_paths(client_ndx)[1] != null)
                {
                    data = Scorpion_RSA.Scorpion_RSA.encrypt_data((string)var_get(objects[1]), Do_on.sdh.get_tcpclient_key_paths(client_ndx)[1]);
                }
                else
                {
                    data = Do_on.crypto.To_Byte((string)var_get(objects[1]));
                }

                tcl.Write(data);
                Do_on.write_success("Data sent");
                //Do_on.sdh.remove_tcpclient(client_ndx);
            }
            catch (Exception e) { Do_on.write_error(e.Message); };

            var_dispose_internal(ref data);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void tcpclientstop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            Do_on.sdh.remove_tcpclient(Do_on.sdh.get_index_tcpclient((string)var_get(objects[0])));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}

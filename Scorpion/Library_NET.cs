/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2016>  <Oscar Arjun Singh Tark>

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

using System.Collections;
using System.Net;
using System;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace Scorpion
{
    partial class Librarian
    {
        public void createserver(string Scorp_line_Exec, ArrayList objects)
        {
            /*
             * 
             * Packets must be sent in the following format:
             * |UNAME|PWD|DATA|
             * 
             */
            try
            {
                IPHostEntry ipe = Dns.GetHostEntry((string)var_get((string)objects[0]));
                IPEndPoint ipep = new IPEndPoint(ipe.AddressList[0], Convert.ToInt16(var_get((string)objects[1])));


                Socket sc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sc.Bind(ipep);
                sc.Listen(500);

                //Accept client connection
                byte[] by_recv = new byte[512];
                int bytes_rec = sc.BeginReceive(by_recv, SocketFlags.None, )

                if (bytes_rec > 512){
                    Do_on.write_to_cui("Recieved length larger than 512 bytes @ " + ipe.AddressList[0].ToString());
                    return;
                    }

                Do_on.write_to_cui(bytes_rec.ToString());
                sc.Close();
            }
            catch(Exception erty) { Do_on.write_to_cui(erty.Message); }
        }

        public

        public void send()
        {

        }

        public void recieve()
        {

        }

        public void disconnect()
        {
            //NAME
        }

        private void set_server()
        {

        }
    }
}
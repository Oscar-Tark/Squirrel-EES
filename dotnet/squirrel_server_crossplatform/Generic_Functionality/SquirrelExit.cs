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
using System.Collections;
using System.Collections.Generic;

namespace Scorpion
{
    partial class Librarian
    {
        public void exit(ref string Scorp_Line_Exec, ref ArrayList Objects)
        {
            instancecleanup();
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Exiting..");
            Environment.Exit(0);
            return;
        }

        public void instancecleanup()
        {
            //Clean memory up
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Clearing GLOBAL memory..");

            Types.HANDLE.mem.AL_CURR_VAR.Clear();
            Types.HANDLE.mem.AL_CURR_VAR_NACESSED.Clear();
            Types.HANDLE.mem.AL_CURR_VAR_REF.Clear();
            Types.HANDLE.mem.AL_CURR_VAR_TAG.Clear();
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Cleared GLOBAL memory");

            //Delete all TCP servers
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput($"Clearing {Types.HANDLE.mem.AL_TCP.Count} TCP servers..");
            for(int i = Types.HANDLE.mem.AL_TCP.Count-1; i >= 0; i--)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeWarning($"Closing tcp server {Types.HANDLE.mem.AL_TCP_REF[i]}");
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[i]).Stop();
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[i]).DataReceived -= Types.HANDLE.sdh.Sctl_DataReceived;
                ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess($"Closed tcp server {Types.HANDLE.mem.AL_TCP_REF[i]}");
            }
            Types.HANDLE.mem.AL_TCP.Clear();
            Types.HANDLE.mem.AL_TCP_KY.Clear();
            Types.HANDLE.mem.AL_TCP_REF.Clear();
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Cleared TCP servers");

            //Clean TCP clients
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput($"Clearing {Types.HANDLE.mem.AL_TCP_CLIENTS.Count} TCP clients..");
            for(int i = Types.HANDLE.mem.AL_TCP_CLIENTS.Count-1; i >= 0; i--)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeWarning($"Closing tcp client connection {Types.HANDLE.mem.AL_TCP_CLIENTS[i]}");
                Types.HANDLE.sdh.RemoveTcpClient(i);
                ScorpionConsoleReadWrite.ConsoleWrite.writeWarning($"Closed tcp client connection {Types.HANDLE.mem.AL_TCP_CLIENTS[i]}");
            }
            Types.HANDLE.mem.AL_TCP_CLIENTS.Clear();
            Types.HANDLE.mem.AL_TCP_CLIENTS_KY.Clear();
            Types.HANDLE.mem.AL_TCP_CLIENTS_REF.Clear();
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Cleared TCP clients");
            return;
        }
    }
}
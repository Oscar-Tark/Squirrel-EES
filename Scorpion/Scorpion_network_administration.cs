/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark> <Benjamin Jack Johnson>

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class Scorpion_Interprocess_Network_Admin
    {
        //localserver
        private PipeServer.Server pipeServer; Librarian lb_;
        public Scorpion_Interprocess_Network_Admin start_pipe_server(string name, Librarian lb)
        {
            lb_ = lb;
            this.pipeServer = new PipeServer.Server();
            this.pipeServer.MessageReceived += new PipeServer.Server.MessageReceivedHandler(pipeServer_MessageReceived);

            //start the pipe server if it's not already running
            if (!this.pipeServer.Running)
            {
                this.pipeServer.PipeName = "\\\\.\\pipe\\OneP";// + name;//Scorpion";
                this.pipeServer.Start();
            }

            return this;
        }

        public void stop_pipe_server()
        {
            pipeServer = null;
            return;
        }

        void pipeServer_MessageReceived(PipeServer.Server.Client client, byte[] message)
        {
            //AL[String],[Data]

            //readr.access_library(message);
            /*this.Invoke(new PipeServer.Server.MessageReceivedHandler(DisplayMessageReceived),
                   new object[] { client, message });*/
            MessageBox.Show("fribit");
            lb_.work_(((ArrayList)lb_.ByteArrayToObject(message))[0]);
            return;
        }

        /*void DisplayMessageReceived(PipeServer.Server.Client client, string message)
        {
        }*/

        public void broadcast_local(object message)
        {
            //AL[String],[Data]
            try
            {
                this.pipeServer.SendMessage(lb_.ObjectToByteArray(message));
            }
            catch { MessageBox.Show("Local Application Communication has Failed. Contact the Scorpion team for help", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return;
        }
        //-->end
    }
}
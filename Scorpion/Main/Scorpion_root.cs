/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014-2016>  <Oscar Arjun Singh Tark>

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
using System.Threading;

namespace Scorpion
{
    //CHANGE CLASS NAME
    public partial class Form1
    {
        public Form1()
        {
            start_classes();
            Console.WriteLine("Welcome to Scorpion V1.0b\n\n{0}", "Licensed Under the GNU GPL Version 3\n< Scorpion IEE Copyright(C) 2020+ Oscar Arjun Singh Tark >\n\nThis program is free software: you can redistribute it and / or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or(at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see < http://www.gnu.org/licenses/>.\n\n");
            string command = null;
            int ky = 0x00;

            while ((ky = Console.Read()) != 0x1b)
            {
                switch(ky)
                {
                    case 0x0a:
                        commands_point = 0;
                        readr.access_library(command);
                        command = null;
                        break;
                    case 0x09:
                        Console.Write("Historical Command: {0:G}\n\tPress Enter to use", commands[commands_point]);
                        command = commands[commands_point];
                        break;
                    case 0x00:
                        if(command.Length >= 1)
                            command = command.Remove(command.Length - 1);
                        break;
                    default:
                        command += Convert.ToChar(ky);
                        break;
                }
                th_clean_strt();
            }
        }

        public void Application_ApplicationExit(object sender, EventArgs e)
        {
            //DUMP
            types.unload_system_vars();
            sender = null;
            e = null;
            return;
        }

        private Thread th_clean;
        private void th_clean_strt()
        {
            th_clean = new Thread(new ThreadStart(clean));
            th_clean.IsBackground = true;
            th_clean.Start();
            return;
        }

        private void clean()
        {
            //Implement in a better way
            //General Memory
            AL_CURR_VAR.TrimToSize();
            AL_CURR_VAR_REF.TrimToSize();
            AL_CURR_VAR_TAG.TrimToSize();
            AL_CURR_VAR_EVT.TrimToSize();

            AL_AMCS.TrimToSize();
            AL_AMCS_REF.TrimToSize();

            GC.Collect();
            return;
        }

        //ACCESSORS
        public void Access_Work(string Line)
        {
            readr.access_library(Line);
            return;
        }

        public void write_to_cui(string Text)
        {
            Console.WriteLine(Text + "\n");
            Text = null;

            return;
        }
    }
}

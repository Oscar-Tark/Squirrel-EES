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

using System;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Security;
using ScorpionConsoleReadWrite;

namespace Scorpion
{
    //CHANGE CLASS NAME
    public class Scorp
    {
        public int instance;
        public void Init()
        {
            vds = new Scorpion_MDB.ScorpionMicroDB();
            crypto = new Crypto.Cryptographer();
            mmsec = new Memory_Security.Secure_Memory(this);
            san = new Memory_Security.Sanitizer(this);
            tms = new Timer_(this);
            readr = new reader(this);
            wkp = new Workspaces.Workspaces(this);
            mem = new Memory();
            types = new Types(this);
            sdh = new SessionDependentNetworkHandlers(this);
            sclog = new Scorpion_LOG.Scorpion_LOG(types.main_user_path);
            return;
        }

        public reader readr;
        public Scorpion_MDB.ScorpionMicroDB vds;
        public Crypto.Cryptographer crypto;
        public Memory_Security.Secure_Memory mmsec;
        public Memory_Security.Sanitizer san;
        public Timer_ tms;
        public Workspaces.Workspaces wkp;
        public Memory mem;
        public Types types;
        public SessionDependentNetworkHandlers sdh;
        public Scorpion_LOG.Scorpion_LOG sclog;

        private bool check_directory()
        {
            //Check if the main Scorpion directory exists if not create it
            if (!Directory.Exists(types.main_user_path))
                Directory.CreateDirectory(types.main_user_path);

            //Check if the Directory was created if not return false, if yes return true
            if (!Directory.Exists(types.main_user_path))
                return false;
            return true;
        }

        public Scorp(int instance_descriptor, double version)
        {
            //Assign the session instance int:identifier for this instance
            instance = instance_descriptor;
            
            //Initialization functions
            Init();

            //Check that the main data directory exists
            if (!check_directory())
            {
                Console.WriteLine("Could not create the main user folder for Scorpion at {0}", types.main_user_path);
                return;
            }

            SecureString ss_passcode = new SecureString();
            string uname = null;
            byte[] pin = new byte[4];
            int tries = 1;
            const int max_tries = 2;
            types.LoadSystemVars();
            Scorpion_Authenticator.Authenticator auth = new Scorpion_Authenticator.Authenticator();

            //Read passcode and pin
            while (true)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeSpecial($"\nScorpion Enterprise Server V{version} - Login - [0*0]__/ 'Robo Scorpio says: Hi! Who are you?'\n-----------------------------------------------------------------------\n");
                ConsoleWrite.writeOutput("Please enter your user login credentials [Try " + tries + "/" + max_tries + "]");
                ConsoleWrite.writeOutput("Username >> ");
                uname = Console.ReadLine();
                ConsoleWrite.writeOutput("Password >> ");
                ss_passcode = auth.read_password();

                if (auth.authenticate(ref uname, ss_passcode))
                {
                    mmsec.set_uname(ref uname);
                    mmsec.set_pass(ss_passcode);
                    break;
                }
                else
                {
                    ScorpionConsoleReadWrite.ConsoleWrite.writeError("Wrong username password combination");
                    sclog.log("Login attempt failed as " + uname);
                    if (max_tries == tries)
                    {
                        auth.create_user(readr.lib_SCR.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance));
                        tries = 0;
                    }
                }
                tries++;
            }

            ConsoleWrite.writeSpecial(string.Format("\nWelcome {1} to Scorpion Enterprise Server V1.0b\n\n{0}", "Licensed Under the GNU GPL Version 3\n< Scorpion IEE Copyright(C) 2020+ Oscar Arjun Singh Tark >\n\nThis program is free software: you can redistribute it and / or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or(at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see < http://www.gnu.org/licenses/>.\n", uname));
            ConsoleWrite.writeSpecial(string.Format("-------------------------\nCPU logical Cores: {0}\nMachine name: {1}\nOperating system: {4}\n64bit OS: {2}\n64bit process: {3}\nProcess ID: {5}\nInstance: {6}\nUsername: {7}\n-------------------------\n", Environment.ProcessorCount, Environment.MachineName, Environment.Is64BitProcess, Environment.Is64BitOperatingSystem, Environment.OSVersion, Environment.CurrentManagedThreadId, instance, mmsec.get_uname()));
        }

        private Thread th_clean;
        public void th_clean_strt()
        {
            th_clean = new Thread(new ThreadStart(clean));
            th_clean.IsBackground = true;
            th_clean.Start();
            return;
        }

        private static void clean()
        {
            //Invoke GC manually although not loved, with the number of stack frames and ref's used i'd like to keep things GC'd
            GC.Collect();
            return;
        }

        //ACCESSORS
        public void Access_Work(string Line)
        {
            readr.access_library(Line);
            return;
        }
    }
}

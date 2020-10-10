/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
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
using System.Threading;

namespace Scorpion
{
    //CHANGE CLASS NAME
    public partial class Form1
    {
        public Form1()
        {
            start_classes();
            string passcode = null;
            string uname = null;
            ConsoleKeyInfo key_rd;
            byte[] pin = new byte[4];
            int pin_up = 0;
            int tries = 1;
            const int max_tries = 2;

            //Read passcode and pin
            while (true)
            {
                write_special("\nScorpion V1.0b - Login - [0_0]__/ 'Robo scorpio says: Hi! Who are you?'\n-----------------------------------------------------------------------\n");
                write_to_cui("Please enter your username [Try " + tries + "/" + max_tries + "]");
                Console.Write("Username >> ");
                uname = Console.ReadLine();

                write_to_cui("Scorpion Passcode: ");
                Console.Write("Passcode >> ");
                passcode = Console.ReadLine();

                if (pin_up != 4)
                {
                    write_to_cui("\n4 character pin [Only numbers] (NOTE: This pin is saved only for this session. Saving encrypted variables to files may render them not recoverable): ");
                    Console.Write("Pin >> ");
                    while (pin_up <= 3)
                    {
                        key_rd = Console.ReadKey();
                        if (char.IsDigit(key_rd.KeyChar))
                        {
                            pin[pin_up] = (byte)key_rd.KeyChar;
                            pin_up++;
                        }
                        else
                            Console.WriteLine("\nEntered key '" + key_rd.KeyChar + "' is not a number and will be ignored");
                    }
                }

                Scorpion_Authenticator.Authenticator auth = new Scorpion_Authenticator.Authenticator();
                if (auth.authenticate(ref uname, ref passcode))
                {
                    mmsec.set_uname(ref uname);
                    mmsec.set_pass(ref passcode, ref pin);
                    break;
                }
                else
                {
                    write_error("Wrong username password combination");
                    if (max_tries == tries)
                    {
                        auth.create_user();
                        tries = 0;
                    }
                }

                tries++;
            }

            Console.WriteLine("\nWelcome {1} to Scorpion V1.0b\n\n{0}", "Licensed Under the GNU GPL Version 3\n< Scorpion IEE Copyright(C) 2020+ Oscar Arjun Singh Tark >\n\nThis program is free software: you can redistribute it and / or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or(at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see < http://www.gnu.org/licenses/>.\n", uname);
            Console.WriteLine("-------------------------\nCores {0}\nMachine name: {1}\nOperating system: {4}\n64bit OS: {2}\n64bit process: {3}\nProcess ID: {5}\n-------------------------\n", Environment.ProcessorCount, Environment.MachineName, Environment.Is64BitProcess, Environment.Is64BitOperatingSystem, Environment.OSVersion, Environment.CurrentManagedThreadId);
            while (true)
            {
                readr.access_library(Console.ReadLine());
                th_clean_strt();
            }
        }

        public void Application_ApplicationExit(object sender, EventArgs e)
        {
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

        //WRITE STRING
        public void write_to_cui(object To_out)
        {
            change_console_color(ConsoleColor.Yellow);
            if (To_out.GetType() == Type.GetType("byte[]"))
                foreach (byte b_ in (byte[])To_out)
                    Console.WriteLine("{0:X}", b_);
            else
                Console.WriteLine(To_out + "\n");
            
            change_console_color(ConsoleColor.White);
            To_out = null;
            return;
        }

        public void write_error(object To_out)
        {
            change_console_color(ConsoleColor.Red);
            Console.WriteLine("[ERROR]: {0}", To_out);
            change_console_color(ConsoleColor.White);
            return;
        }

        public void write_special(object To_out)
        {
            change_console_color(ConsoleColor.Blue);
            Console.WriteLine("{0}", To_out);
            change_console_color(ConsoleColor.White);
            return;
        }

        public void write_success(object To_out)
        {
            change_console_color(ConsoleColor.Green);
            Console.WriteLine("{0}", To_out);
            change_console_color(ConsoleColor.White);
            return;
        }

        public void write_debug(object To_out)
        {
            if (To_out.GetType() == Type.GetType("byte[]"))
                foreach (byte b_ in (byte[])To_out)
                    Console.WriteLine("{0:X}", b_);
            else
                Console.WriteLine("[DEBUG]:\n" + To_out + "\n");
            To_out = null;
            return;
        }

        public void change_console_color(ConsoleColor cc)
        {
            Console.ForegroundColor = cc;
            return;
        }
    }
}

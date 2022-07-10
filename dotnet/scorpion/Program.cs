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

using System.Collections;
using System;

namespace Scorpion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private const double kversion = 0.9;
        //static Scorp sc = new Scorp(0, kversion);
        static ArrayList sessions = new ArrayList();

        [STAThread]
        static int Main()
        {
            //Uncomment commented to allow sessions. I am removing sessions until proper work is done to properly isolate outputs etc.
            int current_session = 0;
            sessions.Add(new Scorp(current_session, kversion));

            //sc.th_clean_strt();

            //Add an event to cleanup the system before exiting
            Console.CancelKeyPress += Console_CancelKeyPress;

            //while(true)
            //    sc.readr.access_library(Console.ReadLine());

            string line = null;
            //Create new session on demand
            while(true)
            {
                line = Console.ReadLine();

                if (line.ToLower() == "**new")
                {
                    current_session++;
                    Console.WriteLine("Created new session");
                    sessions.Add(new Scorp(current_session, kversion));
                }
                else if (line.ToLower() == "**back")
                {
                    if (current_session > 0)
                    {
                        current_session--;
                        Console.WriteLine("Session: [{0}]-[{1}]\n", current_session, ((Scorp)sessions[current_session]).mmsec.get_uname());
                    }
                    else
                        Console.WriteLine("No previous session available");
                }
                else if (line.ToLower() == "**next")
                {
                    if (current_session != sessions.Count - 1)
                    {
                        current_session++;
                        Console.WriteLine("Session [{0}]-[{1}]\n", current_session, ((Scorp)sessions[current_session]).mmsec.get_uname());
                    }
                    else
                        Console.WriteLine("No next session available");
                }
                else if (line.ToLower() == "**exit")
                {
                    Console.WriteLine("Removing session [{0}]-[{1}]\n", current_session, ((Scorp)sessions[current_session]).mmsec.get_uname());
                    sessions.RemoveAt(current_session);
                    if (sessions.Count == 0)
                    {
                        Console.WriteLine("No sessions running. Exiting...\n");
                        return 0; //Had Environment.Exit(0)
                    }
                    else if (current_session > 0)
                        current_session--;
                    else if (current_session != sessions.Count - 1)
                        current_session++;
                    Console.WriteLine("Switched to session [{0}]-[{1}]\n", current_session, ((Scorp)sessions[current_session]).mmsec.get_uname());
                }
                else
                {
                    ((Scorp)sessions[current_session]).readr.access_library(line);
                    ((Scorp)sessions[current_session]).th_clean_strt();
                }
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("Interrupt Signal. Exiting..");
            string temp = null;
            ArrayList al_temp = new ArrayList();

            foreach(Scorp session in sessions)
            {
                Console.WriteLine("Closing Instance [{0}]", session.instance);
                session.readr.lib_SCR.apkill(ref temp, ref al_temp);
                session.readr.lib_SCR.instancecleanup();
            }

            Environment.Exit(0);
        }
    }
}

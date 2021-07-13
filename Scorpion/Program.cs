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

namespace Scorpion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int current_session = 0;
            ArrayList sessions = new ArrayList();
            sessions.Add(new Scorp(current_session));

            string line = null;
            //Create new session on demand
            while(true)
            {
                line = Console.ReadLine();
                if (line.ToLower() == "**new")
                {
                    current_session++;
                    Console.WriteLine("Created new session");
                    sessions.Add(new Scorp(current_session));
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
                        Environment.Exit(0);
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
    }
}

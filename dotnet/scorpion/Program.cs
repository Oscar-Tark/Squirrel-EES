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
        static Scorp running_instance;

        [STAThread]
        static int Main()
        {
            running_instance = new Scorp(0, kversion);
            Console.CancelKeyPress += Console_CancelKeyPress;
            while(true)
                running_instance.librarian_instance.librarian.scorpioniee(Console.ReadLine());
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("Interrupt Signal. Exiting..");

            string temp = default;
            ArrayList al_temp = new ArrayList();

            running_instance.librarian_instance.librarian.apkill(ref temp, ref al_temp);
            running_instance.librarian_instance.librarian.instancecleanup();

            Environment.Exit(0);
        }
    }
}

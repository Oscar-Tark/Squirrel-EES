

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
            lineFeed.lineReader();
            //running_instance.lineReader();
            return 0;
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

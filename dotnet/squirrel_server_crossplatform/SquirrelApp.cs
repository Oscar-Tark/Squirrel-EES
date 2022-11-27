using System.Reflection;
using System.Security;
using ScorpionConsoleReadWrite;

namespace Scorpion
{
    //CHANGE CLASS NAME
    public class Scorp
    {
        //Instance descriptor
        public int instance;

        //Initialize classes
        public void InitClasses(out LibrarianInstance lib_instance, out Memory mem_instance, out Scorpion_MDB.ScorpionMicroDB xmldb_instance, out Memory_Security.Secure_Memory mmsec_instance, out Memory_Security.Sanitizer memsan_instance, out Crypto.Cryptographer crypto_instance, out ScorpionProcesses.ScorpionProcessHandler proc_instance, out Scorpion_LOG.Scorpion_LOG sclog_instance, out SessionDependentNetworkHandlers sdh_instance, out ScorpionTimer sctme_instance, out XMLDBEditor.XMLDBEditorRun xmldbeditor)
        {
            lib_instance = new LibrarianInstance();
            mem_instance = new Memory();
            xmldb_instance = new Scorpion_MDB.ScorpionMicroDB();
            xmldbeditor = new XMLDBEditor.XMLDBEditorRun();
            mmsec_instance = new Memory_Security.Secure_Memory();
            memsan_instance = new Memory_Security.Sanitizer();
            crypto_instance = new Crypto.Cryptographer();
            proc_instance = new ScorpionProcesses.ScorpionProcessHandler();
            sclog_instance = new Scorpion_LOG.Scorpion_LOG();
            sdh_instance = new SessionDependentNetworkHandlers();
            sctme_instance = new ScorpionTimer();
        }

        //Classes used by scorpion
        public LibrarianInstance librarian_instance;
        public Scorpion_MDB.ScorpionMicroDB vds;
        public XMLDBEditor.XMLDBEditorRun xmldbeditor;
        public Crypto.Cryptographer crypto;
        public Memory_Security.Secure_Memory mmsec;
        public Memory_Security.Sanitizer san;
        public ScorpionTimer tms;
        public Memory mem;
        public SessionDependentNetworkHandlers sdh;
        public Scorpion_LOG.Scorpion_LOG sclog;
        public ScorpionProcesses.ScorpionProcessHandler scp;

        public Scorp(int instance_descriptor, double version)
        {
            Types.HANDLE = this;

            //Assign the session instance int:identifier for this instance
            instance = instance_descriptor;

            //Initialization functions
            InitClasses(out librarian_instance, out mem, out vds, out mmsec, out san, out crypto, out scp, out sclog, out sdh, out tms, out xmldbeditor);

            //Check that the main data directory exists
            if (!checkDirectories())
                return;

            //Create logging path
            sclog.startLoggingPath(Types.main_user_path);

            SecureString ss_passcode = new SecureString();
            string uname = null;
            int tries = 1;
            const int max_tries = 2;
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
                        auth.create_user(librarian_instance.librarian.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance));
                        tries = 0;
                    }
                }
                tries++;
            }

            ConsoleWrite.writeSpecial(string.Format("\nWelcome {1} to Scorpion Enterprise Server V1.0b\n\n{0}", "Licensed Under the GNU GPL Version 3\n< Scorpion IEE Copyright(C) 2020+ Oscar Arjun Singh Tark >\n\nThis program is free software: you can redistribute it and / or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or(at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see < http://www.gnu.org/licenses/>.\n", uname));
            ConsoleWrite.writeSpecial(string.Format("-------------------------\nCPU logical Cores: {0}\nMachine name: {1}\nOperating system: {4}\n64bit OS: {2}\n64bit process: {3}\nProcess ID: {5}\nInstance: {6}\nUsername: {7}\n-------------------------\n", Environment.ProcessorCount, Environment.MachineName, Environment.Is64BitProcess, Environment.Is64BitOperatingSystem, Environment.OSVersion, Environment.CurrentManagedThreadId, instance, mmsec.get_uname()));

            mem.LoadSystemVars();
        }

        //Check Scorpion directory exists if not create
        private bool checkDirectories()
        {
            ConsoleWrite.writeOutput("Checking main user directory exists..");
            if (!Directory.Exists(Types.main_user_path))
                Directory.CreateDirectory(Types.main_user_path);

            //Check if the Directory was created if not return false, if yes return true
            if (!Directory.Exists(Types.main_user_path))
                return false;
            
            //Main directory created then continue
            if (!Directory.Exists(Types.main_user_manuals_path))
                Directory.CreateDirectory(Types.main_user_manuals_path);
            if (!Directory.Exists(Types.main_user_projects_path))
                Directory.CreateDirectory(Types.main_user_projects_path);
            if (!Directory.Exists(Types.main_user_aes_path))
                Directory.CreateDirectory(Types.main_user_aes_path);
            if (!Directory.Exists(Types.main_user_rsa_path))
                Directory.CreateDirectory(Types.main_user_rsa_path);

            return true;
        }
    }
}

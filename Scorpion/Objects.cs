/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark>

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
using OpenTK;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;

namespace Scorpion
{
    public partial class Form1
    {
        public void start_classes()
        {
            types = new Types(this);
            vds = new Dumper.Virtual_Dumper_System(this);
            crypto = new Crypto.Cryptographer(this);
            hook = new Hooking.Hooker(this);
            mmsec = new Memory_Security.Secure_Memory(this);
            fleoper = new File_operations.Fileopr(this);
            san = new Memory_Security.Sanitizer(this);

            types.load_system_vars();
            readr = new reader(this);
            return;
        }

        private object CloneObject(object o)
        {
            Object p = o.GetType().InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);

            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                if (pi.CanWrite)
                    pi.SetValue(p, pi.GetValue(o, null), null);
            }
            return p;
        }

        public int GUI_TEMPLATE_COUNT = 0;

        public reader readr;
        public GameWindow game;
        public Dumper.Virtual_Dumper_System vds;
        public Crypto.Cryptographer crypto;
        public Hooking.Hooker hook;
        public Memory_Security.Secure_Memory mmsec;
        public File_operations.Fileopr fleoper;
        public Memory_Security.Sanitizer san;

        public string SHA;
        public string[] cmdargs;

        public string Mess;
        public int Index = 0; //Reader Index
        public string Prog_s; //Application String
        public string Orig_path; //Path
        public string func_tmp = ""; public string path_tmp = "";
        public bool real_time = false;
        public int current_thread_count = 0;

        public AutoCompleteStringCollection acsc;
        //public AutocompleteMenuNS.AutocompleteMenu acm_xmd = new AutocompleteMenuNS.AutocompleteMenu();

        public bool is_hardware_accelerated = false;

        public Types types;

        public enum list_type { db_list };

        //Main Collections

        //TERMS/WIKI TERMS
        public ArrayList AL_TERMS_WIKI_REF = new ArrayList() { "license", "windows", "commands" };
        public ArrayList AL_TERMS_WIKI = new ArrayList() { "[LICENSE]", "[WIKI:]", "[COMMANDS:]" };
        public ArrayList AL_WIKI = new ArrayList() {
            "Licensed Under the GNU GPL Version 3\n<One Platform. Noded Command Framework>\nCopyright (C) <2013-2016>  <Oscar Arjun Singh Tark>\n\nThis program is free software: you can redistribute it and/or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or (at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see<http://www.gnu.org/licenses/>.\n\n",
            "If you are trying to start a cmd process and want it to hold. Set the process name as cmd and in the beginning of the first argument put /K as: '/K ping.exe 127.0.0.1'",
            "One Platform internal commands:\nAll commands start with a specific syntax directive.function(*variable,*variable,...)\n\n>System Commands(Start with one.):\none.write(*variable,*variable,...)\none.about()"
        };

        //Ubearables
        public ArrayList AL_UNBEARABLE_CHARS = new ArrayList() { ",", "]"/*, " "*/, ")" };
        public ArrayList AL_WILDCARDS = new ArrayList() { "-", " " };

        //Events used for DB
        public ArrayList AL_Ref_EVT = new ArrayList();
        public ArrayList AL_EVT = new ArrayList();

        //OPENTK
        /*public ArrayList AL_DISP_DEVICES = new ArrayList();
        public ArrayList AL_OBJ_3D = new ArrayList();
        public ArrayList AL_OBJ_3D_REF = new ArrayList();
        public ArrayList AL_UPDATED_OTK_GUI = new ArrayList();*/

        //Sockets
        public ArrayList AL_SOCK = new ArrayList();
        public ArrayList AL_SOCK_REF = new ArrayList();
        public ArrayList AL_SOCK_SESSION = new ArrayList();

        //Variables
        public ArrayList AL_CURR_VAR = new ArrayList();
        public ArrayList AL_CURR_VAR_REF = new ArrayList();
        public ArrayList AL_CURR_VAR_TAG = new ArrayList();
        public ArrayList AL_CURR_VAR_EVT = new ArrayList();

        //Tables DATA
        public ArrayList AL_TBLE = new ArrayList();
        public ArrayList AL_TBLE_REF = new ArrayList();
        
        //Variables for Queries
        public ArrayList AL_QUERY_VAR = new ArrayList() { "<cell>", "</cell>", "<operator>", "</operator>", "<create>", "</create>", "<delete>", "</delete>", "<search>", "</search>", "<is>", "</is>", "<isnot>", "</isnot>", "<getall>", "</getall>" };

        //Recursive Callers
        public ArrayList AL_REC_REF = new ArrayList();
        public ArrayList AL_REC = new ArrayList();
        public ArrayList AL_REC_TME = new ArrayList();

        //AuthTable
        public ArrayList AL_AUTH_REF = new ArrayList();
        public ArrayList AL_AUTH = new ArrayList();

        //PIPE Networking
        public ArrayList AL_PIPES = new ArrayList();
        public ArrayList AL_PIPES_REF = new ArrayList();

        //TCPIP Networking
        public ArrayList AL_AMCS = new ArrayList();
        public ArrayList AL_AMCS_REF = new ArrayList();

        //SHS REDEFINED TO EXECUTE SYSTEM COMMAND
        public ArrayList AL_SHS_REF = new ArrayList();
        public ArrayList AL_SHS = new ArrayList();
        public ArrayList AL_SHS_APP = new ArrayList();
        public ArrayList AL_SHS_APP_REF = new ArrayList();
        
        //Controllable Running Processes
        public ArrayList AL_PRC_REF = new ArrayList();
        public ArrayList AL_PRC = new ArrayList();

        //HIB SETTINGS
        public ArrayList AL_HIB_FILES = new ArrayList() { Application.StartupPath + "\\System\\Data\\one.vds" };

        //OneDB Dir                                                                                                                                                                 Alternative OneDB Path without \
        public ArrayList AL_DIRECTORIES = new ArrayList() { Application.StartupPath + "\\System\\OneDB\\", Application.StartupPath + "\\", Application.StartupPath + "\\System\\Data\\", Application.StartupPath + "\\System\\OneBack\\", Application.StartupPath + "\\System\\OneDB", Application.StartupPath + "\\System\\OneSource\\", Application.StartupPath + "\\System\\OneAssemblies\\", Application.StartupPath + "\\System\\SQLite\\" };
        public ArrayList AL_EXTENSNS = new ArrayList() { ".vds", ".vdb", ".vdsqlite" };

        //Accessors Definitions
        public ArrayList AL_ACC = new ArrayList() { ">>", "*", ".", "(", ")", "\\", "@", "#" };
        public ArrayList AL_SECTIONS = new ArrayList() { "data", "gui", "commands", "variables", "assemblies", "scripts" };

        //Operators Definitions
        public ArrayList AL_OPRTRS = new ArrayList() { "+", "-", "/", "*", "%", "&&", "||", "<", ">", "=", "<=", ">=", "!", /*contains*/ "?", /*Union*/"<->", "<-", "->", /*Intersection*/ "<~>" };

        public string[] AL_KEYS = new string[9]
        {
            "fnc",
            "io",
            "net",
            "eng",
            "db",
            "mem",
            "exe",
            "typ",
            "sec"
        };

        /*public string[][] AL_CALLERS = new string[1][]
        {
            //ALL KEYS CORRESPOND TO INDEX OF CALLERS
        };*/

      //TRANSFER TO TABLES
        public ArrayList AL_MESSAGE = new ArrayList() { "[IP REFUSED]", "[PORT REFUSED]", "[FATAL EXIT]", "The specified GUI element could not run a method, make sure it is not disposed, if so delete it and create it again.", "Databases may only be opened from the \\OneDB folder, please make sure that the database you would like to open is copied to that folder or use the import tool in the menu VDB->Import Database.", "[WARNING]", "A System function has raised an error, this might be the cause of a faulty configuration or an unfound bug, Press 'Ignore' to continue on using the system.", "Main database not found. The system will create it on shutdown.\n\nIF you are seeing this message over and over again, your system database might be corrupt. Run the command 'mem.deleteall()', current running system variables will be saved to a fresh database on shutdown, so that you do not loose your work.", "File loaded successfully to variable." };

        //Assemblies
        public ArrayList AL_ASSEMB = new ArrayList();
        public ArrayList AL_ASSEMB_REF = new ArrayList();
        public ArrayList AL_ASSEMB_INST = new ArrayList();
        public ArrayList AL_ASSEMB_PROG = new ArrayList();

        //SQL/NOSQL
        public ArrayList AL_SQL_REF = new ArrayList();
        public ArrayList AL_SQL = new ArrayList();
        

        //TCP
        public long session = 0;
        public static string IP = "127.0.0.1";
    }
}
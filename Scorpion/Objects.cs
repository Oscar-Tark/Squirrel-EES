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
using System.Collections;
using System.Windows.Forms;
using System.Reflection;

namespace Scorpion
{
    public class Timer_
    {
        Form1 Do_on;
        public ArrayList AL_REC = new ArrayList() { };
        public ArrayList AL_REC_REF = new ArrayList();
        System.Threading.Timer tms;

        public Timer_(Form1 fm1)
        {
            Do_on = fm1;
            tms = new System.Threading.Timer(tms_call, null, 0, 10000);
            return;
        }

        public void add(object Name, object Object)
        {
            /*VARIABLES MUST BE @ not * */
            if (AL_REC_REF.IndexOf(Name) < 0)
            {
                AL_REC.Add(Object);
                AL_REC_REF.Add(Name);
            }
            else
                Do_on.write_to_cui("Name for recursive function already exists");
            return;
        }

        public void delete(object Name)
        {
            AL_REC.RemoveAt(AL_REC_REF.IndexOf(Name));
            AL_REC_REF.Remove(Name);
            return;
        }

        public void tms_call(Object obj)
        {
            foreach (string command in AL_REC)
                Do_on.readr.lib_SCR.scorpioniee((object)(command.Replace('@', '*')));

            obj = null;
            GC.Collect();
            return;
        }
    }

    //CLASSES HERE
    public partial class Form1
    {
        public reader readr;
        public Dumper.Virtual_Dumper_System vds;
        public Crypto.Cryptographer crypto;
        public Hooking.Hooker hook;
        public Memory_Security.Secure_Memory mmsec;
        public Memory_Security.Sanitizer san;
        public Timer_ tms;
    }

    public partial class Form1
    {
        public void start_classes()
        {
            types = new Types(this);
            vds = new Dumper.Virtual_Dumper_System(this);
            crypto = new Crypto.Cryptographer(this);
            hook = new Hooking.Hooker(this);
            mmsec = new Memory_Security.Secure_Memory(this);
            san = new Memory_Security.Sanitizer(this);
            tms = new Timer_(this);

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

        public string[] cmdargs;

        public string Mess;
        public int Index = 0; //Reader Index
        public string Prog_s; //Application String
        public string Orig_path; //Path
        public string func_tmp = ""; public string path_tmp = "";
        public bool real_time = false;
        public int current_thread_count = 0;

        public Types types;

        public enum list_type { db_list };

        //Main Collections

        //COMMAND LOG
        public int commands_point = 0;
        public string[] commands = new string[64];

        //Ubearables
        public ArrayList AL_UNBEARABLE_CHARS = new ArrayList() { ",", "]"/*, " "*/, ")" };
        public ArrayList AL_WILDCARDS = new ArrayList() { "-", " " };

        //Events used for DB
        public ArrayList AL_Ref_EVT = new ArrayList();
        public ArrayList AL_EVT = new ArrayList();

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

        //AuthTable
        public ArrayList AL_AUTH_REF = new ArrayList();
        public ArrayList AL_AUTH = new ArrayList();

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
        //public ArrayList AL_SECTIONS = new ArrayList() { "data", "gui", "commands", "variables", "assemblies", "scripts" };

        //Operators Definitions
        public ArrayList AL_OPRTRS = new ArrayList() { "+", "-", "/", "*", "%", "&&", "||", "<", ">", "=", "<=", ">=", "!", /*contains*/ "?", /*Union*/"<->", "<-", "->", /*Intersection*/ "<~>" };


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
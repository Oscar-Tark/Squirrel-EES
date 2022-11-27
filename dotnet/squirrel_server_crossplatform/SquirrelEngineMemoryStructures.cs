using System.Collections;
using System.Reflection;

namespace Scorpion
{
    public class ScorpionTimer
    {
        public ArrayList AL_REC = new ArrayList { };
        public ArrayList AL_REC_REF = new ArrayList();
        Timer tms;
        int interval = 100000;

        public ScorpionTimer()
        {
            tms = new Timer(tms_call, null, 0, interval);
            return;
        }

        public void change_interval(int time)
        {
            interval = time;
            tms.Change(0, interval);
            return;
        }

        public void add(object Name, object Object)
        {
            /*VARIABLES MUST BE {&quot}, {&var} not * or *'' */
            if (AL_REC_REF.IndexOf(Name) < 0)
            {
                AL_REC.Add(Object);
                AL_REC_REF.Add(Name);
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Recursive functions by default will run every " + interval + "s. To run functions everyday use the recursivetime::*time function to store 86400000s");
            }
            else
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Name for recursive function already exists");
            return;
        }

        public void delete(object Name)
        {
            AL_REC.RemoveAt(AL_REC_REF.IndexOf(Name));
            AL_REC_REF.Remove(Name);
            return;
        }

        public void tms_call(object obj)
        {
            foreach (string command in AL_REC)
                Types.HANDLE.librarian_instance.librarian.scorpioniee(Enginefunctions.replace_fakes(command));
            obj = null;
            GC.Collect();
            return;
        }
    }

    //NEW WAY OF DEALING WITH MEMORY, SHIFT HERE
    public class Memory
    {
        private object CloneObject(object o)
        {
            object p = o.GetType().InvokeMember("", BindingFlags.CreateInstance, null, o, null);
            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                if (pi.CanWrite)
                    pi.SetValue(p, pi.GetValue(o, null), null);
            }
            return p;
        }

        //COMMAND LOG
        public int commands_point = 0;
        public string[] commands = new string[64];

        public void AddTcpPath(string private_s_RSA, string public_s_RSA)
        {
            //Add as secure string?
            //No ref, we need actual value copied
            AL_TCP_KY.Add(new string[2] { private_s_RSA, public_s_RSA });
            return;
        }

        public void RemoveTcpPath(ref int ndx)
        {
            try
            {
                //Server started with a key
                AL_TCP_KY.RemoveAt(ndx);
            }
            catch { /*Server started without a key*/ }
            return;
        }

        public string[] GetTcpKeyPath(int ndx)
        {
            return (string[])AL_TCP_KY[ndx];
        }

        //Loads system/default variables to the scorpion memory structure
        [Obsolete] //Should be initialized immediately on memory structure creation, there is a major bug with S_YES
        //Initialize Scorpion.DefaultVariables
        public void LoadSystemVars()
        {
            //Set variables that should exist by default
            Types.HANDLE.librarian_instance.librarian.var("", new ArrayList(5) { Types.S_Yes, Types.S_No, "null", "yes", "no", "temp", "path" });
            Types.HANDLE.librarian_instance.librarian.varset("", new ArrayList(5) { "yes", "'" + Types.S_Yes + "'" });
            Types.HANDLE.librarian_instance.librarian.varset("", new ArrayList(5) { "no", "'" + Types.S_No + "'" });

            Types.HANDLE.librarian_instance.librarian.varset("", new ArrayList(5) { "null", "'" + Types.S_NULL + "'" });
            Types.HANDLE.librarian_instance.librarian.varset("", new ArrayList(5) { "path", "'" + Types.main_user_path + "'" });
            Types.HANDLE.librarian_instance.librarian.varset("", new ArrayList(5) { "true", "'" + Types.S_Yes + "'" });
            return;
        }

        //<--
        /*Could have used concurrentbag, but in this case the lock statement and synchronized arraylists even at degredation of performance, works.
        Define Arraylist Synchronized wrappers for threadsafe Memory structures:*/
        internal ArrayList AL_CURR_VAR = ArrayList.Synchronized(new ArrayList());
        internal ArrayList AL_CURR_VAR_REF = ArrayList.Synchronized(new ArrayList());
        internal ArrayList AL_CURR_VAR_TAG = ArrayList.Synchronized(new ArrayList());
        internal ArrayList AL_CURR_VAR_NACESSED = ArrayList.Synchronized(new ArrayList());

        //Scorpion Formatted Tcpservers
        internal ArrayList AL_TCP = ArrayList.Synchronized(new ArrayList());
        internal ArrayList AL_TCP_REF = ArrayList.Synchronized(new ArrayList());
        internal ArrayList AL_TCP_KY = ArrayList.Synchronized(new ArrayList());
        internal ArrayList AL_TCP_CONNECTION_STRING = ArrayList.Synchronized(new ArrayList());
    }
}
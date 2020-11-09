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
using System.Reflection;
using System.Threading;

namespace Scorpion
{
    public class Timer_
    {
        Scorp Do_on;
        public ArrayList AL_REC = new ArrayList { };
        public ArrayList AL_REC_REF = new ArrayList();
        Timer tms;
        Enginefunctions ef__ = new Enginefunctions();
        int interval = 100000;

        public Timer_(Scorp fm1)
        {
            Do_on = fm1;
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
                Do_on.write_to_cui("Recursive functions by default will run every " + interval + "s. To run functions everyday use the recursivetime::*time function to store 86400000s");
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

        public void tms_call(object obj)
        {
            foreach (string command in AL_REC)
                Do_on.readr.lib_SCR.scorpioniee(ef__.replace_fakes(command));

            obj = null;
            GC.Collect();
            return;
        }
    }

    //CLASSES HERE
    public partial class Scorp
    {
        public reader readr;
        public Dumper.Virtual_Dumper_System vds;
        public Crypto.Cryptographer crypto;
        public Memory_Security.Secure_Memory mmsec;
        public Memory_Security.Sanitizer san;
        public Timer_ tms;
        public Workspaces.Workspaces wkp;
        public Memory mem;
    }

    public partial class Scorp
    {
        public void start_classes()
        {
            vds = new Dumper.Virtual_Dumper_System(this);
            crypto = new Crypto.Cryptographer(this);
            mmsec = new Memory_Security.Secure_Memory(this);
            san = new Memory_Security.Sanitizer(this);
            tms = new Timer_(this);

            readr = new reader(this);
            types = new Types(this);
            types.load_system_vars();
            wkp = new Workspaces.Workspaces(this);
            mem = new Memory();
            return;
        }

        private object CloneObject(object o)
        {
            Object p = o.GetType().InvokeMember("", BindingFlags.CreateInstance, null, o, null);

            foreach (PropertyInfo pi in o.GetType().GetProperties())
            {
                if (pi.CanWrite)
                    pi.SetValue(p, pi.GetValue(o, null), null);
            }
            return p;
        }

        public string[] cmdargs;
        public long engine_ndx = 0;
        public Types types;
        public enum list_type { db_list };

        //COMMAND LOG
        public int commands_point = 0;
        public string[] commands = new string[64];

        //Ubearables
        public readonly string[] AL_UNBEARABLE_CHARS = new string[3] { ",", "]", ")" };
        public readonly string[] AL_WILDCARDS = new string[2] { "-", " " };

        //Sockets
        public ArrayList AL_SOCK = new ArrayList();
        public ArrayList AL_SOCK_REF = new ArrayList();
        public ArrayList AL_SOCK_SESSION = new ArrayList();

        //Tcp-->
        public ArrayList AL_TCP = new ArrayList();
        public ArrayList AL_TCP_REF = new ArrayList();
        private ArrayList AL_TCP_KY = new ArrayList();

        public void add_tcp_key_path(string private_s_RSA, string public_s_RSA)
        {
            //Add as secure string?
            //No ref, we need actual value copied
            AL_TCP_KY.Add(new string[2]{ private_s_RSA, public_s_RSA });
            return;
        }

        public void remove_tcp_key_path(ref int ndx)
        {
            AL_TCP_KY.RemoveAt(ndx);
            return;
        }

        public string[] get_tcp_key_paths(int ndx)
        {
            return (string[])AL_TCP_KY[ndx];
        }
        //<--


        //Variables
        public ArrayList AL_CURR_VAR = new ArrayList();
        public ArrayList AL_CURR_VAR_REF = new ArrayList();
        public ArrayList AL_CURR_VAR_TAG = new ArrayList();
        public ArrayList AL_CURR_VAR_EVT = new ArrayList();

        //Tables DATA
        public ArrayList AL_TBLE = new ArrayList();
        public ArrayList AL_TBLE_REF = new ArrayList();

        //TCPIP Networking
        public ArrayList AL_AMCS = new ArrayList();
        public ArrayList AL_AMCS_REF = new ArrayList();

        //Assemblies
        public ArrayList AL_ASSEMB = new ArrayList();
        public ArrayList AL_ASSEMB_REF = new ArrayList();

        //Inatantiated Assemblies
        public ArrayList AL_ASSEMB_INST = new ArrayList();
        public ArrayList AL_ASSEMB_PROG = new ArrayList();
    }

    public partial class Memory
    {
        //new memory class

        //Must be singleton
        bool instance = false;
        /*Memory Memory()
        {
            if (!instance)
                return new Memory();
            return null;
        }*/

        //Tcpclients
        private ArrayList AL_TCP_CLIENTS = new ArrayList();
        private ArrayList AL_TCP_CLIENTS_REF = new ArrayList();
        private ArrayList AL_TCP_CLIENTS_KY = new ArrayList();
    }

    public partial class Memory
    {
        public void add_tcpclient(ref string reference, ref SimpleTCP.SimpleTcpClient to_add, ref string private_key_path, ref string public_key_path)
        {
            lock (AL_TCP_CLIENTS) lock (AL_TCP_CLIENTS_REF) lock (AL_TCP_CLIENTS_KY)
                    {
                        AL_TCP_CLIENTS.Add(to_add);
                        AL_TCP_CLIENTS_REF.Add(reference);
                        AL_TCP_CLIENTS_KY.Add(new string[] { private_key_path, public_key_path });
                    }
            return;
        }

        public SimpleTCP.SimpleTcpClient get_tcpclient(int ndx)
        {
            return (SimpleTCP.SimpleTcpClient)AL_TCP_CLIENTS[ndx];
        }

        public int get_index_tcpclient(object client)
        {
            return AL_TCP_CLIENTS.IndexOf(client);
        }

        public int get_index_tcpclient(string client)
        {
            return AL_TCP_CLIENTS_REF.IndexOf(client);
        }

        public string[] get_tcpclient_key_paths(int ndx)
        {
            return (string[])AL_TCP_CLIENTS_KY[ndx];
        }

        public void remove_tcpclient(int ndx)
        {
            lock (AL_TCP_CLIENTS) lock (AL_TCP_CLIENTS_REF) lock (AL_TCP_CLIENTS_KY)
                    {
                        AL_TCP_CLIENTS.RemoveAt(ndx);
                        AL_TCP_CLIENTS_KY.RemoveAt(ndx);
                        AL_TCP_CLIENTS_REF.RemoveAt(ndx);
                    }
            return;
        }
    }
}
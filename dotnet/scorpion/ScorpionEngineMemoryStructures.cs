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

using System;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace Scorpion
{
    public class Timer_
    {
        Scorp HANDLE = default;
        public ArrayList AL_REC = new ArrayList { };
        public ArrayList AL_REC_REF = new ArrayList();
        Timer tms;
        Enginefunctions ef__ = new Enginefunctions();
        int interval = 100000;

        public Timer_(Scorp HANDLE_)
        {
            HANDLE = HANDLE_;
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
                HANDLE.write_to_cui("Recursive functions by default will run every " + interval + "s. To run functions everyday use the recursivetime::*time function to store 86400000s");
            }
            else
                HANDLE.write_to_cui("Name for recursive function already exists");
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
                HANDLE.readr.lib_SCR.scorpioniee(ef__.replace_fakes(command), HANDLE);
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

        //<--
        //Could have used dictionaries, but meh
        //Variables
        internal List<object> AL_CURR_VAR = new List<object>();
        internal List<string> AL_CURR_VAR_REF = new List<string>();
        internal List<string> AL_CURR_VAR_TAG = new List<string>();
        internal List<DateTime> AL_CURR_VAR_NACESSED = new List<DateTime>();

        //Tables DATA
        internal List<object> AL_TBLE = new List<object>();
        internal List<string> AL_TBLE_REF = new List<string>();
        internal List<string> AL_TBLE_PATH = new List<string>();

        //Tcpclients
        internal List<object> AL_TCP_CLIENTS = new List<object>();
        internal List<string> AL_TCP_CLIENTS_REF = new List<string>();
        internal List<string[]> AL_TCP_CLIENTS_KY = new List<string[]>();

        //Tcpservers
        internal List<object> AL_TCP = new List<object>();
        internal List<object> AL_TCP_REF = new List<object>();
        internal List<string[]> AL_TCP_KY = new List<string[]>();
    }
}
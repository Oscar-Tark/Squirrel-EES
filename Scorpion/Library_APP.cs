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
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.ComponentModel;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        //PASS 30-11-14
        public void APP(ref string Scorp_Line)
        {
            if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[0] + Do_on.AL_ACC[3].ToString()))
            {
                write_to_cui(Scorp_Line);
            }
                //Change Accessors
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[1] + Do_on.AL_ACC[3].ToString()))
            {
                change_accessor(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[2] + Do_on.AL_ACC[3].ToString()))
            {
                About ab = new About();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[3] + Do_on.AL_ACC[3].ToString()))
            {
                Application.Exit();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[4] + Do_on.AL_ACC[3].ToString()))
            {
                Application.Restart();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[110] + Do_on.AL_ACC[3].ToString()))
            {
                execute_windows_command(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[111] + Do_on.AL_ACC[3].ToString()))
            {
                browse_wiki(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[105] + Do_on.AL_ACC[3].ToString()))
            {
                MessageBox.Show("IMPLEMENT");
                //load db
                //load gui
                //submit to gui setup system
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[112] + Do_on.AL_ACC[3].ToString()))
            {
                load_analyzer();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[113] + Do_on.AL_ACC[3].ToString()))
            {
                load_cui();
            }
            else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {"+ Do_on.AL_ACC_SUP[3] +"} in line {" + Scorp_Line + "}"); }

            //CLEAN
            Scorp_Line = null;

            return;
        }

        public void load_analyzer()
        {
            Do_on.start_Analyzer_object();
            Do_on.f.Controls.Add(Do_on.vdb_analyzer);
            Do_on.vdb_analyzer.spc_pnl.Controls.Add(Do_on.spc);
            return;
        }

        public void load_cui()
        {
            Do_on.f.Controls.Add(Do_on.spc);
            Do_on.vdb_analyzer.Dispose();
            return;
        }

        public void browse_wiki(ref string Scorp_Line_Exec)
        {
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            for (int i = 0; i < al.Count; i++)
            {
                Do_on.write_to_cui(Do_on.AL_WIKI[Do_on.AL_TERMS_WIKI_REF.IndexOf(var_get(al[i].ToString()))].ToString());
            }

            return;
        }

        public void execute_windows_command(ref string Scorp_Line_Exec)
        {
            //(*Processname/Directory, *Argument,*Argument...)

            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            string accum = "";
            for(int i = 1; i< al.Count; i++)
            {
                if(i!=1)
                { accum = accum + " "; }
                accum = accum + var_get(al[i].ToString()).ToString();
            }

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo((var_get(al[0].ToString()).ToString()), accum);
            p.Start();

            Do_on.AL_PRC.Add(p);
            Do_on.AL_PRC_REF.Add(p.ProcessName);
            Do_on.write_to_cui("Started Process: '" + p.ProcessName + "'.\n" + Do_on.AL_TERMS_WIKI[0].ToString() + Do_on.AL_WIKI[0].ToString());

            var_arraylist_dispose(ref al);
            accum = null;

            return;
        }

        //Accessors and functions
        private void change_accessor(ref string Scorp_Line_Exec)
        {
            //(*actual_name,*new_name) - These can be uncreated variables
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            Do_on.AL_ACC_SUP[Do_on.AL_ACC_SUP.IndexOf(var_cut_symbol(al[0].ToString()), 0)] = var_cut_symbol(al[1].ToString());

            var_arraylist_dispose(ref al);

            return;
        }

        //PASS
        private void write_to_cui(string Scorp_Line_Exec)
        {
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            foreach (object o in al)
            {
                Do_on.write_to_cui(var_get(o.ToString()).ToString() + "\n");
            }

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;
            return;
        }

        //PASS
        private void event_path(string Scorp_Line_Exec)
        {
            //(path(*))
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            foreach (string s in al)
            {
                Do_on.AL_Ref_EVT.Add(var_get(s));
            }

            FileStream fs = new FileStream(Do_on.Current_dir + "\\" + Scorp_Line_Exec, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Do_on.AL_EVT.Add(sr.ReadToEnd());
            fs.Flush();
            sr.Close();
            fs.Close();

            //Clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        /*private void realtime(string Scorp_Line_Exec)
        {
            int ndx = Scorp_Line_Exec.IndexOf("realtime(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string result = Scorp_Line_Exec.Remove(ndx2);
            result = result.Remove(0, ndx + 9);

            if (result == "yes" || result == "true")
            {
                Do_on.real_time = true;
                Do_on.real_time_time.Start();
            }
            else if (result == "no" || result == "false")
            {
                Do_on.real_time = false;
                Do_on.real_time_time.Stop();
            }
            return;
        }

        private void realtime_clocking(string Scorp_Line_Exec)
        {
            int ndx = Scorp_Line_Exec.IndexOf("realtimelapse(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string result = Scorp_Line_Exec.Remove(ndx2);
            result = result.Remove(0, ndx + 14);

            Do_on.real_time_time.Interval = Convert.ToInt32(result);
        }*/
    }
}
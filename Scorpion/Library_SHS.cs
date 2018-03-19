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
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Management;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        public void SHS(ref string Scorp_Line)
        {
            if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[7] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[80] + Do_on.AL_ACC[3].ToString()))
            {
                execute_shs(ref Scorp_Line);
            }
            else if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[7] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[87] + Do_on.AL_ACC[3].ToString()))
            {
                start_builder();
            }
            else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {" + Do_on.AL_ACC_SUP[7] + "} in line {" + Scorp_Line + "}"); }

            Scorp_Line = null;

            return;
        }

        public ArrayList get_arguments(string Name, string arg)
        {
            ProcessStartInfo psi = new ProcessStartInfo(Do_on.AL_SHS_APP[Do_on.AL_SHS_APP_REF.IndexOf(Name)].ToString(), arg);
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            ArrayList al = new ArrayList();
            string temp;

            using (Process p = Process.Start(psi))
            {
                using (StreamReader reader = p.StandardOutput)
                {
                    while (!reader.EndOfStream)
                    {
                        temp = reader.ReadLine();
                        //check
                        if (check_wildcard(ref temp))
                        {
                            al.Add(temp);
                        }
                    }
                }
            }

            arg = null;
            Name = null;
            return al;
        }

        private bool check_wildcard(ref string arg)
        {
            if (arg.Replace(" ", "").StartsWith("-"))
            {

            }

            return true;
        }

        public void start_builder()
        {
            Argument_Builder ab = new Argument_Builder(Do_on);
            return;
        }

        public void register_applications()
        {
            DirectoryInfo d = new DirectoryInfo(Environment.CurrentDirectory + "\\Tools");
            foreach (FileInfo fi in d.GetFiles("*.exe", SearchOption.AllDirectories))
            {
                try
                {
                    Do_on.AL_SHS_APP_REF.Add(fi.Name.Replace(" ", "_"));
                    Do_on.AL_SHS_APP.Add(fi.FullName.ToString());
                }
                catch { write_to_cui("1 SHS tool not loaded."); }
            }

            //Statics

            Do_on.AL_SHS_APP_REF.Add("cmd.exe");
            Do_on.AL_SHS_APP.Add("cmd.exe");

            d = null;

            return;
        }

        //[DllImport("User32", CharSet = CharSet.Auto, ExactSpelling = true)]
        //internal static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndParent);
        //shs.function(args)
        private void execute_shs(ref string Scorp_Line_Exec)
        {
            //shs.cll(*application,*args,*iscmd,*hosted)
            ArrayList al = cut_variables(Scorp_Line_Exec);

            Process p = new Process(); ProcessStartInfo psi = null;

            psi = new ProcessStartInfo();
            try
            {
                psi.FileName = @"" + @Do_on.AL_SHS_APP[Do_on.AL_SHS_APP_REF.IndexOf(var_cut_symbol(al[0].ToString()))].ToString() + @"";
            }
            catch
            {
                psi.FileName = @"" + @var_cut_symbol(al[0].ToString()).ToString() + @"";
            }
            try
            {
                psi.Arguments = @var_cut_symbol(var_get(al[1].ToString()).ToString());
            }
            catch { }

            p.StartInfo = psi;

            p.Exited += new EventHandler(p_Exited);
            p.EnableRaisingEvents = true;

            p.Start();

            Do_on.write_to_cui("Started Process: [" + psi.FileName + "]\nWith Arguments: [" + psi.Arguments + "]\n");

            //Do_on.AL_PRC_REF.Add(Do_on.AL_SHS_APP_REF.IndexOf(var_cut_symbol(al[0].ToString())));
            //Do_on.AL_PRC.Add(p);

            /*try
            {
                Do_on.spc.tv_ip.Nodes.Find("prc", true)[0].Nodes.Add(p.Id.ToString(), p.ProcessName + " [" + psi.Arguments + "]", 296).Nodes.Add("Remove", "Remove", 90);
            }
            catch { }

            Do_on.spc.add_xmd(new xmd(p.StartInfo.FileName, p.StartInfo.Arguments, Do_on));
            p.Dispose();*/

            //var_arraylist_dispose(ref al);

            return;
        }

        void p_Exited(object sender, EventArgs e)
        {
            try
            {
                Do_on.AL_PRC_REF.RemoveAt(Do_on.AL_PRC.IndexOf(((Process)sender)));
                Do_on.AL_PRC.Remove(((Process)sender));
            }
            catch { }

            try
            {
                ((Process)sender).Close();
            }
            catch { }
            //Do_on.spc.treeView2.Nodes.Remove(Do_on.spc.treeView2.Nodes.Find(((Process)sender).Id.ToString(), true)[0]);
            
            sender = null;
            e = null;
            return;
        }

        private void ping(ref string Scorp_Line_Exec)
        {
            //(*name,*address)
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            System.Diagnostics.Process p = new Process();
            System.Diagnostics.ProcessStartInfo psi = new ProcessStartInfo("cmd", @"/k ping.exe " + var_get(al[0].ToString()).ToString());
            p.StartInfo = psi;
            p.Start();
            StreamReader sri = p.StandardOutput;
            write_to_cui(sri.ReadLine());

            Scorp_Line_Exec = null;

            return;
        }
    }
}
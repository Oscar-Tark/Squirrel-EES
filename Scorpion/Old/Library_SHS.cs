/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark> <Benjamin Jack Johnson>

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
using System.Threading;
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
            else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {" + Do_on.AL_ACC_SUP[7] + "} in line {" + Scorp_Line + "}"); }

            Scorp_Line = null;

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

            /*if (var_get(al[2].ToString()).ToString() == Do_on.types.S_Yes)
            {
                /*try
                {
                    psi = new ProcessStartInfo(Do_on.AL_SHS_APP[Do_on.AL_SHS_APP_REF.IndexOf(var_cut_symbol(al[0].ToString()))].ToString());
                }
                catch
                {
                    psi = new ProcessStartInfo(var_cut_symbol(al[0].ToString()));
                }*/

                psi = new ProcessStartInfo("cmd.exe");
                //CHECK OS
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

                /*try
                {
                    psi.Arguments = var_cut_symbol(var_get(al[1].ToString()).ToString());
                }
                catch
                {
                    psi.Arguments = var_cut_symbol(var_get(al[1].ToString()).ToString());
                }*/

                //psi = new ProcessStartInfo("cmd");
                /*try
                {
                    psi.Arguments = @"/k """ + @Do_on.AL_SHS_APP[Do_on.AL_SHS_APP_REF.IndexOf(var_cut_symbol(al[0].ToString()))].ToString() + @""" " + var_cut_symbol(var_get(al[1].ToString()).ToString());
                }
                catch
                {
                    psi.Arguments = @"/k """ + @var_cut_symbol(al[0].ToString()).ToString() + @""" " + var_cut_symbol(var_get(al[1].ToString()).ToString());
                }*/

                //psi.CreateNoWindow = true;
                //psi.UseShellExecute = false;
                //psi.RedirectStandardOutput = true;
            /*}
            else if (var_get(al[2].ToString()).ToString() == Do_on.types.S_No)
            {
                try
                {
                    psi = new ProcessStartInfo(Do_on.AL_SHS_APP[Do_on.AL_SHS_APP_REF.IndexOf(var_cut_symbol(al[0].ToString()))].ToString());
                }
                catch
                {
                    psi = new ProcessStartInfo(var_cut_symbol(al[0].ToString()).ToString());
                }
                psi.Arguments = var_cut_symbol(var_get(al[1].ToString()).ToString());
            }*/

            p.StartInfo = psi;

            p.Exited += new EventHandler(p_Exited);
            p.EnableRaisingEvents = true;
            //
            Do_on.write_to_cui("Started Process: [" + psi.FileName + "]\nWith Arguments: [" + psi.Arguments + "]\n");


            Do_on.AL_PRC_REF.Add(Do_on.AL_SHS_APP_REF.IndexOf(var_cut_symbol(al[0].ToString())));
            Do_on.AL_PRC.Add(p);

            try
            {
                Do_on.spc.treeView2.Nodes.Find("prc", true)[0].Nodes.Add(p.Id.ToString(), p.ProcessName + " [" + psi.Arguments + "]", 296).Nodes.Add("Remove", "Remove", 90);
            }
            catch { }

            /*if (var_get(al[2].ToString()).ToString() == Do_on.types.S_Yes)
            {
                try
                {
                    if (var_get(al[3].ToString()).ToString() == Do_on.types.S_Yes)
                    {*/
            Do_on.spc.add_xmd(new xmd(p.StartInfo.FileName, p.StartInfo.Arguments, Do_on));
            //Do_on.spc.add_xmd(new xmd("notepad.exe", "", Do_on));
            // }
            p.Dispose();
            /*    }
                catch { }
            }
            else
            {
                p.Start();
                SetParent(p.Handle, Do_on.spc.ParentForm.Handle);
            }*/

            var_arraylist_dispose(ref al);

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

            /*try
            {
                ((Process)sender).Close();
            }
            catch { }*/
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
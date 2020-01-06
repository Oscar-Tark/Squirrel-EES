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
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scorpion_IDE
{
    public partial class Special_TextView : UserControl
    {
        public Scorpion.Form1 fm1;
        public Special_TextView(Scorpion.Form1 Do_on)
        {
            fm1 = Do_on;
            InitializeComponent();
            ch_fnt();
            //acm.Show(rtb, false);
            load_elements();
            load_();
            load_shs_suggestions();
            add_object_viewer();
            //add_server_tool();
        }

        /*public void add_xmd(Scorpion.xmd xmd)
        {
            panel1.Controls.Add(xmd);
            return;
        }*/

        private void load_()
        {
            //Misc. Loads
            rtb.AutoCompleteCustomSource = fm1.acsc;

            rtb_final.WordWrap = Scorpion.Properties.Settings.Default.wwrap;
            ww.Checked = rtb_final.WordWrap;

            return;
        }

        private void load_elements()
        {
            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                f_type.Items.Add(font.Name);
            }

            /*foreach (string s in fm1.AL_FNC_SCRP)
            {
                acm.AddItem(new AutocompleteItem(s, 470));
            }*/
        }

        Thread th_ld_shs;
        delegate void del_ld_shs();
        private void del_ld_shs_strt()
        {
            this.Invoke(new del_ld_shs(load_shs_suggestions));
            return;
        }

        private void iterate_suggestions_shs()
        {
            /*foreach (string s in fm1.AL_SHS_APP_REF)
            {
                try
                {
                    acm.AddItem(new AutocompleteItem(fm1.AL_ACC_SUP[7] + fm1.AL_ACC[2].ToString() + fm1.AL_FNC_SCRP[80] + fm1.AL_ACC[3].ToString() + fm1.AL_ACC[1].ToString() + s + fm1.AL_ACC[4].ToString(), 259));
                    //tv_shs.Nodes.Find("Node4", true)[0].Nodes.Add(fm1.AL_ACC_SUP[7] + fm1.AL_ACC[2].ToString() + fm1.AL_FNC_SCRP[80] + fm1.AL_ACC[3].ToString() + fm1.AL_ACC[1].ToString() + s + fm1.AL_ACC[4].ToString(), fm1.AL_ACC_SUP[7] + fm1.AL_ACC[2].ToString() + fm1.AL_FNC_SCRP[80] + fm1.AL_ACC[3].ToString() + fm1.AL_ACC[1].ToString() + s + fm1.AL_ACC[4].ToString(), 259, 259);
                }
                catch {  }
            }*/
            return;
        }

        public void load_shs_suggestions()
        {
            th_ld_shs = new Thread(new ThreadStart(iterate_suggestions_shs));
            th_ld_shs.Start();

            th_ld_shs = null;

            return;
        }

        public Special_TextView()
        {
            InitializeComponent();
            ch_fnt();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            sfd.ShowDialog();
        }

        private void sfd_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(rtb.Text);
            sw.Flush();
            fs.Flush();
            sw.Close();
            fs.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            rtb.Text = sw.ReadToEnd();
            fs.Flush();
            sw.Close();
            fs.Close();
        }

        private void f_size_TextChanged(object sender, EventArgs e)
        {
            ch_fnt();
        }

        private void f_type_TextChanged(object sender, EventArgs e)
        {
            ch_ty_fnt();
        }

        private void ch_fnt()
        {
            try
            {
                double d = Convert.ToDouble(f_size.Text);
                rtb_final.Font = new Font(rtb.Font.FontFamily, (float)d, rtb.Font.Style);
            }
            catch { fm1.write_to_cui("Font Type does Not Exist."); }
            return;
        }

        private void ch_ty_fnt()
        {
            try
            {
                rtb_final.Font = new Font(f_type.Text, rtb.Font.Size, rtb.Font.Style);
            }
            catch { fm1.write_to_cui("Font Type does Not Exist."); }
            return;
        }

        private void toolStripDropDownButton2_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text != "Creation Functions" && e.ClickedItem.Text != "Edit Functions" && e.ClickedItem.Text != "Destroy Functions" && e.ClickedItem.Text != "General" && e.ClickedItem.Text != "CUI Functions" && e.ClickedItem.Text != "Loops" && e.ClickedItem.Text != "Conditionals" && e.ClickedItem.Text != "Internet Functions" && e.ClickedItem.Text != "Data Functions" && e.ClickedItem.Text != "GUI Based Data Functions" && e.ClickedItem.Text != "Events")
            {
                int ndx = rtb.Text.IndexOf("\n", rtb.SelectionStart);

                if (ndx != -1)
                {
                    rtb.Text = rtb.Text.Insert(ndx, "\n" + e.ClickedItem.Text);
                }
                else
                {
                    rtb.Text = rtb.Text + e.ClickedItem.Text + "\n";
                }

                int ndx2;
                try
                {
                    ndx2 = rtb.Text.IndexOf("(", ndx);
                }
                catch
                {
                    ndx2 = rtb.Text.IndexOf("(", 0);
                }
                rtb.SelectionStart = ndx2 + 1;
            }
        }

        public string buffer = ""; 
        /*SqlCeConnection conn = new SqlCeConnection(Scorpion.Properties.Settings.Default.SC_suggestConnectionString);
        SqlCeCommand cmd;
        SqlCeDataReader dr;
        DataTable dtp = new DataTable();*/

        /*private void rtb_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue > 33 && e.KeyValue < 126)
                {
                    buffer = buffer + Convert.ToChar(e.KeyValue).ToString();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    buffer = buffer.Remove(buffer.Length - 1);
                }
                else { buffer = ""; }
            }
            catch { }
            try
            {
                bkk_suggest.RunWorkerAsync();
            }
            catch {  }
            
            sender = null;
            e = null;
            return;
        }*/

        /*private void dgv_suggest_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtb.Text = rtb.Text.Remove(rtb.SelectionStart - buffer.Length);
                dtp.Clear();
                buffer = "";
                rtb.Select();
                rtb.DeselectAll();
            }

            return;
        }*/

        private void cms_item_Click(object sender, EventArgs e)
        {
            if (sender.Equals(copyToolStripMenuItem))
                rtb_final.Copy();
            else if (sender.Equals(pasteToolStripMenuItem))
                rtb.Paste();
            else if (sender.Equals(cutToolStripMenuItem))
                rtb_final.Cut();
            else if (sender.Equals(selectAllToolStripMenuItem))
                rtb_final.SelectAll();
            else if (sender.Equals(undoToolStripMenuItem))
                rtb.Undo();
            else if (sender.Equals(toolStripButton3))
                execute();

            sender = null;
            e = null;

            return;
        }

        private void rtb_KeyDown(object sender, KeyEventArgs e)
        {
            //status.Text = "Sel : {" + rtb.SelectionStart.ToString() + "}   Ln : {" + rtb.GetLineFromCharIndex(rtb.SelectionStart) + "}   LST : {" + e.KeyCode.ToString() + "(" + e.KeyValue.ToString() + ")}   BUFF : {'" + buffer.ToLower() + "'}";
            if (e.KeyCode == Keys.Enter)
                execute();
            sender = null;
            e = null;

            return;
        }

        private void execute()
        {
            rtb.AutoCompleteCustomSource.Add(rtb.Text);
            //string s_rd = rtb.Lines[rtb.GetLineFromCharIndex(rtb.SelectionStart)];
            fm1.readr.access_library(rtb.Text);
            rtb.Text = "";
            Scroll_To_End();

            return;
        }

        private void Scroll_To_End()
        {
            try
            {
                rtb_final.SelectionStart = rtb_final.TextLength - 1;
                rtb_final.ScrollToCaret();
            }
            catch { }
            return;
        }

        private void execute(string Command)
        {
            rtb.AutoCompleteCustomSource.Add(rtb.Text);
            fm1.readr.access_library(Command);
            return;
        }

        private void message_rtb(string message)
        {
            rtb_final.AppendText(rtb_final.Text + message + "\n");
            return;
        }

        private void add_object_viewer()
        {
            Scorpion.Obj_vw obj = new Scorpion.Obj_vw(fm1);
            obj.TopLevel = false;
            obj.WindowState = FormWindowState.Maximized;
            splitContainer1.Panel1.Controls.Add(obj);
            return;
        }

        private void objectViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(objectViewerToolStripMenuItem))
                add_object_viewer();
            else if (sender.Equals(analyzerToolStripMenuItem))
                execute("analyzer()");

            sender = null;
            e = null;
            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(exitToolStripMenuItem))
            {
                execute(fm1.AL_ACC_SUP[3] + fm1.AL_ACC[2].ToString() + fm1.AL_FNC_SCRP[3] + fm1.AL_ACC[3].ToString() + fm1.AL_ACC[4].ToString());
            }
            else { execute(fm1.AL_ACC_SUP[3] + fm1.AL_ACC[2].ToString() + fm1.AL_FNC_SCRP[2] + fm1.AL_ACC[3].ToString() + fm1.AL_ACC[4].ToString()); }

            sender = null;
            e = null;

            return;
        }

        int num = 0;
        public void colorize_output(string TEXT)
        {
            num = 0;
            num = rtb_final.Text.IndexOf(TEXT, num);
            rtb_final.SelectionStart = num;
            rtb_final.SelectionLength = TEXT.Length;
            rtb_final.SelectionColor = Color.Red;
            rtb_final.DeselectAll();
            num = num + TEXT.Length;

            return;
        }

        private void ww_Click(object sender, EventArgs e)
        {
            rtb_final.WordWrap = ww.Checked;
            Scorpion.Properties.Settings.Default.wwrap = ww.Checked;
            Scorpion.Properties.Settings.Default.Save();
        }

        /*delegate void del_suggest(); Thread th_suggest;
        private void bkk_suggest_DoWork(object sender, DoWorkEventArgs e)
        {
            th_suggest = new Thread(new ThreadStart(del_suggest_strt));
            th_suggest.Start();
        }

        private void del_suggest_strt()
        {
            this.Invoke(new del_suggest(suggest));
            return;
        }

        private void suggest()
        {
            bs.ResetBindings(false);
            sugg.SelectedIndex = sugg.FindString(buffer);

            return;
        }*/

        int caret = 0;
        private void sugg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                rtb.Focus();
            }
            else if (e.KeyCode == Keys.F1)
            {
                add_suggestion();
            }
            e = null;
            return;
        }

        private void rtb_final_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
            return;
        }

        private void sugg_DoubleClick(object sender, EventArgs e)
        {
            add_suggestion();
        }

        private void add_suggestion()
        {
            /*try
            {
                rtb.Text = rtb.Text.Remove(caret - buffer.Length, buffer.Length);
                rtb.Text = rtb.Text.Insert(caret - buffer.Length, "*" + sugg.SelectedValue.ToString());
                rtb.SelectionStart = caret + (sugg.SelectedValue.ToString().Length - 2);
            }
            catch { }*/
            return;
        }

        private void tms_Tick(object sender, EventArgs e)
        {

        }

        private void del_mem_Click_1(object sender, EventArgs e)
        {

        }

        private void Panic_Stop(object sender, EventArgs e)
        {
            fm1.panic_stop();
        }

        public void mount_(TreeNode Node)
        {
            var Serializer = new BinaryFormatter();

            //GET REF
            try
            {
                using (var stream = File.OpenRead(Environment.CurrentDirectory + Node.Text))
                {
                    fm1.write_to_cui("Mounting - [" + Environment.CurrentDirectory + Node.Text + "]");
                    Node.Nodes.Clear();
                    ArrayList al_fetch = (ArrayList)Serializer.Deserialize(stream);
                    foreach (object o in al_fetch)
                    {
                        Node.Nodes.Add(o.ToString());
                    }
                    //CLEAN
                    fm1.readr.lib_SCR.var_arraylist_dispose(ref al_fetch);
                }
            }
            catch { }
            Serializer = null;
            Node = null;
        }

        public void unmount_(TreeNode Node)
        {
            Node.Nodes.Clear();
            Node = null;
            return;
        }

        private void closeAllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //close_all_xmd();

            sender = null;
            e = null;

            return;
        }

        private void xmd_function_call(object sender, EventArgs e)
        {
            /*if (sender.Equals(clearSelectedXmdToolStripMenuItem))
            {
                rtb_final.Text = "";
            }
            else if (sender.Equals(minimizeToolStripMenuItem))
            {
                foreach (Form f in panel1.Controls)
                {
                    f.WindowState = FormWindowState.Minimized;
                    f.Refresh();
                }
            }*/

            sender = null;
            e = null;

            return;
        }
    }
}

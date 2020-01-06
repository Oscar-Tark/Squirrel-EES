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
            load_elements();
            load_();
        }

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
                f_type.Items.Add(font.Name);
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
            fm1.readr.access_library(rtb.Text);
            rtb.Text = "";
            Scroll_To_End();
            return;
        }

        private void Scroll_To_End()
        {
            rtb_final.SelectionStart = rtb_final.TextLength - 1;
            rtb_final.ScrollToCaret();
            return;
        }

        private void execute(string Command)
        {
            rtb.AutoCompleteCustomSource.Add(rtb.Text);
            fm1.readr.access_library(Command);
            Scroll_To_End();
            return;
        }

        private void message_rtb(string message)
        {
            rtb_final.AppendText(rtb_final.Text + message + "\n");
            return;
        }

        private void objectViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sender = null;
            e = null;
            return;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.Equals(exitToolStripMenuItem))
                execute("exit()");
            else
                execute("about()");
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

        int caret = 0;
        private void sugg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                rtb.Focus();
            e = null;
            return;
        }

        private void rtb_final_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
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
    }
}

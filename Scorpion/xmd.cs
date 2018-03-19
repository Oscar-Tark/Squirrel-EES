using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class xmd : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        ConsoleControl.ConsoleControl c = new ConsoleControl.ConsoleControl();

        Process p = new Process();

        string prc, arg;
        Form1 Do_on;
        public xmd(string Process,string args, Form1 fm1)
        {
            Do_on = fm1;
            try
            {
                prc = Process; arg = args;
                InitializeComponent();
                this.FormClosing += new FormClosingEventHandler(xmd_FormClosing);

                xmcd.StartProcess(Process, args);
                p = xmcd.ProcessInterface.Process;
                //SetParent(xmcd.ProcessInterface.Process.Handle, this.Handle);

                this.TopLevel = false;
                this.Show();
                this.BringToFront();
                set_text(xmcd.ProcessInterface.ProcessFileName);
            }
            catch { }

            update_bindings();

            return;
        }

        public void update_bindings()
        {
            lv_objs.DataSource = Do_on.AL_CURR_VAR_REF;
            return;
        }

        void xmd_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void xmd_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (sender.Equals(toolStripButton4))
            {
                try
                {
                    try
                    {
                        xmcd.ProcessInterface.Process.Kill();

                        xmcd.ProcessInterface.Process.Start();
                    }
                    catch {
                        xmcd.ProcessInterface.StartProcess(prc, arg);
                    }
                    
                    set_text(xmcd.ProcessInterface.ProcessFileName);
                }
                catch (Exception erty) { Do_on.write_to_cui("Error: " + erty.Message); erty = null; }
            }
            else if (sender.Equals(clear))
            {
                clear_screen();
            }
            else if (sender.Equals(stop) || sender.Equals(stopToolStripMenuItem))
            {
                stop_process();
            }
            else if (sender.Equals(toolStripButton1) || sender.Equals(returnToCommandLineToolStripMenuItem))
            {
                try
                {
                    //xmcd.ProcessInterface.Process.Kill();
                    //xmcd.ProcessInterface.Process.Start();
                    xmcd.scroll_to_end();
                    xmcd.ProcessInterface.StartProcess("cmd.exe", "");

                    set_text(xmcd.ProcessInterface.ProcessFileName);
                }
                catch (Exception erty) { Do_on.write_to_cui("Error: " + erty.Message); erty = null; }
            }
            else if (sender.Equals(cursorToEndToolStripMenuItem))
            {
                xmcd.scroll_to_end();
            }
            else if (sender.Equals(cursorToBeginingToolStripMenuItem))
            {
                xmcd.scroll_to_begining();
            }
            else if (sender.Equals(cursorToToolStripMenuItem))
            {
                xmcd.scroll_to_carret();
            }
            else if (sender.Equals(toolStripButton3))
            {
                lv_objs.DataSource = null;
                lv_objs.DataSource = Do_on.AL_CURR_VAR_REF;
            }
            else if (sender.Equals(addVariableToolStripMenuItem))
            {
                xmcd.Write_To_XMD(Do_on.readr.lib_SCR.var_get(lv_objs.SelectedItem.ToString()).ToString());
            }
            else if (sender.Equals(toolStripButton2))
            {
                try
                {
                    Find = xmcd.richTextBoxConsole.Find(toolStripTextBox1.Text, Find, xmcd.richTextBoxConsole.Text.Length - 1, RichTextBoxFinds.MatchCase);
                    xmcd.richTextBoxConsole.Select(Find, toolStripTextBox1.Text.Length);
                    xmcd.richTextBoxConsole.Select();
                    Find++;
                }
                catch { MessageBox.Show("No String Defined for Search OR EOF"); Find = 0; }
            }

            sender = null;
            e = null;

            return;
        }
        int Find = 0;

        private void clear_screen()
        {
            xmcd.ClearOutput();
            return;
        }

        public void stop_process()
        {
            try
            {
                xmcd.ProcessInterface.Process.Kill();
                xmcd.StopProcess();
                set_text("Idle/Dead");
            }
            catch { }
            return;
        }

        private void set_text(string Text)
        {
            this.Text = "XMD - [" + Text + "]";
            Prcnme.Text = Text;

            Text = null;
            return;
        }

        private void xmcd_Load(object sender, EventArgs e)
        {
            sender = null;
            e = null;
            return;
        }

        public void xmd_functions(string function)
        {
            if (function.ToLower() == "stop")
            {
                stop_process();
            }
            else if (function.ToLower() == "clear")
            {
                clear_screen();
            }

            function = null;
            return;
        }

        private void in_out_Click(object sender, EventArgs e)
        {
            if (in_out.Checked)
            {
                this.TopLevel = false;
                this.Parent = Do_on.spc.panel1;
            }
            else { this.Parent = null; this.TopLevel = true; }

            e = null;
            sender = null;
            return;
        }
    }
}

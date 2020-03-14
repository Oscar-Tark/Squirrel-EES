/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014-2016>  <Oscar Arjun Singh Tark>

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
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            bkk_load_apps.RunWorkerAsync();
            IntPtr Hicon = Properties.Resources.application_view_xp_terminal.GetHicon();
            this.Icon = Icon.FromHandle(Hicon);
            Startup_Load_Objects.RunWorkerAsync();
            initialize_scorpion();
            return;
        }

        public void initialize_scorpion()
        {
            types = new Types(this);
            vds = new Dumper.Virtual_Dumper_System(this);
            crypto = new Crypto.Cryptographer(this);
            iff = new Internetwork_File_Format.Internetwork_Video_File_Format(this);
            RS = new Game_Engine.Scorpion_RS(this);
            hook = new Hooking.Hooker(this);
            mmsec = new Memory_Security.Secure_Memory(this);
            fleoper = new File_operations.Fileopr(this);
            san = new Memory_Security.Sanitizer(this);

            types.load_system_vars();
            readr = new reader(this);
            start_CUI();
            return;
        }

        public void Load_init_db()
        {
            try
            {
                vds.Un_Dump();
            }
            catch { write_to_cui(AL_MESSAGE[7].ToString()); }
            return;
        }

        public void start_after_services()
        {
            //Main Scorpion Foundation Server Connection
            amcl = new Main.Amatrix_Sever_Client_Lite(this);
            serv = new Amatrix_Server_1._1.Form1(this);

            //FTP server connection
            ftp_serv = new FTP.ftp_server(this);
            return;
        }

        private void Startup_Load_Objects_DoWork(object sender, DoWorkEventArgs e)
        {
            sender = null;
            e = null;
            return;
        }

        public void panic_stop()
        {
            this.FormClosing -= Form1_FormClosing;

            Application.ApplicationExit -= Application_ApplicationExit;
            Application.Exit();
            return;
        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                scnti.Dispose();
                amcl = null;
                Application.Exit();
            }
            catch { Application.ExitThread(); }
            sender = null;
            e = null;
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmdargs = Environment.GetCommandLineArgs();

            sender = null;
            e = null;
            return;
        }

        private void startupservices_Tick(object sender, EventArgs e)
        {
            startup_services();
            startupservices.Stop();
            startupservices.Dispose();
            return;
        }

        private void startup_services()
        {
            try
            {
                string[] s = Environment.GetCommandLineArgs();
                if (s[1] == "pipe_start")
                { readr.access_library("net.strtpip(name(\\.\\pipe\\OneP))"); write_to_cui("Started with pipe"); }
                else if (s[1] == "tcpip_start")
                { write_to_cui("Started with tcp/ip"); }
                s = null;
            }
            catch
            {
            }
            return;
        }

        public void Application_ApplicationExit(object sender, EventArgs e)
        {
            //DUMP
            types.unload_system_vars();
            vds.Dump_main_db();
            sender = null;
            e = null;
            return;
        }

        public void restart_scorp_app()
        {
            Index = 0;
            readr.ndx = 0;
            FileStream fs = new FileStream(OFD.FileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Prog_s = sr.ReadToEnd();
            fs.Flush();
            sr.Close();
            fs.Close();
            FileInfo fnf = new FileInfo(OFD.FileName);
            re_read(Index);
            return;
        }

        public string Current_dir;
        public void OFD_FileOk(object sender, CancelEventArgs e)
        {
            Open(OFD.FileName);
            //start_vee();
            sender = null;
            e = null;
            return;
        }

        public void Open(string Location)
        {
            AL_CURR_VAR_REF.Add("svar"); /*AL_CURR_VAR_TYPE.Add("system");*/ AL_CURR_VAR.Add("0");
            Orig_path = Location;
            FileStream fs = new FileStream(Location, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Prog_s = sr.ReadToEnd();
            fs.Flush();
            sr.Close();
            fs.Close();
            FileInfo fnf = new FileInfo(Location);
            Current_dir = fnf.DirectoryName;
            re_read(Index);

            return;
        }
         
        public void read_again(bool Cont)
        {
            Continue = Cont;
            return;
        }

        public void re_read(int Index_)
        {
            try
            {
                Index = Index_;
                bkk_read = new BackgroundWorker();
                bkk_read.DoWork += new DoWorkEventHandler(bkk_read_DoWork);
                bkk_read.RunWorkerAsync();
            }
            catch { Application.ExitThread(); }
            return;
        }

        private void bkk_read_DoWork(object sender, DoWorkEventArgs e)
        {
            //readr.read(Prog_s, Index, this);
            sender = null;
            e = null;
            return;
        }

        public bool Continue = true;
        private void bkk_read_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bkk_read.Dispose();
            sender = null;
            e = null;
            return;
        }

        //CLEANING
        private void Cleaner_Tick(object sender, EventArgs e)
        {
            try
            {
                bkk_cleaner.RunWorkerAsync();
            }
            catch { }

            if (Application.OpenForms.Count == 1)
            {
                Application.Exit();
            }
            sender = null;
            e = null;
            return;
        }

        private Thread th_clean;
        private delegate void del_clean();
        private void bkk_cleaner_DoWork(object sender, DoWorkEventArgs e)
        {
            th_clean_strt();
            sender = null;
            e = null;
            return;
        }

        private void th_clean_strt()
        {
            try
            {
                th_clean = new Thread(new ThreadStart(del_clean_strt));
                th_clean.IsBackground = true;
                th_clean.Start();
            }
            catch { }
            return;
        }

        private void del_clean_strt()
        {
            try
            {
                this.Invoke(new del_clean(clean));
            }
            catch { }
            return;
        }

        private void clean()
        {
            AL_GUI_TEMPLATES.TrimToSize();
            AL_GUI_TEMPLATES_REF.TrimToSize();
            AL_UNBEARABLE_CHARS.TrimToSize();

            //General Memory
            AL_CURR_VAR.TrimToSize();
            AL_CURR_VAR_REF.TrimToSize();
            AL_CURR_VAR_TAG.TrimToSize();
            AL_CURR_VAR_EVT.TrimToSize();

            //Functions
            AL_Ref_EVT.TrimToSize();
            AL_EVT.TrimToSize();

            AL_PIPES.TrimToSize();
            AL_PIPES_REF.TrimToSize();

            //Recursive Calls
            AL_REC.TrimToSize();
            AL_REC_REF.TrimToSize();
            AL_REC_TME.TrimToSize();

            //System
            AL_HIB_FILES.TrimToSize();

            AL_AMCS.TrimToSize();
            AL_AMCS_REF.TrimToSize();

            AL_DISP_DEVICES.TrimToSize();
            AL_OBJ_3D.TrimToSize();
            AL_OBJ_3D_REF.TrimToSize();

            AL_SHS.TrimToSize();
            AL_SHS_APP.TrimToSize();
            AL_SHS_APP_REF.TrimToSize();
            AL_SHS_REF.TrimToSize();

            AL_PRC.TrimToSize();
            AL_PRC_REF.TrimToSize();

            AL_ACC.TrimToSize();
            AL_FNC_SCRP.TrimToSize();

            GC.Collect();

            return;
        }

        //ACCESSORS
        public void Access_Work(string Line)
        {
            readr.access_library(Line);
            return;
        }

        //CUI
        bool CUI_MODE = false; public Form f;
        public void start_CUI()
        {
            acsc = new AutoCompleteStringCollection();
            this.WindowState = FormWindowState.Normal;

            f = new Form();
            f.AutoScaleMode = AutoScaleMode.Dpi;
            f.Text = "Scorpion - Command Line";
            f.Disposed += new EventHandler(f_Disposed);
            f.SizeChanged += new EventHandler(f_SizeChanged);
            f.WindowState = Properties.Settings.Default.fscreen;

            spc = new Scorpion_IDE.Special_TextView(this);
            spc.Dock = DockStyle.Fill;

            IntPtr Hicon = Properties.Resources.application_osx_terminal.GetHicon();
            f.Icon = Icon.FromHandle(Hicon);
            f.Size = new Size(950, 500);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Controls.Add(spc);
            f.Show();
            CUI_MODE = true;
            
            write_to_cui("Scorpion CEE/VEE/NEE*\n\nby Oscar Arjun Singh Tark >>> Version 0.2 Development Release \nProtected under the GNU AGPL as Open Source Free software.\n\n*Command Execution Environment\n/Visual Execution Environment\n/Network Execution Environment");

            return;
        }

        void f_SizeChanged(object sender, EventArgs e)
        {
            Scorpion.Properties.Settings.Default.fscreen = ((Form)sender).WindowState;
            Scorpion.Properties.Settings.Default.Save();
            sender = null;
            e = null;
            return;
        }

        public void write_to_cui(string Text)
        {
            try
            {
                if (!Text.EndsWith(".", StringComparison.CurrentCultureIgnoreCase))
                    Text = Text + ".";
                spc.rtb_final.Text = spc.rtb_final.Text + "[PRINT:]\n" + Text + "[END]\n";
                this.richTextBox1.Text = richTextBox1.Text + "[PRINT:]\n" + Text + "[END]\n";
                Console.WriteLine(Text + "\n");
                spc.colorize_output(Text);
            }
            catch { }
            Text = null;

            return;
        }

        public void add_to_cui_suggestions(string Text)
        {
            /*try
            {
                spc.acm.AddItem(new AutocompleteMenuNS.AutocompleteItem(Text, 0));
            }
            catch { }
            Text = null;*/
            return;
        }

        void f_Disposed(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch { Application.ExitThread(); }
            sender = null;
            e = null;
            return;
        }

        //REALTIME
        private void real_time_time_Tick(object sender, EventArgs e)
        {
            try
            {
                bkk_real_time.RunWorkerAsync();
            }
            catch { }
            sender = null;
            e = null;
            return;
        }

        FileStream fs_sample; StreamReader sr_sample; Thread th_sample;
        delegate void del_sample(object Sample_Line); string sampled = ""; string temp = "";
        private void bkk_real_time_DoWork(object sender, DoWorkEventArgs e)
        {
            /*try
            {
                fs_sample = new FileStream(Orig_path, FileMode.Open, FileAccess.Read);
                sr_sample = new StreamReader(fs_sample);

                sampled = sr_sample.ReadToEnd();

                fs_sample.Flush();
                sr_sample.Close();
                fs_sample.Close();

                int ndx = 0; int ndx2 = 0; 
                if(sampled != Prog_s)
                {
                    foreach (char c in sampled)
                    {
                        ndx2 = sampled.IndexOf("~;", ndx);
                        temp = sampled.Remove(ndx2);
                        temp = temp.Remove(0, ndx);

                        if (AL_Sample_Lines.Contains(temp) == false && temp != null)
                        {
                            readr.access_library(temp);
                            AL_Sample_Lines.Add(temp);
                            //th_execute_sample(temp);
                        }

                        ndx = ndx2 + 2;
                    }
                }
            }
            catch { }*/

            sender = null;
            e = null;

            return;
        }

        private void th_execute_sample(string Sample_Line)
        {
            th_sample = new Thread(new ParameterizedThreadStart(del_execute_sample));
            th_sample.IsBackground = true;
            th_sample.Start(Sample_Line);

            return;
        }

        private void del_execute_sample(object Sample_Line)
        {
            del_sample dd = new del_sample(execute_sample);
            dd.Invoke(Sample_Line);

            return;
        }

        private void execute_sample(object Sample_Line)
        {
            /*MessageBox.Show(Sample_Line.ToString());
            Access_Work(Sample_Line.ToString());
            AL_Sample_Lines.Add(Sample_Line.ToString());

            Sample_Line = null;*/

            return;
        }

        private void scnti_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Close all Proccesses Registered?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (System.Diagnostics.Process p in AL_PRC)
                    {
                        try
                        {
                            p.Kill();
                        }
                        catch { }
                    }
                }
            }
            catch { }

            this.FormClosing -= Form1_FormClosing;
            Application.ApplicationExit -= Application_ApplicationExit;
            try
            {
                Application.Exit();
            }
            catch { Application.ExitThread(); }

            sender = null;
            e = null;
            return;
        }

        private void Startup_Load_Objects_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            sender = null;
            e = null;
            return;
        }


        private void bkk_load_apps_DoWork(object sender, DoWorkEventArgs e)
        {
            //load_SHS();
            sender = null;
            e = null;
            return;
        }

        private void Root_menu_log_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileOk += Sfd_FileOk;
            sfd.DefaultExt = "*.txt";
            sfd.ShowDialog();
        }

        private void Sfd_FileOk(object sender, CancelEventArgs e)
        {
            this.readr.access_library("createfile(*\"" + ((SaveFileDialog)sender).FileName + "\")");
            return;
        }
    }
}

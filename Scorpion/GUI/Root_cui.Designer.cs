namespace Scorpion
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /*protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }*/

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.bkk_cleaner = new System.ComponentModel.BackgroundWorker();
            this.OFD = new System.Windows.Forms.OpenFileDialog();
            this.Cleaner = new System.Windows.Forms.Timer(this.components);
            this.scnti = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scorpionResearchSystemV01bToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bkk_real_time = new System.ComponentModel.BackgroundWorker();
            this.bkk_read = new System.ComponentModel.BackgroundWorker();
            this.real_time_time = new System.Windows.Forms.Timer(this.components);
            this.Startup_Load_Objects = new System.ComponentModel.BackgroundWorker();
            this.bkk_load_apps = new System.ComponentModel.BackgroundWorker();
            this.startupservices = new System.Windows.Forms.Timer(this.components);
            this.image_list = new System.Windows.Forms.ImageList(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.root_menu_log = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cms.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            //this.SuspendLayout();
            // 
            // OFD
            // 
            this.OFD.FileName = "Scorpion File";
            resources.ApplyResources(this.OFD, "OFD");
            this.OFD.FileOk += new System.ComponentModel.CancelEventHandler(this.OFD_FileOk);
            // 
            // Cleaner
            // 
            this.Cleaner.Enabled = true;
            this.Cleaner.Interval = 2000;
            this.Cleaner.Tick += new System.EventHandler(this.Cleaner_Tick);
            // 
            // cms
            // 
            this.cms.BackColor = System.Drawing.Color.White;
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scorpionResearchSystemV01bToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.cms.Name = "cms";
            resources.ApplyResources(this.cms, "cms");
            // 
            // scorpionResearchSystemV01bToolStripMenuItem
            // 
            this.scorpionResearchSystemV01bToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.scorpionResearchSystemV01bToolStripMenuItem.Image = global::Scorpion.Properties.Resources.application_osx_terminal;
            this.scorpionResearchSystemV01bToolStripMenuItem.Name = "scorpionResearchSystemV01bToolStripMenuItem";
            resources.ApplyResources(this.scorpionResearchSystemV01bToolStripMenuItem, "scorpionResearchSystemV01bToolStripMenuItem");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.DimGray;
            this.exitToolStripMenuItem.Image = global::Scorpion.Properties.Resources.application_delete;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.scnti_DoubleClick);
            // 
            // bkk_real_time
            // 
            this.bkk_real_time.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_real_time_DoWork);
            // 
            // bkk_read
            // 
            this.bkk_read.WorkerReportsProgress = true;
            this.bkk_read.WorkerSupportsCancellation = true;
            this.bkk_read.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_read_DoWork);
            this.bkk_read.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkk_read_RunWorkerCompleted);
            // 
            // real_time_time
            // 
            this.real_time_time.Interval = 1500;
            this.real_time_time.Tick += new System.EventHandler(this.real_time_time_Tick);
            // 
            // Startup_Load_Objects
            // startupservices
            // 
            this.startupservices.Enabled = true;
            this.startupservices.Interval = 2000;
            this.startupservices.Tick += new System.EventHandler(this.startupservices_Tick);
            // 
            // image_list
            // 
            this.image_list.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("image_list.ImageStream")));
            this.image_list.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ShortcutsEnabled = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.root_menu_log,
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // root_menu_log
            // 
            this.root_menu_log.Image = global::Scorpion.Properties.Resources.buildings;
            resources.ApplyResources(this.root_menu_log, "root_menu_log");
            this.root_menu_log.Name = "root_menu_log";
            this.root_menu_log.Click += new System.EventHandler(this.Root_menu_log_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            /*this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);*/
            this.cms.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            /*this.ResumeLayout(false);
            this.PerformLayout();*/

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bkk_cleaner;
        public System.Windows.Forms.OpenFileDialog OFD;
        private System.Windows.Forms.Timer Cleaner;
        private System.Windows.Forms.NotifyIcon scnti;
        private System.ComponentModel.BackgroundWorker bkk_real_time;
        public System.ComponentModel.BackgroundWorker bkk_read;
        public System.Windows.Forms.Timer real_time_time;
        public System.ComponentModel.BackgroundWorker Startup_Load_Objects;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem scorpionResearchSystemV01bToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bkk_load_apps;
        private System.Windows.Forms.Timer startupservices;
        public System.Windows.Forms.ImageList image_list;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton root_menu_log;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}


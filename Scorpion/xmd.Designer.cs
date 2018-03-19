using System.Windows.Forms;

namespace Scorpion
{
    partial class xmd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Do_on.write_to_cui("Closing Process - [" + xmcd.ProcessInterface.ProcessFileName + "]");
            try
            {
                xmcd.ProcessInterface.Process.Kill();
            }
            catch { Do_on.write_to_cui("Warning - [Failed Kill!]"); }
            xmcd.ProcessInterface.Cancel_all();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xmd));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.clear = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripSplitButton();
            this.recursivelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.in_out = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.xmcd = new ConsoleControl.ConsoleControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Prcnme = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.returnToCommandLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cursorToEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cursorToBeginingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cursorToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lv_objs = new System.Windows.Forms.ListBox();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.addVariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clear,
            this.toolStripButton4,
            this.stop,
            this.toolStripSeparator1,
            this.in_out,
            this.toolStripSeparator2,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator5,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 248);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(483, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // clear
            // 
            this.clear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clear.Image = global::Scorpion.Properties.Resources.bin_closed;
            this.clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(23, 22);
            this.clear.Text = "Clear";
            this.clear.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recursivelyToolStripMenuItem});
            this.toolStripButton4.Image = global::Scorpion.Properties.Resources.control_repeat;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(32, 22);
            this.toolStripButton4.Text = "Restart";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // recursivelyToolStripMenuItem
            // 
            this.recursivelyToolStripMenuItem.CheckOnClick = true;
            this.recursivelyToolStripMenuItem.Name = "recursivelyToolStripMenuItem";
            this.recursivelyToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.recursivelyToolStripMenuItem.Text = "Recursively";
            // 
            // stop
            // 
            this.stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stop.Image = global::Scorpion.Properties.Resources.control_stop;
            this.stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(23, 22);
            this.stop.Text = "Stop";
            this.stop.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // in_out
            // 
            this.in_out.Checked = true;
            this.in_out.CheckOnClick = true;
            this.in_out.CheckState = System.Windows.Forms.CheckState.Checked;
            this.in_out.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.in_out.Image = global::Scorpion.Properties.Resources.application_view_tile;
            this.in_out.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.in_out.Name = "in_out";
            this.in_out.Size = new System.Drawing.Size(23, 22);
            this.in_out.Text = "Eject";
            this.in_out.Click += new System.EventHandler(this.in_out_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Scorpion.Properties.Resources.application_view_xp_terminal;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Return to Command Line";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1});
            this.toolStripButton2.Image = global::Scorpion.Properties.Resources.magnifier;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(62, 22);
            this.toolStripButton2.Text = "Find";
            this.toolStripButton2.ButtonClick += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            // 
            // xmcd
            // 
            this.xmcd.AllowDrop = true;
            this.xmcd.AutoScroll = true;
            this.xmcd.BackColor = System.Drawing.Color.Gray;
            this.xmcd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmcd.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmcd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xmcd.IsInputEnabled = true;
            this.xmcd.Location = new System.Drawing.Point(0, 0);
            this.xmcd.Name = "xmcd";
            this.xmcd.SendKeyboardCommandsToProcess = true;
            this.xmcd.ShowDiagnostics = false;
            this.xmcd.Size = new System.Drawing.Size(324, 224);
            this.xmcd.TabIndex = 1;
            this.xmcd.Load += new System.EventHandler(this.xmcd_Load);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Prcnme});
            this.statusStrip1.Location = new System.Drawing.Point(0, 273);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Prcnme
            // 
            this.Prcnme.BackColor = System.Drawing.Color.Gainsboro;
            this.Prcnme.Name = "Prcnme";
            this.Prcnme.Size = new System.Drawing.Size(42, 17);
            this.Prcnme.Text = "Ready.";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.xMDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(483, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Image = global::Scorpion.Properties.Resources.doc_stand;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // xMDToolStripMenuItem
            // 
            this.xMDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripSeparator4,
            this.returnToCommandLineToolStripMenuItem,
            this.toolStripSeparator3,
            this.cursorToEndToolStripMenuItem,
            this.cursorToBeginingToolStripMenuItem,
            this.cursorToToolStripMenuItem,
            this.toolStripSeparator6,
            this.addVariableToolStripMenuItem});
            this.xMDToolStripMenuItem.Image = global::Scorpion.Properties.Resources.application_osx_terminal;
            this.xMDToolStripMenuItem.Name = "xMDToolStripMenuItem";
            this.xMDToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.xMDToolStripMenuItem.Text = "XMD";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(278, 6);
            // 
            // returnToCommandLineToolStripMenuItem
            // 
            this.returnToCommandLineToolStripMenuItem.Name = "returnToCommandLineToolStripMenuItem";
            this.returnToCommandLineToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.R)));
            this.returnToCommandLineToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.returnToCommandLineToolStripMenuItem.Text = "Return to Command Line";
            this.returnToCommandLineToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(278, 6);
            // 
            // cursorToEndToolStripMenuItem
            // 
            this.cursorToEndToolStripMenuItem.Name = "cursorToEndToolStripMenuItem";
            this.cursorToEndToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.E)));
            this.cursorToEndToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.cursorToEndToolStripMenuItem.Text = "Cursor to End";
            this.cursorToEndToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // cursorToBeginingToolStripMenuItem
            // 
            this.cursorToBeginingToolStripMenuItem.Name = "cursorToBeginingToolStripMenuItem";
            this.cursorToBeginingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.B)));
            this.cursorToBeginingToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.cursorToBeginingToolStripMenuItem.Text = "Cursor to Begining";
            this.cursorToBeginingToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // cursorToToolStripMenuItem
            // 
            this.cursorToToolStripMenuItem.Name = "cursorToToolStripMenuItem";
            this.cursorToToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.G)));
            this.cursorToToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.cursorToToolStripMenuItem.Text = "Go To Carret";
            this.cursorToToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xmcd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lv_objs);
            this.splitContainer1.Size = new System.Drawing.Size(483, 224);
            this.splitContainer1.SplitterDistance = 324;
            this.splitContainer1.TabIndex = 4;
            // 
            // lv_objs
            // 
            this.lv_objs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lv_objs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv_objs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_objs.ForeColor = System.Drawing.Color.Black;
            this.lv_objs.FormattingEnabled = true;
            this.lv_objs.Location = new System.Drawing.Point(0, 0);
            this.lv_objs.Name = "lv_objs";
            this.lv_objs.Size = new System.Drawing.Size(155, 224);
            this.lv_objs.TabIndex = 0;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::Scorpion.Properties.Resources.arrow_refresh;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "Refresh Object Bindings";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(278, 6);
            // 
            // addVariableToolStripMenuItem
            // 
            this.addVariableToolStripMenuItem.Name = "addVariableToolStripMenuItem";
            this.addVariableToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.addVariableToolStripMenuItem.Size = new System.Drawing.Size(281, 22);
            this.addVariableToolStripMenuItem.Text = "Add Variable";
            this.addVariableToolStripMenuItem.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // xmd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(483, 295);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "xmd";
            this.Text = "xmd";
            this.Load += new System.EventHandler(this.xmd_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton clear;
        private ConsoleControl.ConsoleControl xmcd;
        private System.Windows.Forms.ToolStripButton stop;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Prcnme;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton in_out;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem xMDToolStripMenuItem;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem returnToCommandLineToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cursorToEndToolStripMenuItem;
        private ToolStripMenuItem cursorToBeginingToolStripMenuItem;
        private ToolStripMenuItem cursorToToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSplitButton toolStripButton4;
        private ToolStripMenuItem recursivelyToolStripMenuItem;
        private ToolStripSplitButton toolStripButton2;
        private ToolStripTextBox toolStripTextBox1;
        private SplitContainer splitContainer1;
        private ListBox lv_objs;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem addVariableToolStripMenuItem;

    }
}
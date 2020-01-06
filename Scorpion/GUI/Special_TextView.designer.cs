namespace Scorpion_IDE
{
    partial class Special_TextView
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Special_TextView));
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Main_Control_Strip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.f_type = new System.Windows.Forms.ToolStripComboBox();
            this.f_size = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.objectViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.serverManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ww = new System.Windows.Forms.ToolStripButton();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.rtb_final = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rtb = new System.Windows.Forms.TextBox();
            this.bkk_suggest = new System.ComponentModel.BackgroundWorker();
            this.cms.SuspendLayout();
            this.Main_Control_Strip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cms
            // 
            this.cms.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.toolStripSeparator30,
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator29,
            this.selectAllToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cms.Size = new System.Drawing.Size(188, 126);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = global::Scorpion.Properties.Resources.arrow_undo;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.undoToolStripMenuItem.Text = "Undo in editable field";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.cms_item_Click);
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(184, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::Scorpion.Properties.Resources.clipboard_text;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.cms_item_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::Scorpion.Properties.Resources.cut;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cms_item_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::Scorpion.Properties.Resources.paste_plain;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.pasteToolStripMenuItem.Text = "Paste to editable field";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.cms_item_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(184, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Image = global::Scorpion.Properties.Resources.selection_select;
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.cms_item_Click);
            // 
            // Main_Control_Strip
            // 
            this.Main_Control_Strip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Main_Control_Strip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Main_Control_Strip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Main_Control_Strip.GripMargin = new System.Windows.Forms.Padding(0);
            this.Main_Control_Strip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Main_Control_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripSeparator8,
            this.toolStripButton1,
            this.toolStripButton2,
            this.ww,
            this.toolStripSeparator3,
            this.toolStripButton3,
            this.f_type,
            this.f_size});
            this.Main_Control_Strip.Location = new System.Drawing.Point(0, 425);
            this.Main_Control_Strip.Name = "Main_Control_Strip";
            this.Main_Control_Strip.Padding = new System.Windows.Forms.Padding(0);
            this.Main_Control_Strip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Main_Control_Strip.ShowItemToolTips = false;
            this.Main_Control_Strip.Size = new System.Drawing.Size(920, 25);
            this.Main_Control_Strip.TabIndex = 1;
            this.Main_Control_Strip.TabStop = true;
            this.Main_Control_Strip.Text = "toolStrip1";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton4.Image = global::Scorpion.Properties.Resources.application_delete;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(88, 22);
            this.toolStripButton4.Text = "Panic Close";
            this.toolStripButton4.ToolTipText = "Panic Close";
            this.toolStripButton4.Click += new System.EventHandler(this.Panic_Stop);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem});
            this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton1.Image = global::Scorpion.Properties.Resources.application_osx_terminal;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton1.Text = "Menu";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::Scorpion.Properties.Resources.exclamation;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.aboutToolStripMenuItem.Text = "About Scorpion";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(197, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::Scorpion.Properties.Resources.delete;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exitToolStripMenuItem.Text = "Exit Commander";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.Image = global::Scorpion.Properties.Resources.application_go;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton3.Text = "Run Line";
            this.toolStripButton3.ToolTipText = "Run the command specified in the edit field";
            this.toolStripButton3.Click += new System.EventHandler(this.cms_item_Click);
            // 
            // f_type
            // 
            this.f_type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.f_type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.f_type.BackColor = System.Drawing.Color.DimGray;
            this.f_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.f_type.ForeColor = System.Drawing.Color.White;
            this.f_type.Items.AddRange(new object[] {
            "Arial",
            "Courier New",
            "Microsoft Sans Serif",
            "Times New Roman",
            "Verdana"});
            this.f_type.Name = "f_type";
            this.f_type.Size = new System.Drawing.Size(121, 25);
            this.f_type.Text = "Courier New";
            this.f_type.TextChanged += new System.EventHandler(this.f_type_TextChanged);
            // 
            // f_size
            // 
            this.f_size.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.f_size.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.f_size.BackColor = System.Drawing.Color.DimGray;
            this.f_size.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.f_size.ForeColor = System.Drawing.Color.White;
            this.f_size.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30",
            "32",
            "36",
            "40",
            "42",
            "50",
            "70",
            "72",
            "86",
            "100"});
            this.f_size.Name = "f_size";
            this.f_size.Size = new System.Drawing.Size(75, 25);
            this.f_size.Text = "10";
            this.f_size.TextChanged += new System.EventHandler(this.f_size_TextChanged);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectViewerToolStripMenuItem,
            this.analyzerToolStripMenuItem,
            this.toolStripSeparator2,
            this.serverManagerToolStripMenuItem});
            this.toolStripButton2.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton2.Image = global::Scorpion.Properties.Resources.hammer_screwdriver;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton2.Text = "Tools";
            // 
            // objectViewerToolStripMenuItem
            // 
            this.objectViewerToolStripMenuItem.Image = global::Scorpion.Properties.Resources.magnifier;
            this.objectViewerToolStripMenuItem.Name = "objectViewerToolStripMenuItem";
            this.objectViewerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.objectViewerToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.objectViewerToolStripMenuItem.Text = "Object Viewer";
            this.objectViewerToolStripMenuItem.Click += new System.EventHandler(this.objectViewerToolStripMenuItem_Click);
            // 
            // analyzerToolStripMenuItem
            // 
            this.analyzerToolStripMenuItem.Name = "analyzerToolStripMenuItem";
            this.analyzerToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.analyzerToolStripMenuItem.Text = "DB Analyzer";
            this.analyzerToolStripMenuItem.Click += new System.EventHandler(this.objectViewerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(187, 6);
            // 
            // serverManagerToolStripMenuItem
            // 
            this.serverManagerToolStripMenuItem.Image = global::Scorpion.Properties.Resources.server;
            this.serverManagerToolStripMenuItem.Name = "serverManagerToolStripMenuItem";
            this.serverManagerToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.serverManagerToolStripMenuItem.Text = "Scorpion Server";
            this.serverManagerToolStripMenuItem.Click += new System.EventHandler(this.objectViewerToolStripMenuItem_Click);
            // 
            // ww
            // 
            this.ww.CheckOnClick = true;
            this.ww.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ww.Image = global::Scorpion.Properties.Resources.text_padding_right;
            this.ww.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ww.Name = "ww";
            this.ww.Size = new System.Drawing.Size(23, 22);
            this.ww.Text = "Word Wrap";
            this.ww.Click += new System.EventHandler(this.ww_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "Scorpion Files|*.scorp";
            this.ofd.FileOk += new System.ComponentModel.CancelEventHandler(this.ofd_FileOk);
            // 
            // sfd
            // 
            this.sfd.Filter = "Scorpion Files|*.scorp;";
            this.sfd.FileOk += new System.ComponentModel.CancelEventHandler(this.sfd_FileOk);
            // 
            // rtb_final
            // 
            this.rtb_final.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtb_final.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_final.ContextMenuStrip = this.cms;
            this.rtb_final.Cursor = System.Windows.Forms.Cursors.Cross;
            this.rtb_final.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_final.Font = new System.Drawing.Font("Courier New", 10F);
            this.rtb_final.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.rtb_final.Location = new System.Drawing.Point(0, 0);
            this.rtb_final.Margin = new System.Windows.Forms.Padding(0);
            this.rtb_final.Name = "rtb_final";
            this.rtb_final.ReadOnly = true;
            this.rtb_final.Size = new System.Drawing.Size(460, 399);
            this.rtb_final.TabIndex = 4;
            this.rtb_final.Text = "";
            this.rtb_final.WordWrap = false;
            this.rtb_final.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtb_final_LinkClicked);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "brick.png");
            this.imageList1.Images.SetKeyName(1, "application_form.png");
            this.imageList1.Images.SetKeyName(2, "accept.png");
            this.imageList1.Images.SetKeyName(3, "add.png");
            this.imageList1.Images.SetKeyName(4, "alarm.png");
            this.imageList1.Images.SetKeyName(5, "anchor.png");
            this.imageList1.Images.SetKeyName(6, "application.png");
            this.imageList1.Images.SetKeyName(7, "application_add.png");
            this.imageList1.Images.SetKeyName(8, "application_cascade.png");
            this.imageList1.Images.SetKeyName(9, "application_delete.png");
            this.imageList1.Images.SetKeyName(10, "application_double.png");
            this.imageList1.Images.SetKeyName(11, "application_edit.png");
            this.imageList1.Images.SetKeyName(12, "application_error.png");
            this.imageList1.Images.SetKeyName(13, "application_form.png");
            this.imageList1.Images.SetKeyName(14, "application_get.png");
            this.imageList1.Images.SetKeyName(15, "application_go.png");
            this.imageList1.Images.SetKeyName(16, "application_home.png");
            this.imageList1.Images.SetKeyName(17, "application_key.png");
            this.imageList1.Images.SetKeyName(18, "application_lightning.png");
            this.imageList1.Images.SetKeyName(19, "application_link.png");
            this.imageList1.Images.SetKeyName(20, "application_osx.png");
            this.imageList1.Images.SetKeyName(21, "application_osx_terminal.png");
            this.imageList1.Images.SetKeyName(22, "application_put.png");
            this.imageList1.Images.SetKeyName(23, "application_side_boxes.png");
            this.imageList1.Images.SetKeyName(24, "application_side_contract.png");
            this.imageList1.Images.SetKeyName(25, "application_side_expand.png");
            this.imageList1.Images.SetKeyName(26, "application_side_list.png");
            this.imageList1.Images.SetKeyName(27, "application_side_tree.png");
            this.imageList1.Images.SetKeyName(28, "application_split.png");
            this.imageList1.Images.SetKeyName(29, "application_tile_horizontal.png");
            this.imageList1.Images.SetKeyName(30, "application_tile_vertical.png");
            this.imageList1.Images.SetKeyName(31, "application_view_columns.png");
            this.imageList1.Images.SetKeyName(32, "application_view_detail.png");
            this.imageList1.Images.SetKeyName(33, "application_view_gallery.png");
            this.imageList1.Images.SetKeyName(34, "application_view_icons.png");
            this.imageList1.Images.SetKeyName(35, "application_view_list.png");
            this.imageList1.Images.SetKeyName(36, "application_view_tile.png");
            this.imageList1.Images.SetKeyName(37, "application_view_xp.png");
            this.imageList1.Images.SetKeyName(38, "application_view_xp_terminal.png");
            this.imageList1.Images.SetKeyName(39, "application2.png");
            this.imageList1.Images.SetKeyName(40, "arrow_branch.png");
            this.imageList1.Images.SetKeyName(41, "arrow_divide.png");
            this.imageList1.Images.SetKeyName(42, "arrow_in.png");
            this.imageList1.Images.SetKeyName(43, "arrow_inout.png");
            this.imageList1.Images.SetKeyName(44, "arrow_join.png");
            this.imageList1.Images.SetKeyName(45, "arrow_left.png");
            this.imageList1.Images.SetKeyName(46, "arrow_merge.png");
            this.imageList1.Images.SetKeyName(47, "arrow_out.png");
            this.imageList1.Images.SetKeyName(48, "arrow_redo.png");
            this.imageList1.Images.SetKeyName(49, "arrow_refresh.png");
            this.imageList1.Images.SetKeyName(50, "arrow_right.png");
            this.imageList1.Images.SetKeyName(51, "arrow_undo.png");
            this.imageList1.Images.SetKeyName(52, "asterisk_orange.png");
            this.imageList1.Images.SetKeyName(53, "attach.png");
            this.imageList1.Images.SetKeyName(54, "attach_2.png");
            this.imageList1.Images.SetKeyName(55, "award_star_gold.png");
            this.imageList1.Images.SetKeyName(56, "bandaid.png");
            this.imageList1.Images.SetKeyName(57, "basket.png");
            this.imageList1.Images.SetKeyName(58, "bell.png");
            this.imageList1.Images.SetKeyName(59, "bin_closed.png");
            this.imageList1.Images.SetKeyName(60, "blog.png");
            this.imageList1.Images.SetKeyName(61, "blueprint.png");
            this.imageList1.Images.SetKeyName(62, "blueprint_horizontal.png");
            this.imageList1.Images.SetKeyName(63, "bluetooth.png");
            this.imageList1.Images.SetKeyName(64, "bomb.png");
            this.imageList1.Images.SetKeyName(65, "book.png");
            this.imageList1.Images.SetKeyName(66, "book_addresses.png");
            this.imageList1.Images.SetKeyName(67, "book_next.png");
            this.imageList1.Images.SetKeyName(68, "book_open.png");
            this.imageList1.Images.SetKeyName(69, "book_previous.png");
            this.imageList1.Images.SetKeyName(70, "bookmark.png");
            this.imageList1.Images.SetKeyName(71, "bookmark_book.png");
            this.imageList1.Images.SetKeyName(72, "bookmark_book_open.png");
            this.imageList1.Images.SetKeyName(73, "bookmark_document.png");
            this.imageList1.Images.SetKeyName(74, "bookmark_folder.png");
            this.imageList1.Images.SetKeyName(75, "books.png");
            this.imageList1.Images.SetKeyName(76, "box.png");
            this.imageList1.Images.SetKeyName(77, "brick.png");
            this.imageList1.Images.SetKeyName(78, "bricks.png");
            this.imageList1.Images.SetKeyName(79, "briefcase.png");
            this.imageList1.Images.SetKeyName(80, "bug.png");
            this.imageList1.Images.SetKeyName(81, "buildings.png");
            this.imageList1.Images.SetKeyName(82, "bullet_add_1.png");
            this.imageList1.Images.SetKeyName(83, "bullet_add_2.png");
            this.imageList1.Images.SetKeyName(84, "bullet_key.png");
            this.imageList1.Images.SetKeyName(85, "cake.png");
            this.imageList1.Images.SetKeyName(86, "calculator.png");
            this.imageList1.Images.SetKeyName(87, "calendar_1.png");
            this.imageList1.Images.SetKeyName(88, "calendar_2.png");
            this.imageList1.Images.SetKeyName(89, "camera.png");
            this.imageList1.Images.SetKeyName(90, "cancel.png");
            this.imageList1.Images.SetKeyName(91, "car.png");
            this.imageList1.Images.SetKeyName(92, "cart.png");
            this.imageList1.Images.SetKeyName(93, "cd.png");
            this.imageList1.Images.SetKeyName(94, "chart_bar.png");
            this.imageList1.Images.SetKeyName(95, "chart_curve.png");
            this.imageList1.Images.SetKeyName(96, "chart_line.png");
            this.imageList1.Images.SetKeyName(97, "chart_organisation.png");
            this.imageList1.Images.SetKeyName(98, "chart_pie.png");
            this.imageList1.Images.SetKeyName(99, "clipboard_paste_image.png");
            this.imageList1.Images.SetKeyName(100, "clipboard_sign.png");
            this.imageList1.Images.SetKeyName(101, "clipboard_text.png");
            this.imageList1.Images.SetKeyName(102, "clock.png");
            this.imageList1.Images.SetKeyName(103, "cog.png");
            this.imageList1.Images.SetKeyName(104, "coins.png");
            this.imageList1.Images.SetKeyName(105, "color_swatch_1.png");
            this.imageList1.Images.SetKeyName(106, "color_swatch_2.png");
            this.imageList1.Images.SetKeyName(107, "comment.png");
            this.imageList1.Images.SetKeyName(108, "compass.png");
            this.imageList1.Images.SetKeyName(109, "compress.png");
            this.imageList1.Images.SetKeyName(110, "computer.png");
            this.imageList1.Images.SetKeyName(111, "connect.png");
            this.imageList1.Images.SetKeyName(112, "contrast.png");
            this.imageList1.Images.SetKeyName(113, "control_eject.png");
            this.imageList1.Images.SetKeyName(114, "control_end.png");
            this.imageList1.Images.SetKeyName(115, "control_equalizer.png");
            this.imageList1.Images.SetKeyName(116, "control_fastforward.png");
            this.imageList1.Images.SetKeyName(117, "control_pause.png");
            this.imageList1.Images.SetKeyName(118, "control_play.png");
            this.imageList1.Images.SetKeyName(119, "control_repeat.png");
            this.imageList1.Images.SetKeyName(120, "control_rewind.png");
            this.imageList1.Images.SetKeyName(121, "control_start.png");
            this.imageList1.Images.SetKeyName(122, "control_stop.png");
            this.imageList1.Images.SetKeyName(123, "control_wheel.png");
            this.imageList1.Images.SetKeyName(124, "counter.png");
            this.imageList1.Images.SetKeyName(125, "counter_count.png");
            this.imageList1.Images.SetKeyName(126, "counter_count_up.png");
            this.imageList1.Images.SetKeyName(127, "counter_reset.png");
            this.imageList1.Images.SetKeyName(128, "counter_stop.png");
            this.imageList1.Images.SetKeyName(129, "cross.png");
            this.imageList1.Images.SetKeyName(130, "cross_octagon.png");
            this.imageList1.Images.SetKeyName(131, "cross_octagon_fram.png");
            this.imageList1.Images.SetKeyName(132, "cross_shield.png");
            this.imageList1.Images.SetKeyName(133, "cross_shield_2.png");
            this.imageList1.Images.SetKeyName(134, "crown.png");
            this.imageList1.Images.SetKeyName(135, "crown_bronze.png");
            this.imageList1.Images.SetKeyName(136, "crown_silver.png");
            this.imageList1.Images.SetKeyName(137, "css.png");
            this.imageList1.Images.SetKeyName(138, "cursor.png");
            this.imageList1.Images.SetKeyName(139, "cut.png");
            this.imageList1.Images.SetKeyName(140, "dashboard.png");
            this.imageList1.Images.SetKeyName(141, "data.png");
            this.imageList1.Images.SetKeyName(142, "database.png");
            this.imageList1.Images.SetKeyName(143, "databases.png");
            this.imageList1.Images.SetKeyName(144, "delete.png");
            this.imageList1.Images.SetKeyName(145, "delivery.png");
            this.imageList1.Images.SetKeyName(146, "desktop.png");
            this.imageList1.Images.SetKeyName(147, "desktop_empty.png");
            this.imageList1.Images.SetKeyName(148, "direction.png");
            this.imageList1.Images.SetKeyName(149, "disconnect.png");
            this.imageList1.Images.SetKeyName(150, "disk.png");
            this.imageList1.Images.SetKeyName(151, "doc_access.png");
            this.imageList1.Images.SetKeyName(152, "doc_break.png");
            this.imageList1.Images.SetKeyName(153, "doc_convert.png");
            this.imageList1.Images.SetKeyName(154, "doc_excel_csv.png");
            this.imageList1.Images.SetKeyName(155, "doc_excel_table.png");
            this.imageList1.Images.SetKeyName(156, "doc_film.png");
            this.imageList1.Images.SetKeyName(157, "doc_illustrator.png");
            this.imageList1.Images.SetKeyName(158, "doc_music.png");
            this.imageList1.Images.SetKeyName(159, "doc_music_playlist.png");
            this.imageList1.Images.SetKeyName(160, "doc_offlice.png");
            this.imageList1.Images.SetKeyName(161, "doc_page.png");
            this.imageList1.Images.SetKeyName(162, "doc_page_previous.png");
            this.imageList1.Images.SetKeyName(163, "doc_pdf.png");
            this.imageList1.Images.SetKeyName(164, "doc_photoshop.png");
            this.imageList1.Images.SetKeyName(165, "doc_resize.png");
            this.imageList1.Images.SetKeyName(166, "doc_resize_actual.png");
            this.imageList1.Images.SetKeyName(167, "doc_shred.png");
            this.imageList1.Images.SetKeyName(168, "doc_stand.png");
            this.imageList1.Images.SetKeyName(169, "doc_table.png");
            this.imageList1.Images.SetKeyName(170, "doc_tag.png");
            this.imageList1.Images.SetKeyName(171, "doc_text_image.png");
            this.imageList1.Images.SetKeyName(172, "door.png");
            this.imageList1.Images.SetKeyName(173, "door_in.png");
            this.imageList1.Images.SetKeyName(174, "drawer.png");
            this.imageList1.Images.SetKeyName(175, "drink.png");
            this.imageList1.Images.SetKeyName(176, "drink_empty.png");
            this.imageList1.Images.SetKeyName(177, "drive.png");
            this.imageList1.Images.SetKeyName(178, "drive_burn.png");
            this.imageList1.Images.SetKeyName(179, "drive_cd.png");
            this.imageList1.Images.SetKeyName(180, "drive_cd_empty.png");
            this.imageList1.Images.SetKeyName(181, "drive_delete.png");
            this.imageList1.Images.SetKeyName(182, "drive_disk.png");
            this.imageList1.Images.SetKeyName(183, "drive_error.png");
            this.imageList1.Images.SetKeyName(184, "drive_go.png");
            this.imageList1.Images.SetKeyName(185, "drive_link.png");
            this.imageList1.Images.SetKeyName(186, "drive_network.png");
            this.imageList1.Images.SetKeyName(187, "drive_rename.png");
            this.imageList1.Images.SetKeyName(188, "dvd.png");
            this.imageList1.Images.SetKeyName(189, "email.png");
            this.imageList1.Images.SetKeyName(190, "email_open.png");
            this.imageList1.Images.SetKeyName(191, "email_open_image.png");
            this.imageList1.Images.SetKeyName(192, "emoticon_evilgrin.png");
            this.imageList1.Images.SetKeyName(193, "emoticon_grin.png");
            this.imageList1.Images.SetKeyName(194, "emoticon_happy.png");
            this.imageList1.Images.SetKeyName(195, "emoticon_smile.png");
            this.imageList1.Images.SetKeyName(196, "emoticon_surprised.png");
            this.imageList1.Images.SetKeyName(197, "emoticon_tongue.png");
            this.imageList1.Images.SetKeyName(198, "emoticon_unhappy.png");
            this.imageList1.Images.SetKeyName(199, "emoticon_waii.png");
            this.imageList1.Images.SetKeyName(200, "emoticon_wink.png");
            this.imageList1.Images.SetKeyName(201, "envelope.png");
            this.imageList1.Images.SetKeyName(202, "envelope_2.png");
            this.imageList1.Images.SetKeyName(203, "error.png");
            this.imageList1.Images.SetKeyName(204, "exclamation.png");
            this.imageList1.Images.SetKeyName(205, "exclamation_octagon_fram.png");
            this.imageList1.Images.SetKeyName(206, "eye.png");
            this.imageList1.Images.SetKeyName(207, "feed.png");
            this.imageList1.Images.SetKeyName(208, "feed_ballon.png");
            this.imageList1.Images.SetKeyName(209, "feed_document.png");
            this.imageList1.Images.SetKeyName(210, "female.png");
            this.imageList1.Images.SetKeyName(211, "film.png");
            this.imageList1.Images.SetKeyName(212, "films.png");
            this.imageList1.Images.SetKeyName(213, "find.png");
            this.imageList1.Images.SetKeyName(214, "flag_blue.png");
            this.imageList1.Images.SetKeyName(215, "folder.png");
            this.imageList1.Images.SetKeyName(216, "font.png");
            this.imageList1.Images.SetKeyName(217, "funnel.png");
            this.imageList1.Images.SetKeyName(218, "grid.png");
            this.imageList1.Images.SetKeyName(219, "grid_dot.png");
            this.imageList1.Images.SetKeyName(220, "group.png");
            this.imageList1.Images.SetKeyName(221, "hammer.png");
            this.imageList1.Images.SetKeyName(222, "hammer_screwdriver.png");
            this.imageList1.Images.SetKeyName(223, "hand.png");
            this.imageList1.Images.SetKeyName(224, "hand_point.png");
            this.imageList1.Images.SetKeyName(225, "heart.png");
            this.imageList1.Images.SetKeyName(226, "heart_break.png");
            this.imageList1.Images.SetKeyName(227, "heart_empty.png");
            this.imageList1.Images.SetKeyName(228, "heart_half.png");
            this.imageList1.Images.SetKeyName(229, "heart_small.png");
            this.imageList1.Images.SetKeyName(230, "help.png");
            this.imageList1.Images.SetKeyName(231, "highlighter.png");
            this.imageList1.Images.SetKeyName(232, "house.png");
            this.imageList1.Images.SetKeyName(233, "html.png");
            this.imageList1.Images.SetKeyName(234, "image_1.png");
            this.imageList1.Images.SetKeyName(235, "image_2.png");
            this.imageList1.Images.SetKeyName(236, "images.png");
            this.imageList1.Images.SetKeyName(237, "inbox.png");
            this.imageList1.Images.SetKeyName(238, "ipod.png");
            this.imageList1.Images.SetKeyName(239, "ipod_cast.png");
            this.imageList1.Images.SetKeyName(240, "joystick.png");
            this.imageList1.Images.SetKeyName(241, "key.png");
            this.imageList1.Images.SetKeyName(242, "keyboard.png");
            this.imageList1.Images.SetKeyName(243, "layer_treansparent.png");
            this.imageList1.Images.SetKeyName(244, "layers.png");
            this.imageList1.Images.SetKeyName(245, "layout.png");
            this.imageList1.Images.SetKeyName(246, "layout_header_footer_3.png");
            this.imageList1.Images.SetKeyName(247, "layout_header_footer_3_mix.png");
            this.imageList1.Images.SetKeyName(248, "layout_join.png");
            this.imageList1.Images.SetKeyName(249, "layout_join_vertical.png");
            this.imageList1.Images.SetKeyName(250, "layout_select.png");
            this.imageList1.Images.SetKeyName(251, "layout_select_content.png");
            this.imageList1.Images.SetKeyName(252, "layout_select_footer.png");
            this.imageList1.Images.SetKeyName(253, "layout_select_sidebar.png");
            this.imageList1.Images.SetKeyName(254, "layout_split.png");
            this.imageList1.Images.SetKeyName(255, "layout_split_vertical.png");
            this.imageList1.Images.SetKeyName(256, "lifebuoy.png");
            this.imageList1.Images.SetKeyName(257, "lightbulb.png");
            this.imageList1.Images.SetKeyName(258, "lightbulb_off.png");
            this.imageList1.Images.SetKeyName(259, "lightning.png");
            this.imageList1.Images.SetKeyName(260, "link.png");
            this.imageList1.Images.SetKeyName(261, "link_break.png");
            this.imageList1.Images.SetKeyName(262, "lock.png");
            this.imageList1.Images.SetKeyName(263, "lock_unlock.png");
            this.imageList1.Images.SetKeyName(264, "magnet.png");
            this.imageList1.Images.SetKeyName(265, "magnifier.png");
            this.imageList1.Images.SetKeyName(266, "magnifier_zoom_in.png");
            this.imageList1.Images.SetKeyName(267, "male.png");
            this.imageList1.Images.SetKeyName(268, "map.png");
            this.imageList1.Images.SetKeyName(269, "marker.png");
            this.imageList1.Images.SetKeyName(270, "medal_bronze_1.png");
            this.imageList1.Images.SetKeyName(271, "medal_gold_1.png");
            this.imageList1.Images.SetKeyName(272, "media_player_small_blue.png");
            this.imageList1.Images.SetKeyName(273, "microphone.png");
            this.imageList1.Images.SetKeyName(274, "mobile_phone.png");
            this.imageList1.Images.SetKeyName(275, "money.png");
            this.imageList1.Images.SetKeyName(276, "money_dollar.png");
            this.imageList1.Images.SetKeyName(277, "money_euro.png");
            this.imageList1.Images.SetKeyName(278, "money_pound.png");
            this.imageList1.Images.SetKeyName(279, "money_yen.png");
            this.imageList1.Images.SetKeyName(280, "monitor.png");
            this.imageList1.Images.SetKeyName(281, "mouse.png");
            this.imageList1.Images.SetKeyName(282, "music.png");
            this.imageList1.Images.SetKeyName(283, "music_beam.png");
            this.imageList1.Images.SetKeyName(284, "neutral.png");
            this.imageList1.Images.SetKeyName(285, "new.png");
            this.imageList1.Images.SetKeyName(286, "newspaper.png");
            this.imageList1.Images.SetKeyName(287, "note.png");
            this.imageList1.Images.SetKeyName(288, "nuclear.png");
            this.imageList1.Images.SetKeyName(289, "package.png");
            this.imageList1.Images.SetKeyName(290, "page.png");
            this.imageList1.Images.SetKeyName(291, "page_2.png");
            this.imageList1.Images.SetKeyName(292, "page_2_copy.png");
            this.imageList1.Images.SetKeyName(293, "page_code.png");
            this.imageList1.Images.SetKeyName(294, "page_copy.png");
            this.imageList1.Images.SetKeyName(295, "page_excel.png");
            this.imageList1.Images.SetKeyName(296, "page_lightning.png");
            this.imageList1.Images.SetKeyName(297, "page_paste.png");
            this.imageList1.Images.SetKeyName(298, "page_red.png");
            this.imageList1.Images.SetKeyName(299, "page_refresh.png");
            this.imageList1.Images.SetKeyName(300, "page_save.png");
            this.imageList1.Images.SetKeyName(301, "page_white_cplusplus.png");
            this.imageList1.Images.SetKeyName(302, "page_white_csharp.png");
            this.imageList1.Images.SetKeyName(303, "page_white_cup.png");
            this.imageList1.Images.SetKeyName(304, "page_white_database.png");
            this.imageList1.Images.SetKeyName(305, "page_white_delete.png");
            this.imageList1.Images.SetKeyName(306, "page_white_dvd.png");
            this.imageList1.Images.SetKeyName(307, "page_white_edit.png");
            this.imageList1.Images.SetKeyName(308, "page_white_error.png");
            this.imageList1.Images.SetKeyName(309, "page_white_excel.png");
            this.imageList1.Images.SetKeyName(310, "page_white_find.png");
            this.imageList1.Images.SetKeyName(311, "page_white_flash.png");
            this.imageList1.Images.SetKeyName(312, "page_white_freehand.png");
            this.imageList1.Images.SetKeyName(313, "page_white_gear.png");
            this.imageList1.Images.SetKeyName(314, "page_white_get.png");
            this.imageList1.Images.SetKeyName(315, "page_white_paintbrush.png");
            this.imageList1.Images.SetKeyName(316, "page_white_paste.png");
            this.imageList1.Images.SetKeyName(317, "page_white_php.png");
            this.imageList1.Images.SetKeyName(318, "page_white_picture.png");
            this.imageList1.Images.SetKeyName(319, "page_white_powerpoint.png");
            this.imageList1.Images.SetKeyName(320, "page_white_put.png");
            this.imageList1.Images.SetKeyName(321, "page_white_ruby.png");
            this.imageList1.Images.SetKeyName(322, "page_white_stack.png");
            this.imageList1.Images.SetKeyName(323, "page_white_star.png");
            this.imageList1.Images.SetKeyName(324, "page_white_swoosh.png");
            this.imageList1.Images.SetKeyName(325, "page_white_text.png");
            this.imageList1.Images.SetKeyName(326, "page_white_text_width.png");
            this.imageList1.Images.SetKeyName(327, "page_white_tux.png");
            this.imageList1.Images.SetKeyName(328, "page_white_vector.png");
            this.imageList1.Images.SetKeyName(329, "page_white_visualstudio.png");
            this.imageList1.Images.SetKeyName(330, "page_white_width.png");
            this.imageList1.Images.SetKeyName(331, "page_white_word.png");
            this.imageList1.Images.SetKeyName(332, "page_white_world.png");
            this.imageList1.Images.SetKeyName(333, "page_white_wrench.png");
            this.imageList1.Images.SetKeyName(334, "page_white_zip.png");
            this.imageList1.Images.SetKeyName(335, "paintbrush.png");
            this.imageList1.Images.SetKeyName(336, "paintcan.png");
            this.imageList1.Images.SetKeyName(337, "palette.png");
            this.imageList1.Images.SetKeyName(338, "paper_bag.png");
            this.imageList1.Images.SetKeyName(339, "paste_plain.png");
            this.imageList1.Images.SetKeyName(340, "paste_word.png");
            this.imageList1.Images.SetKeyName(341, "pencil.png");
            this.imageList1.Images.SetKeyName(342, "photo.png");
            this.imageList1.Images.SetKeyName(343, "photo_album.png");
            this.imageList1.Images.SetKeyName(344, "photos.png");
            this.imageList1.Images.SetKeyName(345, "piano.png");
            this.imageList1.Images.SetKeyName(346, "picture.png");
            this.imageList1.Images.SetKeyName(347, "pilcrow.png");
            this.imageList1.Images.SetKeyName(348, "pill.png");
            this.imageList1.Images.SetKeyName(349, "pin.png");
            this.imageList1.Images.SetKeyName(350, "pipette.png");
            this.imageList1.Images.SetKeyName(351, "plaing_card.png");
            this.imageList1.Images.SetKeyName(352, "plug.png");
            this.imageList1.Images.SetKeyName(353, "plugin.png");
            this.imageList1.Images.SetKeyName(354, "printer.png");
            this.imageList1.Images.SetKeyName(355, "projection_screen.png");
            this.imageList1.Images.SetKeyName(356, "projection_screen_present.png");
            this.imageList1.Images.SetKeyName(357, "rainbow.png");
            this.imageList1.Images.SetKeyName(358, "report.png");
            this.imageList1.Images.SetKeyName(359, "rocket.png");
            this.imageList1.Images.SetKeyName(360, "rosette.png");
            this.imageList1.Images.SetKeyName(361, "rss.png");
            this.imageList1.Images.SetKeyName(362, "ruby.png");
            this.imageList1.Images.SetKeyName(363, "ruler_1.png");
            this.imageList1.Images.SetKeyName(364, "ruler_2.png");
            this.imageList1.Images.SetKeyName(365, "ruler_crop.png");
            this.imageList1.Images.SetKeyName(366, "ruler_triangle.png");
            this.imageList1.Images.SetKeyName(367, "safe.png");
            this.imageList1.Images.SetKeyName(368, "script.png");
            this.imageList1.Images.SetKeyName(369, "selection.png");
            this.imageList1.Images.SetKeyName(370, "selection_select.png");
            this.imageList1.Images.SetKeyName(371, "server.png");
            this.imageList1.Images.SetKeyName(372, "shading.png");
            this.imageList1.Images.SetKeyName(373, "shape_aling_bottom.png");
            this.imageList1.Images.SetKeyName(374, "shape_aling_center.png");
            this.imageList1.Images.SetKeyName(375, "shape_aling_left.png");
            this.imageList1.Images.SetKeyName(376, "shape_aling_middle.png");
            this.imageList1.Images.SetKeyName(377, "shape_aling_right.png");
            this.imageList1.Images.SetKeyName(378, "shape_aling_top.png");
            this.imageList1.Images.SetKeyName(379, "shape_flip_horizontal.png");
            this.imageList1.Images.SetKeyName(380, "shape_flip_vertical.png");
            this.imageList1.Images.SetKeyName(381, "shape_group.png");
            this.imageList1.Images.SetKeyName(382, "shape_handles.png");
            this.imageList1.Images.SetKeyName(383, "shape_move_back.png");
            this.imageList1.Images.SetKeyName(384, "shape_move_backwards.png");
            this.imageList1.Images.SetKeyName(385, "shape_move_forwards.png");
            this.imageList1.Images.SetKeyName(386, "shape_move_front.png");
            this.imageList1.Images.SetKeyName(387, "shape_square.png");
            this.imageList1.Images.SetKeyName(388, "shield.png");
            this.imageList1.Images.SetKeyName(389, "sitemap.png");
            this.imageList1.Images.SetKeyName(390, "slide.png");
            this.imageList1.Images.SetKeyName(391, "slides.png");
            this.imageList1.Images.SetKeyName(392, "slides_stack.png");
            this.imageList1.Images.SetKeyName(393, "smiley_confuse.png");
            this.imageList1.Images.SetKeyName(394, "smiley_cool.png");
            this.imageList1.Images.SetKeyName(395, "smiley_cry.png");
            this.imageList1.Images.SetKeyName(396, "smiley_fat.png");
            this.imageList1.Images.SetKeyName(397, "smiley_mad.png");
            this.imageList1.Images.SetKeyName(398, "smiley_red.png");
            this.imageList1.Images.SetKeyName(399, "smiley_roll.png");
            this.imageList1.Images.SetKeyName(400, "smiley_slim.png");
            this.imageList1.Images.SetKeyName(401, "smiley_yell.png");
            this.imageList1.Images.SetKeyName(402, "socket.png");
            this.imageList1.Images.SetKeyName(403, "sockets.png");
            this.imageList1.Images.SetKeyName(404, "sort.png");
            this.imageList1.Images.SetKeyName(405, "sort_alphabet.png");
            this.imageList1.Images.SetKeyName(406, "sort_date.png");
            this.imageList1.Images.SetKeyName(407, "sort_disable.png");
            this.imageList1.Images.SetKeyName(408, "sort_number.png");
            this.imageList1.Images.SetKeyName(409, "sort_price.png");
            this.imageList1.Images.SetKeyName(410, "sort_quantity.png");
            this.imageList1.Images.SetKeyName(411, "sort_rating.png");
            this.imageList1.Images.SetKeyName(412, "sound.png");
            this.imageList1.Images.SetKeyName(413, "sound_note.png");
            this.imageList1.Images.SetKeyName(414, "spellcheck.png");
            this.imageList1.Images.SetKeyName(415, "sport_8ball.png");
            this.imageList1.Images.SetKeyName(416, "sport_basketball.png");
            this.imageList1.Images.SetKeyName(417, "sport_football.png");
            this.imageList1.Images.SetKeyName(418, "sport_golf.png");
            this.imageList1.Images.SetKeyName(419, "sport_raquet.png");
            this.imageList1.Images.SetKeyName(420, "sport_shuttlecock.png");
            this.imageList1.Images.SetKeyName(421, "sport_soccer.png");
            this.imageList1.Images.SetKeyName(422, "sport_tennis.png");
            this.imageList1.Images.SetKeyName(423, "stamp.png");
            this.imageList1.Images.SetKeyName(424, "star_1.png");
            this.imageList1.Images.SetKeyName(425, "star_2.png");
            this.imageList1.Images.SetKeyName(426, "status_online.png");
            this.imageList1.Images.SetKeyName(427, "stop.png");
            this.imageList1.Images.SetKeyName(428, "style.png");
            this.imageList1.Images.SetKeyName(429, "sum.png");
            this.imageList1.Images.SetKeyName(430, "sum_2.png");
            this.imageList1.Images.SetKeyName(431, "switch.png");
            this.imageList1.Images.SetKeyName(432, "tab.png");
            this.imageList1.Images.SetKeyName(433, "table.png");
            this.imageList1.Images.SetKeyName(434, "tag.png");
            this.imageList1.Images.SetKeyName(435, "tag_blue.png");
            this.imageList1.Images.SetKeyName(436, "target.png");
            this.imageList1.Images.SetKeyName(437, "telephone.png");
            this.imageList1.Images.SetKeyName(438, "television.png");
            this.imageList1.Images.SetKeyName(439, "text_align_center.png");
            this.imageList1.Images.SetKeyName(440, "text_align_justify.png");
            this.imageList1.Images.SetKeyName(441, "text_align_left.png");
            this.imageList1.Images.SetKeyName(442, "text_align_right.png");
            this.imageList1.Images.SetKeyName(443, "text_allcaps.png");
            this.imageList1.Images.SetKeyName(444, "text_bold.png");
            this.imageList1.Images.SetKeyName(445, "text_columns.png");
            this.imageList1.Images.SetKeyName(446, "text_dropcaps.png");
            this.imageList1.Images.SetKeyName(447, "text_heading_1.png");
            this.imageList1.Images.SetKeyName(448, "text_horizontalrule.png");
            this.imageList1.Images.SetKeyName(449, "text_indent.png");
            this.imageList1.Images.SetKeyName(450, "text_indent_remove.png");
            this.imageList1.Images.SetKeyName(451, "text_italic.png");
            this.imageList1.Images.SetKeyName(452, "text_kerning.png");
            this.imageList1.Images.SetKeyName(453, "text_letter_omega.png");
            this.imageList1.Images.SetKeyName(454, "text_letterspacing.png");
            this.imageList1.Images.SetKeyName(455, "text_linespacing.png");
            this.imageList1.Images.SetKeyName(456, "text_list_bullets.png");
            this.imageList1.Images.SetKeyName(457, "text_list_numbers.png");
            this.imageList1.Images.SetKeyName(458, "text_lowercase.png");
            this.imageList1.Images.SetKeyName(459, "text_padding_bottom.png");
            this.imageList1.Images.SetKeyName(460, "text_padding_left.png");
            this.imageList1.Images.SetKeyName(461, "text_padding_right.png");
            this.imageList1.Images.SetKeyName(462, "text_padding_top.png");
            this.imageList1.Images.SetKeyName(463, "text_signature.png");
            this.imageList1.Images.SetKeyName(464, "text_smallcaps.png");
            this.imageList1.Images.SetKeyName(465, "text_strikethrough.png");
            this.imageList1.Images.SetKeyName(466, "text_subscript.png");
            this.imageList1.Images.SetKeyName(467, "textfield.png");
            this.imageList1.Images.SetKeyName(468, "textfield_rename.png");
            this.imageList1.Images.SetKeyName(469, "ticket.png");
            this.imageList1.Images.SetKeyName(470, "timeline_marker.png");
            this.imageList1.Images.SetKeyName(471, "traffic.png");
            this.imageList1.Images.SetKeyName(472, "transmit.png");
            this.imageList1.Images.SetKeyName(473, "trophy.png");
            this.imageList1.Images.SetKeyName(474, "trophy_bronze.png");
            this.imageList1.Images.SetKeyName(475, "trophy_silver.png");
            this.imageList1.Images.SetKeyName(476, "ui_combo_box.png");
            this.imageList1.Images.SetKeyName(477, "ui_saccordion.png");
            this.imageList1.Images.SetKeyName(478, "ui_slider_1.png");
            this.imageList1.Images.SetKeyName(479, "ui_slider_2.png");
            this.imageList1.Images.SetKeyName(480, "ui_tab_bottom.png");
            this.imageList1.Images.SetKeyName(481, "ui_tab_content.png");
            this.imageList1.Images.SetKeyName(482, "ui_tab_disable.png");
            this.imageList1.Images.SetKeyName(483, "ui_tab_side.png");
            this.imageList1.Images.SetKeyName(484, "ui_text_field_hidden.png");
            this.imageList1.Images.SetKeyName(485, "ui_text_field_password.png");
            this.imageList1.Images.SetKeyName(486, "umbrella.png");
            this.imageList1.Images.SetKeyName(487, "user.png");
            this.imageList1.Images.SetKeyName(488, "user_black_female.png");
            this.imageList1.Images.SetKeyName(489, "user_business.png");
            this.imageList1.Images.SetKeyName(490, "user_business_boss.png");
            this.imageList1.Images.SetKeyName(491, "user_female.png");
            this.imageList1.Images.SetKeyName(492, "user_silhouette.png");
            this.imageList1.Images.SetKeyName(493, "user_thief.png");
            this.imageList1.Images.SetKeyName(494, "user_thief_baldie.png");
            this.imageList1.Images.SetKeyName(495, "vcard.png");
            this.imageList1.Images.SetKeyName(496, "vector.png");
            this.imageList1.Images.SetKeyName(497, "wait.png");
            this.imageList1.Images.SetKeyName(498, "wall.png");
            this.imageList1.Images.SetKeyName(499, "wall_break.png");
            this.imageList1.Images.SetKeyName(500, "wall_brick.png");
            this.imageList1.Images.SetKeyName(501, "wall_disable.png");
            this.imageList1.Images.SetKeyName(502, "wand.png");
            this.imageList1.Images.SetKeyName(503, "weather_clouds.png");
            this.imageList1.Images.SetKeyName(504, "weather_cloudy.png");
            this.imageList1.Images.SetKeyName(505, "weather_lightning.png");
            this.imageList1.Images.SetKeyName(506, "weather_rain.png");
            this.imageList1.Images.SetKeyName(507, "weather_snow.png");
            this.imageList1.Images.SetKeyName(508, "weather_sun.png");
            this.imageList1.Images.SetKeyName(509, "webcam.png");
            this.imageList1.Images.SetKeyName(510, "world.png");
            this.imageList1.Images.SetKeyName(511, "zone.png");
            this.imageList1.Images.SetKeyName(512, "zone_money.png");
            this.imageList1.Images.SetKeyName(513, "zones.png");
            this.imageList1.Images.SetKeyName(514, "tutorials_opengl_01.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtb_final);
            this.splitContainer1.Size = new System.Drawing.Size(920, 399);
            this.splitContainer1.SplitterDistance = 456;
            this.splitContainer1.TabIndex = 5;
            // 
            // rtb
            // 
            this.rtb.AcceptsReturn = true;
            this.rtb.AcceptsTab = true;
            this.rtb.AllowDrop = true;
            this.rtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb.ForeColor = System.Drawing.Color.White;
            this.rtb.Location = new System.Drawing.Point(3, 403);
            this.rtb.Margin = new System.Windows.Forms.Padding(0);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(914, 15);
            this.rtb.TabIndex = 0;
            this.rtb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb_KeyDown);
            // 
            // bkk_suggest
            // 
            this.bkk_suggest.WorkerSupportsCancellation = true;
            // 
            // Special_TextView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.Main_Control_Strip);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "Special_TextView";
            this.Size = new System.Drawing.Size(920, 450);
            this.cms.ResumeLayout(false);
            this.Main_Control_Strip.ResumeLayout(false);
            this.Main_Control_Strip.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        public System.Windows.Forms.ToolStrip Main_Control_Strip;
        private System.Windows.Forms.ToolStripComboBox f_type;
        private System.Windows.Forms.ToolStripComboBox f_size;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        public System.Windows.Forms.RichTextBox rtb_final;
        public System.Windows.Forms.TextBox rtb;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem objectViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ww;
        private System.ComponentModel.BackgroundWorker bkk_suggest;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem serverManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem analyzerToolStripMenuItem;
    }
}

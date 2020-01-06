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
        protected override void Dispose(bool disposing)
        {
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
            this.SuspendLayout();
            // 
            // bkk_cleaner
            // 
            this.bkk_cleaner.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_cleaner_DoWork);
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
            // scnti
            // 
            this.scnti.ContextMenuStrip = this.cms;
            resources.ApplyResources(this.scnti, "scnti");
            this.scnti.DoubleClick += new System.EventHandler(this.scnti_DoubleClick);
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
            // 
            this.Startup_Load_Objects.WorkerSupportsCancellation = true;
            this.Startup_Load_Objects.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Startup_Load_Objects_DoWork);
            this.Startup_Load_Objects.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Startup_Load_Objects_RunWorkerCompleted);
            // 
            // bkk_load_apps
            // 
            this.bkk_load_apps.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_load_apps_DoWork);
            // 
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
            this.image_list.Images.SetKeyName(0, "brick.png");
            this.image_list.Images.SetKeyName(1, "application_form.png");
            this.image_list.Images.SetKeyName(2, "accept.png");
            this.image_list.Images.SetKeyName(3, "add.png");
            this.image_list.Images.SetKeyName(4, "alarm.png");
            this.image_list.Images.SetKeyName(5, "anchor.png");
            this.image_list.Images.SetKeyName(6, "application.png");
            this.image_list.Images.SetKeyName(7, "application_add.png");
            this.image_list.Images.SetKeyName(8, "application_cascade.png");
            this.image_list.Images.SetKeyName(9, "application_delete.png");
            this.image_list.Images.SetKeyName(10, "application_double.png");
            this.image_list.Images.SetKeyName(11, "application_edit.png");
            this.image_list.Images.SetKeyName(12, "application_error.png");
            this.image_list.Images.SetKeyName(13, "application_form.png");
            this.image_list.Images.SetKeyName(14, "application_get.png");
            this.image_list.Images.SetKeyName(15, "application_go.png");
            this.image_list.Images.SetKeyName(16, "application_home.png");
            this.image_list.Images.SetKeyName(17, "application_key.png");
            this.image_list.Images.SetKeyName(18, "application_lightning.png");
            this.image_list.Images.SetKeyName(19, "application_link.png");
            this.image_list.Images.SetKeyName(20, "application_osx.png");
            this.image_list.Images.SetKeyName(21, "application_osx_terminal.png");
            this.image_list.Images.SetKeyName(22, "application_put.png");
            this.image_list.Images.SetKeyName(23, "application_side_boxes.png");
            this.image_list.Images.SetKeyName(24, "application_side_contract.png");
            this.image_list.Images.SetKeyName(25, "application_side_expand.png");
            this.image_list.Images.SetKeyName(26, "application_side_list.png");
            this.image_list.Images.SetKeyName(27, "application_side_tree.png");
            this.image_list.Images.SetKeyName(28, "application_split.png");
            this.image_list.Images.SetKeyName(29, "application_tile_horizontal.png");
            this.image_list.Images.SetKeyName(30, "application_tile_vertical.png");
            this.image_list.Images.SetKeyName(31, "application_view_columns.png");
            this.image_list.Images.SetKeyName(32, "application_view_detail.png");
            this.image_list.Images.SetKeyName(33, "application_view_gallery.png");
            this.image_list.Images.SetKeyName(34, "application_view_icons.png");
            this.image_list.Images.SetKeyName(35, "application_view_list.png");
            this.image_list.Images.SetKeyName(36, "application_view_tile.png");
            this.image_list.Images.SetKeyName(37, "application_view_xp.png");
            this.image_list.Images.SetKeyName(38, "application_view_xp_terminal.png");
            this.image_list.Images.SetKeyName(39, "application2.png");
            this.image_list.Images.SetKeyName(40, "arrow_branch.png");
            this.image_list.Images.SetKeyName(41, "arrow_divide.png");
            this.image_list.Images.SetKeyName(42, "arrow_in.png");
            this.image_list.Images.SetKeyName(43, "arrow_inout.png");
            this.image_list.Images.SetKeyName(44, "arrow_join.png");
            this.image_list.Images.SetKeyName(45, "arrow_left.png");
            this.image_list.Images.SetKeyName(46, "arrow_merge.png");
            this.image_list.Images.SetKeyName(47, "arrow_out.png");
            this.image_list.Images.SetKeyName(48, "arrow_redo.png");
            this.image_list.Images.SetKeyName(49, "arrow_refresh.png");
            this.image_list.Images.SetKeyName(50, "arrow_right.png");
            this.image_list.Images.SetKeyName(51, "arrow_undo.png");
            this.image_list.Images.SetKeyName(52, "asterisk_orange.png");
            this.image_list.Images.SetKeyName(53, "attach.png");
            this.image_list.Images.SetKeyName(54, "attach_2.png");
            this.image_list.Images.SetKeyName(55, "award_star_gold.png");
            this.image_list.Images.SetKeyName(56, "bandaid.png");
            this.image_list.Images.SetKeyName(57, "basket.png");
            this.image_list.Images.SetKeyName(58, "bell.png");
            this.image_list.Images.SetKeyName(59, "bin_closed.png");
            this.image_list.Images.SetKeyName(60, "blog.png");
            this.image_list.Images.SetKeyName(61, "blueprint.png");
            this.image_list.Images.SetKeyName(62, "blueprint_horizontal.png");
            this.image_list.Images.SetKeyName(63, "bluetooth.png");
            this.image_list.Images.SetKeyName(64, "bomb.png");
            this.image_list.Images.SetKeyName(65, "book.png");
            this.image_list.Images.SetKeyName(66, "book_addresses.png");
            this.image_list.Images.SetKeyName(67, "book_next.png");
            this.image_list.Images.SetKeyName(68, "book_open.png");
            this.image_list.Images.SetKeyName(69, "book_previous.png");
            this.image_list.Images.SetKeyName(70, "bookmark.png");
            this.image_list.Images.SetKeyName(71, "bookmark_book.png");
            this.image_list.Images.SetKeyName(72, "bookmark_book_open.png");
            this.image_list.Images.SetKeyName(73, "bookmark_document.png");
            this.image_list.Images.SetKeyName(74, "bookmark_folder.png");
            this.image_list.Images.SetKeyName(75, "books.png");
            this.image_list.Images.SetKeyName(76, "box.png");
            this.image_list.Images.SetKeyName(77, "brick.png");
            this.image_list.Images.SetKeyName(78, "bricks.png");
            this.image_list.Images.SetKeyName(79, "briefcase.png");
            this.image_list.Images.SetKeyName(80, "bug.png");
            this.image_list.Images.SetKeyName(81, "buildings.png");
            this.image_list.Images.SetKeyName(82, "bullet_add_1.png");
            this.image_list.Images.SetKeyName(83, "bullet_add_2.png");
            this.image_list.Images.SetKeyName(84, "bullet_key.png");
            this.image_list.Images.SetKeyName(85, "cake.png");
            this.image_list.Images.SetKeyName(86, "calculator.png");
            this.image_list.Images.SetKeyName(87, "calendar_1.png");
            this.image_list.Images.SetKeyName(88, "calendar_2.png");
            this.image_list.Images.SetKeyName(89, "camera.png");
            this.image_list.Images.SetKeyName(90, "cancel.png");
            this.image_list.Images.SetKeyName(91, "car.png");
            this.image_list.Images.SetKeyName(92, "cart.png");
            this.image_list.Images.SetKeyName(93, "cd.png");
            this.image_list.Images.SetKeyName(94, "chart_bar.png");
            this.image_list.Images.SetKeyName(95, "chart_curve.png");
            this.image_list.Images.SetKeyName(96, "chart_line.png");
            this.image_list.Images.SetKeyName(97, "chart_organisation.png");
            this.image_list.Images.SetKeyName(98, "chart_pie.png");
            this.image_list.Images.SetKeyName(99, "clipboard_paste_image.png");
            this.image_list.Images.SetKeyName(100, "clipboard_sign.png");
            this.image_list.Images.SetKeyName(101, "clipboard_text.png");
            this.image_list.Images.SetKeyName(102, "clock.png");
            this.image_list.Images.SetKeyName(103, "cog.png");
            this.image_list.Images.SetKeyName(104, "coins.png");
            this.image_list.Images.SetKeyName(105, "color_swatch_1.png");
            this.image_list.Images.SetKeyName(106, "color_swatch_2.png");
            this.image_list.Images.SetKeyName(107, "comment.png");
            this.image_list.Images.SetKeyName(108, "compass.png");
            this.image_list.Images.SetKeyName(109, "compress.png");
            this.image_list.Images.SetKeyName(110, "computer.png");
            this.image_list.Images.SetKeyName(111, "connect.png");
            this.image_list.Images.SetKeyName(112, "contrast.png");
            this.image_list.Images.SetKeyName(113, "control_eject.png");
            this.image_list.Images.SetKeyName(114, "control_end.png");
            this.image_list.Images.SetKeyName(115, "control_equalizer.png");
            this.image_list.Images.SetKeyName(116, "control_fastforward.png");
            this.image_list.Images.SetKeyName(117, "control_pause.png");
            this.image_list.Images.SetKeyName(118, "control_play.png");
            this.image_list.Images.SetKeyName(119, "control_repeat.png");
            this.image_list.Images.SetKeyName(120, "control_rewind.png");
            this.image_list.Images.SetKeyName(121, "control_start.png");
            this.image_list.Images.SetKeyName(122, "control_stop.png");
            this.image_list.Images.SetKeyName(123, "control_wheel.png");
            this.image_list.Images.SetKeyName(124, "counter.png");
            this.image_list.Images.SetKeyName(125, "counter_count.png");
            this.image_list.Images.SetKeyName(126, "counter_count_up.png");
            this.image_list.Images.SetKeyName(127, "counter_reset.png");
            this.image_list.Images.SetKeyName(128, "counter_stop.png");
            this.image_list.Images.SetKeyName(129, "cross.png");
            this.image_list.Images.SetKeyName(130, "cross_octagon.png");
            this.image_list.Images.SetKeyName(131, "cross_octagon_fram.png");
            this.image_list.Images.SetKeyName(132, "cross_shield.png");
            this.image_list.Images.SetKeyName(133, "cross_shield_2.png");
            this.image_list.Images.SetKeyName(134, "crown.png");
            this.image_list.Images.SetKeyName(135, "crown_bronze.png");
            this.image_list.Images.SetKeyName(136, "crown_silver.png");
            this.image_list.Images.SetKeyName(137, "css.png");
            this.image_list.Images.SetKeyName(138, "cursor.png");
            this.image_list.Images.SetKeyName(139, "cut.png");
            this.image_list.Images.SetKeyName(140, "dashboard.png");
            this.image_list.Images.SetKeyName(141, "data.png");
            this.image_list.Images.SetKeyName(142, "database.png");
            this.image_list.Images.SetKeyName(143, "databases.png");
            this.image_list.Images.SetKeyName(144, "delete.png");
            this.image_list.Images.SetKeyName(145, "delivery.png");
            this.image_list.Images.SetKeyName(146, "desktop.png");
            this.image_list.Images.SetKeyName(147, "desktop_empty.png");
            this.image_list.Images.SetKeyName(148, "direction.png");
            this.image_list.Images.SetKeyName(149, "disconnect.png");
            this.image_list.Images.SetKeyName(150, "disk.png");
            this.image_list.Images.SetKeyName(151, "doc_access.png");
            this.image_list.Images.SetKeyName(152, "doc_break.png");
            this.image_list.Images.SetKeyName(153, "doc_convert.png");
            this.image_list.Images.SetKeyName(154, "doc_excel_csv.png");
            this.image_list.Images.SetKeyName(155, "doc_excel_table.png");
            this.image_list.Images.SetKeyName(156, "doc_film.png");
            this.image_list.Images.SetKeyName(157, "doc_illustrator.png");
            this.image_list.Images.SetKeyName(158, "doc_music.png");
            this.image_list.Images.SetKeyName(159, "doc_music_playlist.png");
            this.image_list.Images.SetKeyName(160, "doc_offlice.png");
            this.image_list.Images.SetKeyName(161, "doc_page.png");
            this.image_list.Images.SetKeyName(162, "doc_page_previous.png");
            this.image_list.Images.SetKeyName(163, "doc_pdf.png");
            this.image_list.Images.SetKeyName(164, "doc_photoshop.png");
            this.image_list.Images.SetKeyName(165, "doc_resize.png");
            this.image_list.Images.SetKeyName(166, "doc_resize_actual.png");
            this.image_list.Images.SetKeyName(167, "doc_shred.png");
            this.image_list.Images.SetKeyName(168, "doc_stand.png");
            this.image_list.Images.SetKeyName(169, "doc_table.png");
            this.image_list.Images.SetKeyName(170, "doc_tag.png");
            this.image_list.Images.SetKeyName(171, "doc_text_image.png");
            this.image_list.Images.SetKeyName(172, "door.png");
            this.image_list.Images.SetKeyName(173, "door_in.png");
            this.image_list.Images.SetKeyName(174, "drawer.png");
            this.image_list.Images.SetKeyName(175, "drink.png");
            this.image_list.Images.SetKeyName(176, "drink_empty.png");
            this.image_list.Images.SetKeyName(177, "drive.png");
            this.image_list.Images.SetKeyName(178, "drive_burn.png");
            this.image_list.Images.SetKeyName(179, "drive_cd.png");
            this.image_list.Images.SetKeyName(180, "drive_cd_empty.png");
            this.image_list.Images.SetKeyName(181, "drive_delete.png");
            this.image_list.Images.SetKeyName(182, "drive_disk.png");
            this.image_list.Images.SetKeyName(183, "drive_error.png");
            this.image_list.Images.SetKeyName(184, "drive_go.png");
            this.image_list.Images.SetKeyName(185, "drive_link.png");
            this.image_list.Images.SetKeyName(186, "drive_network.png");
            this.image_list.Images.SetKeyName(187, "drive_rename.png");
            this.image_list.Images.SetKeyName(188, "dvd.png");
            this.image_list.Images.SetKeyName(189, "email.png");
            this.image_list.Images.SetKeyName(190, "email_open.png");
            this.image_list.Images.SetKeyName(191, "email_open_image.png");
            this.image_list.Images.SetKeyName(192, "emoticon_evilgrin.png");
            this.image_list.Images.SetKeyName(193, "emoticon_grin.png");
            this.image_list.Images.SetKeyName(194, "emoticon_happy.png");
            this.image_list.Images.SetKeyName(195, "emoticon_smile.png");
            this.image_list.Images.SetKeyName(196, "emoticon_surprised.png");
            this.image_list.Images.SetKeyName(197, "emoticon_tongue.png");
            this.image_list.Images.SetKeyName(198, "emoticon_unhappy.png");
            this.image_list.Images.SetKeyName(199, "emoticon_waii.png");
            this.image_list.Images.SetKeyName(200, "emoticon_wink.png");
            this.image_list.Images.SetKeyName(201, "envelope.png");
            this.image_list.Images.SetKeyName(202, "envelope_2.png");
            this.image_list.Images.SetKeyName(203, "error.png");
            this.image_list.Images.SetKeyName(204, "exclamation.png");
            this.image_list.Images.SetKeyName(205, "exclamation_octagon_fram.png");
            this.image_list.Images.SetKeyName(206, "eye.png");
            this.image_list.Images.SetKeyName(207, "feed.png");
            this.image_list.Images.SetKeyName(208, "feed_ballon.png");
            this.image_list.Images.SetKeyName(209, "feed_document.png");
            this.image_list.Images.SetKeyName(210, "female.png");
            this.image_list.Images.SetKeyName(211, "film.png");
            this.image_list.Images.SetKeyName(212, "films.png");
            this.image_list.Images.SetKeyName(213, "find.png");
            this.image_list.Images.SetKeyName(214, "flag_blue.png");
            this.image_list.Images.SetKeyName(215, "folder.png");
            this.image_list.Images.SetKeyName(216, "font.png");
            this.image_list.Images.SetKeyName(217, "funnel.png");
            this.image_list.Images.SetKeyName(218, "grid.png");
            this.image_list.Images.SetKeyName(219, "grid_dot.png");
            this.image_list.Images.SetKeyName(220, "group.png");
            this.image_list.Images.SetKeyName(221, "hammer.png");
            this.image_list.Images.SetKeyName(222, "hammer_screwdriver.png");
            this.image_list.Images.SetKeyName(223, "hand.png");
            this.image_list.Images.SetKeyName(224, "hand_point.png");
            this.image_list.Images.SetKeyName(225, "heart.png");
            this.image_list.Images.SetKeyName(226, "heart_break.png");
            this.image_list.Images.SetKeyName(227, "heart_empty.png");
            this.image_list.Images.SetKeyName(228, "heart_half.png");
            this.image_list.Images.SetKeyName(229, "heart_small.png");
            this.image_list.Images.SetKeyName(230, "help.png");
            this.image_list.Images.SetKeyName(231, "highlighter.png");
            this.image_list.Images.SetKeyName(232, "house.png");
            this.image_list.Images.SetKeyName(233, "html.png");
            this.image_list.Images.SetKeyName(234, "image_1.png");
            this.image_list.Images.SetKeyName(235, "image_2.png");
            this.image_list.Images.SetKeyName(236, "images.png");
            this.image_list.Images.SetKeyName(237, "inbox.png");
            this.image_list.Images.SetKeyName(238, "ipod.png");
            this.image_list.Images.SetKeyName(239, "ipod_cast.png");
            this.image_list.Images.SetKeyName(240, "joystick.png");
            this.image_list.Images.SetKeyName(241, "key.png");
            this.image_list.Images.SetKeyName(242, "keyboard.png");
            this.image_list.Images.SetKeyName(243, "layer_treansparent.png");
            this.image_list.Images.SetKeyName(244, "layers.png");
            this.image_list.Images.SetKeyName(245, "layout.png");
            this.image_list.Images.SetKeyName(246, "layout_header_footer_3.png");
            this.image_list.Images.SetKeyName(247, "layout_header_footer_3_mix.png");
            this.image_list.Images.SetKeyName(248, "layout_join.png");
            this.image_list.Images.SetKeyName(249, "layout_join_vertical.png");
            this.image_list.Images.SetKeyName(250, "layout_select.png");
            this.image_list.Images.SetKeyName(251, "layout_select_content.png");
            this.image_list.Images.SetKeyName(252, "layout_select_footer.png");
            this.image_list.Images.SetKeyName(253, "layout_select_sidebar.png");
            this.image_list.Images.SetKeyName(254, "layout_split.png");
            this.image_list.Images.SetKeyName(255, "layout_split_vertical.png");
            this.image_list.Images.SetKeyName(256, "lifebuoy.png");
            this.image_list.Images.SetKeyName(257, "lightbulb.png");
            this.image_list.Images.SetKeyName(258, "lightbulb_off.png");
            this.image_list.Images.SetKeyName(259, "lightning.png");
            this.image_list.Images.SetKeyName(260, "link.png");
            this.image_list.Images.SetKeyName(261, "link_break.png");
            this.image_list.Images.SetKeyName(262, "lock.png");
            this.image_list.Images.SetKeyName(263, "lock_unlock.png");
            this.image_list.Images.SetKeyName(264, "magnet.png");
            this.image_list.Images.SetKeyName(265, "magnifier.png");
            this.image_list.Images.SetKeyName(266, "magnifier_zoom_in.png");
            this.image_list.Images.SetKeyName(267, "male.png");
            this.image_list.Images.SetKeyName(268, "map.png");
            this.image_list.Images.SetKeyName(269, "marker.png");
            this.image_list.Images.SetKeyName(270, "medal_bronze_1.png");
            this.image_list.Images.SetKeyName(271, "medal_gold_1.png");
            this.image_list.Images.SetKeyName(272, "media_player_small_blue.png");
            this.image_list.Images.SetKeyName(273, "microphone.png");
            this.image_list.Images.SetKeyName(274, "mobile_phone.png");
            this.image_list.Images.SetKeyName(275, "money.png");
            this.image_list.Images.SetKeyName(276, "money_dollar.png");
            this.image_list.Images.SetKeyName(277, "money_euro.png");
            this.image_list.Images.SetKeyName(278, "money_pound.png");
            this.image_list.Images.SetKeyName(279, "money_yen.png");
            this.image_list.Images.SetKeyName(280, "monitor.png");
            this.image_list.Images.SetKeyName(281, "mouse.png");
            this.image_list.Images.SetKeyName(282, "music.png");
            this.image_list.Images.SetKeyName(283, "music_beam.png");
            this.image_list.Images.SetKeyName(284, "neutral.png");
            this.image_list.Images.SetKeyName(285, "new.png");
            this.image_list.Images.SetKeyName(286, "newspaper.png");
            this.image_list.Images.SetKeyName(287, "note.png");
            this.image_list.Images.SetKeyName(288, "nuclear.png");
            this.image_list.Images.SetKeyName(289, "package.png");
            this.image_list.Images.SetKeyName(290, "page.png");
            this.image_list.Images.SetKeyName(291, "page_2.png");
            this.image_list.Images.SetKeyName(292, "page_2_copy.png");
            this.image_list.Images.SetKeyName(293, "page_code.png");
            this.image_list.Images.SetKeyName(294, "page_copy.png");
            this.image_list.Images.SetKeyName(295, "page_excel.png");
            this.image_list.Images.SetKeyName(296, "page_lightning.png");
            this.image_list.Images.SetKeyName(297, "page_paste.png");
            this.image_list.Images.SetKeyName(298, "page_red.png");
            this.image_list.Images.SetKeyName(299, "page_refresh.png");
            this.image_list.Images.SetKeyName(300, "page_save.png");
            this.image_list.Images.SetKeyName(301, "page_white_cplusplus.png");
            this.image_list.Images.SetKeyName(302, "page_white_csharp.png");
            this.image_list.Images.SetKeyName(303, "page_white_cup.png");
            this.image_list.Images.SetKeyName(304, "page_white_database.png");
            this.image_list.Images.SetKeyName(305, "page_white_delete.png");
            this.image_list.Images.SetKeyName(306, "page_white_dvd.png");
            this.image_list.Images.SetKeyName(307, "page_white_edit.png");
            this.image_list.Images.SetKeyName(308, "page_white_error.png");
            this.image_list.Images.SetKeyName(309, "page_white_excel.png");
            this.image_list.Images.SetKeyName(310, "page_white_find.png");
            this.image_list.Images.SetKeyName(311, "page_white_flash.png");
            this.image_list.Images.SetKeyName(312, "page_white_freehand.png");
            this.image_list.Images.SetKeyName(313, "page_white_gear.png");
            this.image_list.Images.SetKeyName(314, "page_white_get.png");
            this.image_list.Images.SetKeyName(315, "page_white_paintbrush.png");
            this.image_list.Images.SetKeyName(316, "page_white_paste.png");
            this.image_list.Images.SetKeyName(317, "page_white_php.png");
            this.image_list.Images.SetKeyName(318, "page_white_picture.png");
            this.image_list.Images.SetKeyName(319, "page_white_powerpoint.png");
            this.image_list.Images.SetKeyName(320, "page_white_put.png");
            this.image_list.Images.SetKeyName(321, "page_white_ruby.png");
            this.image_list.Images.SetKeyName(322, "page_white_stack.png");
            this.image_list.Images.SetKeyName(323, "page_white_star.png");
            this.image_list.Images.SetKeyName(324, "page_white_swoosh.png");
            this.image_list.Images.SetKeyName(325, "page_white_text.png");
            this.image_list.Images.SetKeyName(326, "page_white_text_width.png");
            this.image_list.Images.SetKeyName(327, "page_white_tux.png");
            this.image_list.Images.SetKeyName(328, "page_white_vector.png");
            this.image_list.Images.SetKeyName(329, "page_white_visualstudio.png");
            this.image_list.Images.SetKeyName(330, "page_white_width.png");
            this.image_list.Images.SetKeyName(331, "page_white_word.png");
            this.image_list.Images.SetKeyName(332, "page_white_world.png");
            this.image_list.Images.SetKeyName(333, "page_white_wrench.png");
            this.image_list.Images.SetKeyName(334, "page_white_zip.png");
            this.image_list.Images.SetKeyName(335, "paintbrush.png");
            this.image_list.Images.SetKeyName(336, "paintcan.png");
            this.image_list.Images.SetKeyName(337, "palette.png");
            this.image_list.Images.SetKeyName(338, "paper_bag.png");
            this.image_list.Images.SetKeyName(339, "paste_plain.png");
            this.image_list.Images.SetKeyName(340, "paste_word.png");
            this.image_list.Images.SetKeyName(341, "pencil.png");
            this.image_list.Images.SetKeyName(342, "photo.png");
            this.image_list.Images.SetKeyName(343, "photo_album.png");
            this.image_list.Images.SetKeyName(344, "photos.png");
            this.image_list.Images.SetKeyName(345, "piano.png");
            this.image_list.Images.SetKeyName(346, "picture.png");
            this.image_list.Images.SetKeyName(347, "pilcrow.png");
            this.image_list.Images.SetKeyName(348, "pill.png");
            this.image_list.Images.SetKeyName(349, "pin.png");
            this.image_list.Images.SetKeyName(350, "pipette.png");
            this.image_list.Images.SetKeyName(351, "plaing_card.png");
            this.image_list.Images.SetKeyName(352, "plug.png");
            this.image_list.Images.SetKeyName(353, "plugin.png");
            this.image_list.Images.SetKeyName(354, "printer.png");
            this.image_list.Images.SetKeyName(355, "projection_screen.png");
            this.image_list.Images.SetKeyName(356, "projection_screen_present.png");
            this.image_list.Images.SetKeyName(357, "rainbow.png");
            this.image_list.Images.SetKeyName(358, "report.png");
            this.image_list.Images.SetKeyName(359, "rocket.png");
            this.image_list.Images.SetKeyName(360, "rosette.png");
            this.image_list.Images.SetKeyName(361, "rss.png");
            this.image_list.Images.SetKeyName(362, "ruby.png");
            this.image_list.Images.SetKeyName(363, "ruler_1.png");
            this.image_list.Images.SetKeyName(364, "ruler_2.png");
            this.image_list.Images.SetKeyName(365, "ruler_crop.png");
            this.image_list.Images.SetKeyName(366, "ruler_triangle.png");
            this.image_list.Images.SetKeyName(367, "safe.png");
            this.image_list.Images.SetKeyName(368, "script.png");
            this.image_list.Images.SetKeyName(369, "selection.png");
            this.image_list.Images.SetKeyName(370, "selection_select.png");
            this.image_list.Images.SetKeyName(371, "server.png");
            this.image_list.Images.SetKeyName(372, "shading.png");
            this.image_list.Images.SetKeyName(373, "shape_aling_bottom.png");
            this.image_list.Images.SetKeyName(374, "shape_aling_center.png");
            this.image_list.Images.SetKeyName(375, "shape_aling_left.png");
            this.image_list.Images.SetKeyName(376, "shape_aling_middle.png");
            this.image_list.Images.SetKeyName(377, "shape_aling_right.png");
            this.image_list.Images.SetKeyName(378, "shape_aling_top.png");
            this.image_list.Images.SetKeyName(379, "shape_flip_horizontal.png");
            this.image_list.Images.SetKeyName(380, "shape_flip_vertical.png");
            this.image_list.Images.SetKeyName(381, "shape_group.png");
            this.image_list.Images.SetKeyName(382, "shape_handles.png");
            this.image_list.Images.SetKeyName(383, "shape_move_back.png");
            this.image_list.Images.SetKeyName(384, "shape_move_backwards.png");
            this.image_list.Images.SetKeyName(385, "shape_move_forwards.png");
            this.image_list.Images.SetKeyName(386, "shape_move_front.png");
            this.image_list.Images.SetKeyName(387, "shape_square.png");
            this.image_list.Images.SetKeyName(388, "shield.png");
            this.image_list.Images.SetKeyName(389, "sitemap.png");
            this.image_list.Images.SetKeyName(390, "slide.png");
            this.image_list.Images.SetKeyName(391, "slides.png");
            this.image_list.Images.SetKeyName(392, "slides_stack.png");
            this.image_list.Images.SetKeyName(393, "smiley_confuse.png");
            this.image_list.Images.SetKeyName(394, "smiley_cool.png");
            this.image_list.Images.SetKeyName(395, "smiley_cry.png");
            this.image_list.Images.SetKeyName(396, "smiley_fat.png");
            this.image_list.Images.SetKeyName(397, "smiley_mad.png");
            this.image_list.Images.SetKeyName(398, "smiley_red.png");
            this.image_list.Images.SetKeyName(399, "smiley_roll.png");
            this.image_list.Images.SetKeyName(400, "smiley_slim.png");
            this.image_list.Images.SetKeyName(401, "smiley_yell.png");
            this.image_list.Images.SetKeyName(402, "socket.png");
            this.image_list.Images.SetKeyName(403, "sockets.png");
            this.image_list.Images.SetKeyName(404, "sort.png");
            this.image_list.Images.SetKeyName(405, "sort_alphabet.png");
            this.image_list.Images.SetKeyName(406, "sort_date.png");
            this.image_list.Images.SetKeyName(407, "sort_disable.png");
            this.image_list.Images.SetKeyName(408, "sort_number.png");
            this.image_list.Images.SetKeyName(409, "sort_price.png");
            this.image_list.Images.SetKeyName(410, "sort_quantity.png");
            this.image_list.Images.SetKeyName(411, "sort_rating.png");
            this.image_list.Images.SetKeyName(412, "sound.png");
            this.image_list.Images.SetKeyName(413, "sound_note.png");
            this.image_list.Images.SetKeyName(414, "spellcheck.png");
            this.image_list.Images.SetKeyName(415, "sport_8ball.png");
            this.image_list.Images.SetKeyName(416, "sport_basketball.png");
            this.image_list.Images.SetKeyName(417, "sport_football.png");
            this.image_list.Images.SetKeyName(418, "sport_golf.png");
            this.image_list.Images.SetKeyName(419, "sport_raquet.png");
            this.image_list.Images.SetKeyName(420, "sport_shuttlecock.png");
            this.image_list.Images.SetKeyName(421, "sport_soccer.png");
            this.image_list.Images.SetKeyName(422, "sport_tennis.png");
            this.image_list.Images.SetKeyName(423, "stamp.png");
            this.image_list.Images.SetKeyName(424, "star_1.png");
            this.image_list.Images.SetKeyName(425, "star_2.png");
            this.image_list.Images.SetKeyName(426, "status_online.png");
            this.image_list.Images.SetKeyName(427, "stop.png");
            this.image_list.Images.SetKeyName(428, "style.png");
            this.image_list.Images.SetKeyName(429, "sum.png");
            this.image_list.Images.SetKeyName(430, "sum_2.png");
            this.image_list.Images.SetKeyName(431, "switch.png");
            this.image_list.Images.SetKeyName(432, "tab.png");
            this.image_list.Images.SetKeyName(433, "table.png");
            this.image_list.Images.SetKeyName(434, "tag.png");
            this.image_list.Images.SetKeyName(435, "tag_blue.png");
            this.image_list.Images.SetKeyName(436, "target.png");
            this.image_list.Images.SetKeyName(437, "telephone.png");
            this.image_list.Images.SetKeyName(438, "television.png");
            this.image_list.Images.SetKeyName(439, "text_align_center.png");
            this.image_list.Images.SetKeyName(440, "text_align_justify.png");
            this.image_list.Images.SetKeyName(441, "text_align_left.png");
            this.image_list.Images.SetKeyName(442, "text_align_right.png");
            this.image_list.Images.SetKeyName(443, "text_allcaps.png");
            this.image_list.Images.SetKeyName(444, "text_bold.png");
            this.image_list.Images.SetKeyName(445, "text_columns.png");
            this.image_list.Images.SetKeyName(446, "text_dropcaps.png");
            this.image_list.Images.SetKeyName(447, "text_heading_1.png");
            this.image_list.Images.SetKeyName(448, "text_horizontalrule.png");
            this.image_list.Images.SetKeyName(449, "text_indent.png");
            this.image_list.Images.SetKeyName(450, "text_indent_remove.png");
            this.image_list.Images.SetKeyName(451, "text_italic.png");
            this.image_list.Images.SetKeyName(452, "text_kerning.png");
            this.image_list.Images.SetKeyName(453, "text_letter_omega.png");
            this.image_list.Images.SetKeyName(454, "text_letterspacing.png");
            this.image_list.Images.SetKeyName(455, "text_linespacing.png");
            this.image_list.Images.SetKeyName(456, "text_list_bullets.png");
            this.image_list.Images.SetKeyName(457, "text_list_numbers.png");
            this.image_list.Images.SetKeyName(458, "text_lowercase.png");
            this.image_list.Images.SetKeyName(459, "text_padding_bottom.png");
            this.image_list.Images.SetKeyName(460, "text_padding_left.png");
            this.image_list.Images.SetKeyName(461, "text_padding_right.png");
            this.image_list.Images.SetKeyName(462, "text_padding_top.png");
            this.image_list.Images.SetKeyName(463, "text_signature.png");
            this.image_list.Images.SetKeyName(464, "text_smallcaps.png");
            this.image_list.Images.SetKeyName(465, "text_strikethrough.png");
            this.image_list.Images.SetKeyName(466, "text_subscript.png");
            this.image_list.Images.SetKeyName(467, "textfield.png");
            this.image_list.Images.SetKeyName(468, "textfield_rename.png");
            this.image_list.Images.SetKeyName(469, "ticket.png");
            this.image_list.Images.SetKeyName(470, "timeline_marker.png");
            this.image_list.Images.SetKeyName(471, "traffic.png");
            this.image_list.Images.SetKeyName(472, "transmit.png");
            this.image_list.Images.SetKeyName(473, "trophy.png");
            this.image_list.Images.SetKeyName(474, "trophy_bronze.png");
            this.image_list.Images.SetKeyName(475, "trophy_silver.png");
            this.image_list.Images.SetKeyName(476, "ui_combo_box.png");
            this.image_list.Images.SetKeyName(477, "ui_saccordion.png");
            this.image_list.Images.SetKeyName(478, "ui_slider_1.png");
            this.image_list.Images.SetKeyName(479, "ui_slider_2.png");
            this.image_list.Images.SetKeyName(480, "ui_tab_bottom.png");
            this.image_list.Images.SetKeyName(481, "ui_tab_content.png");
            this.image_list.Images.SetKeyName(482, "ui_tab_disable.png");
            this.image_list.Images.SetKeyName(483, "ui_tab_side.png");
            this.image_list.Images.SetKeyName(484, "ui_text_field_hidden.png");
            this.image_list.Images.SetKeyName(485, "ui_text_field_password.png");
            this.image_list.Images.SetKeyName(486, "umbrella.png");
            this.image_list.Images.SetKeyName(487, "user.png");
            this.image_list.Images.SetKeyName(488, "user_black_female.png");
            this.image_list.Images.SetKeyName(489, "user_business.png");
            this.image_list.Images.SetKeyName(490, "user_business_boss.png");
            this.image_list.Images.SetKeyName(491, "user_female.png");
            this.image_list.Images.SetKeyName(492, "user_silhouette.png");
            this.image_list.Images.SetKeyName(493, "user_thief.png");
            this.image_list.Images.SetKeyName(494, "user_thief_baldie.png");
            this.image_list.Images.SetKeyName(495, "vcard.png");
            this.image_list.Images.SetKeyName(496, "vector.png");
            this.image_list.Images.SetKeyName(497, "wait.png");
            this.image_list.Images.SetKeyName(498, "wall.png");
            this.image_list.Images.SetKeyName(499, "wall_break.png");
            this.image_list.Images.SetKeyName(500, "wall_brick.png");
            this.image_list.Images.SetKeyName(501, "wall_disable.png");
            this.image_list.Images.SetKeyName(502, "wand.png");
            this.image_list.Images.SetKeyName(503, "weather_clouds.png");
            this.image_list.Images.SetKeyName(504, "weather_cloudy.png");
            this.image_list.Images.SetKeyName(505, "weather_lightning.png");
            this.image_list.Images.SetKeyName(506, "weather_rain.png");
            this.image_list.Images.SetKeyName(507, "weather_snow.png");
            this.image_list.Images.SetKeyName(508, "weather_sun.png");
            this.image_list.Images.SetKeyName(509, "webcam.png");
            this.image_list.Images.SetKeyName(510, "world.png");
            this.image_list.Images.SetKeyName(511, "zone.png");
            this.image_list.Images.SetKeyName(512, "zone_money.png");
            this.image_list.Images.SetKeyName(513, "zones.png");
            this.image_list.Images.SetKeyName(514, "tutorials_opengl_01.png");
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.cms.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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


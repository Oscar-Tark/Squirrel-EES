namespace Scorpion
{
    partial class Obj_vw
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Objects");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("References");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Memory", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Network Connections : 0");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Network Connections", new System.Windows.Forms.TreeNode[] {
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Count");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Cells Pools: N.A.");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("OneDB", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Functions");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("References");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Functions", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("SHS");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Forms");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("References");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Winforms", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("IEE", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode5,
            treeNode8,
            treeNode11,
            treeNode12,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Engines", new System.Windows.Forms.TreeNode[] {
            treeNode16});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Obj_vw));
            this.tv = new System.Windows.Forms.TreeView();
            this.il = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bkk_ld = new System.ComponentModel.BackgroundWorker();
            this.check_changes = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv
            // 
            this.tv.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.ForeColor = System.Drawing.Color.Black;
            this.tv.FullRowSelect = true;
            this.tv.ImageIndex = 0;
            this.tv.ImageList = this.il;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            treeNode1.ImageIndex = 78;
            treeNode1.Name = "Node16";
            treeNode1.Text = "Objects";
            treeNode2.ImageIndex = 389;
            treeNode2.Name = "NodeRef";
            treeNode2.Text = "References";
            treeNode3.Name = "Node12";
            treeNode3.Text = "Memory";
            treeNode4.ImageIndex = 472;
            treeNode4.Name = "AMCS";
            treeNode4.Text = "Network Connections : 0";
            treeNode5.ImageIndex = 149;
            treeNode5.Name = "Node14";
            treeNode5.Text = "Network Connections";
            treeNode6.ImageIndex = 429;
            treeNode6.Name = "Node3";
            treeNode6.Text = "Count";
            treeNode7.ImageIndex = 513;
            treeNode7.Name = "odbcll";
            treeNode7.Text = "Cells Pools: N.A.";
            treeNode8.ImageIndex = 142;
            treeNode8.Name = "Node15";
            treeNode8.Text = "OneDB";
            treeNode9.ImageIndex = 429;
            treeNode9.Name = "fnc_cnt";
            treeNode9.Text = "Functions";
            treeNode10.ImageIndex = 429;
            treeNode10.Name = "rf_fnc_cnt";
            treeNode10.Text = "References";
            treeNode11.ImageIndex = 470;
            treeNode11.Name = "Node0";
            treeNode11.Text = "Functions";
            treeNode12.ImageIndex = 259;
            treeNode12.Name = "shs";
            treeNode12.Text = "SHS";
            treeNode13.ImageIndex = 13;
            treeNode13.Name = "wff";
            treeNode13.Text = "Forms";
            treeNode14.ImageIndex = 389;
            treeNode14.Name = "wfref";
            treeNode14.Text = "References";
            treeNode15.ImageIndex = 1;
            treeNode15.Name = "wf";
            treeNode15.Text = "Winforms";
            treeNode16.ImageIndex = 49;
            treeNode16.Name = "Node5";
            treeNode16.Text = "IEE";
            treeNode17.ImageIndex = 106;
            treeNode17.Name = "Node4";
            treeNode17.Text = "Engines";
            this.tv.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17});
            this.tv.SelectedImageIndex = 2;
            this.tv.ShowLines = false;
            this.tv.ShowNodeToolTips = true;
            this.tv.Size = new System.Drawing.Size(665, 367);
            this.tv.TabIndex = 8;
            // 
            // il
            // 
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.TransparentColor = System.Drawing.Color.Transparent;
            this.il.Images.SetKeyName(0, "brick.png");
            this.il.Images.SetKeyName(1, "application_form.png");
            this.il.Images.SetKeyName(2, "accept.png");
            this.il.Images.SetKeyName(3, "add.png");
            this.il.Images.SetKeyName(4, "alarm.png");
            this.il.Images.SetKeyName(5, "anchor.png");
            this.il.Images.SetKeyName(6, "application.png");
            this.il.Images.SetKeyName(7, "application_add.png");
            this.il.Images.SetKeyName(8, "application_cascade.png");
            this.il.Images.SetKeyName(9, "application_delete.png");
            this.il.Images.SetKeyName(10, "application_double.png");
            this.il.Images.SetKeyName(11, "application_edit.png");
            this.il.Images.SetKeyName(12, "application_error.png");
            this.il.Images.SetKeyName(13, "application_form.png");
            this.il.Images.SetKeyName(14, "application_get.png");
            this.il.Images.SetKeyName(15, "application_go.png");
            this.il.Images.SetKeyName(16, "application_home.png");
            this.il.Images.SetKeyName(17, "application_key.png");
            this.il.Images.SetKeyName(18, "application_lightning.png");
            this.il.Images.SetKeyName(19, "application_link.png");
            this.il.Images.SetKeyName(20, "application_osx.png");
            this.il.Images.SetKeyName(21, "application_osx_terminal.png");
            this.il.Images.SetKeyName(22, "application_put.png");
            this.il.Images.SetKeyName(23, "application_side_boxes.png");
            this.il.Images.SetKeyName(24, "application_side_contract.png");
            this.il.Images.SetKeyName(25, "application_side_expand.png");
            this.il.Images.SetKeyName(26, "application_side_list.png");
            this.il.Images.SetKeyName(27, "application_side_tree.png");
            this.il.Images.SetKeyName(28, "application_split.png");
            this.il.Images.SetKeyName(29, "application_tile_horizontal.png");
            this.il.Images.SetKeyName(30, "application_tile_vertical.png");
            this.il.Images.SetKeyName(31, "application_view_columns.png");
            this.il.Images.SetKeyName(32, "application_view_detail.png");
            this.il.Images.SetKeyName(33, "application_view_gallery.png");
            this.il.Images.SetKeyName(34, "application_view_icons.png");
            this.il.Images.SetKeyName(35, "application_view_list.png");
            this.il.Images.SetKeyName(36, "application_view_tile.png");
            this.il.Images.SetKeyName(37, "application_view_xp.png");
            this.il.Images.SetKeyName(38, "application_view_xp_terminal.png");
            this.il.Images.SetKeyName(39, "application2.png");
            this.il.Images.SetKeyName(40, "arrow_branch.png");
            this.il.Images.SetKeyName(41, "arrow_divide.png");
            this.il.Images.SetKeyName(42, "arrow_in.png");
            this.il.Images.SetKeyName(43, "arrow_inout.png");
            this.il.Images.SetKeyName(44, "arrow_join.png");
            this.il.Images.SetKeyName(45, "arrow_left.png");
            this.il.Images.SetKeyName(46, "arrow_merge.png");
            this.il.Images.SetKeyName(47, "arrow_out.png");
            this.il.Images.SetKeyName(48, "arrow_redo.png");
            this.il.Images.SetKeyName(49, "arrow_refresh.png");
            this.il.Images.SetKeyName(50, "arrow_right.png");
            this.il.Images.SetKeyName(51, "arrow_undo.png");
            this.il.Images.SetKeyName(52, "asterisk_orange.png");
            this.il.Images.SetKeyName(53, "attach.png");
            this.il.Images.SetKeyName(54, "attach_2.png");
            this.il.Images.SetKeyName(55, "award_star_gold.png");
            this.il.Images.SetKeyName(56, "bandaid.png");
            this.il.Images.SetKeyName(57, "basket.png");
            this.il.Images.SetKeyName(58, "bell.png");
            this.il.Images.SetKeyName(59, "bin_closed.png");
            this.il.Images.SetKeyName(60, "blog.png");
            this.il.Images.SetKeyName(61, "blueprint.png");
            this.il.Images.SetKeyName(62, "blueprint_horizontal.png");
            this.il.Images.SetKeyName(63, "bluetooth.png");
            this.il.Images.SetKeyName(64, "bomb.png");
            this.il.Images.SetKeyName(65, "book.png");
            this.il.Images.SetKeyName(66, "book_addresses.png");
            this.il.Images.SetKeyName(67, "book_next.png");
            this.il.Images.SetKeyName(68, "book_open.png");
            this.il.Images.SetKeyName(69, "book_previous.png");
            this.il.Images.SetKeyName(70, "bookmark.png");
            this.il.Images.SetKeyName(71, "bookmark_book.png");
            this.il.Images.SetKeyName(72, "bookmark_book_open.png");
            this.il.Images.SetKeyName(73, "bookmark_document.png");
            this.il.Images.SetKeyName(74, "bookmark_folder.png");
            this.il.Images.SetKeyName(75, "books.png");
            this.il.Images.SetKeyName(76, "box.png");
            this.il.Images.SetKeyName(77, "brick.png");
            this.il.Images.SetKeyName(78, "bricks.png");
            this.il.Images.SetKeyName(79, "briefcase.png");
            this.il.Images.SetKeyName(80, "bug.png");
            this.il.Images.SetKeyName(81, "buildings.png");
            this.il.Images.SetKeyName(82, "bullet_add_1.png");
            this.il.Images.SetKeyName(83, "bullet_add_2.png");
            this.il.Images.SetKeyName(84, "bullet_key.png");
            this.il.Images.SetKeyName(85, "cake.png");
            this.il.Images.SetKeyName(86, "calculator.png");
            this.il.Images.SetKeyName(87, "calendar_1.png");
            this.il.Images.SetKeyName(88, "calendar_2.png");
            this.il.Images.SetKeyName(89, "camera.png");
            this.il.Images.SetKeyName(90, "cancel.png");
            this.il.Images.SetKeyName(91, "car.png");
            this.il.Images.SetKeyName(92, "cart.png");
            this.il.Images.SetKeyName(93, "cd.png");
            this.il.Images.SetKeyName(94, "chart_bar.png");
            this.il.Images.SetKeyName(95, "chart_curve.png");
            this.il.Images.SetKeyName(96, "chart_line.png");
            this.il.Images.SetKeyName(97, "chart_organisation.png");
            this.il.Images.SetKeyName(98, "chart_pie.png");
            this.il.Images.SetKeyName(99, "clipboard_paste_image.png");
            this.il.Images.SetKeyName(100, "clipboard_sign.png");
            this.il.Images.SetKeyName(101, "clipboard_text.png");
            this.il.Images.SetKeyName(102, "clock.png");
            this.il.Images.SetKeyName(103, "cog.png");
            this.il.Images.SetKeyName(104, "coins.png");
            this.il.Images.SetKeyName(105, "color_swatch_1.png");
            this.il.Images.SetKeyName(106, "color_swatch_2.png");
            this.il.Images.SetKeyName(107, "comment.png");
            this.il.Images.SetKeyName(108, "compass.png");
            this.il.Images.SetKeyName(109, "compress.png");
            this.il.Images.SetKeyName(110, "computer.png");
            this.il.Images.SetKeyName(111, "connect.png");
            this.il.Images.SetKeyName(112, "contrast.png");
            this.il.Images.SetKeyName(113, "control_eject.png");
            this.il.Images.SetKeyName(114, "control_end.png");
            this.il.Images.SetKeyName(115, "control_equalizer.png");
            this.il.Images.SetKeyName(116, "control_fastforward.png");
            this.il.Images.SetKeyName(117, "control_pause.png");
            this.il.Images.SetKeyName(118, "control_play.png");
            this.il.Images.SetKeyName(119, "control_repeat.png");
            this.il.Images.SetKeyName(120, "control_rewind.png");
            this.il.Images.SetKeyName(121, "control_start.png");
            this.il.Images.SetKeyName(122, "control_stop.png");
            this.il.Images.SetKeyName(123, "control_wheel.png");
            this.il.Images.SetKeyName(124, "counter.png");
            this.il.Images.SetKeyName(125, "counter_count.png");
            this.il.Images.SetKeyName(126, "counter_count_up.png");
            this.il.Images.SetKeyName(127, "counter_reset.png");
            this.il.Images.SetKeyName(128, "counter_stop.png");
            this.il.Images.SetKeyName(129, "cross.png");
            this.il.Images.SetKeyName(130, "cross_octagon.png");
            this.il.Images.SetKeyName(131, "cross_octagon_fram.png");
            this.il.Images.SetKeyName(132, "cross_shield.png");
            this.il.Images.SetKeyName(133, "cross_shield_2.png");
            this.il.Images.SetKeyName(134, "crown.png");
            this.il.Images.SetKeyName(135, "crown_bronze.png");
            this.il.Images.SetKeyName(136, "crown_silver.png");
            this.il.Images.SetKeyName(137, "css.png");
            this.il.Images.SetKeyName(138, "cursor.png");
            this.il.Images.SetKeyName(139, "cut.png");
            this.il.Images.SetKeyName(140, "dashboard.png");
            this.il.Images.SetKeyName(141, "data.png");
            this.il.Images.SetKeyName(142, "database.png");
            this.il.Images.SetKeyName(143, "databases.png");
            this.il.Images.SetKeyName(144, "delete.png");
            this.il.Images.SetKeyName(145, "delivery.png");
            this.il.Images.SetKeyName(146, "desktop.png");
            this.il.Images.SetKeyName(147, "desktop_empty.png");
            this.il.Images.SetKeyName(148, "direction.png");
            this.il.Images.SetKeyName(149, "disconnect.png");
            this.il.Images.SetKeyName(150, "disk.png");
            this.il.Images.SetKeyName(151, "doc_access.png");
            this.il.Images.SetKeyName(152, "doc_break.png");
            this.il.Images.SetKeyName(153, "doc_convert.png");
            this.il.Images.SetKeyName(154, "doc_excel_csv.png");
            this.il.Images.SetKeyName(155, "doc_excel_table.png");
            this.il.Images.SetKeyName(156, "doc_film.png");
            this.il.Images.SetKeyName(157, "doc_illustrator.png");
            this.il.Images.SetKeyName(158, "doc_music.png");
            this.il.Images.SetKeyName(159, "doc_music_playlist.png");
            this.il.Images.SetKeyName(160, "doc_offlice.png");
            this.il.Images.SetKeyName(161, "doc_page.png");
            this.il.Images.SetKeyName(162, "doc_page_previous.png");
            this.il.Images.SetKeyName(163, "doc_pdf.png");
            this.il.Images.SetKeyName(164, "doc_photoshop.png");
            this.il.Images.SetKeyName(165, "doc_resize.png");
            this.il.Images.SetKeyName(166, "doc_resize_actual.png");
            this.il.Images.SetKeyName(167, "doc_shred.png");
            this.il.Images.SetKeyName(168, "doc_stand.png");
            this.il.Images.SetKeyName(169, "doc_table.png");
            this.il.Images.SetKeyName(170, "doc_tag.png");
            this.il.Images.SetKeyName(171, "doc_text_image.png");
            this.il.Images.SetKeyName(172, "door.png");
            this.il.Images.SetKeyName(173, "door_in.png");
            this.il.Images.SetKeyName(174, "drawer.png");
            this.il.Images.SetKeyName(175, "drink.png");
            this.il.Images.SetKeyName(176, "drink_empty.png");
            this.il.Images.SetKeyName(177, "drive.png");
            this.il.Images.SetKeyName(178, "drive_burn.png");
            this.il.Images.SetKeyName(179, "drive_cd.png");
            this.il.Images.SetKeyName(180, "drive_cd_empty.png");
            this.il.Images.SetKeyName(181, "drive_delete.png");
            this.il.Images.SetKeyName(182, "drive_disk.png");
            this.il.Images.SetKeyName(183, "drive_error.png");
            this.il.Images.SetKeyName(184, "drive_go.png");
            this.il.Images.SetKeyName(185, "drive_link.png");
            this.il.Images.SetKeyName(186, "drive_network.png");
            this.il.Images.SetKeyName(187, "drive_rename.png");
            this.il.Images.SetKeyName(188, "dvd.png");
            this.il.Images.SetKeyName(189, "email.png");
            this.il.Images.SetKeyName(190, "email_open.png");
            this.il.Images.SetKeyName(191, "email_open_image.png");
            this.il.Images.SetKeyName(192, "emoticon_evilgrin.png");
            this.il.Images.SetKeyName(193, "emoticon_grin.png");
            this.il.Images.SetKeyName(194, "emoticon_happy.png");
            this.il.Images.SetKeyName(195, "emoticon_smile.png");
            this.il.Images.SetKeyName(196, "emoticon_surprised.png");
            this.il.Images.SetKeyName(197, "emoticon_tongue.png");
            this.il.Images.SetKeyName(198, "emoticon_unhappy.png");
            this.il.Images.SetKeyName(199, "emoticon_waii.png");
            this.il.Images.SetKeyName(200, "emoticon_wink.png");
            this.il.Images.SetKeyName(201, "envelope.png");
            this.il.Images.SetKeyName(202, "envelope_2.png");
            this.il.Images.SetKeyName(203, "error.png");
            this.il.Images.SetKeyName(204, "exclamation.png");
            this.il.Images.SetKeyName(205, "exclamation_octagon_fram.png");
            this.il.Images.SetKeyName(206, "eye.png");
            this.il.Images.SetKeyName(207, "feed.png");
            this.il.Images.SetKeyName(208, "feed_ballon.png");
            this.il.Images.SetKeyName(209, "feed_document.png");
            this.il.Images.SetKeyName(210, "female.png");
            this.il.Images.SetKeyName(211, "film.png");
            this.il.Images.SetKeyName(212, "films.png");
            this.il.Images.SetKeyName(213, "find.png");
            this.il.Images.SetKeyName(214, "flag_blue.png");
            this.il.Images.SetKeyName(215, "folder.png");
            this.il.Images.SetKeyName(216, "font.png");
            this.il.Images.SetKeyName(217, "funnel.png");
            this.il.Images.SetKeyName(218, "grid.png");
            this.il.Images.SetKeyName(219, "grid_dot.png");
            this.il.Images.SetKeyName(220, "group.png");
            this.il.Images.SetKeyName(221, "hammer.png");
            this.il.Images.SetKeyName(222, "hammer_screwdriver.png");
            this.il.Images.SetKeyName(223, "hand.png");
            this.il.Images.SetKeyName(224, "hand_point.png");
            this.il.Images.SetKeyName(225, "heart.png");
            this.il.Images.SetKeyName(226, "heart_break.png");
            this.il.Images.SetKeyName(227, "heart_empty.png");
            this.il.Images.SetKeyName(228, "heart_half.png");
            this.il.Images.SetKeyName(229, "heart_small.png");
            this.il.Images.SetKeyName(230, "help.png");
            this.il.Images.SetKeyName(231, "highlighter.png");
            this.il.Images.SetKeyName(232, "house.png");
            this.il.Images.SetKeyName(233, "html.png");
            this.il.Images.SetKeyName(234, "image_1.png");
            this.il.Images.SetKeyName(235, "image_2.png");
            this.il.Images.SetKeyName(236, "images.png");
            this.il.Images.SetKeyName(237, "inbox.png");
            this.il.Images.SetKeyName(238, "ipod.png");
            this.il.Images.SetKeyName(239, "ipod_cast.png");
            this.il.Images.SetKeyName(240, "joystick.png");
            this.il.Images.SetKeyName(241, "key.png");
            this.il.Images.SetKeyName(242, "keyboard.png");
            this.il.Images.SetKeyName(243, "layer_treansparent.png");
            this.il.Images.SetKeyName(244, "layers.png");
            this.il.Images.SetKeyName(245, "layout.png");
            this.il.Images.SetKeyName(246, "layout_header_footer_3.png");
            this.il.Images.SetKeyName(247, "layout_header_footer_3_mix.png");
            this.il.Images.SetKeyName(248, "layout_join.png");
            this.il.Images.SetKeyName(249, "layout_join_vertical.png");
            this.il.Images.SetKeyName(250, "layout_select.png");
            this.il.Images.SetKeyName(251, "layout_select_content.png");
            this.il.Images.SetKeyName(252, "layout_select_footer.png");
            this.il.Images.SetKeyName(253, "layout_select_sidebar.png");
            this.il.Images.SetKeyName(254, "layout_split.png");
            this.il.Images.SetKeyName(255, "layout_split_vertical.png");
            this.il.Images.SetKeyName(256, "lifebuoy.png");
            this.il.Images.SetKeyName(257, "lightbulb.png");
            this.il.Images.SetKeyName(258, "lightbulb_off.png");
            this.il.Images.SetKeyName(259, "lightning.png");
            this.il.Images.SetKeyName(260, "link.png");
            this.il.Images.SetKeyName(261, "link_break.png");
            this.il.Images.SetKeyName(262, "lock.png");
            this.il.Images.SetKeyName(263, "lock_unlock.png");
            this.il.Images.SetKeyName(264, "magnet.png");
            this.il.Images.SetKeyName(265, "magnifier.png");
            this.il.Images.SetKeyName(266, "magnifier_zoom_in.png");
            this.il.Images.SetKeyName(267, "male.png");
            this.il.Images.SetKeyName(268, "map.png");
            this.il.Images.SetKeyName(269, "marker.png");
            this.il.Images.SetKeyName(270, "medal_bronze_1.png");
            this.il.Images.SetKeyName(271, "medal_gold_1.png");
            this.il.Images.SetKeyName(272, "media_player_small_blue.png");
            this.il.Images.SetKeyName(273, "microphone.png");
            this.il.Images.SetKeyName(274, "mobile_phone.png");
            this.il.Images.SetKeyName(275, "money.png");
            this.il.Images.SetKeyName(276, "money_dollar.png");
            this.il.Images.SetKeyName(277, "money_euro.png");
            this.il.Images.SetKeyName(278, "money_pound.png");
            this.il.Images.SetKeyName(279, "money_yen.png");
            this.il.Images.SetKeyName(280, "monitor.png");
            this.il.Images.SetKeyName(281, "mouse.png");
            this.il.Images.SetKeyName(282, "music.png");
            this.il.Images.SetKeyName(283, "music_beam.png");
            this.il.Images.SetKeyName(284, "neutral.png");
            this.il.Images.SetKeyName(285, "new.png");
            this.il.Images.SetKeyName(286, "newspaper.png");
            this.il.Images.SetKeyName(287, "note.png");
            this.il.Images.SetKeyName(288, "nuclear.png");
            this.il.Images.SetKeyName(289, "package.png");
            this.il.Images.SetKeyName(290, "page.png");
            this.il.Images.SetKeyName(291, "page_2.png");
            this.il.Images.SetKeyName(292, "page_2_copy.png");
            this.il.Images.SetKeyName(293, "page_code.png");
            this.il.Images.SetKeyName(294, "page_copy.png");
            this.il.Images.SetKeyName(295, "page_excel.png");
            this.il.Images.SetKeyName(296, "page_lightning.png");
            this.il.Images.SetKeyName(297, "page_paste.png");
            this.il.Images.SetKeyName(298, "page_red.png");
            this.il.Images.SetKeyName(299, "page_refresh.png");
            this.il.Images.SetKeyName(300, "page_save.png");
            this.il.Images.SetKeyName(301, "page_white_cplusplus.png");
            this.il.Images.SetKeyName(302, "page_white_csharp.png");
            this.il.Images.SetKeyName(303, "page_white_cup.png");
            this.il.Images.SetKeyName(304, "page_white_database.png");
            this.il.Images.SetKeyName(305, "page_white_delete.png");
            this.il.Images.SetKeyName(306, "page_white_dvd.png");
            this.il.Images.SetKeyName(307, "page_white_edit.png");
            this.il.Images.SetKeyName(308, "page_white_error.png");
            this.il.Images.SetKeyName(309, "page_white_excel.png");
            this.il.Images.SetKeyName(310, "page_white_find.png");
            this.il.Images.SetKeyName(311, "page_white_flash.png");
            this.il.Images.SetKeyName(312, "page_white_freehand.png");
            this.il.Images.SetKeyName(313, "page_white_gear.png");
            this.il.Images.SetKeyName(314, "page_white_get.png");
            this.il.Images.SetKeyName(315, "page_white_paintbrush.png");
            this.il.Images.SetKeyName(316, "page_white_paste.png");
            this.il.Images.SetKeyName(317, "page_white_php.png");
            this.il.Images.SetKeyName(318, "page_white_picture.png");
            this.il.Images.SetKeyName(319, "page_white_powerpoint.png");
            this.il.Images.SetKeyName(320, "page_white_put.png");
            this.il.Images.SetKeyName(321, "page_white_ruby.png");
            this.il.Images.SetKeyName(322, "page_white_stack.png");
            this.il.Images.SetKeyName(323, "page_white_star.png");
            this.il.Images.SetKeyName(324, "page_white_swoosh.png");
            this.il.Images.SetKeyName(325, "page_white_text.png");
            this.il.Images.SetKeyName(326, "page_white_text_width.png");
            this.il.Images.SetKeyName(327, "page_white_tux.png");
            this.il.Images.SetKeyName(328, "page_white_vector.png");
            this.il.Images.SetKeyName(329, "page_white_visualstudio.png");
            this.il.Images.SetKeyName(330, "page_white_width.png");
            this.il.Images.SetKeyName(331, "page_white_word.png");
            this.il.Images.SetKeyName(332, "page_white_world.png");
            this.il.Images.SetKeyName(333, "page_white_wrench.png");
            this.il.Images.SetKeyName(334, "page_white_zip.png");
            this.il.Images.SetKeyName(335, "paintbrush.png");
            this.il.Images.SetKeyName(336, "paintcan.png");
            this.il.Images.SetKeyName(337, "palette.png");
            this.il.Images.SetKeyName(338, "paper_bag.png");
            this.il.Images.SetKeyName(339, "paste_plain.png");
            this.il.Images.SetKeyName(340, "paste_word.png");
            this.il.Images.SetKeyName(341, "pencil.png");
            this.il.Images.SetKeyName(342, "photo.png");
            this.il.Images.SetKeyName(343, "photo_album.png");
            this.il.Images.SetKeyName(344, "photos.png");
            this.il.Images.SetKeyName(345, "piano.png");
            this.il.Images.SetKeyName(346, "picture.png");
            this.il.Images.SetKeyName(347, "pilcrow.png");
            this.il.Images.SetKeyName(348, "pill.png");
            this.il.Images.SetKeyName(349, "pin.png");
            this.il.Images.SetKeyName(350, "pipette.png");
            this.il.Images.SetKeyName(351, "plaing_card.png");
            this.il.Images.SetKeyName(352, "plug.png");
            this.il.Images.SetKeyName(353, "plugin.png");
            this.il.Images.SetKeyName(354, "printer.png");
            this.il.Images.SetKeyName(355, "projection_screen.png");
            this.il.Images.SetKeyName(356, "projection_screen_present.png");
            this.il.Images.SetKeyName(357, "rainbow.png");
            this.il.Images.SetKeyName(358, "report.png");
            this.il.Images.SetKeyName(359, "rocket.png");
            this.il.Images.SetKeyName(360, "rosette.png");
            this.il.Images.SetKeyName(361, "rss.png");
            this.il.Images.SetKeyName(362, "ruby.png");
            this.il.Images.SetKeyName(363, "ruler_1.png");
            this.il.Images.SetKeyName(364, "ruler_2.png");
            this.il.Images.SetKeyName(365, "ruler_crop.png");
            this.il.Images.SetKeyName(366, "ruler_triangle.png");
            this.il.Images.SetKeyName(367, "safe.png");
            this.il.Images.SetKeyName(368, "script.png");
            this.il.Images.SetKeyName(369, "selection.png");
            this.il.Images.SetKeyName(370, "selection_select.png");
            this.il.Images.SetKeyName(371, "server.png");
            this.il.Images.SetKeyName(372, "shading.png");
            this.il.Images.SetKeyName(373, "shape_aling_bottom.png");
            this.il.Images.SetKeyName(374, "shape_aling_center.png");
            this.il.Images.SetKeyName(375, "shape_aling_left.png");
            this.il.Images.SetKeyName(376, "shape_aling_middle.png");
            this.il.Images.SetKeyName(377, "shape_aling_right.png");
            this.il.Images.SetKeyName(378, "shape_aling_top.png");
            this.il.Images.SetKeyName(379, "shape_flip_horizontal.png");
            this.il.Images.SetKeyName(380, "shape_flip_vertical.png");
            this.il.Images.SetKeyName(381, "shape_group.png");
            this.il.Images.SetKeyName(382, "shape_handles.png");
            this.il.Images.SetKeyName(383, "shape_move_back.png");
            this.il.Images.SetKeyName(384, "shape_move_backwards.png");
            this.il.Images.SetKeyName(385, "shape_move_forwards.png");
            this.il.Images.SetKeyName(386, "shape_move_front.png");
            this.il.Images.SetKeyName(387, "shape_square.png");
            this.il.Images.SetKeyName(388, "shield.png");
            this.il.Images.SetKeyName(389, "sitemap.png");
            this.il.Images.SetKeyName(390, "slide.png");
            this.il.Images.SetKeyName(391, "slides.png");
            this.il.Images.SetKeyName(392, "slides_stack.png");
            this.il.Images.SetKeyName(393, "smiley_confuse.png");
            this.il.Images.SetKeyName(394, "smiley_cool.png");
            this.il.Images.SetKeyName(395, "smiley_cry.png");
            this.il.Images.SetKeyName(396, "smiley_fat.png");
            this.il.Images.SetKeyName(397, "smiley_mad.png");
            this.il.Images.SetKeyName(398, "smiley_red.png");
            this.il.Images.SetKeyName(399, "smiley_roll.png");
            this.il.Images.SetKeyName(400, "smiley_slim.png");
            this.il.Images.SetKeyName(401, "smiley_yell.png");
            this.il.Images.SetKeyName(402, "socket.png");
            this.il.Images.SetKeyName(403, "sockets.png");
            this.il.Images.SetKeyName(404, "sort.png");
            this.il.Images.SetKeyName(405, "sort_alphabet.png");
            this.il.Images.SetKeyName(406, "sort_date.png");
            this.il.Images.SetKeyName(407, "sort_disable.png");
            this.il.Images.SetKeyName(408, "sort_number.png");
            this.il.Images.SetKeyName(409, "sort_price.png");
            this.il.Images.SetKeyName(410, "sort_quantity.png");
            this.il.Images.SetKeyName(411, "sort_rating.png");
            this.il.Images.SetKeyName(412, "sound.png");
            this.il.Images.SetKeyName(413, "sound_note.png");
            this.il.Images.SetKeyName(414, "spellcheck.png");
            this.il.Images.SetKeyName(415, "sport_8ball.png");
            this.il.Images.SetKeyName(416, "sport_basketball.png");
            this.il.Images.SetKeyName(417, "sport_football.png");
            this.il.Images.SetKeyName(418, "sport_golf.png");
            this.il.Images.SetKeyName(419, "sport_raquet.png");
            this.il.Images.SetKeyName(420, "sport_shuttlecock.png");
            this.il.Images.SetKeyName(421, "sport_soccer.png");
            this.il.Images.SetKeyName(422, "sport_tennis.png");
            this.il.Images.SetKeyName(423, "stamp.png");
            this.il.Images.SetKeyName(424, "star_1.png");
            this.il.Images.SetKeyName(425, "star_2.png");
            this.il.Images.SetKeyName(426, "status_online.png");
            this.il.Images.SetKeyName(427, "stop.png");
            this.il.Images.SetKeyName(428, "style.png");
            this.il.Images.SetKeyName(429, "sum.png");
            this.il.Images.SetKeyName(430, "sum_2.png");
            this.il.Images.SetKeyName(431, "switch.png");
            this.il.Images.SetKeyName(432, "tab.png");
            this.il.Images.SetKeyName(433, "table.png");
            this.il.Images.SetKeyName(434, "tag.png");
            this.il.Images.SetKeyName(435, "tag_blue.png");
            this.il.Images.SetKeyName(436, "target.png");
            this.il.Images.SetKeyName(437, "telephone.png");
            this.il.Images.SetKeyName(438, "television.png");
            this.il.Images.SetKeyName(439, "text_align_center.png");
            this.il.Images.SetKeyName(440, "text_align_justify.png");
            this.il.Images.SetKeyName(441, "text_align_left.png");
            this.il.Images.SetKeyName(442, "text_align_right.png");
            this.il.Images.SetKeyName(443, "text_allcaps.png");
            this.il.Images.SetKeyName(444, "text_bold.png");
            this.il.Images.SetKeyName(445, "text_columns.png");
            this.il.Images.SetKeyName(446, "text_dropcaps.png");
            this.il.Images.SetKeyName(447, "text_heading_1.png");
            this.il.Images.SetKeyName(448, "text_horizontalrule.png");
            this.il.Images.SetKeyName(449, "text_indent.png");
            this.il.Images.SetKeyName(450, "text_indent_remove.png");
            this.il.Images.SetKeyName(451, "text_italic.png");
            this.il.Images.SetKeyName(452, "text_kerning.png");
            this.il.Images.SetKeyName(453, "text_letter_omega.png");
            this.il.Images.SetKeyName(454, "text_letterspacing.png");
            this.il.Images.SetKeyName(455, "text_linespacing.png");
            this.il.Images.SetKeyName(456, "text_list_bullets.png");
            this.il.Images.SetKeyName(457, "text_list_numbers.png");
            this.il.Images.SetKeyName(458, "text_lowercase.png");
            this.il.Images.SetKeyName(459, "text_padding_bottom.png");
            this.il.Images.SetKeyName(460, "text_padding_left.png");
            this.il.Images.SetKeyName(461, "text_padding_right.png");
            this.il.Images.SetKeyName(462, "text_padding_top.png");
            this.il.Images.SetKeyName(463, "text_signature.png");
            this.il.Images.SetKeyName(464, "text_smallcaps.png");
            this.il.Images.SetKeyName(465, "text_strikethrough.png");
            this.il.Images.SetKeyName(466, "text_subscript.png");
            this.il.Images.SetKeyName(467, "textfield.png");
            this.il.Images.SetKeyName(468, "textfield_rename.png");
            this.il.Images.SetKeyName(469, "ticket.png");
            this.il.Images.SetKeyName(470, "timeline_marker.png");
            this.il.Images.SetKeyName(471, "traffic.png");
            this.il.Images.SetKeyName(472, "transmit.png");
            this.il.Images.SetKeyName(473, "trophy.png");
            this.il.Images.SetKeyName(474, "trophy_bronze.png");
            this.il.Images.SetKeyName(475, "trophy_silver.png");
            this.il.Images.SetKeyName(476, "ui_combo_box.png");
            this.il.Images.SetKeyName(477, "ui_saccordion.png");
            this.il.Images.SetKeyName(478, "ui_slider_1.png");
            this.il.Images.SetKeyName(479, "ui_slider_2.png");
            this.il.Images.SetKeyName(480, "ui_tab_bottom.png");
            this.il.Images.SetKeyName(481, "ui_tab_content.png");
            this.il.Images.SetKeyName(482, "ui_tab_disable.png");
            this.il.Images.SetKeyName(483, "ui_tab_side.png");
            this.il.Images.SetKeyName(484, "ui_text_field_hidden.png");
            this.il.Images.SetKeyName(485, "ui_text_field_password.png");
            this.il.Images.SetKeyName(486, "umbrella.png");
            this.il.Images.SetKeyName(487, "user.png");
            this.il.Images.SetKeyName(488, "user_black_female.png");
            this.il.Images.SetKeyName(489, "user_business.png");
            this.il.Images.SetKeyName(490, "user_business_boss.png");
            this.il.Images.SetKeyName(491, "user_female.png");
            this.il.Images.SetKeyName(492, "user_silhouette.png");
            this.il.Images.SetKeyName(493, "user_thief.png");
            this.il.Images.SetKeyName(494, "user_thief_baldie.png");
            this.il.Images.SetKeyName(495, "vcard.png");
            this.il.Images.SetKeyName(496, "vector.png");
            this.il.Images.SetKeyName(497, "wait.png");
            this.il.Images.SetKeyName(498, "wall.png");
            this.il.Images.SetKeyName(499, "wall_break.png");
            this.il.Images.SetKeyName(500, "wall_brick.png");
            this.il.Images.SetKeyName(501, "wall_disable.png");
            this.il.Images.SetKeyName(502, "wand.png");
            this.il.Images.SetKeyName(503, "weather_clouds.png");
            this.il.Images.SetKeyName(504, "weather_cloudy.png");
            this.il.Images.SetKeyName(505, "weather_lightning.png");
            this.il.Images.SetKeyName(506, "weather_rain.png");
            this.il.Images.SetKeyName(507, "weather_snow.png");
            this.il.Images.SetKeyName(508, "weather_sun.png");
            this.il.Images.SetKeyName(509, "webcam.png");
            this.il.Images.SetKeyName(510, "world.png");
            this.il.Images.SetKeyName(511, "zone.png");
            this.il.Images.SetKeyName(512, "zone_money.png");
            this.il.Images.SetKeyName(513, "zones.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 367);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(665, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton1.Image = global::Scorpion.Properties.Resources.arrow_redo;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton1.Text = "Refresh";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator1.Visible = false;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.msToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(82, 22);
            this.toolStripDropDownButton1.Text = "Refresh rate";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.sToolStripMenuItem.Text = "5s";
            // 
            // msToolStripMenuItem
            // 
            this.msToolStripMenuItem.Name = "msToolStripMenuItem";
            this.msToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.msToolStripMenuItem.Text = "100ms";
            this.msToolStripMenuItem.Click += new System.EventHandler(this.MsToolStripMenuItem_Click);
            // 
            // bkk_ld
            // 
            this.bkk_ld.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkk_ld_DoWork);
            // 
            // check_changes
            // 
            this.check_changes.Interval = 1000;
            this.check_changes.Tick += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Obj_vw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 392);
            this.Controls.Add(this.tv);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Obj_vw";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Scorpion - Overview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Obj_vw_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.ComponentModel.BackgroundWorker bkk_ld;
        private System.Windows.Forms.ImageList il;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.Timer check_changes;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
    }
}
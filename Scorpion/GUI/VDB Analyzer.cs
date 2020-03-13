using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scorpion.GUI
{
    public partial class VDB_Analyzer : UserControl
    {
        Scorpion.Form1 Do_on;
        public VDB_Analyzer(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.Dock = DockStyle.Fill;
            tv_db.ImageList = Do_on.image_list;
            tv_db.SelectedImageIndex = 142;
            ofd.InitialDirectory = Do_on.AL_DIRECTORIES[0].ToString();
            sfd.InitialDirectory = Do_on.AL_DIRECTORIES[0].ToString();
            load_list(0);
            //dgv.DataSource

            foreach (string s in Do_on.AL_SECTIONS)
            { toolStripComboBox1.Items.Add(s); }

            return;
        }

        private void load_list(int Type_)
        {
            if (Type_ == 0)
            {
                tv_db.Nodes.Clear();
                foreach (string s in Do_on.AL_TBLE_REF)
                {
                    tv_db.Nodes.Add(s, s, 142);
                }
            }

            return;
        }

        private void show_file_open()
        {
            ofd.ShowDialog();
            return;
        }


        private void tv_select(object sender, TreeViewEventArgs e)
        {
            if (sender.Equals(tv_db))
            {
                load_listing();
            }
        }

        public void load_listing()
        {

        }

        private void DropDown(object sender, EventArgs e)
        {
            if (sender.Equals(toolStripComboBox1))
            {
                toolStripComboBox1.Items.Clear();
                toolStripComboBox1.Items.AddRange(Do_on.AL_SECTIONS.ToArray());
            }
            return;
        }

        private void Selectedindexchanged(object sender, EventArgs e)
        {
            lv.Items.Clear();
            if (sender.Equals(toolStripComboBox1))
            {
                int n = 0;
                foreach (object o in ((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(tv_db.SelectedNode.Text)])[Do_on.AL_SECTIONS.IndexOf(toolStripComboBox1.Text)]))
                {
                    try
                    {
                        lv.Items.Add(((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(tv_db.SelectedNode.Text)])[Do_on.AL_SECTIONS.IndexOf(toolStripComboBox1.Text)]))[0])[n])[2].ToString());
                        lv.Items[n].SubItems.Add(((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(tv_db.SelectedNode.Text)])[Do_on.AL_SECTIONS.IndexOf(toolStripComboBox1.Text)]))[0])[n])[1].ToString());
                    }
                    catch { }
                    n++;
                }
            }
            return;
        }

        private void labeledit(object sender, LabelEditEventArgs e)
        {
            Do_on.readr.lib_SCR.scorpioniee("mem.set(*" + tv_db.SelectedNode.Text + "@" + toolStripComboBox1.Text + "@" + lv.Items[e.Item].SubItems[1].Text + ",*\"" + lv.Items[e.Item].Text + "\")");
            MessageBox.Show("mem.set(*" + tv_db.SelectedNode.Text + "@" + toolStripComboBox1.Text + "@" + lv.Items[e.Item].SubItems[1].Text + ",*\"" + lv.Items[e.Item].Text + "\")");
        }
    }
}
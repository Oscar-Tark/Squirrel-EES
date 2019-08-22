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

        private void Actions(object sender, EventArgs e)
        {
            if (((ToolStripItem)sender).Tag.ToString() == "open")
            { show_file_open(); }
            else if (((ToolStripItem)sender).Tag.ToString() == "about")
            {
                Do_on.readr.access_library(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[2] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[4].ToString());
            }
            else if (((ToolStripItem)sender).Tag.ToString() == "refreshdb")
            { load_list(0); }
            else if (((ToolStripItem)sender).Tag.ToString() == "save")
            {
                if (DialogResult.Yes == MessageBox.Show("Save the Database '" + tv_db.SelectedNode.Text + "' to disk?, this will overwrite any previous versions.", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    Do_on.readr.access_library(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[97] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1].ToString() + Do_on.AL_ACC[5].ToString() + tv_db.SelectedNode.Text + Do_on.AL_ACC[5].ToString() + Do_on.AL_ACC[4].ToString());
                }
            }
            else if (((ToolStripItem)sender).Tag.ToString() == "unmount")
            {
                Do_on.readr.access_library(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[100] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1].ToString() + Do_on.AL_ACC[5].ToString() + tv_db.SelectedNode.Text + Do_on.AL_ACC[5].ToString() + Do_on.AL_ACC[4].ToString());
                load_list(0);
            }
            else if (((ToolStripItem)sender).Tag.ToString() == "cui")
            {
                Do_on.readr.access_library(Do_on.AL_ACC_SUP[3] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[113] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1].ToString() + Do_on.AL_ACC[4].ToString());
                load_list(0);
            }
            else if (((ToolStripItem)sender).Tag.ToString() == "create")
            {
                sfd.ShowDialog();
                load_list(0);
            }
            return;
        }

        private void show_file_open()
        {
            ofd.ShowDialog();
            return;
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {
            if (new System.IO.FileInfo(ofd.FileName).Directory.ToString() != Do_on.AL_DIRECTORIES[4].ToString())
            {
                MessageBox.Show(Do_on.AL_MESSAGE[4].ToString(), Do_on.AL_MESSAGE[5].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (string s in ofd.SafeFileNames)
            {
                Do_on.readr.access_library(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[94] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1].ToString() + Do_on.AL_ACC[5].ToString() + s.Replace(".vdb", "") + Do_on.AL_ACC[5].ToString() + Do_on.AL_ACC[4].ToString());
            }
            load_list(0);

            return;
        }

        private void sfd_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.FileInfo fnf = new System.IO.FileInfo(sfd.FileName);
            Do_on.readr.access_library(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[95] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1].ToString() + Do_on.AL_ACC[5].ToString() + fnf.Name.Replace(".vdb", "") + Do_on.AL_ACC[5].ToString() + ",*true" + Do_on.AL_ACC[4].ToString());

            Do_on.readr.access_library(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[94] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1].ToString() + Do_on.AL_ACC[5].ToString() + fnf.Name.Replace(".vdb", "") + Do_on.AL_ACC[5].ToString() + Do_on.AL_ACC[4].ToString());
            load_list(0);
            fnf = null;
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
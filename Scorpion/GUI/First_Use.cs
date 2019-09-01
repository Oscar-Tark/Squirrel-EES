using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class First_Use : Form
    {
        Scorpion.Form1 fm1;
        public First_Use(Scorpion.Form1 Main_)
        {
            fm1 = Main_;
            InitializeComponent();
            this.Icon = Icon.FromHandle(Properties.Resources.bt.GetHicon());
            this.Show();
            return;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            fm1.readr.lib_SCR.scorpioniee(fm1.AL_ACC_SUP[3] + fm1.AL_ACC[2].ToString() + fm1.AL_FNC_SCRP[3] + fm1.AL_ACC[3].ToString() + fm1.AL_ACC[4].ToString());
        }
        
        public static bool continue_ = false; int ndx = 0;
        private void event_handler(object sender, EventArgs e)
        {
            if (sender == pwd)
            {
                fm1.SHA = pwd.Text;
                fm1.Load_init_db();
                fm1.start_after_services();

                fm1.start_CUI();
                fm1.readr.access_library("");
                this.Close();
            }

            return;
        }

        private void First_Use_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if(pwd.Text == "")
            {
                button1.Enabled = false;
                button4.Enabled = false;
            }
            else { button1.Enabled = true; button4.Enabled = true; }
        }
    }
}

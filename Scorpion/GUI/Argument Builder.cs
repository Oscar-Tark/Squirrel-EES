using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class Argument_Builder : Form
    {
        Form1 Do_on;
        public Argument_Builder(Form1 fm1)
        {
            Do_on = fm1;
            InitializeComponent();
            this.Show();
            return;
        }

        private void cbx_DropDown(object sender, EventArgs e)
        {
            cbx.DataSource = Do_on.AL_SHS_APP_REF;

            e = null;
            sender = null;
            return;
        }

        private void action(object sender, EventArgs e)
        {
            if (sender.Equals(get))
            {
                //arg_to_gui(Do_on.readr.lib_SCR.get_arguments(cbx.Text, cbx_arg.Text));
            }

            e = null;
            sender = null;
            return;
        }

        private void arg_to_gui(ArrayList al)
        {
            foreach (string s in al)
            {
                dataGridView1.Rows.Add(s);
            }

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }
    }
}

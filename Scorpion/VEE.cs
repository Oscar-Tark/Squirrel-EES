using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Scorpion
{
    public partial class VEE : Form
    {
        public Form1 Do_on;
        public VEE(Form1 fm1)
        {
            Do_on = fm1;
            InitializeComponent();
            init();
            this.Show();
            return;
        }

        private void init()
        { 
            this.Controls.Add(new OpenTK.GLControl());
            this.Controls[0].Dock = DockStyle.Fill;
            return;
        }

        private void VEE_Load(object sender, EventArgs e)
        {

            return;
        }
    }
}

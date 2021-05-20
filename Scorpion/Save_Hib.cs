using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Windows.Forms;

namespace Scorpion
{
    [RunInstaller(true)]
    public partial class Save_Hib : System.Configuration.Install.Installer
    {
        public Save_Hib()
        {
            InitializeComponent();
            if (DialogResult.Yes == MessageBox.Show("Save Hibernation Files to another location?"))
            {

            }
        }
    }
}

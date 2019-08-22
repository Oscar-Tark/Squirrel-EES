
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Drawing;
using System.ComponentModel;

//Static Library
namespace Scorpion
{
    public partial class Librarian
    {
        private delegate void del_do(object Scorp_Line);
        private delegate void del_do_rf_str(ref string Scorp_Line, ref ArrayList al);
        private delegate void del_do_rf_obj(object Scorp_Line);

        public bool pointered = false;
        string Temp_str = "";
        public TreeNode tn_tmp;
        public string Item_type;
        public bool cuimode = false;
        Scorpion_IDE.Special_TextView cui_stview = new Scorpion_IDE.Special_TextView();
        bool Continue_SCR = true;
        public Form1 Do_on;
        reader rdd;

        public NotifyIcon nfy_tmp;
        public bool resend = false;

        public string strng_tmp;
        public ArrayList AL_Ref_EVT = new ArrayList();


        public object FC;
    }
}
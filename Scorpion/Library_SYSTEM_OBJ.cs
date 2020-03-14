using System.Collections;
using System.Windows.Forms;

//Static Library
namespace Scorpion
{
    public partial class Librarian
    {
        private delegate void del_do(object Scorp_Line);
        private delegate void del_do_rf_str(ref string Scorp_Line, ref ArrayList al);
        private delegate void del_do_rf_obj(object Scorp_Line);

        public bool pointered = false;
        public TreeNode tn_tmp;
        public string Item_type;
        public bool cuimode = false;
        Scorpion_IDE.Special_TextView cui_stview = new Scorpion_IDE.Special_TextView();
        public Form1 Do_on;
        int limit = 25;

        public NotifyIcon nfy_tmp;
        public bool resend = false;

        public string strng_tmp;
        public ArrayList AL_Ref_EVT = new ArrayList();


        public object FC;
    }
}
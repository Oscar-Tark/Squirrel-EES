using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        protected void thread_(object pack)
        {
            new Thread(new ParameterizedThreadStart(delegate_), 0).Start(pack);
            return;
        }

        protected void delegate_(object pack)
        {
            //MethodInfo mi = ((MethodInfo)((ArrayList)pack)[0]);
            //private delegate void deleg(object pack_);
        //deleg del = new deleg(mi.;
            //del.invoke(pack);
            return;
        }

        /*
         public void do_(string Scorp_Line)
        {
            try
            {
                Thread th_doo = new Thread(new ParameterizedThreadStart(do2_));
                th_doo.IsBackground = true;
                th_doo.Start((object)Scorp_Line);
            }
            catch { }
        }

        private void do2_(object Scorp_Line)
        {
            try
            {
                del_do ddo = new del_do(work_);
                ddo.Invoke(Scorp_Line);

                ddo = null;
            }
            catch (Exception erty) { MessageBox.Show(erty.Message + " (" + erty.StackTrace + ")"); }
        }*/
    }
}

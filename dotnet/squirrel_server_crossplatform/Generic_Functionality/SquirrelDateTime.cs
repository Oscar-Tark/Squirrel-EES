

using System;
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        /*
        Scorpion standard is: yyyy_MM_dd
        */

        public string date(ref string Scorp_Line_Exec, ref ArrayList objects) =>
            //*RETURNABLE<<::
            var_create_return(DateTime.Today.ToString("yyyy_MM_dd"), true);

        public string dateyesterday(ref string Scorp_Line_Exec, ref ArrayList objects) =>
            //*RETURNABLE<<::
            var_create_return(DateTime.Today.AddDays(-1).ToString("yyyy_MM_dd"), true);

        public string datetomorrow(ref string Scorp_Line_Exec, ref ArrayList objects) =>
            //*RETURNABLE<<::
            var_create_return(DateTime.Today.AddDays(+1).ToString("yyyy_MM_dd"), true);
        
    }
}

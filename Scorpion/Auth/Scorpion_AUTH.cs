using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        public void permissions(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //This function allows a user to view their execution permissions
            //::
            Do_on.mmsec.write_permissions();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
        }
    }
}
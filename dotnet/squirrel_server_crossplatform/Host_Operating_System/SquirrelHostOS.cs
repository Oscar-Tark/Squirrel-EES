

using System;
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        public string getuserfolder(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //RETURNABLE::
            return var_create_return(Environment.GetFolderPath(Environment.SpecialFolder.Personal), true);
        }
    }
}
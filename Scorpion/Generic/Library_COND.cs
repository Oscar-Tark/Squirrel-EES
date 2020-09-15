using System;
using System.Collections;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        public string isequal(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*arg1, *arg2
            string returnable = "";
            if (var_get((string)objects[0]) == var_get((string)objects[1]))
                returnable = "true";
            else
                returnable = "false";

            //clean
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }

        private void compare_greater(string Line_of_Code, int index)
        {
            ArrayList al = cut_variables(ref Line_of_Code);

            if (Convert.ToDouble(var_get(al[0].ToString())) > Convert.ToDouble(var_get(al[1].ToString())))
            {
                Do_on.AL_CURR_VAR[index] = "true";
            }
            else
            { Do_on.AL_CURR_VAR[index] = "false"; }

            //clean
            Line_of_Code = null;
            var_arraylist_dispose(ref al);

            return;
        }

        private void compare_less(string Line_of_Code, int index)
        {
            ArrayList al = cut_variables(ref Line_of_Code);
            if (Convert.ToDouble(var_get(al[0].ToString())) < Convert.ToDouble(var_get(al[0].ToString())))
            {
                Do_on.AL_CURR_VAR[index] = "true";
            }
            else
            { Do_on.AL_CURR_VAR[index] = "false"; }

            //clean
            Line_of_Code = null;
            var_arraylist_dispose(ref al);

            return;
        }
    }
}
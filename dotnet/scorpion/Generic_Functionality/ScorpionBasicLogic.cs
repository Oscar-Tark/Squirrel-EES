using System;
using System.Collections;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        public string isequal(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*compare, *with
            string returnable = null;
            ScorpionConsoleReadWrite.ConsoleWrite.writeDebug(var_get((string)objects[0]).ToString() + var_get((string)objects[1]).ToString());
            if ((string)var_get((string)objects[0]) == (string)var_get((string)objects[1]))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;
            //clean
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }

        public string isnotequal(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*compare, *with
            string returnable = "";
            if ((string)var_get((string)objects[0]) != (string)var_get((string)objects[1]))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;
            //clean
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }

        public string isgreater(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*compare, *with
            string returnable = null;
            if (Convert.ToInt32(var_get(objects[0])) > Convert.ToInt32(var_get(objects[1])))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }

        public string isgreaterequal(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*compare, *with
            string returnable = null;
            if (Convert.ToInt32(var_get(objects[0])) >= Convert.ToInt32(var_get(objects[1])))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }

        public string isless(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*compare, *with
            string returnable = null;
            if (Convert.ToInt32(var_get(objects[0])) < Convert.ToInt32(var_get(objects[1])))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }

        public string islessequal(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*compare, *with
            string returnable = null;
            if (Convert.ToInt32(var_get(objects[0])) <= Convert.ToInt32(var_get(objects[1])))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }
    }
}
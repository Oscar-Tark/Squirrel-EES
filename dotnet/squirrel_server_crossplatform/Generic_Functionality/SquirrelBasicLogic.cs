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
            ScorpionConsoleReadWrite.ConsoleWrite.writeDebug(MemoryCore.varGet((string)objects[0]).ToString() + MemoryCore.varGet((string)objects[1]).ToString());
            if ((string)MemoryCore.varGet((string)objects[0]) == (string)MemoryCore.varGet((string)objects[1]))
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
            if ((string)MemoryCore.varGet((string)objects[0]) != (string)MemoryCore.varGet((string)objects[1]))
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
            if (Convert.ToInt32(MemoryCore.varGet(objects[0])) > Convert.ToInt32(MemoryCore.varGet(objects[1])))
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
            if (Convert.ToInt32(MemoryCore.varGet(objects[0])) >= Convert.ToInt32(MemoryCore.varGet(objects[1])))
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
            if (Convert.ToInt32(MemoryCore.varGet(objects[0])) < Convert.ToInt32(MemoryCore.varGet(objects[1])))
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
            if (Convert.ToInt32(MemoryCore.varGet(objects[0])) <= Convert.ToInt32(MemoryCore.varGet(objects[1])))
                returnable = Types.S_Yes;
            else
                returnable = Types.S_No;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable, true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {


    }
}

namespace Scorpion.File_operations
{
    public class Fileopr
    {
        Scorpion.Form1 Do_on;
        public Fileopr(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void filesave()
        {
            
        }

        public void from_file_tobin(ref string Scorp_Line)
        {
            //(*path, *var);
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(ref Scorp_Line);
            Do_on.write_to_cui(al[0].ToString());
            Do_on.readr.lib_SCR.var_set(al[1].ToString(), File.ReadAllBytes(Do_on.readr.lib_SCR.var_cut_str_symbol(Do_on.readr.lib_SCR.var_cut_symbol(al[0].ToString()))));
            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            Do_on.write_to_cui(Do_on.AL_MESSAGE[8].ToString());

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            Scorp_Line = null;

            return;
        }

        public void reformat()
        {
            //GET PDF FILE & Convert to text, store images in seperate section
            //original file is kept in the main object
        }

        public void create_meta()
        {
            //Allows you to create meta objects from the meta viewer
        }

        public void extract_information(ref string Scorp_Line)
        {
            //(*var)

        }
    }
}

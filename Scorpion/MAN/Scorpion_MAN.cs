using System.Collections;
using System.IO;

namespace Scorpion
{ 
    public partial class Librarian
    { 
        public void manual(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show a manual file
            //::*page
            //STD path for all MAN is ./man
            if (objects.Count != 0)
                showman((string)var_get(objects[0]));
            else
                showmandir();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }

    public partial class Librarian
    {
        private string manpath = "./man/";
        private string man_extension = ".man";

        private void showman(string function)
        {
            //Show a manual file
            if (File.Exists(manpath + function + man_extension))
            {                                                       
                Do_on.write_to_cui("MAN/READERS MANUAL Entry for '" + function + "':\n******************************************************\nFUNCTION: [" + function + "]\n");
                Do_on.write_to_cui(read_file(manpath + function + man_extension));
            }
            else
                Do_on.write_warning("No man entry exists for '" + function + "'");
            function = null;
            return;
        }

        private void showmandir()
        {
            //Enumerate and show the contents of the man pages directory
            Do_on.write_to_cui("Available man pages:");
            if (Directory.Exists(manpath))
            {
                DirectoryInfo df = new DirectoryInfo(manpath);
                foreach (FileInfo man_fnf in df.EnumerateFiles("*.man"))
                    Do_on.write_special(man_fnf.Name.Replace(".man", ""));
            }
            return;
        }
    }
}
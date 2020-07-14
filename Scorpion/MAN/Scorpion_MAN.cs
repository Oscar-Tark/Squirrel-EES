using System.Collections;

namespace Scorpion
{ 
    public partial class Librarian
    { 
        public void man(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //STD path for all MAN is ./man
            showman((string)var_get(objects[0]));
            return;
        }
    }

    public partial class Librarian
    {
        private string manpath = "./man/";
        private string man_extension = ".man";

        public void showman(string function)
        {
            if (System.IO.File.Exists(manpath + function + man_extension))
            {                                                       
                Do_on.write_to_cui("MAN/READERS MANUAL Entry for '" + function + "':\n******************************************************\nFUNCTION: [" + function + "]\n");
                Do_on.write_to_cui(read_file(manpath + function + man_extension));
            }
            else
                Do_on.write_to_cui("No man entry exists for '" + function + "'");
            function = null;
            return;
        }
    }
}
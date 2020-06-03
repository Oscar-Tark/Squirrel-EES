using System.IO;
using System.Collections;
using System;

namespace Scorpion
{
    public partial class Librarian
    {
        public void runscript(string Scorp_Line_Exec, ArrayList objects)
        {
            //All fules must be UTF8
            //(*Path)
            string line;
            FileStream fd = new FileStream((string)var_get((string)objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            while((line = sr.ReadLine()) != null)
            {
                scorpion_exec((object)line);
            }
            sr.Close();
            fd.Close();

            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}

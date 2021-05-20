/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections;
using System.IO;

namespace Scorpion
{
    partial class Librarian
    {
        public string readfile(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable<<::*path
            FileStream fs = new FileStream((string)var_get(objects[0]), FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string RSA = sr.ReadToEnd();
            fs.Flush();
            sr.Close();
            fs.Close();

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(ref RSA, true);
        }

        public object readfilebinary(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable<<::*path
            byte[] byter = File.ReadAllBytes((string)var_get(objects[0]));

            //claen
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);

            return var_create_return(ref byter, true);
        }

        public void writefile(string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *append<true>, *text
            FileMode fm = FileMode.OpenOrCreate;
            if ((string)var_get(objects[1]) == Do_on.types.S_Yes)
                fm = FileMode.Append;

            FileStream fs = new FileStream((string)var_get(objects[0]), fm, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(var_get(objects[2]));
            sw.Flush();
            fs.Flush();
            sw.Close();
            fs.Close();

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void writefilebinary(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *byteobject
            byte[] bytew = Do_on.crypto.To_Byte(var_get(objects[1]));
            File.WriteAllBytes((string)var_get(objects[0]), bytew);

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref bytew);

            return;
        }

        public string getfilefolder(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Get a files folder
            //RETURNABLE<<::*file
            FileInfo fnf = new FileInfo((string)var_get(objects[0]));

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);

            return var_create_return(fnf.DirectoryName, true);
        }
    }

    partial class Librarian
    {
        private string read_file(string path)
        {
            FileStream fd = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            string s_read = sr.ReadToEnd();
            sr.Close();
            fd.Close();

            path = null;
            return s_read;
        }
    }
}
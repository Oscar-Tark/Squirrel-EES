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

using System.IO;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        //Add to Session dependent handler
        public void runscriptcondition(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All files must be UTF8
            //::*if, *is, *Path, *result_must_be_equal
            if ((string)var_get(objects[3]) == Do_on.types.S_Yes)
            {
                if ((string)var_get(objects[0]) == (string)var_get(objects[1]))
                {
                    string line;
                    FileStream fd = new FileStream((string)var_get((string)objects[2]), FileMode.Open);
                    StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                    while ((line = sr.ReadLine()) != null)
                        scorpion_exec((object)line);
                    sr.Close();
                    fd.Close();

                    var_dispose_internal(ref line);
                }
            }
            else if ((string)var_get(objects[3]) == Do_on.types.S_Yes)
            {
                if ((string)var_get(objects[0]) != (string)var_get(objects[1]))
                {
                    string line;
                    FileStream fd = new FileStream((string)var_get((string)objects[2]), FileMode.Open);
                    StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                    while ((line = sr.ReadLine()) != null)
                        scorpion_exec((object)line);
                    sr.Close();
                    fd.Close();

                    var_dispose_internal(ref line);
                }
            }
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void runscript(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All files must be UTF8
            //::*path, *hasconfig:bool
            string line;

            //Run Script
            FileStream fd = new FileStream((string)var_get((string)objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            while ((line = sr.ReadLine()) != null)
                scorpion_exec((object)line);
            sr.Close();
            fd.Close();

            Scorp_Line_Exec = null;
            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }

        /*public void runscriptencrypted(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All fules must be UTF8
            //::*path, *key
            string line = "";
            string encry_ = read_file((string)var_get(objects[0]));

            //byte[] b = Do_on.crypto.decrypt(Do_on.crypto.To_Byte(encry_), (string)var_get(objects[1]));

            //object decry_ = Do_on.crypto.To_Object(new MemoryStream(b));

            /*while ((line = sr.ReadLine()) != null)
            {
                scorpion_exec((object)line);
            }

            Scorp_Line_Exec = null;
            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void scriptencrypt(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *key
            FileStream fd = new FileStream((string)var_get(objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);

           // byte[] b = Do_on.crypto.encrypt(sr.ReadToEnd(), (string)var_get(objects[1]));

            //File.WriteAllBytes((string)var_get(objects[0]), b);

            sr.Close();
            fd.Close();

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }*/
    }
}

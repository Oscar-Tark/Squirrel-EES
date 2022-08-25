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
        //Add to Session dependent handler and encryption dependent
        public void scriptruncondition(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All files must be UTF8
            //::*if, *is, *Path, *result_must_be_equal

            if ((string)MemoryCore.varGet(objects[3]) == Types.S_Yes)
            {
                if ((string)MemoryCore.varGet(objects[0]) == (string)MemoryCore.varGet(objects[1]))
                {
                    string line;
                    FileStream fd = new FileStream((string)MemoryCore.varGet((string)objects[2]), FileMode.Open);
                    StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                    while ((line = sr.ReadLine()) != null)
                        scorpionExec(line);

                    fd.Flush();
                    sr.Close();
                    fd.Close();

                    var_dispose_internal(ref line);
                }
            }
            else if ((string)MemoryCore.varGet(objects[3]) == Types.S_Yes)
            {
                if ((string)MemoryCore.varGet(objects[0]) != (string)MemoryCore.varGet(objects[1]))
                {
                    string line;
                    FileStream fd = new FileStream((string)MemoryCore.varGet((string)objects[2]), FileMode.Open);
                    StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                    while ((line = sr.ReadLine()) != null)
                        scorpionExec(line);

                    fd.Flush();
                    sr.Close();
                    fd.Close();

                    var_dispose_internal(ref line);
                }
            }
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void scriptrun(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All files must be UTF8
            //::*path

            string line;
            //Run Script
            FileStream fd = new FileStream((string)MemoryCore.varGet((string)objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            while ((line = sr.ReadLine()) != null)
                scorpionExec(line);

            fd.Flush();
            sr.Close();
            fd.Close();
            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void scriptrundb(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *subtag==*tag
            //::*path, *subtag

            //Query the database
            //string db, object data, string tag, string subtag, short OPCODE
            ArrayList result = Types.HANDLE.vds.doDBSelectiveNoThread((string)MemoryCore.varGet(objects[0]), Types.S_NULL, (string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[1]), Types.HANDLE.vds.OPCODE_GET);
            string script = default;

            foreach(object o_script in result)
            {
                script = Enginefunctions.replaceEscape((string)o_script);
                ScorpionConsoleReadWrite.ConsoleWrite.writeSpecial("Running XMLDB script: ", script);
                scorpioniee(script);
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
        }
    }
}
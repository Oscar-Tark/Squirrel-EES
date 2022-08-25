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
            //Read a UTF8 Encoded file from disk
            //*returnable<<::*path
            FileStream fs = new FileStream((string)MemoryCore.varGet(objects[0]), FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, true);
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
            //Read a raw binary file
            //*returnable<<::*path
            byte[] byter = File.ReadAllBytes((string)MemoryCore.varGet(objects[0]));

            //claen
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(ref byter, true);
        }

        public void writefile(string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Write a UTF8 Encoded file to disk
            //::*path, *append<true>, *text
            FileMode fm = FileMode.OpenOrCreate;
            if ((string)MemoryCore.varGet(objects[1]) == Types.S_Yes)
                fm = FileMode.Append;

            FileStream fs = new FileStream((string)MemoryCore.varGet(objects[0]), fm, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            sw.Write(MemoryCore.varGet(objects[2]));
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
            //Write a raw binary file to disk
            //::*path, *byteobject
            byte[] bytew = Types.HANDLE.crypto.To_Byte(MemoryCore.varGet(objects[1]));
            File.WriteAllBytes((string)MemoryCore.varGet(objects[0]), bytew);

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
            FileInfo fnf = new FileInfo((string)MemoryCore.varGet(objects[0]));

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(fnf.DirectoryName, true);
        }

        public object listfolders(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Get directories within folder
            //RETURNABLE<<::*path, *[tolist:BOOLEAN]
            DirectoryInfo df = new DirectoryInfo((string)MemoryCore.varGet(objects[0]));
            ArrayList al_ret = new ArrayList();
            foreach (DirectoryInfo dirs in df.GetDirectories())
            {
                if ((string)MemoryCore.varGet(objects[1]) == Types.S_Yes)
                    al_ret.Add(dirs.Name);
                else
                    ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(dirs.Name);
            }

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(ref al_ret);
        }

        public object listfiles(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Get files within a folder
            //RETURNABLE<<::*path, *[tolist:BOOLEAN]
            DirectoryInfo df = new DirectoryInfo((string)MemoryCore.varGet(objects[0]));
            ArrayList al_ret = new ArrayList();
            foreach (FileInfo fils in df.GetFiles())
            {
                if ((string)MemoryCore.varGet(objects[1]) == Types.S_Yes)
                    al_ret.Add(fils.Name);
                else
                    ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(fils.Name);
            }

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(ref al_ret);
        }

        public void createDirectory(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Directory.CreateDirectory((string)MemoryCore.varGet(objects[0]));

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }
    }
}
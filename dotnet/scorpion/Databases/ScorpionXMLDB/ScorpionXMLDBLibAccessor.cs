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
        //Legacy DB
        /*BASIC DB FUNCTIONS FOR INTERNAL DB
         * DB's in scorpion store by name&value  
         * Basic structure
         * {data}
         * {tag}
         * {meta}
         * {type}        
        */
        //LEGACY
        public void dbcreate(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*File_Name_w_path, *pwd
            //{val}
            string name = (string)var_get(objects[0]);
            string password = (string)var_get(objects[1]);

            Do_on.vds.createDB(name, false, password);
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Created Data File(to disk) : " + name + "]");
            name = null;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void dbopen(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *path, *password
            string name = (string)var_get(objects[0]);
            string path = (string)var_get(objects[1]);
            string password = (string)var_get(objects[2]);
            
            Do_on.vds.loadDB(path, name, password);
            var_arraylist_dispose(ref objects);
            name = null;
            Scorp_Line_Exec = null;
            return;
        }

        public void dbclose(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            Do_on.vds.closeDB((string)var_get(objects[0]));
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Database [" + var_get(objects[0]) + "] closed");
            return;
        }

        public void dbsave(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *password
            string name = (string)var_get(objects[0]);
            string password = (string)var_get(objects[1]);
            
            Do_on.vds.saveDB(name, password);
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Database [" + name + "] saved");
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            name = null;
            return;
        }

        public void dbreload(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*dbname, *password
            string name = (string)var_get(objects[0]);
            string password = (string)var_get(objects[1]);

            Do_on.vds.reloadDB(name, password);

            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public void listdbs(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.vds.ViewDBS();
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void ld(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            listdbs(ref Scorp_Line_Exec, ref objects);
            return;
        }

        public void dbset(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *data, *tag|or *null, *subtag|or *null
            if (Do_on.vds.setDB((string)var_get(objects[0]), var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3])))
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Value set to database");
            else
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("Unable to set value to database");
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object dbgetall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*path/name of database
            ArrayList result = Do_on.vds.getDBAllNoThread((string)var_get(objects[0]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref result);
        }

        public object dbget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //This function gets values from a Scorpion.Database by Datavalue, Tag or Subtag
            //return<<::*path, *data|OR NULL, *tag|OR NULL, *subtag|OR NULL
            /*
             * path = path/name of database
             * data = the specific value and related data you may want to get | If data is *'' elements are searched by tag and subtag
             * tag  = the specific cluster tag such as "Row1" that clusters multiple data slots together. If this is *'' then values are searched by *data
             * subtag = the specific subtag such as 'Name' that can be extracted from a tag cluster. If this is *'' then values are searched by tag, if tag is also empty then values are searched by *data
            */

            ArrayList result = Do_on.vds.doDBSelectiveNoThread((string)var_get(objects[0]), var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), Do_on.vds.OPCODE_GET);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref result);
        }

        public void dbdelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //This function gets values from a Scorpion.Database by Datavalue, Tag or Subtag
            //::*path, *data|OR NULL, *tag|OR NULL, *subtag|OR NULL
            /*
             * path = path/name of database
             * data = the specific value and related data you may want to delete | If data is *'' elements are searched by tag and subtag for deletion
             * tag  = the specific cluster tag such as "Row1" that clusters multiple data slots together. If this is *'' then values are searched and deleted by *data
             * subtag = the specific subtag such as 'Name' that can be extracted for deletion from a tag cluster. If this is *'' then values are searched for deletion by tag, if tag is also empty then values are searched for deletion by *data
            */

            Do_on.vds.doDBSelectiveNoThread((string)var_get(objects[0]), var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), Do_on.vds.OPCODE_DELETE);
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void stress_test(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            FileStream fd = new FileStream("/home/ferret/Scorpion/Databases/stress.script", FileMode.OpenOrCreate);
            StreamWriter sr = new StreamWriter(fd);
            sr.WriteLine("var::*dbname, *result >> varset::*dbname, *path >> *dbname<<varconcatenate::*dbname, *'/Databases/stress.db' >> dbcreate::*dbname >> dbopen::*dbname");

            for (int i = 0; i < 15000; i++)
            {
                sr.WriteLine("dbset::* dbname, *'" + i + "', *'row" + i + "', *'age'");
                sr.WriteLine("dbset::* dbname, *'John" + i + "', *'row" + i + "', *'name'");
                sr.WriteLine("dbset::* dbname, *'Doe" + i + "', *'row" + i +"', *'lname'");
            }
            sr.WriteLine("dbsave::*dbname");
            sr.WriteLine("*result<<dbget::*dbname, *'Doe2578', *'row1', *'lname'");

            sr.Flush();
            fd.Flush();
            sr.Close();
            fd.Close();
            return;
        }
    }
}
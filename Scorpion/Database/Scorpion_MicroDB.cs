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
            //::*File_Name_w_path
            //{val}
            string name = (string)var_get(objects[0]);
            Do_on.vds.Create_DB(name, false);
            Do_on.write_to_cui("Created Data File(to disk) : " + name + "]");
            name = null;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void dbopen(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name, *path
            string name = (string)var_get(objects[0]);
            string path = (string)var_get(objects[1]);

            lock (Do_on.mem.AL_TBLE) lock (Do_on.mem.AL_TBLE_REF) lock (Do_on.mem.AL_TBLE_PATH)
                        Do_on.vds.Load_DB(path, name);
            var_arraylist_dispose(ref objects);
            name = null;
            Scorp_Line_Exec = null;
            return;
        }

        public void dbclose(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            lock(Do_on.mem.AL_TBLE) lock(Do_on.mem.AL_TBLE_REF) lock(Do_on.mem.AL_TBLE_PATH)
                {
                    Do_on.vds.Close_DB((string)var_get(objects[0]));
                }
            Do_on.write_to_cui("Database [" + var_get(objects[0]) + "] closed");
            return;
        }

        public void dbsave(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*name
            string name = (string)var_get(objects[0]);
            //Save without passphrase for now
            Do_on.vds.Save_DB(name, "");
            Do_on.write_success("Database [" + name + "] saved");
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            name = null;
            return;
        }

        public void listdbs(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.write_to_cui("Loaded databases:\n-------------------------\n");
            foreach (string s_name in Do_on.mem.AL_TBLE_REF)
                Do_on.write_to_cui("NAME: [" + s_name + "] CURRENT USED SLOT CAPACITY: [" + ((ArrayList)((ArrayList)Do_on.mem.AL_TBLE[Do_on.mem.AL_TBLE_REF.IndexOf(s_name)])[2]).Count + "] MAXIMUM SYSTEM SLOT CAPACITY: [" + ((ArrayList)((ArrayList)Do_on.mem.AL_TBLE[Do_on.mem.AL_TBLE_REF.IndexOf(s_name)])[2]).Capacity + "]");
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
            if (Do_on.vds.Data_setDB((string)var_get(objects[0]), var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3])))
                Do_on.write_to_cui("Value set to database");
            else
                Do_on.write_error("Unable to set value to database");
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object dbgetall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            ArrayList result = Do_on.vds.Data_getDB_all_no_thread((string)var_get(objects[0]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref result);
        }

        public object dbget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //This function gets values from a Scorpion.Database by Datavalue, Tag or Subtag
            //::*path, *data|OR NULL, *tag|OR NULL, *subtag|OR NULL
            /*
             * path = path/name of database
             * data = the specific value and related data you may want to get | If data is *'' elements are searched by tag and subtag
             * tag  = the specific cluster tag such as "Row1" that clusters multiple data slots together. If this is *'' then values are searched by *data
             * subtag = the specific subtag such as 'Name' that can be extracted from a tag cluster. If this is *'' then values are searched by tag, if tag is also empty then values are searched by *data
            */

            ArrayList result = Do_on.vds.Data_doDB_selective_no_thread((string)var_get(objects[0]), var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), Do_on.vds.OPCODE_GET);
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

            Do_on.vds.Data_doDB_selective_no_thread((string)var_get(objects[0]), var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), Do_on.vds.OPCODE_DELETE);
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

        public int dbcapacity(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            return 0;
        }
    }
}
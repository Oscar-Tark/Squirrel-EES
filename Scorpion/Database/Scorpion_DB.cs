/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020>  <Oscar Arjun Singh Tark>

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

using System;
using System.Collections;

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
            Do_on.vds.Create_DB(name);
            Do_on.write_to_cui("Created Data File(to disk) : " + name + "]");
            name = null;
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void dbopen(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*File_Name_w_path, *pwd
            string name = (string)var_get(objects[0]);
            if (!Do_on.mem.AL_TBLE_REF.Contains(name))
            {
                Do_on.mem.AL_TBLE.Add(Do_on.vds.Load_DB(name));
                Do_on.mem.AL_TBLE_REF.Add(name);
                Do_on.write_to_cui("Opened Database: [" + name + "]");
            }
            else { Do_on.write_to_cui("Database [" + name + "] already in memory"); }

            var_arraylist_dispose(ref objects);
            name = null;
            Scorp_Line_Exec = null;
            return;
        }

        public void dbclose(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path/name
            int ndx = Do_on.mem.AL_TBLE_REF.IndexOf(var_get(objects[0]));
            lock(Do_on.mem.AL_TBLE) lock(Do_on.mem.AL_TBLE_REF)
                {
                    //Do_on.AL_TBLE[ndx] = 0x00;
                    //Do_on.AL_TCP_REF[ndx] = 0x00;
                    Do_on.mem.AL_TBLE.RemoveAt(ndx);
                    Do_on.mem.AL_TCP_REF.RemoveAt(ndx);
                    trim_table_memory();
                }
            Do_on.write_to_cui("Database [" + var_get(objects[0]) + "] closed");
            return;
        }

        public void dbsave(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path/name, *pwd
            string name = (string)var_get(objects[0]);

            //Save without passphrase for now
            Do_on.vds.Save_DB(name, "");
            Do_on.write_to_cui("Database [" + name + "] saved");

            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            name = null;
            return;
        }

        public void listdbs(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            foreach (string s_name in Do_on.mem.AL_TBLE_REF)
                Do_on.write_to_cui(s_name);
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void dbset(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path/name, *value, *tag, *meta, *field_type
            if (Do_on.vds.Data_setDB((string)var_get(objects[0]), (object)var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), Convert.ToUInt16(var_get(objects[4]))))
                Do_on.write_success("Value set to database");
            else
                Do_on.write_error("Unable to set value to database");
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //QUERY STYLED
        public object dbget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *searchORfalse for all
            object[] elem = Do_on.vds.Data_getDB((string)var_get(objects[0]), (string)var_get(objects[1]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref elem);
        }

        private void trim_table_memory()
        {
            Do_on.mem.AL_TBLE.TrimToSize();
            Do_on.mem.AL_TBLE_REF.TrimToSize();
            return;
        }
    }
}
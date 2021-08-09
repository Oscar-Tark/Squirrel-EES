//DEPRECIATED TO SCORPION_MICRO_DB

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
using Scorpion_MongoDB_Library;

namespace Scorpion
{
    partial class Librarian
    {
        //Legacy DB
        /*BASIC DB FUNCTIONS FOR INTERNAL DB
         * DB's in scorpion store by name&value  
         * Basic structure
         * {ref}
         * {data}
         * {tags}
         * {meta}
        */
        //MONGODB
        public void mongodbstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*url, *port
            Mongodb.Mongodbstart((string)var_get(objects[0]), (string)var_get(objects[1]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public string mongodbgetall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //returnable<<::*db, *collection
            string JSON = null;
            JSON = Mongodb.Mongogetall((string)var_get(objects[0]), (string)var_get(objects[1]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return var_create_return(ref JSON, true);
        }

        public string mongodbgetfilter(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //returnable<<::*db, *collection, *filter
            string JSON = Mongodb.Mongogetspecific((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return var_create_return(ref JSON, true);
        }

        public void mongodbsetone(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Mongodb.Mongosetone((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }
    }
}
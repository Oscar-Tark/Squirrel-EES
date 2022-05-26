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

using System;
using System.IO;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        public void mysqlnew(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates a new generic data table
            //Table format: [id:int] [tag|path:string] [subtag|identifier(name, age...):string] [data:string]
            //::*connectionstringvar, *tablename
            //var::*con >> *con<<mysqlcreatestring::*'localhost', *'3306', *'scorpion_iee', *'root', *'' >> mysqlnew::*con, *'test'
            using(var mysql = new ScorpionMySql.ScorpionSql())
            {
                mysql.sqlfmtnew((string)var_get(objects[0]), (string)var_get(objects[1]));
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public string mysqlcreatestring(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //var::*con >> *con<<mysqlcreatestring::*'localhost', *'3306', *'scorpion_iee', *'root', *'' >> mysqltest::*con
            //*connectionstringvar<<::*'host', *'port', *'db', *'uname', *'password'
            //Creates a connection string to the specified variable

            string returnable = string.Format("server={0};port={1};userid={2};password={3};database={4}", (string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[3]), (string)var_get(objects[4]), (string)var_get(objects[2]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(returnable, true);
        }

        public object mysqlget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets data from a Mysql table
            //Table format: [id:int] [tag|path:string] [subtag|identifier(name, age...):string] [data:string]
            //*returnable<<*connectionstringvar, *table, *[path], *[identifier], *conditional_[data]_parameter
            //var::*con >> *con<<mysqlcreatestring::*'localhost', *'3306', *'scorpion_iee', *'root', *'' >> *temp<<mysqlget::*con, *'test', *'/test', *'name', *'', *'token'
            
            object returnable = Do_on.types.S_NULL;
            using(var mysql = new ScorpionMySql.ScorpionSql())
            {
                returnable = mysql.scfmtSqlGet((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), (string)var_get(objects[4]), (string)var_get(objects[5]));
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref returnable);
        }

        public void mysqlset(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Inserts data into a MySql table
            //Table format: [id:int] [tag|path:string] [subtag|identifier(name, age...):string] [data:string]
            //*returnable<<*connectionstringvar, *table, *[path], *[identifier], *conditional_[data]_parameter
            //var::*con >> *con<<mysqlcreatestring::*'localhost', *'3306', *'scorpion_iee', *'root', *'' >> mysqlset::*con, *'test', *'/test', *'last name', *'Doe', *'token'
            
            using(var mysql = new ScorpionMySql.ScorpionSql())
            {
                mysql.scfmtSqlSet((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), (string)var_get(objects[4]), (string)var_get(objects[5]));
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void mysqlupdate()
        {}

        public void mysqltest(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            using(var mysql = new ScorpionMySql.ScorpionSql())
            {
                mysql.test((string)var_get(objects[0]));
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}
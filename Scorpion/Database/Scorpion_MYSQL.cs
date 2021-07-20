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
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        public void mysql(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*ip_or_hostname, *uname, *password, *dbname
            Do_on.sql = new Scorpion_MYSQL((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void mysqlquery(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*Scorpion.Array<<::*query
            Do_on.sql.mysqlquery((string)var_get(objects[0]));
            Do_on.write_debug(var_get(objects[0]));
            return;
        }
    }

    public class Scorpion_MYSQL
    {
        private string ip_host, uname, pass, dbname;
        public Scorpion_MYSQL(string ip_host_, string uname_, string pass_, string dbname_)
        {
            ip_host = ip_host_;
            uname = uname_;
            pass = pass_;
            dbname = dbname_;
        }

        public void mysqlquery(string query_)
        {
            try
            {
                string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", ip_host, uname, pass, dbname);
                var conn = new MySql.Data.MySqlClient.MySqlConnection(dbConnectionString);
                conn.Open();
                sanitize(ref query_);
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(query_, conn);
                var reader = cmd.ExecuteReader();

            }
            catch(Exception e) { Console.WriteLine(e.Message); }

            /*while (reader.Read())
            {
                var someValue = reader["SomeColumnName"];

                // Do something with someValue
            }*/
        }

        private void sanitize(ref string query)
        { 
        
        }
    }
}

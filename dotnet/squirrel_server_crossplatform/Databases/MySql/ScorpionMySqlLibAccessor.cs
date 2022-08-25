

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
                mysql.sqlfmtnew((string)MemoryCore.varGet(objects[0]), (string)MemoryCore.varGet(objects[1]));
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

            string returnable = string.Format("server={0};port={1};userid={2};password={3};database={4}", (string)MemoryCore.varGet(objects[0]), (string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[3]), (string)MemoryCore.varGet(objects[4]), (string)MemoryCore.varGet(objects[2]));

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
            
            object returnable = Types.S_NULL;
            using(var mysql = new ScorpionMySql.ScorpionSql())
            {
                returnable = mysql.scfmtSqlGet((string)MemoryCore.varGet(objects[0]), (string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[2]), (string)MemoryCore.varGet(objects[3]), (string)MemoryCore.varGet(objects[4]), (string)MemoryCore.varGet(objects[5]));
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
                mysql.scfmtSqlSet((string)MemoryCore.varGet(objects[0]), (string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[2]), (string)MemoryCore.varGet(objects[3]), (string)MemoryCore.varGet(objects[4]), (string)MemoryCore.varGet(objects[5]));
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
                mysql.test((string)MemoryCore.varGet(objects[0]));
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}
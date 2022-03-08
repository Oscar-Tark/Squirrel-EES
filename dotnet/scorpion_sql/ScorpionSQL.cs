/*A class for storing and retrieving data from SQL
* SQL sructure
* [id:int] [path:string] [identifier(name, age...):string] [data:string]
*/

using System;
using System.Data;
using System.Collections;
using MySqlConnector;
using System.ComponentModel;

namespace ScorpionMySql
{
public class ScorpionSql:IDisposable
{
    public object scfmtSqlGet(string connection_string, string table, string path, string identifier, string data)
    {
        //Get data from MySql in the generic format: [id:int] [path:string] [identifier(name, age...):string] [data:string]

        //object returnable;
        //DataTable returnable_data = new DataTable();
        ArrayList returnable_data = new ArrayList();

        using (var connection = new MySqlConnection(connection_string))
        {
            try{
            connection.Open();
            using (var command = new MySqlCommand(string.Format("SELECT * FROM {0} WHERE path=@path AND identifier=@identifier AND data LIKE @data;", table), connection))
            {
                command.Parameters.AddWithValue("path", path);
                command.Parameters.AddWithValue("identifier", identifier);
                command.Parameters.AddWithValue("data", string.Format("%{0}%", data));

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(reader.HasRows)
                            Console.WriteLine("Data has been returned by MySql");
                        else
                            Console.WriteLine("No data has been returned by MySql");
                        returnable_data.Add(reader.GetString(3));
                    }
                }
            }
            }
            catch(System.Exception e){ System.Console.WriteLine(e.Message); }
        }
        return (object)returnable_data;
    }

    public void scfmtSqlSet()
    {
        return;
    }

    public void sqlnew(string connection_string, string table_name)
    {
        //Creates a new generic data table
        //[id:int] [path:string] [identifier(name, age...):string] [data:string]
        
        using (var connection = new MySqlConnection(connection_string))
        {
            connection.Open();
            using (var command = new MySqlCommand(string.Format("CREATE TABLE {0} (id INT NOT NULL AUTO_INCREMENT, path VARCHAR(128) NOT NULL, identifier VARCHAR(32) NOT NULL, data VARCHAR(2048) NULL, PRIMARY KEY (id))", table_name), connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch(System.Exception e){ System.Console.WriteLine(e.Message); }
            }
        }
        return;
    }

    //System functions
    private static string sqlSanitize()
    {
        return null;
    }

    public void test(string connection_string)
    {
            using (var connection = new MySqlConnection(connection_string))
            {
                try
                {
                connection.Open();
                using (var command = new MySqlCommand("SELECT json FROM module_users;", connection))
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
                connection.Close();        
                }
                catch(System.Exception e)
                { System.Console.WriteLine(e.Message); }
            }
            return;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        return;
    }
}
}
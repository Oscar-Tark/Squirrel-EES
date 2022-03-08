/*A class for storing and retrieving data from SQL
* SQL sructure
* [id:int] [path:string] [identifier(name, age...):string] [data:string]
*/
using MySqlConnector;
using System.ComponentModel;

namespace ScorpionMySql
{
public class ScorpionSql:IDisposable
{
    public object sqlGet(string connection_string, string table, string path, string data)
    {
        using (var connection = new MySqlConnection(connection_string))
        {
            connection.Open();
            using (var command = new MySqlCommand(string.Format("SELECT * FROM {0} WHERE path=@path AND data=@data;", table), connection))
            {
                command.Parameters.AddWithValue("path", path);
                command.Parameters.AddWithValue("data", data);
                using (var reader = command.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }
        }
        return null;
    }

    public void sqlSet()
    {

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
    }

    public void Dispose()
    {
    
    }
}
}
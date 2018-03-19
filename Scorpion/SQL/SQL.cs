using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Collections;

namespace Scorpion.SQL_lite
{
    public class SQL_lite
    {
        Form1 Do_on;
        string createTableQuery = @"CREATE TABLE IF NOT EXISTS [scobjects] (
                          [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                          [SCRIPTNAME] NVARCHAR(2048)  NULL,
                          [SCRIPT] NVARCHAR NULL
                          )";
        string insertTableQuery = @"INSERT INTO scobjects (SCRIPTNAME, SCRIPT) VALUES(";
        string getTableQuery = @"SELECT * FROM scobjects WHERE SCRIPTNAME='";

        public SQL_lite(Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void create_sql_file(ref string Scorp_Line_Exec)
        {
            //(*dbname)
            ArrayList AL = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);

            Do_on.write_to_cui(Do_on.AL_DIRECTORIES[7].ToString() + Do_on.readr.lib_SCR.var_get(AL[0].ToString()));
            SQLiteConnection.CreateFile(Do_on.AL_DIRECTORIES[7].ToString() + Do_on.readr.lib_SCR.var_get(AL[0].ToString()) + Do_on.AL_EXTENSNS[2]);
            Do_on.readr.lib_SCR.var_arraylist_dispose(ref AL);

            return;
        }

        public void add_connection(ref string Scorp_Line_Exec)
        {
            //(*dbname, *name)
            //Data Source=c:\mydb.db;Version=3;Password=myPassword;
            ArrayList AL = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);
            SQLiteConnection conn = new SQLiteConnection("Data Source=" + Do_on.AL_DIRECTORIES[7].ToString() + Do_on.readr.lib_SCR.var_get(AL[0].ToString()) + Do_on.AL_EXTENSNS[2] + ";Version=3;New=false;Password=" + Do_on.SHA + ";");
            Do_on.write_to_cui("Data Source=" + Do_on.AL_DIRECTORIES[7].ToString() + Do_on.readr.lib_SCR.var_get(AL[0].ToString()) + ".db;Version=3;New=true;Password=" + Do_on.SHA + ";");

            Do_on.AL_SQL.Add(conn);
            Do_on.AL_SQL_REF.Add(Do_on.readr.lib_SCR.var_get(AL[1].ToString()));

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref AL);
            return;
        }

        public void open_connection(ref string Scorp_Line_Exec)
        {
            //(*name)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);

            ((SQLiteConnection)Do_on.AL_SQL[Do_on.AL_SQL_REF.IndexOf(Do_on.readr.lib_SCR.var_get(al[0].ToString()))]).Open();

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);

            return;
        }

        public void close_connection(ref string Scorp_Line_Exec)
        {
            //(*name)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);

            ((SQLiteConnection)Do_on.AL_SQL[Do_on.AL_SQL_REF.IndexOf(Do_on.readr.lib_SCR.var_get(al[0].ToString()))]).Close();

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);

            return;
        }

        public void verify(ref string Scorp_Line_Exec)
        {
            //(*name)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);

            SQLiteCommand cmd = new SQLiteCommand(createTableQuery, ((SQLiteConnection)Do_on.AL_SQL[Do_on.AL_SQL_REF.IndexOf(Do_on.readr.lib_SCR.var_get(al[0].ToString()))]));
            cmd.ExecuteReader(System.Data.CommandBehavior.Default);
            
            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }

        public void getobjects_query(ref string Scorp_Line_Exec)
        {
            //(*query, *variable, *name)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);
            SQLiteCommand cmd = new SQLiteCommand(getTableQuery + Do_on.readr.lib_SCR.var_get(al[0].ToString()).ToString() + "'", ((SQLiteConnection)Do_on.AL_SQL[Do_on.AL_SQL_REF.IndexOf(Do_on.readr.lib_SCR.var_get(al[2].ToString()))]));
            SQLiteDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
            dr.Read();
            Do_on.write_to_cui(dr.GetString(2));
            cmd.Dispose();

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }

        public void setobjects_query(ref string Scorp_Line_Exec)
        {
            //(*name, *variable, *name)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line_Exec);

            int size = Do_on.crypto.To_Byte(al[1]).Length;
            SQLiteCommand cmd = new SQLiteCommand(insertTableQuery + "'" + Do_on.readr.lib_SCR.var_get(al[0].ToString()) + "','" + Do_on.readr.lib_SCR.var_get(al[1].ToString()) + "')", ((SQLiteConnection)Do_on.AL_SQL[Do_on.AL_SQL_REF.IndexOf(Do_on.readr.lib_SCR.var_get(al[2].ToString()))]));
            cmd.ExecuteNonQuery(System.Data.CommandBehavior.Default);
            //cmd.Dispose();

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }
    }
}

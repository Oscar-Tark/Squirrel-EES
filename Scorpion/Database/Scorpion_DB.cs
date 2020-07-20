/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark>

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
using System.Data.SQLite;

//Static Library
namespace Scorpion
{
    class SQLfunctions
    { 
        public string clean_sql_single_word(ref string sql)
        {
            sql = sql.Replace("'", "");
            sql = sql.Replace("\"", "");
            sql = sql.Replace(" ", "");
            return sql;
        }

    }

    partial class Librarian
    {
        /*BASIC DB FUNCTIONS FOR INTERNAL DB
         * DB's in scorpion store by name&value
         * 
         * SYNTAX:
         * -------
         * @table@name
         *         
         * STRUCTURE:
         * ----------
         * {name, value}        
        */

        public void dbcreate(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path
            SQLiteConnection.CreateFile((string)var_get(objects[0]));

            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public void dbtablecreate(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *table name, *column, *column...

            SQLfunctions s_fun = new SQLfunctions();
            string Database = (string)var_get(objects[0]);
            SQLiteConnection db_conn = new SQLiteConnection("Data Source=" + Database + "; Version=3");
            db_conn.Open();

            string Table = (string)var_get(objects[0]);
            Table = s_fun.clean_sql_single_word(ref Table);

            SQLiteCommand cmd = new SQLiteCommand("CREATE TABLE ", db_conn);
            cmd.ExecuteNonQuery();

            db_conn.Close();

            s_fun = null;
            db_conn = null;
            cmd = null;
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        /*

        //OLD
    public void dbcreate(ref string Scorp_line_Exec, ref ArrayList objects)
    {
        //::*File_Name_w_path, *pwd
        //MAX TABLE LEN IS 0x3a
        //{val}
        string name = (string)var_get(objects[0]);
        //Could directly do a new Arraylist(), but want to dispose of it after the operation is done.
        ArrayList al_db = new ArrayList();
            Do_on.AL_TBLE.Add(new string[0x3a]);
            Do_on.AL_TBLE_REF.Add(name);

            File.WriteAllBytes(name, Do_on.crypto.encrypt(al_db, (string)var_get(objects[1])));
            Do_on.write_to_cui("Created table file(to disk) : " + name);

        name = null;
        var_arraylist_dispose(ref al_db);
        var_arraylist_dispose(ref objects);
        Scorp_line_Exec = null;
        return;
    }

    public void dbopen(string Scorp_line_Exec, ArrayList objects)
    {
        //::*path, *pwd
        //MAX TABLE LEN IS 0x3a
        string name = (string)var_get(objects[0]);

        if (!Do_on.AL_TBLE_REF.Contains((string)var_get(objects[0])))
        {
            Do_on.AL_TBLE_REF.Add(var_get(objects[0]));

            byte[] b = File.ReadAllBytes(name);
            b = Do_on.crypto.decrypt(b, (string)var_get(objects[1]));

            if (!verifydb(ref b))
                return;

            Do_on.AL_TBLE.Add(Do_on.crypto.To_Object(new MemoryStream(b)));
            Do_on.AL_TBLE_REF.Add(name);
            //Do_on.AL_TBLE.Add(Do_on.vds.UnDump_DB(var_get(objects[0].ToString()).ToString(), Do_on.SHA));
            //verifyload(var_get(objects[0].ToString()).ToString());
            Do_on.write_to_cui("Added Data File: '" + var_get(objects[0]) + "'");
        }
        else { Do_on.write_to_cui("Table '" + var_get(objects[0]) + "' already in memory"); }

        var_arraylist_dispose(ref objects);
        Scorp_line_Exec = null;
        return;
    }

    public void listdbs(ref string Scorp_Line_Exec, ref ArrayList objects)
    {
        foreach (string s_name in Do_on.AL_TBLE_REF)
            Do_on.write_to_cui(s_name);

        Scorp_Line_Exec = null;
        var_arraylist_dispose(ref objects);
        return;
    }

    private bool verifydb(ref byte[] b)
    {
        if(b.Length == 0x3a)
            return true;
        return false;
    }

    /*
    public void dbdelete(String Scorp_Line_Exec, ArrayList objects)
    {
        File.Delete(Do_on.AL_DIRECTORIES[0] + var_get(objects[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1]);
        Do_on.write_to_cui("Deleteing data file(from disk): " + var_get(objects[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1]);

        var_arraylist_dispose(ref objects);
        Scorp_Line_Exec = null;
        return;
    }

    public void dbsave(string Scorp_Line_Exec, ArrayList objects)
    {
        Do_on.vds.Dump_DB(var_get(objects[0].ToString()).ToString());

        var_arraylist_dispose(ref objects);
        Scorp_Line_Exec = null;
        return;
    }*/

        //OLD REMOVE
        /*private void verifyload(string Name)
        {
            //Verify all three arraylists exist in file
            //{Cells},{Ref},{GUI FUNCTIONS(Element based updating)},{USERS{Name}}}
            if (((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Name)]).Count < 5 && ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Name)]).Count >= 0)
            {
                //                                                                                REF              VAR              TAG
                foreach (string s in Do_on.AL_SECTIONS)
                    ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Name)]).Add(new ArrayList() { new ArrayList(), new ArrayList(), new ArrayList() });
                Do_on.write_to_cui("Verifying loaded file, Added " + Do_on.AL_SECTIONS.Count.ToString() + " Parent Cells");
            }
            Name = null;
            return;
        }*/

        //OLD
        /*private void unget_data_file(ref string File)
        {
            ArrayList al = cut_variables(ref File);

            Do_on.AL_TBLE.RemoveAt(Do_on.AL_TBLE_REF.IndexOf(var_get(al[0].ToString())));
            Do_on.AL_TBLE_REF.Remove(var_get(al[0].ToString()));

            Do_on.write_to_cui("Removed Data File(From Memory Only): " + var_get(al[0].ToString()));

            var_arraylist_dispose(ref al);
            return;
        }

        public void list_internals(ref string Scorp_Line)
        {
            /*(*name)*/
        /*string accum = "ROOT\n";
        ArrayList al_ = cut_variables(Scorp_Line);
        foreach(ArrayList al in ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_[0].ToString()))]))
        {

        }
    }




    private void export_visual(ref string Scorp_Line_Exec)
    {
        //(*table,*viewtype)
        ArrayList al = cut_variables(ref Scorp_Line_Exec);

        string accum = "";
        foreach(string s in ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al[0].ToString()))])[0])[0]))
        {
            accum += s + "\n";
        }

        Do_on.write_to_cui(accum);

        return;
    }

    public void add_linkages(ref string Scorp_Line_Exec)
    {
        ArrayList al = cut_variables(ref Scorp_Line_Exec);
        //(*Table_name,*value_of_cell,*link,*link,*link)
        /*          {Linkage Name}
            {ref cell,ref cell,ref cell,ref cell,ref cell,ref cell}

        */
        //([Name],*cell,*cell,*cell)
        /*
        for (int i = 2; i < al.Count; i++)
        {
           ((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al[0].ToString()))])[1]).Add(var_get(al[i].ToString()));
        }

        var_arraylist_dispose(ref al);
        return;
    }*/
    }
}
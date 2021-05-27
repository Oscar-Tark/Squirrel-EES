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

using System.Collections;
using System.Security;
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
            string JSON = Mongodb.Mongogetall((string)var_get(objects[0]), (string)var_get(objects[1]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return var_create_return(ref JSON, true);
        }

        public string mongodbgetspecific(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //returnable<<::*db, *collection, *filter
            string JSON = Mongodb.Mongogetspecific((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return var_create_return(ref JSON, true);
        }

        public void mongodbsetone(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Mongodb.Mongoset((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }


        //LEGACY
        public void dbcreate(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*File_Name_w_path, *sizein slots
            //MAX TABLE LEN IS UNLIMITED
            //{val}
            string name = (string)var_get(objects[0]);
            Do_on.vds.Create_DB(name, int.Parse((string)var_get(objects[1])));
            Do_on.write_to_cui("Created Data File(to disk) : " + name + " ["+ (string)var_get(objects[1]) + " slots]");

            name = null;
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public void dbopen(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *pwd
            string name = (string)var_get(objects[0]);
            if (!Do_on.AL_TBLE_REF.Contains(name))
            {
                Do_on.AL_TBLE.Add(Do_on.vds.Load_DB(name, ""));
                Do_on.AL_TBLE_REF.Add(name);
                Do_on.write_to_cui("Added Data File: '" + name + "'");
            }
            else { Do_on.write_to_cui("Data File '" + name + "' already in memory"); }

            var_arraylist_dispose(ref objects);
            name = null;
            Scorp_Line_Exec = null;
            return;
        }

        public void dbsave(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path/name, *pwd
            string name = (string)var_get(objects[0]);

            Do_on.vds.Save_DB(name, "");
            Do_on.write_to_cui("Data File '" + name + "' saved");

            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            name = null;
            return;
        }

        public void dbset(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *value, *tag, *meta
            Do_on.vds.Data_setDB((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]), (string)var_get(objects[3]), (string)var_get(objects[4]), (string)objects[0]);
            return;
        }

        public string dbget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *search
            //*search='value@Joe Donson'
            string elem = Do_on.vds.Data_getDB((string)var_get(objects[0]), (string)var_get(objects[1]));
            return var_create_return(ref elem, false);
        }

        public void listdb(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*db
            ArrayList al = ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(objects[0]))]);
            for (int i = 0x00; i < 0x3a; i++)
                Do_on.write_to_cui((string)al[i]);

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);
            var_arraylist_dispose(ref objects);
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

        public void dbseg(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path
            string path = (string)var_get(objects[0]);
            byte[] b = Do_on.crypto.To_Byte(Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(path)]);
            //Do_on.vds.Segment_DB(ref path, ref b);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //DEBUG
        public void tss(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *var
            string path = (string)var_get(objects[0]);
            string var_ = (string)var_get(objects[1]);
            Do_on.write_debug(Do_on.vds.Segment_search(ref path, ref var_));

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
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
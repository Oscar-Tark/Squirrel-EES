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

using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        public void dbopen(string Scorp_line_Exec, ArrayList objects)
        {
            //{table, pass}
            //Requested Undump
            Do_on.vds.Verify_File_DB(var_get(objects[0].ToString()).ToString());

            if (!Do_on.AL_TBLE_REF.Contains(var_get(objects[0].ToString()).ToString()))
            {
                Do_on.AL_TBLE_REF.Add(var_get(objects[0].ToString()).ToString());
                Do_on.AL_TBLE.Add(Do_on.vds.UnDump_DB(var_get(objects[0].ToString()).ToString(), Do_on.SHA));
                verify_load(var_get(objects[0].ToString()).ToString());
                Do_on.write_to_cui("Added Data File: '" + var_get(objects[0].ToString()).ToString() + "'");
            }
            else { Do_on.write_to_cui("Table '" + var_get(objects[0].ToString()).ToString() + "' already in memory"); }

            var_arraylist_dispose(ref objects);
            Scorp_line_Exec = null;
            return;
        }

        private void unget_data_file(ref string File)
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
            string accum = "ROOT\n";
            ArrayList al_ = cut_variables(Scorp_Line);
            foreach(ArrayList al in ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_[0].ToString()))]))
            {

            }
        }

        public void dbcreate(string Scorp_line_Exec, ArrayList objects)
        {
            //(*File_Name)
            Do_on.vds.Verify_Directory_DB();
            Do_on.AL_TBLE.Add(new ArrayList());
            Do_on.AL_TBLE_REF.Add(var_get(objects[0].ToString()).ToString());
            verify_load(var_get(objects[0].ToString()).ToString());
            File.WriteAllBytes(Do_on.AL_DIRECTORIES[0] + var_get(objects[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1], Do_on.crypto.encrypt(Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(objects[0].ToString()))], Do_on.SHA));
            Do_on.write_to_cui("Created table file(to disk) : " + var_get(objects[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1]);

            var_arraylist_dispose(ref objects);
            Scorp_line_Exec = null;
            return;
        }

        private void verify_load(string Name)
        {
            //Verify all three arraylists exist in file
            //{Cells},{Ref},{GUI FUNCTIONS(Element based updating)},{USERS{Name}}}

            //OPTIMIZE
            if (((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Name)]).Count < 5 && ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Name)]).Count >= 0)
            {
                //DATA

                //                                                                                REF              VAR              TAG
                foreach(string s in Do_on.AL_SECTIONS)
                {
                    ((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Name)]).Add(new ArrayList() { new ArrayList(), new ArrayList(), new ArrayList() });
                }
                Do_on.write_to_cui("Verifying loaded file, Added " + Do_on.AL_SECTIONS.Count.ToString() + " Parent Cells");
            }

            Name = null;
            return;
        }

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

            for (int i = 2; i < al.Count; i++)
            {
               ((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al[0].ToString()))])[1]).Add(var_get(al[i].ToString()));
            }

            var_arraylist_dispose(ref al);
            return;
        }

        public void destroy_linkages(ref string Scorp_Line_exec)
        {
            //(*value_of_cell)

            return;
        }

        public void release(ref string Scorp_line_exec)
        {
            return;
        }
    }
}
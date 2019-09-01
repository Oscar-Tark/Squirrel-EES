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

        private void query_is(string Res, string Table)
        {
            Do_on.write_to_cui("Processing <is></is>");



            Res = null;
            return;
        }

        /*GUI QUERIES
        Create a third arraylist in table for gui

            from rasmus i get {{names to give back to rasmus}{commands}}

        send gui arraylist to rasmus

            > tabs
            > element{*name,*type,*color{},*text,*datatext{{Row}{Columns}{Text}},backendoperation{{typeof(query,operation_no_return),typeof(query,operation_no_return)}{command,command}}}

            Create new array for permessions for AL_FNC what functions they can access.

            > Dynamically connected objects to GUI. MYSIDE
            > Services to keep things updated

            */

            //CREATE RETURN SYSTEM TO RASMUS
            //CREATE USERS AND PERMISSIONS

        //GET ALL GUI (SAVEALL) 
        //SET ALL GUI (SAVEALL)
        //GET DATA
        //SET DATA
        //GETRESULT FROM FUNCTION sendback(STATECHANGED)
        //GET RESOURCE from binary file on demand
        //LOAD ON DEMAND

        private void query_isnot(string Res, string Table)
        {
            Do_on.write_to_cui("Processing <isnot></isnot>");

            Res = null;
            return;
        }

        private void query_getall(string Res, string Table, string Result_to)
        {
            Do_on.write_to_cui("Processing <getall>section</getall>");
            foreach (string s in ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(Table)])[Do_on.AL_SECTIONS.IndexOf(cut_custom(Res, Do_on.AL_QUERY_VAR[14].ToString(), Do_on.AL_QUERY_VAR[15].ToString()))])[1]))
            {
                MessageBox.Show(s);
            }

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
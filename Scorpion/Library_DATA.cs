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
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Threading;
using System.Drawing;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        //PASS 30-11-14
        public void DATA(ref string Scorp_Line)
        {
            //NEW DATA SYSTEM
            if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[94] + Do_on.AL_ACC[3].ToString()))
            {
                get_data_file(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[100] + Do_on.AL_ACC[3].ToString()))
            {
                //unget
                unget_data_file(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[95] + Do_on.AL_ACC[3].ToString()))
            {
                //Create Table
                create_data_file(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[96] + Do_on.AL_ACC[3].ToString()))
            {
                //Delete Table
                delete_data_file(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[97] + Do_on.AL_ACC[3].ToString()))
            {
                //Save Table
                save_data_file(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[5] + Do_on.AL_ACC[3].ToString()))
            {

            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[101] + Do_on.AL_ACC[3].ToString()))
            {
                add_linkages(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[6] + Do_on.AL_ACC[3].ToString()))
            {
                //Data_Cluster_Read(Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[104] + Do_on.AL_ACC[3].ToString()))
            {
                export_visual(ref Scorp_Line);
            }

            //SQLite
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[125] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.add_connection(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[126] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.create_sql_file(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[127] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.open_connection(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[128] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.close_connection(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[129] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.verify(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[130] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.setobjects_query(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[131] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.sql.getobjects_query(ref Scorp_Line);
            }

            else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {" + Do_on.AL_ACC_SUP[4] + "} in line {" + Scorp_Line + "}."); }

            //clean
            Scorp_Line = null;

            return;
        }

        public void get_data_file(ref string File)
        {
            ArrayList al = cut_variables(ref File);
            //{table, pass}
            //Requested Undump
            Do_on.vds.Verify_File_DB(var_get(al[0].ToString()).ToString());

            if (!Do_on.AL_TBLE_REF.Contains(var_get(al[0].ToString()).ToString()))
            {
                Do_on.AL_TBLE_REF.Add(var_get(al[0].ToString()).ToString());
                Do_on.AL_TBLE.Add(Do_on.vds.UnDump_DB(var_get(al[0].ToString()).ToString(), var_get(al[1].ToString()).ToString()));
                verify_load(var_get(al[0].ToString()).ToString());
                Do_on.write_to_cui("Added Data File: '" + var_get(al[0].ToString()).ToString() + "'");
            }
            else { Do_on.write_to_cui("Table '" + var_get(al[0].ToString()).ToString() + "' already in memory"); }

            File = null;
            return;
        }

        public void unget_data_file(ref string File)
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

        public void create_data_file(ref string Scorp_Line)
        {
            //(*File_Name)
            ArrayList al = cut_variables(ref Scorp_Line);

            Do_on.vds.Verify_Directory_DB();
            Do_on.AL_TBLE.Add(new ArrayList());
            Do_on.AL_TBLE_REF.Add(var_get(al[0].ToString()).ToString());
            verify_load(var_get(al[0].ToString()).ToString());

            File.WriteAllBytes(Do_on.AL_DIRECTORIES[0] + var_get(al[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1], Do_on.crypto.encrypt(Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al[0].ToString()))], "Anus"));

            Do_on.write_to_cui("Created table file(to disk) : " + var_get(al[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1]);
            
            var_arraylist_dispose(ref al);
            Scorp_Line = null;
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

        public void delete_data_file(ref string Scorp_Line)
        {
            ArrayList al = cut_variables(ref Scorp_Line);
            File.Delete(Do_on.AL_DIRECTORIES[0] + var_get(al[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1]);
            Do_on.write_to_cui("Deleteing data file(from disk): " + var_get(al[0].ToString()).ToString() + Do_on.AL_EXTENSNS[1]);

            var_arraylist_dispose(ref al);
            Scorp_Line = null;

            return;
        }

        public void save_data_file(ref string Scorp_Line)
        {
            //(*name)
            ArrayList al = cut_variables(ref Scorp_Line);
            Do_on.vds.Dump_DB(var_get(al[0].ToString()).ToString());

            var_arraylist_dispose(ref al);
            Scorp_Line = null;
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
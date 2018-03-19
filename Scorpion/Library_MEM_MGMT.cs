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
using System.IO;
using System.Threading;
using System.Drawing;
using System.Reflection;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        //PASS
        public void MEMORY(ref string Scorp_Line)
        {
            if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[70] + Do_on.AL_ACC[3].ToString()))
            {
                var_create(Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[71] + Do_on.AL_ACC[3].ToString()))
            {
                var_system_load();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[114] + Do_on.AL_ACC[3].ToString()))
            {
                var_set(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[72] + Do_on.AL_ACC[3].ToString()))
            {
                //var_tag_delete(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[73] + Do_on.AL_ACC[3].ToString()))
            {
                //var_tag_add(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[74] + Do_on.AL_ACC[3].ToString()))
            {
                var_delete(Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[75] + Do_on.AL_ACC[3].ToString()))
            {
                var_delete_all();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[76] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.vds.Dump_main_db();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[77] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.vds.Un_Dump();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[77] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.vds.Un_Dump();
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[119] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.mmsec.Encrypt_Block(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[120] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.mmsec.Decrypt_Block(ref Scorp_Line);
            }
            else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {" + Do_on.AL_ACC_SUP[5] + "} in line {" + Scorp_Line + "}"); }
            //SECURITY ISSUE / NOT PART OF MAIN STREAM / OBJECTS NOT ACCESSIBLE
            /*else if (Scorp_Line.ToLower().StartsWith("mem.vs("))
            {
                var_stream_create(ref Scorp_Line);
            }
            else if (Scorp_Line.ToLower().StartsWith("mem.vds("))
            {
                var_stream_delete(ref Scorp_Line);
            }*/

            //clean
            Scorp_Line = null;

            return;
        }

        //Variables-->
        private void var_create(string Scorp_Line_Exec)
        {
            //(*,*,*,*,...)
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            foreach (string s in al)
            {
                var_new((object)"", s, "");
            }

            //clean
            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }
        
        public void var_set(string Scorp_Line_Exec, object o)
        {
            /*(*@var,*object_value)*/
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            if (!var_cut_symbol(al[0].ToString()).ToString().Contains(Do_on.AL_ACC[6].ToString()))
            {
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = o;
            }
            else
            {
                ArrayList al_ = var_cut_domain(al[0].ToString());
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = o;
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[3] = 1;
            }

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }

        public void var_set(ref string Scorp_Line_Exec)
        {
            /*(*where,*value)*/
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            if (!var_cut_symbol(al[0].ToString()).ToString().Contains(Do_on.AL_ACC[6].ToString()))
            {
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = var_get(al[1].ToString());
            }
            else
            {
                ArrayList al_ = var_cut_domain(al[0].ToString());
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = var_get(al[1].ToString());
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[3] = 1;
            }

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }

        public void var_set_decrypted(string where, object value)
        {
            /*(*where,*value)*/
            if (!where.Contains(Do_on.AL_ACC[6].ToString())/* && (int)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] == 1*/)
            {
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[2] = value;
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] = 0;
            }
            else
            {
                ArrayList al_ = var_cut_domain(where);
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = value;
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[3] = 0;
            }

            where = null;
            value = null;

            return;
        }

        public void var_set_encrypted(string where, byte[] value)
        {
            /*(*where,*value)*/
            if (!where.Contains(Do_on.AL_ACC[6].ToString())/* && (int)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] == 0*/)
            {
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[2] = value;
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] = 1;
            }
            else
            {
                ArrayList al_ = var_cut_domain(where);
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = value;
            }

            where = null;
            value = null;

            return;
        }

        private void delete_variable_transition(string Scorp_Line_Exec)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int nnx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            Scorp_Line_Exec = Scorp_Line_Exec.Remove(nnx2);
            Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, ndx + 1);

            Scorp_Line_Exec = Scorp_Line_Exec.Replace("*", "");

            var_delete(Scorp_Line_Exec);
        }

        public void var_new(object Variable, string Reference, string Type_)
        {
            //(*,*,*,*,...)
            try
            {
                if (Do_on.AL_CURR_VAR_REF.Contains(Reference) == false)
                {
                    //Variable = var_get(Variable.ToString());

                    //{key, ref, val, encry, tag}

                    //Normal Var
                    if (!Reference.Contains(Do_on.AL_ACC[6].ToString()))
                    {
                        var_cut_symbol(ref Reference);
                        //OBJ REF            INDX ???                                                   INFOBLOCKS:       INFO             META             RESOURCES(images...)
                        Do_on.AL_CURR_VAR.Add(new ArrayList { "", Reference, "", "0", new ArrayList() { new ArrayList(), new ArrayList() }, new ArrayList() { new ArrayList(), new ArrayList(), new ArrayList() } });
                        Do_on.AL_CURR_VAR_REF.Add(Reference);
                        Do_on.AL_CURR_VAR_TAG.Add("");
                    }
                    else
                    {
                        ArrayList al_variables = var_cut_domain(Reference);
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_variables[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_variables[1].ToString()))])[0]).Add(new ArrayList() { "", al_variables[2], "", "0", new ArrayList() { new ArrayList(), new ArrayList() } });
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_variables[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_variables[1].ToString()))])[1]).Add(al_variables[2]);
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_variables[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_variables[1].ToString()))])[2]).Add(al_variables[2]);
                    }
                }
            }
            catch { Do_on.write_to_cui("Scorpion IEE Error : Unable to Allocate Memory (Variable : '" + Variable.ToString() + "', Reference : '" + Reference + "')"); }

            //clean
            Variable = null;
            Reference = null;
            Type_ = null;

            return;
        }

        public void var_delete(String Scorp_Line_Exec)
        {
            //(*,*,*,*,*,...)
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            foreach (string s in al)
            {
                if (!s.Contains(Do_on.AL_ACC[6].ToString()))
                {
                    Scorp_Line_Exec = s;
                    Do_on.AL_CURR_VAR.RemoveAt(Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(ref Scorp_Line_Exec)));
                    Do_on.AL_CURR_VAR_REF.RemoveAt(Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(ref Scorp_Line_Exec)));
                    Do_on.AL_CURR_VAR_TAG.RemoveAt(Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(ref Scorp_Line_Exec)));
                    
                    //TRIM
                    Do_on.AL_CURR_VAR.TrimToSize();
                    Do_on.AL_CURR_VAR_REF.TrimToSize();
                    Do_on.AL_CURR_VAR_TAG.TrimToSize();
                }
                else
                {
                    ArrayList al_ = var_cut_domain(s);
                    //{db}                                      {table}                     {var}
                    try
                    {
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0]).RemoveAt(((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2]));
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).RemoveAt(((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2]));
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[2]).RemoveAt(((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2]));

                        //TRIM
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0]).TrimToSize();
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).TrimToSize();
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[2]).TrimToSize();
                    }
                    catch { }
                }
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        //DEFUNCT
        public void var_delete_all()
        {
            Do_on.AL_CURR_VAR.Clear();
            Do_on.AL_CURR_VAR_REF.Clear();

            Do_on.AL_CURR_VAR.TrimToSize();
            Do_on.AL_CURR_VAR_REF.TrimToSize();

            File.Delete(Do_on.AL_HIB_FILES[0].ToString());

            return;
        }

        public void var_system_load()
        {
            Do_on.AL_CURR_VAR.Clear();
            Do_on.AL_CURR_VAR_REF.Clear();
            Do_on.AL_CURR_VAR_EVT.Clear();
            Do_on.AL_CURR_VAR_TAG.Clear();
            Do_on.AL_CURR_VAR.TrimToSize();
            Do_on.AL_CURR_VAR_REF.TrimToSize();
            Do_on.AL_CURR_VAR_EVT.TrimToSize();
            Do_on.AL_CURR_VAR_TAG.TrimToSize();

            Do_on.types.load_system_vars();
            Do_on.types.load_primary_vars();
            return;
        }

        public void on_object_changed(object object_)
        {
            return;
        }

        //memory get
        public object var_get(ref string Block)
        {
            object o = (object)Block;
            try
            {
                if (!o.ToString().StartsWith("*\""))
                {
                    if (!o.ToString().Contains(Do_on.AL_ACC[6].ToString()))
                    {
                        try
                        {
                            o = (object)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))])[2];
                        }
                        catch { }
                    }
                    else
                    {
                        ArrayList al_ = var_cut_domain(ref o);
                        //{db}                                      {table}                     {var}
                        try
                        {
                            o = ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2];
                        }
                        catch { }
                    }
                }
                else
                {
                    o = var_cut_str_symbol(var_cut_symbol(ref Block));
                }
            }
            catch { }
            return o;
        }

        public object var_get(string Block)
        {
            //Create 2 domains
            //main.var
            //table.section.var
            //*true@table@gui

            ArrayList al_get = new ArrayList();
            object o = (object)Block;
            try
            {
                if (!o.ToString().StartsWith("*\""))
                {
                    if (!o.ToString().Contains(Do_on.AL_ACC[6].ToString()))
                    {
                        try
                        {
                            o = (object)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))])[2];
                        }
                        catch { }
                    }
                    else
                    {
                        ArrayList al_ = var_cut_domain(ref o);
                        //{db}                                      {table}                     {var}
                        try
                        {
                            o = ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2];
                        }
                        catch { }
                    }
                }
                else
                {
                    //General String
                    o = var_cut_str_symbol(var_cut_symbol(ref Block));
                }
            }
            catch { }

            return o;
        }

        public ArrayList var_cut_domain(ref object o)
        {
            return cut_custom_domain(o.ToString());
        }

        public ArrayList var_cut_domain(object o)
        {
            return cut_custom_domain(o.ToString());
        }

        public ArrayList var_cut_subvar(object o)
        {
            return cut_custom_subvar(o.ToString());
        }

        public object var_get_full(string Block)
        {
            object o = (object)Block;
            try
            {
                o = (object)(ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))];
            }
            catch { }
            return o;
        }
        /*
        public void var_tag_delete(ref string Scorp_Line_Exec)
        {
            //(*var#subvar)
            //(*tagname)

            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            //ArrayList al = cut_custom_subvar(ref Scorp_Line_Exec);
            if (!var_cut_symbol(al[0].ToString()).ToString().Contains(Do_on.AL_ACC[6].ToString()))
            {
                //Innexistent feature for one.vdb
            }
            else
            {
                ArrayList al_ = var_cut_domain(al[0].ToString());
                ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_[1].ToString()))])[2]).RemoveAt(((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_[1].ToString()))])[1]).IndexOf(var_get(al_[0].ToString())));

                //{db}                                      {table}                     {var}
                /*((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[4])[0]).Add(var_get(al[0].ToString()));
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[4])[1]).Add(var_get(al[1].ToString()));
            
            }
        }

        public void var_tag_add(ref string Scorp_Line_Exec)
        {
            //Tagging does not exist for one.vdb
            //(*var#subvar)
            //(*domain,*tag)

            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            //ArrayList al = cut_custom_subvar(ref Scorp_Line_Exec);
            if (!var_cut_symbol(al[0].ToString()).ToString().Contains(Do_on.AL_ACC[6].ToString()))
            {
                //Innexistent feature for one.vdb
            }
            else
            {
                ArrayList al_ = var_cut_domain(al[0].ToString());
                ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_[1].ToString()))])[2]).Add(al[1]);
                /*ArrayList al_ = var_cut_domain(al[0].ToString());
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[4])[0]).Add(var_get(al[0].ToString()));
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[4])[1]).Add("");
            
            }

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }*/

        public void var_assign(string Block, int index)
        {
            ArrayList al = cut_variables(ref Block);
            ((ArrayList)Do_on.AL_CURR_VAR[index])[2] = var_get(al[0].ToString()).ToString();

            //clean
            var_arraylist_dispose(ref al);
            Block = null;
        }

        //Actions
        public string var_cut_spaces(string Var)
        {
            return Var.Replace(" ", "");
        }

        public string var_cut_symbol(ref string Var)
        {
            if (Var.StartsWith("*") == true)
            {
                Var = Var.Replace("*", "");
            }

            return Var;
        }

        public string var_cut_symbol(string Var)
        {
            if (Var.StartsWith("*") == true)
            {
                Var = Var.Replace("*", "");
            }

            return Var;
        }
        public string var_cut_str_symbol(ref string Var)
        {
            if (Var.Contains("\"") == true)
            {
                Var = Var.Replace("\"", "");
            }

            return Var;
        }

        public string var_cut_str_symbol(string Var)
        {
            if (Var.Contains("\"") == true)
            {
                Var = Var.Replace("\"", "");
            }

            return Var;
        }

        public void var_arraylist_dispose(ref ArrayList al)
        {
            try
            {
                for (int i = 0; i < al.Count; i++)
                {
                    try
                    {
                        al[i] = null;
                    }
                    catch { write_to_cui("Memory Segment Dispose Fail: One Element"); }
                }
                al = null;
            }
            catch { write_to_cui("Memory Segment Dispose Fail: Segment"); }

            return;
        }
    }
}
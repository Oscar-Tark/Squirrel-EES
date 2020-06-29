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
using System.IO;
using System.Linq.Expressions;

namespace Scorpion
{
    partial class Librarian
    {
        private void var_dispose_internal(ref object __object)
        {
            __object = null;
            return;
        }

        private void var_dispose_internal(ref string __object)
        {
            __object = null;
            return;
        }

        //Make private
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
                    catch { write_to_cui("Memory Slot Dispose Fail: One Element"); }
                }
                al = null;
            }
            catch { write_to_cui("Memory Slot Dispose Fail: Segment"); }

            return;
        }

        //Creates return value for function according to value or copy value of another variable
        private string var_create_return(ref string val, bool is_val)
        {
            if (is_val)
                return "\'" + val + "\'";
            return val;
        }
    }

    partial class Librarian
    {
        //Variables-->o
        //NEW
        public void var(string Scorp_Line_Exec, ArrayList objects)
        {
            //(*,*,*,*,...)
            //ArrayList al = cut_variables(ref Scorp_Line_Exec);
            foreach (string s in objects)
                var_new((object)"", s, "");

            //clean
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public void varset(string Scorp_Line_Exec, ArrayList objects)
        {
            /*(*where,*value)*/
            //if (!var_cut_symbol(objects[0].ToString()).Contains(Do_on.AL_ACC[6].ToString()))
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[2] = var_get(objects[1].ToString());
            /*else
            {
                ArrayList al_ = var_cut_domain(objects[0].ToString());
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = var_get(objects[1].ToString());
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[3] = 1;
            }*/

            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public string varconcatenate(ref string Scorp_Line_Exec, ArrayList objects)
        {
            /*RETURNS :: *arg, *arg...*/
            string end = "";
            foreach(object obj in objects)
                end += var_get(obj);
            return var_create_return(ref end, true);
        }

        public void listvars(string Scorp_Line_Exec, ArrayList objects)
        {
            string STR_ = "";
            foreach (string s in Do_on.AL_CURR_VAR_REF)
                STR_ += s + " [" + ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(s)])[2] + "]"+ "\n";
            Do_on.write_to_cui(STR_);

            //clean
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }


        //OLD LEGACY
        public void var_set(string Scorp_Line_Exec, object o)
        {
            /*(*@var,*object_value)*/
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            string s = o.ToString();
            //if (!var_cut_symbol(al[0].ToString()).Contains(Do_on.AL_ACC[6].ToString()))
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = var_get(ref s);
            /*else
            {
                ArrayList al_ = var_cut_domain(al[0].ToString());
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = o;
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[3] = 1;
            }*/

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;

            return;
        }

        public void var_set_decrypted(string where, object value)
        {
            /*(*where,*value)*/
            //if (!where.Contains(Do_on.AL_ACC[6].ToString())/* && (int)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] == 1*/)
            //{
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[2] = value;
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] = 0;
            /*}
            else
            {
                ArrayList al_ = var_cut_domain(where);
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = value;
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[3] = 0;
            }*/

            where = null;
            value = null;

            return;
        }

        public void var_set_encrypted(string where, byte[] value)
        {
            /*(*where,*value)*/
            //if (!where.Contains(Do_on.AL_ACC[6].ToString())/* && (int)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] == 0*/)
            //{
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[2] = value;
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(where))])[3] = 1;
            /*}
            else
            {
                ArrayList al_ = var_cut_domain(where);
                //{db}                                      {table}                     {var}
                ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2] = value;
            }*/

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

            vardelete(Scorp_Line_Exec, new ArrayList());
        }

        private void var_set(string Reference, string Variable)
        {
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = var_get(Variable);
            return;
        }

        private void var_new(object Variable, string Reference, string Type_)
        {
            //(*,*,*,*,...)
            try
            {
                if (Do_on.AL_CURR_VAR_REF.Contains(Reference) == false)
                {
                    //Variable = var_get(Variable.ToString());

                    //{key, ref, val, encry, tag}

                    //Normal Var
                    //if (!Reference.Contains(Do_on.AL_ACC[6].ToString()))
                    //{
                        var_cut_symbol(ref Reference);
                        //OBJ REF            INDX ???                                                   INFOBLOCKS:       INFO             META             RESOURCES(images...)
                        Do_on.AL_CURR_VAR.Add(new ArrayList { "", Reference, "", "0", new ArrayList() { new ArrayList(), new ArrayList() }, new ArrayList() { new ArrayList(), new ArrayList(), new ArrayList() } });
                        Do_on.AL_CURR_VAR_REF.Add(Reference);
                        Do_on.AL_CURR_VAR_TAG.Add("");
                    /*}
                    else
                    {
                        ArrayList al_variables = var_cut_domain(Reference);
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_variables[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_variables[1].ToString()))])[0]).Add(new ArrayList() { "", al_variables[2], "", "0", new ArrayList() { new ArrayList(), new ArrayList() } });
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_variables[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_variables[1].ToString()))])[1]).Add(al_variables[2]);
                        ((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(var_get(al_variables[0].ToString()))])[Do_on.AL_SECTIONS.IndexOf(var_get(al_variables[1].ToString()))])[2]).Add(al_variables[2]);
                    }*/
                }
            }
            catch { Do_on.write_to_cui("Scorpion IEE Error : Unable to Allocate Memory (Variable : '" + Variable.ToString() + "', Reference : '" + Reference + "')"); }

            //clean
            Variable = null;
            Reference = null;
            Type_ = null;

            return;
        }

        public void vardelete(string Scorp_Line_Exec, ArrayList objects)
        {
            //(*,*,*,*,*,...)
            ArrayList al = cut_variables(ref Scorp_Line_Exec);

            foreach (string s in al)
            {
                //if (!s.Contains(Do_on.AL_ACC[6].ToString()))
                //{
                    Scorp_Line_Exec = s;
                    Do_on.AL_CURR_VAR.RemoveAt(Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(ref Scorp_Line_Exec)));
                    Do_on.AL_CURR_VAR_REF.RemoveAt(Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(ref Scorp_Line_Exec)));
                    Do_on.AL_CURR_VAR_TAG.RemoveAt(Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(ref Scorp_Line_Exec)));
                    
                    //TRIM
                    Do_on.AL_CURR_VAR.TrimToSize();
                    Do_on.AL_CURR_VAR_REF.TrimToSize();
                    Do_on.AL_CURR_VAR_TAG.TrimToSize();
                /*}
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
                }*/
            }

            //clean
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref al);

            return;
        }

        public void vardeleteall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.AL_CURR_VAR.Clear();
            Do_on.AL_CURR_VAR_REF.Clear();

            Do_on.AL_CURR_VAR.TrimToSize();
            Do_on.AL_CURR_VAR_REF.TrimToSize();

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            //File.Delete(Do_on.AL_HIB_FILES[0].ToString());
            return;
        }

        public void varsystem(ref string Scorp_Line_Exec, ref ArrayList objects)
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

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
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
                if (!o.ToString().StartsWith("\'", System.StringComparison.CurrentCulture))
                {
                        try
                        {
                            o = (object)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))])[2];
                        }
                        catch { }
                }
                else
                    o = var_cut_str_symbol(var_cut_symbol(ref Block));
            }
            catch { }
            return o;
        }

        private object var_get(string Block)
        {
            return var_get(ref Block);
            //Create 2 domains
            //main.var
            //table.section.var
            //*true@table@gui

            /*ArrayList al_get = new ArrayList();
            object o = (object)Block;
            if (Block.StartsWith("\"", System.StringComparison.CurrentCulture))
                //General String
                o = var_cut_str_symbol(var_cut_symbol(ref Block));
            else
            {
                    //if (!o.ToString().Contains(Do_on.AL_ACC[6].ToString()))
                        o = (object)((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))])[2];
                    /*else
                    {
                        ArrayList al_ = var_cut_domain(ref o);
                        //{db}                                      {table}                     {var}
                        o = ((ArrayList)((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[0])[((ArrayList)((ArrayList)((ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(al_[0])])[Do_on.AL_SECTIONS.IndexOf(al_[1])])[1]).IndexOf(al_[2])])[2];
                    }*/
            //}
        }

        public object var_get(object Block)
        {
            return var_get((string)Block);
        }

        public object var_get(ref object Block)
        {
            return var_get((string)Block);
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

        //Actions
        public string var_cut_spaces(string Var)
        {
            return Var.Replace(" ", "");
        }

        public string var_cut_symbol(ref string Var)
        {
            if (Var.StartsWith("*", System.StringComparison.CurrentCulture) == true)
            {
                Var = Var.Replace("*", "");
            }

            return Var;
        }

        public string var_cut_symbol(string Var)
        {
            if (Var.StartsWith("*", System.StringComparison.CurrentCulture) == true)
            {
                Var = Var.Replace("*", "");
            }

            return Var;
        }
        public string var_cut_str_symbol(ref string Var)
        {
            if (Var.Contains("'") == true)
            {
                Var = Var.Replace("'", "");
            }

            return Var;
        }

        public string var_cut_str_symbol(string Var)
        {
            if (Var.Contains("'") == true)
                return Var.Replace("'", "");
            return Var;
        }

        private string var_replace_fake(ref string Block)
        {
            return Block.Replace("@", "*");
        }

        private string var_replace_fake(object Block)
        {
            return var_replace_fake((string)Block);
        }
    }
}
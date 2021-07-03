/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

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
using System.Security;

namespace Scorpion
{
    partial class Librarian
    {
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
        private object var_create_return(ref object val)
        {
            return val;
        }

        private object var_create_return(ref ArrayList val)
        {
            return (object)val;
        }

        private object var_create_return(ref object[] val)
        {
            return val;
        }

        private string var_create_return(ref string val, bool is_val)
        {
            if (is_val)
                return "\'" + val + "\'";
            return val;
        }

        private string var_create_return(string val, bool is_val)
        {
            if (is_val)
                return "\'" + val + "\'";
            return val;
        }

        private object var_create_return(ref byte[] val, bool is_val)
        {
            if (is_val)
                return "\'" + Do_on.crypto.To_Object(val) + "\'";
            return val;
        }

        private object var_create_return(ref SecureString val, bool is_val)
        {
            Do_on.write_warning("A secure string is being returned to a normal scorpion memory variable, this may compromise security");
            if (is_val)
                return "\'" + val + "\'";
            return val;
        }

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

        private void var_dispose_internal(ref byte[] __bytes)
        {
            for(int i = 0; i < __bytes.Length; i++)
                __bytes[i] = 0x00;
            __bytes = null;
            return;
        }

        private void var_dispose_internal(ref object[] __obj)
        {
            for (int i = 0; i < __obj.Length; i++)
                __obj[i] = null;
            __obj = null;
            return;
        }

        private void get_variadic(ref ArrayList objects, int start, int end)
        {

        }
    }

    //MEMORY TAGS
    partial class Librarian
    {
        //TAG FUNCTIONS FOR CHAINING
        public void vartag(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*var, *tag

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }
    }

    //MEMORY SECURITY
    partial class Librarian
    { 
        public void encrypt(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*var, *var..
            string ref_ = (string)objects[0];
            //Allows us to get pin by going through mmsec
            Do_on.mmsec.encrypt(ref ref_);

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void decrypt(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            string ref_ = (string)objects[0];
            Do_on.mmsec.decrypt(ref ref_);

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
        }
    }

    partial class Librarian
    {
        //ADD ARRAY TYPE! **var, **var[[2]]
        public void var(string Scorp_Line_Exec, ArrayList objects)
        {
            //(*,*,*,*,...)
            //Variables by default are created as booleans bearing the 'false' value
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            foreach (string s in objects)
                                var_new(Do_on.types.S_No, s, null, null);
                        }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vararray(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*var, *maintain_contents[BOOL]
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            if ((string)var_get(objects[1]) == Do_on.types.S_Yes)
                                ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[2] = new ArrayList { var_get(objects[0]) };
                            else
                                ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[2] = new ArrayList();
                                }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void varintoarray(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*var, *var, *var...
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[2]).Add(var_get(objects[1]));
                        }
            return;
        }

        public object varfromarray(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*result<<::*array, *index, *asobject
            object ret = Do_on.types.S_No;
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            int ndx = Convert.ToInt32((string)var_get(objects[1]));
                            ret = (string)((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol((string)objects[0]))])[2])[ndx];
                        }
            if((string)var_get(objects[2]) == Do_on.types.S_No)
                return var_create_return((string)ret, true);
            return var_create_return(ref ret);
        }

        //delete var from array
        public void vardeletearray()
        { }

        //indexof
        public void varindexarray()
        { }

        public void varset(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*where, *value
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            try
                            {
                                ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[2] = var_get((string)objects[1]);
                            }
                            catch
                            {
                                //if not string based then save object directly
                                ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[2] = objects[1];
                            }
                        }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public string varconcatenate(ref string Scorp_Line_Exec, ArrayList objects)
        {
            /*RETURNS<<:: *arg, *arg...*/

            string end = "";
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            foreach (object obj in objects)
                                end += var_get(obj);
                        }
            return var_create_return(ref end, true);
        }

        public string varreplace(ref string Scorp_Line_Exec, ArrayList objects)
        {
            //RETURNS<<::*to_modify, *replace_this, *replace_with
            return var_create_return((var_get(objects[0])).ToString().Replace((string)var_get(objects[1]), (string)var_get(objects[2])), true);
        }

        public void lv(string Scorp_Line_Exec, ArrayList objects)
        {
            listvars(Scorp_Line_Exec, objects);
            return;
        }

        public void listvars(string Scorp_Line_Exec, ArrayList objects)
        {
            string STR_ = ""; string val = "";
            foreach (string s in Do_on.mem.AL_CURR_VAR_REF)
            {
                val = (string)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(s)])[2];
                if (val.Length > 256)
                    val = val.Remove(256) + "...";
                STR_ += "*" + s + " [" + val + "] TAG: [" + ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(s)])[3] + "]\n";
                val = null;
            }
            Do_on.write_to_cui(STR_);

            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vardelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //(*,*,*,*,*,...)
            int ndx = -1;

            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            foreach (string s_var in objects)
                            {
                                ndx = Do_on.mem.AL_CURR_VAR_REF.IndexOf(s_var);
                                Do_on.mem.AL_CURR_VAR.RemoveAt(ndx);
                                Do_on.mem.AL_CURR_VAR_REF.RemoveAt(ndx);
                                Do_on.mem.AL_CURR_VAR_TAG.RemoveAt(ndx);

                                //TRIM
                                Do_on.mem.AL_CURR_VAR.TrimToSize();
                                Do_on.mem.AL_CURR_VAR_REF.TrimToSize();
                                Do_on.mem.AL_CURR_VAR_TAG.TrimToSize();
                            }
                        }

            //clean
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void vardeleteall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.mem.AL_CURR_VAR.Clear();
            Do_on.mem.AL_CURR_VAR_REF.Clear();

            Do_on.mem.AL_CURR_VAR.TrimToSize();
            Do_on.mem.AL_CURR_VAR_REF.TrimToSize();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void varsystem(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.mem.AL_CURR_VAR.Clear();
            Do_on.mem.AL_CURR_VAR_REF.Clear();
            Do_on.mem.AL_CURR_VAR_EVT.Clear();
            Do_on.mem.AL_CURR_VAR_TAG.Clear();
            Do_on.mem.AL_CURR_VAR.TrimToSize();
            Do_on.mem.AL_CURR_VAR_REF.TrimToSize();
            Do_on.mem.AL_CURR_VAR_EVT.TrimToSize();
            Do_on.mem.AL_CURR_VAR_TAG.TrimToSize();

            Do_on.types.load_system_vars();

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //memory get
        public object var_get(ref string Block)
        {
            object o = Block;
            //BY VAR
            if (!((string)o).StartsWith("\'", StringComparison.CurrentCulture) && !((string)o).StartsWith("f\'", StringComparison.CurrentCulture))
            {
                try
                {
                    o = ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))])[2];
                }
                finally { }
            }
            //BY VALUE
            else
            {
                //Check if the string has formatted elements within it formatted strings start with an f', all values denoted between {[[, ]]} are replaced by existing variables if one is found
                if (Block.StartsWith("f\'", StringComparison.CurrentCulture))
                    Block = ef__.replace_format(ref Do_on,ref Block).Remove(0, 1);

                //Directly assign the value contained in the single quotes to the variable
                o = var_cut_str_symbol(var_cut_symbol(ref Block));
            }
            return o;
        }
    }

    public partial class Librarian
    {
        private object var_get(string Block)
        {
            return var_get(ref Block);
        }

        private object var_get(object Block)
        {
            return var_get((string)Block);
        }

        private object var_get(ref object Block)
        {
            return var_get((string)Block);
        }

        //Actions
        private string var_cut_spaces(string Var)
        {
            return Var.Replace(" ", "");
        }

        private string var_cut_symbol(ref string Var)
        {
            if (Var.StartsWith("*", System.StringComparison.CurrentCulture) == true)
                Var = Var.Replace("*", "");

            return Var;
        }

        private string var_cut_symbol(string Var)
        {
            if (Var.StartsWith("*", System.StringComparison.CurrentCulture) == true)
                Var = Var.Replace("*", "");

            return Var;
        }
        private string var_cut_str_symbol(ref string Var)
        {
            if (Var.Contains("'") == true)
                Var = Var.Replace("'", "");

            return Var;
        }

        private string var_cut_str_symbol(string Var)
        {
            if (Var.Contains("'") == true)
                return Var.Replace("'", "");
            return Var;
        }

        private void var_set(string Reference, string Variable)
        {
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = var_get(Variable);
                        }
            return;
        }

        private void var_new(object Variable, string Reference, string Type_, string Tag)
        {
            //By default all variables are created as bools with a default value of 'false'
            //(*,*,*,*,...)
            try
            {
                lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_EVT) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                            {
                                if (Do_on.mem.AL_CURR_VAR_REF.Contains(Reference) == false)
                                {
                                    //Variable = var_get(Variable.ToString());
                                    //{key, ref, val, encry, tag}
                                    var_cut_symbol(ref Reference);
                                    Do_on.mem.AL_CURR_VAR.Add(new ArrayList { "", Reference, Variable, Tag });
                                    Do_on.mem.AL_CURR_VAR_REF.Add(Reference);
                                    Do_on.mem.AL_CURR_VAR_TAG.Add(Tag);
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
    }
}
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
using System.Collections.Generic;
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
            return val;
        }

        private object var_create_return(ref Dictionary<object, object> val)
        {
            return val;
        }

        private object var_create_return(ref object[] val)
        {
            return val;
        }

        private string var_create_return(ref long val, bool is_val)
        {
            if (is_val)
                return "\'" + val + "\'";
            return Convert.ToString(val);
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
    }

    //MEMORY TAGS
    partial class Librarian
    {
        //TAG FUNCTIONS FOR CHAINING OR IDENTIFYING SESSIONS
        public void vartag(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Sets a tag element to a variable, this allows us to pool variables by an identifiable element
            //In order to remove a tag set the tag value to *null/Scorpion.Types.S_NULL
            //::*var, *tag
            if (var_get(objects[1]) is string)
                Do_on.mem.AL_CURR_VAR_TAG[Do_on.mem.AL_CURR_VAR_REF.IndexOf(objects[0])] = var_get(objects[1]);
            else
                Do_on.write_error("Could not add tag to the specified variable. Tag is not an identifyable string but an object of another type");

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public string vartagexists(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Resurns a Scorpion.BOOLEAN describing whether a tag already exists
            //*result<<::*tag

            if (Do_on.mem.AL_CURR_VAR_TAG.Contains(var_get(objects[0])))
                return Do_on.types.S_Yes;
            return Do_on.types.S_No;
        }

        public object vartagget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Returns an array of elements with a specific tag
            //*result<<::*tag

            Dictionary<object, object> result = new Dictionary<object, object>();
            int current_ndx = 0;
            foreach (string tag in Do_on.mem.AL_CURR_VAR_TAG)
            {
                current_ndx = Do_on.mem.AL_CURR_VAR_TAG.IndexOf(var_get(objects[0]), current_ndx);

                if (current_ndx == -1)
                    break;

                result.Add((string)Do_on.mem.AL_CURR_VAR_REF[current_ndx], (string)((ArrayList)(Do_on.mem.AL_CURR_VAR[current_ndx]))[2]);
                current_ndx++;
            }
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return var_create_return(ref result);
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

            var_dispose_internal(ref Scorp_Line_Exec);
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
        //Create a new or many new variables
        public void var(string Scorp_Line_Exec, ArrayList objects)
        {
            //Variadic function that creates a new variable. Variables are instantiated with the boolean value 'false'
            //Template:
            //::*var, *var, *var...
            foreach (string s in objects)
                var_new(Do_on.types.S_No, s, null, null, Do_on.types.S_No);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //Set variable as readonly
        public void varreadonly(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Change a variable to a constant variable
            //::*to_set_as_ro, *[BOOL]

            //Make sure values are of type bool
            if ((string)var_get(objects[1]) != Do_on.types.S_Yes && (string)var_get(objects[1]) != Do_on.types.S_No)
            {
                Do_on.write_error("Could not set the variable's readonly status as the value passed was incorrect: The value '" + var_get(objects[1]) + "' is not boolean ('true', 'false')");
                return;
            }
            ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString()))])[4] = var_get(objects[1]);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //Create an array variable
        public void vararray(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates a Scorpion.Array of type C#.ArrayList from an existing variable or creates a new variable. You may decide to retian the value of the variable being transformed, in which case
            //The existing variables value will be inserted at index 0

            //Template:
            //::*var, *maintain_contents[BOOL]

            //Check if variable exists if not create a new one
            if (Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString())) == -1)
                var_new(Do_on.types.S_No, var_cut_symbol(objects[0].ToString()), null, null, Do_on.types.S_No);

            //Change the variable into a Scorpion.Array
            if ((string)var_get(objects[1]) == Do_on.types.S_Yes)
                var_manipulate((string)objects[0], new ArrayList { var_get(objects[0]) }, false, false, OPCODE_SET );
            else
                var_manipulate((string)objects[0], new ArrayList { }, false, false, OPCODE_SET);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //Insert into an array variable
        public void vararrayappend(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //A variadic function which allows us to insert values into the Scorpion.Array type

            //Template:
            //::*destination_array_variable, *var, *var...

            //*destination_array_variable: the array variable that we would like to insert into
            //*var, *var...: variables that we would like to insert into the array
            object var_ = var_get(objects[0]);
            if (var_ is ArrayList)
            {
                for (int i = 1; i < objects.Count; i++)
                    var_manipulate((string)objects[0], objects[i], true, false, OPCODE_SET);
            }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vararrayinsert(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //This function allows you to insert a value into an array using an index

            //Template:
            //::*destination_array_variable, *object, *index
            var_manipulate((string)objects[0], new object[] { objects[1], objects[2] }, true, false, OPCODE_INSERT);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object vararrayget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets a value from a Scorpion.Array type and stores it into a resultant variable

            //Template:
            //*result<<::*array, *index, *asobject

            //*array: the array variable to get from
            //*index: the value at index in the array we would like to access
            //*asobject: (BOOL:true)get the value as it's original object form, (BOOL:false)get the value as a string
            object ret = Do_on.types.S_No;
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                    {
                        int ndx = Convert.ToInt32((string)var_get(objects[1]));
                        ret = ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol((string)objects[0]))])[2])[ndx];
                    }
            if ((string)var_get(objects[2]) == Do_on.types.S_No)
                return var_create_return((string)ret, true);
            return var_create_return(ref ret);
        }

        //delete var from array
        public void vararraydelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Deletes a specific value from a Scorpion.Array. One can pass an index/reference/object. Please note that numerical references will be treated as numbers

            //Template:
            //::*array, *index_or_object

            //*array: the array to delete from
            //*index_or_object: the index/reference/object to delete within the array.
            //NOTE! if a value and a variable reference coincide the value stored within the standalone variable will be taken
            //if you want to delete as value make sure to use the value delimiters ''
            var_manipulate((string)objects[0], objects[1], true, false, OPCODE_DELETE);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        //indexof
        public string vararrayindex(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets the index of an array value

            //*return::*array, *value
            //*array: the array from which to get the variable index from
            //*value: the value we want to get the indexof within the array
            string ndx = Convert.ToString(((ArrayList)var_get(objects[0])).IndexOf(var_get(objects[1])));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref ndx, true);
        }

        public void vararraysort(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Sorts a Scorpion.Array. Values are ASCII sorted

            //::*array

            ((ArrayList)var_get(objects[0])).Sort();
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        //Dictionaries
        public void vardictionary(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            /*Creates a Scorpion.Dictionary of type C#.Dictionary<string><string>
            * from an existing variable or creates a new variable.
            * If the deictionary replaces an existing variable any values that the preceding
            * variable had will not be mainained.
            */
            //Template:
            //::*var

            //Check if variable exists if not create a new one
            if (Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(objects[0].ToString())) == -1)
                var_new(Do_on.types.S_No, var_cut_symbol(objects[0].ToString()), null, null, Do_on.types.S_No);
            //Change the variable into a Scorpion.Array
            var_manipulate((string)objects[0], new Dictionary<string, string>(), false, true, OPCODE_SET);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vardictionaryappend(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Add a key pair value to an existing Scorpion.Dictionary
            //::*dictionary, *key, *value
            var_manipulate((string)objects[0], new object[] { var_get(objects[1]), var_get(objects[2]) }, false, true, OPCODE_INSERT);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object vardictionaryget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets a value from a Scorpion.Dictionary using a Scorpion.Dictionary.Key
            //*return<<::*dictionary, *key, *asobject

            object ret = Do_on.types.S_No;
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                    {
                        string key = (string)var_get(objects[1]);
                        ret = ((IDictionary)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol((string)objects[0]))])[2])[key];
                    }
            if ((string)var_get(objects[2]) == Do_on.types.S_No)
                return var_create_return((string)ret, true);
            return var_create_return(ref ret);
        }

        public void vardictionarydelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Deletes a key, value pair from a Scorpion.Dictionary using the key in order to find the value to delete
            //::*dictionary, *key

            var_manipulate((string)objects[0], objects[1], false, true, OPCODE_DELETE);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object varlength(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets the object length of a variable.
            //*return<<::*var
            long len = 0;
            object obj = var_get((string)objects[0]);
            if (obj is string)
                len = ((string)obj).Length;
            else if (obj is ArrayList)
                len = ((ArrayList)obj).Count;
            else if (obj is IDictionary)
                len = ((IDictionary)obj).Count;
            else
                len = 1;
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return var_create_return(ref len, true);
        }

        public object vartype(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets the Scorpion.Type for a specific variable
            Do_on.write_to_cui(var_get(objects[0]).GetType());

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return 0;
        }

        public void varset(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*where, *value
            //If string store the string value
            if (objects[1] is string)
            {
                try
                {
                    var_manipulate((string)objects[0], var_get((string)objects[1]), false, false, OPCODE_SET);
                }
                catch { var_manipulate((string)objects[0], objects[1], false, false, OPCODE_SET); }
            }
            //If not a string do not attempt a var_get and just store the object directly
            else
                var_manipulate((string)objects[0], objects[1], false, false, OPCODE_SET);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public string varconcatenate(ref string Scorp_Line_Exec, ArrayList objects)
        {
            /*returns<<:: *arg, *arg...*/
            string end = "";
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG)
                        {
                            foreach (object obj in objects)
                                end += var_get(obj);
                        }
            return var_create_return(ref end, true);
        }

        public string varreplace(ref string Scorp_Line_Exec, ArrayList objects)
        {
            //*returns<<::*to_modify, *replace_this, *replace_with
            return var_create_return((var_get(objects[0])).ToString().Replace((string)var_get(objects[1]), (string)var_get(objects[2])), true);
        }

        public void lv(string Scorp_Line_Exec, ArrayList objects)
        {
            listvars(Scorp_Line_Exec, objects);
            return;
        }

        public void listvars(string Scorp_Line_Exec, ArrayList objects)
        {
            string STR_ = ""; object val = null; int index = -1;
            Do_on.write_to_cui("Loaded GLOBAL variables:\n-------------------------\n");
            foreach (string s in Do_on.mem.AL_CURR_VAR_REF)
            {
                index = Do_on.mem.AL_CURR_VAR_REF.IndexOf(s);
                val = ((ArrayList)Do_on.mem.AL_CURR_VAR[index])[2];
                if (val is string)
                {
                    if (((string)val).Length > 256)
                        val = ((string)val).Remove(256) + "...";
                }
                else if(val is ArrayList)
                {
                    val = "Array: [(";
                    foreach (object o in (ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[index])[2])
                        val += " '" + o + "' ";
                    val += ")]";
                }
                else if(val is IDictionary)
                {
                    val = "Dictionary: [(";
                    foreach(DictionaryEntry de in (IDictionary)((ArrayList)Do_on.mem.AL_CURR_VAR[index])[2])
                        val += " '" + de.Key + "' : '" + de.Value + "', ";
                    val += ")]";
                }
                STR_ += "*" + s + " [" + val + "] TAG: [" + (string)Do_on.mem.AL_CURR_VAR_TAG[index] + "] READONLY: [" + (string)((ArrayList)Do_on.mem.AL_CURR_VAR[index])[4] + "] CREATION TIME (UTC) [" + ((ArrayList)Do_on.mem.AL_CURR_VAR[index])[6].ToString() + "] CACHED [" + ((ArrayList)Do_on.mem.AL_CURR_VAR[index])[5].ToString() + "]\n";
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
            foreach (string s_var in objects)
                var_manipulate(s_var, null, false, false, OPCODE_DELETE);
            //clean
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        //memory get
        public object var_get(ref string Block)
        {
            //BUILD CACHE FUNCTIONS HERE
            //
            //

            object o = Block;
            //IS OBJECT//IS BINARY//IS BIN
            if (!((string)o).StartsWith("\'", StringComparison.CurrentCulture) && !((string)o).StartsWith("f\'", StringComparison.CurrentCulture))
            {
                try
                {
                    //Assign a raw C# object regardless of type
                    o = ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(o.ToString().Replace(" ", "").Replace("*", ""))])[2];
                }
                finally { }
            }
            //IS STRING
            else
            {
                //Check if the string has formatted elements within it formatted strings start with an f', all values denoted between [{{, }}] are replaced by existing variables if one is found
                if (Block.StartsWith("f\'", StringComparison.CurrentCulture))
                    Block = ef__.replace_format(ref Do_on,ref Block).Remove(0, 1);

                //Directly assign the value contained in the single quotes to the variable, or so the string contained in *''
                o = var_cut_str_symbol(var_cut_symbol(ref Block));

                //Replace escape sequences
                o = ef__.replace_escape(ref Do_on, (string)o);
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
            if (Var.StartsWith("*", StringComparison.CurrentCulture) == true)
                Var = Var.Replace("*", "");

            return Var;
        }

        private string var_cut_symbol(string Var)
        {
            if (Var.StartsWith("*", StringComparison.CurrentCulture) == true)
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

        //SET //DELETE
        private ushort OPCODE_SET = 0x01;
        private ushort OPCODE_DELETE = 0x02;
        private ushort OPCODE_INSERT = 0x03;
        private void var_manipulate(string Reference, object Variable, bool is_array, bool is_dictionary, ushort OPCODE)
        {
            Reference = var_cut_symbol(Reference);
            lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG) lock(Do_on.mem.AL_CURR_VAR_NACESSED)
                    {
                        if ((string)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[4] == Do_on.types.S_No)
                        {
                                if (OPCODE == OPCODE_SET)
                                {
                                    if (!is_array && !is_dictionary)
                                    {
                                        try
                                        {
                                            ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = var_get(Variable);
                                        }
                                        catch
                                        {
                                            ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = Variable;
                                        }
                                    }
                                    else if (!is_array && is_dictionary)
                                            ((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = Variable;
                                    else
                                    {
                                        try
                                        {
                                            ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(var_get(Variable));
                                        }
                                        catch
                                        {
                                            ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(Variable);
                                        }
                                    }
                                }
                                else if (OPCODE == OPCODE_DELETE)
                                {
                                    if (!is_array && !is_dictionary)
                                    {
                                        int ndx = Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference);
                                        Do_on.mem.AL_CURR_VAR.RemoveAt(ndx);
                                        Do_on.mem.AL_CURR_VAR_REF.RemoveAt(ndx);
                                        Do_on.mem.AL_CURR_VAR_TAG.RemoveAt(ndx);
                                        Do_on.mem.AL_CURR_VAR_NACESSED.RemoveAt(ndx);

                                        //TRIM
                                        Do_on.mem.AL_CURR_VAR.TrimToSize();
                                        Do_on.mem.AL_CURR_VAR_REF.TrimToSize();
                                        Do_on.mem.AL_CURR_VAR_TAG.TrimToSize();
                                        Do_on.mem.AL_CURR_VAR_NACESSED.TrimToSize();
                                    }
                                    else if(!is_array && is_dictionary)
                                        ((IDictionary)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Remove(var_get(Variable));
                                    else
                                    {
                                        int ndx;
                                        bool is_number = int.TryParse((string)var_get(Variable), out ndx);
                                        if (!is_number)
                                            ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Remove(var_get(Variable));
                                        else
                                            ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).RemoveAt(ndx);
                                    }
                                }
                                else if (OPCODE == OPCODE_INSERT)
                                {
                                    if (is_array && !is_dictionary)
                                    {
                                        bool is_number = int.TryParse((string)var_get(((object[])Variable)[1]), out int ndx);
                                        if (is_number)
                                            ((ArrayList)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Insert(ndx, var_get(((object[])Variable)[0]));
                                        else
                                            Do_on.write_error("The specified index was not found: " + var_get(Variable));
                                    }
                                    else if (!is_array && is_dictionary)
                                    {
                                        object key = ((object[])Variable)[0];
                                        object value = ((object[])Variable)[1];
                                        try
                                        {
                                            ((IDictionary)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(key, var_get(value));
                                        }
                                        catch
                                        {
                                            ((IDictionary)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(key, value);
                                        }
                                    }
                                }
                        }
                        else
                            Do_on.write_error("Unable to write changes to the variable: *" + Reference + ", the variable is set to READONLY");
                    }
            return;
        }

        //Create a new variable
        private void var_new(object Variable, string Reference, string Type_, string Tag, string RONLY)
        {
            //By default all variables are created as bools with a default value of 'false'
            //(*,*,*,*,...)
            try
            {
                lock (Do_on.mem.AL_CURR_VAR) lock (Do_on.mem.AL_CURR_VAR_REF) lock (Do_on.mem.AL_CURR_VAR_TAG) lock(Do_on.mem.AL_CURR_VAR_NACESSED)
                            {
                                    if (Do_on.mem.AL_CURR_VAR_REF.Contains(Reference) == false)
                                    {
                                        //Variable = var_get(Variable.ToString());
                                        //{??, ref, val, tag, is_readonly, cached, created}
                                        var_cut_symbol(ref Reference);
                                        Do_on.mem.AL_CURR_VAR.Add(new ArrayList(7) { "", Reference, Variable, Tag, RONLY, false, DateTime.UtcNow });
                                        Do_on.mem.AL_CURR_VAR_REF.Add(Reference);
                                        Do_on.mem.AL_CURR_VAR_TAG.Add(Tag);
                                        Do_on.mem.AL_CURR_VAR_NACESSED.Add(0);
                                    }
                            }
            }
            catch { Do_on.write_to_cui("Scorpion IEE Error : Unable to Allocate Memory (Variable : '" + Variable + "', Reference : '" + Reference + "')"); }

            //clean
            Variable = null;
            Reference = null;
            Type_ = null;
            return;
        }

        private string check_readonly(string Reference)
        {
            string RONLY = (string)((ArrayList)Do_on.mem.AL_CURR_VAR[Do_on.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(Reference))])[4];
            Do_on.write_warning("The varaible: " + Reference + " results as " + RONLY);
            return RONLY;
        }
    }
}
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
                    catch { ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Memory Slot Dispose Fail: One Element"); }
                }
                al = null;
            }
            catch { ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Memory Slot Dispose Fail: Segment"); }
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

        private object var_create_return(ref Dictionary<string, string> val)
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

        private object var_create_return(ref SecureString val, bool is_val)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("A secure string is being returned to a normal scorpion memory variable, this may compromise security");
            if (is_val)
                return "\'" + val + "\'";
            return val;
        }

        private ArrayList var_create_return(ref string[] val)
        {
            return new ArrayList(val);
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

    //Escapecharacters
    partial class Librarian
    {
        public string varescape(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Replaces a string with values which cannot be properly parsed by scorpion into an escaped string
            //Ex: "Hi my name is 'Oscar'" to: "Hi my name is &qOscar&q"
            //*return<::*vartoescape
            string escaped = Enginefunctions.toEscape((string)MemoryCore.varGet(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref escaped, true);
        }

        public string varunescape(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Replaces a string's escaped values into unescaped values
            //Ex: "Hi my name is &qOscar&q" to: "Hi my name is 'Oscar'"
            //*return<::*vartounescape
            string unescaped = Enginefunctions.replaceEscape((string)MemoryCore.varGet(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref unescaped, true);
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
            if (MemoryCore.varGet(objects[1]) is string)
                Types.HANDLE.mem.AL_CURR_VAR_TAG[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf((string)objects[0])] = (string)MemoryCore.varGet(objects[1]);
            else
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("Could not add tag to the specified variable. Tag is not an identifyable string but an object of another type");

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public string vartagexists(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Resurns a Scorpion.BOOLEAN describing whether a tag already exists
            //*result<<::*tag

            if (Types.HANDLE.mem.AL_CURR_VAR_TAG.Contains(MemoryCore.varGet(objects[0])))
                return Types.S_Yes;
            return Types.S_No;
        }

        public object vartagget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Returns an array of elements with a specific tag
            //*result<<::*tag

            Dictionary<object, object> result = new Dictionary<object, object>();
            int current_ndx = 0;
            foreach (string tag in Types.HANDLE.mem.AL_CURR_VAR_TAG)
            {
                current_ndx = Types.HANDLE.mem.AL_CURR_VAR_TAG.IndexOf((string)MemoryCore.varGet(objects[0]), current_ndx);

                if (current_ndx == -1)
                    break;

                result.Add((string)Types.HANDLE.mem.AL_CURR_VAR_REF[current_ndx], (string)((ArrayList)(Types.HANDLE.mem.AL_CURR_VAR[current_ndx]))[2]);
                current_ndx++;
            }
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return var_create_return(ref result);
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
            {
                MemoryCore.var_new(Types.S_No, s, null, null, Types.S_No);
            }
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
            if ((string)MemoryCore.varGet(objects[1]) != Types.S_Yes && (string)MemoryCore.varGet(objects[1]) != Types.S_No)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("Could not set the variable's readonly status as the value passed was incorrect: The value '" + MemoryCore.varGet(objects[1]) + "' is not boolean ('true', 'false')");
                return;
            }
            ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(MemoryCore.var_cut_symbol(objects[0].ToString()))])[4] = MemoryCore.varGet(objects[1]);
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
            if (Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(MemoryCore.var_cut_symbol(objects[0].ToString())) == -1)
                MemoryCore.var_new(Types.S_No, MemoryCore.var_cut_symbol(objects[0].ToString()), null, null, Types.S_No);

            //Change the variable into a Scorpion.Array
            if ((string)MemoryCore.varGet(objects[1]) == Types.S_Yes)
                MemoryCore.var_manipulate((string)objects[0], new ArrayList { MemoryCore.varGet(objects[0]) }, false, false, MemoryCore.OPCODE_SET );
            else
                MemoryCore.var_manipulate((string)objects[0], new ArrayList { }, false, false, MemoryCore.OPCODE_SET);
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
            object var_ = MemoryCore.varGet(objects[0]);
            if (var_ is ArrayList)
            {
                for (int i = 1; i < objects.Count; i++)
                    MemoryCore.var_manipulate((string)objects[0], objects[i], true, false, MemoryCore.OPCODE_SET);
            }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vararrayinsert(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //This function allows you to insert a value into an array using an index, to refresh a 2d or 3d array set the new array in a pyramid method to the elements

            //Template:
            //::*destination_array_variable, *object, *index
            MemoryCore.var_manipulate((string)objects[0], new object[] { objects[1], objects[2] }, true, false, MemoryCore.OPCODE_INSERT);
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
            object ret = Types.S_No;
            lock (Types.HANDLE.mem.AL_CURR_VAR) lock (Types.HANDLE.mem.AL_CURR_VAR_REF) lock (Types.HANDLE.mem.AL_CURR_VAR_TAG)
                    {
                        int ndx = Convert.ToInt32((string)MemoryCore.varGet(objects[1]));
                        ret = ((ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(MemoryCore.var_cut_symbol((string)objects[0]))])[2])[ndx];
                    }
            if ((string)MemoryCore.varGet(objects[2]) == Types.S_No)
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
            MemoryCore.var_manipulate((string)objects[0], objects[1], true, false, MemoryCore.OPCODE_DELETE);
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
            string ndx = Convert.ToString(((ArrayList)MemoryCore.varGet(objects[0])).IndexOf(MemoryCore.varGet(objects[1])));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref ndx, true);
        }

        public void vararraysort(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Sorts a Scorpion.Array.
            //::*array

            ((ArrayList)MemoryCore.varGet(objects[0])).Sort();
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
            if (Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(MemoryCore.var_cut_symbol(objects[0].ToString())) == -1)
                MemoryCore.var_new(Types.S_No, MemoryCore.var_cut_symbol(objects[0].ToString()), null, null, Types.S_No);
            //Change the variable into a Scorpion.Dictionary
            MemoryCore.var_manipulate((string)objects[0], new Dictionary<string, string>(), false, true, MemoryCore.OPCODE_SET);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vardictionaryappend(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Add a key pair value to an existing Scorpion.Dictionary
            //::*dictionary, *key, *value
            MemoryCore.var_manipulate((string)objects[0], new object[] { MemoryCore.varGet(objects[1]), MemoryCore.varGet(objects[2]) }, false, true, MemoryCore.OPCODE_INSERT);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object vardictionaryget(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets a value from a Scorpion.Dictionary using a Scorpion.Dictionary.Key
            //*return<<::*dictionary, *key, *asobj

            object ret = Types.S_No;
            lock (Types.HANDLE.mem.AL_CURR_VAR) lock (Types.HANDLE.mem.AL_CURR_VAR_REF) lock (Types.HANDLE.mem.AL_CURR_VAR_TAG)
                    {
                        string key = (string)MemoryCore.varGet(objects[1]);
                        ret = ((IDictionary)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(MemoryCore.var_cut_symbol((string)objects[0]))])[2])[key];
                    }
            if ((string)MemoryCore.varGet(objects[2]) == Types.S_No)
                return var_create_return((string)ret, true);
            return var_create_return(ref ret);
        }

        public void vardictionarydelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Deletes a key, value pair from a Scorpion.Dictionary using the key in order to find the value to delete
            //::*dictionary, *key

            MemoryCore.var_manipulate((string)objects[0], objects[1], false, true, MemoryCore.OPCODE_DELETE);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public object varlength(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets the object length of a variable.
            //*return<<::*var
            long len = 0;
            object obj = MemoryCore.varGet((string)objects[0]);
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

        public void vartype(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Gets the Scorpion.Type for a specific variable
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(MemoryCore.varGet(objects[0]).GetType().Name);

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void varset(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*where, *value
            //If string store the string value
            if (objects[1] is string)
            {
                try
                {
                    MemoryCore.var_manipulate((string)objects[0], MemoryCore.varGet((string)objects[1]), false, false, MemoryCore.OPCODE_SET);
                }
                catch { MemoryCore.var_manipulate((string)objects[0], objects[1], false, false, MemoryCore.OPCODE_SET); }
            }
            //If not a string do not attempt a var_get and just store the object directly
            else
                MemoryCore.var_manipulate((string)objects[0], objects[1], false, false, MemoryCore.OPCODE_SET);
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public string varconcatenate(ref string Scorp_Line_Exec, ArrayList objects)
        {
            /*returns<<:: *arg, *arg...*/
            string end = "";
            lock (Types.HANDLE.mem.AL_CURR_VAR) lock (Types.HANDLE.mem.AL_CURR_VAR_REF) lock (Types.HANDLE.mem.AL_CURR_VAR_TAG)
                        {
                            foreach (object obj in objects)
                                end += MemoryCore.varGet(obj);
                        }
            return var_create_return(ref end, true);
        }

        public ArrayList varsplit(ref string Scorp_Line_Exec, ArrayList objects)
        {
            //*return<<:: *splitwhat, *with
            string[] s_temp = ((string)MemoryCore.varGet(objects[0])).Split((string)MemoryCore.varGet(objects[1]));
            return var_create_return(ref s_temp);
        }

        public string varreplace(ref string Scorp_Line_Exec, ArrayList objects)
        {
            //*returns<<::*to_modify, *replace_this, *replace_with
            return var_create_return((MemoryCore.varGet(objects[0])).ToString().Replace((string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[2])), true);
        }

        public void lv(string Scorp_Line_Exec, ArrayList objects)
        {
            listvars(Scorp_Line_Exec, objects);
            return;
        }

        public void listvars(string Scorp_Line_Exec, ArrayList objects)
        {
            string STR_ = ""; object val = null; int index = -1;
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Loaded GLOBAL variables:\n-------------------------\n");
            foreach (string s in Types.HANDLE.mem.AL_CURR_VAR_REF)
            {
                index = Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(s);
                val = ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[index])[2];
                if (val is string)
                {
                    if (((string)val).Length > 256)
                        val = ((string)val).Remove(256) + "...";
                }
                else if(val is ArrayList)
                {
                    val = "Array: [(";
                    foreach (object o in (ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[index])[2])
                        val += " '" + o + "' ";
                    val += ")]";
                }
                else if(val is IDictionary)
                {
                    val = "Dictionary: [(";
                    foreach(DictionaryEntry de in (IDictionary)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[index])[2])
                        val += " '" + de.Key + "' : '" + de.Value + "', ";
                    val += ")]";
                }
                STR_ += "*" + s + " [" + val + "] TAG: [" + (string)Types.HANDLE.mem.AL_CURR_VAR_TAG[index] + "] READONLY: [" + (string)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[index])[4] + "] CREATION TIME (UTC) [" + ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[index])[6].ToString() + "] CACHED [" + ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[index])[5] + "]\n";
                val = null;
            }
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(STR_);
            //clean
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }

        public void vardelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //(*,*,*,*,*,...)
            foreach (string s_var in objects)
                MemoryCore.var_manipulate(s_var, null, false, false, MemoryCore.OPCODE_DELETE);
            //clean
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}
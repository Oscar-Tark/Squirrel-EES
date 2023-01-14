/*
In Ver 2.0 Replace Arraylist with a custom object type for variables
*/

using System.Collections;

namespace Scorpion
{
    //internal functionality of scorpion not accessible by GetMethod()
    public static class MemoryCore
    {
        private static bool varNameCheck(string name)
        {
            //Returns false if the name does not conform and contains unwanted characters. The opposite if it conforms
            if(name.IndexOfAny(Types.S_UNWANTED_CHAR_NAME) != -1)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError(string.Format("Unwanted characters found in the name for a variable decleration. '{0}', '{1}' Found", Types.S_UNWANTED_CHAR_NAME[0], Types.S_UNWANTED_CHAR_NAME[1]));
                return false;
            }
            return true;
        }

        internal static object varGet(string name)
        {
            return varGet(ref name);
        }

        internal static object varGet(object name)
        {
            return varGet((string)name);
        }

        internal static object varGet(ref object name)
        {
            return varGet((string)name);
        }

        //Actions
        private static string var_cut_spaces(string Var)
        {
            return Var.Replace(" ", "");
        }

        private static string var_cut_symbol(ref string Var)
        {
            if (Var.StartsWith("*", StringComparison.CurrentCulture) == true)
                Var = Var.Replace("*", "");

            return Var;
        }

        internal static string var_cut_symbol(string Var)
        {
            if (Var.StartsWith("*", StringComparison.CurrentCulture) == true)
                Var = Var.Replace("*", "");

            return Var;
        }
        private static string var_cut_str_symbol(ref string Var)
        {
            if (Var.Contains("'") == true)
                Var = Var.Replace("'", "");

            return Var;
        }

        private static string var_cut_str_symbol(string Var)
        {
            if (Var.Contains("'"))
                return Var.Replace("'", String.Empty);
            else
                return Var;
        }

        //SET //DELETE
        public static ushort OPCODE_SET     =   0x01;
        public static ushort OPCODE_DELETE  =   0x02;
        public static ushort OPCODE_INSERT  =   0x03;
        public static ushort OPCODE_MERGE   =   0x04;
        internal static void var_manipulate(string Reference, object Variable, bool is_array, bool is_dictionary, ushort OPCODE)
        {
            Reference = var_cut_symbol(Reference);
            lock (Types.HANDLE.mem.AL_CURR_VAR) lock (Types.HANDLE.mem.AL_CURR_VAR_REF) lock (Types.HANDLE.mem.AL_CURR_VAR_TAG) lock(Types.HANDLE.mem.AL_CURR_VAR_NACESSED)
                    {
                        if ((string)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[4] == Types.S_No)
                        {
                                if (OPCODE == OPCODE_SET)
                                {
                                    if (!is_array && !is_dictionary)
                                    {
                                        try
                                        {
                                            ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = varGet(Variable);
                                        }
                                        catch
                                        {
                                            ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = Variable;
                                        }
                                    }
                                    else if (!is_array && is_dictionary)
                                            ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = Variable;
                                    else
                                    {
                                        try
                                        {
                                            ((ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(varGet(Variable));
                                        }
                                        catch
                                        {
                                            ((ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(Variable);
                                        }
                                    }
                                }
                                else if (OPCODE == OPCODE_DELETE)
                                {
                                    if (!is_array && !is_dictionary)
                                    {
                                        int ndx = Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference);
                                        Types.HANDLE.mem.AL_CURR_VAR.RemoveAt(ndx);
                                        Types.HANDLE.mem.AL_CURR_VAR_REF.RemoveAt(ndx);
                                        Types.HANDLE.mem.AL_CURR_VAR_TAG.RemoveAt(ndx);
                                        Types.HANDLE.mem.AL_CURR_VAR_NACESSED.RemoveAt(ndx);
                                    }
                                    else if(!is_array && is_dictionary)
                                        ((IDictionary)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Remove(varGet(Variable));
                                    else
                                    {
                                        int ndx;
                                        bool is_number = int.TryParse((string)varGet(Variable), out ndx);
                                        if (!is_number)
                                            ((ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Remove(varGet(Variable));
                                        else
                                            ((ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).RemoveAt(ndx);
                                    }
                                }
                                else if (OPCODE == OPCODE_INSERT)
                                {
                                    if (is_array && !is_dictionary)
                                    {
                                        bool is_number = int.TryParse((string)varGet(((object[])Variable)[1]), out int ndx);
                                        if (is_number)
                                            ((ArrayList)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Insert(ndx, varGet(((object[])Variable)[0]));
                                        else
                                            ScorpionConsoleReadWrite.ConsoleWrite.writeError("The specified index was not found: " + varGet(Variable));
                                    }
                                    else if (!is_array && is_dictionary)
                                    {
                                        object key = ((object[])Variable)[0];
                                        object value = ((object[])Variable)[1];
                                        try
                                        {
                                            ((IDictionary)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(key, varGet(value));
                                        }
                                        catch
                                        {
                                            ((IDictionary)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]).Add(key, value);
                                        }
                                    }
                                }
                                else if (OPCODE == OPCODE_MERGE)
                                {
                                    if(is_dictionary)
                                    {
                                        ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = mergeDictionaries(((Dictionary<string, string>)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(Reference)])[2]), (Dictionary<string, string>)Variable);
                                    }
                                }
                        }
                        else
                            ScorpionConsoleReadWrite.ConsoleWrite.writeError("Unable to write changes to the variable: *" + Reference + ", the variable is set to READONLY");
                    }
            return;
        }

        //Create a new variable
        public static void var_new(object Variable, string Reference, string Type_, string Tag, string RONLY)
        {
            //By default all variables are created as bools with a default value of 'false'
            //(*,*,*,*,...)
            try
            {
                Reference = Enginefunctions.CleanEscape(Reference);

                //Check if the name has unwanted characters
                if(!varNameCheck(Reference))
                    return;

                lock (Types.HANDLE.mem.AL_CURR_VAR) lock (Types.HANDLE.mem.AL_CURR_VAR_REF) lock (Types.HANDLE.mem.AL_CURR_VAR_TAG) lock(Types.HANDLE.mem.AL_CURR_VAR_NACESSED)
                            {
                                    if (Types.HANDLE.mem.AL_CURR_VAR_REF.Contains(Reference) == false)
                                    {
                                        //Variable = varGet(Variable.ToString());
                                        //{??, ref, val, tag, is_readonly, cached, created}
                                        var_cut_symbol(ref Reference);
                                        Types.HANDLE.mem.AL_CURR_VAR.Add(new ArrayList(7) { "", Reference, Variable, Tag, RONLY, false, DateTime.UtcNow });
                                        Types.HANDLE.mem.AL_CURR_VAR_REF.Add(Reference);
                                        Types.HANDLE.mem.AL_CURR_VAR_TAG.Add(Tag);
                                        Types.HANDLE.mem.AL_CURR_VAR_NACESSED.Add(DateTime.UtcNow);
                                    }
                            }
            }
            catch { ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Scorpion IEE Error : Unable to Allocate Memory (Variable : '" + Variable + "', Reference : '" + Reference + "')"); }

            //clean
            Variable = null;
            Reference = null;
            Type_ = null;
            return;
        }

        //Dictionaries
        public static Dictionary<string, string> mergeDictionaries(Dictionary<string, string> dictionary_1, Dictionary<string, string> dictionary_2)
        {
            foreach(KeyValuePair<string, string> kvp in dictionary_2)
            {
                if(!dictionary_1.ContainsKey(kvp.Key))
                    dictionary_1.Add(kvp.Key, kvp.Value);
            }
            return dictionary_1;
        }

        private static string check_readonly(string Reference)
        {
            string RONLY = (string)((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(Reference))])[4];
            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("The varaible: " + Reference + " results as " + RONLY);
            return RONLY;
        }

        //Get a variable from the main memory block
        internal static object varGet(ref string name)
        {
            object o = null;
            string block_with_depth = name;
            string block_without_depth = name;
            bool contains_depth = false;

            //If contains []
            if(name.Contains(Types.S_UNWANTED_CHAR_NAME[0]) || name.Contains(Types.S_UNWANTED_CHAR_NAME[1]))
            {
                block_without_depth = name.Remove(name.IndexOf(Types.S_UNWANTED_CHAR_NAME[0]));
                contains_depth = true;
            }

            //IS OBJECT|IS BINARY|IS BIN
            if (!block_without_depth.StartsWith("\'", StringComparison.CurrentCulture) && !(block_without_depth).StartsWith("f\'", StringComparison.CurrentCulture))
            {
                try
                {
                    //Assign a raw C# object regardless of type
                    o = ((ArrayList)Types.HANDLE.mem.AL_CURR_VAR[Types.HANDLE.mem.AL_CURR_VAR_REF.IndexOf(block_without_depth.Replace(" ", "").Replace("*", ""))])[2];

                    if(contains_depth)
                        o = varCheckGetDepth(block_with_depth, o, o.GetType(), true);
                }
                finally { }
            }

            //IS STRING
            else
            {
                //Check if the string has formatted elements within it formatted strings start with an f', all values denoted between [{{, }}] are replaced by existing variables if one is found
                if (block_without_depth.StartsWith("f\'", StringComparison.CurrentCulture))
                    block_without_depth= Enginefunctions.replace_format(ref block_with_depth).Remove(0, 1);

                //Directly assign the value contained in the single quotes to the variable, or so the string contained in *''
                o = var_cut_str_symbol(var_cut_symbol(ref block_without_depth));

                //Replace escape sequences
                o = Enginefunctions.replaceEscape((string)o);
            }
            return o;
        }

        private static object varCheckGetDepth(string block, object o_array, Type t_obj_type, bool containsdepth)
        {
            //Only for arrays not dictionaries. Implement depth for mixed types in the future
            object returnable = o_array;
            //If index must be array or dictionary
            try
            {
                if(containsdepth)
                {
                    //if array[n]
                    if(t_obj_type == Types.S_TYPES[0])
                    {
                        //Get the index at array point
                        string[] s_indexes = block.Split(Types.S_UNWANTED_CHAR_NAME, StringSplitOptions.RemoveEmptyEntries);
                        int[] indexes = ParsingCore.toIntArray(s_indexes);

                        if(indexes.Length > 0)
                        {
                            //Get the first index element
                            returnable = ((ArrayList)o_array)[indexes[0]];

                            //Single depth
                            if(indexes.Length == 1)
                                return returnable;

                            //Multiple depths
                            else
                            {
                                //Temporary array and dictionary
                                ArrayList temp_array = (ArrayList)returnable;

                                //Get multiple depths with multitypes
                                for(int i = 0; i < indexes.Length - 1; i++)
                                {
                                    //if not at end of indexes contiue as arraylists else take object
                                    if(i < (indexes.Length - 2))
                                        temp_array = (ArrayList)temp_array[Convert.ToInt32(indexes[i+1])];
                                    else
                                        returnable = temp_array[indexes[i+1]];
                                }
                            }
                        }
                    }
                    //If Dict<string, string>
                    else if(t_obj_type == Types.S_TYPES[1])
                    {
                        //Get the key at dictionary point
                        block = block.Remove(0, block.IndexOf('[', 0));
                        string[] s_indexes = block.Split(Types.S_UNWANTED_CHAR_NAME, StringSplitOptions.RemoveEmptyEntries);

                        if(s_indexes.Length > 0)
                        {
                            //Get the first index element
                            string tryget_temp = string.Empty;
                            ((Dictionary<string, string>)o_array).TryGetValue(s_indexes[0], out tryget_temp);
                            returnable = ((string)tryget_temp);

                            //Single depth
                            if(s_indexes.Length == 1)
                                return returnable;
                            else
                                ScorpionConsoleReadWrite.ConsoleWrite.writeExperimental("Not implemented, feel free to implement!");
                        }
                    }
                    else //Assume string
                    {
                        string[] s_indexes = block.Split(Types.S_UNWANTED_CHAR_NAME, StringSplitOptions.RemoveEmptyEntries);
                        int[] indexes = ParsingCore.toIntArray(s_indexes);

                        return ((string)o_array)[indexes[0]];
                    }
                }
            }
            catch(Exception e){ ScorpionConsoleReadWrite.ConsoleWrite.writeError(e.StackTrace); }
            return returnable;
        }

        internal static bool varCheck(string reference)
        {
            return Types.HANDLE.mem.AL_CURR_VAR_REF.Contains(reference);
        }

        //memory get
        internal static string varGetCustomFormattedOnlyDictionary(ref string Block, ref string reference)
        {
            //source: [reference, value]
            string temp_var = Block;
            Dictionary<string, string> dictionary_temp = (Dictionary<string, string>)varGet(ref reference);
            Block = Enginefunctions.replaceFormatCustomSourceDictionary(ref Block, ref dictionary_temp);
            
            //Directly assign the value contained in the single quotes to the variable, or so the string contained in *''
            temp_var = var_cut_str_symbol(var_cut_symbol(ref Block));
            //Replace escape sequences
            return Enginefunctions.replaceEscape((string)temp_var);
        }
    }
}
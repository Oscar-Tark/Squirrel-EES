

using System.Collections;

//Static Library
using System;
namespace Scorpion
{
    partial class Librarian
    {
        public void output(ref string Scorp_Line_Exec, ArrayList Objects)
        {
            //::*var, *var..
            string writable = ""; object temp;
            foreach (string reference in Objects)
            {
                if ((temp = MemoryCore.varGet(reference)) is ArrayList)
                {
                    try
                    {
                        writable += "Array: [(";
                        foreach (object internal_obj in (ArrayList)temp)
                            writable += " '" + internal_obj + "' ";
                        writable += ")]";
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                else if ((temp = MemoryCore.varGet(reference)) is IDictionary)
                {
                    try
                    {
                        writable += "Dictionary: [(";
                        foreach (DictionaryEntry internal_obj in (IDictionary)temp)
                            writable += " '" + internal_obj.Key + "' : '" + internal_obj.Value + "' ";
                        writable += ")]";
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                else
                    writable += MemoryCore.varGet(reference);
            }

            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(writable);

            var_arraylist_dispose(ref Objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return;
        }
    }
}
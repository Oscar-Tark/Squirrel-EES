using System.Collections;

namespace Scorpion
{
    public static class Enginefunctions
    {
        public static string[] execution_seperation(ref string Scorp)
        {
            //You can add multiple functions to an execution with the <<< or >>> symbols. >>> means execute rightwards <<< means execute leftwards
            string[] delimiterChars = { ">>" };
            string[] commands = Scorp.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < commands.Length; i++)
                commands[i] = commands[i].Trim();
            return commands;
        }

        public static string getFunction(ref string Scorp)
        {
            string[] delimiterChars = { "::" };
            return Scorp.ToLower().Replace(" ", "").Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public static string[] get_return(ref string Scorp)
        {
            string[] delimiterChars = { "<<" };
            return Scorp.Split(delimiterChars, StringSplitOptions.None);
        }

        //OLD DEPRECIATE TO: replace_escape
        public static string replace_fakes(string Scorp_Line)
        {
            return Scorp_Line.Replace("{&v}", "*").Replace("{&q}", "'").Replace("{&r}", ">>").Replace("{&l}", "<<").Replace("{&c}", "::").Replace("{&u}", ",");
        }

        //OLD DEPRECIATE TO: toEscape()
        public static string create_fakes(string Scorp)
        {
            //return Scorp.Replace();
            return Scorp.Replace("*", "{&v}").Replace("'", "{&q}").Replace(">>", "{&r}").Replace("<<", "{&l}").Replace("::", "{&c}").Replace(",", "{&u}");
        }

        public static string replaceEscape(string paramse)
        {
            foreach (string[] esc_arr in Types.S_ESCAPE_SEQUENCES)
                paramse = paramse.Replace(esc_arr[0], esc_arr[1]);
            return paramse;
        }

        public static string toEscape(string Scorp)
        {
            foreach (string[] esc_arr in Types.S_ESCAPE_SEQUENCES)
                Scorp = Scorp.Replace(esc_arr[1], esc_arr[0]);
            return Scorp;
        }

        public static string CleanEscape(string Scorp)
        {
            foreach (string[] esc_arr in Types.S_ESCAPE_SEQUENCES)
                Scorp = Scorp.Replace(esc_arr[1], "");
            return Scorp;
        }

        //Acts as a Python style formatted string, works by scanning variables themselves for formats and replaces variables instantly
        public static string replace_format(ref string var)
        {
            //f'Hi my name is {((name))}'
            string to_change = "";
            string[] vars = var.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < vars.Length; i++)
            {
                if (vars[i].StartsWith("((", StringComparison.CurrentCulture) && vars[i].EndsWith("))", StringComparison.CurrentCulture))
                {
                    to_change = vars[i].Replace("((", "*").Replace("))", "");
                    var = var.Replace("{" + vars[i] + "}", (string)MemoryCore.varGet(ref to_change));
                }
            }
            return var;
        }

        public static string replaceFormatCustomSourceDictionary(ref string var, ref Dictionary<string, string> source)
        {
            //f'Hi my name is {[[name]]}'
            string to_change = "";
            string[] vars = var.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            string temp_var = null;

            for (int i = 0; i < vars.Length; i++)
            {
                if (vars[i].StartsWith("((", StringComparison.CurrentCulture) && vars[i].EndsWith("))", StringComparison.CurrentCulture))
                {
                    to_change = vars[i].Replace("((", "").Replace("))", "");
                    source.TryGetValue(to_change, out temp_var);
                    var = var.Replace("{" + vars[i] + "}", temp_var);
                }
            }
            return var;
        }

        public static string remove_commented(ref string Scorp_Line)
        {
            //Removes comments denoted by '###' in a line of code or a comment line.
            return Scorp_Line.Remove(Scorp_Line.IndexOf("###", 0, StringComparison.CurrentCulture));
        }

        public static string line_check(ref Scorp HANDLE, ref string Scorp)
        {
            return HANDLE.san.sanitize(ref Scorp);
        }

        public static bool process_return(ref object return_object, ref string var, Librarian lib)
        {
            lib.varset("", new ArrayList { var.Replace("*", ""), return_object });
            return true;
        }
    }
}
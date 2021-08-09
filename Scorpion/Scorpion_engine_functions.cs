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
using System.Reflection;
using System.Threading;

namespace Scorpion
{
    public class Enginefunctions
    {
        public string[] execution_seperation(ref string Scorp)
        {
            //You can add multiple functions to an execution with the <<< or >>> symbols. >>> means execute rightwards <<< means execute leftwards
            string[] delimiterChars = { ">>" };
            string[] commands = Scorp.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < commands.Length; i++)
                commands[i] = commands[i].Trim();
            return commands;
        }

        public string[] get_functions(ref string Scorp)
        {
            string[] delimiterChars = { "::" };
            return Scorp.ToLower().Replace(" ", "").Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[] get_return(ref string Scorp)
        {
            string[] delimiterChars = { "<<" };
            return Scorp.Split(delimiterChars, StringSplitOptions.None);
        }

        //OLD DEPRECIATE TO: replace_escape
        public string replace_fakes(string Scorp_Line)
        {
            return Scorp_Line.Replace("{&v}", "*").Replace("{&q}", "'").Replace("{&r}", ">>").Replace("{&l}", "<<").Replace("{&c}", "::");
        }

        public string create_fakes(string Scorp)
        {
            //return Scorp.Replace();
            return Scorp.Replace("*", "{&v}").Replace("'", "{&q}").Replace(">>", "{&r}").Replace("<<", "{&l}").Replace("::", "{&c}");
        }

        public string replace_escape(ref Scorp HANDLE, string paramse)
        {
            foreach (string[] esc_arr in HANDLE.types.S_ESCAPE_SEQUENCES)
                paramse = paramse.Replace(esc_arr[0], esc_arr[1]);
            return paramse;
        }

        //Acts as a Python style formatted string, works by scanning variables themselves for formats and replaces variables instantly
        public string replace_format(ref Scorp HANDLE, ref string var)
        {
            //f'Hi my name is {[[name]]}'
            string to_change = "";
            string[] vars = var.Split(new char[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < vars.Length; i++)
            {
                if (vars[i].StartsWith("[[", StringComparison.CurrentCulture) && vars[i].EndsWith("]]", StringComparison.CurrentCulture))
                {
                    to_change = vars[i].Replace("[[", "*").Replace("]]", "");
                    var = var.Replace("{" + vars[i] + "}", (string)HANDLE.readr.lib_SCR.var_get(ref to_change));
                }
            }
            return var;
        }

        public string replace_telnet(string Scorp_Line)
        {
            return Scorp_Line.Replace("\r\n", "").Replace("959;1R", "");
        }

        public string remove_commented(ref string Scorp_Line)
        {
            //Removes comments denoted by '###' in a line of code or a comment line.
            return Scorp_Line.Remove(Scorp_Line.IndexOf("###", 0, StringComparison.CurrentCulture));
        }

        public string replace_phpapi(string Scorp_Line)
        {
            Scorp_Line = Scorp_Line.Remove(Scorp_Line.IndexOf("{&scorpion_end}", StringComparison.CurrentCulture));
            return Scorp_Line.Remove(0, (Scorp_Line.IndexOf("{&scorpion}", StringComparison.CurrentCulture) + 11));
        }

        public string line_check(ref Scorp HANDLE, ref string Scorp)
        {
            return HANDLE.san.sanitize(ref Scorp);
        }

        public bool process_return(ref object return_object, ref string var, Librarian lib)
        {
            lib.varset("", new ArrayList { var.Replace("*", ""), return_object });
            return true;
        }
    }
}
//#
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

        public string getFunction(ref string Scorp)
        {
            string[] delimiterChars = { "::" };
            return Scorp.ToLower().Replace(" ", "").Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public string[] get_return(ref string Scorp)
        {
            string[] delimiterChars = { "<<" };
            return Scorp.Split(delimiterChars, StringSplitOptions.None);
        }

        //OLD DEPRECIATE TO: replace_escape
        public string replace_fakes(string Scorp_Line)
        {
            return Scorp_Line.Replace("{&v}", "*").Replace("{&q}", "'").Replace("{&r}", ">>").Replace("{&l}", "<<").Replace("{&c}", "::").Replace("{&u}", ",");
        }

        //OLD DEPRECIATE TO: toEscape()
        public string create_fakes(string Scorp)
        {
            //return Scorp.Replace();
            return Scorp.Replace("*", "{&v}").Replace("'", "{&q}").Replace(">>", "{&r}").Replace("<<", "{&l}").Replace("::", "{&c}").Replace(",", "{&u}");
        }

        public string replaceEscape(ref Scorp HANDLE, string paramse)
        {
            foreach (string[] esc_arr in HANDLE.types.S_ESCAPE_SEQUENCES)
                paramse = paramse.Replace(esc_arr[0], esc_arr[1]);
            return paramse;
        }

        public string toEscape(ref Scorp HANDLE, string Scorp)
        {
            foreach (string[] esc_arr in HANDLE.types.S_ESCAPE_SEQUENCES)
                Scorp = Scorp.Replace(esc_arr[1], esc_arr[0]);
            return Scorp;
        }

        public static string CleanEscape(ref Scorp HANDLE, string Scorp)
        {
            foreach (string[] esc_arr in HANDLE.types.S_ESCAPE_SEQUENCES)
                Scorp = Scorp.Replace(esc_arr[1], "");
            return Scorp;
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

        public string remove_commented(ref string Scorp_Line)
        {
            //Removes comments denoted by '###' in a line of code or a comment line.
            return Scorp_Line.Remove(Scorp_Line.IndexOf("###", 0, StringComparison.CurrentCulture));
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

    public class NetworkEngineFunctions
    {
        private static readonly Dictionary<string, string[]> api = new Dictionary<string, string[]> 
        {
            { "scorpion", new string[]{ "{&scorpion}", "{&/scorpion}" } },
            { "database", new string[]{ "{&database}", "{&/database}" } },
            { "type", new string[] {"{&type}", "{&/type}" } },
            { "tag", new string[] {"{&tag}", "{&/tag}" } },
            { "subtag", new string[] {"{&subtag}", "{&/subtag}" } },
            { "data", new string[] {"{&data}", "{&/data}" } },
            { "status", new string[] {"{&status}", "{&/status}" } },
                };

        public readonly Dictionary<string, string> api_requests = new Dictionary<string, string>
        {
            { "get", "get" },
            { "set", "set" },
            { "response", "response" }
                };

        private readonly Dictionary<string, string> api_result = new Dictionary<string, string>
        {
            { "ok", "ok" },
            { "error", "error" }
        };

        public Dictionary<string, string> replace_api(string Scorp_Line)
        {
            Scorp_Line = Scorp_Line.Remove(0, Scorp_Line.IndexOf(api["scorpion"][0], StringComparison.CurrentCulture));
            if (Scorp_Line.Contains(api["scorpion"][0]) && Scorp_Line.Contains(api["scorpion"][1]))
            {
                //Split other elements
                //Get the app
                string[] db, tag, subtag, type;
                type = Scorp_Line.Split(api["type"], StringSplitOptions.RemoveEmptyEntries);
                db = Scorp_Line.Split(api["database"], StringSplitOptions.RemoveEmptyEntries);
                tag = Scorp_Line.Split(api["tag"], StringSplitOptions.RemoveEmptyEntries);
                subtag = Scorp_Line.Split(api["subtag"], StringSplitOptions.RemoveEmptyEntries);
                return new Dictionary<string, string> { { "type", type[1] }, { "db", db[1] }, { "tag", tag[1] }, { "subtag", subtag[1] } };
            }
            return null;
        }

        public string build_api(string data, bool error)
        {
            if (!error)
                return api["scorpion"][0] + api["type"][0] + api_requests["response"] + api["type"][1] + api["data"][0] + data + api["data"][1] + api["status"][0] + api_result["ok"] + api["status"][1] + api["scorpion"][1];
            return api["scorpion"][0] + api["type"][0] + api_requests["response"] + api["type"][1] + api["data"][0] + data + api["data"][1] + api["status"][0] + api_result["error"] + api["status"][1] + api["scorpion"][1];
        }

        public string replace_telnet(string Scorp_Line)
        {
            return Scorp_Line.Replace("\r\n", "").Replace("959;1R", "");
        }
    }
}
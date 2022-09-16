

using System;
using System.Collections;
using System.Collections.Generic;
using ScorpionConsoleReadWrite;

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

    public static class NetworkEngineFunctions
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
            { "session", new string[] {"{&session}", "{&/session}" } },
            { "includedata", new string[] {"{&includedata}", "{&/includedata}" } },
        };

        public static readonly Dictionary<string, string> api_requests = new Dictionary<string, string>
        {
            { "get", "get" },
            { "set", "set" },
            { "delete", "delete" },
            { "response", "response" }
        };

        private static readonly Dictionary<string, string> api_result = new Dictionary<string, string>
        {
            { "ok", "ok" },
            { "error", "error" }
        };

        public static Dictionary<string, string> replace_api(string Scorp_Line)
        {
            Scorp_Line = Scorp_Line.Remove(0, Scorp_Line.IndexOf(api["scorpion"][0], StringComparison.CurrentCulture));
            if (Scorp_Line.Contains(api["scorpion"][0]) && Scorp_Line.Contains(api["scorpion"][1]))
            {
                //Split other elements
                //Get the app
                string[] db, tag, subtag, type, session, inlcudedata;
                type = Scorp_Line.Split(api["type"], StringSplitOptions.RemoveEmptyEntries);
                db = Scorp_Line.Split(api["database"], StringSplitOptions.RemoveEmptyEntries);
                tag = Scorp_Line.Split(api["tag"], StringSplitOptions.RemoveEmptyEntries);
                subtag = Scorp_Line.Split(api["subtag"], StringSplitOptions.RemoveEmptyEntries);
                session = Scorp_Line.Split(api["session"], StringSplitOptions.RemoveEmptyEntries);
                inlcudedata = Scorp_Line.Split(api["includedata"], StringSplitOptions.RemoveEmptyEntries);
                return new Dictionary<string, string> { { "type", type[1] }, { "db", db[1] }, { "tag", tag[1] }, { "subtag", subtag[1] }, { "session", session[1] }, { "includedata", inlcudedata[1] } };
            }
            return null;
        }

        public static string build_api(string data, string session, bool error)
        {
            if (!error)
                return api["scorpion"][0] + api["type"][0] + api_requests["response"] + api["type"][1] + api["session"][0] + session + api["session"][1] + api["data"][0] + data + api["data"][1] + api["status"][0] + api_result["ok"] + api["status"][1] + api["scorpion"][1];
            return api["scorpion"][0] + api["type"][0] + api_requests["response"] + api["type"][1] + api["data"][0] + data + api["data"][1] + api["status"][0] + api_result["error"] + api["status"][1] + api["scorpion"][1];
        }

        public static string replace_telnet(string Scorp_Line)
        {
            return Scorp_Line.Replace("\r\n", "").Replace("959;1R", "");
        }
    }
}
/*  <Scorpion IEE(Intelligent Execution Environment)>
    Copyright (C) <2014+>  <Oscar Arjun Singh Tark>

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

namespace Scorpion
{
    public class Types
    {
        public readonly string S_Yes = "true";
        public readonly string S_No = "false";
        public readonly string S_NULL = "";
        public readonly string[][] S_ESCAPE_SEQUENCES = { new string[] { "{&c}", "," }, new string[] { "{&v}", "*" }, new string[] { "{&q}", "'" }, new string[] { "{&r}", ">>" }, new string[] { "{&l}", "<<" }, new string[] { "{&d}", "::" }, new string[] { "{&fl}", "{[[" } , new string[] { "{&fr}", "]]}" } };

        Scorp HANDLE;
        public Types(Scorp HANDLE_)
        {
            HANDLE = HANDLE_;
            return;
        }

        public void load_system_vars()
        {
            HANDLE.readr.lib_SCR.var("", new ArrayList(5) { "true", "false", "null", "yes", "no", "temp", "path" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "false", "'" + S_No + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "no", "'" + S_No + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "null", "'" + S_NULL + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "path", "'" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "'/Scorpion" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "true", "'" + S_Yes + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "yes", "'" + S_Yes + "'" });
            return;
        }

        //Conversions
        public bool Convert_String_To_Bool(string YN)
        {
            if (YN.ToLower() == "yes" || YN.ToLower() == "true")
                return true;
            //CLEAN
            YN = null;
            return false;
        }

        public string Convert_Bool_To_String(bool bool_)
        {
            switch(bool_)
            {
                case true:
                    return S_Yes;
                default:
                    return S_No;
            }
        }
    }
}
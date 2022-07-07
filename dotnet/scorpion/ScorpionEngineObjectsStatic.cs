//#
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
        //Available Scorpion.Hardtypes
        public readonly string S_Yes = bool.TrueString.ToLower();
        public readonly string S_No = bool.FalseString.ToLower();
        public readonly string S_NULL = "";
        public readonly string C_S_NULL = "\0";
        public readonly string main_user_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion";
        public readonly string main_user_projects_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/Projects";
        public readonly string main_user_manuals_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/Manuals";

        //Available Scorpion.EscapeSequences
        public readonly string[][] S_ESCAPE_SEQUENCES = { new string[] { "{&c}", "," }, new string[] { "{&v}", "*" }, new string[] { "{&q}", "'" }, new string[] { "{&r}", ">>" }, new string[] { "{&l}", "<<" }, new string[] { "{&d}", "::" }, new string[] { "{&fl}", "{[[" } , new string[] { "{&fr}", "]]}" }, new string[] { "{&u}", "," } };

        //Unwanted characters in names
        public char[] S_UNWANTED_CHAR_NAME = { '[', ']' };

        //Available Scorpion.Types
        public readonly Type[] S_TYPES = { new ArrayList().GetType() };

        //Available Scorpion.Handle that can be passed between classes to access C#.Scorp
        Scorp HANDLE;
        public Types(Scorp HANDLE_)
        {
            HANDLE = HANDLE_;
            return;
        }

        
        [Obsolete] //Should be initialized immediately on memory structure creation, there is a major bug with S_YES
        //Initialize Scorpion.DefaultVariables
        public void LoadSystemVars()
        {
            //Set variables that should exist by default
            HANDLE.readr.lib_SCR.var("", new ArrayList(5) { S_Yes, S_No, "null", "yes", "no", "temp", "path" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "yes", "'" + S_Yes + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "no", "'" + S_No + "'" });

            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "null", "'" + S_NULL + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "path", "'" + main_user_path + "'" });
            HANDLE.readr.lib_SCR.varset("", new ArrayList(5) { "true", "'" + S_Yes + "'" });
            return;
        }
    }
}
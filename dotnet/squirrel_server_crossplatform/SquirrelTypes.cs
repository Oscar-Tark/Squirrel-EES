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
    public static class Types
    {
        //Available Scorpion.Hardtypes
        public static string S_Yes = bool.TrueString.ToLower();
        public static string S_No = bool.FalseString.ToLower();
        public static string S_NULL = "";
        public static string C_S_NULL = "\0";
        public static string main_user_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion";
        public static string main_user_projects_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/Projects";
        public static string main_user_manuals_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/Manuals";
        public static string main_user_aes_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/AES";
        public static string main_user_aes_path_file = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/AES/aes.ky";
        public static string main_user_rsa_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Scorpion/RSA";

        public static Scorp HANDLE;
        public static short READ_SIGNAL_OFF = 0x00;
        public static short READ_SIGNAL_ON  = 0x01;
        public static short READ_SIGNAL_CURRENT = READ_SIGNAL_ON;

        //Available Scorpion.EscapeSequences !DO NOT INSERT ONLY APPEND
        public static string[][] S_ESCAPE_SEQUENCES = { new string[] { "{&c}", "," }, new string[] { "{&v}", "*" }, new string[] { "{&q}", "'" }, new string[] { "{&r}", ">>" }, new string[] { "{&l}", "<<" }, new string[] { "{&d}", "::" }, new string[] { "{&fl}", "{((" } , new string[] { "{&fr}", "))}" }, new string[] { "{&u}", "," } };

        //Unwanted characters in names
        public static char[] S_UNWANTED_CHAR_NAME = { '[', ']' };

        //Available Scorpion.Types
        public static Type[] S_TYPES = { new ArrayList().GetType() };

        public static string S_ROOT_LINUX = "sudo";
    }
}
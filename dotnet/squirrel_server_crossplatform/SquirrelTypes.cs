using System;
using System.Collections;
using SquirrelDefaultPaths;

namespace Scorpion
{
    public static class Types
    {
        //Available Scorpion.Hardtypes
        public static string S_Yes = bool.TrueString.ToLower();
        public static string S_No = bool.FalseString.ToLower();
        public static string S_NULL = "";
        public static string C_S_NULL = "\0";
        public static string main_user_path = SquirrelPaths.main_user_path;
        public static string main_user_projects_path = SquirrelPaths.main_user_projects_path;
        public static string main_user_manuals_path = SquirrelPaths.main_user_manuals_path;
        public static string main_user_aes_path = SquirrelPaths.main_user_aes_path;
        public static string main_user_aes_path_file = SquirrelPaths.main_user_aes_path_file;
        public static string main_user_rsa_path = SquirrelPaths.main_user_rsa_path;

        public static Scorp HANDLE;
        public static short READ_SIGNAL_OFF = 0x00;
        public static short READ_SIGNAL_ON  = 0x01;
        public static short READ_SIGNAL_CURRENT = READ_SIGNAL_ON;

        //Available Scorpion.EscapeSequences !DO NOT INSERT ONLY APPEND
        public static string[][] S_ESCAPE_SEQUENCES = { new string[] { "{&c}", "," }, new string[] { "{&v}", "*" }, new string[] { "{&q}", "'" }, new string[] { "{&r}", ">>" }, new string[] { "{&l}", "<<" }, new string[] { "{&d}", "::" }, new string[] { "{&fl}", "{((" } , new string[] { "{&fr}", "))}" }, new string[] { "{&u}", "," } };

        //Unwanted characters in names
        public static char[] S_UNWANTED_CHAR_NAME = { '[', ']' };

        //Available Scorpion.Types
        public static Type[] S_TYPES = { new ArrayList().GetType(), new Dictionary<string, string>().GetType() };

        public static string S_ROOT_LINUX = "sudo";
    }
}
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
using System.Reflection;
using System.Threading;

namespace Scorpion
{
    public partial class Librarian
    {
        private Enginefunctions ef__ = new Enginefunctions();
        System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();

        public Librarian(Scorp Form_Handle)
        {
            //Start the class and add the main instance handle so that class.Librarian can access elements or cross elements from class.Scorp
            Do_on = Form_Handle;
            return;
        }

        public void scorpioniee(object Scorp_Line, Scorp Handle)
        {
            //This function is not used internally from class.Librarian but rather from an external class such as class.Scorp. This helps thread execution
            //*****
            //The class.Scorp handle is checked to see weather it has an instance associeted to it
            if(Handle != null)
                Do_on = Handle;
            //Start thread for the single line of interpretation code
            try
            {
                Thread ths = new Thread(new ParameterizedThreadStart(scorpion_exec));
                ths.IsBackground = true;
                ths.Start(Scorp_Line);
            }
            catch { Do_on.write_error("Line could not be interpreted: " + Scorp_Line + ", Scorpion was unable to start a new engine thread"); }
            return;
        }

        private void scorpion_exec(object Scorp_Line)
        {
            //*return<<function::*vars ###comment
            //Start the timer to count how long it takes to execute this line of code
            sp.Start();
            string Scorp_Line_Exec = (string)Scorp_Line;
            string[] functions = null;
            try
            {
                //Check if there are comments, and strip the string of anything after the comment
                if (Scorp_Line_Exec.Contains("###"))
                {
                    //If a comment line do not waste resources and return else well waste a few more resources in order to make sure :P
                    if ((Scorp_Line_Exec = ef__.remove_commented(ref Scorp_Line_Exec)).Replace(" ", "").Length == 0)
                        return;
                }

                //Gets and removes the return variable
                string[] final = ef__.get_return(ref Scorp_Line_Exec);
                if (final.Length > 1)
                    Scorp_Line_Exec = final[1];
                //Gets the function to call. This function is a C# function which is instantiated and is publically accessible in class.Librarian
                functions = ef__.get_function(ref Scorp_Line_Exec);

                //Check if the current user has the required permissions to run this function
                if (!Do_on.mmsec.authenticate_execution(ref functions[0]))
                {
                    Do_on.write_error("This user does not have enough privileges to execute this function");
                    return;
                }

                //Set variables that will be sent to the invoked C# function with the default parameters of {string:Line_of_code, Arraylist:Variable_names}
                object[] paramse = { Scorp_Line_Exec, cut_variables(ref Scorp_Line_Exec) };

                //Invoke the C# function and get a return value if any as an object
                object retfun = GetType().GetMethod(functions[0], BindingFlags.Public | BindingFlags.Instance).Invoke(this, paramse);

                //If there is a return value, process it and set it to a Scorpion variable
                if (retfun != null)
                    ef__.process_return(ref retfun, ref final[0], this);
                functions = null;
                paramse = null;
                retfun = null;
            }
            catch (Exception erty)
            {
                Do_on.write_error("------------------------------------------------------\nThere was an error while processing your function call [Command that caused the error: " + Scorp_Line_Exec + "]\n[Stack trace: " + erty.StackTrace + "]\n[System message: " + erty.Message + "]");
                showman(functions[0]);
            }
            //End the timer to count how long it took to run the specific line of code
            sp.Stop();
            Do_on.write_success("LOGGED IN SESSION INSTANCE NUMBER [" + Do_on.instance + "/UNAME: "+ "NA" +"] --> Executed >> " + Scorp_Line_Exec + " in " + (sp.ElapsedMilliseconds / 1000) + "s/" + sp.ElapsedMilliseconds + "ms");
            sp.Reset();

            //Make sure objects are set to null and disposed
            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref Scorp_Line);
            GC.Collect();
            return;
        }
    }

    public class Enginefunctions
    {
        public string replace_fakes(string Scorp_Line)
        {
            return Scorp_Line.Replace("{&var}", "*").Replace("{&quot}", "'");
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

        public string[] get_function(ref string Scorp)
        {
            string[] delimiterChars = { "::" };
            return Scorp.ToLower().Replace(" ", "").Split(delimiterChars, StringSplitOptions.None);
        }

        public string[] get_return(ref string Scorp)
        {
            string[] delimiterChars = { "<<" };
            return Scorp.Split(delimiterChars, StringSplitOptions.None);
        }

        public string line_check(ref Scorp HANDLE, ref string Scorp)
        {
            return HANDLE.san.sanitize(ref Scorp);
        }

        public bool process_return(ref object o, ref string var, Librarian lib)
        {
            lib.varset("", new System.Collections.ArrayList { var.Replace("*", "") , o });
            return true;
        }
    }
}
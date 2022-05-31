/*  <Scorpion Server>
    Copyright (C) <2022+>  <Oscar Arjun Singh Tark>

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
            Do_on = Handle == null ? null : Handle;

            //Start thread for the single line of interpretation code
            try
            {
                Thread ths = new Thread(new ParameterizedThreadStart(scorpionExec));
                ths.IsBackground = true;
                ths.Start(Scorp_Line);
            }
            catch { ScorpionConsoleReadWrite.ConsoleWrite.writeError("Line could not be interpreted: " + Scorp_Line + ", Scorpion was unable to start a new engine thread"); }
            return;
        }

        private void scorpionExec(object Scorp_Line)
        {
            //*return<<function::*vars ###comment
            //Start the timer to count how long it takes to execute this line of code
            string Scorp_Line_Exec = (string)Scorp_Line;
            //Empty line then do not waste CPU cycles and return
            if (Scorp_Line_Exec.Trim() == "")
                return;

            string function = null;
            sp.Start();
            
            try
            {
                //Check if there are comments, and strip the string of anything after the comment
                if (Scorp_Line_Exec.Contains("###"))
                {
                    //If a comment line do not waste resources and return else well waste a few more resources in order to make sure :P
                    if ((Scorp_Line_Exec = ef__.remove_commented(ref Scorp_Line_Exec)).Replace(" ", "").Length == 0)
                    {
                        sp.Stop();
                        return;
                    }
                }

                //You can add multiple functions to an execution with the >> symbol. >> means execute rightwards
                string[] commands = ef__.execution_seperation(ref Scorp_Line_Exec);

                string exec_ = null;
                foreach (string command in commands)
                {
                    exec_ = command;
                    string[] final = ef__.get_return(ref exec_);
                    if (final.Length > 1)
                        exec_ = final[1];

                    //Remove all value based variables temporarily


                    //Gets the function to call. This function is a C# function which is instantiated and is publically accessible in class.Librarian
                    //Seperates all commands that may be in one function and makes them executable sequentially
                    function = ef__.getFunction(ref exec_);

                    //Set variables that will be sent to the invoked C# function with the default parameters of {string:Line_of_code, Arraylist:Variable_names}
                    object[] paramse = { exec_, cut_variables(ref exec_) };

                    //Check if the current user has the required permissions to run this function
                    if (!Do_on.mmsec.authenticate_execution(ref function))
                    {
                        ScorpionConsoleReadWrite.ConsoleWrite.writeError("This user does not have enough privileges to execute this function");
                        sp.Stop();
                        return;
                    }
                    //Invoke the C# function and get a return value if any as an object
                    object retfun = GetType().GetMethod(function, BindingFlags.Public | BindingFlags.Instance).Invoke(this, paramse);

                    //If there is a return value, process it and set it to a Scorpion variable
                    if (retfun != null)
                    {
                        if (final.Length > 1)
                            ef__.process_return(ref retfun, ref final[0], this);
                        else
                            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("This function requires a return variable");
                    }

                    //Set used variables to null for GC
                    function = null;
                    var_dispose_internal(ref paramse);
                    paramse = null;
                    retfun = null;
                }
            }
            catch (Exception erty)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("------------------------------------------------------\nThere was an error while processing your function call [Command that caused the error: " + Scorp_Line_Exec + "]\n[Stack trace: " + erty.StackTrace + "]\n[System message: " + erty.Message + "]");
                showMan(function);
            }
            //End the timer to count how long it took to run the specific line of code
            sp.Stop();
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("[Instance: " + Do_on.instance + "]-[Username: "+ Do_on.mmsec.get_uname() +"] --> Executed >> " + Scorp_Line_Exec + " in " + (sp.ElapsedMilliseconds / 1000) + "s/" + sp.ElapsedMilliseconds + "ms");
            sp.Reset();

            //Make sure objects are set to null and disposed
            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref Scorp_Line);
            //GC.Collect();
            return;
        }
    }
}
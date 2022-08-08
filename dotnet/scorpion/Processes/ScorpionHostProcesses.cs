using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scorpion
{ 
    partial class Librarian
    {
        public void apkill(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Kills all processes controlled by Scorpion
            //::

            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("Killing all proccesses..");
            Types.HANDLE.scp.killAllProcesses();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void processrun(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*process_name
            //Types.READ_SIGNAL_CURRENT = Types.READ_SIGNAL_OFF;
            Types.HANDLE.scp.startProcess((string)var_get(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void processdefine(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates a new Scorpion.Process. This is a standard process which can be any program. The program is partially controlled by scorpion
            //::*program, *arguments, *process_name, *foregroundoutput, *as_super
            //*status<<
            //*foregroundoutput: shows output in standard console and not by calling processio
            //*arguments is a unique string, function is not variadic in nature

            try
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Process created. Call processrun::*processname in order to start the process");
                //ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Process is starting call 'processio::*name' to see output");
                Process pri_ = new Process();
                ProcessStartInfo pri_s;
                bool is_sudo = false;

                if(var_get(objects[4]) == Types.S_No)
                {
                    pri_s = new ProcessStartInfo((string)var_get(objects[0]), (string)var_get(objects[1]));
                }
                else
                {
                    pri_s = new ProcessStartInfo(Types.S_ROOT_LINUX, String.Format("{0} {1}", (string)var_get(objects[0]), (string)var_get(objects[1])));
                    is_sudo = true;
                }

                pri_.Exited += Types.HANDLE.scp.processExited;
                if ((string)var_get(objects[3]) == Types.S_No)
                {
                    pri_s.RedirectStandardOutput = true;
                    pri_s.RedirectStandardInput = true;
                    pri_s.UseShellExecute = false;
                }

                pri_.StartInfo = pri_s;
                Types.HANDLE.scp.addProcess(ref pri_, (string)var_get(objects[2]), is_sudo);
            }
            catch(Exception e) { Console.WriteLine(e.Message); }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public string processio(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show output for a process
            //*return<<::*process_name

            string output_ = Types.HANDLE.scp.getStdOut((string)var_get(objects[0]));
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(output_);

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref output_, true);
        }

        public void asyncprocessio(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show C#.Async output for a process
            //::*process_name

            Types.HANDLE.scp.get_stdout_async((string)var_get(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void processinput(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Sends input from a variable to a process
            //::*process_name, *input

            Types.HANDLE.scp.setStdIn((string)var_get(objects[0]), (string)var_get(objects[1]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        //ALWAYS CALL PROCESSDELETE ELSE PROCESS WILL STAY IN MEMORY
        public void processdelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Stops an closes a process and deletes it from Scorpions control
            //::*process_name

            Types.HANDLE.scp.killProcess((string)var_get(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void lp(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            listprocesses(ref Scorp_Line_Exec, ref objects);
            return;
        }

        public void listprocesses(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Lists all Scorpion controlled processes and their state
            //::
            Types.HANDLE.scp.listProcesses();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
        }
    }
}
using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scorpion
{ 
    partial class Librarian
    {
        Scorpion_Process scp = new Scorpion_Process();
        public void apkill(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Kills all processes controlled by Scorpion
            //::

            Do_on.write_to_cui("Killing all proccesses");
            Do_on.write_to_cui(scp.kill_processes());
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void processrun(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*process_name
            scp.start_process((string)var_get(objects[0]));
            //Do_on.write_success("Process '" + var_get(0) + "' started...");
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void processoption(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*process_name, *options...
            //All options for scorpion or SEHT programs must use the '=' delimiter, other programs may do as they want
            //function is variadic
            //scp.get_process((string)var_get(objects[0]));
            scp.add_option((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);

            return;
        }

        public void processlistoptions(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            var returned = scp.get_options((string)var_get(objects[0]));

            if (returned != null)
            {
                foreach (KeyValuePair<string, string> kvp in returned)
                    Do_on.write_to_cui(kvp.Key + " = " + kvp.Value);
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);

            return;
        }

        public void process(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates a new Scorpion.Process. This is a standard process which can be any program. The program is partially controlled by scorpion
            //::*program, *arguments, *process_name, *foregroundoutput
            //*status<<
            //*foregroundoutput: shows output in standard console and not by calling processio
            //*arguments is a unique string, function is not variadic in nature

            try
            {
                Do_on.write_to_cui("Process created. Call processrun::*processname in order to start the process");
                //Do_on.write_to_cui("Process is starting call 'processio::*name' to see output");
                Process pri_ = new Process();
                ProcessStartInfo pri_s = new ProcessStartInfo((string)var_get(objects[0]), (string)var_get(objects[1]));
                pri_.Exited += Pri__Exited;
                if ((string)var_get(objects[3]) == Do_on.types.S_No)
                {
                    pri_s.RedirectStandardOutput = true;
                    pri_s.RedirectStandardInput = true;
                    pri_s.UseShellExecute = false;
                }
                pri_.StartInfo = pri_s;
                scp.add_proccess(ref pri_, (string)var_get(objects[2]));
                //pri_.Start();
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        private void Pri__Exited(object sender, EventArgs e)
        {
            Process p = (Process)sender;
            Do_on.write_to_cui("Process Ended: [Status Code: " + p.ExitCode + "] " + p.ProcessName);
            return;
        }

        public string processio(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show output for a process
            //*return<<::*process_name

            string output_ = scp.get_std((string)var_get(objects[0]));
            Do_on.write_to_cui(output_);

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(ref output_, true);
        }

        public void asyncprocessio(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show C#.Async output for a process
            //::*process_name

            scp.get_stdout_async((string)var_get(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void processinput(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Sends input from a variable to a process
            //::*process_name, *input

            scp.set_stdin((string)var_get(objects[0]), (string)var_get(objects[1]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        //ALWAYS CALL PROCESSDELETE ELSE PROCESS WILL STAY IN MEMORY
        public void processdelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Stops an closes a process and deletes it from Scorpions control
            //::*process_name

            scp.remove_process((string)var_get(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void listprocesses(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Lists all Scorpion controlled processes and their state
            //::

            Do_on.write_to_cui(scp.list_processes());
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
        }
    }

    class Scorpion_Process
    {
        //This class invokes the internal working of the Scorpion.Process system

        int processes = -1;
        private Process[] pr_list = new Process[0x4];
        private string[] pr_list_ref = new string[0x4];
        private string[] pr_list_name = new string[0x4];
        private string[] pr_output = new string[0x4];
        private Dictionary<string, Dictionary<string, string>> pr_options = new Dictionary<string, Dictionary<string, string>>();

        public void add_proccess(ref Process pri, string name)
        {
            processes+=1;
            pr_list[processes] = pri;
            pr_list_ref[processes] = name;
            pr_list_name[processes] = pri.StartInfo.FileName + " " + pri.StartInfo.Arguments;

            return;
        }

        public void remove_process(string name)
        {
            int i = get_process(name);
            if(!pr_list[i].HasExited)
                pr_list[i].Kill();
            pr_list_ref[i] = null;
            pr_list_name[i] = null;
            pr_list[i] = null;
            processes -= 1;
            return;
        }

        public void start_process(string name)
        {
            //process options as arguments
            var options = get_options(name);
            if (options != null)
            {
                Console.WriteLine("IN");
                foreach (KeyValuePair<string, string> kvp in options)
                {
                    Console.WriteLine("OPT");
                    pr_list[get_process(name)].StartInfo.Arguments.Insert(0, kvp.Key + "=" + kvp.Value);
                }
            }

            pr_list[get_process(name)].Start();
            Console.WriteLine("Process '{0}' started", name);
            return;
        }

        public string kill_processes()
        {
            //BUG: Remove all process elements!!!!!!

            string out_ = "";
            foreach (Process p in pr_list)
            {
                try
                {
                    out_ = out_ + "[KILL] " + p.ProcessName + "\n";
                    p.Kill();
                    if (p.HasExited)
                        out_ = out_ + "[KILLED] " + p.ProcessName + "\n";
                    else
                        out_ = out_ + "[UNKILLED OR LOST] " + p.ProcessName + "\n";
                }
                finally { }
            }
            return out_;
        }

        public void set_stdin(string name, string input)
        {
            int proc = get_process(name);
            pr_list[proc].StandardInput.WriteLine(input);
            return;
        }

        public void add_option(string proc_name, string option_name, string option)
        {
            //Check if the process is running if so warn the user that options will not e applied unless the process is restarted
            /*try
            {
                if (!pr_list[get_process(proc_name)].HasExited)
                    Console.WriteLine("The process '{0}' is currently running. Any options cannot be applied to a running process, please restart the process to apply any options", proc_name);
            }
            finally
            {*/
                //check if process option entry exists
                if (!pr_options.ContainsKey(proc_name))
                    pr_options.Add(proc_name, new Dictionary<string, string>());

                //Add the options to the process options dictionary
                pr_options[proc_name].Add(option_name, option);
            //}
            return;
        }

        public Dictionary<string, string> get_options(string name)
        {
            return pr_options.ContainsKey(name) ? pr_options[name] : null;
        }

        public string get_std(string name)
        {
            int proc = get_process(name);
            string out_ = "";
            while (pr_list[proc].StandardOutput.Peek() > -1)
                out_ = out_ + (pr_list[proc].StandardOutput.ReadLine()) + "\n";
            pr_list[proc].StandardOutput.DiscardBufferedData();
            pr_list[proc].StandardOutput.BaseStream.Flush();
            return out_;
        }

        public void get_stdout_async(string name)
        {
            int proc = get_process(name);
            Get_stdout_async_task(pr_list[proc]);
        }

        static async Task Get_stdout_async_task(Process p)
        {
            string s = await p.StandardOutput.ReadLineAsync();
            Console.WriteLine(s);
        }

        public int get_process(string name)
        {
            for (int i = 0; i < 5; i++)
            {
                if (name == pr_list_ref[i])
                    return i;
            }
            return -1;
        }

        public string list_processes()
        {
            string list = "Processes:\n";
            for (int i = 0; i <= processes; i++)
                list = list + "\n[Name: " + pr_list_ref[i] + "] (" + pr_list_name[i] + ")\n" + "[Exited: " + pr_list[i].HasExited + "]\n" + "[Id: " + pr_list[i].Id + "]\n" + "[Affinity: " + pr_list[i].ProcessorAffinity + "]\n" + "[Priority: " + pr_list[i].BasePriority + "]\n" + "[Full process name: " + pr_list[i].ProcessName + "]\n";
            return list;
        }
    }
}
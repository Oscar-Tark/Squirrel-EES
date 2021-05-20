using System;
using System.Diagnostics;
using System.Collections;

namespace Scorpion
{ 
    partial class Librarian
    {
        Scorpion_Process scp = new Scorpion_Process();

        //Pauses all processes incase they are taking the foreground output
        public void apkill(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.write_to_cui("Killing all proccesses");
            scp.kill_processes();
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void process(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*program, *arguments, *name, *foregroundoutput
            //*status<<
            //*foregroundoutput: shows output in standard console and not by calling processio
            //*arguments is a unique string, function is not variadic in nature
            try
            {
                Do_on.write_to_cui("Process is starting call 'processio::*name' to see output");
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
                pri_.Start();
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            var_arraylist_dispose(ref objects);
            return;
        }

        void Pri__Exited(object sender, EventArgs e)
        {
            Process p = (Process)sender;
            Do_on.write_to_cui("Process Ended: [Status Code: " + p.ExitCode + "] " + p.ProcessName);
            return;
        }

        public void processio(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show output for a process
            //::*name
            Do_on.write_to_cui(scp.get_std((string)var_get(objects[0])));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void proccessinput(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Incase a process asks for input a dialog will show up asking for command input


            return;
        }

        //ALWAYS CALL PROCESSDELETE ELSE PROCESS WILL STAY IN MEMORY
        public void processdelete(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            scp.remove_process((string)var_get(objects[0]));

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void listprocesses(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.write_to_cui(scp.list_processes());

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
        }
    }

    class Scorpion_Process
    {
        int processes = -1;
        private Process[] pr_list = new Process[4];
        private string[] pr_list_ref = new string[4];
        private string[] pr_list_name = new string[4];
        private string[] pr_output = new string[4];

        public Scorpion_Process()
        {
            return;
        }

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

        public void kill_processes()
        {
            foreach (Process p in pr_list)
                p.Kill();
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
                list = list + "\n" + pr_list_ref[i] + " (" + pr_list_name[i] + ")\n" + "[Exited: " + pr_list[i].HasExited + "]\n" + "[Id: " + pr_list[i].Id + "]\n" + "[Affinity: " + pr_list[i].ProcessorAffinity + "]\n" + "[Priority: " + pr_list[i].BasePriority + "]" + "\n";
            return list;
        }
    }
}
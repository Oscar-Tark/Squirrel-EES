using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace Scorpion
{ 
    partial class Librarian
    {
        Scorpion_Process scp = new Scorpion_Process();

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
            //::*name
            Console.WriteLine(scp.get_std((string)var_get(objects[0])));
            return;
        }

        public void processtop(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            var_arraylist_dispose(ref objects);
            return;
        }
    }

    class Scorpion_Process
    {
        int processes = 0;
        private Process[] pr_list = new Process[4];
        private string[] pr_list_ref = new string[4];
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

            return;
        }

        public void remove_process(string name)
        {
            int i = get_process(name);
            pr_list[i].Kill();
            pr_list_ref[i] = null;
            pr_list[i] = null;
            processes -= 1;

            return;
        }

        public string get_std(string name)
        {
            int proc = get_process(name);
            return pr_list[proc].StandardOutput.ReadToEnd();
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
    }
}
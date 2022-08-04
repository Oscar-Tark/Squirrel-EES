using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScorpionProcesses
{
    public class ScorpionProcessHandler
    {
        //This class invokes the internal working of the Scorpion.Process system

        internal struct scorpion_process
        {
            public string name;
            public string application_name;
            public string arguments;
            public string output;
            public bool is_sudo;
            public int pid;
            public Process process;
        };

        internal Dictionary<string ,scorpion_process> scpl = new Dictionary<string, scorpion_process>();

        public void addProcess(ref Process pri, string name, bool is_sudo)
        {
            //Check if the name is already used if not allow
            if(scpl.ContainsKey(name))
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("The name allocated for the process already exist. Please choose a different name");
                return;
            }

            scorpion_process scp = new scorpion_process();
            scp.name = name;
            scp.application_name = pri.StartInfo.FileName;
            scp.arguments = pri.StartInfo.Arguments;
            scp.process = pri;
            scp.is_sudo = is_sudo;
            
            scpl.Add(name, scp);
            return;
        }

        public void killProcess(string name)
        {
            scorpion_process scp = getProcess(name);

            try
            {
                if(!scp.process.HasExited)
                    scp.process.Kill();
            }
            catch(Exception e)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError("The process: ", name, " could not be killed, the process will be orphaned. Please kill the process using your systems tools\n\n");

                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Application name: ", scp.application_name, "\n", "Pid: ", Convert.ToString(scp.pid));
            }
            finally
            {
                scpl.Remove(name);
            }

            return;
        }

        public void startProcess(string name)
        {
            scorpion_process scp = getProcess(name);
            scp.process.Start();
            scp.pid = scp.process.Id;

            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess($"Process '{name}' started");
            return;
        }

        public void killAllProcesses()
        {
            foreach( KeyValuePair<string, scorpion_process> kvp in scpl)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput($"[KILL] {kvp.Key}");
                killProcess(kvp.Key);
            }

            return;
        }

        public void setStdIn(string name, string input)
        {
            scorpion_process scp = getProcess(name);
            scp.process.StandardInput.WriteLine(input);
            return;
        }

        public string getStdOut(string name)
        {
            string returnable = default;
            
            scorpion_process scp = getProcess(name);

            while (scp.process.StandardOutput.Peek() > -1)
                returnable = returnable + (scp.process.StandardOutput.ReadLine()) + "\n";
            
            scp.process.StandardOutput.DiscardBufferedData();
            scp.process.StandardOutput.BaseStream.Flush();

            return returnable;
        }

        public void get_stdout_async(string name)
        {
            scorpion_process scp = getProcess(name);
            Get_stdout_async_task(scp.process);
        }

        static async Task Get_stdout_async_task(Process process)
        {
            string s = await process.StandardOutput.ReadLineAsync();
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(s);
        }

        internal scorpion_process getProcess(string name)
        {
            scorpion_process returnable;
            if(!scpl.TryGetValue(name, out returnable))
            //!!!!!!!!!!!!PROBLEMATIC: CANNOT DISTINGUISH BETWEEN EMPTY AND NOT
                return returnable;
            return returnable;
        }

        public void listProcesses()
        {
            string list = "Processes:\n";
            /*foreach(KeyValuePair<string, scorpion_process> kvp in scpl)
            {
                list = list + "\n[Name: " + pr_list_ref[i] + "] (" + pr_list_name[i] + ")\n" + "[Exited: " + pr_list[i].HasExited + "]\n" + "[Id: " + pr_list[i].Id + "]\n" + "[Priority: " + pr_list[i].BasePriority + "]\n" + "[Full process name: " + pr_list[i].ProcessName + "]\n";
            }*/

            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(list);

            return;
        }

        public void processExited(object sender, EventArgs e)
        {
            try
            {
                Process p = (Process)sender;
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Process Ended: [Status Code: " + p.ExitCode + "] " + p.ProcessName);
            }
            finally
            {
                //Should clear from scpl
            }
            return;
        }
    }
}
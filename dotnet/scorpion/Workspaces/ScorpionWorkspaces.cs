using System;
using System.Collections;
using System.IO;

namespace Scorpion
{
    partial class Librarian
    {
        /*public void workspace(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            wkp.create_workspace();
            return;
        }*/

        public void wnet(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.wkp.set_stream_tcp();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void wout(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.wkp.set_stream_out();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void writenet(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.wkp.write_net("KAKOR");

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void outnet(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.wkp.out_net();
            return;
        }
    }
}

namespace Workspaces
{
    public class Workspaces
    {
        //Only two workspaces are available at the moment
        readonly Scorpion.Scorp Do_on;

        //private int current = 0;
        const int workspaces = 2;

        private MemoryStream[] ms_workspaces = new MemoryStream[2];
        private TextWriter[] ts_workspaces = new TextWriter[2];

        public Workspaces(Scorpion.Scorp fm1)
        {
            Do_on = fm1;
            Console.WriteLine("[DEBUG] Generating workspaces...");
            get_main_stream();
            Console.WriteLine("[DEBUG] Available workspaces {0}", workspaces);
            return;
        }

        public void get_main_stream()
        {
            ms_workspaces[0] = null;
            ts_workspaces[0] = Console.Out;

            MemoryStream ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);

            ms_workspaces[1] = ms;
            ts_workspaces[1] = tw;
            return;
        }

        public void set_stream_tcp()
        {
            Console.SetOut(ts_workspaces[1]);
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Set workspace to: NETWORK");
            return;
        }

        public void set_stream_out()
        {
            Console.SetOut(ts_workspaces[0]);
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Set workspace to: STANDARD_OUTPUT");
            return;
        }

        public void write_net(string Writeable)
        {
            ts_workspaces[1].WriteLine(Writeable);
            return;
        }

        public void out_net()
        {
            StreamReader sr = new StreamReader(ms_workspaces[1]);
            Console.WriteLine(sr.ReadToEnd());
            return;
        }

        /*public void create_workspace()
        {
            MemoryStream ms = new MemoryStream();
            TextWriter tw = new StreamWriter(ms);
            ms_workspaces[current] = ms;
            ts_workspaces[current] = tw;

            workspaces++;

            Console.WriteLine("Workspace {0} created. Switching to workspace");
            current++;
            Console.SetOut(tw);
            return;
        }*/
    }
}

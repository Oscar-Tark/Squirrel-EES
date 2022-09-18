using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        public void exit(ref string Scorp_Line_Exec, ref ArrayList Objects)
        {
            instancecleanup();
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Exiting..");
            Environment.Exit(0);
            return;
        }

        public void instancecleanup()
        {
            //Clean memory up
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Clearing GLOBAL memory..");

            Types.HANDLE.mem.AL_CURR_VAR.Clear();
            Types.HANDLE.mem.AL_CURR_VAR_NACESSED.Clear();
            Types.HANDLE.mem.AL_CURR_VAR_REF.Clear();
            Types.HANDLE.mem.AL_CURR_VAR_TAG.Clear();
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Cleared GLOBAL memory");

            //Delete all TCP servers
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput($"Clearing {Types.HANDLE.mem.AL_TCP.Count} TCP servers..");
            for(int i = Types.HANDLE.mem.AL_TCP.Count-1; i >= 0; i--)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeWarning($"Closing tcp server {Types.HANDLE.mem.AL_TCP_REF[i]}");
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[i]).Stop();
                ((SimpleTCP.SimpleTcpServer)Types.HANDLE.mem.AL_TCP[i]).DataReceived -= Types.HANDLE.sdh.ServerDataReceivedEvent;
                ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess($"Closed tcp server {Types.HANDLE.mem.AL_TCP_REF[i]}");
            }
            Types.HANDLE.mem.AL_TCP.Clear();
            Types.HANDLE.mem.AL_TCP_KY.Clear();
            Types.HANDLE.mem.AL_TCP_REF.Clear();
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Cleared TCP servers");
            return;
        }
    }
}
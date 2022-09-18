

using System.IO;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        //Add to Session dependent handler and encryption dependent
        public void scriptruncondition(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All files must be UTF8
            //::*if, *is, *Path, *result_must_be_equal

            if ((string)MemoryCore.varGet(objects[3]) == Types.S_Yes)
            {
                if ((string)MemoryCore.varGet(objects[0]) == (string)MemoryCore.varGet(objects[1]))
                {
                    string line;
                    FileStream fd = new FileStream((string)MemoryCore.varGet((string)objects[2]), FileMode.Open);
                    StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                    while ((line = sr.ReadLine()) != null)
                        scorpionExec(line);

                    fd.Flush();
                    sr.Close();
                    fd.Close();

                    var_dispose_internal(ref line);
                }
            }
            else if ((string)MemoryCore.varGet(objects[3]) == Types.S_Yes)
            {
                if ((string)MemoryCore.varGet(objects[0]) != (string)MemoryCore.varGet(objects[1]))
                {
                    string line;
                    FileStream fd = new FileStream((string)MemoryCore.varGet((string)objects[2]), FileMode.Open);
                    StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                    while ((line = sr.ReadLine()) != null)
                        scorpionExec(line);

                    fd.Flush();
                    sr.Close();
                    fd.Close();

                    var_dispose_internal(ref line);
                }
            }
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void scriptrun(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All files must be UTF8
            //::*path

            string line;
            //Run Script
            FileStream fd = new FileStream((string)MemoryCore.varGet((string)objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            while ((line = sr.ReadLine()) != null)
                scorpionExec(line);

            fd.Flush();
            sr.Close();
            fd.Close();
            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void scriptrundb(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *subtag==*tag
            //::*path, *subtag

            //Query the database
            //string db, object data, string tag, string subtag, short OPCODE
            /*ArrayList result*/Scorpion_MDB.ScorpionMicroDB.XMLDBResult result = Types.HANDLE.vds.doDBSelectiveNoThread((string)MemoryCore.varGet(objects[0]), Types.S_NULL, (string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[1]), Types.HANDLE.vds.OPCODE_GET);
            string script = default;

            foreach(object o_script in result.getAllDataAsArray())
            {
                script = Enginefunctions.replaceEscape((string)o_script);
                ScorpionConsoleReadWrite.ConsoleWrite.writeSpecial("Running XMLDB script: ", script);
                scorpioniee(script);
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
        }
    }
}

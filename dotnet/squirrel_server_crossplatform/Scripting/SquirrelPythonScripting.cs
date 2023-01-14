using SquirrelPython;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        public object pythonrun(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returns<<::*lines, *python_result_variable
            object python_returns = SquirrelPython.SquirrelPython.runPythonInLine((string)MemoryCore.varGet(objects[0]), (string)MemoryCore.varGet(objects[1]));
            return var_create_return(ref python_returns);
        }
        
        public void pythonrunfilenoreturn(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            SquirrelPython.SquirrelPython.runPythonFileNoReturn((string)MemoryCore.varGet(objects[0]));
            return;
        }
    }
}
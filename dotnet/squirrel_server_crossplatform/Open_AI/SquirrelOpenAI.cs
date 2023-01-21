using SquirrelOpenAI;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        private SquirrelOpenAIController squirrel_open_ai;

        public void openaisetup(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //var::*key_path
            squirrel_open_ai = new SquirrelOpenAIController((string)MemoryCore.varGet(objects[0]));
            
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public object openaiquery(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //var::*query
            object returnable = string.Empty;
            returnable = squirrel_open_ai.openAIQuery((string)MemoryCore.varGet(objects[0]));

            return var_create_return(ref returnable);
        }
    }
}
using System.Collections;
using System.IO;
using XMLDBEditor;

namespace Scorpion
{
    partial class Librarian
    {
        public void xmldbedit(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //string database_path, string tag, string password, string editor = null, string subtag = null)
            //::*database, *password, *string tag, *string editor_bin_name, *string subtag_or_null)

            string subtag = (string)MemoryCore.varGet(objects[4]);

            XMLDBEditorRun xmldbeditor = new XMLDBEditorRun();
            xmldbeditor.edit((string)MemoryCore.varGet(objects[0]), (string)MemoryCore.varGet(objects[1]), (string)MemoryCore.varGet(objects[2]), (string)MemoryCore.varGet(objects[3]), subtag == Types.S_No ? null : subtag);

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}
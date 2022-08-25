
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        public void generateaeskey(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::
            ScorpionAES.ScorpionAESInHouse.exportNewKey(Types.main_user_aes_path_file);

            if(File.Exists(Types.main_user_aes_path_file))
                ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("New key created at default path: ", Types.main_user_aes_path_file);
            return;
        }
    }
}
using System.Collections;
using Scorpion_Hasher_Library;

namespace Scorpion
{
    partial class Librarian
    {
        public string hash(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returns::*var_to_hash
            string hash_ = new Scorpion_Hasher().hash((string)var_get(objects[0]));
            return var_create_return(ref hash_, true);
        }

        public string hashverify(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returns_bool::*var, *hashed_var
            bool verify_ = new Scorpion_Hasher().verify((string)var_get(objects[0]), (string)var_get(objects[1]));
            string s_n = Do_on.types.Convert_booltostring(verify_);
            return var_create_return(ref s_n, true);
        }
    }
}

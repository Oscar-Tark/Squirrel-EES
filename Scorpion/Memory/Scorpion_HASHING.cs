/*  <Scorpion IEE(Intelligent Execution Environment)>
    Copyright (C) <2014+>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

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
            string s_n = Do_on.types.Convert_Bool_To_String(verify_);
            return var_create_return(ref s_n, true);
        }
    }
}

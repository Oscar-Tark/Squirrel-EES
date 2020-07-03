//DEFUNCT
/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark> <Benjamin Jack Johnson>

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

namespace Scorpion
{
    partial class Librarian
    {
        //get variables as STATIC
        public void GET_var_tag(string Scorp_Line_Get, int index)
        {
            Scorp_Line_Get = cut_parenth(ref Scorp_Line_Get);
            Do_on.AL_CURR_VAR[index] = Do_on.AL_CURR_VAR_TAG[Do_on.AL_CURR_VAR_REF.IndexOf(var_get(ref Scorp_Line_Get))];

            Scorp_Line_Get = null;

            return;
        }

        //get variable allocation as STATIC
        public void GET_var_alloc(string Scorp_Line_Get, int index)
        {
            Scorp_Line_Get = cut_parenth(ref Scorp_Line_Get);

            Do_on.AL_CURR_VAR[index] = Do_on.AL_CURR_VAR_REF.IndexOf(var_get(ref Scorp_Line_Get));

            //clean
            Scorp_Line_Get = null;

            return;
        }

        public void GET_raw_var(string Scorp_Line_Get, int index)
        {
            ArrayList al = cut_variables(ref Scorp_Line_Get);

            Do_on.AL_CURR_VAR[index] = Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_get(al[0].ToString()))];

            //clean
            var_arraylist_dispose(ref al);
            Scorp_Line_Get = null;

            return;
        }

    }
}
/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

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

using System;
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        /*
        Scorpion standard is: yyyy_MM_dd
        */

        public string date(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*RETURNABLE<<::
            return var_create_return(DateTime.Today.ToString("yyyy_MM_dd"), true);
        }

        public string dateyesterday(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*RETURNABLE<<::
            return var_create_return(DateTime.Today.AddDays(-1).ToString("yyyy_MM_dd"), true);
        }

        public string datetomorrow(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*RETURNABLE<<::
            return var_create_return(DateTime.Today.AddDays(+1).ToString("yyyy_MM_dd"), true);
        }
    }
}

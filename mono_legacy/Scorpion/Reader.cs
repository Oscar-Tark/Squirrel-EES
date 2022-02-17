//#
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
namespace Scorpion
{
    public class reader
    {
        Scorp fmm;
        public Librarian lib_SCR;
        public reader(Scorp Form_Handle)
        {
           fmm = Form_Handle;
           lib_SCR = new Librarian(Form_Handle);
           return;
        }

        //ACCESS ANY LIB
        public void access_library(string Scorp_Line)
        {
            //fmm.write_to_cui("READER: DOING for: " + fmm.instance);
            lib_SCR.scorpioniee(Scorp_Line, fmm);
            Scorp_Line = null;
            return;
        }
    }
}
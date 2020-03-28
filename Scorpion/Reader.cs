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

using System.Threading;

namespace Scorpion
{
    public class reader
    {
        Form1 fmm;
        public Librarian lib_SCR;
        private Thread th_cross; public bool Re_do = true; public int ndx = 0;
        private delegate void dl_cross();
        public reader(Form1 Form_Handle)
        {
           fmm = Form_Handle;
           lib_SCR = new Librarian(Form_Handle);
           return;
        }

        private void Access_strt()
        {
                th_cross = new Thread(new ThreadStart(crss_));
                th_cross.IsBackground = true;
                th_cross.Start();
        }

        private void crss_()
        {
            if (Re_do == true)
            {
                fmm.read_again(true);
                fmm.re_read(ndx);
            }
            else
                fmm.read_again(false);
        }

        //ACCESS ANY LIB
        public void access_library(string Scorp_Line)
        {
            lib_SCR.scorpioniee(Scorp_Line);
            Scorp_Line = null;
            return;
        }
    }
}
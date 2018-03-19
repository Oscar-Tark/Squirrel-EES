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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Scorpion
{
    public class reader
    {
        Form1 fmm; 
        public Librarian lib_SCR = new Librarian();
        private Thread th_cross; public bool Re_do = true; public int ndx = 0;
        private delegate void dl_cross();
        public void read(string Scorpion_String, int Cursor_Index, Form1 Form_Handle)
        {
            try
            {
                fmm = Form_Handle;
                int n = Cursor_Index;
                int n2 = Scorpion_String.IndexOf(";", Cursor_Index); //~;
                ndx = n2 + 1; //2
                Scorpion_String = Scorpion_String.Remove(n2, Scorpion_String.Length - n2);
                Scorpion_String = Scorpion_String.Remove(0, Cursor_Index);
                Re_do = lib_SCR.Decider(Scorpion_String, Form_Handle, this);

                if (Re_do == true)
                {
                    Access_strt();
                }
                else
                { fmm.read_again(false); }
            }
            catch { }

            //clean
            Scorpion_String = null;
        }

        private void Access_strt()
        {
            try
            {
                th_cross = new Thread(new ThreadStart(Accessing));
                th_cross.IsBackground = true;
                th_cross.Start();
            }
            catch { }
        }

        private void Accessing()
        {
            try
            {
                this.fmm.Invoke(new dl_cross(crss_));
            }
            catch { }
        }

        private void crss_()
        {
            if (Re_do == true)
            {
                fmm.read_again(true);
                fmm.re_read(ndx);
            }
            else
            {
                fmm.read_again(false);
            }
        }

        //ACCESS ANY LIB
        public void access_library(string Scorp_Line)
        {
            lib_SCR.work_(Scorp_Line);
            Scorp_Line = null;
            return;
        }
    }
}
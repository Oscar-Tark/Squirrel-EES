/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020>  <Oscar Arjun Singh Tark>

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
using System.Windows.Forms;

//Static Library
using System;
namespace Scorpion
{
    partial class Librarian
    {
        public void exit(string Scorp_Line_Exec, ArrayList Objects)
        {
            Environment.Exit(0);
            return;
        }

        //??
        public string writeto(ref string Scorp_Line_Exec, ArrayList Objects)
        {
            //::*arg..
            string writable = "";
            foreach (string o in Objects)
                writable += var_get(o);
            write_to_console(ref writable);
            return var_create_return(ref writable, true);
        }

        public string input(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable<<::*message, *allowed_length
            Do_on.write_to_cui(var_get(objects[0]));
            Console.WriteLine("Input a value:");

            //Due to main engine and input() having a simultaneous Console.Readline() Input is not read properly. Create a dialog or fix
            string __ret = Console.ReadLine();
            return var_create_return(ref __ret, true);
        }

        public void output(ref string Scorp_Line_Exec, ArrayList Objects)
        {
            //::*var, *var..
            string writable = "";
            foreach (string o in Objects)
                writable += var_get(o);
            write_to_console(ref writable);

            var_arraylist_dispose(ref Objects);
            Scorp_Line_Exec = null;
            return;
        }

        public void outputbytes(ref string Scorp_Line_Exec, ArrayList Objects)
        {
            //::*var
            object obj = var_get(Objects[0]);
            byte[] bytes = Do_on.crypto.To_Byte(obj);

            Console.WriteLine("Byte sequence for [" + Objects[0] + "]:\n");
            foreach (byte __byte in bytes)
                Console.Write("{0:X} ", __byte);

            var_arraylist_dispose(ref Objects);
            Scorp_Line_Exec = null;
            return;
        }

        private void write_to_console(ref string STR_)
        {
            Do_on.write_to_cui(STR_);
            return;
        }

        private void write_to_console(string STR_)
        {
            Do_on.write_to_cui(STR_);
            return;
        }

        private void write_to_cui(string Scorp_Line_Exec)
        {
            ArrayList al = cut_variables(ref Scorp_Line_Exec);
            foreach (object o in al)
            {
                Do_on.write_to_cui(var_get(o.ToString()).ToString() + "\n");
            }

            var_arraylist_dispose(ref al);
            Scorp_Line_Exec = null;
            return;
        }
    }
}
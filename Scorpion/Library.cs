/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark>

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
using System.Reflection;
using System.IO;
using System.Threading;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

//Static Library
namespace Scorpion
{
    public partial class Librarian
    {
        System.Diagnostics.Stopwatch sp = new System.Diagnostics.Stopwatch();
        public Librarian(Form1 Form_Handle)
        {
            Do_on = Form_Handle;
            return;
        }

        public void scorpioniee(object Scorp_Line)
        {
            sp.Start();
            string Scorp_Line_Exec = prepare_Scorp_line(ref Scorp_Line);
            //Threading needed!
            try
            {
                string[] functions = get_function(ref Scorp_Line_Exec);
                object[] paramse = new object[2];
                paramse[0] = Scorp_Line_Exec;
                paramse[1] = cut_variables(ref Scorp_Line_Exec);

                //NOT THE RIGHT WAY!!!!
                try
                {
                    this.GetType().GetMethod(functions[0], BindingFlags.Public | BindingFlags.Instance).Invoke(this, paramse);
                }
                catch (Exception erty)
                {
                    this.GetType().GetMethod(functions[0], BindingFlags.Public | BindingFlags.Instance).Invoke(this, new object[0]);
                }
                Scorp_Line = null;
            }
            catch (Exception erty)
            {
                Do_on.write_to_cui("There was an error while processing your function call [Line of Code that Caused the Error : >> " + Scorp_Line_Exec + "] " + erty.StackTrace + "");
            }

            sp.Stop();
            Do_on.write_to_cui("Executed >> " + Scorp_Line_Exec + " in " + (sp.ElapsedMilliseconds/1000) + "s/" + sp.ElapsedMilliseconds + "ms" + "");
            sp.Reset();

            pointered = false;
            Scorp_Line_Exec = null;
            GC.Collect();

            return;
        }
        private string prepare_Scorp_line(ref object Scorp_Line)
        {
            return Scorp_Line.ToString().ToLower();
        }

        private string[] get_function(ref string Scorp)
        {
            char[] delimiterChars = { '.', '(' };
            return Scorp.Split(delimiterChars);
        }
    }
}
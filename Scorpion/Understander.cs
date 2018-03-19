/*
 * Works on main objects and executed lines
 * 
 * 1 Understand what the command is trying to achieve Next
 * 2 
 */
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace Understander
{
    class Understander
    {
        Scorpion.Form1 Do_on;

        public void Initialize(Scorpion.Form1 fm1)
        {
            Do_on = fm1;

            return;
        }

        public void Understand_Command(ref string Scorp_Line)
        {
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line);


            return;
        }
    }
}
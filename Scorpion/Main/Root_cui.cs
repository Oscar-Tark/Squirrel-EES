/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014-2016>  <Oscar Arjun Singh Tark>

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
using System.Threading;

namespace Scorpion
{
    //CHANGE CLASS NAME
    public partial class Form1
    {
        public Form1()
        {
            start_classes();
            Console.WriteLine("Welcome to Scorpion V1.0 :) Sting STING sTiNG\n\n{0}", "Licensed Under the GNU GPL Version 3\n < One Platform.Noded Command Framework >\nCopyright(C) < 2020 >  < Oscar Arjun Singh Tark >\n\nThis program is free software: you can redistribute it and / or modify\nit under the terms of the GNU Affero General Public License as \npublished by the Free Software Foundation, either version 3 of the \nLicense, or(at your option) any later version.\n\nThis program is distributed in the hope that it will be useful,\nbut WITHOUT ANY WARRANTY; without even the implied warranty of\nMERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the\nGNU Affero General Public License for more details.\n\nYou should have received a copy of the GNU Affero General Public License\nalong with this program.If not, see < http://www.gnu.org/licenses/>.\n\n");
            while (true)
            {
                readr.access_library(Console.ReadLine());
                th_clean_strt();
            }
        }

        public void Load_init_db()
        {
            try
            {
                vds.Un_Dump();
            }
            catch { write_to_cui(AL_MESSAGE[7].ToString()); }
            return;
        }

        public void Application_ApplicationExit(object sender, EventArgs e)
        {
            //DUMP
            types.unload_system_vars();
            vds.Dump_main_db();
            sender = null;
            e = null;
            return;
        }

        private Thread th_clean;
        private void th_clean_strt()
        {
            th_clean = new Thread(new ThreadStart(clean));
            th_clean.IsBackground = true;
            th_clean.Start();
            return;
        }

        private void clean()
        {
            AL_UNBEARABLE_CHARS.TrimToSize();

            //General Memory
            AL_CURR_VAR.TrimToSize();
            AL_CURR_VAR_REF.TrimToSize();
            AL_CURR_VAR_TAG.TrimToSize();
            AL_CURR_VAR_EVT.TrimToSize();

            //Functions
            AL_Ref_EVT.TrimToSize();
            AL_EVT.TrimToSize();

            //System
            AL_HIB_FILES.TrimToSize();

            AL_AMCS.TrimToSize();
            AL_AMCS_REF.TrimToSize();

            AL_SHS.TrimToSize();
            AL_SHS_APP.TrimToSize();
            AL_SHS_APP_REF.TrimToSize();
            AL_SHS_REF.TrimToSize();

            AL_PRC.TrimToSize();
            AL_PRC_REF.TrimToSize();

            AL_ACC.TrimToSize();
            GC.Collect();
            return;
        }

        //ACCESSORS
        public void Access_Work(string Line)
        {
            readr.access_library(Line);
            return;
        }

        public void write_to_cui(string Text)
        {
            Console.WriteLine(Text + "\n");
            Text = null;

            return;
        }
    }
}

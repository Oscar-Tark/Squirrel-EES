using System;
using System.Collections;
using System.Collections.Generic;

namespace Internetwork_File_Format
{
    public class Internetwork_Video_File_Format
    {
        Scorpion.Form1 Do_on;
        public Internetwork_Video_File_Format(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
        }

        public void add_vector(ref string Scorp_Line)
        {
            //(*name,*type,*var,*var....)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line);
            Do_on.AL_OBJ_3D_REF.Add(Do_on.readr.lib_SCR.var_get(al[0].ToString()));
            al.RemoveAt(0);
            //Do_on.AL_OBJ_3D.Add(Do_on.readr.lib_SCR.cut_symbol(ref al));

            List<double> l = new List<double>();
            for (int i = 2; i < al.Count - 1; i++)
            {
                try
                {
                    l.Add(Convert.ToDouble(Do_on.readr.lib_SCR.var_get(al[i].ToString())));
                }
                catch { break; }
            }

            Do_on.AL_OBJ_3D.Add(new ArrayList() { Do_on.readr.lib_SCR.var_get(al[1].ToString()), l });

            //EVALUATE REMOVE al
            Do_on.write_to_cui("Control Added");

            Scorp_Line = null;

            return;
        }

        public void remove_vector(ref string Scorp_Line)
        {
            //(*name,*name,*name)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(ref Scorp_Line);
            foreach (string s in al)
            {
                Do_on.AL_OBJ_3D.RemoveAt(Do_on.AL_OBJ_3D_REF.IndexOf(Do_on.readr.lib_SCR.var_get(s)));
                Do_on.AL_OBJ_3D_REF.Remove(Do_on.readr.lib_SCR.var_get(s));
            }

            Do_on.AL_OBJ_3D.TrimToSize();
            Do_on.AL_OBJ_3D_REF.TrimToSize();

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);

            return;
        }

        public void send_vector(ref string Scorp_Line)
        {
            //(*reciever_name,*object)
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(ref Scorp_Line);
            
            //!PROBLEM!
            //Do_on.amcl.broadcast(Do_on.readr.lib_SCR.var_get(al[0].ToString()).ToString(), Do_on.readr.lib_SCR.var_get(al[1].ToString()).ToString(), Scorp_Line);

            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }

        public void vector_recieved(ref string Scorp_Line)
        {

        }
    }
}
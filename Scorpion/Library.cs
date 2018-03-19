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

//Static Library
namespace Scorpion
{
    public partial class Librarian
    {
        private delegate void del_do(object Scorp_Line);
        private delegate void del_do_rf_str(ref string Scorp_Line);
        private delegate void del_do_rf_obj(object Scorp_Line);

        public bool pointered = false;
        string Temp_str = "";
        public TreeNode tn_tmp;
        public string Item_type;
        public bool cuimode = false;
        Scorpion_IDE.Special_TextView cui_stview = new Scorpion_IDE.Special_TextView();
        bool Continue_SCR = true;
        public Form1 Do_on;
        reader rdd;

        public NotifyIcon nfy_tmp;
        public bool resend = false;
        
        public string strng_tmp;
        public ArrayList AL_Ref_EVT = new ArrayList();
        

        public object FC;

        public bool Decider(string Method, Form1 Frm, reader rd_send)
        {
            rdd = rd_send;
            Temp_str = Method;
            try
            {
                Do_on = Frm;
                /*if (resend == true)
                {
                    FC = fm_temp;
                }
                else if (resend == false)
                {
                    FC = Do_on;
                }*/
            }
            catch { }
            do_(Method);
            return Continue_SCR;
        }

        public void do_(string Scorp_Line)
        {
            try
            {
                Thread th_doo = new Thread(new ParameterizedThreadStart(do2_));
                th_doo.IsBackground = true;
                th_doo.Start((object)Scorp_Line);
            }
            catch { }
        }

        private void do2_(object Scorp_Line)
        {
            try
            {
                del_do ddo = new del_do(work_);
                ddo.Invoke(Scorp_Line);

                ddo = null;
            }
            catch (Exception erty) { MessageBox.Show(erty.Message + " (" + erty.StackTrace + ")"); }
        }
        
        /*ENGINE ENTRY POINT*/
        public void work_(object Scorp_Line)
        {
            string Scorp_Line_Exec = Scorp_Line.ToString();
            //LIB
            try
            {
                if (Scorp_Line_Exec.Contains(Do_on.AL_ACC[0].ToString()))
                {
                    string Temp_keep = Scorp_Line_Exec;
                    int n = Scorp_Line_Exec.IndexOf(Do_on.AL_ACC[0].ToString(), 0);
                    Temp_keep = Temp_keep.Remove(n);

                    try
                    {
                        Temp_keep = Temp_keep.TrimStart(null);
                    }
                    catch { }

                    int ndxx = 0;
                    Temp_keep = Temp_keep.Replace("*", "");
                    ndxx = Do_on.AL_CURR_VAR_REF.IndexOf(Temp_keep);

                    //PROCESS AFTER THE >>
                    try
                    {
                        string Temp_keep2 = Scorp_Line_Exec;
                        int n3 = Temp_keep2.IndexOf(Do_on.AL_ACC[0].ToString(), 0);
                        int n4 = 0;
                        try
                        {
                            n4 = Temp_keep2.IndexOf(");", n3);
                        }
                        catch { n4 = Temp_keep2.IndexOf("\n", n3); }
                        Temp_keep2 = Temp_keep2.Remove(0, n3 + 2);

                        if (Temp_keep2.StartsWith(Do_on.AL_ACC[1].ToString()))
                        {
                            ((ArrayList)Do_on.AL_CURR_VAR[ndxx])[2] = var_get(Temp_keep2);
                        }
                        else
                        {
                            ((ArrayList)Do_on.AL_CURR_VAR[ndxx])[2] = Temp_keep2;
                        }

                        //Object Event Call
                        if (((ArrayList)Do_on.AL_CURR_VAR[ndxx])[4].ToString() != "")
                        {
                            call_function(((ArrayList)Do_on.AL_CURR_VAR[ndxx])[4].ToString());
                        }
                    }
                    catch (Exception erty) { Do_on.write_to_cui("Error: " + erty); }
                }
                else
                {
                    //hyper threading
                    if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[0].ToString()))
                    {
                        del_do_rf_str ddo3 = new del_do_rf_str(Functions);
                        ddo3.Invoke(ref Scorp_Line_Exec);
                        
                        //clean
                        ddo3 = null;
                    }
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[1].ToString()))
                    {
                        del_do_rf_str ddo4 = new del_do_rf_str(IO);
                        ddo4.Invoke(ref Scorp_Line_Exec);
                        
                        //clean
                        ddo4 = null;
                    }
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[2].ToString()))
                    {
                        del_do_rf_str ddo5 = new del_do_rf_str(NET);
                        ddo5.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddo5 = null;
                    }
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[3].ToString()))
                    {
                        del_do_rf_str ddo6 = new del_do_rf_str(APP);
                        ddo6.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddo6 = null;
                    }
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[4].ToString()))
                    {
                        del_do_rf_str ddo8 = new del_do_rf_str(DATA);
                        ddo8.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddo8 = null;
                    }
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[5].ToString()))
                    {
                        del_do_rf_str ddo10 = new del_do_rf_str(MEMORY);
                        ddo10.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddo10 = null;
                    }
                    /*else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[8].ToString()))
                    {
                        del_do_rf_str ddo10 = new del_do_rf_str(Type_);
                        ddo10.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddo10 = null;
                    }*/
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[6].ToString()))
                    {
                        del_do_rf_str ddo81 = new del_do_rf_str(GUI);
                        ddo81.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddo81 = null;
                    }
                    else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[7].ToString()))
                    {
                        del_do_rf_str ddoshs = new del_do_rf_str(SHS);
                        ddoshs.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddoshs = null;
                    }
                    /*else if (Scorp_Line.ToString().ToLower().StartsWith(Do_on.AL_ACC_SUP[9].ToString()))
                    {
                        del_do_rf_str ddoshs = new del_do_rf_str(.encrypt_data);
                        ddoshs.Invoke(ref Scorp_Line_Exec);

                        //clean
                        ddoshs = null;
                    }*/
                    else
                    {
                        Do_on.Mess = "NO DIRECTIVE FOUND FOR: " + Scorp_Line_Exec + ". \n\nAvailable Directives are {";
                        foreach (string s in Do_on.AL_ACC_SUP)
                        { Do_on.Mess += "[" + s + "]"; }
                        Do_on.write_to_cui(Do_on.Mess + "}");
                    }
                    //END-->
                }

                //clean
                Scorp_Line = null;
            }
            catch (Exception erty)
            {
                Do_on.Continue = false; 
                rdd.Re_do = false;
                Do_on.write_to_cui(erty.Message + "\n\n" + " {Stack: " + erty.StackTrace + "}" + "\n\n" + "[Line of Code that Caused the Error : " + Scorp_Line_Exec + "]");
            }

            pointered = false;
            Scorp_Line_Exec = null;
            GC.Collect();

            return;
        }

        public void return_result()
        {
            //returnto

            /*
            
            tcp->al{}->work{}->fucntion(gui,mem...)->return_result(){returnarray}
            array{command, returnto, id, tempid, return variables{ref,val}}

            */

            //tcp//local//driver//client
            return;
        }

        void del_mem_Click(object sender, EventArgs e)
        {
            Do_on.AL_CURR_VAR.Clear();
            Do_on.AL_CURR_VAR_REF.Clear();
            Do_on.AL_GUI_TEMPLATES.Clear();
        }

        void toolStripButton3_Click(object sender, EventArgs e)
        {
            int ndx = 0;
            foreach (char c in cui_stview.rtb.Text)
            {
                try
                {
                    do_(cui_stview.rtb.Lines[ndx]);
                    ndx++;
                }
                catch { }
            }
        }

        void spc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                try
                {
                    int nCUR = cui_stview.rtb.GetLineFromCharIndex(cui_stview.rtb.SelectionStart);
                    string s_rd = cui_stview.rtb.Lines[nCUR];
                    do_(s_rd);
                }
                catch { }
            }
        }

        //Common functions
        public string cut_parenth(ref string Scorp_Line_Exec)
        {
            Scorp_Line_Exec = Scorp_Line_Exec.Remove(Scorp_Line_Exec.IndexOf("("));
            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, Scorp_Line_Exec.IndexOf(")") + 1);
        }

        public string cut_parenth(ref string Scorp_Line_Exec, string Del1, string Del2)
        {
            int ndx = Scorp_Line_Exec.IndexOf(Del1, 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx);

            string new_ = Scorp_Line_Exec.Remove(ndx2);
            new_ = new_.Remove(0, ndx + Del1.Length);

            return new_;
        }

        public string cut_custom(ref string Scorp_Line_Exec, string Del1, string Del2)
        {
            int ndx = Scorp_Line_Exec.IndexOf(Del1);
            int ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx);

            string EDIT_Scorp_Line_Exec = Scorp_Line_Exec.Remove(ndx2);
            return EDIT_Scorp_Line_Exec = EDIT_Scorp_Line_Exec.Remove(0, ndx + Del1.Length);
        }

        public string cut_custom(string Scorp_Line_Exec, string Del1, string Del2)
        {
            int ndx = Scorp_Line_Exec.IndexOf(Del1);
            int ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx);

            Scorp_Line_Exec = Scorp_Line_Exec.Remove(ndx2);
            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, ndx + Del1.Length);
        }

        public string cut_custom(ref string Scorp_Line_Exec, string Del1, string Del2, int start_at)
        {
            int ndx = Scorp_Line_Exec.IndexOf(Del1, start_at);
            int ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx);

            Scorp_Line_Exec = Scorp_Line_Exec.Remove(ndx2);
            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, ndx + Del1.Length);
        }

        public string cut_custom(string Scorp_Line_Exec, string Del1, string Del2, int start_at)
        {
            int ndx = Scorp_Line_Exec.IndexOf(Del1, start_at);
            int ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx);

            Scorp_Line_Exec = Scorp_Line_Exec.Remove(ndx2);
            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, ndx + Del1.Length);
        }

        public string cut_custom_progressive(string Scorp_Line_Exec, string Del1, string Del2, bool is_final_char)
        {
            int ndx = 0;
            int ndx2 = 0;
            
            if (!is_final_char)
            {
                ndx = Scorp_Line_Exec.IndexOf(Del1);
                ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx + 1);
                Scorp_Line_Exec = Scorp_Line_Exec.Remove(ndx2);
            }
            else
            {
                ndx = Scorp_Line_Exec.LastIndexOf(Del1);
            }

            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, ndx + Del1.Length);
        }

        public string cut_custom_domain(ref string Scorp_Line_Exec, string Del1, string Del2)
        {
            int ndx = Scorp_Line_Exec.IndexOf(Del1);
            int ndx2 = Scorp_Line_Exec.IndexOf(Del2, ndx + 1);

            Scorp_Line_Exec = Scorp_Line_Exec.Remove(ndx2);
            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, ndx + Del1.Length);
        }

        public ArrayList cut_custom_domain(string Scorp_Line_Exec)
        {
            return new ArrayList() { cut_custom(Scorp_Line_Exec, "*", "@"), cut_custom_progressive(Scorp_Line_Exec, "@", "@", false), cut_custom_progressive(Scorp_Line_Exec, "@", "", true) };
        }

        public ArrayList cut_custom_subvar(string Scorp_Line_Exec)
        {
            return new ArrayList() { cut_custom(Scorp_Line_Exec, "*", "#"), cut_custom_progressive(Scorp_Line_Exec, "@", "", true) };
        }

        public ArrayList cut_custom_subvar(ref string Scorp_Line_Exec)
        {
            return new ArrayList() { cut_custom(Scorp_Line_Exec, "*", "#"), cut_custom_progressive(Scorp_Line_Exec, "@", "", true) };
        }

        static T DirectCast<T>(object o, Type type) where T : class
        {
            if (!(type.IsInstanceOfType(o)))
            {
                throw new ArgumentException();
            }
            T value = o as T;
            if (value == null && o != null)
            {
                throw new InvalidCastException();
            }
            return value;
        }

        private object CloneObject(object o)
        {
            Type t = o.GetType();
            PropertyInfo[] properties = t.GetProperties();

            Object p = t.InvokeMember("", System.Reflection.BindingFlags.CreateInstance, null, o, null);

            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanWrite)
                {
                    pi.SetValue(p, pi.GetValue(o, null), null);
                }
            }

            return p;
        }

        public ArrayList cut_variables(string Scorp_Line_Exec)
        {
            //(*v),(,*v),[*v],[*v,],[*v ]
            int ndx = 0; int ndx2 = 0; ArrayList al = new ArrayList();
            foreach (char c in Scorp_Line_Exec)
            {
                ndx = Scorp_Line_Exec.IndexOf("*", ndx);
                if (ndx == -1) { break; }
                al.Add(Scorp_Line_Exec.Remove(0, ndx));

                al[ndx2] = cut_next_unbearable(al[ndx2].ToString(), ndx);

                ndx++;
                ndx2++;
            }

            return al;
        }

        public ArrayList cut_variables(ref string Scorp_Line_Exec)
        {
            //(*v),(,*v),[*v],[*v,],[*v ]
            int ndx = 0; int ndx2 = 0; ArrayList al = new ArrayList();
            foreach (char c in Scorp_Line_Exec)
            {
                ndx = Scorp_Line_Exec.IndexOf("*", ndx);
                if (ndx == -1) { break; }
                al.Add(Scorp_Line_Exec.Remove(0, ndx));

                al[ndx2] = cut_next_unbearable(al[ndx2].ToString(), ndx);

                ndx++;
                ndx2++;
            }

            return al;
        }

        public ArrayList cut_query(ref string Scorp_Line_Exec)
        {
            //"Name=Benny&&Age=24"
            ArrayList al = new ArrayList();
            int ndx = 0;
            int ndx2;

            int ndxOPR = 0;
            foreach(char s in Scorp_Line_Exec)
            {
                 
            }

            return al;
        }

        public ArrayList return_query_variables (string Scorp_Line_Exec)
        {
            //<Cell></Cell><Operator></Operator><Cell></Cell>
            ArrayList al = new ArrayList();
            for(int i = 0; i < 2; i++)
            {
                if (i == 0)
                { al.Add(cut_custom(ref Scorp_Line_Exec, "<opr1>", "</opr1>")); }
                else if (i == 1)
                { al.Add(cut_custom(ref Scorp_Line_Exec, "<opr>", "</opr>")); }
                else { al.Add(cut_custom(ref Scorp_Line_Exec, "<opr2>", "</opr2>")); }
            }

            Scorp_Line_Exec = null;
            return al;
        }

        public ArrayList cut_commands(ref string Scorp_Line_Exec, ref ArrayList append, int index)
        {
            //(''),(,''),[''],['',],['' ]
            int ndx = 0; int ndx2 = 0;// ArrayList al = new ArrayList();
            append.RemoveRange(index, append.Count - (index + 1));
            foreach (char c in Scorp_Line_Exec)
            {
                ndx = Scorp_Line_Exec.IndexOf("'", ndx);
                ndx2 = Scorp_Line_Exec.IndexOf("'", ndx + 1);
                if (ndx == -1 || ndx2 == -1) { break; }

                append.Add(Scorp_Line_Exec.Remove(ndx2).Remove(0,ndx+1));
               
                ndx++;
            }
         

            return append;
        }

        public ArrayList cut_symbol(ref ArrayList al)
        {
            for (int i = 0; i < al.Count; i++)
            {
                al[i] = var_get(al[i].ToString());
            }

            return al;
        }

        private string cut_next_unbearable(string Scorp_Line_Exec, int Starting_From)
        {
            int u1 = -1; int u2 = -1; int u3 = -1; int u4 = -1;

                u1 = Scorp_Line_Exec.IndexOf(Do_on.AL_UNBEARABLE_CHARS[0].ToString(), 0);
                u2 = Scorp_Line_Exec.IndexOf(Do_on.AL_UNBEARABLE_CHARS[1].ToString(), 0);
                u3 = Scorp_Line_Exec.IndexOf(Do_on.AL_UNBEARABLE_CHARS[2].ToString(), 0);
                //u4 = Scorp_Line_Exec.IndexOf(Do_on.AL_UNBEARABLE_CHARS[3].ToString(), 0);

                if (u1 == -1)
                {
                    u1 = Scorp_Line_Exec.Length + 100;
                }
                if (u2 == -1)
                {
                    u2 = Scorp_Line_Exec.Length + 100;
                }
                if (u3 == -1)
                {
                    u3 = Scorp_Line_Exec.Length + 100;
                }
                /*if (u4 == -1)
                {
                    u4 = Scorp_Line_Exec.Length + 100;
                }*/

            if ((u1 < u2 && u1 < u3/* && u1 < u4*/) && u1 != -1)
            {
                Scorp_Line_Exec = Scorp_Line_Exec.Remove(u1);
            }
            else if ((u2 < u1 && u2 < u3/* && u2 < u4*/) && u2 != -1)
            {
                Scorp_Line_Exec = Scorp_Line_Exec.Remove(u2);
            }
            else if ((u3 < u1 && u3 < u2/* && u3 < u4*/) && u3 != -1)
            {
                Scorp_Line_Exec = Scorp_Line_Exec.Remove(u3);
            }/*
            else if ((u4 < u1 && u4 < u2 && u4 < u3) && u4 != -1)
            {
                Scorp_Line_Exec = Scorp_Line_Exec.Remove(u4);
            }*/

            return Scorp_Line_Exec;
        }

        public string cut_square_parenth_LEADING_TRACE(ref string Scorp_Line_Exec, int Trail, char Last_Del)
        {
            int index = -1;
            for (int i = 0; i < Trail; i++)
            {
                if (i == Trail - 1)
                {
                    index = Scorp_Line_Exec.IndexOf("*", index + 1);
                }
                else { index = Scorp_Line_Exec.IndexOf("*", index + 1); }
            }

            Scorp_Line_Exec = Scorp_Line_Exec.Remove(Scorp_Line_Exec.IndexOf(Last_Del));
            return Scorp_Line_Exec = Scorp_Line_Exec.Remove(0, index);
        }
    }
}
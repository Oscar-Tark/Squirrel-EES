﻿/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
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
                object[] paramse = new object[1];
                paramse[0] = Scorp_Line_Exec;

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
                Do_on.write_to_cui("There was an error while processing your function call [Line of Code that Caused the Error : >> " + Scorp_Line_Exec + "]");
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
                    scorpioniee(cui_stview.rtb.Lines[ndx]);
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
                    scorpioniee(s_rd);
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
            return split_vars(ref Scorp_Line_Exec);
        }

        public ArrayList cut_variables(ref string Scorp_Line_Exec)
        {
            return split_vars(ref Scorp_Line_Exec);
        }

        private ArrayList split_vars(ref string Scorp)
        {
            string[] vars = Scorp.Split('*', '(', ')', ',');
            ArrayList vars_ = new ArrayList();
            int ndx = 0;
            foreach (string s in vars)
            {
                if (s != "" && s != " " && ndx != 0)
                {
                    vars_.Add(s);
                }
                ndx++;
            }
            return vars_;
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
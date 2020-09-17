
using System;
using System.Collections;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
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

            //Allow value variables with ubearables

            ArrayList vars_ = new ArrayList();
            int ndx = 0;
            foreach (string s in vars)
            {
                if (s != "" && s != " " && ndx != 0 && s.EndsWith(">>", StringComparison.CurrentCulture) != true)
                    vars_.Add(s);
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
            foreach (char s in Scorp_Line_Exec)
            {

            }

            return al;
        }

        public ArrayList return_query_variables(string Scorp_Line_Exec)
        {
            //<Cell></Cell><Operator></Operator><Cell></Cell>
            ArrayList al = new ArrayList();
            for (int i = 0; i < 2; i++)
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

                append.Add(Scorp_Line_Exec.Remove(ndx2).Remove(0, ndx + 1));

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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Scorpion
{
    public partial class Obj_vw : Form
    {
        Form1 Do_on;
        public Obj_vw(Form1 fm1)
        {
            Do_on = fm1;
            InitializeComponent();
            IntPtr Hicon = Properties.Resources.magnifier.GetHicon();
            this.Icon = Icon.FromHandle(Hicon);
            this.Show();
            bkk_ld.RunWorkerAsync();
        }

        private void th_ld_strt()
        {
            new Thread(new ThreadStart(del_ld_strt)).Start();
        }

        private void del_ld_strt()
        {
            this.Invoke(new del_ld(load_objects));
        }

        private void load_objects()
        {
            tv.Visible = false;
            tv.Nodes.Find("NodeRef", true)[0].Text = "References: " + Do_on.AL_CURR_VAR_REF.Count.ToString();
            tv.Nodes.Find("NodeRef", true)[0].Nodes.Clear();
            foreach (object obj in Do_on.AL_CURR_VAR_REF)
            {
                try
                {
                    tv.Nodes.Find("NodeRef", true)[0].Nodes.Add(obj.ToString());
                }
                catch
                {
                    tv.Nodes.Find("NodeRef", true)[0].Nodes.Add("!Unaccessible Value!");
                }
            }

            tv.Nodes.Find("Node16", true)[0].Text = "Objects: " + Do_on.AL_CURR_VAR.Count.ToString();
            tv.Nodes.Find("Node16", true)[0].Nodes.Clear();
            int ndx = 0; 
            foreach (object obj in Do_on.AL_CURR_VAR)
            {
                string s = "";
                foreach (object o in ((System.Collections.ArrayList)Do_on.AL_CURR_VAR[ndx]))
                {
                    try
                    {
                        s = s + (" ('" + o.ToString() + "') ");
                    }
                    catch
                    {
                        s = s + "!Unaccessible Value!";
                    }
                }
                tv.Nodes.Find("Node16", true)[0].Nodes.Add(Do_on.AL_CURR_VAR_REF[ndx].ToString() + ": " + s);

                ndx++;
            }

            int index = 0;
            tv.Nodes.Find("rf_fnc_cnt", true)[0].Text = "References: " + Do_on.AL_Ref_EVT.Count.ToString();
            tv.Nodes.Find("rf_fnc_cnt", true)[0].Nodes.Clear();
            index = 0;
            foreach (object obj in Do_on.AL_Ref_EVT)
            {
                try
                {
                    tv.Nodes.Find("rf_fnc_cnt", true)[0].Nodes.Add("[" + Do_on.AL_Ref_EVT[index].ToString() + "] " + obj.ToString());
                }
                catch
                {
                    tv.Nodes.Find("rf_fnc_cnt", true)[0].Nodes.Add("!Unaccessible Value!");
                }
                index++;
            }

            tv.Nodes.Find("fnc_cnt", true)[0].Text = "Functions: " + Do_on.AL_EVT.Count.ToString();
            tv.Nodes.Find("fnc_cnt", true)[0].Nodes.Clear();
            index = 0;
            foreach (object obj in Do_on.AL_EVT)
            {
                try
                {
                    tv.Nodes.Find("fnc_cnt", true)[0].Nodes.Add("[" + Do_on.AL_EVT[index].ToString() + "] " + obj.ToString());
                }
                catch
                {
                    tv.Nodes.Find("fnc_cnt", true)[0].Nodes.Add("!Unaccessible Value!");
                }
                index++;
            }

            index = 0;
            tv.Nodes.Find("shs", true)[0].Text = "SHS: " + Do_on.AL_SHS_APP.Count.ToString();
            tv.Nodes.Find("shs", true)[0].Nodes.Clear();
            foreach (object obj in Do_on.AL_SHS_APP)
            {
                try
                {
                    tv.Nodes.Find("shs", true)[0].Nodes.Add("", "[" + Do_on.AL_SHS_APP_REF[index].ToString() + "] " + obj.ToString(),259);
                }
                catch
                {
                    tv.Nodes.Find("shs", true)[0].Nodes.Add("!Unaccessible Value!");
                }
                index++;
            }

            index = 0;
            tv.Nodes.Find("amcs", true)[0].Text = "Network Connections: " + Do_on.AL_SOCK.Count.ToString();
            tv.Nodes.Find("amcs", true)[0].Nodes.Clear();
            foreach (object obj in Do_on.AL_SOCK_REF)
            {
                try
                {
                    tv.Nodes.Find("amcs", true)[0].Nodes.Add("", "(" + obj.ToString() + ")" + "[" + ((System.Net.Sockets.Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf(obj.ToString())]).RemoteEndPoint.ToString() + "]" + " -> [" + ((System.Net.Sockets.Socket)Do_on.AL_SOCK[Do_on.AL_SOCK_REF.IndexOf(obj.ToString())]).RemoteEndPoint.ToString() + "]", 472);
                }
                catch
                {
                    tv.Nodes.Find("amcs", true)[0].Nodes.Add("!Unaccessible Value!");
                }
                index++;
            }

            //odbcll
            index = 0;
            tv.Nodes.Find("odbcll", true)[0].Text = "Cell Pools: " + Do_on.AL_TBLE.Count.ToString();
            tv.Nodes.Find("odbcll", true)[0].Nodes.Clear();
            foreach (string s in Do_on.AL_TBLE_REF)
            {
                try
                {
                    tv.Nodes.Find("odbcll", true)[0].Nodes.Add("", s, 511);
                    foreach (System.Collections.ArrayList al in ((System.Collections.ArrayList)Do_on.AL_TBLE_REF[index]))
                    {
                        tv.Nodes.Find("odbcll", true)[0].Nodes[index].Nodes.Add(al.ToString());
                    }
                }
                catch
                {
                    tv.Nodes.Find("odbcll", true)[0].Nodes.Add("!Unaccessible Value!");
                }

                /*foreach(string s1 in ((System.Collections.ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(s)]))
                {
                    tv.Nodes.Find("odbcll", true)[0].Nodes[tv.Nodes.Find("odbcll", true)[0].Nodes.Count - 1].Nodes.Add(s1); ;
                }*/


                index++;
            }

            //wff
            index = 0;
            tv.Nodes.Find("wff", true)[0].Text = "Forms: " + Do_on.AL_GUI_TEMPLATES.Count.ToString();
            tv.Nodes.Find("wff", true)[0].Nodes.Clear();
            foreach (object o in Do_on.AL_GUI_TEMPLATES)
            {
                try
                {
                    tv.Nodes.Find("wff", true)[0].Nodes.Add("", o.GetType().ToString(), 511);
                }
                catch
                {
                    tv.Nodes.Find("wff", true)[0].Nodes.Add("!Unaccessible Value!");
                }
                index++;
            }

            //wfrf
            index = 0;
            tv.Nodes.Find("wfref", true)[0].Text = "References: " + Do_on.AL_GUI_TEMPLATES_REF.Count.ToString();
            tv.Nodes.Find("wfref", true)[0].Nodes.Clear();
            foreach (string s in Do_on.AL_GUI_TEMPLATES_REF)
            {
                try
                {
                    tv.Nodes.Find("wfref", true)[0].Nodes.Add("", s, 389);
                }
                catch
                {
                    tv.Nodes.Find("wfref", true)[0].Nodes.Add("!Unaccessible Value!");
                }
                index++;
            }

            tv.Visible = true;

            return; 
        }

        private void Obj_vw_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            bkk_ld.RunWorkerAsync();
        }

        delegate void del_ld();
        private void bkk_ld_DoWork(object sender, DoWorkEventArgs e)
        {
            th_ld_strt();
        }
    }
}

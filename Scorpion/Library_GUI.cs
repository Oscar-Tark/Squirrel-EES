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

//DEPRECIATED
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        public void GUI(ref string Scorp_Line_)
        {
            //WINFORMS !INCOMPLETE NOT FOR MAIN USAGE!
            if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[20] + Do_on.AL_ACC[3].ToString()))
            {
                Create_GUI_element(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[107] + Do_on.AL_ACC[3].ToString()))
            {
                Compile_GUI_Properties();
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[108] + Do_on.AL_ACC[3].ToString()))
            {
                GUI_Edit_Properties(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[109] + Do_on.AL_ACC[3].ToString()))
            {
                GUI_Call_Method(ref Scorp_Line_);
            }

            //OPENTK/OPENGL
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[16] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.RS.load_game(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[17] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.RS.stop_Game_engine();
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[18] + Do_on.AL_ACC[3].ToString()))
            {
                Do_on.RS.set_Game_vsync(ref Scorp_Line_);
            }
            else if (Scorp_Line_.ToLower().ToString() == Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[22] + Do_on.AL_ACC[3].ToString())
            {
                Do_on.RS.reset_all_resolutions();
            }


            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[19] + Do_on.AL_ACC[3].ToString()))
            {
                //EMPTY
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[20] + Do_on.AL_ACC[3].ToString()))
            {
                //EMPTY
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[106] + Do_on.AL_ACC[3].ToString()))
            {
                //EMPTY
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[90] + Do_on.AL_ACC[3].ToString()))
            {
                //EMPTY
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[6] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[91] + Do_on.AL_ACC[3].ToString()))
            {
                //EMPTY
            }
            else { Do_on.write_to_cui("NO FUNCTION FOUND FOR DIRECTIVE {" + Do_on.AL_ACC_SUP[6] + "} in line {" + Scorp_Line_ + "}"); }
            //clean
            Scorp_Line_ = null;

            return;
        }


        //WINFORMS
        /*
         * Forms in scorpion are limited to the following:
         * > Forms
         * > Button
         * > Check and Radio Buttons
         * > Textboxes
         * > Comboboxes
         * > TBD.
         */

        //2D Controls
        //2D Controls
        public void Create_GUI_element(ref string Scorp_Line)
        {
            //(*type,*name)
            ArrayList al = cut_variables(ref Scorp_Line);

            //WINFORMS

            Do_on.AL_GUI_TEMPLATES.Add(GUI_Type(var_get(al[0].ToString()).ToString()));
            Do_on.AL_GUI_TEMPLATES_REF.Add(var_get(al[1].ToString()));

            //GUI_Initialize(ref al);
            //GUI_Edit_Properties(ref al);
            
            //CLEAN
            Scorp_Line = null;
            //var_arraylist_dispose(ref al);

            return;
        }

        public void Delete_GUI_element(ref string Scorp_Line)
        {
            Scorp_Line = null;
            return;
        }

        
        public object GUI_Type(string Type_)
        {
            return Activator.CreateInstance(Do_on.AL_GUI_TEMPLATES[Do_on.AL_GUI_TEMPLATES_REF.IndexOf(var_cut_symbol(Type_))].GetType());
        }

        public void GUI_Call_Method(ref string Scorp_line)
        {
            //(*name,*method,*parameter)
            ArrayList al = cut_variables(ref Scorp_line);
            //Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[0].ToString()))].GetType().GetMethod(var_get(al[0].ToString()).ToString(), BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.DeclaredOnly).Invoke(Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[0].ToString()))], null);
            MethodInfo mi = Do_on.AL_GUI_TEMPLATES[Do_on.AL_GUI_TEMPLATES_REF.IndexOf(var_get(al[0].ToString()))].GetType().GetMethod(var_get(al[1].ToString()).ToString(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            //generate parameters
            object[] obj = new object[al.Count - 2];
            
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i] = var_get(al[i + 2].ToString());
            }
            //obj[0] = Do_on;

            try
            {
                mi.Invoke(Do_on.AL_GUI_TEMPLATES[Do_on.AL_GUI_TEMPLATES_REF.IndexOf(var_get(al[0].ToString()))], obj);
            }
            catch (Exception erty) { Do_on.write_to_cui(Do_on.AL_MESSAGE[3].ToString() + "\n\nMessage:\n\n" + erty.Message); }

            //clean
            var_arraylist_dispose(ref al);
            obj[0] = null;
            obj = null;

            return;
        }

        public void GUI_Edit_Properties(ref string Scorp_Line)
        {
            //dynamically allocated
            //(*name,*property,*value)
            ArrayList al = cut_variables(ref Scorp_Line);

            PropertyInfo pi = ((Form)Do_on.AL_GUI_TEMPLATES[Do_on.AL_GUI_TEMPLATES_REF.IndexOf(var_get(al[0].ToString()))]).GetType().GetProperty(var_get(al[1].ToString()).ToString());
            pi.SetValue(((Form)Do_on.AL_GUI_TEMPLATES[Do_on.AL_GUI_TEMPLATES_REF.IndexOf(var_get(al[0].ToString()))]), Convert.ChangeType(var_get(al[2].ToString()).ToString(), pi.PropertyType), null);

            var_arraylist_dispose(ref al);
            Scorp_Line = null;

            return;
        }

        public void Compile_GUI_Properties()
        {
            Do_on.AL_GUI_PROPERTIES.Clear();
            Do_on.AL_GUI_PROPERTIES.TrimToSize();

            foreach (Object o in Do_on.AL_GUI_TEMPLATES)
            {
                Do_on.write_to_cui("Creating properties for: " + o.GetType().ToString());
                Do_on.AL_GUI_PROPERTIES.Add(o.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public));
            }

            return;
        }


        //-----------------------OTHER/OLD/DEPRECIATED FUNCTIONS---------------------

        //WINFORMS



        /*
         * Forms in scorpion are limited to the following:
         * > Forms
         * > Button
         * > Check and Radio Buttons
         * > Textboxes
         * > Comboboxes
         * > TBD.
         */

        //2D Controls
        //2D Controls
        /*public void Create_GUI_element(ref string Scorp_Line)
        {
            //(*type,*name,*txt,*fg,*bg,*anchor,*dock,*parent,*event)
            ArrayList al = cut_variables(ref Scorp_Line);

            //WINFORMS
            /*
            Do_on.AL_GUI.Add(GUI_Type(var_get(al[0].ToString()).ToString()));
            Do_on.AL_GUI_REF.Add(var_get(al[1].ToString()));

            GUI_Initialize(ref al);
            GUI_Initialize_Properties(ref al);
            */

        //CLEAN
        /*Scorp_Line = null;
        var_arraylist_dispose(ref al);

        return;
    }*/

        /*
        public object GUI_Type(string Type_)
        {
            return Activator.CreateInstance(Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_cut_symbol(Type_))].GetType());
        }

        public void GUI_Initialize(ref ArrayList al)
        {
            if (Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))] is Form)
            {
                ((Form)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]).Show();
            }
            else
            {
                if (Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[7].ToString()))] is Form)
                {
                    ((Form)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[7].ToString()))]).Controls.Add((Control)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]);
                }
                else
                {
                    ((Control)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[7].ToString()))]).Controls.Add((Control)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]);
                }
            }

            return;
        }

        public void GUI_Initialize_Properties(ref ArrayList al)
        {
            //(*type,*name,*txt,*fg,*bg,*anchor,*dock,*parent,/*event)*/
        /*
        int ndx = 1;
        foreach (String s in Do_on.AL_GUI_PROPERTIES)
        {
            try
            {
                if (Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))] is Form)
                {//.SetValue(Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))],var_get(al[1].ToString()), 0);
                    PropertyInfo pi = ((Form)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]).GetType().GetProperty(s);
                    pi.SetValue(((Form)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]), Convert.ChangeType(var_get(al[ndx].ToString()), pi.PropertyType), null);
                }
                else
                {
                    PropertyInfo pi = ((Control)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]).GetType().GetProperty(s);
                    pi.SetValue(((Control)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]), Convert.ChangeType(var_get(al[ndx].ToString()), pi.PropertyType), null);
                    //((Control)Do_on.AL_GUI[Do_on.AL_GUI_REF.IndexOf(var_get(al[1].ToString()))]).Name = var_get(al[1].ToString()).ToString();
                }
            }
            catch { /*break;*/
    }
    /*ndx++;
}
}*/

    //Works on the basis of templates
    /*public void Create(ref string Scorp_Line_Control)
    {
        //create([name]XML);
        Object obj = return_Template(ref Scorp_Line_Control);
        if (obj is Form)
        {
            Do_on.AL_WINS.Add(((Form)obj));
            Do_on.STR_INDEXER.Add(cut_custom(ref Scorp_Line_Control, "[", "]"));
            ((Form)Do_on.AL_WINS[Do_on.AL_WINS.Count - 1]).Show();
        }
        else
        { 
            //<parent></parent>
        }

        return;
    }

    public void Edit(ref string Scorp_Line_Control)
    {
        //edit(XML)
        set_Properties(ref Scorp_Line_Control);

        return;
    }

    public void Delete(ref string Scorp_Line_Control)
    {
        //delete(XML)
        return;
    }

    public void start_vee_xml()
    {
        Do_on.vee_xml = new _3d_FORM(Do_on);
        return;
    }

    public void stop_vee_xml()
    {
        Do_on.vee_xml.Close();
        return;
    }

    public object return_Template(ref string Scorp_Line_Exec)
    {
        return CloneObject(var_get(cut_custom(ref Scorp_Line_Exec, "<control>", "</control>")));
    }

    public void set_Properties(ref string Scorp_Line_Exec)
    {
        //Automated Property Accessor
        //size(*,*),location(*,*),text(*),dock(*),anchor(*),parent(*),image(*),textcolor(*),backcolor(*),textimagerel(*)

        //PropertyInfo pi = ((Control)var_get(cut_custom(ref Scorp_Line_Exec, "<control>", "</control>"))).GetType().GetProperty(var_get(cut_custom(ref Scorp_Line_Exec, "<", ">")).ToString());
    }

    public void set_events(ref string Scorp_Line_Exec)
    {
        //click(*),mousedn(*),mouseup(*),mouseovr(*),mouselve(*),textchanged(*)
        return;
    }
*/
}
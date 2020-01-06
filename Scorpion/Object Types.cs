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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using OpenTK;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Text;
using System.Windows.Forms;

namespace Scorpion
{
    public class Types
    {
        public bool B_Yes = true;
        public bool B_No = false;
        public string S_Yes = "true";
        public string S_No = "false";
        public int INDEX_NOT_FOUND = -1;
        public char C_Delim_Start = '[';
        public char C_Delim_Stop = ']';
        public string S_NULL = "";

        //COMPARERS
        public string S_Equal = "=";
        public string S_Less = "<";
        public string S_Greater = ">";
        public string S_Not_Equal = "!=";
        public string S_And = "&";
        public string S_Or = "|";

        //SHS
        public string in_ = "in";
        public string out_ = "out";

        Form1 fm_1_ref;
        public Types(Form1 fm_1)
        {
            fm_1_ref = fm_1;
            //Load_GUI_TEMPLATES();
            Load_3D_vars();

            return;
        }

        public void load_system_vars()
        {
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "one_root", fm_1_ref, "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("one_root");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "secs", S_Yes, "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("secs");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            return;
        }

        public void unload_system_vars()
        {
            try
            {
                fm_1_ref.readr.lib_SCR.delete("*one_root", new ArrayList());
                fm_1_ref.readr.lib_SCR.delete("*secs", new ArrayList());
            }
            catch { }

            return;
        }

        public void load_primary_vars()
        {
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "null", "", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("null");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            //revise functions
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "true", "true", "", "", "" });
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "false", "false", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("true");
            fm_1_ref.AL_CURR_VAR_REF.Add("false");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");


            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "in", "in", "", "", "" });
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "out", "out", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("in");
            fm_1_ref.AL_CURR_VAR_REF.Add("out");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "ip", "127.0.0.1", "", "", "" });
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "port", "5632", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("ip");
            fm_1_ref.AL_CURR_VAR_REF.Add("port");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "append", "append", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("append");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "equal", "=", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("equal");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "disequal", "!", "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("disequal");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");

            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "vsync", VSyncMode.Adaptive, "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("vsync");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "v_on", VSyncMode.On, "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("v_on");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");
            fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", "v_off", VSyncMode.Off, "", "", "" });
            fm_1_ref.AL_CURR_VAR_REF.Add("v_off");
            fm_1_ref.AL_CURR_VAR_TAG.Add("");


            /*ArrayList ColorList = new ArrayList();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", c.Name, c, "", "", "" });
                fm_1_ref.AL_CURR_VAR_REF.Add(c.Name);
            }

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fm_1_ref.AL_CURR_VAR.Add(new ArrayList { "", font.Name, font, "", "", "" });
                fm_1_ref.AL_CURR_VAR_REF.Add(font.Name);
            }*/

            //clean
            //propInfoList = null;
            //colorType = null;
            //fm_1_ref.readr.lib_SCR.var_arraylist_dispose(ref ColorList);

            return;
        }

        public void Load_3D_vars()
        {
            foreach (DisplayIndex index in Enum.GetValues(typeof(DisplayIndex)))
            {
                fm_1_ref.AL_DISP_DEVICES.Add(DisplayDevice.GetDisplay(index));
            }
            return;
        }

        public void Load_GUI_TEMPLATES()
        {
            //GUI TEMPLATES

            /*
             * Forms in scorpion are limited to the following:
             * > Forms
             * > Button
             * > Check and Radio Buttons
             * > Textboxes
             * > Comboboxes
             * > TBD.
             */
            fm_1_ref.AL_GUI_TEMPLATES.Add(new Form());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("Form");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new Button());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("Button");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new CheckBox());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("CheckBox");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new RadioButton());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("RadioButton");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new TextBox());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("TextBox");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new ComboBox());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("ComboBox");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new DataGridView());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("DataGridView");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new Label());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("Label");
            fm_1_ref.AL_GUI_TEMPLATES.Add(new LinkLabel());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("LinkLabel");

            //Containers
            fm_1_ref.AL_GUI_TEMPLATES.Add(new Panel());
            fm_1_ref.AL_GUI_TEMPLATES_REF.Add("Panel");

            fm_1_ref.GUI_TEMPLATE_COUNT = fm_1_ref.AL_GUI_TEMPLATES.Count;

            return;
        }

        //Conversions
        public bool Convert_String_To_Bool(string YN)
        {
            if (YN.ToLower() == "yes")
            {
                return true;
            }
            else if (YN.ToLower() == "no") 
            {
                return false;
            }
            else if (YN.ToLower() == "true")
            {
                return true;
            }
            else if (YN.ToLower() == "false")
            {
                return false;
            }

            //CLEAN
            YN = null;

            return false;
        }
    }
}
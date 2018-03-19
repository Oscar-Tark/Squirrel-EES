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
using System.IO;
using System.Data;
using System.Threading;
using System.Drawing;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {

        /*The 6 types
         * int
         * double
         * string
         * object
         * byte[]
         * vector
         * */

        public void STR(ref string Scorp_Line_)
        {
            //NULL
            //clean
            Scorp_Line_ = null;

            return;
        }

        //DEFUNCT
        public void Type_(ref string Scorp_Line)
        {
            //TYPE INDEPENDANT FUNCTIONS
            if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[8] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[81] + Do_on.AL_ACC[3].ToString()))
            {
                type_add(ref Scorp_Line);
            }
            else if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[8] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[82] + Do_on.AL_ACC[3].ToString()))
            {
                type_add_at(ref Scorp_Line);
            }
            else if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[8] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[83] + Do_on.AL_ACC[3].ToString()))
            {
                type_remove(ref Scorp_Line);
            }
            else if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[8] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[84] + Do_on.AL_ACC[3].ToString()))
            {
                type_remove_at(ref Scorp_Line);
            }
            else if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[8] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[85] + Do_on.AL_ACC[3].ToString()))
            {
                type_get_index(ref Scorp_Line);
            }
            else if (Scorp_Line.Contains(Do_on.AL_ACC_SUP[8] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[86] + Do_on.AL_ACC[3].ToString()))
            {
                type_get_length(ref Scorp_Line);
            }

            //clean
            Scorp_Line = null;

            return;
        }

        //PASSED
        private void type_add(ref string Line_of_Code)
        {
            //add(what(*),with(*))
            ArrayList al = cut_variables(ref Line_of_Code);
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = (var_get(al[0].ToString()).ToString() + var_get(al[1].ToString()).ToString());

            //clean
            var_arraylist_dispose(ref al);
            Line_of_Code = null;

            return;
        }

        //PASSED
        private void type_add_at(ref string Line_of_Code)
        {
            //addat(what(*),with(*),at(*))
            ArrayList al = cut_variables(ref Line_of_Code);
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = var_get(al[0].ToString()).ToString().Insert(Convert.ToInt32(var_get(al[2].ToString())), var_get(al[1].ToString()).ToString());

            var_arraylist_dispose(ref al);
            Line_of_Code = null;
            return;
        }

        //PASSED
        private void type_remove(ref string Line_of_Code)
        {
            //rem(what(*),with(*))
            ArrayList al = cut_variables(ref Line_of_Code);
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = var_get(al[0].ToString()).ToString().Replace(var_get(al[1].ToString()).ToString(), "");

            //clean
            var_arraylist_dispose(ref al);
            Line_of_Code = null;

            return;
        }

        //PASSED
        private void type_remove_at(ref string Line_of_Code)
        {
            //remat(what(*),at(*),count(*))
            ArrayList al = cut_variables(ref Line_of_Code);
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = var_get(al[0].ToString()).ToString().Remove(Convert.ToInt32(var_get(al[1].ToString())), Convert.ToInt32(var_get(al[2].ToString())));

            //clean
            var_arraylist_dispose(ref al);
            Line_of_Code = null;

            return;
        }

        //PASSED
        private void type_get_index(ref string Line_of_Code)
        {
            //indx(in(*),what(*),skip(*))

            ArrayList al = cut_variables(ref Line_of_Code);

            int occurence = 0; int ndx = 0;
            foreach (char c in var_get(al[0].ToString()).ToString())
            {
                ndx = var_get(al[0].ToString()).ToString().IndexOf(var_get(al[1].ToString()).ToString(), ndx);

                if (occurence == Convert.ToInt32(var_get(al[2].ToString())) || ndx == Do_on.types.INDEX_NOT_FOUND)
                {
                    break;
                }
                else { ndx++; }
                occurence++;
            }

            if (occurence == Convert.ToInt32(var_get(al[2].ToString())))
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = ndx;
            else
                ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = Do_on.types.INDEX_NOT_FOUND;

            //clean
            var_arraylist_dispose(ref al);
            Line_of_Code = null;

            return;
        }

        //PASSED
        private void type_get_length(ref string Line_of_Code)
        {
            //len(what(*))
            ArrayList al = cut_variables(ref Line_of_Code);

            //3((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(var_cut_symbol(al[0].ToString()))])[2] = var_get(al[0].ToString()).ToString().Length;

            Do_on.write_to_cui("What:" + al[0].ToString() + " Length:" +al[0].ToString().Length.ToString());//+ Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(al[0].ToString())].ToString());

            //clean
            var_arraylist_dispose(ref al);
            Line_of_Code = null;

            return;
        }
    }
}
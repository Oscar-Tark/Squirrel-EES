/*  <Scorpion IEE(Intelligent Execution Environment). Kernel To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2014>  <Oscar Arjun Singh Tark> <Benjamin Jack Johnson> <Rasmus Hoeberg>

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
        public void Math_(string Scorp_Line_, int index)
        {
            bool is_f = true;
            //GENERAL
            if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[5] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[34] + Do_on.AL_ACC[3].ToString()))
            {
                Add_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[35] + Do_on.AL_ACC[3].ToString()))
            {
                Subtract_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[36] + Do_on.AL_ACC[3].ToString()))
            {
                Multiply_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[37] + Do_on.AL_ACC[3].ToString()))
            {
                Divide_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[38] + Do_on.AL_ACC[3].ToString()))
            {
                Percentage_val_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[39] + Do_on.AL_ACC[3].ToString()))
            {
                Percentage_ratio_(Scorp_Line_, index);
            }
            //SIN
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[40] + Do_on.AL_ACC[3].ToString()))
            {
                Sin_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[41] + Do_on.AL_ACC[3].ToString()))
            {
                Sinh_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[42] + Do_on.AL_ACC[3].ToString()))
            {
                Sign_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[43] + Do_on.AL_ACC[3].ToString()))
            {
                ASin_(Scorp_Line_, index);
            }
            //COS
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[44] + Do_on.AL_ACC[3].ToString()))
            {
                Cos_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[45] + Do_on.AL_ACC[3].ToString()))
            {
                Cosh_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[46] + Do_on.AL_ACC[3].ToString()))
            {
                ACos_(Scorp_Line_, index);
            }
            //TAN
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[47] + Do_on.AL_ACC[3].ToString()))
            {
                Tan_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[48] + Do_on.AL_ACC[3].ToString()))
            {
                Tanh_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[49] + Do_on.AL_ACC[3].ToString()))
            {
                ATan_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[50] + Do_on.AL_ACC[3].ToString()))
            {
                ATan2_(Scorp_Line_, index);
            }
            //LOG
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[51] + Do_on.AL_ACC[3].ToString()))
            {
                NLog_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[52] + Do_on.AL_ACC[3].ToString()))
            {
                Log_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[53] + Do_on.AL_ACC[3].ToString()))
            {
                Log10_(Scorp_Line_, index);
            }
            //POW
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[54] + Do_on.AL_ACC[3].ToString()))
            {
                Pow_(Scorp_Line_, index);
            }
            //BIGMUL
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[55] + Do_on.AL_ACC[3].ToString()))
            {
                Bigmul_(Scorp_Line_, index);
            }
            //DIVREM
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[56] + Do_on.AL_ACC[3].ToString()))
            {
                Divrem32_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[57] + Do_on.AL_ACC[3].ToString()))
            {
                Divrem64_(Scorp_Line_, index);
            }
            //IEEEREMAINDER
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[58] + Do_on.AL_ACC[3].ToString()))
            {
                IEEERemainder_(Scorp_Line_, index);
            }
            //MAX
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[59] + Do_on.AL_ACC[3].ToString()))
            {
                Max_(Scorp_Line_, index);
            }
            //MIN
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[60] + Do_on.AL_ACC[3].ToString()))
            {
                Min_(Scorp_Line_, index);
            }
            //Cieling
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[61] + Do_on.AL_ACC[3].ToString()))
            {
                Cieling_(Scorp_Line_, index);
            }
            //Floor
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[62] + Do_on.AL_ACC[3].ToString()))
            {
                Floor_(Scorp_Line_, index);
            }
            //EXP
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[63] + Do_on.AL_ACC[3].ToString()))
            {
                Exp_(Scorp_Line_, index);
            }
            //ABS
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[64] + Do_on.AL_ACC[3].ToString()))
            {
                Abs_(Scorp_Line_, index);
            }
            //SQRT
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[65] + Do_on.AL_ACC[3].ToString()))
            {
                Sqrt_(Scorp_Line_, index);
            }
            //Trunc
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[66] + Do_on.AL_ACC[3].ToString()))
            {
                Trunc_(Scorp_Line_, index);
            }
            //Round
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[67] + Do_on.AL_ACC[3].ToString()))
            {
                Round_one_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[68] + Do_on.AL_ACC[3].ToString()))
            {
                Round_with_dec_(Scorp_Line_, index);
            }
            else if (Scorp_Line_.ToLower().Contains(Do_on.AL_ACC_SUP[1] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[69] + Do_on.AL_ACC[3].ToString()))
            {
                Round_with_dec_valp_(Scorp_Line_, index);
            }
            else { is_f = false; }

            //clean
            Scorp_Line_ = null;

            return;
        }

        private void Add_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf("+", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Convert.ToDouble(first_n) + Convert.ToDouble(second_n);

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Subtract_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf("-", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Convert.ToDouble(first_n) - Convert.ToDouble(second_n);

            //clean
            first_n = null;
            second_n = null;
            Scorp_Line_Exec = null;

            return;
        }

        private void Multiply_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf("*", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Convert.ToDouble(first_n) * Convert.ToDouble(second_n);

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Percentage_val_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            Do_on.AL_CURR_VAR[index] = (Convert.ToDouble(var_get(first_n)) * Convert.ToDouble(var_get(second_n))) / 100;

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Percentage_ratio_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            Do_on.AL_CURR_VAR[index] = (Convert.ToDouble(var_get(first_n)) * 100) / Convert.ToDouble(var_get(second_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Divide_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf("/", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Convert.ToDouble(first_n) / Convert.ToDouble(second_n);

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //Higher Maths
        private void Sin_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Sin(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Sinh_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Sinh(Convert.ToDouble(first_n));

            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Sign_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Sign(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void ASin_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Asin(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Cos_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Cos(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Cosh_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Cosh(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void ACos_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Acos(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //TAN-->
        private void Tan_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Tan(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Tanh_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Tanh(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void ATan_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Atan(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void ATan2_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Atan2(Convert.ToDouble(first_n), Convert.ToDouble(second_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //LOG-->
        private void NLog_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Log(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Log_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Log(Convert.ToDouble(first_n), Convert.ToDouble(second_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Log10_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            first_n = var_get(first_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Log10(Convert.ToDouble(first_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Pow-->
        private void Pow_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.Pow(Convert.ToDouble(first_n), Convert.ToDouble(second_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //Bigmul-->
        private void Bigmul_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            first_n = var_get(first_n).ToString();
            second_n = var_get(second_n).ToString();

            Do_on.AL_CURR_VAR[index] = Math.BigMul(Convert.ToInt32(first_n), Convert.ToInt32(second_n));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //Divrem-->
        private void Divrem32_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            int result;

            Do_on.AL_CURR_VAR[index] = Math.DivRem(Convert.ToInt32(var_get(first_n)), Convert.ToInt32(var_get(second_n)), out result);

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Divrem64_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            long result;

            Do_on.AL_CURR_VAR[index] = Math.DivRem(Convert.ToInt64(var_get(first_n)), Convert.ToInt64(var_get(second_n)), out result);

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //IEEERemainder-->
        private void IEEERemainder_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            Do_on.AL_CURR_VAR[index] = Math.IEEERemainder(Convert.ToDouble(var_get(first_n)), Convert.ToDouble(var_get(second_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //Max-->
        private void Max_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            Do_on.AL_CURR_VAR[index] = Math.Max(Convert.ToDouble(var_get(first_n)), Convert.ToDouble(var_get(second_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //Min-->
        private void Min_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            Do_on.AL_CURR_VAR[index] = Math.Min(Convert.ToDouble(var_get(first_n)), Convert.ToDouble(var_get(second_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        //Cieling-->
        private void Cieling_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Ceiling(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Floor-->
        private void Floor_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Floor(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Exp-->
        private void Exp_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Exp(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Abs-->
        private void Abs_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Abs(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Sqrt-->
        private void Sqrt_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Sqrt(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Trunc-->
        private void Trunc_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Truncate(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        //Round-->
        private void Round_one_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            Do_on.AL_CURR_VAR[index] = Math.Round(Convert.ToDouble(var_get(first_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;

            return;
        }

        private void Round_with_dec_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);
            ndx = Scorp_Line_Exec.IndexOf(")", ndx2);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            Do_on.AL_CURR_VAR[index] = Math.Round(Convert.ToDouble(var_get(first_n)), Convert.ToInt32(var_get(second_n)));

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;

            return;
        }

        private void Round_with_dec_valp_(string Scorp_Line_Exec, int index)
        {
            int ndx = Scorp_Line_Exec.IndexOf("(", 0);
            int ndx2 = Scorp_Line_Exec.IndexOf(",", ndx);

            string first_n = Scorp_Line_Exec.Remove(ndx2);
            first_n = first_n.Remove(0, ndx + 1);

            ndx = Scorp_Line_Exec.IndexOf(",", ndx2 + 1);

            string second_n = Scorp_Line_Exec.Remove(ndx);
            second_n = second_n.Remove(0, ndx2 + 1);

            ndx2 = Scorp_Line_Exec.IndexOf(")", ndx);

            string third_n = Scorp_Line_Exec.Remove(ndx2);
            third_n = third_n.Remove(0, ndx + 1);

            MidpointRounding m;
            if (third_n.ToLower() == "awayfromzero")
            {
                m = MidpointRounding.AwayFromZero;
            }
            else { m = MidpointRounding.ToEven; }

            Do_on.AL_CURR_VAR[index] = Math.Round(Convert.ToDouble(var_get(first_n)), Convert.ToInt32(var_get(second_n)), m);

            //clean
            Scorp_Line_Exec = null;
            first_n = null;
            second_n = null;
            third_n = null;

            return;
        }
    }
}
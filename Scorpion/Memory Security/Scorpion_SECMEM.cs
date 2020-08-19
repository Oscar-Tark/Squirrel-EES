using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;

namespace Scorpion.Memory_Security
{
    public class REGEX
    {
        public bool regex_match(ref string string_)
        {
            bool res = false;
            MatchCollection mc;
            string[] s_chars =
            {
                //STARTING SCRIPT
                /*"[<]{1}[s]{1}[c]{1}[r]{1}[i]{1}[p]{1}[t]{1}[>]{1}",
                "(<|>)[s]{1}[c]{1}[r]{1}[i]{1}[p]{1}[t]{1}(<|>)",
                //ENDING SCRIPT
                "[<]{1}[/]{1}[s]{1}[c]{1}[r]{1}[i]{1}[p]{1}[t]{1}[>]{1}",
                "(<|/>)[s]{1}[c]{1}[r]{1}[i]{1}[p]{1}[t]{1}(<|/>)",*/
                //AVOID HEX PAYLOADS
                @"[\]{1}[x]{1}[a-fA-F0-9]{1,2}"
            };

            foreach (string s in s_chars)
            {
                Regex r = new Regex(@"^"+s+"$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                mc = r.Matches(string_.Replace(" ", ""));
                if (mc.Count > 0)
                    res = true;
                r = null;
            }

            mc = null;

            return res;
        }
    }

    public class Sanitizer
    {
        Form1 Do_on;
        public Sanitizer(Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public string sanitize(ref string string_)
        {
            if (check_len(ref string_))
                return sanitize_string(ref string_);
            else
                return "\0";
        }

        private string sanitize_string(ref string string_)
        {
            //MATCHES UNSAFE PATTERNS, IF FOUND RETURNS A NULL TERMINATION CHARACTER
            REGEX rg = new REGEX();
            if (rg.regex_match(ref string_))
            {
                rg = null;
                return "\0";
            }
            else
            {
                rg = null;
                return string_;
            }
        }

        private bool check_len(ref string string_)
        {
            if(string_.Length > Do_on.readr.lib_SCR.get_limit())
                return false;
            return true;
        }
    }

    public class Secure_Memory
    {
        Form1 Do_on;
        public Secure_Memory(Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        private string password = null;
        private byte[] pin_cde_hx = new byte[32];
        private byte[] pin_iv = new byte[16];
        private byte pin_cde = 0x00;

        public byte get_pin()
        {
            return pin_cde;
        }

        //make private
        public void set_pass(ref string pass, ref byte[] pin)
        {
            bool success = true;
            password = pass;
            //CREATE MAXED OUT PIN CODE OD ONE BYTE TO USE AS A SEED
            do
            {
                try
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        pin_cde_hx[i] = pin[i];
                        pin_cde += pin_cde_hx[i];
                    }

                    //SCRAMBLE
                    Random rnd = new Random();
                    int seed = rnd.Next(1, pin_cde);
                    for (short i = 0; i <= 31; i++)
                    {
                        if (i > 0)
                            pin_cde_hx[i] = (byte)((pin_cde_hx[i - 1] / Convert.ToByte(password[rnd.Next(0, 4)]) + Convert.ToByte(rnd.Next(0, 12))) * (seed / 10));
                        else
                            pin_cde_hx[i] = (byte)(pin_cde + Convert.ToByte(rnd.Next(0, 4)));

                        if (i < 16)
                            pin_iv[i] = (byte)(((pin_cde_hx[i] + Convert.ToByte(i)) / Convert.ToByte(password[0])) + rnd.Next(0, 12));
                    }

                    Do_on.crypto.AES_KEY(pin_cde_hx, pin_iv);
                    Do_on.write_to_cui("\n\n*************************************************************************************************************************\n[Notice] Cryptographic key generated: You may use the 'encrypt' and 'decrypt' functions in order to safeguard variables.\nSaving variables to a database will require you to use the same Passcode and pin in order to decrypt them\n*************************************************************************************************************************");
                    success = true;
                }
                catch { Do_on.write_to_cui("\n\n***********************************************\nArithmetic error: regenerating crypto key\n***********************************************"); }
            }
            while (success == false);
            return;
        }

        public void encrypt(ref string Reference)
        {
            //::*ref, var
            byte[] b_e = Do_on.crypto.AES_ENCRYPT((string)Reference, (string)Do_on.readr.lib_SCR.var_get(ref Reference), pin_cde_hx, pin_iv);
            var_set_encrypted(Reference, b_e);
            return;
        }

        public void decrypt(ref string Reference)
        {
            //::*ref
            string s_d = Do_on.crypto.AES_DECRYPT(Reference, var_get_encrypted(ref Reference), pin_cde_hx, pin_iv);
            Do_on.readr.lib_SCR.varset("", new ArrayList() { Reference, s_d});
            return;
        }

        private void var_set_encrypted(string Reference, byte[] block_)
        {
            ((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(Reference)])[2] = block_;
            return;
        }

        private byte[] var_get_encrypted(ref string Reference)
        {
            return (byte[])((ArrayList)Do_on.AL_CURR_VAR[Do_on.AL_CURR_VAR_REF.IndexOf(Reference)])[2]; ;
        }
    }
}

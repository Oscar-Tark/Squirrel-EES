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

        /*public void Encrypt_Block(ref string Scorp_Line)
        {
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line);
            byte[] tmpbyte;

            foreach (string s in al)
            {
                Do_on.write_to_cui("Encrypting Variable: '" + s + "'");
                tmpbyte = Do_on.crypto.encrypt(Do_on.readr.lib_SCR.var_get(s), "anus");
                Do_on.readr.lib_SCR.var_set_encrypted(s, tmpbyte);
            }

            tmpbyte = null;
            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }

        public void Decrypt_Block(ref string Scorp_Line)
        {
            ArrayList al = Do_on.readr.lib_SCR.cut_variables(Scorp_Line);
            byte[] tmpbyte;

            foreach (string s in al)
            {
                Do_on.write_to_cui("Decrypting Variable: '" + s + "'");
                tmpbyte = Do_on.crypto.decrypt((byte[])Do_on.readr.lib_SCR.var_get(s), "anus");
                //Do_on.readr.lib_SCR.var_set_decrypted(s, Do_on.crypto.To_Object(new System.IO.MemoryStream(tmpbyte)));
            }

            tmpbyte = null;
            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }*/
        private string password = null;
        private byte[] pin_cde_hx = new byte[32];
        private byte[] pin_iv = new byte[16];
        private byte pin_cde = 0x00;

        //make private
        public void set_pass(ref string pass, ref byte[] pin)
        {
            password = pass;
            //CREATE MAXED OUT PIN CODE OD ONE BYTE TO USE AS A SEED
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
            //try
            //{
            string s_d = Do_on.crypto.AES_DECRYPT(Reference, var_get_encrypted(ref Reference), pin_cde_hx, pin_iv);
            Do_on.readr.lib_SCR.varset("", new ArrayList() { Reference, s_d});
            //}
            //catch(Exception e) { Console.WriteLine(e.Message); }
            return;
        }

        /*public void secure(ref string reference)
        {
            //GETVAR
            string block = (string)Do_on.readr.lib_SCR.var_get(ref reference);

            //TO_BYTE
            byte[] b_raw = Do_on.crypto.To_Byte(block);
            //REVERSE
            byte[] b_raw_rev = b_raw.Reverse().ToArray();
            //AES
            byte[] b_raw_AES = Do_on.crypto.encrypt_noconvert(b_raw_rev, password);
            //DEBUG
            Console.WriteLine("{0:X}", b_raw_AES[0]);
            //SET
            var_set_encrypted(reference, b_raw_AES);
            //CLEAN
            reference = null;
            return;
        }

        public void revsecure(ref string reference)
        {
            //GETVAR
            try
            {
                //GET BLOCK
                byte[] block_ = var_get_encrypted(reference);
                Console.WriteLine("{0:X}", block_[0]);
                //DECRYPT AES
                byte[] AES = Do_on.crypto.decrypt(block_, password);
                //REVERSE
                byte[] b_raw_corrected = AES.Reverse().ToArray();
                //DEBUG
                Console.WriteLine("{0:X}", block_);
                //TO OBJ
                object b_obj = Do_on.crypto.To_Object(b_raw_corrected);
                //SET
                Do_on.readr.lib_SCR.varset("", new ArrayList() { reference, b_obj });
            }
            catch(Exception ery) { Console.WriteLine(ery.Message + " | " + ery.StackTrace); }
            reference = null;
            return;
        }*/

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

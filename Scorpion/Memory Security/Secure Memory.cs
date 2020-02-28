using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections;

namespace Scorpion.Memory_Security
{
    public class Sanitizer
    {
        public bool sanitize(ref string string_)
        {
            return check_len(ref string_);
        }

        private string sanitize_string(ref string string_)
        { 
            //USEREGEX
            ArrayList san = new ArrayList { "\\x", "script" };
            foreach (string s in san)
                string_ = string_.Replace(s, "");
            return string_;
        }

        private bool check_len(ref string string_)
        {
            if(string_.Length > 25)
                return false;
            return true;
        }
    }

    public class Secure_Memory
    {
        Scorpion.Form1 Do_on;
        public Secure_Memory(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void Encrypt_Block(ref string Scorp_Line)
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
                Do_on.readr.lib_SCR.var_set_decrypted(s, Do_on.crypto.To_Object(new System.IO.MemoryStream(tmpbyte)));
            }

            tmpbyte = null;
            Do_on.readr.lib_SCR.var_arraylist_dispose(ref al);
            return;
        }


    }
}

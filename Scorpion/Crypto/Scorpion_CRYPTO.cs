using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scorpion.Crypto
{
    public partial class Cryptographer
    {
        private static Aes aes = Aes.Create();
        private static ICryptoTransform ics;
        private static ICryptoTransform icd;

        //CREATE AES PARAMETERS - DO ONLY ONCE!
        public void AES_KEY(byte[] pin, byte[] pin_iv)
        {
            aes.Key = pin;
            aes.IV = pin_iv;
            aes.Mode = CipherMode.CFB;
            aes.Padding = PaddingMode.PKCS7;

            ics = aes.CreateEncryptor(pin, pin_iv);
            icd = aes.CreateDecryptor(pin, pin_iv);
            return;
        }

        public byte[] AES_ENCRYPT(string Reference, string block_, byte[] pin, byte[] iv)
        {
            byte[] b_encrypted = null;
            b_encrypted = Encrypt(block_, pin, iv);
            return b_encrypted;
        }

        public string AES_DECRYPT(string Reference, byte[] block_, byte[] pin, byte[] iv)
        {
            string s_decrypted = null;
            s_decrypted = Decrypt(block_, pin);
            return s_decrypted;
        }

        private static byte[] Encrypt(string block_, byte[] pin, byte[] IV)
        {
            byte[] encrypted = null;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ics, CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(block_);
            sw.Flush();
            sw.Close();
            encrypted = ms.ToArray();
            cs.Flush();
            cs.Close();
            ms.Flush();
            ms.Close();
            return encrypted;
        }

        private static string Decrypt(byte[] block_, byte[] pin)
        {
            string decrypted = null;
            MemoryStream ms = new MemoryStream(block_);
            CryptoStream cs = new CryptoStream(ms, icd, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            decrypted = sr.ReadToEnd();
            sr.Close();
            cs.Flush();
            cs.Close();
            ms.Flush();
            ms.Close();
            return decrypted;
        }
    }

    public partial class Cryptographer
    {
        Scorpion.Form1 Do_on;
        /*Objects: These objects must be contained only in this file, any replications in other files can give away any encryption data*/

        public Cryptographer(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
        }

        public byte[] To_Byte(object obj)
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray();
        }

        public object To_Object(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            return new BinaryFormatter().Deserialize(ms);
        }

        public string SHA(string pass)
        {
            byte[] to_convert = Encoding.UTF8.GetBytes(pass);
            to_convert = SHA256.Create().ComputeHash(to_convert);
            return Convert.ToBase64String(to_convert);
        }
    }
}

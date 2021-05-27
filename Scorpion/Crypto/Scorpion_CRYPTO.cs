using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Runtime.InteropServices;

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

        public byte[] AES_SECURE_ENCRYPT(SecureString SS, byte[] block_)
        {
            //Encrypts using a secure string as the key source
            return Secure_Encrypt(SS, block_);
        }

        private static byte[] Secure_Encrypt(SecureString SS, byte[] block_)
        {
            byte[] encrypted_block = null;
            IntPtr marshalled_pass = Marshal.SecureStringToGlobalAllocUnicode(SS);
            unsafe
            {

            }
            Marshal.ZeroFreeGlobalAllocUnicode(marshalled_pass);

            return encrypted_block;
        }

        private static byte[] Encrypt(string block_, byte[] pin, byte[] IV)
        {
            byte[] encrypted = null;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ics, CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            
            sw.Write(Encoder.encodebase64string(block_));
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
            return Encoder.decodebase64string(decrypted);
        }
    }

    public partial class Cryptographer
    {
        Scorp Do_on;
        /*Objects: These objects must be contained only in this file, any replications in other files can give away any encryption data*/

        public Cryptographer(Scorp fm1)
        {
            Do_on = fm1;
        }

        public Cryptographer()
        {
        }

        public byte[] To_Byte(object obj)
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, obj);
            return ms.ToArray();
        }

        public string To_String(byte[] byt)
        {
            return Encoding.Default.GetString(byt);
        }

        public object To_Object(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            return new BinaryFormatter().Deserialize(ms);
        }

        public SecureString Create_Seed()
        {
            SecureString s_seed = new SecureString();
            Random rnd = new Random(DateTimeOffset.Now.Millisecond);
            for (int i = 0; i <= 10; i++)
                s_seed.AppendChar(Convert.ToChar(rnd.Next(21, 126)));
            return s_seed;
        }

        public static string SHA(string pass)
        {
            byte[] to_convert = Encoding.UTF8.GetBytes(pass);
            to_convert = SHA256.Create().ComputeHash(to_convert);
            return Convert.ToBase64String(to_convert);
        }

        public string SHA_SS(SecureString pass)
        {
            //Create SHA
            byte[] to_convert = new byte[10];
            IntPtr marshalled_pass = Marshal.SecureStringToGlobalAllocUnicode(pass);
            unsafe
            {
                //Generate SHA seed from seed
                byte* byteArray = (byte*)marshalled_pass.ToPointer();
                int ndx = 0;
                while(*byteArray != 0x00)
                {
                    to_convert[ndx] = *byteArray;
                    byteArray++;
                    ndx++;
                }
            }
            Marshal.ZeroFreeGlobalAllocUnicode(marshalled_pass);

            to_convert = SHA256.Create().ComputeHash(to_convert);
            return Convert.ToBase64String(to_convert);
        }
    }

    public static class Encoder
    {
        public static string encodebase64string(string to_encode)
        {
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(to_encode));
        }

        public static string decodebase64string(string to_decode)
        {
            return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(to_decode));
        }
    }
}

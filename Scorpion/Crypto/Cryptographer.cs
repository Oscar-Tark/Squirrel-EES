using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;

namespace Scorpion.Crypto
{
    public class Cryptographer
    {
        Scorpion.Form1 Do_on;

        /*Objects: These objects must be contained only in this file, any replications in other files can give away any encryption data*/

        public Cryptographer(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
        }

        public byte[] To_Byte(object To_Encrypt)
        {
            MemoryStream ms = new MemoryStream();
            new BinaryFormatter().Serialize(ms, To_Encrypt);
            return ms.ToArray();
        }

        public object To_Object(MemoryStream To_Convert)
        {
            return new BinaryFormatter().Deserialize(To_Convert);
        }

        /*private byte[] Set_Salt(ref string Scorp_Line_Exec)
        {
            /*(*var*var*var*var*var*var*var*var)
        }*/

        public string SHA(string pass)
        {
            byte[] to_convert = Encoding.UTF8.GetBytes(pass);
            to_convert = SHA256.Create().ComputeHash(to_convert);

            return Convert.ToBase64String(to_convert);
        }

        public byte[] encrypt(object To_Encrypt, string pass)
        {
            byte[] to_convert = To_Byte(To_Encrypt);
            byte[] b_pass = Convert.FromBase64String(pass);
            b_pass = SHA256.Create().ComputeHash(b_pass);

            byte[] saltbytes = getrandombytes();

            byte[] To_Convert = new byte[saltbytes.Length + to_convert.Length];
            for(int i = 0; i < saltbytes.Length; i++)
            {
                To_Convert[i] = saltbytes[i];
            }

            for(int i = 0; i<to_convert.Length; i++)
            {
                To_Convert[i + saltbytes.Length] = to_convert[i];
            }

            return encrypt_object(b_pass, To_Convert);
        }

        public byte[] decrypt(byte[] To_Decrypt, string pass)
        {
            byte[] b_pass = Convert.FromBase64String(pass);
            b_pass = SHA256.Create().ComputeHash(b_pass);

            byte[] decrypted = decrypt_object(b_pass, To_Decrypt);

            int salt_size = 4;

            byte[] orig = new byte[decrypted.Length - salt_size];
            for(int i = salt_size; i< decrypted.Length; i++)
            {
                decrypted[i - salt_size] = decrypted[i];
            }

            return decrypted;
        }

        private byte[] encrypt_object(byte[] pass, byte[] To_Encrypt)
        {
            byte[] encrypted_bytes = null;
            byte[] to_convert = To_Encrypt;
            byte[] saltbytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            //byte[] b_pass = Convert.FromBase64String(pass);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged rj = new RijndaelManaged())
                {
                    rj.Padding = PaddingMode.Zeros;
                    rj.KeySize = 256;
                    rj.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(pass, saltbytes, 1000);
                    rj.Key = key.GetBytes(rj.KeySize / 8);
                    rj.IV = key.GetBytes(rj.BlockSize / 8);

                    rj.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, rj.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(to_convert, 0, to_convert.Length);
                        cs.Close();
                    }
                    encrypted_bytes = ms.ToArray();
                }
            }
            return encrypted_bytes;
        }

        private byte[] decrypt_object(byte[] b_pass, byte[] To_Decrypt)
        {
            byte[] decrypted_bytes = null;
            byte[] to_convert = To_Decrypt;
            byte[] saltbytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            //byte[] b_pass = Convert.FromBase64String(pass);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged rj = new RijndaelManaged())
                {
                    rj.Padding = PaddingMode.Zeros;
                    rj.KeySize = 256;
                    rj.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(b_pass, saltbytes, 1000);
                    rj.Key = key.GetBytes(rj.KeySize / 8);
                    rj.IV = key.GetBytes(rj.BlockSize / 8);

                    rj.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, rj.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(to_convert, 0, to_convert.Length);
                        cs.Close();
                    }
                    decrypted_bytes = ms.ToArray();
                }
            }
            return decrypted_bytes;
        }

        private byte[] getrandombytes()
        {
            int salt_size = 4;
            byte[] boo = new byte[salt_size];
            RNGCryptoServiceProvider.Create().GetBytes(boo);

            return boo;
        }
    }
}

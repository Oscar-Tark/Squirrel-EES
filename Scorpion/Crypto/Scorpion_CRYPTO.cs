using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Runtime.InteropServices;
using System.Collections;

namespace Scorpion.Crypto
{
    public partial class Cryptographer
    {
        public byte[] AES_ENCRYPT(string Reference, object block_, byte[] pin)
        {
            byte[] b_encrypted = Cauldron.Cryptography.Aes.Encrypt(Do_on.mmsec.get_pwd(), To_Byte(block_));            
            return b_encrypted;
        }

        public object AES_DECRYPT(string Reference, byte[] block_, byte[] pin)
        {
            return To_Object(Cauldron.Cryptography.Aes.Decrypt(Do_on.mmsec.get_pwd(), block_));
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

        public string Array_To_String(System.Collections.ArrayList ar)
        {
            StringBuilder sb = new StringBuilder();
            System.Xml.XmlWriterSettings st = new System.Xml.XmlWriterSettings();
            st.OmitXmlDeclaration = true;
            st.Indent = false;
            System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(sb, st);
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(ar.GetType());
            s.Serialize(w, ar);
            w.Close();
            return sb.ToString();
        }

        public ArrayList String_To_Array(string str)
        {
            return null;
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

/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

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
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Runtime.InteropServices;
using System.Collections;
using System.Xml.Serialization;

namespace Scorpion.Crypto
{
    public partial class Cryptographer
    {
        public byte[] AES_ENCRYPT(SecureString internal_user_password, object block)
        {
            return Cauldron.Cryptography.Aes.Encrypt(internal_user_password, To_Byte(block)); 
        }

        public object AES_DECRYPT(SecureString internal_user_password, byte[] block)
        {
            return To_Object(Cauldron.Cryptography.Aes.Decrypt(internal_user_password, block));
        }
    }

    public partial class Cryptographer
    {
        /*Objects: These objects must be contained only in this file, any replications in other files can give away any encryption data*/
        public string Array_To_String(ArrayList obj)
        {
            StringBuilder sb = new StringBuilder();
            System.Xml.XmlWriterSettings st = new System.Xml.XmlWriterSettings();
            st.OmitXmlDeclaration = true;
            st.Indent = false;
            System.Xml.XmlWriter w = System.Xml.XmlWriter.Create(sb, st);
            XmlSerializer s = new XmlSerializer(obj.GetType());
            s.Serialize(w, obj);
            w.Close();
            return sb.ToString();
        }

        public ArrayList String_To_Array(string obj)
        {
            XmlSerializer xml = new XmlSerializer(typeof(ArrayList));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(obj));
            ArrayList al_ret = (ArrayList)xml.Deserialize(ms);
            ms.Flush();
            ms.Close();
            return al_ret;
        }

        [Obsolete]
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

        [Obsolete]
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

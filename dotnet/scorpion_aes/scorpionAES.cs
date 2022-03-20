using System;
using System.Text;
using EasyEncrypt2;

namespace ScorpionAES
{
    public static class ScorpionAES
    {
        public static void encryptFile(string path, string destination_path)
        {
            var encrypter = new EasyEncrypt();
            encrypter.EncryptFile(path, destination_path);
            return;
        }

        public static void decryptFile(string path, string destination_path)
        {
            var encrypter = new EasyEncrypt();
            encrypter.DecryptFile(path, destination_path);
            return;
        }

        public static byte[] encryptData(string contents, string pwd)
        {
            using var encrypterWithPassword = new EasyEncrypt(pwd, "Salty09820");
            return encrypterWithPassword.Encrypt(Encoding.UTF8.GetBytes(contents));
        }

        public static string decryptData(byte[] contents, string pwd)
        {
            using var encrypterWithPassword = new EasyEncrypt(pwd, "Salty09820");
            var decryptedArray = encrypterWithPassword.Decrypt(contents);
            return Encoding.UTF8.GetString(decryptedArray, 0, decryptedArray.Length);
        }
    }
}

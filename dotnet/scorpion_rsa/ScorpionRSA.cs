using System;
using System.Security;
using System.IO;
using Cauldron.Cryptography;
using System.Security.Cryptography;
using System.Diagnostics;

//A library to load and create RSA keys
namespace Scorpion_RSA
{
    public static class ScorpionRSAMin
    {
        public static byte[] decrypt(byte[] data, string path)
        {
            //return Rsa.Decrypt(private_key_path, data);
            using(var rsa = RSAOpenSsl.Create())
            {
              rsa.ImportFromPem(File.ReadAllText(path));
              return rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
            }
        }

        public static byte[] encrypt(byte[] data, string path)
        {
            using(var rsa = RSAOpenSsl.Create())
            {
              rsa.ImportFromPem(File.ReadAllText(path));
              return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
            }
        }
    }

    //BELOW ALL DEPRECIATED
    public static class Scorpion_RSA
    {
        //[Obsolete]
        public static void generateRSAkeys(string public_key_file, string private_key_file)
        {
            Console.WriteLine("Generating keys (This might take a while)...");
            try
            {
                KeyPair kp = create_keypair();
                save_publickey_tofile(kp.PublicKey, public_key_file);
                save_privatekey_tofile(kp.PrivateKey, private_key_file);
                Console.WriteLine("Private and public keys generated and saved (Never give you're private key to anyone)");
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to generate keys due to: {0}", e.Message);
            }
            return;
        }

        public static byte[] decrypt_data(byte[] data, SecureString private_key)
        {
            return decrypt(private_key, data);
        }

        public static byte[] encrypt_data(string data, string public_key_path)
        {
            return encrypt(public_key_path, data);
        }

        public static string get_public_key_file(string public_key_file)
        {
            return get_publickey_fromfile(ref public_key_file);
        }

        public static SecureString get_private_key_file(string private_key_file)
        {
            Console.WriteLine("[Warning]: It is not recommended to load private keys to non secured memory");
            return get_privatekey_fromfile(ref private_key_file);
        }

        private static KeyPair create_keypair()
        {
            return Rsa.CreateKeyPair(RSAKeySizes.Size2048);
        }

        private static void save_publickey_tofile(string key, string file)
        {
            RSA_FILE_READER.write_publickey_file(ref file, key);
            return;
        }

        private static void save_privatekey_tofile(SecureString key, string file)
        {
            RSA_FILE_READER.write_privatekey_file(ref file, key);
            return;
        }

        private static string get_publickey_fromfile(ref string file)
        {
            return RSA_FILE_READER.read_publickey_file(ref file);
        }

        private static SecureString get_privatekey_fromfile(ref string file)
        {
            return RSA_FILE_READER.read_privatekey_file(ref file);
        }

        private static byte[] decrypt(SecureString private_key, byte[] data)
        {
            return Rsa.Decrypt(private_key, data);
        }

        private static byte[] encrypt(string path, string data)
        {
            return Rsa.Encrypt(RSA_FILE_READER.read_publickey_file(ref path), data);
        }
    }

    static class RSA_FILE_READER
    {
        public static string read_publickey_file(ref string path)
        {
            return File.ReadAllText(path);
        }

        public static SecureString read_privatekey_file(ref string path)
        {
            SecureString ss = Cauldron.ExtensionsCryptography.ToSecureString(File.ReadAllText(path));
            return ss;
        }

        public static void write_publickey_file(ref string path, string key)
        {
            //Write as text not bytes
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(key);
            string s_b64 = Convert.ToBase64String(plainTextBytes);
            Console.WriteLine("Writing public key");
            File.WriteAllText(path, s_b64, System.Text.Encoding.UTF8);
        }

        public static void write_privatekey_file(ref string path, SecureString key)
        {
            //Write as text not bytes
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(Cauldron.ExtensionsSecureString.GetString(key));
            string s_b64 = Convert.ToBase64String(plainTextBytes);
            Console.WriteLine("Writing private key");
            File.WriteAllText(path, s_b64, System.Text.Encoding.UTF8);
        }
    }
}
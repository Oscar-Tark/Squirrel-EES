/*
2020+ <Oscar Arjun Singh Tark>
Based of the work of Christian Gollhardt(https://stackoverflow.com/users/2441442/christian-gollhardt) & csharptest.net(https://stackoverflow.com/users/164392/csharptest-net)
https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
http://csharptest.net/470/another-example-of-how-to-store-a-salted-password-hash/
*/

using System.Security;

namespace Scorpion_Hasher_Library
{
    public class Scorpion_Hasher
    {
        public string Bhash(SecureString password)
        {
            return SecurePasswordHasher.BHash(password);
        }

        public bool Bverify(SecureString password, string hashed_password)
        {
            return SecurePasswordHasher.Bverify(password, hashed_password);
        }
    }

    static class SecurePasswordHasher
    {
        public static string BHash(SecureString password)
        {
           return BCrypt.Net.BCrypt.HashPassword(Cauldron.ExtensionsSecureString.GetString(password));
        }

        public static bool Bverify(SecureString password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(Cauldron.ExtensionsSecureString.GetString(password), hash);
        }

        private static SecureString create_secure_string(string element)
        {
            SecureString sec = new SecureString();
            foreach (char c_ in element)
                sec.AppendChar(c_);
            return sec;
        }
    }
}
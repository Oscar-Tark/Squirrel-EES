
using System.Collections;

namespace Scorpion
{
    partial class Librarian
    {
        public void generatersakeys(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Please run the following script using scriptrun for a Private key: ", "processdefine::*'openssl', *f'genrsa -out {((path))}/RSA/private-key.pem 2048', *'genrsapriv', *false, *false >> processrun::*'genrsapriv'");
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("\v", "Please run the following script using scriptrun for a Public key: ", "processdefine::*'openssl', *f'rsa -in {((path))}/RSA/private-key.pem -pubout -out {((path))}/RSA/public-key.pem', *'genrsapub', *false, *false >> processrun::*'genrsapriv'");
            return;
        }
    }
}
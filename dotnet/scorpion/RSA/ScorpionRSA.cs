using System.Collections;
using System.Security;

namespace Scorpion
{
    public partial class Librarian
    {
        //Private key always on reciever software, public keys for senders
        public void rsakeys(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*publickeyfile, *privatekeyfile
            ScorpionConsoleReadWrite.ConsoleWrite.writeExperimental("Generating RSA...");
            Scorpion_RSA.Scorpion_RSA.generateRSAkeys((string)var_get(objects[0]), (string)var_get(objects[1]));
            return;
        }

        //Unavailable due to security concerns, use file path instead
        /*public string rsapublickey(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable::*key_file
            return var_create_return(Scorpion_RSA.Scorpion_RSA.get_public_key_file((string)var_get(objects[0])), true);
        }

        //Unavailable due to security concerns, use file path instead
        public object rsaprivatekey(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*returnable::*key_file
            SecureString s_ = Scorpion_RSA.Scorpion_RSA.get_private_key_file((string)var_get(objects[0]));
            return var_create_return(ref s_, true);
        }*/
    }
}

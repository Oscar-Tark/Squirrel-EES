using System.Collections;
using Scorpion_Authenticator;

namespace Scorpion
{
    public partial class Librarian
    {
        //USE SECURE STRING FOR PWD
        public void deleteuser(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Authenticator scauth = new Authenticator();
            string usr = (string)var_get(objects[0]);
            string pwd = (string)var_get(objects[1]);
            scauth.delete_user(ref usr, ref pwd);

            var_dispose_internal(ref Scorp_Line_Exec);
            var_dispose_internal(ref usr);
            var_dispose_internal(ref pwd);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void resetuser()
        {


        }
    }
}
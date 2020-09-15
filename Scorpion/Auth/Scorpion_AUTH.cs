using System;


namespace Scorpion.Auth
{
    public class Scorpion_AUTH
    {
        Form1 Do_on;
        const string collection = "users";
        public Scorpion_AUTH(Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void create_user()
        {

        }

        public void delete_user()
        {

        }

        public void check_user_pwd(ref string uname, ref string passcode, ref byte[] pin)
        {
            string filter = uname;
            Do_on.mdb.get(ref filter, collection);
            return;
        }

        public void passwd()
        {

        }

        public bool check_db()
        {

            return true;
        }
    }

    class Scorpion_USER
    {

    }
}

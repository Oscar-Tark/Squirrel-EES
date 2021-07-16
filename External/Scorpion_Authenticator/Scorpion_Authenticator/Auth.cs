using System.IO;
using System.Security.Principal;
using Scorpion_Hasher_Library;
using System;

namespace Scorpion_Authenticator
{
    public class Authenticator
    {
        string System_user;
        string full_path;
        string full_directory_path;
        string[] Config_file_content;

        const string scorpion_directory = ".scorpion";
        const string scorpion_config = "scorpion_users.conf";

        public Authenticator()
        {
            check();
            return;
        }

        public bool authenticate(ref string User, ref string Passcode)
        {
            Scorpion_Hasher sch = new Scorpion_Hasher();
            string[] elements;
            string[] sep = { "@@@~" };
            if (Config_file_content.Length == 0 || Config_file_content[0] == "")
                return false;

            foreach(string line in Config_file_content)
            {
                elements = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                if (elements[0] == User)
                    return sch.verify(Passcode, elements[1]);
            }
            return false;
        }

        public void create_user()
        {
            Console.WriteLine("New username:");
            string uname = Console.ReadLine();
            Console.WriteLine("Passcode:");
            string pwd = Console.ReadLine();
            write_config(ref uname, ref pwd);

            ExecutionPersmissions ep = new ExecutionPersmissions(ref uname);
            ep.create(ref uname);
            return;
        }

        public void delete_user(ref string user, ref string pwd)
        {
            Console.WriteLine("Deleting user...");
            if (authenticate(ref user, ref pwd))
                delete_from_config(ref user);
            return;
        }

        private void get_system_user()
        {
            System_user = WindowsIdentity.GetCurrent().Name;
            return;
        }

        private void create_path()
        {
            full_path = "/home/" + System_user + "/" + scorpion_directory + "/" + scorpion_config;
            full_directory_path = "/home/" + System_user + "/" + scorpion_directory;
            return;
        }

        public void check()
        {
            get_system_user();
            create_path();
            check_directory();
            check_configuration();
            read_config();
            return;
        }

        private void check_directory()
        {
            if (!Directory.Exists(full_directory_path))
                Directory.CreateDirectory(full_directory_path);
            return;
        }

        private void check_configuration()
        {
            if (!File.Exists(full_path))
                File.Create(full_path).Close();
            return;
        }

        private void read_config()
        {
            Config_file_content = File.ReadAllLines(full_path, System.Text.Encoding.UTF8);
            return;
        }

        private void write_config(ref string uname, ref string pwd)
        {
            Scorpion_Hasher sch = new Scorpion_Hasher();
            StreamWriter sr = File.AppendText(full_path);
            Console.WriteLine("Creating user...");
            sr.WriteLine("@@@~" + uname + "@@@~" + sch.hash(pwd) + "@@@~");
            sr.Flush();
            sr.Close();
            return;
        }

        private void delete_from_config(ref string uname)
        {
            read_config(); int ndx = 0;
            foreach(string s_accnt in Config_file_content)
            {
                if (s_accnt.StartsWith("@@@~" + uname + "@@@~", StringComparison.CurrentCulture))
                {
                    Config_file_content.SetValue("", ndx);
                    File.WriteAllLines(full_path, Config_file_content);
                    break;
                }
                ndx++;
            }
            return;
        }
    }

    public class ExecutionPersmissions
    {
        string[] Config_file_content;
        string System_user, full_path, full_directory_path;
        protected string user = null;
        const string scorpion_directory = ".scorpion";
        const string scorpion_config = "_scorpion_users_permissions.conf";

        public ExecutionPersmissions(ref string user_)
        {
            user = user_;
            check();
            return;
        }

        private void get_system_user()
        {
            System_user = WindowsIdentity.GetCurrent().Name;
            return;
        }

        private void create_path()
        {
            full_path = "/home/" + System_user + "/" + scorpion_directory + "/" + user + scorpion_config;
            full_directory_path = "/home/" + System_user + "/" + scorpion_directory;
            return;
        }

        public void check()
        {
            get_system_user();
            create_path();
            check_directory();
            check_configuration();
            read_config();
            return;
        }

        private void check_directory()
        {
            if (!Directory.Exists(full_directory_path))
                Directory.CreateDirectory(full_directory_path);
            return;
        }

        private void check_configuration()
        {
            if (!File.Exists(full_path))
                File.Create(full_path).Close();
            return;
        }

        private void read_config()
        {
            Config_file_content = File.ReadAllLines(full_path, System.Text.Encoding.UTF8);
            return;
        }

        public void create(ref string user_)
        {
            write_permissions();
            return;
        }

        public bool check_authentication(ref string user_, ref string function)
        {
            user = user_;
            return authenticate(ref function);
        }

        private void write_permissions()
        {
            read_config();
            Scorpion_Hasher sch = new Scorpion_Hasher();
            StreamWriter sr = File.AppendText(full_path);
            //Hash is null if no permission, Hash is same name as function if exists
            Console.WriteLine("Grant all privileges to user? (You may change permissions later on during the login process) [Y/N]");
            string s_ans = Console.ReadLine();
            Console.WriteLine("Creating permissions...");
            if (s_ans == "y")
                sr.WriteLine("ALL");
            sr.Flush();
            sr.Close();
            return;
        }

        private bool authenticate(ref string function)
        {
            create_path();
            read_config();
            if (Config_file_content.Length > 0)
            {
                if (Config_file_content[0] == "ALL")
                    return true;
                else
                {
                    //Find permission
                    foreach(string s_function in Config_file_content)
                    {
                        if (s_function == function)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}

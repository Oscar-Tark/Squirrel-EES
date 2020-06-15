using System.IO;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        public void runscriptcondition(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All fules must be UTF8
            //::*if, *is, *Path
            if ((string)(var_get(objects[0])) == (string)(var_get(objects[1])))
            {
                string line;
                FileStream fd = new FileStream((string)var_get((string)objects[2]), FileMode.Open);
                StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
                while ((line = sr.ReadLine()) != null)
                {
                    scorpion_exec((object)line);
                }
                sr.Close();
                fd.Close();

                var_dispose_internal(ref line);
            }
            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }

        public void runscript(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All fules must be UTF8
            //(*Path)
            string line;
            FileStream fd = new FileStream((string)var_get((string)objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            while ((line = sr.ReadLine()) != null)
                scorpion_exec((object)line);
            sr.Close();
            fd.Close();

            Scorp_Line_Exec = null;
            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void runscriptencrypted(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //All fules must be UTF8
            //::*path, *key
            string line = "";
            string encry_ = read_file((string)var_get(objects[0]));

            byte[] b = Do_on.crypto.decrypt(Do_on.crypto.To_Byte(encry_), (string)var_get(objects[1]));

            object decry_ = Do_on.crypto.To_Object(new MemoryStream(b));

            /*while ((line = sr.ReadLine()) != null)
            {
                scorpion_exec((object)line);
            }*/

            Scorp_Line_Exec = null;
            var_dispose_internal(ref line);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void scriptencrypt(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*path, *key
            FileStream fd = new FileStream((string)var_get(objects[0]), FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);

            byte[] b = Do_on.crypto.encrypt(sr.ReadToEnd(), (string)var_get(objects[1]));

            File.WriteAllBytes((string)var_get(objects[0]), b);

            sr.Close();
            fd.Close();

            Scorp_Line_Exec = null;
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}

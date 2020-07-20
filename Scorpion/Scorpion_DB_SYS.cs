using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Windows.Forms;

namespace Dumper
{
    public class Virtual_Dumper_System
    {
        Scorpion.Form1 Do_on;

        public Virtual_Dumper_System(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void Create_DB(string path, string pwd)
        {
            ArrayList al = new ArrayList { new string[0x3a], new string[0x3a] };
            byte[] b = Do_on.crypto.To_Byte(al);
            //b = Do_on.crypto.encrypt(b, pwd);

            File.WriteAllBytes(path, b);//Do_on.crypto.encrypt(b, pwd));

            b = null;
            path = null;
            pwd = null;
            return;
        }

        public ArrayList Load_DB(string path, string pwd)
        {
            byte[] b = File.ReadAllBytes(path);
            //b = Do_on.crypto.decrypt(b, pwd);

            pwd = null;
            object o = Do_on.crypto.To_Object(b);
            return (ArrayList)o;
        }

        public void Save_DB(string path, string pwd)
        {
            byte[] b = Do_on.crypto.To_Byte(Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(path)]);//Do_on.crypto.encrypt(Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(path)], pwd);
            File.WriteAllBytes(path, b);

            path = null;
            pwd = null;

            return;
        }

        public void Close_DB(string path)
        {
            Do_on.AL_TBLE.TrimToSize();
            Do_on.AL_TBLE_REF.TrimToSize();

            return;
        }

        public string Data_getDB(string db, string reference)
        {
            ArrayList al_tmp = (ArrayList)Do_on.AL_TBLE[Do_on.AL_TBLE_REF.IndexOf(db)];
            int ndx = Array.IndexOf((string[])al_tmp[0], reference);

            if (ndx == -1)
                return "";
            return ((string[])al_tmp[1])[ndx];
        }

        public void Data_setDB(string db, string name, string data)
        {
            int ndx = Do_on.AL_TBLE_REF.IndexOf(db);
            int position = -1;

            string[] tbl = (string[])((ArrayList)Do_on.AL_TBLE[ndx])[0];

            for (int i = 0; i < tbl.Length; i++)
            {
                if (tbl[i] == null || tbl[i] == Do_on.types.S_NULL)
                    position = i;
                break;
            }

            //Add reference
            ((string[])((ArrayList)Do_on.AL_TBLE[ndx])[0])[position] = name;

            //Add data
            ((string[])((ArrayList)Do_on.AL_TBLE[ndx])[1])[position] = data;

            tbl = null;
            return;
        }
        /*
        //DUMP SYSTEM DB's
        public void Dump_main_db()
        {
            /*var Serializer = new BinaryFormatter();
            
            ArrayList al_rec_tmp = new ArrayList();
            foreach (ArrayList al in Do_on.AL_REC)
            {
                al_rec_tmp.Add(((System.Windows.Forms.Timer)al[0]).Interval);
            }
            byte[] b = Do_on.crypto.encrypt(new ArrayList() { Do_on.AL_CURR_VAR, Do_on.AL_CURR_VAR_REF, Do_on.AL_EVT, Do_on.AL_Ref_EVT, al_rec_tmp, Do_on.AL_REC_REF, Do_on.AL_SHS, Do_on.AL_SHS_REF, Do_on.AL_AUTH, Do_on.AL_AUTH_REF }, Do_on.SHA);
            File.WriteAllBytes(Do_on.AL_HIB_FILES[0].ToString(), b);
            
            Serializer = null;*/

        /*  return;
      }
        /*
        public void Verify_Directory_DB()
        {
            if (!Directory.Exists(Do_on.AL_DIRECTORIES[0].ToString()))
            {
                Directory.CreateDirectory(Do_on.AL_DIRECTORIES[0].ToString());
            }
            return;
        }

        public void Verify_File_DB(string Name)
        {
            if (!Directory.Exists(Do_on.AL_DIRECTORIES[0].ToString()))
            {
                Directory.CreateDirectory(Do_on.AL_DIRECTORIES[0].ToString());
            }

            if (!File.Exists(Do_on.AL_DIRECTORIES[0].ToString() + Name + Do_on.AL_EXTENSNS[1]))
            {
                FileStream fs = new FileStream(Do_on.AL_DIRECTORIES[0].ToString() + Name + Do_on.AL_EXTENSNS[1], FileMode.Create, FileAccess.Write);
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }

            Name = null;
            return;
        }

        public void Un_Dump(string pwd)
        {
            ArrayList al_tmp = new ArrayList();
            /*if (Do_on.readr.lib_SCR.var_get("*secs").ToString() == Do_on.types.S_Yes)
            {*/
        /* byte[] b = File.ReadAllBytes(Do_on.AL_HIB_FILES[0].ToString());
         byte[] read_bytes = Do_on.crypto.decrypt(b, pwd);

         al_tmp = (ArrayList)Do_on.crypto.To_Object(new MemoryStream(read_bytes));
     /*}
     else
     {
         var Serializer = new BinaryFormatter();
         using (var stream = File.OpenRead(Do_on.AL_HIB_FILES[0].ToString()))
         {
             al_tmp = (ArrayList)Serializer.Deserialize(stream);
         }

         Serializer = null;
     }*/


        /* Do_on.AL_CURR_VAR = (ArrayList)al_tmp[0];
         Do_on.AL_CURR_VAR_REF = (ArrayList)al_tmp[1];
         //Do_on.AL_EVT = (ArrayList)al_tmp[2];
         //Do_on.AL_Ref_EVT = (ArrayList)al_tmp[3];

         foreach (int i in (ArrayList)al_tmp[4])
         {
             System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
             t.Interval = i;
             //Do_on.AL_REC.Add(t);
         }

         //Do_on.AL_REC_REF = (ArrayList)al_tmp[5];
         //Do_on.AL_SHS = (ArrayList)al_tmp[6];
         //Do_on.AL_SHS_REF = (ArrayList)al_tmp[7];
         //Do_on.AL_AUTH = (ArrayList)al_tmp[8];
         //Do_on.AL_AUTH_REF = (ArrayList)al_tmp[9];
         //Do_on.AL_OBJ_3D = (ArrayList)al_tmp[8];
         //Do_on.AL_OBJ_3D_REF = (ArrayList)al_tmp[9];

         Do_on.readr.lib_SCR.var_arraylist_dispose(ref al_tmp);

         //OLD
         /*
         var Serializer = new BinaryFormatter();
         using (var stream = File.OpenRead(Do_on.AL_HIB_FILES[0].ToString()))
         {
             try
             {
                 ArrayList al_tmp = (ArrayList)Serializer.Deserialize(stream);
                 Do_on.AL_CURR_VAR = (ArrayList)al_tmp[0];
                 Do_on.AL_CURR_VAR_REF = (ArrayList)al_tmp[1];
                 Do_on.AL_EVT = (ArrayList)al_tmp[2];
                 Do_on.AL_Ref_EVT = (ArrayList)al_tmp[3];

                 foreach (int i in (ArrayList)al_tmp[4])
                 {
                     System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                     t.Interval = i;
                     Do_on.AL_REC.Add(t);
                 }

                 Do_on.AL_REC_REF = (ArrayList)al_tmp[5];
                 Do_on.AL_SHS = (ArrayList)al_tmp[6];
                 Do_on.AL_SHS_REF = (ArrayList)al_tmp[7];
                 //Do_on.AL_OBJ_3D = (ArrayList)al_tmp[8];
                 //Do_on.AL_OBJ_3D_REF = (ArrayList)al_tmp[9];

                 Do_on.readr.lib_SCR.var_arraylist_dispose(ref al_tmp);
             }
             catch { }
         }*/

        //Serializer = null;
        /* return;
     }*/

    }
}
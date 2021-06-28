using System;
using System.Collections;
using System.IO;
using System.Security;
using System.Threading;

//DEPRECIATED
namespace Dumper
{
    public class Virtual_Dumper_System
    {
        private const int DEFAULT_SLOT_SIZE = 2048;
        private const int DEFAULT_STRT_SIZE = 0;
        private readonly ushort[] Field_Type_Data = { 0x00, 0x01 };

        Scorpion.Scorp HANDLE;
        /*private string db_file = ".stg";
        private string current_path = "";
        private string db_folder = "vds";
        private string db_folder_full_path = null;
        private string db_config_path = null;
        private string db_config_file = "scorpion.config";
        private string db_config = null;*/

        public Virtual_Dumper_System(Scorpion.Scorp HANDLE_)
        {
            HANDLE = HANDLE_;
            /*get_paths();
            create_FileDir();
            get_config();*/
            return;
        }

        /*private void get_config()
        {
            db_config = File.ReadAllText(db_config_path);
        }

        private void get_paths()
        {
            current_path = Environment.CurrentDirectory;
            db_folder_full_path = current_path + db_folder;
            db_config_path = current_path + db_config_file;
            return;
        }

        private void create_FileDir()
        {
            if (!Directory.Exists(db_folder_full_path))
                Directory.CreateDirectory(db_folder_full_path);

            if (!File.Exists(db_config_path))
                File.Create(db_config_path);
            return;
        }*/

        private void NULLIFY(ref string[] reference_, ref string[] data)
        {
            for(int i = 0; i < reference_.Length; i++)
            {
                reference_[i] = null;
                data[i] = null;
            }
            return;
        }

        private bool Field_Type_Ok(ushort FIELD_TYPE)
        {
            foreach(ushort val in Field_Type_Data)
            {
                if (FIELD_TYPE == val)
                    return true;
            }
            return false;
        }

        public void Create_DB(string path)
        {
            object[] s_data = new object[DEFAULT_SLOT_SIZE];
            string[] s_tag = new string[DEFAULT_SLOT_SIZE];
            string[] s_meta = new string[DEFAULT_SLOT_SIZE];
            ushort[] field_type = new ushort[DEFAULT_SLOT_SIZE];
            //Create SHA seed
            SecureString s_seed = HANDLE.crypto.Create_Seed();
            //Create SHA out of seed
            string sha_ = HANDLE.crypto.SHA_SS(s_seed);
                                               /*DATA  TAG    META    TYPE        VERIFYING SHA     CURRENT SIZE        MAXSIZE*/
            ArrayList al = new ArrayList (7) { s_data, s_tag, s_meta, field_type, sha_,             DEFAULT_STRT_SIZE, DEFAULT_SLOT_SIZE };

            //Encrypt s_dat, s_tag, s_meta with the seed

            byte[] bte = HANDLE.crypto.To_Byte(al);
            File.WriteAllBytes(path, bte);
            bte = null;
            path = null;
            return;
        }

        public ArrayList Load_DB(string path)
        {
            //File.Decrypt(path);
            byte[] b = File.ReadAllBytes(path);
            //Get pwd as securestring
            SecureString scr = new SecureString();
            object o = HANDLE.crypto.To_Object(b);
            return (ArrayList)o;
        }

        public void Save_DB(string path, string pwd)
        {
            //Save in segments of 0x3a each
            //File.Encrypt(path);
            byte[] b = HANDLE.crypto.To_Byte(HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(path)]);
            File.WriteAllBytes(path, b);
            path = null;
            pwd = null;
            return;
        }

        public object[] Data_getDB(string db, string query)
        {
            return Run_Query(ref db, ref query);
        }

        public bool Data_setDB(string path, object data, string meta, string tag, ushort type)
        {
            lock (HANDLE.mem.AL_TBLE)
            {
                ArrayList al_tmp = (ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(path)];
                int size = (int)al_tmp[5];
                if (size == DEFAULT_SLOT_SIZE - 1 && Field_Type_Ok(type))
                    return false;
                ((object[])al_tmp[0])[size] = data;
                ((string[])al_tmp[1])[size] = tag;
                ((string[])al_tmp[2])[size] = meta;
                ((ushort[])al_tmp[3])[size] = type;
                al_tmp[5] = size++;
            }
            return true;
        }

        private int get_position(ref string[] tbl)
        {
            for (int i_pos = 0; i_pos < tbl.Length; i_pos++)
            {
                if (tbl[i_pos] == null)
                    return i_pos;

                //if (i_pos == tbl.Length - 1 && position == 0)
                //    Segment_DB(ref db);
            }


            return -1;
        }

        private object[] Run_Query(ref string db_name, ref string query)
        {
            //'value@joe dawson'
            //'ref@12342'
            //'meta@city=DENVER'
            //'tag@TEACHERS'
            int pcount = Environment.ProcessorCount;
            //Get size
            int size = (int)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db_name)])[5];
            //get rounded sizes to work on
            double split_size = size / pcount;
            int split_final = (int)Math.Round(split_size);

            //Create array with final elements to split for a processor count based search
            object[] parent_of_splits = new object[pcount];
            for (int i = 1; i <= pcount; i++)
            {
                ((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db_name)]).CopyTo(0, splitable_array, 0, split_final);
            }
            for (int i = 1; i < split_final; i++)
            {
                Thread th_query = new Thread(new ParameterizedThreadStart()).Start();
            }
            //check if search is false then return everything
            return new object[0];
        }


        /*
        //DUMP SYSTEM DB's
        public void Dump_main_db()
        {
            /*var Serializer = new BinaryFormatter();
            
            ArrayList al_rec_tmp = new ArrayList();
            foreach (ArrayList al in HANDLE.AL_REC)
            {
                al_rec_tmp.Add(((System.Windows.Forms.Timer)al[0]).Interval);
            }
            byte[] b = HANDLE.crypto.encrypt(new ArrayList() { HANDLE.AL_CURR_VAR, HANDLE.AL_CURR_VAR_REF, HANDLE.AL_EVT, HANDLE.AL_Ref_EVT, al_rec_tmp, HANDLE.AL_REC_REF, HANDLE.AL_SHS, HANDLE.AL_SHS_REF, HANDLE.AL_AUTH, HANDLE.AL_AUTH_REF }, HANDLE.SHA);
            File.WriteAllBytes(HANDLE.AL_HIB_FILES[0].ToString(), b);
            
            Serializer = null;*/

        /*  return;
      }
        /*
        public void Verify_Directory_DB()
        {
            if (!Directory.Exists(HANDLE.AL_DIRECTORIES[0].ToString()))
            {
                Directory.CreateDirectory(HANDLE.AL_DIRECTORIES[0].ToString());
            }
            return;
        }

        public void Verify_File_DB(string Name)
        {
            if (!Directory.Exists(HANDLE.AL_DIRECTORIES[0].ToString()))
            {
                Directory.CreateDirectory(HANDLE.AL_DIRECTORIES[0].ToString());
            }

            if (!File.Exists(HANDLE.AL_DIRECTORIES[0].ToString() + Name + HANDLE.AL_EXTENSNS[1]))
            {
                FileStream fs = new FileStream(HANDLE.AL_DIRECTORIES[0].ToString() + Name + HANDLE.AL_EXTENSNS[1], FileMode.Create, FileAccess.Write);
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
            /*if (HANDLE.readr.lib_SCR.var_get("*secs").ToString() == HANDLE.types.S_Yes)
            {*/
        /* byte[] b = File.ReadAllBytes(HANDLE.AL_HIB_FILES[0].ToString());
         byte[] read_bytes = HANDLE.crypto.decrypt(b, pwd);

         al_tmp = (ArrayList)HANDLE.crypto.To_Object(new MemoryStream(read_bytes));
     /*}
     else
     {
         var Serializer = new BinaryFormatter();
         using (var stream = File.OpenRead(HANDLE.AL_HIB_FILES[0].ToString()))
         {
             al_tmp = (ArrayList)Serializer.Deserialize(stream);
         }

         Serializer = null;
     }*/


        /* HANDLE.AL_CURR_VAR = (ArrayList)al_tmp[0];
         HANDLE.AL_CURR_VAR_REF = (ArrayList)al_tmp[1];
         //HANDLE.AL_EVT = (ArrayList)al_tmp[2];
         //HANDLE.AL_Ref_EVT = (ArrayList)al_tmp[3];

         foreach (int i in (ArrayList)al_tmp[4])
         {
             System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
             t.Interval = i;
             //HANDLE.AL_REC.Add(t);
         }

         //HANDLE.AL_REC_REF = (ArrayList)al_tmp[5];
         //HANDLE.AL_SHS = (ArrayList)al_tmp[6];
         //HANDLE.AL_SHS_REF = (ArrayList)al_tmp[7];
         //HANDLE.AL_AUTH = (ArrayList)al_tmp[8];
         //HANDLE.AL_AUTH_REF = (ArrayList)al_tmp[9];
         //HANDLE.AL_OBJ_3D = (ArrayList)al_tmp[8];
         //HANDLE.AL_OBJ_3D_REF = (ArrayList)al_tmp[9];

         HANDLE.readr.lib_SCR.var_arraylist_dispose(ref al_tmp);

         //OLD
         /*
         var Serializer = new BinaryFormatter();
         using (var stream = File.OpenRead(HANDLE.AL_HIB_FILES[0].ToString()))
         {
             try
             {
                 ArrayList al_tmp = (ArrayList)Serializer.Deserialize(stream);
                 HANDLE.AL_CURR_VAR = (ArrayList)al_tmp[0];
                 HANDLE.AL_CURR_VAR_REF = (ArrayList)al_tmp[1];
                 HANDLE.AL_EVT = (ArrayList)al_tmp[2];
                 HANDLE.AL_Ref_EVT = (ArrayList)al_tmp[3];

                 foreach (int i in (ArrayList)al_tmp[4])
                 {
                     System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
                     t.Interval = i;
                     HANDLE.AL_REC.Add(t);
                 }

                 HANDLE.AL_REC_REF = (ArrayList)al_tmp[5];
                 HANDLE.AL_SHS = (ArrayList)al_tmp[6];
                 HANDLE.AL_SHS_REF = (ArrayList)al_tmp[7];
                 //HANDLE.AL_OBJ_3D = (ArrayList)al_tmp[8];
                 //HANDLE.AL_OBJ_3D_REF = (ArrayList)al_tmp[9];

                 HANDLE.readr.lib_SCR.var_arraylist_dispose(ref al_tmp);
             }
             catch { }
         }*/

        //Serializer = null;
        /* return;
     }*/

    }
}
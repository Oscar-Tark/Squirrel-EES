using System.Collections;
using System.IO;
using System.Security;

//DEPRECIATED
namespace Dumper
{
    public class Virtual_Dumper_System
    {
        private const int DEFAULT_SLOT_SIZE = 4096;
        private const int DEFAULT_STRT_SIZE = 0;
        private readonly string[] Field_Type_Data = { "dat", "num", "bin" };
        private readonly ArrayList Query_Types = new ArrayList(4) { "data", "tag", "meta", "type" };
        Scorpion.Scorp HANDLE;

        public Virtual_Dumper_System(Scorpion.Scorp HANDLE_)
        {
            HANDLE = HANDLE_;
            return;
        }

        private void NULLIFY(ref ArrayList AL)
        {
            for (int i = 0; i < AL.Count; i++)
                AL[i] = 0x00;
            return;
        }

        private bool Field_Type_Ok(string FIELD_TYPE)
        {
            foreach(string val in Field_Type_Data)
            {
                if (FIELD_TYPE == val)
                    return true;
            }
            return false;
        }

        public void Create_DB(string path)
        {
            ArrayList s_tag = new ArrayList(DEFAULT_SLOT_SIZE);
            ArrayList s_data = new ArrayList(DEFAULT_SLOT_SIZE);
            //Create SHA seed
            SecureString s_seed = HANDLE.crypto.Create_Seed();
            //Create SHA out of seed
            string sha_ = HANDLE.crypto.SHA_SS(s_seed);
                                               /*DATA  TAG */
            ArrayList al = new ArrayList (2) { s_data, s_tag };
            byte[] bte = HANDLE.crypto.To_Byte(al);
            File.WriteAllBytes(path, bte);
            bte = null;
            path = null;
            return;
        }

        public void Close_DB(string path)
        {
            //PURGE MEMORY
            ((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(path)]).Clear();
            ((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(path)]).TrimToSize();
            HANDLE.mem.AL_TBLE.RemoveAt(HANDLE.mem.AL_TBLE_REF.IndexOf(path));
            HANDLE.mem.AL_TBLE.TrimToSize();
            HANDLE.mem.AL_TBLE_REF.Remove(path);
            HANDLE.mem.AL_TBLE_REF.TrimToSize();
            return;
            //Close and remove
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

        public ArrayList Data_getDB(string db, object data, string tag)
        {
            //0=tagsearch
            //1=datasearch
            ArrayList returnable = new ArrayList();
            ArrayList tag_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[1];
            ArrayList data_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0];
            int current = 0;
            if (tag != HANDLE.types.S_NULL && tag != HANDLE.types.S_No)
            {
                while (current < DEFAULT_SLOT_SIZE)
                {
                    current = tag_handle.IndexOf(tag, current);
                    if (current == -1)
                        break;
                    returnable.Add(data_handle[current]);
                    current++;
                }
            }
            else if(data != null)
            {
                while (current < DEFAULT_SLOT_SIZE)
                {
                    current = data_handle.IndexOf(data, current);
                    if (current == -1)
                        break;
                    returnable.Add(data_handle[current]);
                    current++;
                }
            }
            return returnable;
        }

        public bool Data_setDB(string path, object data, string tag)
        {
            lock (HANDLE.mem.AL_TBLE)
            {
                ArrayList al_tmp = (ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(path)];
                if (al_tmp.Count >= DEFAULT_SLOT_SIZE)
                    return false;
                ((ArrayList)al_tmp[0]).Add(data);
                ((ArrayList)al_tmp[1]).Add(tag);
            }
            return true;
        }
    }
}
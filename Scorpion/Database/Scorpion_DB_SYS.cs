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

        public void Data_getDB(string db, object data, string tag, object resultvar)
        {
            //Make container for thread objects that will be initiated
            Thread[] th_contain = new Thread[Environment.ProcessorCount];
            int current_start = 0; int current_end = 0;

            //Create threads for processing
            for (int i = 0; i <= (Environment.ProcessorCount - 1); i++)
            {
                th_contain[i] = new Thread(new ParameterizedThreadStart(DBGET_thread));
                current_start = (DEFAULT_SLOT_SIZE / Environment.ProcessorCount) * i;
                current_end = (DEFAULT_SLOT_SIZE / Environment.ProcessorCount) * i++;
                th_contain[i].Start(new object[] { current_start, current_end, data, tag, db, resultvar });
            }

            //DEPRECIATED, keeping just incase
            /*ArrayList returnable = new ArrayList();
            ArrayList tag_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[1];
            ArrayList data_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0];
            /*int current = 0;
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
            return returnable;*/
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

        private void DBGET_thread(object dat)
        {
            //Get passed elements
            int start = (int)((object[])dat)[0];
            int end = (int)((object[])dat)[1];
            string src_dat = (string)((object[])dat)[2];
            string src_tag = (string)((object[])dat)[3];
            string db = (string)((object[])dat)[4];
            string resvar = (string)((object[])dat)[5];

            //Create variable for results
            ArrayList returnable = new ArrayList(4096) { };
            //Create a copy of the database
            ArrayList tag_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[1];
            ArrayList data_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0];

            //Shorten down to ranges defined by the start and end integers
            //tag_handle.RemoveRange();

            int current = 0;
            if (src_tag != HANDLE.types.S_NULL && src_tag != HANDLE.types.S_No)
            {
                while (current < DEFAULT_SLOT_SIZE)
                {
                    current = tag_handle.IndexOf(src_tag, current);
                    if (current == -1)
                        break;
                    returnable.Add(data_handle[current]);
                    current++;
                }
            }
            else if (src_dat != null)
            {
                while (current < DEFAULT_SLOT_SIZE)
                {
                    current = data_handle.IndexOf(src_dat, current);
                    if (current == -1)
                        break;
                    returnable.Add(data_handle[current]);
                    current++;
                }
            }

            //Add result to existing variable, this is done manually for specific reasons such as checking weather the variable already contains an array if so the results are added to the variable if not the array is added directly
            lock(HANDLE.mem.AL_CURR_VAR) lock(HANDLE.mem.AL_CURR_VAR_REF) lock(HANDLE.mem.AL_CURR_VAR_TAG) lock(HANDLE.mem.AL_CURR_VAR_EVT)
                        {
                            if(((ArrayList)HANDLE.mem.AL_CURR_VAR[HANDLE.mem.AL_CURR_VAR_REF.IndexOf(resvar)])[2].GetType() is ArrayList)
                            {
                                //This variable already contains an array, add to that array
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_CURR_VAR[HANDLE.mem.AL_CURR_VAR_REF.IndexOf(resvar)])[2]);
                            }
                            else
                            {
                                //This variable does not contain an array, convert its value to one by directly copying the result array
                            }
                        }
            return;
        }
    }
}
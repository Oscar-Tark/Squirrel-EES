/*  <Scorpion IEE(Intelligent Execution Environment). Server To Run Scorpion Built Applications Using the Scorpion Language>
    Copyright (C) <2020+>  <Oscar Arjun Singh Tark>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Collections;
using System.IO;
using System.Security;

//DEPRECIATED will be replaced with a C version
namespace Scorpion_MDB
{
    public class Scorpion_Micro_DB
    {
        public readonly int DEFAULT_SLOT_SIZE = 50000;
        private const int DEFAULT_STRT_SIZE = 0;
        private readonly string[] Field_Type_Data = { "dat", "num", "bin" };
        private readonly ArrayList Query_Types = new ArrayList(4) { "data", "tag", "meta", "type" };
        Scorpion.Scorp HANDLE;

        public Scorpion_Micro_DB(Scorpion.Scorp HANDLE_)
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

        public void Create_DB(string path, bool spill)
        {
            //A scorpion database is composed of three data fields:
            /*
            * Tag: A tag consists of a super identifier. This can be a group to which the data belongs to such as 'Row1'
            * SubTag: A subtag consists of an identifier of what the data represents within the tag, for example 'Name' or 'Age' within the Tag 'Row1'
            * Data: Data contained in the database
            */

            ArrayList s_subtag = new ArrayList(DEFAULT_SLOT_SIZE);
            ArrayList s_tag = new ArrayList(DEFAULT_SLOT_SIZE);
            ArrayList s_data = new ArrayList(DEFAULT_SLOT_SIZE);

            //Group tag allows us to group multiple databases in clusters incase one database gets to it's maximum size data can spillover into a new database with the same groupname
            FileInfo fnf_db = new FileInfo(path);

            //Create SHA seed
            SecureString s_seed = HANDLE.crypto.Create_Seed();
            //Create SHA out of seed
            string sha_ = HANDLE.crypto.SHA_SS(s_seed);
            /*DATA  TAG */
            ArrayList al = new ArrayList (3) { s_data, s_tag, s_subtag };
            File.WriteAllText(path, HANDLE.crypto.Array_To_String(al));
            path = null;
            return;
        }

        public void Close_DB(string name)
        {
            //Close and remove
            ((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(name)]).Clear();
            ((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(name)]).TrimToSize();
            HANDLE.mem.AL_TBLE.RemoveAt(HANDLE.mem.AL_TBLE_REF.IndexOf(name));
            HANDLE.mem.AL_TBLE.TrimToSize();
            HANDLE.mem.AL_TBLE_PATH.RemoveAt(HANDLE.mem.AL_TBLE_REF.IndexOf(name));
            HANDLE.mem.AL_TBLE_PATH.TrimToSize();
            HANDLE.mem.AL_TBLE_REF.Remove(name);
            HANDLE.mem.AL_TBLE_REF.TrimToSize();
            return;
        }

        public void Load_DB(string path, string name)
        {
            //File.Decrypt(path);
            //byte[] b = File.ReadAllBytes(path);
            string xml = File.ReadAllText(path);

            //Get pwd as securestring
            SecureString scr = new SecureString();
            object db_object = HANDLE.crypto.String_To_Array(xml);

            if (!HANDLE.mem.AL_TBLE_REF.Contains(name) && !HANDLE.mem.AL_TBLE_PATH.Contains(path))
            {
                HANDLE.mem.AL_TBLE.Add(db_object);
                HANDLE.mem.AL_TBLE_REF.Add(name);
                HANDLE.mem.AL_TBLE_PATH.Add(path);
                HANDLE.write_to_cui("Opened Database: [" + path + "] as [" + name + "]");
            }
            else
                HANDLE.write_to_cui("Database [" + path + "]/[" + name + "] already in memory");

            return;
        }

        public void Save_DB(string name, string pwd)
        {
            //Save in segments of 0x3a each
            //File.Encrypt(path);
            int ndx = HANDLE.mem.AL_TBLE_REF.IndexOf(name);
            File.WriteAllText((string)HANDLE.mem.AL_TBLE_PATH[ndx], HANDLE.crypto.Array_To_String((ArrayList)HANDLE.mem.AL_TBLE[ndx]));
            name = null;
            pwd = null;
            return;
        }

        public bool Data_setDB(string name, object data, string tag, string subtag)
        {
            lock (HANDLE.mem.AL_TBLE)
            {
                ArrayList al_tmp = (ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(name)];
                ((ArrayList)al_tmp[0]).Add(data);
                ((ArrayList)al_tmp[1]).Add(tag);
                ((ArrayList)al_tmp[2]).Add(subtag);
            }
            return true;
        }

        public ArrayList Data_getDB_all_no_thread(string db)
        {
            ArrayList data_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0];
            return data_handle;
        }

        public readonly short OPCODE_GET = 0x00;
        public readonly short OPCODE_DELETE = 0x02;
        public ArrayList Data_doDB_selective_no_thread(string db, object data, string tag, string subtag, short OPCODE)
        {
            ArrayList returnable = new ArrayList();
            ArrayList subtag_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[2];
            ArrayList tag_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[1];
            ArrayList data_handle = (ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0];
            bool skip = false;
            int current = 0;
            //Get data by tag
            if (tag != HANDLE.types.S_NULL && tag != HANDLE.types.S_No)
            {
                while (current < DEFAULT_SLOT_SIZE)
                {
                    //Reset skip
                    skip = false;

                    //Get next index of occurrance
                    current = tag_handle.IndexOf(tag, current);

                    //Refine search with subtag if any. If not skip
                    if (subtag != HANDLE.types.S_NULL && subtag != HANDLE.types.S_No && current != -1)
                    {
                        //If the subtags do not match, do not include the result
                        if ((string)subtag_handle[current] != subtag)
                            skip = true;
                    }

                    //If there are no more occurances break the loop
                    if (current == -1)
                        break;

                    //If the value does not fit due to a wrong subtag skip and advance search
                    if (!skip)
                    {
                        if (OPCODE == OPCODE_GET)
                            returnable.Add(data_handle[current]);
                        else if (OPCODE == OPCODE_DELETE)
                        {
                            lock (HANDLE.mem.AL_TBLE)
                            {
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0]).RemoveAt(current);
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[1]).RemoveAt(current);
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[2]).RemoveAt(current);
                            }
                        }
                    }
                    current++;
                }
            }
            //Get data by value
            else if (data != null && (string)data != HANDLE.types.S_NULL)
            {
                //ArrayList temp_tags = new ArrayList();
                int current_tag = 0;

                while (current < DEFAULT_SLOT_SIZE)
                {
                    current = data_handle.IndexOf(data, current);

                    //Gets the tag for the current data value and extracts all data related to that tag
                    //temp_tags.Add(tag_handle[current]);

                    //Get all data with the same tag
                    if (current == -1)
                        break;
                    //Get data with related tag
                    while (current_tag != -1)
                    {
                        //Find tag for the current data value
                        current_tag = tag_handle.IndexOf(tag_handle[current], current_tag);
                        if (current_tag == -1)
                            break;

                        if (OPCODE == OPCODE_GET)
                        {
                            //If index is not -1 or so the tag exists then add the value
                            returnable.Add(data_handle[current_tag]);
                        }
                        else if (OPCODE == OPCODE_DELETE)
                        {
                            lock (HANDLE.mem.AL_TBLE)
                            {
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[0]).RemoveAt(current_tag);
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[1]).RemoveAt(current_tag);
                                ((ArrayList)((ArrayList)HANDLE.mem.AL_TBLE[HANDLE.mem.AL_TBLE_REF.IndexOf(db)])[2]).RemoveAt(current_tag);
                            }
                        }
                        //Increment tag index so not to stay stuck on the preceeding one
                        current_tag++;
                    }

                    current++;
                }
            }
            return returnable;
        }
    }
}
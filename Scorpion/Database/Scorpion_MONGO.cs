
using System.Collections;
using Scorpion_MongoDB_Library;

namespace Scorpion
{
    partial class Librarian
    {
        //Legacy DB
        /*BASIC DB FUNCTIONS FOR INTERNAL DB
         * DB's in scorpion store by name&value  
         * Basic structure
         * {ref}
         * {data}
         * {tags}
         * {meta}
        */
        //MONGODB
        public void mongodbstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //::*url, *port
            Mongodb.Mongodbstart((string)var_get(objects[0]), (string)var_get(objects[1]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }

        public string mongodbgetall(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //returnable<<::*db, *collection
            string JSON = Mongodb.Mongogetall((string)var_get(objects[0]), (string)var_get(objects[1]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return var_create_return(ref JSON, true);
        }

        public string mongodbgetfilter(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //returnable<<::*db, *collection, *filter
            string JSON = Mongodb.Mongogetspecific((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return var_create_return(ref JSON, true);
        }

        public void mongodbsetone(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Mongodb.Mongoset((string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            var_arraylist_dispose(ref objects);
            Scorp_Line_Exec = null;
            return;
        }
    }
}
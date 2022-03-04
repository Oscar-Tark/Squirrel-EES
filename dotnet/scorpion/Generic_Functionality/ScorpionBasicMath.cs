using System;
using System.Collections;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        public string add(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<add::*arg, *arg, *arg...
            //Check if all values are numeric, else concatenate
            bool numeric = true;
            int rubbish;
            double total = 0;
            string returnable = null;
            foreach(string val in objects)
            {
                if (!int.TryParse((string)var_get(val), out rubbish))
                    numeric = false;
            }

            //If numeric do an addition else concatenate
            if (numeric)
            {
                foreach (object val in objects)
                    total += Convert.ToDouble(var_get(val));
                returnable = Convert.ToString(total);
            }
            else
            {
                foreach (object val in objects)
                    returnable = returnable + (string)var_get(val);
            }

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(returnable, true);
        }

        public string substract(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<substract::*doubleargsubstractthis, *doublearg, *doublearg...
            double total = Convert.ToDouble(var_get(objects[0]));
            for (int i = 1; i < objects.Count; i++)
                total -= Convert.ToDouble(var_get(objects[i]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(Convert.ToString(total), true);
        }

        //multiply, divide, modulo, to the power
        public string multiply(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<multiply::*doublearg, *doublearg, *doublearg...
            double total = Convert.ToDouble(var_get(objects[0]));
            for (int i = 1; i < objects.Count; i++)
                total *= Convert.ToDouble(var_get(objects[i]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(Convert.ToString(total), true);
        }

        public string divide(ref string Scorp_Line_Exec, ref ArrayList objects)
        {   
            //*return<<divide::*doublearg, *doublearg, *doublearg...
            double total = Convert.ToDouble(var_get(objects[0]));
            for (int i = 1; i < objects.Count; i++)
                total /= Convert.ToDouble(var_get(objects[i]));
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return var_create_return(Convert.ToString(total), true);
        }
    }
}
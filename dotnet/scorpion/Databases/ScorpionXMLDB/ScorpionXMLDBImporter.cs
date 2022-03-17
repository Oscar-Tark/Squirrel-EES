using System.IO;

namespace Scorpion.MicroDB
{
    [System.Obsolete]
    public class Scorpion_MicroDB_Importer
    {
        /*Imports three files in any folder into a MicroDB entry as a loadable application:
         * > structure.vue       
         * > logic.vue
         * > visuals.vue        
         */
        private const string structure_file = "structure.vue";
        private const string logic_file = "logic.vue";
        private const string visuals_file = "visuals.vue";

        public static string[] importScripts(string data_base_name, string folder)
        {
            string[] file_contents = { null, null, null };
            if(Directory.Exists(folder))
            {
                //Check if the three required files exist
                if(File.Exists(folder + structure_file) && File.Exists(folder + structure_file) && File.Exists(folder + structure_file))
                {
                    file_contents[0] = File.ReadAllText(folder + structure_file);
                    file_contents[1] = File.ReadAllText(folder + logic_file);
                    file_contents[2] = File.ReadAllText(folder + visuals_file);
                    return file_contents;
                }
            }
            return null;
        }
    }
}

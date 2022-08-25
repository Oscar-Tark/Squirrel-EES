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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Scorpion
{ 
    public partial class Librarian
    { 
        public void manual(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Show a manual file
            //::*page
            //STD path for all MAN is ./man
            if (objects.Count != 0)
                showMan((string)MemoryCore.varGet(objects[0]));
            else
                showManDir();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }

        public void manualrefresh(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            if(Directory.Exists(Types.main_user_manuals_path))
                Directory.Delete(Types.main_user_manuals_path, true);

            checkManDir();

            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        } 
    }

    public partial class Librarian
    {
        private string man_extension = ".man";

        private void showMan(string function)
        {
            //Show a manual file
            checkManDir();
            if (File.Exists(Types.main_user_manuals_path + '/' + function + man_extension))
            {                                                       
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("MAN/READERS MANUAL Entry for '" + function + "':\n******************************************************\nFUNCTION: [" + function + "]\n");
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput(ScorpionFilesOperationsCore.readFile(Types.main_user_manuals_path + '/' + function + man_extension));
            }
            else
                ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("No man entry exists for '" + function + "'");
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("If you would like to refresh your manuals, run the command 'manualrefresh'");
            function = null;
            return;
        }

        private void showManDir()
        {
            //Enumerate and show the contents of the man pages directory
            ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Available man pages:");
            checkManDir();
            DirectoryInfo df = new DirectoryInfo(Types.main_user_manuals_path);
            foreach (FileInfo man_fnf in df.EnumerateFiles("*.man"))
                ScorpionConsoleReadWrite.ConsoleWrite.writeSpecial(man_fnf.Name.Replace(".man", ""));
            return;
        }

        private void checkManDir()
        {
            if (!Directory.Exists(Types.main_user_manuals_path))
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("No manuals directory found. Creating...");
                Directory.CreateDirectory(Types.main_user_manuals_path);
                downloadMan();
            }
            return;
        }

        private async void downloadMan()
        {
            ScorpionConsoleReadWrite.ConsoleWrite.writeWarning("Downloading manuals...");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(
            new ProductInfoHeaderValue("Scorpion-IEE", "1"));
            var repo = "Oscar-Tark/Manuals";
            var contentsUrl = $"https://api.github.com/repos/{repo}/contents";
            var contentsJson = await httpClient.GetStringAsync(contentsUrl);
            var contents = (JArray)JsonConvert.DeserializeObject(contentsJson);
            //Types.HANDLE.write_special(contents);
            foreach(var file in contents)
            {
                var downloadUrl = (string)file["download_url"];
                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput($"Downloading: {downloadUrl}");
                DownloadFile((string)file["download_url"], Types.main_user_manuals_path + "/" + (string)file["name"]);
            }
            ScorpionConsoleReadWrite.ConsoleWrite.writeSuccess("Finished downloading manuals");
        }
    }
}
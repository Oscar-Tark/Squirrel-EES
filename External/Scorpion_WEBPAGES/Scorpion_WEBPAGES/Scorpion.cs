using System;
using System.Collections;
using System.IO;

namespace Scorpion_WEBPAGES
{
    public class Scorpion_WEBPAGES
    {
        private string HTTP_DEFAULT_FOLDER = null;
        private string HTTP_DEFAULT_EXTENSIONS = null;
        Scorpion_WEBTOKENS scwtk;

        public Scorpion_WEBPAGES(string http_default_folder, string[] extensions, short token_list_max_size)
        {
            HTTP_DEFAULT_FOLDER = http_default_folder;
            scwtk = new Scorpion_WEBTOKENS(token_list_max_size);
            return;
        }

        public string newtoken()
        {
            return scwtk.newtoken();
        }

        public ArrayList getpages()
        {
            //Gets an iterable array of all available webpages within the folder, which you can assign to a Scorpion.vararray variable
            return new ArrayList(Directory.GetFiles(HTTP_DEFAULT_FOLDER));
        }

        public string getpage(string path)
        {
            return "";
        }
    }

    class Scorpion_WEBTOKENS
    {
        private string[] tokens;

        public Scorpion_WEBTOKENS(short token_list_max_size)
        {
            tokens = new string[token_list_max_size];
        }

        public string newtoken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            int bound = tokens.GetUpperBound(1);
            return token;
        }
    }
}

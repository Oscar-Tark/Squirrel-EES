using System.Collections;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Scorpion
{
    partial class Librarian
    {
        public string jsonfromdictionary(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*Scorpion.Dictionary
            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(MemoryCore.varGet((string)objects[0]), Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(JSON);
            return "["+JSON+"]";
        }

        public IDictionary jsontodictionary(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates variables partaining to the JSON object. a special tag is used in order to chain variables
            //::*jsonvar, *prefix, *key, *contains_key, tag

            Dictionary<string, object> dictObj = null;
            try
            {
                //Gt raw JSON obj as string or as Jobj
                var raw_json = MemoryCore.varGet(objects[0]);
                string s_json = String.Empty;

                if(raw_json.GetType() == typeof(string))
                    s_json = (string)raw_json;//(string)MemoryCore.varGet(objects[0]);
                else
                    s_json = ((JObject)raw_json).ToString();

                //ScorpionConsoleReadWrite.ConsoleWrite.writeDebug("Converting json: ", s_json);

                if(!s_json.Replace(" ", "").StartsWith("[") && !s_json.Replace(" ", "").EndsWith("]"))
                    s_json = "[" + s_json + "]";

                JArray Jarr = JArray.Parse(s_json);
                foreach (JObject Jobj in Jarr)
                    dictObj = Jobj.ToObject<Dictionary<string, object>>();
            }
            catch(Exception e) { Console.WriteLine(e.Message); }

            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return dictObj;
        }

        //POST
        public string curlpost(string Scorp_Line_Exec, ArrayList objects)
        {
            return curlPost(Scorp_Line_Exec, objects).Result;
        }

        private async Task<string> curlPost(string Scorp_Line_Exec, ArrayList objects)
        {
            //Not TESTED!
            //*returns<<::*URL, *headers_dictionary, *content_format, *content

            string response = string.Empty;

            try
            {
                string url          = (string)MemoryCore.varGet(objects[0]);
                Dictionary<string, string> headers = (Dictionary<string, string>)MemoryCore.varGet(objects[1]);
                string content_format = (string)MemoryCore.varGet(objects[2]);
                StringContent string_content = new StringContent((string)MemoryCore.varGet(objects[3]), System.Text.Encoding.UTF8, content_format);

                var URL = new UriBuilder(url);
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                URL.Query = queryString.ToString();

                var client = new HttpClient();

                foreach(KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Running Curl request: ", URL.ToString());
                var post_response = await client.PostAsync(URL.ToString(), string_content);
                response = await post_response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError(e.Message, " : ", e.StackTrace);
            }

            return var_create_return(ref response, true);
        }

        //GET REQUEST
        public string curlget(string Scorp_Line_Exec, ArrayList objects)
        {
            return curlGet(Scorp_Line_Exec, objects).Result;
        }

        private async Task<string> curlGet(string Scorp_Line_Exec, ArrayList objects)
        {
            //*returns<<::*URL, *headers_dictionary, *params_dictionary

            string response = string.Empty;

            try
            {
                string url          = (string)MemoryCore.varGet(objects[0]);
                Dictionary<string, string> headers = (Dictionary<string, string>)MemoryCore.varGet(objects[1]);
                Dictionary<string, string> parameters = (Dictionary<string, string>)MemoryCore.varGet(objects[2]);

                var URL = new UriBuilder(url);
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                foreach(KeyValuePair<string, string> parameter in parameters)
                {
                    queryString[parameter.Key] = parameter.Value;
                }

                URL.Query = queryString.ToString();

                var client = new HttpClient();

                foreach(KeyValuePair<string, string> header in headers)
                {
                    client.DefaultRequestHeaders./*Headers.*/Add(header.Key, header.Value);
                }

                ScorpionConsoleReadWrite.ConsoleWrite.writeOutput("Running Curl request: ", URL.ToString());
                response = await client.GetStringAsync(URL.ToString());
                ScorpionConsoleReadWrite.ConsoleWrite.writeDebug("Curl Response: ", response);
            }
            catch(Exception e)
            {
                ScorpionConsoleReadWrite.ConsoleWrite.writeError(e.Message, " : ", e.StackTrace);
            }

            return response;
        }
    }
}
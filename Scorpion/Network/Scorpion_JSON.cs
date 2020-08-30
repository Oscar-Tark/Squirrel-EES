using System.Collections;
using System.IO;
using System.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Scorpion
{
    partial class Librarian
    {
        public void jsontovar(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates variables partaining to the JSON object. a special tag is used in order to chain variables
            //::*jsonvar, *prefix, *key, *contains_key, tag

            JsonValue jv = JsonValue.Parse((string)var_get(objects[0]));
            string key = (string)var_get(objects[2]);
            string contains_key = (string)var_get(objects[3]);
            string prefix = (string)var_get(objects[1]);

            for (int i = 0; i < jv.Count-1; i++)
            {
                if(jv[i].ContainsKey(key) && jv[i].ContainsKey(contains_key))
                    var_new(jv[i][contains_key], prefix + jv[i][key], "", (string)var_get(objects[4]));
            }
            var_arraylist_dispose(ref objects);
            return;
        }

        public void jsontoarray(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //Creates variables partaining to the JSON object. a special tag is used in order to chain variables
            //::*jsonvar, *prefix, *key, *contains_key, tag

            JsonValue jv = JsonValue.Parse((string)var_get(objects[0]));
            string key = (string)var_get(objects[2]);
            string contains_key = (string)var_get(objects[3]);
            string prefix = (string)var_get(objects[1]);

            ArrayList al_keys = new ArrayList();
            for (int i = 0; i < jv.Count - 1; i++)
            {
                if (jv[i].ContainsKey(key) && jv[i].ContainsKey(contains_key))
                    al_keys.Add(var_get(objects[4]));
                    //var_new(jv[i][contains_key], prefix + jv[i][key], "", (string)var_get(objects[4]));
            }
            var_arraylist_dispose(ref objects);
            return;
        }

        //public void jsonvarfill()

        public void jsonget(string Scorp_Line_Exec, ArrayList objects)
        {
            //GET WITHOUT HEADERS
            //(*"URL", *returnvariable)
            //Since this function uses ASYNC the return value is deliberated in function rather than an actual normal return variable
            Task ts = curlAsync(Scorp_Line_Exec, objects);
            return;
        }

        public void jsongetauth(string Scorp_Line_Exec, ArrayList objects)
        {
            //GET WITH HEADERS
            //(*"URL", *returnvariable)
            //Since this function uses ASYNC the return value is deliberated in function rather than an actual normal return variable
            Task ts = curlauthAsync(Scorp_Line_Exec, objects);
            return;
        }

        private async Task curlauthAsync(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*URL, *ret, *auth_nme, *auth_key, *content
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add((string)objects[2], (string)objects[3]);

            //Workaround objects[1] gets cancelled after hhtp response message
            string ret = (string)objects[1];

            // Create the HttpContent for the form to be posted.
            StringContent requestContent;
            if (objects.Count > 4)
                requestContent = new StringContent((string)var_get(objects[4]));
            //requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("HTTP/1.1", "GET"),});
            else
                requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Get the response.
            HttpResponseMessage response = await client.GetAsync((string)var_get(objects[0]));

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                string ret__ = await reader.ReadToEndAsync();
                ret__ = var_create_return(ref ret__, true);
                varset("", new ArrayList() { ret, ret__ });
            }
            return;
        }

        private async Task curlAsync(string Scorp_Line_Exec, ArrayList objects)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            StringContent requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //Workaround objects[1] gets cancelled after hhtp response message
            string ret = (string)objects[1];

            // Get the response.
            HttpResponseMessage response = await client.GetAsync((string)var_get(objects[0]));

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                string ret__ = await reader.ReadToEndAsync();
                ret__ = var_create_return(ref ret__, true);
                varset("", new ArrayList() { ret, ret__ });
            }
            return;
        }
    }
}
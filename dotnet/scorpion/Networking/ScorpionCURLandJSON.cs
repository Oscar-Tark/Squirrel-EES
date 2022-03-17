using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Scorpion
{
    partial class Librarian
    {
        public string jsonfromdictionary(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*return<<::*Scorpion.Dictionary
            string JSON = Newtonsoft.Json.JsonConvert.SerializeObject(var_get((string)objects[0]), Newtonsoft.Json.Formatting.Indented);
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
                JArray Jarr = JArray.Parse((string)var_get(objects[0]));
                foreach (JObject Jobj in Jarr)
                    dictObj = Jobj.ToObject<Dictionary<string, object>>();
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            var_arraylist_dispose(ref objects);
            var_dispose_internal(ref Scorp_Line_Exec);
            return dictObj;
        }

        public void jsonget(string Scorp_Line_Exec, ArrayList objects)
        {
            //GET WITHOUT HEADERS
            //(*"URL", *returnvariable)
            //Since this function uses ASYNC the return value is deliberated in function rather than an actual normal return variable
            Task ts = curlAsync(Scorp_Line_Exec, objects);
            return;
        }

        public void jsongetauth(string Scorp_Line_Exec, ArrayList objects, bool has_body, string body)
        {
            //GET WITH HEADERS
            //(*"URL", *returnvariable)
            //Since this function uses ASYNC the return value is deliberated in function rather than an actual normal return variable
            Task ts = curlauthAsync(Scorp_Line_Exec, objects, has_body, body);
            return;
        }

        public void jsonpostauth(string Scorp_Line_Exec, ArrayList objects)
        {
            Task ts = curlpostauthAsync(Scorp_Line_Exec, objects);
            return;
        }

        private async Task curlpostauthAsync(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*URL, *ret, *auth_nme, *auth_key, *content

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add((string)objects[2], (string)objects[3]);
            //System.Console.WriteLine((string)objects[4] + ", " + objects[5]);
            //client.DefaultRequestHeaders.Add((string)objects[4], (string)objects[5]);

            /*for (int i = 4; i < objects.Count; i++)
            {
                client.DefaultRequestHeaders.Add((string)objects[i], (string)objects[i + 1]);
                System.Console.WriteLine("Adding {0}", objects[i]);
            }*/

            //Workaround objects[1] gets cancelled after hhtp response message
            string ret = (string)objects[1];

            System.Console.WriteLine((string)var_get(objects[0]));

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

        //GET REQUEST WITH AUTH
        private async Task curlauthAsync(string Scorp_Line_Exec, ArrayList objects, bool has_body, string body)
        {
            //::*URL, *ret, *auth_nme, *auth_key, *content

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add((string)objects[2], (string)objects[3]);

            for (int i = 4; i < objects.Count; i++)
                client.DefaultRequestHeaders.Add((string)objects[i], (string)objects[i+1]);

            //Workaround objects[1] gets cancelled after hhtp response message
            string ret = (string)objects[1];

            // Create the HttpContent for the form to be posted.
            /*StringContent requestContent;
            if (objects.Count > 4)
                requestContent = new StringContent((string)var_get(objects[4]));
            //requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("HTTP/1.1", "GET"),});
            else
                requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");*/

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
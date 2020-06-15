using System.Net.Http;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using Newtonsoft.Json;

//Static Library
namespace Scorpion
{
    partial class Librarian
    {
        public object jsonparse(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            return JsonConvert.DeserializeObject((string)var_get(objects[0]));
        }

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

        /*private static async Task curlpostAsync(string Scorp_Line_Exec, ArrayList objects)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            var url = "repos/symfony/symfony/contributors";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();

            List<Contributor> contributors = JsonConvert.DeserializeObject<List<Contributor>>(resp);
            contributors.ForEach(Console.WriteLine);
        }*/

        private async Task curlauthAsync(string Scorp_Line_Exec, ArrayList objects)
        {
            //::*URL, *ret, *auth_nme, *auth_key
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add((string)objects[2], (string)objects[3]);


            //for (int i = 0; i < objects.Count - 2; i += 2)
            //    client.DefaultRequestHeaders.Add((string)var_get(objects[i]), (string)var_get(objects[i + 1]));
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml,application/json;q=0.9,image/webp,/*;q=0.8"));
            //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            //client.DefaultRequestHeaders.Add("Accept-Language", "keep-alive");
            //client.DefaultRequestHeaders.Add("DNT", "1");
            //client.DefaultRequestHeaders.Add("Host", "api.exchange.bitpanda.com");
            //client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

            // Create the HttpContent for the form to be posted.
            //var requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("HTTP/1.1", "GET"),});
            StringContent requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            // Get the response.
            HttpResponseMessage response = await client.GetAsync((string)var_get(objects[0]));

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
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
                varset("", new ArrayList() { objects[1], ret__ });
            }
            return;
        }
    }
}
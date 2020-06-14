using System.Net.Http;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Json;
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
            //(*"URL", *returnvariable)
            //Since this function uses ASYNC the return value is deliberated in function rather than an actual normal return variable
            Task ts = curlAsync(Scorp_Line_Exec, objects);
            return;
        }

        public void jsonpost(string Scorp_Line_Exec, ArrayList objects)
        {
            //(*"URL", *returnvariable)
            //Since this function uses ASYNC the return value is deliberated in function rather than an actual normal return variable
            Task ts = curlAsync(Scorp_Line_Exec, objects);
            return;
        }

        /*private async Task curlpostAsync(string Scorp_Line_Exec, ArrayList objects)
{
    var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Clear();
    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml,application/json;q=0.9,image/webp,/*;q=0.8"));
    //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
    //client.DefaultRequestHeaders.Add("Accept-Language", "keep-alive");
    //client.DefaultRequestHeaders.Add("DNT", "1");
    //client.DefaultRequestHeaders.Add("Host", "api.exchange.bitpanda.com");
    //client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");

    // Create the HttpContent for the form to be posted.
    //var requestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("HTTP/1.1", "GET"),});
    /*StringContent requestContent = new StringContent("");
    requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


    // Get the response.
    HttpResponseMessage response = await client.PostAsync((string)var_get((string)objects[0]), requestContent);

    // Get the response content.
    HttpContent responseContent = response.Content;

    // Get the stream of the content.
    using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
    {
        // Write the output.
        Console.WriteLine(await reader.ReadToEndAsync());
    }
    return;
}*/

        private async Task curlAsync(string Scorp_Line_Exec, ArrayList objects)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            StringContent requestContent = new StringContent("");
            requestContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            // Get the response.
            HttpResponseMessage response = await client.GetAsync((string)var_get((string)objects[0]));

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
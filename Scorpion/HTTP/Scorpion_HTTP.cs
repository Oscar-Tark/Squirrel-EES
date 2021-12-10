using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Scorpion
{
    public partial class Librarian
    {
        public void httpstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            //*local_ip, *jsframework, *db
            //Check if the Database exists
            if (Do_on.vds.checkLoaded((string)var_get(objects[2])))
                Do_on.http.startServer((string)var_get(objects[0]) == "" ? null : (string)var_get(objects[0]), (string)var_get(objects[1]), (string)var_get(objects[2]));
            else
                Do_on.write_error("The database '" + (string)var_get(objects[2]) + "' is not loaded");
            var_dispose_internal(ref Scorp_Line_Exec);
            var_arraylist_dispose(ref objects);
            return;
        }
    }
}

namespace ScorpionHTTPServer
{
    public class HTTPServer
    {
        private static HttpListener scorpion_http_listener;

        private static string local_url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        static Scorpion.Scorp HANDLE;
        private static string js_framework = null;
        private static string database = null;

        public HTTPServer(string prefix, Scorpion.Scorp Do_on)
        {
            HANDLE = Do_on;
            return;
        }

        public bool startServer(string prefix, string js_framework_, string database_)
        {
            Console.WriteLine("checked");
            if (!HttpListener.IsSupported && HTTPElements.HTTPElements.js_framework_includers[js_framework_] != null)
                return false;

            scorpion_http_listener = new HttpListener();
            js_framework = js_framework_;
            database = database_;

            //Adds the default prefix is none is given in the arguments when starting the application and starts the HTTP server
            scorpion_http_listener.Prefixes.Add(prefix = (prefix == null ? local_url : prefix));
            scorpion_http_listener.Start();
            Console.WriteLine("HTTP server started with prefix: {0}", prefix);

            //Handle incomming connections
            Task listen_task = handleIncomingConnections();
            listen_task.GetAwaiter().GetResult();
            Console.WriteLine("Awaiting connections...");

            //Stop the HTTP listener and close it
            scorpion_http_listener.Stop();
            scorpion_http_listener.Close();
            Console.WriteLine("HTTP server stopped");
            return true;
        }

        public static async Task handleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await scorpion_http_listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine("Refferer: {0}", req.Url);
                Console.WriteLine("HTTP method: {0}", req.HttpMethod);
                Console.WriteLine("User host name: {0}", req.UserHostName);
                Console.WriteLine("User agent: {0}", req.UserAgent);
                Console.WriteLine("Local end point: {0}", req.LocalEndPoint.Address);
                Console.WriteLine("Remote end point: {0}", req.RemoteEndPoint.Address);
                Console.WriteLine("User host address: {0}", req.UserHostAddress);
                Console.WriteLine("Raw URL: {0}", req.RawUrl[0]);
                Console.WriteLine("Database: {0}", database);
                Console.WriteLine("----------------------------");

                string[] accept = req.AcceptTypes;

                foreach (string acc in accept)
                    Console.WriteLine("Accept type: {0}", acc);

                string[] URL_elements = getPathElements(req);

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/input"))
                {
                    Console.WriteLine("Input requested");
                    string text = null;
                    using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
                    {
                        text = reader.ReadToEnd();
                    }
                    Console.WriteLine("POST Data: {0}", text);
                    Scorpion.NetworkEngineFunctions nef__ = new Scorpion.NetworkEngineFunctions();
                    nef__.replace_api(text);

                    //Execute data operations, data can only enter as JSON
                    
                }
                else
                {
                    // Make sure we don't increment the page views counter if `favicon.ico` is requested
                    if (req.Url.AbsolutePath != "/favicon.ico")
                        pageViews += 1;
                    else continue;

                    // Write the response info
                    string disableSubmit = !runServer ? "disabled" : "";
                    byte[] data = null;

                    if (req.Url.AbsolutePath != "/")
                    {
                        Console.WriteLine("Fetching Page {0}", URL_elements[1]);

                        //Get from db
                        ArrayList structure_result = HANDLE.vds.Data_doDB_selective_no_thread(database, "", URL_elements[1], "structure", HANDLE.vds.OPCODE_GET);
                        ArrayList logic_result = HANDLE.vds.Data_doDB_selective_no_thread(database, "", URL_elements[1], "logic", HANDLE.vds.OPCODE_GET);
                        ArrayList css_result = HANDLE.vds.Data_doDB_selective_no_thread(database, "", URL_elements[1], "visuals", HANDLE.vds.OPCODE_GET);

                        //Load response strings
                        string structure = structure_result.Count == 0 ? HTTPElements.HTTPElements.errorPageData : (string)structure_result[0];
                        string logic = (logic_result.Count == 0 ? "" : "<script>" + (string)logic_result[0] + "</script>");
                        string css = (css_result.Count == 0 ? "" : "<style>" + (string)css_result[0] + "</style>");

                        //Encode response
                        //Elaborate response type:
                        if (Array.IndexOf(accept, HTTPElements.HTTPElements.accept_html) > -1)
                            data = Encoding.UTF8.GetBytes(string.Format(HTTPElements.HTTPElements.developmentFormatData, structure, css, logic, HTTPElements.HTTPElements.js_framework_includers[js_framework]));
                        else if (Array.IndexOf(accept, HTTPElements.HTTPElements.accept_json) > -1)
                        {
                            Console.WriteLine("Response as JSON");
                            data = Encoding.UTF8.GetBytes(string.Format("['structure' : {0}, 'css' : {1}, 'logic' : {2}, 'js_framework' : {3}]", structure, css, logic, HTTPElements.HTTPElements.js_framework_includers[js_framework]));
                        }
                    }

                    if (data != null)
                    {
                        resp.ContentType = "text/html";
                        resp.ContentEncoding = Encoding.UTF8;
                        resp.ContentLength64 = data.LongLength;
                        // Write out to the response stream (asynchronously), then close it
                        await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    }
                }
                resp.Close();
            }
        }

        public static string[] getPathElements(HttpListenerRequest request)
        {
            //This function parses the URL for the needed specific elements
            //URL format: /page:db/data:tag/data:subtag
            if (request.Url.AbsolutePath == "/")
                return new string[] { "/" }; 
            return request.Url.AbsolutePath.Split('/');
        }

        public void stopServer()
        {
            scorpion_http_listener.Stop();
            scorpion_http_listener.Close();
            Console.WriteLine("HTTP server stopped");
            return;
        }
    }
}

namespace HTTPElements
{
    public static class HTTPElements
    {
        public static string accept_html = "text/html";
        public static string accept_json = "application/json";

        public static string pageData =
             "<!DOCTYPE>" +
             "<html>" +
             "  <head>" +
             "    <title>HttpListener Example</title>" +
             "  </head>" +
             "  <body>" +
             "    <p>Page Views: {0}</p>" +
             "    <form method=\"post\" action=\"shutdown\">" +
             "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
             "    </form>" +
             "  </body>" +
             "</html>";
        public static string errorPageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>Error</title>" +
            "  </head>" +
            "  <body>" +
            "    <p><h1>500 Internal server error</h1><br><hr><br>Incorrect response given</p>" +
            "  </body>" +
            "</html>";

        public static string developmentFormatData =
            "<!DOCTYPE html><html><head>{3}</head></script><body>{0} {1} {2}</body></html>";

        public static string productionFormatData =
            "<!DOCTYPE html><html><head>{3}</script></head><body>{0} {1} {2}</body></html>";

        //private List<string> js_frameworks = new List<string> = {};
        public static Dictionary<string, string> js_framework_includers = new Dictionary<string, string> {
            { "vue.js_development", "<script src='https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js'>" },
            { "vue.js", "<script src='https://cdn.jsdelivr.net/npm/vue@2'>" }
        };
    }
}
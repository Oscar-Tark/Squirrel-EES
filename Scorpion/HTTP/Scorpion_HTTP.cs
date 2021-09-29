using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Scorpion
{
    public partial class Librarian
    {
        public void httpstart(ref string Scorp_Line_Exec, ref ArrayList objects)
        {
            Do_on.http.startServer(null);
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
        public static string url = "http://localhost:8000/"; private string current_prefix = null;
        public static int pageViews = 0;
        public static int requestCount = 0;
        static Scorpion.Scorp HANDLE;

        public HTTPServer(string prefix, Scorpion.Scorp Do_on)
        {
            HANDLE = Do_on;
            //Start the HTTP server
            //if (!startServer(prefix == "null" ? null : prefix))
            //    Console.WriteLine("Unable to start the HTTP server as the HTTPListener module is not supported by your system.You may try configuring your server and try running the HTTP server again\n\nexiting server...");
            return;
        }

        public bool startServer(string prefix)
        {
            if (!HttpListener.IsSupported)
                return false;
            scorpion_http_listener = new HttpListener();

            //Adds the default prefix is none is given in the arguments when starting the application and starts the HTTP server
            scorpion_http_listener.Prefixes.Add(prefix = (prefix == null ? url : prefix));
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
                Console.WriteLine(req.Url);
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine(req.RawUrl[0]);

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/input"))
                {
                    //Console.WriteLine("Shutdown requested");
                    //runServer = false;
                }

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = null;
                string[] URL_elements = getPathElements(req);

                if (req.Url.AbsolutePath != "/")
                {
                    Console.WriteLine("Fetching {0} : Page {1}", URL_elements[1], URL_elements[2]);
                    //Get from db
                    ArrayList structure_result = HANDLE.vds.Data_doDB_selective_no_thread(URL_elements[1], "", URL_elements[2], "structure", HANDLE.vds.OPCODE_GET);
                    ArrayList logic_result = HANDLE.vds.Data_doDB_selective_no_thread(URL_elements[1], "", URL_elements[2], "logic", HANDLE.vds.OPCODE_GET);
                    ArrayList css_result = HANDLE.vds.Data_doDB_selective_no_thread(URL_elements[1], "", URL_elements[2], "visuals", HANDLE.vds.OPCODE_GET);

                    //Load response strings
                    string structure = (structure_result.Count == 0 ? StaticElements.StaticElements.errorPageData : (string)structure_result[0]);
                    string logic = (logic_result.Count == 0 ? "" : "<script>" + (string)logic_result[0] + "</script>");
                    string css = (css_result.Count == 0 ? "" : "<style>" + (string)css_result[0] + "</style>");

                    //Encode response
                    data = Encoding.UTF8.GetBytes(string.Format(StaticElements.StaticElements.developmentFormatData, structure, css, logic));
                }

                if (data != null)
                {
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;
                    // Write out to the response stream (asynchronously), then close it
                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
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
            else
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

namespace StaticElements
{
    public static class StaticElements
    {
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
            "<!DOCTYPE html><html><script src='https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js'></script><body>{0} {1} {2}</body></html>";

        public static string productionFormatData =
            "<!DOCTYPE html><html><script src='https://cdn.jsdelivr.net/npm/vue@2'></script><body>{0} {1} {2}</body></html>";
    }
}
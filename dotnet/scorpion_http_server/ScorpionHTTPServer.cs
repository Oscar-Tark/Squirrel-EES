using System;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using ScorpionConsoleReadWrite;
using Scorpion_MDB;

namespace ScorpionHTTPServer
{
    partial class HTTPServer
    {
        //Objects defined in this section
        private static HttpListener scorpion_http_listener;
        private static ScorpionHttpSessions.ScorpionHttpSessions scorpion_sessions;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        private static string DB = null;
        internal ScorpionMicroDB vds;
    }

    class Data
    {
        public string data = "\0";
    }

    partial class HTTPServer
    {
        public HTTPServer(string prefix, string scorpion_host, int scorpion_port, string scorpion_db)
        {
            //Start the scorpion driver in order to get data from MicroDB

            scorpion_sessions = new ScorpionHttpSessions.ScorpionHttpSessions(scorpion_host, scorpion_port);
            DB = scorpion_db;

            //Start the HTTP server
            if(!startServer(prefix == "" ? null : prefix))
                ConsoleWrite.writeError("Unable to start the HTTP server as the HTTPListener module is not supported by your system.You may try configuring your server and try running the HTTP server again\n\nexiting server...");
            return;
        }

        public bool startServer(string prefix)
        {
            if(!HttpListener.IsSupported)
                return false;
            scorpion_http_listener = new HttpListener();

            //Adds the default prefix is none is given in the arguments when starting the application and starts the HTTP server
            scorpion_http_listener.Prefixes.Add(prefix = (prefix == null ? url : prefix));
            scorpion_http_listener.Start();
            ConsoleWrite.writeSuccess(string.Format("HTTP server started with prefix: {0}", prefix));

            //Handle incomming connections
            Serve();
            ConsoleWrite.writeOutput("Awaiting connections...");

            //Stop the HTTP listener and close it
            scorpion_http_listener.Stop();
            scorpion_http_listener.Close();
            ConsoleWrite.writeOutput("HTTP server stopped");
            return true;
        }

        public static void Serve()
        {
            //URL FORMAT GET: /project/page/hash/
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection. Peel out the requests and response objects
                HttpListenerContext ctx = scorpion_http_listener.GetContext();
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                Thread ths = new Thread(new ParameterizedThreadStart(serveThreaded));
                ths.IsBackground = true;
                ths.Start(new object[3]{ ctx, req, resp });
            }
            return;
        }

        private static void serveThreaded(object obj_pass)
        {
            string session = "";
            bool is_script = false, is_css = false;

            HttpListenerContext ctx     = (HttpListenerContext)((object[])obj_pass)[0];
            HttpListenerRequest req     = (HttpListenerRequest)((object[])obj_pass)[1];
            HttpListenerResponse resp   = (HttpListenerResponse)((object[])obj_pass)[2];

            //Create a workable string out of the url seperating elements withing '/'
            string[] URL_elements = getPathElements(req);

            //Get request accept types
            List<string> accept_types = getAcceptTypes(req.AcceptTypes);

            //Write information abour request to the console
            showRequestInfo(ref req, is_script, is_css);

            //Post allows for posting data to the server: Accepts JSON
            if ((req.HttpMethod.Equals("POST", StringComparison.CurrentCultureIgnoreCase)))
            {
                ConsoleWrite.writeSpecial("--Input request-->");

                //For JSON Data
                if(containsAcceptType(accept_types, StaticElements.StaticElements.accept_type_json))
                {
                    ConsoleWrite.writeOutput("Accept Type: JSON-->");

                    //You can use CURL to debug: curl -v -X POST --header "Content-Type: application/json" -d "{\"data\":\"json\"}" http://localhost:8000/
                    string str = new StreamReader(req.InputStream).ReadToEnd();
                    Data d = Newtonsoft.Json.JsonConvert.DeserializeObject<Data>(str);
                    ConsoleWrite.writeDebug(d.data);
                }

                //Close the connection
                resp.Close(/*see params*/);
                return;
            }

            //If request ends with .js start script mode
            is_script = isScript(req.Url.AbsolutePath);
            is_css = isCSS(req.Url.AbsolutePath);

            if(is_script && is_css)
            {
                ConsoleWrite.writeWarning("Request object contains both '.js' and '.css', ignoring request");
                return;
            }

            //Make sure we don't increment the page views counter if `favicon.ico` is requested. Ignore favico for now
            if (req.Url.AbsolutePath != "/favicon.ico")
                pageViews += 1;
            else
                return;

            //Response vars
            //string disableSubmit = !runServer ? "disabled" : "";
            byte[] data = null; object[] checks = null;

            /*Check for any errors including session errors or no response form XMLDB
            on:
            noerror: set the session variable
            error: respond with the appropriate error page
            */
            if(!(bool)(checks = checkErrors(URL_elements, is_script, is_css))[0])
            {
                writeResponse((byte[])checks[1], resp, false, false);
                return;
            }
            else
            {
                //[session:string][isnewsession:bool]
                session = (string)((object[])checks[2])[0];
                //If a new session, redirect to correct URL
                if((bool)((object[])checks[2])[1])
                    resp.Redirect(req.Url.OriginalString + (req.Url.OriginalString.EndsWith("/") ? session : ("/" + session)));
            }
                
            //Page GET request: /Project/Page/Hash
            if(req.Url.AbsolutePath != "/")
            {
                ConsoleWrite.writeSpecial("--Page/script request-->");

                //Get page from DB
                string request_elements = "";//SD.get(DB, URL_elements[0], URL_elements[1], session);

                if(request_elements == null)
                {
                    ConsoleWrite.writeWarning("Incorrect Scorpion IEE response given, sending error page");
                    data = Encoding.UTF8.GetBytes(StaticElements.StaticElements.kerror_page_data);
                }
                else
                {
                    if(!is_script && !is_css)
                        data = Encoding.UTF8.GetBytes(string.Format(StaticElements.StaticElements.kdevelopment_format_data,
                        (request_elements == null ? "" : request_elements),
                        StaticElements.StaticElements.default_js,
                        String.Format(StaticElements.StaticElements.js_session, session, req.Url)));
                    else
                        data = Encoding.UTF8.GetBytes((request_elements == null ? "" : request_elements));
                    }
                }

                //Write a successful page or script response
                writeResponse(data, resp, is_script, is_css);

                //Reset is_script
                is_script = false;
        }

        private static object[] checkSession(ref string[] URL_elements, bool is_script, bool is_css)
        {
            //Check if a user token was passed if not apply a new one. If it was passed verify it
            if(URL_elements.Length < 3 && !is_script && !is_css)
                return new object[2]{scorpion_sessions.newSession(URL_elements[0]), true};
            if(URL_elements.Length >= 3)
                return new object[2]{scorpion_sessions.verifySession(URL_elements[2]) == true ? URL_elements[2] : null, false};
            return null;
        }

        public static /*async Task*/void writeResponse(byte[] data, HttpListenerResponse resp, bool script, bool css)
        {
            if(data != null)
            {
                resp.ContentType = (!script && !css ? "text/html" : script ? "text/javascript" : "text/css");
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                //Add content security policy information to the response header
                resp.AppendHeader("Content-Security-Policy", "default-src http://localhost:8000");

                //Write out to the response stream (asynchronously), then close it
                resp.OutputStream.WriteAsync(data, 0, data.Length);
            }
            resp.Close();
        }

        public static string[] getPathElements(HttpListenerRequest request)
        {
            //This function parses the URL for the needed specific elements
            //URL format: /page:db/data:tag/data:subtag
            if(request.Url.AbsolutePath == "/")
                return new string[] { "/" };
            else
                return request.Url.AbsolutePath.Replace("../", "").Replace("~", "").Replace("~/", "").Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        private static void showRequestInfo(ref HttpListenerRequest req, bool is_script, bool is_css)
        {
            // Print out some info about the request
            ++requestCount;
            ConsoleWrite.writeOutput($"Request #: {requestCount}");
            ConsoleWrite.writeOutput(req.Url.ToString());
            ConsoleWrite.writeOutput(req.HttpMethod);
            ConsoleWrite.writeOutput(req.UserHostName);
            ConsoleWrite.writeOutput(req.UserAgent);
            ConsoleWrite.writeOutput(req.RawUrl[0]);
            ConsoleWrite.writeOutput($"Absolute path {req.Url.AbsolutePath}");
            ConsoleWrite.writeOutput($"Is script: {is_script}");
            ConsoleWrite.writeOutput($"Is css: {is_css}");
            return;
        }

        private static List<string> getAcceptTypes(string[] accept_types)
        {
            List<string> accept_types_processed = new List<string>();
            accept_types_processed.AddRange(accept_types);
            return accept_types_processed;
        }

        private static bool containsAcceptType(List<string> accept_types, string type)
        {
            return accept_types.Contains(type);
        }

        private static bool isScript(string absolute_path)
        {
            return absolute_path.Contains(".js", StringComparison.CurrentCultureIgnoreCase);
        }

        private static bool isCSS(string absolute_path)
        {
            return absolute_path.Contains(".css", StringComparison.CurrentCultureIgnoreCase);
        }

        private static object[] checkErrors(string[] URL_elements, bool is_script, bool is_css)
        {
            //returns (bool, data, session)
            byte[] data = null; object[] session = null;
            bool ok = true;

            if(URL_elements.Length < 1)
            {
                data = Encoding.UTF8.GetBytes(@StaticElements.StaticElements.kurl_error_page_data);
                ok = false;
            }

            //If elements ok, check session
            else if((session = checkSession(ref URL_elements, is_script, is_css))[0] == null)
            {
                data = Encoding.UTF8.GetBytes(@StaticElements.StaticElements.kerror_session_page_data);
                ok = false;
                ConsoleWrite.writeError($"Inexistent session {URL_elements[2]}");
            }
            return new object[3] { ok, data, session };
        }
    }
}
using System.IO;

namespace StaticElements
{
    public static class StaticElements
    {
        public static string default_js = 
        File.ReadAllText("./Data/DefaultAjax.js");

        public static string js_forms =
        File.ReadAllText("./Data/JSForms.js");

        public static string js_session = 
        "const token='{0}'; const project='{1}'";

        public static readonly string accept_type_json = "application/json";

        public static readonly string kerror_page_data = 
            @"<!DOCTYPE html>" +
            "<html>" +
            "  <head>" +
            "<meta charset='UTF-8'>" +
            "    <title>204 No content</title>" +
            "  </head>" +
            "  <body>" +
            "    <p><h1>:( 204 No content</h1><br><hr><br>Type: Data<br>Code: 204<br>Message: Incorrect response given. The server responded with no recognizable data.</p>" +
            "  </body>" +
            "</html>";
        public static readonly string kerror_session_page_data = 
            @"<!DOCTYPE html>" +
            "<html>" +
            "  <head>" +
            "<meta charset='UTF-8'>" +
            "    <title>202 Session error</title>" +
            "  </head>" +
            "  <body>" +
            "    <p><h1>:o 202 Session error</h1><br><hr><br>Type: Session<br>Code: 202<br>Message: Incorrect session given. The server responded with no recognizable session.</p>" +
            "  </body>" +
            "</html>";
        public static readonly string kurl_error_page_data = 
            @"<!DOCTYPE html>" +
            "<html>" +
            "  <head>" +
            "<meta charset='UTF-8'>" +
            "    <title>500 request error</title>" +
            "  </head>" +
            "  <body>" +
            "    <p><h1>:( 500 Internal request error</h1><br><hr><br>Type: Request<br>Code: 500<br>Message: URL parameters incorrect. 2 parameters expected, less given.</p>" +
            "  </body>" +
            "</html>";

        public static readonly string kredirect_new_session = "<meta http-equiv=\"Refresh\" content=\"0; url='{0}'\"/>";

        public static string kdevelopment_format_data =
            "<!DOCTYPE html><html><script>{1}</script><script>{2}</script><head><meta charset='UTF-8'></head><body>{0}</body></html>";

        public static string kproduction_format_data = 
            "<!DOCTYPE html><html><script>{1}</script><script>{2}</script><head><meta charset='UTF-8'></head><body>{0}</body></html>";
    }
}
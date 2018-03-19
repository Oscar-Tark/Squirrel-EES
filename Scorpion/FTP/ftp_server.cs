using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorpion.FTP
{
    public class ftp_server
    {
        Scorpion.Form1 Do_on;
        public ftp_server(Scorpion.Form1 fm1)
        {
            Do_on = fm1;
            return;
        }

        public void auth_table(ref string Scorp_Line)
        {
            //Contained in auth_db
            //(*name,*uname,*pwd,*ftpip,*port)""
            Do_on.readr.access_library(Do_on.AL_ACC_SUP[4] + Do_on.AL_ACC[2].ToString() + Do_on.AL_FNC_SCRP[94] + Do_on.AL_ACC[3].ToString() + Do_on.AL_ACC[1] + "\"auth_tbl\"" + Do_on.AL_ACC[4].ToString());

            return;
        }

        public void download()
        {
            //(*url,*uname,*pwd)
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("URL");
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("","");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responsestream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responsestream);

            string result = reader.ReadToEnd();

            return;
        }

        public void upload()
        {
            //(*url,*uname,*pwd)
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("URL");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            //creds try using securestring
            request.Credentials = new NetworkCredential("","");

            //copy to new file
            StreamReader sourcestream = new StreamReader("file");
            byte[] filecontents = Encoding.UTF8.GetBytes(sourcestream.ReadToEnd());
            sourcestream.Close();

            request.ContentLength = filecontents.Length;

            Stream requeststream = request.GetRequestStream();
            requeststream.Write(filecontents, 0, filecontents.Length);
            requeststream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Do_on.write_to_cui("Uploaded");

            response.Close();

            return;
        }
    }
}
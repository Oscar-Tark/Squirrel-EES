using System;
using System.IO;

namespace Scorpion
{
    public static class ScorpionFilesOperationsCore
    {
        public static string readFile(string path)
        {
            FileStream fd = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fd, System.Text.Encoding.UTF8);
            string s_read = sr.ReadToEnd();
            sr.Close();
            fd.Close();

            path = null;
            return s_read;
        }
    }
}
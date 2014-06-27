using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;

namespace UVA_Arena.Structures
{
    class Internet
    {
        private static int inqueue = 0;

        private static string GetDownloadString(string url, int time = 3000, string method = "GET")
        {
            try
            {
                ++inqueue;
                HttpWebRequest web_req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                web_req.Timeout = time;
                web_req.Method = method;
                WebResponse respns = web_req.GetResponse();
                Stream stream = respns.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                String data = reader.ReadToEnd();
                --inqueue;
                return data;
            }
            catch (Exception ex)
            {
                --inqueue;
                throw ex;
            }
        }

        public static string DownloadString(string url, int time = 3000, string method = "GET")
        {
            while (inqueue >= 2) System.Threading.Thread.Sleep(300);
            return GetDownloadString(url, time, method);
        }

        public static string DownloadUserid(string username)
        {
            string url = "http://uhunt.felix-halim.net/api/uname2uid/" + username;
            return DownloadString(url);
        }

        public static string DownloadProblemDatabase()
        {
            string url = "http://uhunt.felix-halim.net/api/p";
            return DownloadString(url);
        }
    }
}

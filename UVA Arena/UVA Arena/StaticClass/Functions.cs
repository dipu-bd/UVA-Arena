using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using UVA_Arena.Internet;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace UVA_Arena
{
    internal sealed class Functions
    {
        #region Formatter Functions

        public static string FormatRuntime(long run)
        {
            return String.Format("{0:0.000}s", (run / 1000.0));
        }

        public static string FormatMemory(long mem)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB" };

            int ind = 0;
            double res = mem;
            while (res >= 1024.0)
            {
                res /= 1024.0;
                ind++;
            }

            return String.Format("{0:0.00}{1}", res, suf[ind]);
        }

        public static string FormatTimeSpan(long span)
        {
            if (span < 120) return (span).ToString() + " secs ";

            long year, mon, day, hour, min;

            year = span / 31536000;
            span -= 31536000 * year;

            mon = span / 2592000;
            span -= 2592000 * mon;

            day = span / 86400;
            span -= 86400 * day;

            hour = span / 3600;
            span -= 3600 * hour;

            min = span / 60;

            string txt = "";
            if (year > 0) txt += year.ToString() + "year ";
            if (mon > 0) txt += mon.ToString() + "month ";
            if (day > 0) txt += day.ToString() + "day ";
            if (hour > 0) txt += hour.ToString() + "hour ";
            txt += min.ToString() + "min ";

            return txt;
        }

        #endregion

        #region HTML File Parser

        /// <summary>
        /// Parse HTML file and find all local contents to download.
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <param name="replace">True, if you want to replace old files.</param>
        /// <returns></returns>
        public static List<DownloadTask> ProcessHtmlContent(long pnum, bool replace)
        {
            try
            {
                string external = string.Format("http://uva.onlinejudge.org/external/{0}/", pnum / 100);

                string filepath = LocalDirectory.GetProblemHtml(pnum);
                if (!File.Exists(filepath)) return new List<DownloadTask>();

                string html = File.ReadAllText(filepath);
                List<string> urls = new List<string>();
                List<DownloadTask> tasks = new List<DownloadTask>();

                HtmlAgilityPack.HtmlDocument htdoc = new HtmlAgilityPack.HtmlDocument();
                htdoc.Load(filepath);
                DFS(htdoc.DocumentNode, urls);
                htdoc.Save(filepath);

                foreach (string str in urls)
                {
                    string url = str.StartsWith("./") ? str.Remove(0, 2) : str;
                    while (url.StartsWith("/")) url = url.Remove(0, 1);
                    string file = url.Replace('/', Path.DirectorySeparatorChar);
                    file = LocalDirectory.GetProblemContent(pnum, file);                    
                    if (replace || LocalDirectory.GetFileSize(file) < 10)
                    {
                        tasks.Add(new DownloadTask(external + url, file, pnum));
                    }
                }

                urls.Clear();
                return tasks;
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Internet");
                return new List<DownloadTask>();
            }
        }
         

        private static void DFS(HtmlAgilityPack.HtmlNode nod, List<string> urls)
        {
            //process current node
            string name = null;
            if (nod.Name == "img") name = "src";
            if (nod.Name == "link") name = "href";
            if (name != null)
            {
                string url = nod.Attributes[name].Value;
                if (!(url.StartsWith("http:") || url.StartsWith("ftp:") || url.StartsWith("https:")))
                {
                    if (!urls.Contains(url)) urls.Add(url);
                    if (url.StartsWith("./")) url = url.Remove(0, 2);
                    while (url.StartsWith("/")) url = url.Remove(0, 1);
                    url = url.Replace('/', Path.DirectorySeparatorChar);
                    nod.Attributes[name].Value = url;
                }
            }

            //search child nodes
            foreach (var child in nod.ChildNodes)
            {
                DFS(child, urls);
            }

            //remove unnecessary data
            if (nod.NodeType == HtmlAgilityPack.HtmlNodeType.Comment)
            {
                nod.Attributes.RemoveAll();
                nod.Name = "div";
            }
        }

        #endregion

        #region Backup and Restore

        public static void BackupData()
        {
            string file = @"unzip\unzip.exe";
            if (!System.IO.File.Exists(file)) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Problems.uapak";
            sfd.Filter = "UVA Arena Package | *.uapak";
            sfd.DefaultExt = ".uapak";
            sfd.AddExtension = true;
            sfd.CheckPathExists = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(Backup, sfd.FileName);
            }

            sfd.Dispose();
        }

        public static void RestoreData()
        {
            string file = @"unzip\zipit.exe";
            if (!System.IO.File.Exists(file)) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "Problems.uapak";
            ofd.Filter = "UVA Arena Package | *.uapak";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(Restore, ofd.FileName);
            }

            ofd.Dispose();

        }

        private static void Backup(object state)
        {
            string zipit = @"unzip\zipit.exe";
            string file = (string)state;

            //get reg
            string path = LocalDirectory.DefaultPath;
            string data = BackupRegistryData();
            string regfile = Path.Combine(path, "regkey.dat");
            File.WriteAllText(regfile, data);

            //save data
            string arg = string.Format("\"{0}\" \"{1}\"", file, path);
            System.Diagnostics.Process.Start(zipit, arg).WaitForExit();
        }

        private static void Restore(object state)
        {
            string unzip = @"unzip\unzip.exe";
            string file = (string)state;

            //delete old
            string path = LocalDirectory.DefaultPath;
            System.IO.Directory.Delete(path, true);

            //restore all
            string arg = string.Format("-o -d \"{0}\" \"{1}\"", path, file);
            System.Diagnostics.Process.Start(unzip, arg).WaitForExit();

            //restore reg
            string regfile = Path.Combine(path, "regkey.dat");
            string regdat = File.ReadAllText(regfile);
            RestoreRegistryData(regdat);
        }

        private static string BackupRegistryData(RegistryKey key = null)
        {
            if (key == null) key = RegistryAccess.DEFAULT;

            //get values
            List<string> values = new List<string>();
            foreach (string sub in key.GetValueNames())
            {
                values.Add(JsonConvert.SerializeObject(new string[] { sub, 
                    JsonConvert.SerializeObject(key.GetValue(sub)), 
                    ((int)key.GetValueKind(sub)).ToString() }));
            }

            //get keys
            List<string> keydat = new List<string>();
            foreach (string sub in key.GetSubKeyNames())
            {
                string v = BackupRegistryData(key.OpenSubKey(sub));
                if (!string.IsNullOrEmpty(v)) keydat.Add(JsonConvert.SerializeObject(new string[] { sub, v }));
            }

            string val1 = JsonConvert.SerializeObject(values);
            string val2 = JsonConvert.SerializeObject(keydat);
            string[] fdat = new string[] { val1, val2 };
            return JsonConvert.SerializeObject(fdat);
        }

        private static void RestoreRegistryData(string data, RegistryKey key = null)
        {
            if (key == null) key = RegistryAccess.DEFAULT;

            string[] fdat = JsonConvert.DeserializeObject<string[]>(data);
            if (fdat == null) return;

            List<string> values = JsonConvert.DeserializeObject<List<string>>(fdat[0]);
            List<string> keydat = JsonConvert.DeserializeObject<List<string>>(fdat[1]);

            foreach (string dat in values)
            {
                string[] val = JsonConvert.DeserializeObject<string[]>(dat);
                key.SetValue((string)val[0],
                    JsonConvert.DeserializeObject((string)val[1]),
                    (RegistryValueKind)int.Parse(val[2]));
            }

            foreach (string dat in keydat)
            {
                string[] val = JsonConvert.DeserializeObject<string[]>(dat);
                RegistryKey sub = key.CreateSubKey(val[0]);
                RestoreRegistryData(val[1], sub);
            }
        }

        #endregion
    }
}

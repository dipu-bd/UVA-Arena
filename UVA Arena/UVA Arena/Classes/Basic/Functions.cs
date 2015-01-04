using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UVA_Arena.Internet;

namespace UVA_Arena
{
    internal static class Functions
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

        public static string FormatTimeSpan(TimeSpan span)
        {
            int year = (int)(span.TotalDays / 365);
            int month = (span.Days - year * 365) / 30;
            int days = span.Days - year * 365 - month * 30;

            string txt = "";
            bool space = false;

            if (year > 0)
            {
                space = true;
                txt += string.Format("{0} Year", year);
                if (year > 1) txt += "s"; //plural
            }
            if (month > 0)
            {
                if (space) txt += " "; space = true;
                txt += string.Format(" {0} Month", month);
                if (month > 1) txt += "s"; //plural
            }
            if (span.TotalDays < 30)
            {
                if (days > 0)
                {
                    if (space) txt += " "; space = true;
                    txt += string.Format("{0} Day", days);
                    if (days > 1) txt += "s"; //plural
                }
            }
            if (span.TotalDays < 1)
            {
                if (span.Hours > 0)
                {
                    if (space) txt += " "; space = true;
                    txt += string.Format("{0} Hour", span.Hours);
                    if (span.Hours > 1) txt += "s"; //plural
                }
                if (span.Minutes > 0)
                {
                    if (space) txt += " "; space = true;
                    txt += string.Format("{0} Minute", span.Minutes);
                    if (span.Minutes > 1) txt += "s"; //plural
                }
            }

            if (span.TotalMinutes < 1)
            {
                if (space) txt += " "; space = true;
                txt += string.Format("{0} Second", span.TotalSeconds);
                if (span.Seconds > 1) txt += "s"; //plural
            }

            return txt;
        }

        public static string FormatTimeSpan(long span)
        {
            return FormatTimeSpan(new TimeSpan(span * 10000000));
        }

        #endregion

        #region Color and String getter

        public static string GetLanguage(Structures.Language lan)
        {
            switch (lan)
            {
                case Structures.Language.C: return "Ansi C";
                case Structures.Language.CPP: return "C++";
                case Structures.Language.CPP11: return "C++11";
                case Structures.Language.Java: return "Java";
                case Structures.Language.Pascal: return "Pascal";
                default: return "Unknown";
            }
        }

        public static string GetVerdict(Structures.Verdict ver)
        {
            switch (ver)
            {
                case Structures.Verdict.SubError: return "Submission Error";
                case Structures.Verdict.CannotBeJudge: return "Can't Be Judged";
                case Structures.Verdict.CompileError: return "Compile Error";
                case Structures.Verdict.RestrictedFunction: return "Restricted Function";
                case Structures.Verdict.RuntimeError: return "Runtime Error";
                case Structures.Verdict.OutputLimit: return "Output Limit Exceeded";
                case Structures.Verdict.TimLimit: return "Time Limit Exceeded";
                case Structures.Verdict.MemoryLimit: return "Memory Limit Exceeded";
                case Structures.Verdict.WrongAnswer: return "Wrong Answer";
                case Structures.Verdict.PresentationError: return "Presentation Error";
                case Structures.Verdict.Accepted: return "Accepted";
                default: return "- In Queue -";
            }
        }

        public static System.Drawing.Color GetProblemTitleColor(long pnum)
        {
            if (LocalDatabase.DefaultUser == null)
                return System.Drawing.Color.Black;
            if (LocalDatabase.DefaultUser.IsSolved(pnum))
                return System.Drawing.Color.Blue;
            else if (LocalDatabase.DefaultUser.TriedButUnsolved(pnum))
                return System.Drawing.Color.Brown;
            else
                return System.Drawing.Color.Black;
        }

        public static System.Drawing.Color GetVerdictColor(Structures.Verdict ver)
        {
            switch (ver)
            {
                case Structures.Verdict.SubError: return System.Drawing.Color.DarkGoldenrod;
                case Structures.Verdict.CannotBeJudge: return System.Drawing.Color.DarkGray;
                case Structures.Verdict.CompileError: return System.Drawing.Color.Red;
                case Structures.Verdict.RestrictedFunction: return System.Drawing.Color.SlateBlue;
                case Structures.Verdict.RuntimeError: return System.Drawing.Color.Green;
                case Structures.Verdict.OutputLimit: return System.Drawing.Color.Brown;
                case Structures.Verdict.TimLimit: return System.Drawing.Color.Teal;
                case Structures.Verdict.MemoryLimit: return System.Drawing.Color.SlateGray;
                case Structures.Verdict.WrongAnswer: return System.Drawing.Color.Maroon;
                case Structures.Verdict.PresentationError: return System.Drawing.Color.RoyalBlue;
                case Structures.Verdict.Accepted: return System.Drawing.Color.Blue;
                default: return System.Drawing.Color.Black;
            }
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
            string regfile = Path.Combine(path, "regkey.reg");
            File.WriteAllText(regfile, data);

            //save data
            string arg = string.Format("\"{0}\" \"{1}\"", file, path);
            System.Diagnostics.Process.Start(zipit, arg);
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
            string regfile = Path.Combine(path, "regkey.reg");
            System.Diagnostics.Process.Start(regfile);
            Application.Restart();
        }

        private static string BackupRegistryData(RegistryKey key = null)
        {
            string dat = "";
            string NL = Environment.NewLine;
            
            if(key == null)
            {
                key = RegistryAccess.DEFAULT;
                dat = "Windows Registry Editor Version 5.00" + NL + NL;                
            }

            //key path
            dat += string.Format("[{0}]", key) + NL;
                        
            //get values            
            foreach (string name in key.GetValueNames())
            {
                string value = JsonConvert.SerializeObject(key.GetValue(name));  
                dat += string.Format("\"{0}\"={1}", name, value) + NL;
            }

            //get sub keys            
            foreach (string subkey in key.GetSubKeyNames())
            {
                dat += BackupRegistryData(key.OpenSubKey(subkey)) + NL;                
            }

            return dat;
        }

        #endregion
    }
}

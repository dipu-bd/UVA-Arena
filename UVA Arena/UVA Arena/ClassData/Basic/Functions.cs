using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UVA_Arena.Internet;

namespace UVA_Arena
{
    /// <summary>
    /// Basic functions that are called frequently over this project
    /// </summary>
    internal static class Functions
    {
        #region Formatter Functions

        /// <summary>
        /// Apply this format : "{0:0.000}s" to (run / 1000.0)
        /// </summary>
        /// <param name="run">Number to format</param>
        public static string FormatRuntime(long run)
        {
            return String.Format("{0:0.000}s", (run / 1000.0));
        }

        /// <summary>
        /// Format byte memory size into "B", "KB", "MB", "GB", "TB"
        /// </summary>
        /// <param name="mem">Memory unit in byte</param>
        /// <returns>"{0:0.00}{1}" 0 = memory size and 1 = suffix</returns>
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

        /// <summary>
        /// Format a Time Span and returns a human readable string.
        /// </summary>
        /// <param name="span">TimeSpan to format</param>
        /// <returns>Returns the timespan in string</returns>
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

        /// <summary>
        /// Format seconds into human readable time-span string.
        /// </summary>
        /// <param name="span">Time span in seconds</param>
        /// <returns>Returns the timespan in string</returns>
        public static string FormatTimeSpan(long span)
        {
            return FormatTimeSpan(new TimeSpan(span * 10000000));
        }

        /// <summary>
        /// Parse a Category Index data file and returns a dictonary
        /// </summary>
        public static Dictionary<string, long> GetCategoryIndex()
        {
            var result = new Dictionary<string, long>();
            try
            {
                string file = LocalDirectory.GetCategoryIndexFile();
                string text = File.ReadAllText(file);
                var arr = JsonConvert.DeserializeObject(text);
                if (arr == null) return result;

                foreach (JToken tok in (JArray)arr)
                {
                    result.Add(
                        tok.Value<string>("file"),
                        tok.Value<long>("ver"));
                }
            }
            catch { }
            return result;
        }

        #endregion Formatter Functions

        #region Color and String getter

        /// <summary>
        /// Get language name from given language number
        /// </summary>
        /// <param name="lan">Language to get name</param>
        /// <returns>Language name</returns>
        public static string GetLanguage(Structures.Language lan)
        {
            switch (lan)
            {
                case Structures.Language.C: return "ANSI C";
                case Structures.Language.CPP: return "C++";
                case Structures.Language.CPP11: return "C++11";
                case Structures.Language.Java: return "Java";
                case Structures.Language.Pascal: return "Pascal";
                default: return "Unknown";
            }
        }

        /// <summary>
        /// Get verdict name form given verdict number
        /// </summary>
        /// <param name="ver">Verdict number</param>
        /// <returns>Verdict name</returns>
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

        /// <summary>
        /// Get color for given problem title
        /// </summary>
        /// <param name="pnum">Problem number to get color</param>
        /// <returns>A color object for problem title</returns>
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

        /// <summary>
        /// Get color for given verdict
        /// </summary>
        /// <param name="ver">Verdict number to get color</param>
        /// <returns>Color for the given verdict </returns>
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

        #endregion Color and String getter

        #region HTML File Parser

        /// <summary>
        /// Parse HTML file and find all local contents to download.
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <param name="replace">True, if you want to replace old files.</param>
        /// <returns>List of files to download</returns>
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
                GetAllImageFiles(htdoc.DocumentNode, urls);
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

        /// <summary>
        /// Recursive Depth First Search to find all image files
        /// </summary>
        /// <param name="nod">Current node to search</param>
        /// <param name="urls">Reference to the list of all gathered urls</param>
        private static void GetAllImageFiles(HtmlAgilityPack.HtmlNode nod, List<string> urls)
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
                GetAllImageFiles(child, urls);
            }

            //remove unnecessary data
            if (nod.NodeType == HtmlAgilityPack.HtmlNodeType.Comment)
            {
                nod.Attributes.RemoveAll();
                nod.Name = "div";
            }
        }

        #endregion HTML File Parser

        #region Backup and Restore

        /// <summary>
        /// Backup current user's settings and problem description into a .uapak file.
        /// Depends on external program "unzip\unzip.exe"
        /// </summary>
        public static void BackupData()
        {
            string path = LocalDirectory.DefaultPath;
            if (!System.IO.Directory.Exists(path)) return;

            string file = @"unzip\unzip.exe";
            file = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), file);
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

        /// <summary>
        /// Restore current user's settings and problem description from a .uapak file.
        /// Depends on external program "unzip\zipit.exe"
        /// </summary>
        public static void RestoreData()
        {
            string path = LocalDirectory.DefaultPath;
            if (!System.IO.Directory.Exists(path)) return;

            string file = @"unzip\zipit.exe";
            file = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), file);
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

        /// <summary>
        /// Background process form backing up data
        /// </summary>
        /// <param name="state">File name to save data</param>
        private static void Backup(object state)
        {
            string zipit = @"unzip\zipit.exe";
            zipit = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), zipit);
            string file = (string)state;

            string path = LocalDirectory.DefaultPath;
            if (!System.IO.Directory.Exists(path)) return;

            string regfile = Path.Combine(path, "backup.reg");
            BackupRegistryData(regfile);

            //save data
            string arg = string.Format("\"{0}\" \"{1}\" -64 -es -zc \"UVA Arena package\"", file, path);
            System.Diagnostics.Process.Start(zipit, arg).WaitForExit();

            MessageBox.Show("Data backup completed.");
        }

        /// <summary>
        /// Background process form restoring data
        /// </summary>
        /// <param name="state">File name to get data</param>
        private static void Restore(object state)
        {
            string unzip = @"unzip\unzip.exe";
            unzip = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), unzip);
            string file = (string)state;

            //delete old
            string path = LocalDirectory.DefaultPath;
            Directory.Delete(path, true);
            //SHOperations.Delete(new string[] { path },
            //    SHOperations.FileOperationFlags.FOF_ALLOWUNDO |
            //    SHOperations.FileOperationFlags.FOF_NOCONFIRMATION |
            //    SHOperations.FileOperationFlags.FOF_SIMPLEPROGRESS);

            while (System.IO.Directory.Exists(path))
                System.Threading.Thread.Sleep(100);

            //restore all
            string arg = string.Format("-o -d \"{0}\" \"{1}\"", path, file);
            System.Diagnostics.Process pr = System.Diagnostics.Process.Start(unzip, arg);
            pr.WaitForExit();

            //restore registry            
            if (pr.ExitCode == 0)
            {
                string regfile = Path.Combine(path, "backup.reg");
                RestorRegistryData(regfile);
            }

            //prompt to restart application            
            string msg = "Data restore finished. You need to restart the application to make some settings effective.";
            if (pr.ExitCode != 0) msg = "Data restoration failed due to some unknown error.";// +Environment.NewLine + pr.StandardError.ReadToEnd();
            MessageBox.Show(msg);
        }

        public static void BackupRegistryData(string regfile)
        {
            var key = RegistryAccess.GetDefaultRegKeyPath();
            string arg = string.Format("/e \"{0}\" \"{1}\"", regfile, key);
            System.Diagnostics.Process.Start("regedit.exe", arg).WaitForExit();
        }

        public static void RestorRegistryData(string regfile)
        {
            if (!System.IO.File.Exists(regfile)) return;
            string arg = string.Format("/s \"{0}\"", regfile);
            System.Diagnostics.Process.Start("regedit.exe", arg).WaitForExit();
        }

        #endregion Backup and Restore
    }
}
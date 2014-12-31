using System;
using System.Collections.Generic;
using System.IO;

namespace UVA_Arena
{
    internal static class LocalDirectory
    {
        //
        // Usual functions
        //
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        public static void CreateFile(string path, string data = "")
        {
            if (File.Exists(path)) return;
            CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, data);
        }

        public static string GetTempFile()
        {
            return Path.GetTempFileName();
        }

        public static long GetFileSize(string file)
        {
            if (!File.Exists(file)) return 0;
            return (new FileInfo(file)).Length;
        }

        public static bool IsValidFileName(string name)
        {
            List<char> invalid = new List<char>();
            invalid.AddRange(Path.GetInvalidFileNameChars());
            foreach (char ch in name)
            {
                if (invalid.Contains(ch)) return false;
            }
            return true;
        }

        public static string ValidateFileName(string name, string replace = null)
        {
            string res = "";
            List<char> invalid = new List<char>();
            invalid.AddRange(Path.GetInvalidFileNameChars());
            foreach (char ch in name)
            {
                if (!invalid.Contains(ch)) res += ch;
                else if (!string.IsNullOrEmpty(replace)) res += replace;
            }
            return res;
        }

        //
        // Copy and Delete
        //
        public static void CopyFilesOrFolders(string[] from, string dest)
        {
            //try { SHOptionsAPI.Copy(from, dest); }
            //catch
            {
                foreach (string file in from)
                {
                    string npath = Path.Combine(dest, Path.GetFileName(file));
                    if (File.Exists(file))
                    {
                        File.Copy(file, npath, true);
                    }
                    else if (Directory.Exists(file))
                    {
                        CopyFilesOrFolders(Directory.GetFiles(file), npath);
                        CopyFilesOrFolders(Directory.GetDirectories(file), npath);
                    }
                }
            }
        }

        public static void DeleteFilesOrFolders(string[] files)
        {
            //try { SHOptionsAPI.SendToRecycleBin(files); }
            //catch
            {
                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        File.Delete(file);
                    }
                    else
                    {
                        Directory.Delete(file, true);
                    }
                }
            }
        }

        //
        // Needed in project
        //
        private static string _DefaultPath = null;
        public static string DefaultPath
        {
            get
            {
                if (_DefaultPath != null) return _DefaultPath;
                _DefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                _DefaultPath = Path.Combine(_DefaultPath, "UVA Arena");
                CreateDirectory(_DefaultPath);
                return _DefaultPath;
            }
        }

        /// <summary> Get path where downloaded problems info should be saved </summary>
        /// <returns>Valid filename with .json extension</returns>
        public static string GetProblemDataFile()
        {
            string path = Path.Combine(DefaultPath, "problems.json");
            CreateFile(path);
            return path;
        }

        /// <summary> Get path where category data should be saved </summary>
        /// <returns>Valid filename with .json extension</returns>
        public static string GetCategoryPath()
        {
            string path = Path.Combine(DefaultPath, "category.json");
            CreateFile(path);
            return path;
        }

        /// <summary> Get path where problem description is saved (Readonly) </summary>
        public static string ProblemsPath
        {
            get
            {
                string path = Path.Combine(DefaultPath, "Problems");
                CreateDirectory(path);
                return path;
            }
        }

        /// <summary> get path where problem description is saved </summary>
        public static string GetProblemPath(long pnum)
        {
            string volname = string.Format("Volume {0:000}", pnum / 100);
            string path = Path.Combine(ProblemsPath, volname);
            CreateDirectory(path);
            return path;
        }

        public static string DefaultCodesPath()
        {
            string path = Path.Combine(LocalDirectory.DefaultPath, "Codes");
            CreateDirectory(path);
            return path;
        }

        /// <summary> get path where codes are saved </summary>
        public static string GetCodesPath(long pnum)
        {
            if (!LocalDatabase.IsReady) return null;

            string path = Elements.CODES.CodesPath;
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path)) return null;

            string title = LocalDatabase.GetTitle(pnum);
            title = ValidateFileName(title);

            char sep = Path.DirectorySeparatorChar;
            path += sep + string.Format("Volume {0:000}", pnum / 100);
            path += sep + string.Format("{0} - {1}", pnum, title);

            CreateDirectory(path);
            return path;
        }

        /// <summary> get problem html from problem number </summary>
        public static string GetProblemHtml(long pnum)
        {
            string name = string.Format("{0}.html", pnum);
            string file = Path.Combine(GetProblemPath(pnum), name);
            return file;
        }
        /// <summary> get problem html from problem number </summary>
        public static string GetProblemPdf(long pnum)
        {
            string name = string.Format("{0}.pdf", pnum);
            string file = Path.Combine(GetProblemPath(pnum), name);
            return file;
        }
        /// <summary> get problem html from problem number </summary>
        public static string GetProblemContent(long pnum, string name)
        {
            string file = Path.Combine(GetProblemPath(pnum), name);
            return file;
        }

        /// <summary> get user's submission path from username</summary>
        public static string GetUserSubPath(string username)
        {
            string uid = LocalDatabase.GetUserid(username);
            string name = uid.ToString() + ".json";
            string file = Path.Combine("Users", name);
            file = Path.Combine(DefaultPath, file);
            CreateFile(file);
            return file;
        }

        /// <summary> get accepted problems of uid </summary>
        public static string GetAcceptedOf(string uid)
        {
            string path = Path.Combine(DefaultPath, "Accepted List");
            string file = Path.Combine(path, uid);
            if (!File.Exists(file)) return "[]";
            return File.ReadAllText(file);
        }
        /// <summary> set accepted problems of uid </summary>
        public static void SetAcceptedOf(string uid, string data)
        {
            string path = Path.Combine(DefaultPath, "Accepted List");
            string file = Path.Combine(path, uid);
            CreateDirectory(path);
            File.WriteAllText(file, data);
        }

        /// <summary> returns the file path where log could be stored </summary>
        public static string GetLogFile()
        {
            string file = Path.Combine(DefaultPath, "log.dat");
            CreateFile(file);
            if ((new FileInfo(file)).Length > 1048576) // > 1MB
            {
                string[] lines = File.ReadAllLines(file);
                File.WriteAllText(file, "");
                for (int i = Math.Max(lines.Length - 1000, 0); i < lines.Length; ++i)
                    File.AppendAllText(file, lines[i] + Environment.NewLine);
            }
            return file;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace UVA_Arena
{
    sealed class LocalDirectory
    {
        //
        // Usual functions
        //
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }
        public static void CreateFile(string path)
        {
            if (File.Exists(path)) return;
            CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, "");
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
            invalid.AddRange(Path.GetInvalidPathChars());
            invalid.AddRange(Path.GetInvalidFileNameChars());
            foreach (char ch in name)
            {
                if (invalid.Contains(ch)) return false;
            }
            return true;
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
        public static string ProblemData
        {
            get
            {
                string path = Path.Combine(DefaultPath, "problems.json");
                CreateFile(path);
                return path;
            }
        }
        
        /// <summary> get path where last submitted problems were saved </summary>
        public static string LastSubmitPath
        {
            get
            {
                string path = Path.Combine(DefaultPath, "Last Submits");
                CreateFile(path);
                return path;
            }
        }

        /// <summary> get path where problem description is saved </summary>
        public static string ProblemDataPath
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
            string path = Path.Combine(ProblemDataPath, volname);
            CreateDirectory(path);
            return path;
        }

        /// <summary> get path where codes are saved </summary>
        public static string GetCodesPath(long pnum)
        {
            char sep = Path.DirectorySeparatorChar;
            string path = RegistryAccess.CodesPath + sep;
            path += string.Format("Volume {0:000}", pnum / 100) + sep;
            path += string.Format("{0} - {1}", pnum, ProblemDatabase.GetTitle(pnum));
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace UVA_Arena.Structures
{
    public static class LocalDirectory
    {
        private static string DEFAULT_PATH = null;

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

        public static string DefaultPath
        {
            get
            {
                if (DEFAULT_PATH != null) return DEFAULT_PATH;
                DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                DEFAULT_PATH = Path.Combine(DEFAULT_PATH, "UVA Arena");
                CreateDirectory(DEFAULT_PATH);
                return DEFAULT_PATH;
            }
        }

        public static string ProblemDataFile
        {
            get
            {
                string path = Path.Combine(DefaultPath, "problems.json");
                CreateFile(path);
                return path;
            }
        }

        /// <summary> get accepted problems of uid </summary>
        public static string GetAcceptedOf(string uid)
        {
            string path = Path.Combine(DefaultPath, "Accepted List");
            string file = Path.Combine(path, uid);
            if (File.Exists(file)) return File.ReadAllText(file);
            return "[]";
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

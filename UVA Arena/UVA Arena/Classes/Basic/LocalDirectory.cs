using System;
using System.Collections.Generic;
using System.IO;

namespace UVA_Arena
{
    /// <summary>
    /// Provides supportive functions for directories and files
    /// </summary>
    internal static class LocalDirectory
    {
        //
        // Usual functions
        //
        /// <summary>
        /// Create a directory if not exist
        /// </summary>
        /// <param name="path">Directory to create</param>
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Create a file if not exist. Directory is automatically created.
        /// </summary>
        /// <param name="path">Full path of the file to create</param>
        /// <param name="data">Initial data to write if file not exist</param>
        public static void CreateFile(string path, string data = "")
        {
            if (File.Exists(path)) return;
            CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, data);
        }

        /// <summary>
        /// Get a temporary file name located in user's default temporary path
        /// </summary>
        /// <returns>Path to the temporary file</returns>
        public static string GetTempFile()
        {
            return Path.GetTempFileName();
        }

        /// <summary>
        /// Size in bytes of specific file
        /// </summary>
        /// <param name="file">Full path of the file</param>
        /// <returns>0 if file not exist. Otherwise returns the size in bytes.</returns>
        public static long GetFileSize(string file)
        {
            if (!File.Exists(file)) return 0;
            return (new FileInfo(file)).Length;
        }

        /// <summary>
        /// Checked if the given name is valid for a file
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <returns>True if no invalid characters contains in the name</returns>
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

        /// <summary>
        /// Remove all invalid file name characters from given file name.
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <param name="replace">Replacement for invalid characters</param>
        /// <returns>A valid file name</returns>
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
        /// <summary>
        /// Copy files and folders to the destination directory
        /// </summary>
        /// <param name="from">List of files and folders to copy</param>
        /// <param name="dest">Destination folder for the given files and folders</param>
        public static void CopyFilesOrFolders(string[] from, string dest)
        {
            //try { 
                SHOptionsAPI.Copy(from, dest); 
            //}
            //catch
            //{
            //    foreach (string file in from)
            //    {
            //        string npath = Path.Combine(dest, Path.GetFileName(file));
            //        if (File.Exists(file))
            //        {
            //            File.Copy(file, npath, true);
            //        }
            //        else if (Directory.Exists(file))
            //        {
            //            CopyFilesOrFolders(Directory.GetFiles(file), npath);
            //            CopyFilesOrFolders(Directory.GetDirectories(file), npath);
            //        }
            //    }
            //}
        }
        /// <summary>
        /// Change the name of a file or folder
        /// </summary>
        /// <param name="from">Current path of file or folder</param>
        /// <param name="dest">Destination name</param>
        public static void RenameFileOrFolder(string from, string dest)
        {
            //try { 
            SHOptionsAPI.Rename(from, dest);
            //}
            //catch
            //{
            //    foreach (string file in from)
            //    {
            //        string npath = Path.Combine(dest, Path.GetFileName(file));
            //        if (File.Exists(file))
            //        {
            //            File.Copy(file, npath, true);
            //        }
            //        else if (Directory.Exists(file))
            //        {
            //            CopyFilesOrFolders(Directory.GetFiles(file), npath);
            //            CopyFilesOrFolders(Directory.GetDirectories(file), npath);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Delete files and folders permanantly
        /// </summary>
        /// <param name="files">List of files and folders to delete</param>
        public static void DeleteFilesOrFolders(string[] files)
        {
            //try { 
                SHOptionsAPI.SendToRecycleBin(files); 
            //}
            //catch
            //{                
            //    string delfolder = GetRecycleFolder();
            //    foreach (string file in files)
            //    {
            //        if (File.Exists(file))
            //        {
            //            File.Move(file, delfolder);
            //        }
            //        else
            //        {
            //            Directory.Move(file, delfolder);
            //        }
            //    }
            //}
        }



        //
        // Needed in project
        //
        private static string _DefaultPath = null;
        /// <summary>
        /// Default path to store documents. (Usually in MyDocuments folder)
        /// </summary>
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

        /// <summary> 
        /// Get the path where downloaded problems info should be saved 
        /// </summary>
        /// <returns>Valid filename with .json extension</returns>
        public static string GetProblemDataFile()
        {
            string path = Path.Combine(DefaultPath, "problems.json");
            CreateFile(path);
            return path;
        }

        /// <summary> 
        /// Get path where category data should be saved 
        /// </summary>
        /// <returns>Valid filename with .json extension</returns>
        public static string GetCategoryPath()
        {
            string path = Path.Combine(DefaultPath, "category.json");
            CreateFile(path);
            return path;
        }

        /// <summary> 
        /// Get the folder where all problem's description is saved.
        /// </summary>
        public static string GetProblemPath()
        {
                string path = Path.Combine(DefaultPath, "Problems");
                CreateDirectory(path);
                return path;
        }

        /// <summary> 
        /// Get the folder for where a problem's description should be saved.
        /// </summary>
        /// <param name="pnum">Problem Number</param>
        /// <returns>Valid folder to store problems description</returns>
        public static string GetVolumePath(long pnum)
        {
            string volname = string.Format("Volume {0:000}", pnum / 100);
            string path = Path.Combine(GetProblemPath(), volname);
            CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Default path for the code files located at DeafaultPath\Codes
        /// </summary>
        /// <returns>Valid directory for codes</returns>
        public static string DefaultCodesPath()
        {
            string path = Path.Combine(LocalDirectory.DefaultPath, "Codes");
            CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// Get the path where codes for a problem are saved 
        /// </summary>
        /// <param name="pnum">Problem Number</param>
        /// <returns>
        /// Valid folder of codes for a given problem. 
        /// Returns null if LocalDatabase is not ready or CodesPath doesn't exist 
        /// </returns>
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

        /// <summary>
        /// Get a path for precodes of specific language
        /// </summary>
        /// <param name="lang">Language of precode</param>
        /// <returns>
        /// Returns a valid file name to store precode.
        /// If CodesPath doesn't exist DefaultCodesPath is used.
        /// </returns>
        public static string GetPrecode(Structures.Language lang = Structures.Language.CPP)
        {
            string path = Elements.CODES.CodesPath;
            if (!Directory.Exists(path)) path = DefaultCodesPath();
            
            string ext = ".cpp";
            switch (lang)
            {
                case Structures.Language.C: ext = ".c"; break;
                case Structures.Language.Java: ext = ".java";break;
                case Structures.Language.Pascal: ext = ".pascal"; break;
                default: ext = ".cpp"; break;
            }

            path = Path.Combine(path, "Precodes");                        
            string file = Path.Combine(path, "Precode" + ext);
            CreateFile(file);

            return file;
        }

        /// <summary>
        /// Get the HTML file descrition of a problem
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <returns>File with valid path, file is not created</returns>
        public static string GetProblemHtml(long pnum)
        {
            string name = string.Format("{0}.html", pnum);
            string file = Path.Combine(GetVolumePath(pnum), name);
            return file;
        }

        /// <summary>
        /// Get the PDF file descrition of a problem
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <returns>File with valid path, file is not created</returns>
        public static string GetProblemPdf(long pnum)
        {
            string name = string.Format("{0}.pdf", pnum);
            string file = Path.Combine(GetVolumePath(pnum), name);
            return file;
        }

        /// <summary>
        /// Get the image file path to store for a problem's description
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <param name="name">Image file name</param>
        /// <returns>File with valid path, file is not created</returns>
        public static string GetProblemContent(long pnum, string name)
        {
            string file = Path.Combine(GetVolumePath(pnum), name);
            return file;
        }

        /// <summary>
        /// Get path where user's submissions are stored
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>Valid file with .json extension</returns>
        public static string GetUserSubPath(string username)
        {
            string uid = LocalDatabase.GetUserid(username);
            string name = uid.ToString() + ".json";
            string file = Path.Combine("Users", name);
            file = Path.Combine(DefaultPath, file);
            CreateFile(file);
            return file;
        }
         

        /// <summary>
        /// File to store program's logs. 
        /// If a pre-existing file size is > 1MB then,
        /// reduce its size by removing some lines from the beginning.
        /// </summary>
        /// <returns>Returns a valid file.</returns>
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

        /// <summary>
        /// Get the folder where delete code files and folders should be saved.
        /// Folder name starts with dot(.) and it is normally hidden.
        /// </summary>
        /// <returns>Valid directory that is hidden</returns>
        public static string GetRecycleFolder()
        {
            string path = Elements.CODES.CodesPath;
            if(!Directory.Exists(path)) path = DefaultCodesPath();
            path = Path.Combine(path, ".deleted");
            if(Directory.Exists(path)) return path;
            Directory.CreateDirectory(path);
            DirectoryInfo dir = new DirectoryInfo(path);
            dir.Attributes = FileAttributes.Hidden;
            return path;
        }
    }
}

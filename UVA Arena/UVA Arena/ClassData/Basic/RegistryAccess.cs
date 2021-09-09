using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UVA_Arena
{
    /// <summary>
    /// Interactive functions to get or set data into system's registry
    /// </summary>
    internal static class RegistryAccess
    {
        /// <summary>
        /// Default registry key used for this application
        /// </summary>
        public static RegistryKey DEFAULT
        {
            get
            {
                return Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("UVA Arena");
            }
        }

        /// <summary>
        /// Default registry key path used for this application
        /// </summary>
        public static string GetDefaultRegKeyPath()
        {
            return DEFAULT.Name;
        }

        /// <summary>
        /// Get a registry value by it's name and given key
        /// </summary>
        /// <param name="name">Name of the value</param>
        /// <param name="def">Default value</param>
        /// <param name="key">Path to look after default registry key</param>
        /// <returns>Registry entry value as an object</returns>
        public static object GetValue(string name, object def = null, string key = null)
        {
            RegistryKey regkey = DEFAULT;
            if (key != null) regkey = regkey.CreateSubKey(key);
            try
            {
                object val = regkey.GetValue(name);
                if (val != null) def = val;
            }
            catch { }
            return def;
        }

        /// <summary>
        /// Set a value into given registry entry
        /// </summary>
        /// <param name="name">Name of the value</param>
        /// <param name="val">Value to set</param>
        /// <param name="key">Path to look after default registry key</param>
        /// <param name="kind">Registry value kind of the object to save</param>
        public static void SetValue(string name, object val, string key = null,
            RegistryValueKind kind = RegistryValueKind.String)
        {
            RegistryKey regkey = DEFAULT;
            if (key != null) regkey = regkey.CreateSubKey(key);
            regkey.SetValue(name, val, kind);
        }


        /// <summary>
        /// Get of set default user-name
        /// </summary>
        public static string DefaultUsername
        {
            get
            {
                return (string)DEFAULT.GetValue("Default Username", "");
            }
            set
            {
                DEFAULT.SetValue("Default Username", value);
            }
        }

        /// <summary>
        /// Selected codes path by the user
        /// </summary>
        public static string CodesPath
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("Codes Path", null);
                if (dat != null && System.IO.Directory.Exists(dat)) return dat;
                return null;
            }
            set
            {
                RegistryAccess.SetValue("Codes Path", value);
            }
        }

        //
        // UserIDs
        //

        /// <summary> 
        /// Get a list of all stored usernames and userids 
        /// </summary>
        /// <returns>A dictory of [username]->[userid] values</returns>
        public static Dictionary<string, string> GetAllUsers()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                RegistryKey key = DEFAULT.CreateSubKey("UserID");
                foreach (string name in key.GetValueNames())
                {
                    if (dic.ContainsKey(name)) continue;
                    string uid = key.GetValue(name).ToString();
                    long test = 0;
                    if (!long.TryParse(uid, out test)) continue;
                    if (uid != test.ToString()) continue;
                    dic.Add(name, uid);
                }
            }
            catch { }
            return dic;
        }

        /// <summary>
        /// Set the userid of a username
        /// </summary>
        /// <param name="name">Username</param>
        /// <param name="uid">Userid</param>
        public static void AddUserid(string name, string uid)
        {
            if (string.IsNullOrEmpty(name)) return;
            if (string.IsNullOrEmpty(uid) || uid == "-") return;

            SetValue(name, uid, "UserID", RegistryValueKind.String);
            if (!LocalDatabase.usernames.ContainsKey(name))
                LocalDatabase.usernames.Add(name, uid);
            Interactivity.UserNameListChanged();
        }

        /// <summary>
        /// Delete a username from registry entry
        /// </summary>
        /// <param name="name">Username to delete</param>
        public static void DeleteUserid(string name)
        {
            if (!LocalDatabase.usernames.ContainsKey(name)) return;
            LocalDatabase.usernames.Remove(name);
            RegistryKey key = DEFAULT.CreateSubKey("UserID");
            key.DeleteValue(name, false);
            Interactivity.UserNameListChanged();
        }

        //
        // User Rank
        //
        /// <summary>
        /// Set rank-list of a user 
        /// </summary>
        /// <param name="urank">User rank to save</param>
        public static void SetUserRank(Structures.UserRanklist urank)
        {
            string data = JsonConvert.SerializeObject(urank);
            SetValue(urank.username, data, "User Rank", RegistryValueKind.String);
        }

        /// <summary>
        /// Get rank-list of a user
        /// </summary>
        /// <param name="name">Username to get rankling</param>
        /// <returns>Null reference if not found</returns>
        public static Structures.UserRanklist GetUserRank(string name)
        {
            string data = (string)GetValue(name, "", "User Rank");
            if (string.IsNullOrEmpty((string)data)) return null;
            return JsonConvert.DeserializeObject<Structures.UserRanklist>(data);
        }

        //
        // Problem Database
        //
        /// <summary>
        /// Get or set the list of favorite problem numbers
        /// </summary>
        public static List<long> FavoriteProblems
        {
            get
            {
                string dat = (string)GetValue("Favorites", "[]");
                List<long> lst = JsonConvert.DeserializeObject<List<long>>(dat);
                if (lst == null) lst = new List<long>();
                return lst;
            }
            set
            {
                string dat = JsonConvert.SerializeObject(value);
                SetValue("Favorites", dat);
            }
        }

        public static string MinGWCompilerPath
        {
            get
            {
                string dat = (string)GetValue("MinGW Compiler Path");
                if (string.IsNullOrEmpty(dat))
                {
                    dat = @"C:\Program Files (x86)\CodeBlocks\MinGW";
                    if (!System.IO.Directory.Exists(dat))
                        dat = @"C:\Program Files\CodeBlocks\MinGW";
                }
                return dat;
            }
            set
            {
                SetValue("MinGW Compiler Path", value);
            }
        }
        public static string JDKCompilerPath
        {
            get
            {
                string dat = (string)GetValue("JDK Compiler Path");
                if (string.IsNullOrEmpty(dat))
                {
                    dat = @"C:\Program Files\Java";
                    if (!System.IO.Directory.Exists(dat))
                        dat = @"C:\Program Files (x86)\Java";
                    if (System.IO.Directory.Exists(dat))
                    {
                        var all = System.IO.Directory.GetDirectories(dat, "jdk*");
                        if (all.Length == 0) return dat;
                        dat = all[0];
                    }
                    JDKCompilerPath = dat;
                }
                return dat;
            }
            set
            {
                SetValue("JDK Compiler Path", value);
            }
        }

        /// <summary>
        /// Gets or Sets the Compiler options for C compiler
        /// </summary>         
        public static string CCompilerOption
        {
            get
            {
                string dat = (string)GetValue("C Compiler Options");
                if (string.IsNullOrEmpty(dat)) dat = "-Wall -O2 -static -ansi";
                return dat;
            }
            set
            {
                SetValue("C Compiler Options", value);
            }
        }

        /// <summary>
        /// Gets or Sets the Compiler options for C++ compiler
        /// </summary> 
        public static string CPPCompilerOption
        {
            get
            {
                string dat = (string)GetValue("C++ Compiler Options");
                if (string.IsNullOrEmpty(dat)) dat = "-Wall -O2 -static";
                return dat;
            }
            set
            {
                SetValue("C++ Compiler Options", value);
            }
        }

        /// <summary>
        /// Gets or Sets the Compiler options for Java compiler
        /// </summary> 
        public static string JavaCompilerOption
        {
            get
            {
                string dat = (string)GetValue("Java Compiler Options");
                if (string.IsNullOrEmpty(dat)) dat = "";
                return dat;
            }
            set
            {
                SetValue("Java Compiler Options", value);
            }
        }

        /// <summary>
        /// Gets the category file version
        /// </summary> 
        public static long GetCategoryVersion(string category)
        {
            RegistryKey key = DEFAULT.CreateSubKey("Category Index");
            object ob = key.GetValue(category);
            if (ob == null || ob.GetType() != typeof(long))
                return -1;
            return (long)ob;
        }
        /// <summary>
        /// Sets the category file version
        /// </summary> 
        public static void SetCategoryVersion(string category, long version)
        {
            try
            {
                RegistryKey key = DEFAULT.CreateSubKey("Category Index");
                key.SetValue(category, version, RegistryValueKind.QWord);
            }
            catch { }
        }
    }
}

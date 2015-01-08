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
            RegistryKey key = DEFAULT.CreateSubKey("UserID");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string name in key.GetValueNames())
            {
                dic.Add(name, key.GetValue(name).ToString());
            }
            return dic;
        }

        /// <summary>
        /// Set the userid of a username
        /// </summary>
        /// <param name="name">Username</param>
        /// <param name="uid">Userid</param>
        public static void SetUserid(string name, string uid)
        {
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

        /// <summary>
        /// Set category tags of a problem
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <param name="category">Category tags to store</param>
        public static void SetTags(long pnum, List<string> category)
        {
            string data = JsonConvert.SerializeObject(category);
            SetValue(pnum.ToString(), data, "Problem Database", RegistryValueKind.String);
        }

        /// <summary>
        /// Get category tags to a problem
        /// </summary>
        /// <param name="pnum">Problem number</param>
        /// <returns>A list of category. Empty list if none found.</returns>
        public static List<string> GetTags(long pnum)
        {
            string data = (string)GetValue(pnum.ToString(), "[]", "Problem Database");
            List<string> tags = JsonConvert.DeserializeObject<List<string>>(data);
            if (tags == null) tags = new List<string>();
            return tags;
        }

        public static string MinGWCompilerPath
        {
            get
            {
                string dat = (string)GetValue("MinGW Compiler Path");
                if (string.IsNullOrEmpty(dat))
                    dat = @"C:\Program Files\CodeBlocks\MinGW";
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
                    if (!System.IO.Directory.Exists(dat)) return dat;
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
    }
}

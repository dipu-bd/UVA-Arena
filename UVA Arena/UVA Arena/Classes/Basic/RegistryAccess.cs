using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UVA_Arena
{
    internal static class RegistryAccess
    {
        public static RegistryKey DEFAULT
        {
            get
            {
                return Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("UVA Arena");
            }
        }

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

        public static void SetValue(string name, object val, string key = null,
            RegistryValueKind kind = RegistryValueKind.String)
        {
            RegistryKey regkey = DEFAULT;
            if (key != null) regkey = regkey.CreateSubKey(key);
            regkey.SetValue(name, val, kind);
        }


        /// <summary> default user name </summary>
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

        //
        // UserID
        //
        /// <summary> set user id of given name </summary>
        public static void SetUserid(string name, string uid)
        {
            SetValue(name, uid, "UserID", RegistryValueKind.String);
            if (!LocalDatabase.usernames.ContainsKey(name))
                LocalDatabase.usernames.Add(name, uid);
        }

        /// <summary> delete a user id </summary>
        public static void DeleteUserid(string name)
        {
            if (!LocalDatabase.usernames.ContainsKey(name)) return;
            LocalDatabase.usernames.Remove(name);
            RegistryKey key = DEFAULT.CreateSubKey("UserID");
            key.DeleteValue(name, false);
        }

        /// <summary> Get a list of all stored usernames and userids </summary>
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

        //
        // User Rank
        //
        /// <summary> set user rank of given name </summary>
        public static void SetUserRank(Structures.UserRanklist urank)
        {
            string data = JsonConvert.SerializeObject(urank);
            SetValue(urank.username, data, "User Rank", RegistryValueKind.String);            
        }

        public static Structures.UserRanklist GetUserRank(string name)
        {
            string data = (string)GetValue(name, "", "User Rank");
            if (string.IsNullOrEmpty((string)data)) return null;
            return JsonConvert.DeserializeObject<Structures.UserRanklist>(data);
        }
        
        //
        // Problem Database
        //
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

        public static void SetTags(long pnum, List<string> category)
        {
            string data = JsonConvert.SerializeObject(category);
            SetValue(pnum.ToString(), data, "Problem Database", RegistryValueKind.String);
        }

        public static List<string> GetTags(long pnum)
        {
            string data = (string)GetValue(pnum.ToString(), "[]", "Problem Database");
            List<string> tags = JsonConvert.DeserializeObject<List<string>>(data);
            if (tags == null) tags = new List<string>();
            return tags;
        }
    }
}

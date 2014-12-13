using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace UVA_Arena
{
    sealed class RegistryAccess
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
            if(!DefaultDatabase.usernames.ContainsKey(name))
                DefaultDatabase.usernames.Add(name, uid);
        }
        /// <summary> Get a list of all stored usernames and userids </summary>
        public static Dictionary<string, string> GetAllUsers()
        {
            RegistryKey key = DEFAULT.CreateSubKey("UserID");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach(string name in key.GetValueNames())
            {
                dic.Add(name, key.GetValue(name).ToString());
            }
            return dic;
        }
      

        //
        // Problem Database
        //
        public static List<long> FavouriteProblems
        {
            get
            {
                string dat = (string)GetValue("Favourites", "[]");
                List<long> lst = JsonConvert.DeserializeObject<List<long>>(dat);
                if (lst == null) lst = new List<long>();
                return lst;
            }
            set
            {
                string dat = JsonConvert.SerializeObject(value);
                SetValue("Favourites", dat);
            }
        }
        public static void SetTags(long pnum, List<string> catagory)
        {
            string data = JsonConvert.SerializeObject(catagory);
            SetValue(pnum.ToString(), data, "Problem Database", RegistryValueKind.String);
        }
        public static List<string> GetTags(long pnum)
        {
            string dat = (string)GetValue(pnum.ToString(), "[]", "Problem Database");
            List<string> tags = JsonConvert.DeserializeObject<List<string>>(dat);
            if (tags == null) tags = new List<string>();
            return tags;
        }

    }
}

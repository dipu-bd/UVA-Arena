using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace UVA_Arena
{
    internal sealed class RegistryAccess
    {
        public static RegistryKey DEFAULT
        {
            get
            {
                return Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("UVA Arena");
            }
        }

        public static object GetValue(string name, object def, string key = null)
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

        /// <summary> default user name </summary>
        public static string CodesPath
        {
            get
            {
                string dat = (string)GetValue("Codes Path", null);
                if (dat != null && Directory.Exists(dat)) return dat;
                return Path.Combine(LocalDirectory.DefaultPath, "Codes");
            }
            set
            {
                SetValue("Codes Path", value);
            }
        }

        //
        // UserID
        //
        /// <summary> get user id from name </summary>
        public static string GetUserid(string name)
        {
            string dat = (string)GetValue(name, null, "UserID");
            if (string.IsNullOrEmpty(dat) || dat.Length <= 1) return null;
            return dat;
        }
        /// <summary> set user id of given name </summary>
        public static void SetUserid(string name, string uid)
        {
            SetValue(name, uid, "UserID", RegistryValueKind.String);
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

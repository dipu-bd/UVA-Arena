using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace UVA_Arena.Structures
{
    public static class RegistryAccess
    {
        /// <summary> Registry path where values saved </summary>
        public static string REG_PATH = "HKEY_CURRENT_USER\\Software\\UVA Arena";

        /// <summary> default user name </summary>
        public static string DefaultUsername
        {
            get
            {
                object dat = Registry.GetValue(REG_PATH, "Default Username", "");
                if (dat == null) return null;
                return dat.ToString();
            }
            set { Registry.SetValue(REG_PATH, "Default Username", value); }
        }

        #region User Profile Database
    
        /// <summary> get user id from name </summary>
        public static string GetUserid(string name)
        {
            string path = Path.Combine(REG_PATH, "UserID");
            object dat = Registry.GetValue(path, name, "");
            if (dat == null) return null;
            return dat.ToString();
        }
        /// <summary> set user id of given name </summary>
        public static void SetUserid(string name, string uid)
        {
            string path = Path.Combine(REG_PATH, "UserID");
            Registry.SetValue(path, name, uid);
        }

        #endregion
    }
}

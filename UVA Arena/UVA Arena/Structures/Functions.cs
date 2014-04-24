using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace UVA_Arena.Structures
{
    public static class Functions
    {
        public static string FormatRuntime(long run)
        {
            return String.Format("{0:0.000} sec", (run / 1000.0));
        }

        public static string FormatMemory(long mem)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB" };

            int ind = 0;
            double res = mem;
            while (res >= 1024.0)
            {
                res /= 1024.0;
                ind++;
            }

            return String.Format("{0:0.00} {1}", res, suf[ind]);
        }

        public static string FormatTimeSpan(long span)
        {
            if (span < 60)
                return (span).ToString() + " secs ";

            long year, mon, day, hour, min;

            year = span / 31536000;
            span -= 31536000 * year;

            mon = span / 2592000;
            span -= 2592000 * mon;

            day = span / 86400;
            span -= 86400 * day;

            hour = span / 3600;
            span -= 3600 * hour;

            min = span / 60;

            string txt = "";
            if (year > 0) txt += year.ToString() + "y ";
            if (mon > 0) txt += mon.ToString() + "mh ";
            if (day > 0) txt += day.ToString() + "d ";
            if (hour > 0) txt += hour.ToString() + "h ";
            txt += min.ToString() + "m ";

            return txt;
        }
    }

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
        /// <summary> get accepted problems of uid </summary>
        public static string GetAcceptedOf(string uid)
        {
            string path = Path.Combine(REG_PATH, "Accepted List");
            object dat = Registry.GetValue(path, uid, "");
            if (dat == null) return null;
            return dat.ToString();
        }
        /// <summary> set accepted problems of uid </summary>
        public static void SetAcceptedOf(string uid, string data)
        {
            string path = Path.Combine(REG_PATH, "Accepted List");
            Registry.SetValue(path, uid, data);
        }

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

    public static class UnixTimestamp
    {
        /// <summary> EPOCH time - used by the unix timestamp </summary>
        public static DateTime EPOCH = new DateTime(1970, 1, 1);

        /// <summary> amount of error when converting from unix time  </summary>
        private static long UnixTimeError = 0;

        /// <summary>
        /// returns difference between current and given unix-time
        /// if diffrence > 2day returns date-time as string
        /// </summary>
        public static string GetTimeSpan(string unix_time)
        {
            long now = CurrentTime;
            long given = long.Parse(unix_time);

            long diff = now - given - UnixTimeError;
            if (diff < 0)
            {
                UnixTimeError += diff;
                diff = 0;
            }

            if (diff >= 2592000) return FromUnixTime(given).ToString();
            return Functions.FormatTimeSpan(diff) + "ago";
        }

        /// <summary>
        /// convert unix time to local datetime
        /// </summary>
        public static DateTime FromUnixTime(long unix, bool err_fixed = true)
        {
            if (err_fixed) return EPOCH.AddSeconds(unix + UnixTimeError).ToLocalTime();
            else return EPOCH.AddSeconds(unix).ToLocalTime();
        }

        /// <summary>
        /// convert utc datetime to unix time
        /// </summary>
        public static long ToUnixTime(DateTime time)
        {
            return (long)(time - EPOCH).TotalSeconds;
        }

        /// <summary> current unix time of pc </summary>
        public static long CurrentTime
        {
            get { return ToUnixTime(DateTime.UtcNow); }
            private set { }
        }

    }
}

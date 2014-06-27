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
            if (span < 120) return (span).ToString() + " secs ";

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
            if (year > 0) txt += year.ToString() + "year ";
            if (mon > 0) txt += mon.ToString() + "month ";
            if (day > 0) txt += day.ToString() + "day ";
            if (hour > 0) txt += hour.ToString() + "hour ";
            txt += min.ToString() + "min ";

            return txt;
        }
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

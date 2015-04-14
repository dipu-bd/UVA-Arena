using System;
using System.Collections.Generic;
using System.Text;

namespace UVA_Arena
{
	/// <summary>
	/// Class to format various kind of data.
	/// </summary>
    public static class Formatter
    {
		/// <summary>
		/// Apply this format : "{0:0.000}s" to (run / 1000.0)
		/// </summary>
		/// <param name="run">Number to format</param>
        public static string FormatRuntime(long run)
        {
            return String.Format("{0:0.000}s", (run / 1000.0));
        }

        /// <summary>
        /// Format byte memory size into "B", "KB", "MB", "GB", "TB"
        /// </summary>
        /// <param name="mem">Memory unit in byte</param>
        /// <returns>"{0:0.00}{1}" 0 = memory size and 1 = suffix</returns>
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

            return String.Format("{0:0.00}{1}", res, suf[ind]);
        }

        /// <summary>
        /// Format a Time Span and returns a human readable string.
        /// </summary>
        /// <param name="span">TimeSpan to format</param>
        /// <returns>Returns the timespan in string</returns>
        public static string FormatTimeSpan(TimeSpan span)
        {
            int year = (int)(span.TotalDays / 365);
            int month = (span.Days - year * 365) / 30;
            int days = span.Days - year * 365 - month * 30;

            string txt = "";
            bool space = false;

            if (year > 0)
            {
                space = true;
                txt += string.Format("{0} Year", year);
                if (year > 1) txt += "s"; //plural
            }
            if (month > 0)
            {
                if (space) txt += " "; space = true;
                txt += string.Format(" {0} Month", month);
                if (month > 1) txt += "s"; //plural
            }
            if (span.TotalDays < 30)
            {
                if (days > 0)
                {
                    if (space) txt += " "; space = true;
                    txt += string.Format("{0} Day", days);
                    if (days > 1) txt += "s"; //plural
                }
            }
            if (span.TotalDays < 1)
            {
                if (span.Hours > 0)
                {
                    if (space) txt += " "; space = true;
                    txt += string.Format("{0} Hour", span.Hours);
                    if (span.Hours > 1) txt += "s"; //plural
                }
                if (span.Minutes > 0)
                {
                    if (space) txt += " "; space = true;
                    txt += string.Format("{0} Minute", span.Minutes);
                    if (span.Minutes > 1) txt += "s"; //plural
                }
            }

            if (span.TotalMinutes < 1)
            {
                if (space) txt += " "; space = true;
                txt += string.Format("{0} Second", span.TotalSeconds);
                if (span.Seconds > 1) txt += "s"; //plural
            }

            return txt;
        }

        /// <summary>
        /// Format seconds into human readable time-span string.
        /// </summary>
        /// <param name="span">Time span in seconds</param>
        /// <returns>Returns the timespan in string</returns>
        public static string FormatTimeSpan(long span)
        {
            return FormatTimeSpan(new TimeSpan(span * 10000000));
        }
    }
}

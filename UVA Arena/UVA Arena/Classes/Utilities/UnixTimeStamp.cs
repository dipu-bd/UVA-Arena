using System;

namespace UVA_Arena
{
    internal static class UnixTimestamp
    {
        /// <summary> EPOCH time - used by the unix timestamp </summary>
        public static readonly DateTime EPOCH = new DateTime(1970, 1, 1);

        /// <summary> amount of error when converting from unix time  </summary>
        internal static long UnixTimeError = 0;

        /// <summary>
        /// returns difference between current and given unix-time 
        /// </summary>
        public static TimeSpan GetTimeSpan(long unix_time)
        {
            long diff = CurrentTime - (unix_time + UnixTimeError);
            if (diff < 0)
            {
                UnixTimeError += diff;
                diff = 0;
            }
            return new TimeSpan(diff * 10000000);
        }

        public static string FormatUnixTime(long unix_time)
        {
            TimeSpan span = GetTimeSpan(unix_time);
            if (span.TotalHours < 12)
            {
                return Functions.FormatTimeSpan(span) + " ago";
            }
            else
            {
                return string.Format("{0} ago ({1:dd-MMM-yyyy hh:mm tt})",
                    Functions.FormatTimeSpan(span),
                    FromUnixTime(unix_time));
            }
        }

        /// <summary>
        /// convert unix time to local datetime
        /// </summary>
        public static DateTime FromUnixTime(long unix, bool err_fixed = true)
        {
            if (err_fixed) unix += UnixTimeError;
            return EPOCH.AddSeconds(unix).ToLocalTime();
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
        }
    }
}

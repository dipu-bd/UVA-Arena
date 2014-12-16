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
        /// if diffrence > 2day returns date-time as string
        /// </summary>
        public static string GetTimeSpan(long unix_time)
        {
            long now = CurrentTime;

            long diff = now - unix_time - UnixTimeError;
            if (diff < 0)
            {
                UnixTimeError += diff;
                diff = 0;
            }

            if (diff < 2592000) // 2592000 = 2day
                return Functions.FormatTimeSpan(diff) + "ago";
            else
                return FromUnixTime(unix_time).ToString();
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

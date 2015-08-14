using System;
using System.Collections.Generic;

namespace UVA_Arena
{
    public class LogData
    {
        public DateTime time { get; set; }
        public string source { get; set; }
        public string status { get; set; }
        public LogData() { }
    };

    internal static class Logger
    {
        public static List<LogData> LOG = new List<LogData>();
        public static void Add(string text, string source)
        {
            try
            {
                //add to current context
                LogData ld = new LogData();
                ld.time = DateTime.Now;
                ld.source = source;
                ld.status = text;
                LOG.Add(ld);

                //save log
                string dat = "";
                dat += DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToLongTimeString();
                dat += " => " + text + " => " + source + Environment.NewLine;
                if (LOG.Count == 0) text = Environment.NewLine + text;
                System.IO.File.AppendAllText(LocalDirectory.GetLogFile(), dat);
            }
            catch { }
        }
    }
}

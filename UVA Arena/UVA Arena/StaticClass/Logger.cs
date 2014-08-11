using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UVA_Arena
{
    sealed class Logger
    {
        public class LogData
        {
            public DateTime time { get; set; }
            public string source { get; set; }
            public string status { get; set; }
            public LogData() { }
        };

        public static List<LogData> LOG = new List<LogData>();
        public static void Add(string text, string source)
        {
            LogData ld = new LogData();
            ld.time = DateTime.Now;
            ld.source = source;
            ld.status = text;
            LOG.Add(ld);
        }
    }
}

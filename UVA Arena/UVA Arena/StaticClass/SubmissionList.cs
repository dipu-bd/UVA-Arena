using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Security.Permissions;

namespace UVA_Arena.Structures
{
    public enum Language
    {
        C = 0,
        CPP = 1,
        Java = 2,
        Pascal = 3
    }

    public class SubmissionList
    {
        public class SubmissionMessage
        {
            public long sid { get; set; }
            public long uid { get; set; }
            public long pid { get; set; }
            public long ver { get; set; }
            public long lan { get; set; }
            public long run { get; set; }
            public long mem { get; set; }
            public long rank { get; set; }
            public long sbt { get; set; }
            public string name { get; set; }
            public string uname { get; set; }

            public SubmissionMessage() { }
        }

        public long id { get; set; }
        public long type { get; set; }
        public SubmissionMessage msg { get; set; }

        public long sid { get { return msg.sid; } set { msg.sid = value; } }
        public long uid { get { return msg.uid; } set { msg.uid = value; } }
        public long pid { get { return msg.pid; } set { msg.pid = value; } }
        public long ver { get { return msg.ver; } set { msg.ver = value; } }
        public Language lan { get { return (Language)msg.lan; } set { msg.lan = (int)value; } }
        public long rank { get { return msg.rank; } set { msg.rank = value; } }
        public string name { get { return name; } set { msg.name = value; } }
        public string uname { get { return uname; } set { msg.uname = value; } }
        public string runtime { get { return Functions.FormatRuntime(msg.run); } }
        public string memory { get { return Functions.FormatMemory(msg.mem); } }
        public string subtime { get { return UnixTimestamp.GetTimeSpan(msg.sbt); } }

        public SubmissionList() { }
    }
}

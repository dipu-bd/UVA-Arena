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
    [StructLayout(LayoutKind.Sequential)]
    class ProblemList
    {
        public enum Status
        {
            unavailable = 0,
            normal = 1,
            special_judge = 2
        }

        //Problem ID
        public long pid { get; set; }
        //Problem Number
        public long pnum { get; set; }
        //Problem Title
        public string ptitle { get; set; }
        //Number of Distinct Accepted User (DACU)
        public long dacu { get; set; }
        //Best Runtime of an Accepted Submission
        public long run { get; set; }
        //Best Memory used of an Accepted Submission
        public long mem { get; set; }
        //Number of No Verdict Given (can be ignored)
        public long nver { get; set; }
        //Number of Submission Error
        public long sube { get; set; }
        //Number of Can't be Judged
        public long cbj { get; set; }
        //Number of In Queue
        public long inq { get; set; }
        //Number of Compilation Error
        public long ce { get; set; }
        //Number of Restricted Function
        public long resf { get; set; }
        //Number of Runtime Error
        public long re { get; set; }
        //Number of Output Limit Exceeded
        public long ole { get; set; }
        //Number of Time Limit Exceeded
        public long tle { get; set; }
        //Number of Memory Limit Exceeded
        public long mle { get; set; }
        //Number of Wrong Answer
        public long wa { get; set; }
        //Number of Presentation Error
        public long pe { get; set; }
        //Number of Accepted
        public long ac { get; set; }
        //Problem Run-Time Limit (milliseconds)
        public long rtl { get; set; }
        //Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
        public Status status { get; set; }

        //formatted values
        public string runtime { get; set; }
        public string memory { get; set; }
        public string timelimit { get; set; }

        public void parse(List<string> data)
        {
            //[36,100,"The 3n + 1 problem",61026,0,1000000000,0,6473,0,0,93803,0,50329,49,48954,5209,215870,3957,157711,3000,1]
            Type t = typeof(ProblemList);
            PropertyInfo[] pcol = t.GetProperties();
            for (int i = 0; i < data.Count; ++i)
            {
                pcol[i].SetValue(this, data[i], null);
            }

            runtime = Functions.FormatRuntime(run);
            timelimit = Functions.FormatRuntime(rtl);
            memory = (mem == 1000000000) ? "-" : Functions.FormatMemory(mem);
        }

        public ProblemList() { }
        public ProblemList(List<string> data) { parse(data); }
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
            public long name { get; set; }
            public long uname { get; set; }
        }

        public long id { get; set; }
        public long type { get; set; }
        public SubmissionMessage msg { get; set; }

        public long sid { get { return msg.sid; } set { msg.sid = value; } }
        public long uid { get { return msg.uid; } set { msg.uid = value; } }
        public long pid { get { return msg.pid; } set { msg.pid = value; } }
        public long ver { get { return msg.ver; } set { msg.ver = value; } }
        public long lan { get { return msg.lan; } set { msg.lan = value; } }
        public long runtime { get; set; }
        public long memory { get; set; }
        public long rank { get; set; }
        public long subtime { get; set; }
        public long name { get; set; }
        public long uname { get; set; }

    }
}

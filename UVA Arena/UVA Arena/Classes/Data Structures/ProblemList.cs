using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace UVA_Arena.Structures
{ 
    public enum ProblemStatus
    {
        Unavailable,
        Normal,
        Special_Judge
    };

    [StructLayout(LayoutKind.Sequential)]
    public class ProblemInfo 
    {
        public ProblemInfo() { }
        public ProblemInfo(List<object> data) { SetData(data); }

        /// <summary>Problem ID</summary>
        public long pid { get; set; }
        /// <summary>Problem Number
        public long pnum { get; set; }
        /// <summary>Problem Title
        public string ptitle { get; set; }
        /// <summary>Number of Distinct Accepted User (DACU)
        public long dacu { get; set; }
        /// <summary>Best Runtime of an Accepted Submission
        public long run { get; set; }
        /// <summary>Best Memory used of an Accepted Submission
        public long mem { get; set; }
        /// <summary>Number of No Verdict Given (can be ignored)
        public long nver { get; set; }
        /// <summary>Number of Submission Error
        public long sube { get; set; }
        /// <summary>Number of Can't be Judged
        public long cbj { get; set; }
        /// <summary>Number of In Queue
        public long inq { get; set; }
        /// <summary>Number of Compilation Error
        public long ce { get; set; }
        /// <summary>Number of Restricted Function
        public long resf { get; set; }
        /// <summary>Number of Runtime Error
        public long re { get; set; }
        /// <summary>Number of Output Limit Exceeded
        public long ole { get; set; }
        /// <summary>Number of Time Limit Exceeded
        public long tle { get; set; }
        /// <summary>Number of Memory Limit Exceeded
        public long mle { get; set; }
        /// <summary>Number of Wrong Answer
        public long wa { get; set; }
        /// <summary>Number of Presentation Error
        public long pe { get; set; }
        /// <summary>Number of Accepted
        public long ac { get; set; }
        //Problem Run-Time Limit (milliseconds)
        public long rtl { get; set; }
        //Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
        public long stat { get; set; }

        //formatted special values 
        public ProblemStatus status { get; set; }
        public int volume { get; set; }
        public long total { get; set; }
        public double level { get; set; } 
        public bool stared { get; set; }
        public bool solved { get; set; }        
        public bool marked { get; set; }
        public int priority { get; set;}

        public List<string> categories = new List<string>();

        public override string ToString()
        {
            return string.Format(" {0} {1} {2} {3} ", pnum, ptitle,
                    string.Join(" ", categories.ToArray()), status);
        }

        public void SetData(List<object> data)
        {            
            Type t = typeof(ProblemInfo);
            PropertyInfo[] pcol = t.GetProperties();
            for (int i = 0; i < data.Count; ++i)
            {
                pcol[i].SetValue(this, data[i], null);
            }

            solved = false; //default is false
            volume = (int)(pnum / 100);
            if (run >= 1000000000) run = -1;
            if (mem >= 1000000000) mem = -1;
            
            total = ac + wa + cbj + ce + mle + tle + ole + nver + pe + re + resf + sube;

            if (stat == 0) status = ProblemStatus.Unavailable;
            else if (stat == 1) status = ProblemStatus.Normal;
            else status = ProblemStatus.Special_Judge; 
        }
    }
}

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

        /// <summary>0. Problem ID </summary>
        public long pid { get; set; }
        /// <summary>1. Problem Number </summary>
        public long pnum { get; set; }
        /// <summary>2. Problem Title  </summary>
        public string ptitle { get; set; }
        /// <summary>3. Number of Distinct Accepted User (DACU) </summary>
        public long dacu { get; set; }
        /// <summary>4. Best Runtime of an Accepted Submission </summary>
        public long run { get; set; }
        /// <summary>5. Best Memory used of an Accepted Submission </summary>
        public long mem { get; set; }
        /// <summary>6. Number of No Verdict Given (can be ignored) </summary>
        public long nver { get; set; }
        /// <summary>7. Number of Submission Error  </summary>
        public long sube { get; set; }
        /// <summary>8. Number of Can't be Judged </summary>
        public long cbj { get; set; }
        /// <summary>9. Number of In Queue  </summary>
        public long inq { get; set; }
        /// <summary>10. Number of Compilation Error </summary>
        public long ce { get; set; }
        /// <summary>11. Number of Restricted Function </summary>
        public long resf { get; set; }
        /// <summary>12. Number of Runtime Error </summary>
        public long re { get; set; }
        /// <summary>13. Number of Output Limit Exceeded </summary>
        public long ole { get; set; }
        /// <summary>14. Number of Time Limit Exceeded </summary>
        public long tle { get; set; }
        /// <summary>15. Number of Memory Limit Exceeded </summary>
        public long mle { get; set; }
        /// <summary>16. Number of Wrong Answer </summary>
        public long wa { get; set; }
        /// <summary>17. Number of Presentation Error </summary>
        public long pe { get; set; }
        /// <summary>18. Number of Accepted  </summary>
        public long ac { get; set; }
        /// <summary>19. Time Limit (milliseconds)  </summary>
        public long rtl { get; set; }
        /// <summary>20. Problem Status (0 = unavailable, 1 = normal, 2 = special judge)  </summary>
        public long stat { get; set; }

        //formatted special values 
        public int volume { get; set; }
        public long total { get; set; }
        public bool stared { get; set; }
        public bool solved { get; set; }        
        public bool marked { get; set; }
        public int priority { get; set;}
        public long fileSize { get; set; }
        public double level { get; set; } 
        public ProblemStatus status { get; set; }

        public List<string> categories = new List<string>();

        public override string ToString()
        {
            return string.Format(" {0} {1} {2} {3} ", pnum, ptitle,
                    string.Join(" ", categories.ToArray()), status);
        }

        public void SetData(List<object> data)
        {
            //number of property to be assign
            const int number_of_property = 20;

            Type t = typeof(ProblemInfo);
            PropertyInfo[] pcol = t.GetProperties();            
            for (int i = 0; i <= number_of_property; ++i)
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

            //set level
            const int MAX_LEVEL = 9;
            this.level = 1 + MAX_LEVEL;
            if (this.dacu > 0)
            {
                this.level -= Math.Min(MAX_LEVEL, Math.Floor(Math.Log(this.dacu)));
            }
        }
    }
}

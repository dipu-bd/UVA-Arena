using System;
using System.Collections.Generic;
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
        public int Volume { get; set; }
        public long Total { get; set; }
        public bool Starred { get; set; }
        public bool Solved { get; set; }
        public bool Marked { get; set; }
        public int Priority { get; set; }
        public long FileSize { get; set; }
        public double Level { get; set; }
        public ProblemStatus Status { get; set; }

        public List<CategoryNode> categories = new List<CategoryNode>();

        public override string ToString()
        {
            return string.Format(" {0} {1} {2} ", pnum, ptitle, Status);
        }

        public void SetData(List<object> data)
        {
            //number of property to be assign
            //const int number_of_property = 20;

            //Type t = typeof(ProblemInfo);
            //PropertyInfo[] pcol = t.GetProperties();            
            //for (int i = 0; i <= number_of_property; ++i)
            //{
            //    pcol[i].SetValue(this, data[i], null);
            //}

            //assign properties
            //Problem ID
            pid = (long)data[0];
            //Problem Number
            pnum = (long)data[1];
            //Problem Title
            ptitle = (string)data[2];
            //Number of Distinct Accepted User (DACU)
            dacu = (long)data[3];
            //Best Runtime of an Accepted Submission
            run = (long)data[4];
            //Best Memory used of an Accepted Submission
            mem = (long)data[5];
            //Number of No Verdict Given (can be ignored)
            nver = (long)data[6];
            //Number of Submission Error
            sube = (long)data[7];
            //Number of Can't be Judged
            cbj = (long)data[8];
            //Number of In Queue
            inq = (long)data[9];
            //Number of Compilation Error
            ce = (long)data[10];
            //Number of Restricted Function
            resf = (long)data[11];
            //Number of Runtime Error
            re = (long)data[12];
            //Number of Output Limit Exceeded
            ole = (long)data[13];
            //Number of Time Limit Exceeded
            tle = (long)data[14];
            //Number of Memory Limit Exceeded
            mle = (long)data[15];
            //Number of Wrong Answer
            wa = (long)data[16];
            //Number of Presentation Error
            pe = (long)data[17];
            //Number of Accepted
            ac = (long)data[18];
            //Problem Run-Time Limit (milliseconds)
            rtl = (long)data[19];
            //Problem Status (0 = unavailable, 1 = normal, 2 = special judge)
            stat = (long)data[20];

            //load other
            Solved = false; //default is false
            Volume = (int)(pnum / 100);
            if (run >= 1000000000) run = -1;
            if (mem >= 1000000000) mem = -1;

            Total = ac + wa + cbj + ce + mle + tle + ole + nver + pe + re + resf + sube;

            if (stat == 0) Status = ProblemStatus.Unavailable;
            else if (stat == 1) Status = ProblemStatus.Normal;
            else Status = ProblemStatus.Special_Judge;

            //set level
            const int MAX_LEVEL = 9;
            this.Level = 1 + MAX_LEVEL;
            if (this.dacu > 0)
            {
                this.Level -= Math.Min(MAX_LEVEL, Math.Floor(Math.Log(this.dacu)));
            }
        }
    }
}

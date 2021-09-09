using System.Collections.Generic;

namespace UVA_Arena.Structures
{

    public class UserSubmission
    {
        public UserSubmission() { }
        public UserSubmission(List<long> data) { LoadData(data); }

        //Submission ID
        public long sid { get; set; }
        //Problem ID
        public long pid { get; set; }
        //Verdict ID
        public long ver { get; set; }
        //Runtime
        public long run { get; set; }
        //Submission Time (UNIX time stamp)
        public long sbt { get; set; }
        //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
        public long lan { get; set; }
        //Submission Rank
        public long rank { get; set; }

        public string uname { get; set; }
        public string name { get; set; }
        public long pnum { get; set; }
        public string ptitle { get; set; }
        public List<long> data { get; private set; }

        public bool IsAccepted()
        {
            return (ver == 90);
        }

        public bool IsInQueue()
        {
            switch ((Verdict)ver)
            {
                case Structures.Verdict.SubError:
                case Structures.Verdict.CannotBeJudge:
                case Structures.Verdict.CompileError:
                case Structures.Verdict.RestrictedFunction:
                case Structures.Verdict.RuntimeError:
                case Structures.Verdict.OutputLimit:
                case Structures.Verdict.TimLimit:
                case Structures.Verdict.MemoryLimit:
                case Structures.Verdict.WrongAnswer:
                case Structures.Verdict.PresentationError:
                case Structures.Verdict.Accepted:
                    return false;
                default:
                    return true;
            }
        }

        public void LoadData(List<long> data)
        {
            //this.data = data;

            //Type t = typeof(UserSubmission);
            //PropertyInfo[] pcol = t.GetProperties();
            //for (int i = 0; i < data.Count; ++i)
            //{
            //    pcol[i].SetValue(this, data[i], null);
            //}

            //Submission ID
            sid = (long)data[0];
            //Problem ID
            pid = (long)data[1];
            //Verdict ID
            ver = (long)data[2];
            //Runtime
            run = (long)data[3];
            //Submission Time (unix timestamp)
            sbt = (long)data[4];
            //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
            lan = (long)data[5];
            //Submission Rank
            rank = (long)data[6];

            pnum = LocalDatabase.GetNumber(pid);
            ptitle = LocalDatabase.GetTitle(pnum);
        }
    }
}

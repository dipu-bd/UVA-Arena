using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

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
        //Submission Time (unix timestamp)
        public long sbt { get; set; }
        //Language ID (1=ANSI C, 2=Java, 3=C++, 4=Pascal, 5=C++11)
        public long lan { get; set; }
        //Submission Rank
        public long rank { get; set; }

        public string uname { get; set; }
        public string name { get; set; }
        public long pnum { get; set; }
        public string ptitle { get; set; }
        public List<long> data { get; set; }

        public bool IsAccepted()
        {
            return (ver == 90);
        }

        public void LoadData(List<long> data)
        {
            this.data = data;

            Type t = typeof(UserSubmission);
            PropertyInfo[] pcol = t.GetProperties();
            for (int i = 0; i < data.Count; ++i)
            {
                pcol[i].SetValue(this, data[i], null);
            }

            pnum = LocalDatabase.GetNumber(pid);
            ptitle = LocalDatabase.GetTitle(pnum);
        }

    }

    public class UserInfo
    {
        public UserInfo() { LastSID = 0; }
        public UserInfo(string user)
        {
            name = user;
            uname = user;
            subs = new List<List<long>>();
        }

        public string name { get; set; }
        public string uname { get; set; }
        public List<List<long>> subs { get; set; }

        public string uid { get; set; }
        public long LastSID { get; set; }

        public List<long> ACList;
        public List<UserSubmission> submissions;
        public Dictionary<long, UserSubmission> sidToSub;

        public UserSubmission GetSubmission(long sid)
        {
            if (sidToSub == null || !sidToSub.ContainsKey(sid))
                throw new NullReferenceException();
            return sidToSub[sid];
        }

        public string GetJsonData()
        {
            string format = "{\"name\":\"" + name + "\",\"uname\":\"" + uname + "\",\"subs\":";
            format += JsonConvert.SerializeObject(subs) + "}";
            return format;
        }

        public void Process()
        {
            LastSID = 0;
            uid = LocalDatabase.GetUserid(uname);

            ACList = new List<long>();
            submissions = new List<UserSubmission>();
            sidToSub = new Dictionary<long, UserSubmission>();

            ProcessListData(subs);
        }

        public void AddSubmissions(string json)
        {
            if (json == null) return;
            UserInfo ui = JsonConvert.DeserializeObject<UserInfo>(json);
            if (ui == null || ui.subs == null) return;

            name = ui.name;
            uname = ui.uname;
            ProcessListData(ui.subs, true);
        }

        private void ProcessListData(List<List<long>> allsub, bool addToDef = false)
        {
            if (subs == null) subs = new List<List<long>>();

            bool isdef = (this.uname == RegistryAccess.DefaultUsername);
            foreach (List<long> lst in allsub)
            {
                UserSubmission usub = new UserSubmission(lst);
                if (sidToSub.ContainsKey(usub.sid)) continue;

                usub.name = name;
                usub.uname = uname;

                submissions.Add(usub);
                sidToSub.Add(usub.sid, usub);
                if (addToDef) subs.Add(lst);

                if (this.LastSID < usub.sid) this.LastSID = usub.sid;

                //if accepted
                if (usub.IsAccepted() && !ACList.Contains(usub.pnum))
                {
                    ACList.Add(usub.pnum);
                    if (isdef)
                    {
                        ProblemInfo prob = LocalDatabase.GetProblem(usub.pnum);
                        if (prob != null) prob.solved = true;
                    }
                }
            }

            //sort by sid
            submissions.Sort((Comparison<UserSubmission>)
                delegate(UserSubmission a, UserSubmission b)
                {
                    return (int)(a.sid - b.sid);
                });
        }

    }
}

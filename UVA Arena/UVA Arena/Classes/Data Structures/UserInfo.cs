using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace UVA_Arena.Structures
{
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

        public Dictionary<long, bool> TryList;
        public List<UserSubmission> submissions;
        public Dictionary<long, UserSubmission> sidToSub;

        public string GetJSONData()
        {
            string format = "{\"name\":\"" + name + "\",\"uname\":\"" + uname + "\",\"subs\":";
            format += JsonConvert.SerializeObject(subs) + "}";
            return format;
        }
         
        public void Process()
        {
            LastSID = 0;
            uid = LocalDatabase.GetUserid(uname);

            TryList = new Dictionary<long, bool>();
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
        
        public bool IsTried(long pnum)
        {
            return TryList.ContainsKey(pnum);
        }

        public bool IsSolved(long pnum)
        {
            return IsTried(pnum) && TryList[pnum];
        }

        public bool IsTriedButUnsolved(long pnum)
        {
            return IsTried(pnum) && !TryList[pnum];
        }

        private void SetTried(long pnum, bool acc = false)
        {
            if (IsTried(pnum)) 
                TryList[pnum] = acc;
            else
                TryList.Add(pnum, acc);
        }
        
        private void ProcessListData(List<List<long>> allsub, bool addToDef = false)
        {
            if (subs == null) subs = new List<List<long>>();

            bool sortneed = false;
            bool isdef = (this.uname == RegistryAccess.DefaultUsername);
            foreach (List<long> lst in allsub)
            {
                UserSubmission usub = new UserSubmission(lst);
                if (sidToSub.ContainsKey(usub.sid))
                {
                    if (usub.isInQueue()) continue;
                    submissions.Remove(sidToSub[usub.sid]);
                    sidToSub.Remove(usub.sid);
                }

                usub.name = name;
                usub.uname = uname;
                submissions.Add(usub);
                sidToSub.Add(usub.sid, usub);
                sortneed = true;

                if (!usub.isInQueue())
                {
                    if (addToDef) subs.Add(lst);
                    if (this.LastSID < usub.sid) this.LastSID = usub.sid;
                }
                 
                SetTried(usub.pnum, usub.IsAccepted());
                if (isdef && usub.IsAccepted() && IsSolved(usub.pnum))
                {
                    ProblemInfo prob = LocalDatabase.GetProblem(usub.pnum);
                    if (prob != null) prob.solved = true;
                }
            }

            //sort by sid
            if (sortneed)
            {
                submissions.Sort((Comparison<UserSubmission>)
                   delegate(UserSubmission a, UserSubmission b)
                   {
                       return (int)(a.sid - b.sid);
                   });
            }
        }

    }
}

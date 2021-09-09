using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UVA_Arena.Structures
{
    public class UserInfo
    {
        public UserInfo()
        {
            LastSID = 0;
            subs = new List<List<long>>();
        }
        public UserInfo(string user)
        {
            name = user;
            uname = user;
            subs = new List<List<long>>();
            uid = LocalDatabase.GetUserid(user);
        }

        public string name { get; set; }
        public string uname { get; set; }
        public List<List<long>> subs { get; set; }

        public string uid { get; set; }
        public long LastSID { get; set; }

        public List<UserSubmission> submissions;
        public Dictionary<long, UserSubmission> TryList;
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

            submissions = new List<UserSubmission>();
            TryList = new Dictionary<long, UserSubmission>();
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

        public bool HasTried(long pnum)
        {
            return TryList != null && TryList.ContainsKey(pnum);
        }

        public bool IsSolved(long pnum)
        {
            return HasTried(pnum) && TryList[pnum].IsAccepted();
        }

        public bool TriedButUnsolved(long pnum)
        {
            return HasTried(pnum) && !TryList[pnum].IsAccepted();
        }

        private void SetTried(UserSubmission usub)
        {
            if (!HasTried(usub.pnum))
            {
                TryList.Add(usub.pnum, usub);
                return;
            }

            if (usub.IsInQueue()) return;
            if (TryList[usub.pnum].rank <= 0 || usub.rank < TryList[usub.pnum].rank)
            {
                TryList[usub.pnum] = usub;
            }
        }

        private void ProcessListData(List<List<long>> allsub, bool addToDef = false)
        {
            if (subs == null) subs = new List<List<long>>();

            bool needToSort = false;
            bool isdef = (this.uname == RegistryAccess.DefaultUsername);
            foreach (List<long> lst in allsub)
            {
                try
                {
                    UserSubmission usub = new UserSubmission(lst);

                    //remove usub if already existed
                    if (sidToSub.ContainsKey(usub.sid))
                    {
                        if (usub.IsInQueue()) continue;
                        submissions.Remove(sidToSub[usub.sid]);
                        sidToSub.Remove(usub.sid);
                    }

                    //set the properties to usub add add to list
                    usub.name = name;
                    usub.uname = uname;
                    submissions.Add(usub);
                    sidToSub.Add(usub.sid, usub);
                    needToSort = true;

                    //if usub is not in the queue add it
                    if (!usub.IsInQueue())
                    {
                        if (addToDef) subs.Add(lst);
                        if (this.LastSID < usub.sid)
                        {
                            this.LastSID = usub.sid;
                        }
                    }

                    SetTried(usub);
                    if (isdef && IsSolved(usub.pnum))
                    {
                        ProblemInfo prob = LocalDatabase.GetProblem(usub.pnum);
                        if (prob != null) prob.Solved = true;
                    }
                }
                catch { }
            }

            //sort by sid
            if (needToSort)
            {
                submissions.Sort((Comparison<UserSubmission>)
                   delegate (UserSubmission a, UserSubmission b)
                   {
                       return (int)(a.sid - b.sid);
                   });
            }
        }

    }
}

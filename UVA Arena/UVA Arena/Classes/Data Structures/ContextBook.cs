using System;
using System.Collections.Generic;
using System.Text;

namespace UVA_Arena.Structures
{
    public class ContextBook
    {
        public string title { get; set; }
        public List<SubGroup> arr { get; set; }
        public class SubGroup
        {
            public string title { get; set; }
            public List<List<object>> arr { get; set; }            
        }


        private void AddCategory(ProblemInfo pinfo, string cat)
        {
            if(!pinfo.tags.Contains(cat))
            {
                pinfo.tags.Add(cat);
                LocalDatabase.GetCategory(cat).Add(pinfo);
            }
        }

        public void Process()
        {
            if (!LocalDatabase.IsReady) return;
            
            foreach(SubGroup sub in arr)
            {
                foreach(List<object> list in sub.arr)
                {
                    string name = list[0].ToString();
                    for(int i = 1; i < list.Count; ++i)
                    {
                        long pnum = 0;
                        if(!long.TryParse(list[i].ToString(), out pnum)) continue;
                        ProblemInfo pinfo = LocalDatabase.GetProblem(Math.Abs(pnum));
                        if (pinfo == null) continue;
                                                
                        AddCategory(pinfo, title);
                        AddCategory(pinfo, sub.title);
                        AddCategory(pinfo, name);
                        pinfo.stared = (pnum < 0);
                        RegistryAccess.SetTags(pinfo.pnum, pinfo.tags);
                    }
                }
            }
        }
    }
}

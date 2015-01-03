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


        private static void AddCategory(ProblemInfo pinfo, string cat)
        {
            if (!pinfo.tags.Contains(cat))
            {
                pinfo.tags.Add(cat);
                var cl = LocalDatabase.GetCategory(cat);
                if (cl != null) cl.Add(pinfo);
            }
        }

        public void Process()
        {
            if (arr == null) return;

            if (!LocalDatabase.IsReady) return;
            
            foreach (SubGroup sub in arr)
            {
                if (sub.arr == null) continue;

                foreach (List<object> list in sub.arr)
                {
                    if (list == null) continue;

                    string name = (string)list[0];
                    for (int i = 1; i < list.Count; ++i)
                    {
                        long pnum = Math.Abs((long)list[i]);
                        if (!LocalDatabase.HasProblem(pnum)) continue;
                        
                        ProblemInfo pinfo = LocalDatabase.GetProblem(pnum);
                        AddCategory(pinfo, title);
                        AddCategory(pinfo, sub.title);
                        AddCategory(pinfo, name);
                        pinfo.stared = (!pnum.Equals(list[i]));
                        
                        RegistryAccess.SetTags(pinfo.pnum, pinfo.tags);
                    }
                }
            }
        }
    }
}

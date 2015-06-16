using System;
using System.Collections.Generic;

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
         
        public void Process()
        {
            if (arr == null) return;            
            string root = LocalDatabase.CatRoot + CategoryNode.SEPARATOR + title;

            foreach (SubGroup sub in arr)
            {
                if (sub.arr == null) continue;
                string child1 = root + CategoryNode.SEPARATOR + sub.title;

                foreach (List<object> list in sub.arr)
                {
                    if (list == null) continue;
                    string child2 = child1 + CategoryNode.SEPARATOR + (string)list[0];

                    for (int i = 1; i < list.Count; ++i)
                    {
                        long pnum = Math.Abs((long)list[i]);
                        if (!LocalDatabase.HasProblem(pnum)) continue;

                        ProblemInfo problem = LocalDatabase.GetProblem(pnum);
                        problem.stared = ((long)list[i]) < 0;
                        LocalDatabase.category_root[child2].AddProblem(problem);
                    }
                }
            }
        }
    }
}

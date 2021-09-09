using System.Collections.Generic;

namespace UVA_Arena.Structures
{
    public class CategoryProblem
    {
        public CategoryProblem(long pnum = 0, bool star = false, string note = "")
        {
            this.pnum = pnum;
            this.star = star;
            this.note = note;
        }

        public long pnum { get; set; }
        public bool star { get; set; }
        public string note { get; set; }
    }

    /// <summary>
    /// File System like structure for categorize problems
    /// </summary>
    public class CategoryNode
    {
        public CategoryNode(string name = "", string note = "", CategoryNode par = null)
        {
            this.name = name;
            this.note = note;
            this.Parent = par;
            this.problems = new List<CategoryProblem>();
            this.branches = new List<CategoryNode>();
        }

        //
        // Fields and properties
        //
        public string name { get; set; }
        public string note { get; set; }
        public List<CategoryProblem> problems { get; set; }
        public List<CategoryNode> branches { get; set; }

        /// <summary> Parent category node if exist </summary> 
        public CategoryNode Parent = null;
        /// <summary> List of all problems </summary>
        public Dictionary<long, ProblemInfo> allProbs = new Dictionary<long, ProblemInfo>();
        public Dictionary<long, string> problemToNote = new Dictionary<long, string>();

        /// <summary>
        /// Gets the level of current node int the tree
        /// </summary>
        public int Level
        {
            get
            {
                if (Parent == null)
                    return 0;
                else
                    return 1 + Parent.Level;
            }
        }
        public int Count
        {
            get { return allProbs.Count; }
        }

        //
        // Functions
        //
        public void ProcessData()
        {
            foreach (CategoryNode b in branches)
            {
                try
                {
                    b.Parent = this;
                    b.ProcessData();
                    AddProblemsFrom(b);
                }
                catch { }
            }
            foreach (CategoryProblem p in problems)
            {
                try
                {
                    ProblemInfo pinfo = LocalDatabase.GetProblem(p.pnum);
                    if (pinfo == null) continue;

                    if (!pinfo.categories.Contains(this))
                        pinfo.categories.Add(this);
                    AddProblem(pinfo, true);

                    if (p.star) pinfo.Starred = true;
                    if (problemToNote.ContainsKey(p.pnum))
                        problemToNote[p.pnum] = p.note;
                    else
                        problemToNote.Add(p.pnum, p.note);
                }
                catch { }
            }
            problems.Clear();
        }

        public void AddProblem(ProblemInfo p, bool force = false)
        {
            if (allProbs.ContainsKey(p.pnum))
            {
                if (force) allProbs[p.pnum] = p;
            }
            else
            {
                allProbs.Add(p.pnum, p);
            }
        }

        public void AddProblemsFrom(CategoryNode nod)
        {
            foreach (var v in nod.allProbs.Values)
            {
                AddProblem(v);
            }
        }

        public bool HasCategory(string name)
        {
            foreach (CategoryNode nod in branches)
            {
                if (nod.name == name) return true;
            }
            return false;
        }

        public bool RemoveCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            for (int i = 0; i < branches.Count; ++i)
            {
                if (branches[i].name == name)
                {
                    branches.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public string GetCategoryNote(long pnum)
        {
            string note = "";
            if (problemToNote.ContainsKey(pnum))
                note = problemToNote[pnum];
            if (note == "")
                note = "[This problem has no notes]";
            return note;
        }
    }
}

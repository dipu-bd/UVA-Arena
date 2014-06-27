using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public class TrieTree
    {
        private class Node
        {
            public Object val;
            public Dictionary<char, int> next;

            public Node(object val = null)
            {
                this.val = val;
                this.next = new Dictionary<char, int>();
            }
        }

        private List<Node> data;

        public void insert(string str, Object dat)
        {
            int ind = 0;

            for (int i = 0; i < str.Length; ++i)
            {
                char ch = str[i];
                if (data[ind].next.ContainsKey(ch))
                {
                    ind = data[ind].next[ch];
                }
                else
                {
                    data[ind].next.Add(ch, data.Count);
                    ind = data.Count;
                    data.Add(new Node());
                }
                if (i == str.Length - 1)
                {
                    data[ind].val = dat;
                }
            }
        }

        public Object find(string str)
        {
            int ind = 0;

            for (int i = 0; i < str.Length; ++i)
            {
                char ch = str[i];
                if (data[ind].next.ContainsKey(ch))
                {
                    ind = data[ind].next[ch];
                }
                else
                {
                    return null;
                }
                if (i == str.Length - 1)
                {
                    return data[ind].val;
                }
            }

            return null;
        }

        public bool contains(string str)
        {
            return find(str) != null;
        }

        public TrieTree()
        {
            data = new List<Node>();
            data.Add(new Node());
        }
    }

    public static class ProblemDatabase
    {
        private static TrieTree problem_id = new TrieTree();
        private static TrieTree problem_num = new TrieTree();
        public static List<ProblemList> problem_list;

        public static void SetProblemTitle(string pnum, string ptitle)
        {
            problem_num.insert(pnum, ptitle);
        }
        public static string GetProblemTitle(string pnum)
        {
            string n = problem_num.find(pnum).ToString();
            if (string.IsNullOrEmpty(n)) n = "-";
            return n;
        }

        public static void SetProblemNum(string pid, string pnum)
        {
            problem_id.insert(pid, pnum);
        }
        public static string GetProblemNum(string pid, string pnum)
        {
            string n = problem_id.find(pid).ToString();
            if (string.IsNullOrEmpty(n)) n = "-";
            return n;
        }

        /// <summary> Load the database from downloaded data </summary>
        /// <returns> False if fails to load, True if success </returns>
        public static bool LoadDatabase()
        {
            try
            {
                string path = LocalDirectory.ProblemDataFile;
                string text = File.ReadAllText(path);
                if (text.Length < 10) return false;

                List<List<string>> json = JsonConvert.DeserializeObject<List<List<string>>>(text);
                if (json == null || json.Count == 0) return false;

                problem_list = new List<ProblemList>();
                foreach (List<string> lst in json)
                {
                    ProblemList plist = new ProblemList(lst);
                    problem_list.Add(plist);
                    SetProblemNum(plist.pid.ToString(), plist.pnum.ToString());
                    SetProblemTitle(plist.pnum.ToString(), plist.ptitle);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace UVA_Arena
{
    public class TrieTree
    {
        private class Node
        {
            public int to;
            public Object val;

            public Node() { to = -1; val = null; }
            public Node(int to) { this.to = to; }
        }

        private List<Dictionary<char, Node>> data;

        public void insert(string str, Object dat)
        { 
            int ind = 0;
            
            for(int i = 0; i < str.Length; ++i)
            {
                char ch = str[i];
                if (data[ind].ContainsKey(ch))
                {
                    ind = data[ind][ch].to;
                }
                else
                {
                    data.Add(new Dictionary<char, Node>());                    
                    data[ind].Add(ch, new Node(data.Count));
                    ind = data.Count;
                }
                if (i == str.Length - 1)
                {
                    data[ind][ch].val = dat;
                }
            }
        }

        public Object find(string str)
        {
            int ind = 0;
            
            for (int i = 0; i < str.Length; ++i)
            {
                char ch = str[i];
                if (data[ind].ContainsKey(ch))
                    ind = data[ind][ch].to;
                else return null;

                if (i == str.Length - 1)
                    return data[ind][ch].val;
            }

            return null;
        }

        public bool contains(string str)
        {
            return find(str) != null;
        }
        
        public TrieTree()
        {
            data = new List<Dictionary<char, Node>>();
        }
    }

    public static class ProblemDatabase
    { 
        private static TrieTree problem_id = new TrieTree();
        private static TrieTree problem_num = new TrieTree();

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
    }
}

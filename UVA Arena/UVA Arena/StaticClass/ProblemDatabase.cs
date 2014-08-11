using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    sealed class ProblemDatabase
    {
        public static List<ProblemInfo> problem_list;
        public static Dictionary<long, long> problem_id;
        public static Dictionary<long, ProblemInfo> problem_num;
        public static Dictionary<long, List<ProblemInfo>> problem_vol;
        public static Dictionary<string, List<ProblemInfo>> problem_cat;

        /// <summary> Save problem number for given problem id </summary>
        public static void SetNumber(long pid, long pnum)
        {
            if (problem_id == null) return;
            if (problem_id.ContainsKey(pid)) return;
            problem_id.Add(pid, pnum);
        }
        /// <summary> Get problem number for given problem id </summary>
        public static long GetNumber(long pid)
        {
            if (problem_id == null) return 0;
            if (!problem_id.ContainsKey(pid)) return -1;
            return problem_id[pid];
        }

        /// <summary> Get whether given problem number exist </summary>
        public static bool HasProblem(long pnum)
        {
            if (problem_num == null) return false;
            return problem_num.ContainsKey(pnum);
        }
        /// <summary> Save problem info for given problem number </summary>
        public static void SetProblem(long pnum, ProblemInfo plist)
        {
            if (problem_num == null) return;
            if (HasProblem(pnum)) 
                problem_num[pnum] = plist;
            else 
                problem_num.Add(pnum, plist);
        }
        /// <summary> Get problem info for given problem number </summary>
        public static ProblemInfo GetProblem(long pnum)
        {
            if (!HasProblem(pnum)) return null;
            return problem_num[pnum];
        }
        
        /// <summary> Get problem title for given problem number </summary>
        public static string GetTitle(long pnum)
        {
            if (!HasProblem(pnum)) return "-";
            return GetProblem(pnum).ptitle;
        }
        /// <summary> Get problem id for given problem number </summary>
        public static long GetProblemID(long pnum)
        {
            if (!HasProblem(pnum)) return 0;
            return GetProblem(pnum).pid;
        }

        /// <summary> Get problem list for given volume </summary>
        public static List<ProblemInfo> GetVolume(long vol)
        {
            if (problem_vol == null) return null;
            if (problem_vol.ContainsKey(vol)) return problem_vol[vol];
            problem_vol.Add(vol, new List<ProblemInfo>());
            return problem_vol[vol];
        }
        /// <summary> Get problem list for given catagory </summary>
        public static List<ProblemInfo> GetCatagory(string cat)
        {
            if (problem_cat == null) return null;
            if (problem_cat.ContainsKey(cat)) return problem_cat[cat];
            problem_cat.Add(cat, new List<ProblemInfo>());
            return problem_cat[cat];
        }
        
        /// <summary> Load the database from downloaded data </summary>
        public static void LoadDatabase()
        {
            bool queue = ThreadPool.QueueUserWorkItem(RunLoadAsync);
            if(!queue) RunLoadAsync(null);            
        }
         
        private static void RunLoadAsync(object state)
        {            
            try
            {                     
                //get problem list from file
                string text = File.ReadAllText(LocalDirectory.ProblemData);
                if (text.Length < 10) throw new Exception("Problem database was empty");

                //load new problem list
                List<List<string>> data = JsonConvert.DeserializeObject<List<List<string>>>(text);
                if (data == null || data.Count == 0) throw new Exception("Problem data file is empty");
                
                LoadList(data);
                LoadOthers();
                
                data.Clear();
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Database");
            }

            Interactivity.ProblemDatabaseUpdated();
            System.GC.Collect();
        }

        private static void LoadList(List<List<string>> datalist)
        {
            //set values 
            problem_list = new List<ProblemInfo>();
            List<long> DacuAll = new List<long>();
            Dictionary<int, long> MaxDacu = new Dictionary<int, long>();

            //Load problem from list
            foreach (List<string> lst in datalist)
            {
                ProblemInfo plist = new ProblemInfo(lst);
                problem_list.Add(plist);

                if (!DacuAll.Contains(plist.dacu)) DacuAll.Add(plist.dacu);
                if (!MaxDacu.ContainsKey(plist.volume)) MaxDacu.Add(plist.volume, 0);
                MaxDacu[plist.volume] = Math.Max(MaxDacu[plist.volume], plist.dacu);
            }

            //set problem level
            DacuAll.Sort();
            const double r = 0.4;
            double N = DacuAll.Count;
            foreach (ProblemInfo plist in problem_list)
            {
                double d = DacuAll.IndexOf(plist.dacu);
                double m = DacuAll.IndexOf(MaxDacu[plist.volume]);
                double rank = r * (1 - d / N) + (1 - r) * (m - d) / N;
                plist.level = 1 + (int)(rank * 10);
                if (plist.level > 10) plist.level = 10;
            }

            DacuAll.Clear();
            MaxDacu.Clear();
        }

        private static void LoadOthers()
        {
            problem_id = new Dictionary<long, long>();
            problem_num = new Dictionary<long, ProblemInfo>();
            problem_vol = new Dictionary<long, List<ProblemInfo>>();
            problem_cat = new Dictionary<string, List<ProblemInfo>>();
            foreach (ProblemInfo plist in problem_list)
            {
                SetProblem(plist.pnum, plist);
                SetNumber(plist.pid, plist.pnum);
                GetVolume(plist.volume).Add(plist);
                foreach (string cat in plist.tags)                
                    GetCatagory(cat).Add(plist);                
            }
        } 

    }
}

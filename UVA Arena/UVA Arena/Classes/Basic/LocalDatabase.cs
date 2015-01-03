using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    internal static class LocalDatabase
    {
        public static bool IsReady = true;
        public static List<ProblemInfo> problem_list;
        public static Dictionary<long, long> problem_id;
        public static Dictionary<long, ProblemInfo> problem_num;
        public static Dictionary<long, List<ProblemInfo>> problem_vol;
        public static Dictionary<string, List<ProblemInfo>> problem_cat;

        /// <summary> Userinfo of default user </summary>
        public static UserInfo DefaultUser;
        /// <summary> Dictionary of [Username -> Userid] values </summary>
        /// <remarks> initialized in the main function </remarks>
        public static Dictionary<string, string> usernames;

        /// <summary>
        /// True if local database is successfully loaded once
        /// </summary>
        public static bool IsAvailable { get; private set; }

        #region Loader Functions

        /// <summary> Load the database from downloaded data </summary>
        public static void LoadDatabase()
        {
            RunLoadAsync(true);
        }

        public static void RunLoadAsync(object background)
        {
            if (!IsReady) return;

            if ((bool)background)
            {
                bool back = System.Threading.ThreadPool.QueueUserWorkItem(RunLoadAsync, false);
                if (back) return;
            }

            try
            {
                IsReady = false;

                //initialize global values
                problem_list = new List<ProblemInfo>();
                problem_id = new Dictionary<long, long>();
                problem_num = new Dictionary<long, ProblemInfo>();
                problem_vol = new Dictionary<long, List<ProblemInfo>>();
                problem_cat = new Dictionary<string, List<ProblemInfo>>();
                DefaultUser = new UserInfo(RegistryAccess.DefaultUsername);

                //get object data from json data
                string text = File.ReadAllText(LocalDirectory.GetProblemDataFile());
                var data = JsonConvert.DeserializeObject<List<List<object>>>(text);
                if (data == null || data.Count == 0)
                    throw new NullReferenceException("Problem database was empty");

                //load all lists from object data
                LoadList(data);
                LoadOthers();
                
                data.Clear();
                IsAvailable = true;
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Database|RunLoadAsync()");
                if (!IsAvailable) Internet.Downloader.DownloadProblemDatabase();
            }

            //load default user after database updated
            LoadDefaultUser();

            IsReady = true;
            Interactivity.ProblemDatabaseUpdated();
        }

        private static void LoadList(List<List<object>> datalist)
        {
            //Load problem from list
            foreach (List<object> lst in datalist)
            {
                ProblemInfo plist = new ProblemInfo();
                plist.SetData(lst);
                problem_list.Add(plist);

                SetProblem(plist.pnum, plist);
                SetNumber(plist.pid, plist.pnum);
                GetVolume(plist.volume).Add(plist);
                foreach (string cat in plist.tags)
                {
                    GetCategory(cat).Add(plist);
                }
            }

            //set favorites
            foreach (long pnum in RegistryAccess.FavoriteProblems)
            {
                if (HasProblem(pnum))
                {
                    GetProblem(pnum).marked = true;
                }
            }
        }

        private static void LoadOthers()
        {
            if (problem_list.Count < 10) return;

            //get all dacu
            SortedDictionary<long, int> AllDacu = new SortedDictionary<long, int>();
            foreach (ProblemInfo plist in problem_list)
            {
                if (AllDacu.ContainsKey(plist.dacu))
                    AllDacu[plist.dacu]++;
                else
                    AllDacu.Add(plist.dacu, 1);
            }

            //cumulative sum of all dacu            
            int last = 0;
            var it = AllDacu.GetEnumerator();
            Dictionary<long, int> sum = new Dictionary<long, int>();
            while (it.MoveNext())
            {
                last += it.Current.Value;
                sum.Add(it.Current.Key, last);
            }
            it.Dispose();

            //set problem level 
            int product = problem_list.Count / 10;
            foreach (ProblemInfo plist in problem_list)
            {
                int pos = sum[plist.dacu];
                double rank = 10 * (1 - (double)pos / problem_list.Count);
                if (pos + 50 > problem_list.Count) rank -= 1; //among top 50
                else if (pos + 100 > problem_list.Count) rank -= 0.5; //among top 100

                double ac = plist.total <= 0 ? 0 : (double)plist.ac / plist.total;
                if (ac > 0.6) rank -= 1;
                else if (ac > 0.4) rank -= 0.5;
                if (2 * ac < 1 && pos + 100 < problem_list.Count)
                    rank += 2 * (1 - 2 * ac);
                plist.level = 2 + rank;
            }
        }

        public static void LoadDefaultUser()
        {
            try
            {
                string user = RegistryAccess.DefaultUsername;
                string file = LocalDirectory.GetUserSubPath(user);
                string data = File.ReadAllText(file);
                DefaultUser = JsonConvert.DeserializeObject<UserInfo>(data);
                DefaultUser.Process();
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "LocalDatabase|LoadDefaultUser()");
            }
        }

        /// <summary>
        /// Load downloaded categories
        /// </summary>
        /// <param name="wait">True to wait until problem databse is not ready</param>
        public static void LoadCategories()
        {
            try
            {
                string file = LocalDirectory.GetCategoryPath();
                string data = File.ReadAllText(file);
                List<ContextBook> catlist = JsonConvert.DeserializeObject<List<ContextBook>>(data);
                foreach (ContextBook book in catlist)
                {
                    book.Process();
                }
                Logger.Add("Category updated", "LocalDatabase|LoadCategory()");
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "LocalDatabase|LoadCategory()");
            }
        }

        #endregion

        #region Other Functions

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
            if (problem_id == null) return -1;
            if (!problem_id.ContainsKey(pid)) return 0;
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
            if (HasProblem(pnum)) problem_num[pnum] = plist;
            else problem_num.Add(pnum, plist);
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
        /// <summary> Get problem list for given category </summary>
        public static List<ProblemInfo> GetCategory(string cat)
        {
            if (problem_cat == null) return null;
            if (problem_cat.ContainsKey(cat)) return problem_cat[cat];
            problem_cat.Add(cat, new List<ProblemInfo>());
            return problem_cat[cat];
        }

        /// <summary> check if this user contains in the list </summary>
        public static bool ContainsUser(string user)
        {
            return (usernames != null && usernames.ContainsKey(user));
        }
        /// <summary> get user id from name </summary>
        public static string GetUserid(string name)
        {
            if (!ContainsUser(name)) return "-";
            return usernames[name];
        }

        #endregion

    }
}

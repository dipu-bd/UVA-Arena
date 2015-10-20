using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public static class LocalDatabase
    {
        public const string VolRoot = "[Volumes]";
        public const string CatRoot = "Categories";
        public const string NodeRoot = "Root";

        public static bool IsReady = true;
        public static CategoryNode categoryRoot;
        public static List<ProblemInfo> problemList;
        public static SortedDictionary<long, long> problemId;
        public static SortedDictionary<long, ProblemInfo> problemNum;


        /// <summary> User-info of default user </summary>
        public static UserInfo DefaultUser;

        /// <summary> Dictionary of [User-name -> User-id] values </summary>
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

        public static bool UpdateAll()
        {
            const double PROBLEM_ALIVE_DAY = 1;
            const double USER_SUB_ALIVE_DAY = 0.5;

            bool result = true;

            //if database file is too old redownload            
            string file = LocalDirectory.GetProblemInfoFile();
            if (LocalDirectory.GetFileSize(file) < 100 ||
                (new TimeSpan(
                    DateTime.Now.Ticks - new FileInfo(file).LastWriteTime.Ticks
                    ).TotalDays > PROBLEM_ALIVE_DAY))
            {
                UVA_Arena.Internet.Downloader.DownloadProblemDatabase();
                result = false;
            }

            //update user submissions if not available
            if (LocalDatabase.ContainsUser(RegistryAccess.DefaultUsername))
            {
                file = LocalDirectory.GetUserSubPath(RegistryAccess.DefaultUsername);
                if (LocalDirectory.GetFileSize(file) < 50 ||
                     (new TimeSpan(
                    DateTime.Now.Ticks - new FileInfo(file).LastWriteTime.Ticks
                    ).TotalDays > USER_SUB_ALIVE_DAY))
                {
                    long sid = 0;
                    if (LocalDatabase.DefaultUser != null)
                        sid = LocalDatabase.DefaultUser.LastSID;
                    UVA_Arena.Internet.Downloader.DownloadDefaultUserInfo(sid);
                }
            }

            //download category index if too old  
            UVA_Arena.Internet.Downloader.DownloadCategoryIndex();

            return result;
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
                problemList = new List<ProblemInfo>();
                problemId = new SortedDictionary<long, long>();
                problemNum = new SortedDictionary<long, ProblemInfo>();
                categoryRoot = new CategoryNode("Root", "All Categories");

                //get object data from json data
                string text = File.ReadAllText(LocalDirectory.GetProblemInfoFile());
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

            //load categories
            LoadCategories();

            //load default user
            LoadDefaultUser();

            IsReady = true;
            Interactivity.CategoryDataUpdated();
            Interactivity.ProblemDatabaseUpdated();
        }

        private static void LoadList(List<List<object>> datalist)
        {
            SortedDictionary<int, List<ProblemInfo>> catData
                = new SortedDictionary<int, List<ProblemInfo>>();

            //Load problem from list
            foreach (List<object> lst in datalist)
            {
                ProblemInfo plist = new ProblemInfo(lst);
                problemList.Add(plist);

                //set the file size
                string file = LocalDirectory.GetProblemHtml(plist.pnum);
                if (File.Exists(file))
                    plist.FileSize = (new System.IO.FileInfo(file)).Length;

                SetProblem(plist.pnum, plist);
                SetNumber(plist.pid, plist.pnum);

                //Categorize
                if (!catData.ContainsKey(plist.Volume))
                {
                    catData.Add(plist.Volume, new List<ProblemInfo>());
                }
                catData[plist.Volume].Add(plist);
            }

            //add volume category
            var volCat = new CategoryNode("Volumes", "Problem list by volumes");
            categoryRoot.branches.Add(volCat);
            foreach (var data in catData.Values)
            {
                string vol = string.Format("Volume {0:000}", data[0].Volume);
                var nod = new CategoryNode(vol, "", volCat);
                volCat.branches.Add(nod);
                foreach (var p in data)
                {
                    nod.problems.Add(new CategoryProblem(p.pnum));
                }
            }
            volCat.ProcessData();
        }

        private static void LoadOthers()
        {
            if (problemList.Count < 10) return;

            //set favorites
            foreach (long pnum in RegistryAccess.FavoriteProblems)
            {
                if (HasProblem(pnum))
                {
                    GetProblem(pnum).Marked = true;
                }
            }
        }

        public static void LoadDefaultUser()
        {
            try
            {
                string user = RegistryAccess.DefaultUsername;
                if (!ContainsUser(user)) throw new KeyNotFoundException();
                string file = LocalDirectory.GetUserSubPath(user);
                string data = File.ReadAllText(file);
                DefaultUser = JsonConvert.DeserializeObject<UserInfo>(data);
                if (DefaultUser != null) DefaultUser.Process();
                else DefaultUser = new UserInfo(RegistryAccess.DefaultUsername);
            }
            catch (Exception ex)
            {
                DefaultUser = new UserInfo(RegistryAccess.DefaultUsername);
                Logger.Add(ex.Message, "LocalDatabase|LoadDefaultUser()");
            }
        }

        public static void LoadCategories()
        {
            var index = Functions.GetCategoryIndex();
            foreach (string key in index.Keys)
            {
                bool ok = LoadCategoryData(key);
                if (ok)
                {
                    RegistryAccess.SetCategoryVersion(key, index[key]);
                }
                else
                {
                    RegistryAccess.SetCategoryVersion(key, 0);
                }
            }
        }

        public static bool LoadCategoryData(string key)
        {
            try
            {
                string file = LocalDirectory.GetCategoryDataFile(key);
                string json = File.ReadAllText(file);
                var node = (CategoryNode)JsonConvert.DeserializeObject<CategoryNode>(json);
                node.ProcessData();
                categoryRoot.RemoveCategory(node.name);
                categoryRoot.branches.Add(node);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "LoadCategoryData(" + key + ")");
                return false;
            }
        }

        #endregion Loader Functions

        #region Other Functions

        /// <summary> Save problem number for given problem id </summary>
        public static void SetNumber(long pid, long pnum)
        {
            if (problemId == null) return;
            if (problemId.ContainsKey(pid)) return;
            problemId.Add(pid, pnum);
        }

        /// <summary> Get problem number for given problem id </summary>
        public static long GetNumber(long pid)
        {
            if (problemId == null) return -1;
            if (!problemId.ContainsKey(pid)) return 0;
            return problemId[pid];
        }

        /// <summary> Get whether given problem number exist </summary>
        public static bool HasProblem(long pnum)
        {
            if (problemNum == null) return false;
            return problemNum.ContainsKey(pnum);
        }

        /// <summary> Save problem info for given problem number </summary>
        public static void SetProblem(long pnum, ProblemInfo plist)
        {
            if (problemNum == null) return;
            if (HasProblem(pnum)) problemNum[pnum] = plist;
            else problemNum.Add(pnum, plist);
        }

        /// <summary> Get problem info for given problem number </summary>
        public static ProblemInfo GetProblem(long pnum)
        {
            if (!HasProblem(pnum)) return null;
            return problemNum[pnum];
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

        /// <summary> check if this user contains in the list </summary>
        public static bool ContainsUser(string user)
        {
            return (!string.IsNullOrEmpty(user) && usernames.ContainsKey(user));
        }

        /// <summary> get user id from name </summary>
        public static string GetUserid(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;
            if (!ContainsUser(name)) return "-";
            return usernames[name];
        }

        /// <summary>
        /// Get problem number from file name
        /// </summary>
        /// <param name="name">Name of the file</param>
        /// <returns> -1 if problem name is not recognized</returns>
        public static long GetProblemNumber(string name)
        {
            if (string.IsNullOrEmpty(name)) return -1;
            long res = -1;
            var m = System.Text.RegularExpressions.Regex.Match(name, @"\d+");
            if (m.Success)
            {
                string num = name.Substring(m.Index, m.Length);
                long.TryParse(num, out res);
                if (!HasProblem(res)) res = -1;
            }
            return res;
        }

        #endregion Other Functions
    }
}
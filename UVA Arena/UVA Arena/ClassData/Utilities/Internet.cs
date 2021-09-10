using System;
using System.Collections.Generic;

namespace UVA_Arena.Internet
{
    internal static class Downloader
    {
        #region Base Downloader

        private static int _highend = 0;
        private static int _normalend = 0;
        public static DownloadTask CurrentTask;
        public static List<DownloadTask> DownloadQueue = new List<DownloadTask>();

        //
        // Download Queue
        //
        public static void Enqueue(DownloadTask task)
        {
            if (task == null) return;

            task.Status = ProgressStatus.Waiting;
            switch (task.TaskPriority)
            {
                case Priority.Low:
                    DownloadQueue.Add(task);
                    break;
                case Priority.Normal:
                    DownloadQueue.Insert(_normalend, task);
                    ++_normalend;
                    break;
                case Priority.High:
                    DownloadQueue.Insert(_highend, task);
                    ++_normalend;
                    ++_highend;
                    break;
            }
        }

        public static void Dequeue()
        {
            if (DownloadQueue.Count == 0) return;

            DownloadTask task = DownloadQueue[0];
            if (task.Status == ProgressStatus.Running ||
                task.Status == ProgressStatus.Waiting) return;

            switch (task.TaskPriority)
            {
                case Priority.Normal:
                    --_normalend;
                    break;
                case Priority.High:
                    --_highend;
                    --_normalend;
                    break;
            }

            task.Dispose();
            DownloadQueue.RemoveAt(0);
        }

        #endregion

        #region Queue Manager

        private static bool __ContinueDownload = true;

        public static void StartDownload()
        {
            __ContinueDownload = true;
            DownloadNext();
        }

        public static void StopDownload()
        {
            __ContinueDownload = false;
            if (CurrentTask != null && CurrentTask.Status != ProgressStatus.Disposed)
                CurrentTask.Cancel();
        }

        public static void DownloadNext()
        {
            if (!__ContinueDownload || IsBusy()) return;

            //get next task
            Dequeue();
            if (DownloadQueue.Count == 0) return;
            CurrentTask = DownloadQueue[0];

            //if already canceled, then skip it
            if (CurrentTask.Status == ProgressStatus.Cancelled)
            {
                CurrentTask.ReportComplete();
                DownloadNext();
                return;
            }

            //run task         
            CurrentTask.Download();
        }

        #endregion

        #region Add To Download Functions

        public static DownloadTask DownloadAsync(DownloadTask task, Priority priority = Priority.Normal)
        {
            task.TaskPriority = priority;
            Enqueue(task);
            DownloadNext();
            return task;
        }

        public static DownloadTask DownloadStringAsync(
            string url,
            object token = null,
            Priority priority = Priority.High,
            DownloadTaskHandler progress = null,
            DownloadTaskHandler completed = null,
            int retry = 0)
        {
            DownloadTask task = new DownloadTask(url, null, token, retry);
            task.ProgressChangedEvent += progress;
            task.DownloadCompletedEvent += completed;
            return DownloadAsync(task, priority);
        }

        public static DownloadTask DownloadFileAsync(
            string url,
            string file,
            object token = null,
            Priority priority = Priority.Normal,
            DownloadTaskHandler progress = null,
            DownloadTaskHandler completed = null,
            int retry = 0)
        {
            DownloadTask task = new DownloadTask(url, file, token, retry);
            task.ProgressChangedEvent += progress;
            task.DownloadCompletedEvent += completed;
            return DownloadAsync(task, priority);
        }

        #endregion

        #region UserID Downloader

        public static void DownloadUserid(string name, Internet.DownloadTaskHandler complete)
        {
            string url = Config.uHuntBaseUrl + "/api/uname2uid/" + name;
            DownloadTask dt = new DownloadTask(url, null, name, 2);

            if (LocalDatabase.ContainsUser(name))
            {
                dt.Result = LocalDatabase.GetUserid(name);
                dt.Status = ProgressStatus.Completed;
                if (complete != null) complete(dt);
            }
            else
            {
                dt.DownloadCompletedEvent += __DownloadUseridCompleted;
                if (complete != null) dt.DownloadCompletedEvent += complete;
                DownloadAsync(dt, Priority.High);
            }
        }

        private static void __DownloadUseridCompleted(DownloadTask task)
        {
            string uid = (string)task.Result;
            string name = (string)task.Token;

            task.Status = ProgressStatus.Failed;
            if (string.IsNullOrEmpty(uid))
                task.Error = new Exception("Connection error. Retry please.");
            else if (uid.Trim() == "0")
                task.Error = new Exception(name + " doesn't exist.");
            else if (task.Error == null)
                task.Status = ProgressStatus.Completed;

            if (task.Status == ProgressStatus.Completed)
            {
                RegistryAccess.AddUserid(name, uid);
                string msg = "Username downloaded : {0} = {1}";
                Interactivity.SetStatus(string.Format(msg, name, uid));
            }
        }

        #endregion

        #region Problem Database and Category Downloader

        //
        // Problem Database
        //
        private static bool _DownloadingProblemDatabase = false;

        public static void DownloadProblemDatabase()
        {
            if (_DownloadingProblemDatabase) return;
            _DownloadingProblemDatabase = true;

            //problem database
            string url = Config.uHuntBaseUrl + "/api/p";
            string alternate = "https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/problems.json";
            string file = LocalDirectory.GetProblemInfoFile();
            DownloadFileAsync(url, file, alternate, Priority.High,
                __DownloadProblemDatabaseProgress, __DownloadProblemDatabaseCompleted, 0);
        }

        private static void __DownloadProblemDatabaseCompleted(DownloadTask task)
        {
            _DownloadingProblemDatabase = false;

            string msg = "Failed to update problem list.";
            if (task.Status == ProgressStatus.Completed)
            {
                LocalDatabase.LoadDatabase();
                msg = "Problem list is successfully updated.";
                Logger.Add("Downloaded problem database file", "__DownloadProblemDatabaseCompleted");
            }
            else if (task.Error != null)
            {
                if (task.Token == null)
                {
                    LocalDatabase.LoadDatabase();
                    Logger.Add(task.Error.Message, "__DownloadProblemDatabaseCompleted");
                }
                else
                {
                    DownloadFileAsync((string)task.Token, task.FileName, null, Priority.High,
                        __DownloadProblemDatabaseProgress, __DownloadProblemDatabaseCompleted, 0);
                }
            }

            Interactivity.SetStatus(msg);
            Interactivity.SetProgress(0);
        }

        private static void __DownloadProblemDatabaseProgress(DownloadTask task)
        {
            string msg = "Downloading problem list... [{0}/{1} completed]";
            msg = string.Format(msg, Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.SetStatus(msg);
            Interactivity.SetProgress(task.ProgressPercentage);
        }

        //
        // Problem Category Index
        //
        public static void DownloadCategoryIndex()
        {
            //problem category index
            string url = "https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/INDEX";
            string file = LocalDirectory.GetCategoryIndexFile();
            DownloadFileAsync(url, file, null, Priority.Normal,
                __DownloadCategoryIndexProgress, __DownloadProblemCategoryCompleted, 1);
        }
        private static void __DownloadCategoryIndexProgress(DownloadTask task)
        {
            string msg = "Downloading category index... [{0}/{1} completed]";
            msg = string.Format(msg, Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.SetStatus(msg);
            Interactivity.SetProgress(task.ProgressPercentage);
        }

        private static void __DownloadProblemCategoryCompleted(DownloadTask task)
        {
            string msg = "Failed to downloaded category list.";
            if (task.Status == ProgressStatus.Completed)
            {
                DownloadCategoryIndexFiles();
                msg = "Downloaded category list.";
                Logger.Add("Downloaded problem's categories.", "__DownloadProblemCategoryCompleted");
            }
            else if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "__DownloadProblemCategoryCompleted");
            }

            Interactivity.SetStatus(msg);
            Interactivity.SetProgress(0);
        }


        //
        // Download Category files
        //
        public static void DownloadCategoryIndexFiles()
        {
            string url = "https://raw.githubusercontent.com/dipu-bd/uva-problem-category/master/data/";

            var index = Functions.GetCategoryIndex();
            foreach (string key in index.Keys)
            {
                long version = RegistryAccess.GetCategoryVersion(key);
                if (version < index[key])
                {
                    //problem category file      
                    string file = LocalDirectory.GetCategoryDataFile(key);
                    DownloadFileAsync(url + key, file, key, Priority.Low,
                        __DownloadCategoryFileProgress, __DownloadCategoryFileCompleted, 1);
                }
            }
        }

        private static void __DownloadCategoryFileProgress(DownloadTask task)
        {
            string msg = "Downloading category file: {2} ... [{0}/{1} completed]";
            msg = string.Format(msg, Functions.FormatMemory(task.Received),
                Functions.FormatMemory(task.Total), task.Token);
            Interactivity.SetStatus(msg);

            int percent = task.ProgressPercentage;
            Interactivity.SetProgress(task.ProgressPercentage);
        }

        private static void __DownloadCategoryFileCompleted(DownloadTask task)
        {
            string msg = "Failed to downloaded category list.";
            if (task.Status == ProgressStatus.Completed)
            {
                LocalDatabase.LoadCategoryData((string)task.Token);
                Interactivity.CategoryDataUpdated();

                msg = "Downloaded Category file: " + (string)task.Token;
                Logger.Add(msg, "__DownloadCategoryFileCompleted");
            }
            else if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "__DownloadCategoryFileCompleted");
            }

            Interactivity.SetStatus(msg);
            Interactivity.SetProgress(0);
        }

        //
        // Download User Info
        //

        public static void DownloadDefaultUserInfo(long LastSID = 0)
        {
            //user submission info
            string url = Config.uHuntBaseUrl + "/api/subs-user/{0}/{1}";
            url = string.Format(url, LocalDatabase.GetUserid(RegistryAccess.DefaultUsername), LastSID);
            string file = LocalDirectory.GetUserSubPath(RegistryAccess.DefaultUsername);
            DownloadFileAsync(url, file, RegistryAccess.DefaultUsername, Priority.Normal,
                __DownloadUserInfoProgress, __DownloadUserInfoCompleted, 1);
        }

        private static void __DownloadUserInfoProgress(DownloadTask task)
        {
            string msg = "Downloading {0}'s submissions... [{1}/{2} completed]";
            msg = string.Format(msg, task.Token,
                Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.SetStatus(msg);

            int percent = task.ProgressPercentage;
            Interactivity.SetProgress(task.ProgressPercentage);
        }

        private static void __DownloadUserInfoCompleted(DownloadTask task)
        {
            string msg = "Failed to downloaded user submissions.";
            if (task.Status == ProgressStatus.Completed)
            {
                LocalDatabase.LoadDefaultUser();
                msg = "Downloaded user submissions.";
            }
            else if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "__DownloadUserInfoCompleted");
            }

            Interactivity.SetStatus(msg);
            Interactivity.SetProgress(0);
        }

        #endregion

        #region Utilities

        public static bool IsInternetConnected()
        {
            long dwConnectionFlags = 0;
            if (NativeMethods.InternetGetConnectedState(dwConnectionFlags, 0))
                return (NativeMethods.InternetAttemptConnect(0) == 0);
            else
                return false;
        }

        public static bool IsBusy()
        {
            if (CurrentTask == null) return false;
            if (CurrentTask.Status == ProgressStatus.Disposed) return false;
            if (CurrentTask.webClient == null) return false;
            return CurrentTask.webClient.IsBusy;
        }

        #endregion

    }

}

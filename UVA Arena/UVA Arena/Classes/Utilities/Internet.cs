using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace UVA_Arena.Internet
{
    public enum Priority
    {
        High = 0,
        Normal = 1,
        Low = 2
    }

    public enum ProgressStatus
    {
        Waiting,
        Running,
        Cancelled,
        Completed,
        Failed,
        Disposed
    }

    public delegate void DownloadTaskHandler(DownloadTask sender);

    public class DownloadTask : IComparer<DownloadTask>, IDisposable
    {
        #region Properties and Variables

        public Uri Url { get; set; }
        public object Token { get; set; }
        public string Result { get; set; }
        public string FileName { get; set; }
        public string TempFileName { get; private set; }

        public long Total { get; set; }
        public long Received { get; set; }
        public int ProgressPercentage { get; set; }
        public Exception Error { get; set; }
        public Priority TaskPriority { get; set; }
        public ProgressStatus Status { get; set; }

        public DateTime StartedAt { get; set; }
        public TimeSpan TimeElapsed { get { return (DateTime.Now.Subtract(StartedAt)); } }

        public bool IsSaveToFile { get; set; }
        public WebClient webClient;

        public event DownloadTaskHandler ProgressChangedEvent;
        public event DownloadTaskHandler DownloadCompletedEvent;

        #endregion

        #region Constructor and Necessary Functions

        public DownloadTask(string url = null, string file = null, object token = null)
        {
            FileName = file;
            Token = token;
            IsSaveToFile = (file != null);
            if (url != null) Url = new Uri(url);
            TaskPriority = Priority.Normal;
            Status = ProgressStatus.Waiting;
            TempFileName = LocalDirectory.GetTempFile();
        }

        public void Dispose()
        {            
            try
            {
                File.Delete(TempFileName);
                Status = ProgressStatus.Disposed;
                webClient.Dispose();
                GC.SuppressFinalize(this);
            }
            catch { }
        }

        public int Compare(DownloadTask a, DownloadTask b)
        {
            return ((int)a.TaskPriority - (int)b.TaskPriority);
        }

        #endregion

        #region Custom Methods

        public void Cancel()
        {
            Status = ProgressStatus.Cancelled;
            if (webClient != null && webClient.IsBusy)
                webClient.CancelAsync();
        }

        public void ReportComplete()
        {
            if (DownloadCompletedEvent != null)
                DownloadCompletedEvent(this);
        }

        #endregion

        #region Download File Async - Running Task

        public void Download()
        {
            if (webClient == null)
            {
                webClient = new WebClient();
                webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
            }

            webClient.DownloadFileAsync(Url, TempFileName);
            Status = ProgressStatus.Running;
            StartedAt = DateTime.Now; 
        }

        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Error = e.Error;
            if (this.Error == null && this.Status == ProgressStatus.Running)
            {
                try
                {
                    this.Result = File.ReadAllText(this.TempFileName);
                    if (this.IsSaveToFile) File.Copy(this.TempFileName, this.FileName, true);
                    this.Status = ProgressStatus.Completed;
                }
                catch (Exception ex) { this.Error = ex; }
            }
            if (this.Error != null) this.Status = ProgressStatus.Failed;

            this.ReportComplete();
            Downloader.DownloadNext();
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Received = e.BytesReceived;
            this.Total = e.TotalBytesToReceive;
            this.ProgressPercentage = e.ProgressPercentage;
            if (ProgressChangedEvent != null)
                ProgressChangedEvent(this);

            //cancel if it is running for long time
            if (this.TimeElapsed.TotalSeconds > 8 && ProgressPercentage == 0) 
                this.Cancel();
        }

        #endregion

    }

    public static class Downloader
    {
        #region General Properties and Functions

        public static bool IsConnected()
        {
            try
            {
                return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            }
            catch
            {
                return false;
            }
        }

        public static bool IsBusy()
        {
            if (CurrentTask == null) return false;
            if (CurrentTask.Status == ProgressStatus.Disposed) return false;
            if (CurrentTask.webClient == null) return false;
            return CurrentTask.webClient.IsBusy;
        }

        #endregion

        #region Webclient and Download Queue

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

        #region Download Next

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
            if (!__ContinueDownload) return;
            if (!IsConnected() || IsBusy()) return;

            //get next task
            Dequeue();
            if (DownloadQueue.Count == 0) return;
            CurrentTask = DownloadQueue[0];

            //if already cancelled, then skip it
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
            DownloadTaskHandler completed = null)
        {
            DownloadTask task = new DownloadTask(url, null, token);
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
            DownloadTaskHandler completed = null)
        {
            DownloadTask task = new DownloadTask(url, file, token);
            task.ProgressChangedEvent += progress;
            task.DownloadCompletedEvent += completed;
            return DownloadAsync(task, priority);
        }

        #endregion

        #region UserID Downloader

        public static void DownloadUserid(string name, Internet.DownloadTaskHandler complete)
        {
            string url = "http://uhunt.felix-halim.net/api/uname2uid/" + name;
            DownloadTask dt = new DownloadTask(url, null, name);

            if (LocalDatabase.ContainsUsers(name))
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
            else  if(task.Error == null)
                task.Status = ProgressStatus.Completed;

            if (task.Status == ProgressStatus.Completed)
                RegistryAccess.SetUserid(name, uid);
        }

        #endregion

        #region Problem Database and Category Downloader

        public static bool _DownloadingProblemDatabase = false;

        public static void DownloadProblemDatabase(DownloadTaskHandler completed, DownloadTaskHandler progress)
        {
            if (_DownloadingProblemDatabase) return;
            
            _DownloadingProblemDatabase = true;

            //problem database
            string url = "http://uhunt.felix-halim.net/api/p";
            string file = LocalDirectory.GetProblemDataFile();
            DownloadFileAsync(url, file, null, Priority.High, progress, __DownloadProblemDatabaseCompleted);
                        
            //problem catagories
            url = "http://uhunt.felix-halim.net/api/cpbook/3";
            file = LocalDirectory.GetCategoryPath();
            
            DownloadTask task = DownloadFileAsync(url, file, null, Priority.High, progress, completed);
            task.DownloadCompletedEvent += __DownloadProblemCategoryCompleted;
        }

        private static void __DownloadProblemDatabaseCompleted(DownloadTask task)
        {
            _DownloadingProblemDatabase = false;

            if (task.Status == ProgressStatus.Completed)
            {
                LocalDatabase.LoadDatabase();
                Logger.Add("Downloaded problem database file", "Downloader | __DownloadProblemDatabaseCompleted(DownloadTask task)");
            }
            else if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "Downloader | __DownloadProblemDatabaseCompleted(DownloadTask task)");
            }            
        }

        private static void __DownloadProblemCategoryCompleted(DownloadTask task)
        {
            if (task.Status == ProgressStatus.Completed)
            {
                LocalDatabase.LoadCatagories();
                Logger.Add("Downloaded context book 3 categories", "Downloader | __DownloadProblemCategoryCompleted(DownloadTask task)");
            }
            else if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "Downloader | __DownloadProblemCategoryCompleted(DownloadTask task)");
            }
        }

        #endregion
    }

}

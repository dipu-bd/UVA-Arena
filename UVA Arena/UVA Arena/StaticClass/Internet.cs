using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.ComponentModel;
using UVA_Arena.Structures;

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
        Failed
    }

    #region Download Task Class

    public delegate void DownloadProgressHandler(int percentage, DownloadTask Task);
    public delegate void DownloadCompleteHandler(DownloadTask Task);

    public class DownloadTask
    {
        public Uri uri { get; set; }
        public string file { get; set; }
        public bool saveToFile { get; set; }
        public object token { get; set; }
        public string result { get; set; }

        public long total { get; set; }
        public long received { get; set; } 
        public int retry { get; set; }
        public int percentage { get; set; }
        public Exception error { get; set; }
        public Priority priority { get; set; }
        public ProgressStatus status { get; set; }

        public event DownloadProgressHandler progress;
        public event DownloadCompleteHandler completed;

        public DownloadTask(string url = null, string file = null, object token = null)
        {
            this.uri = null;
            this.file = file;
            this.token = token;
            this.result = null;

            this.saveToFile = (file != null);
            if (url != null) uri = new Uri(url);

            this.total = 0;
            this.received = 0; 
            this.retry = 0;
            this.error = null;
            this.priority = Priority.Normal;
            this.status = ProgressStatus.Waiting;

            this.progress = null;
            this.completed = null;
        }

        public void Cancel()
        {
            if (status == ProgressStatus.Running)
                Downloader.client.CancelAsync();
            status = ProgressStatus.Cancelled;
        }
        public void ReportProgress(int percentage)
        {
            if (progress != null) progress(percentage, this);
        }
        public void ReportComplete()
        {
            if (completed != null) completed(this);
        }
    }
    #endregion

    #region Static Downloader Class

    public static class Downloader
    {

        #region Webclient and Download Queue

        private static int _highend = 0;
        private static int _normalend = 0;
        public static List<DownloadTask> DownloadQueue = new List<DownloadTask>();

        public static WebClient client;
        public static DownloadTask CurrentTask;

        private static void Initialize()
        {
            if (client != null) return;

            client = new WebClient();       
            client.Encoding = Encoding.UTF8;
            client.DownloadStringCompleted += client_DownloadStringCompleted;
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted += client_DownloadFileCompleted;
        }

        //
        // Download Queue
        //
        public static void Enqueue(DownloadTask task)
        {
            if (task == null) return;

            task.status = ProgressStatus.Waiting;
            switch (task.priority)
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
            if (task.status != ProgressStatus.Completed
                && task.status != ProgressStatus.Failed) return;

            switch (task.priority)
            {
                case Priority.Normal:
                    --_normalend;
                    break;
                case Priority.High:
                    --_highend;
                    --_normalend;
                    break;
            }
            DownloadQueue.RemoveAt(0);
        }

        //
        // Web Client
        //        
        /// <summary> Gets whether a Web request is in progress. </summary>
        public static bool IsBusy
        {
            get { return (client != null && client.IsBusy); }
        }

        private static void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged(e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);
        }

        private static void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadAsyncCompleted(null, (string)e.UserState, e.Error, e.Cancelled);
        }

        private static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string result = (e.Error == null) ? (string)e.Result : null;
            DownloadAsyncCompleted(result, null, e.Error, e.Cancelled);
        }

        #endregion

        #region Download Next

        public static void DownloadNext()
        {
            Initialize();

            //if client is busy
            if (client.IsBusy)
            {
                if (CurrentTask.status == ProgressStatus.Running) return;
                CurrentTask.status = ProgressStatus.Running;
                client.CancelAsync();
                return;
            }

            //get next task
            Dequeue();
            if (DownloadQueue.Count == 0) return;
            CurrentTask = DownloadQueue[0];

            //if already cancelled, then skip it
            if (CurrentTask.status == ProgressStatus.Cancelled)
            {
                DownloadAsyncCompleted();
                return;
            }

            //run task         
            try 
            {
                CurrentTask.status = ProgressStatus.Running;
                if (CurrentTask.saveToFile)
                {
                    //download file
                    string tmp = Path.GetTempFileName();
                    client.DownloadFileAsync(CurrentTask.uri, tmp, tmp);
                }
                else 
                {
                    //download string
                    client.DownloadStringAsync(CurrentTask.uri, null);
                }
            }
            catch (Exception ex)
            {
                //report error
                DownloadAsyncCompleted(null, null, ex);
            }

            System.GC.Collect();
        }

        private static void DownloadProgressChanged(long received, long total, int percent)
        {
            if (CurrentTask == null) return;

            CurrentTask.total = total;
            CurrentTask.received = received;
            CurrentTask.percentage = percent;

            CurrentTask.ReportProgress(percent);
        }

        private static void DownloadAsyncCompleted(string result = null, string tmpfile = null, Exception error = null, bool cancel = false)
        {
            if (CurrentTask == null) return;
            if (cancel) error = new Exception("Cancelled by user");

            //copy temporary file and delete it
            try
            {
                if (tmpfile == null || !File.Exists(tmpfile))
                    throw new FileNotFoundException();

                if (error == null)
                {
                    LocalDirectory.CreateFile(CurrentTask.file);
                    File.Copy(tmpfile, CurrentTask.file, true);
                    CurrentTask.status = ProgressStatus.Completed;
                }
            }
            catch (Exception ex)
            {
                if (error == null) error = ex;
            }

            //retry if failed
            if (error != null && CurrentTask.retry > 0)
            {
                --CurrentTask.retry;
                DownloadNext();
                return;
            }
                        
            
            if(File.Exists(tmpfile)) File.Delete(tmpfile);
            if (CurrentTask.status != ProgressStatus.Completed)            
                CurrentTask.status = ProgressStatus.Failed;             

            //raise complete event
            CurrentTask.error = error;
            CurrentTask.result = result;
            CurrentTask.ReportComplete();

            //run next download    
            DownloadNext();
        }

        #endregion

        #region Add To Download Functions

        public static DownloadTask DownloadAsync(DownloadTask task, Priority priority = Priority.Normal)
        {
            task.priority = priority;
            task.status = ProgressStatus.Waiting;
            Enqueue(task);
            DownloadNext();

            return task;
        }

        public static DownloadTask DownloadStringAsync(
            string url,
            object token = null,
            Priority priority = Priority.High,
            DownloadProgressHandler progress = null,
            DownloadCompleteHandler completed = null)
        {
            DownloadTask task = new DownloadTask(url, null, token);
            task.progress += progress;
            task.completed += completed;
            return DownloadAsync(task, priority);
        }

        public static DownloadTask DownloadFileAsync(
            string url,
            string file,
            object token = null,
            Priority priority = Priority.Normal,
            DownloadProgressHandler progress = null,
            DownloadCompleteHandler completed = null)
        {
            DownloadTask task = new DownloadTask(url, file, token);
            task.progress += progress;
            task.completed += completed;
            return DownloadAsync(task, priority);
        }

        #endregion


        #region User Database Downloader

        public static void DownloadUserid(string name, Internet.DownloadCompleteHandler complete)
        {
            string url = "http://uhunt.felix-halim.net/api/uname2uid/" + name;
            DownloadTask dt = new DownloadTask(url, null, name);

            dt.retry = 2;
            dt.result = RegistryAccess.GetUserid(name);
            if (dt.result != null)
            {
                dt.status = ProgressStatus.Completed;
                if (complete != null) complete(dt);
            }
            else
            {
                dt.completed += __DownloadUseridCompleted;
                if (complete != null) dt.completed += complete;
                DownloadAsync(dt, Priority.High);
            }
        }

        private static void __DownloadUseridCompleted(DownloadTask task)
        {
            string uid = (string)task.result;
            string name = (string)task.token;

            if (uid == "0")
                task.error = new Exception(name + " doesn't exist.");
            else if (string.IsNullOrEmpty(uid) || uid.Length <= 1)
                task.error = new Exception(uid + " is not valid as userid.");
            else task.status = ProgressStatus.Completed;

            if (task.status == ProgressStatus.Completed)
                RegistryAccess.SetUserid(name, uid);
            else
                task.status = ProgressStatus.Failed;
        }

        #endregion

        #region Problem Database Downloader

        public static void DownloadProblemDatabase(DownloadCompleteHandler completed, DownloadProgressHandler progress)
        {
            string url = "http://uhunt.felix-halim.net/api/p";
            string file = LocalDirectory.ProblemData;
            DownloadTask task = DownloadFileAsync(url, file, null, Priority.Normal, progress, completed);
            task.completed += __DownloadProblemDatabaseCompleted;
        }

        private static void __DownloadProblemDatabaseCompleted(DownloadTask task)
        {
            if (task.status == ProgressStatus.Completed)
            {
                ProblemDatabase.LoadDatabase();
                Logger.Add("Downloaded problem databse file", "Downloader");
            }
            else if (task.error != null)
            {
                Logger.Add(task.error.Message, "Downloader");
            }
        }

        #endregion
    }

    #endregion

}

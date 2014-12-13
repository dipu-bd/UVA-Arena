using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;

namespace UVA_Arena.Internet
{
    #region Download Task Class

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

    public delegate void DownloadProgressHandler(int percentage, DownloadTask Task);
    public delegate void DownloadCompleteHandler(DownloadTask Task);

    public class DownloadTask : IComparer<DownloadTask>, IDisposable
    {
        public Uri Url { get; set; }
        public object Token { get; set; }
        public string Result { get; set; }
        public string FileName { get; set; }
        public string TempFileName { get; private set; }

        public long DataSize { get; set; }
        public long Received { get; set; }
        public int RetryCount { get; set; }
        public int ProgressPercentage { get; set; }
        public Exception Error { get; set; }
        public Priority TaskPriority { get; set; }
        public ProgressStatus Status { get; set; }

        public bool IsSaveToFile()
        {
            return (FileName != null);
        }

        public event DownloadProgressHandler ProgressChangedEvent;
        public event DownloadCompleteHandler CompletedEvent;

        public DownloadTask(string url = null, string file = null, object token = null)
        {
            FileName = file;
            Token = token;
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
            }
            catch { }
        }

        public int Compare(DownloadTask a, DownloadTask b)
        {
            return ((int)a.TaskPriority - (int)b.TaskPriority);
        }

        public void Cancel()
        {
            if (Status == ProgressStatus.Running)
            {
                //To Do to Cancel this task
            }
            else
            {
                Status = ProgressStatus.Cancelled;
            }
        }

        public void ReportProgress()
        {
            ProgressPercentage = (int)(Received * 100 / DataSize);
            if (ProgressChangedEvent != null)
            {
                ProgressChangedEvent(ProgressPercentage, this);
            }
        }

        public void ReportComplete()
        {
            if (CompletedEvent != null) CompletedEvent(this);
        }
    }

    #endregion

    #region Downloader Class

    public static class Downloader
    {
        #region Default Properties

        public static bool IsConnected { get; set; }
        public static void ConnectionChanged(object sender, System.Net.NetworkInformation.NetworkAvailabilityEventArgs e)
        {
            IsConnected = e.IsAvailable;
        }

        #endregion

        #region Download Queue

        public static DownloadTask CurrentTask;
        public static List<DownloadTask> DownloadQueue = new List<DownloadTask>();

        //
        // Download Queue
        //
        public static void Enqueue(DownloadTask task)
        {
            if (task == null) return;
            task.Status = ProgressStatus.Waiting;
            DownloadQueue.Add(task);
            DownloadQueue.Sort();
        }

        public static void Dequeue()
        {
            if (DownloadQueue.Count == 0) return;
            DownloadQueue.RemoveAt(0);
        }

        public static bool IsBusy { get; set; }

        #endregion

        #region Download Next

        public static bool __ContinueDownload = true;

        public static void StopDownload()
        {
            __ContinueDownload = false;
            CurrentTask.Cancel();
        }

        public static void StartDownload()
        {
            __ContinueDownload = true;
            DownloadNext();
        }

        public static void DownloadNext()
        {
            if (!__ContinueDownload) return;

            //if client is busy
            if (IsBusy)
            {
                if (CurrentTask.Status == ProgressStatus.Running) return;
                CurrentTask.Status = ProgressStatus.Running;
                return;
            }

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
            try
            {
                CurrentTask.Status = ProgressStatus.Running;
                DownloadCurrent();
            }
            catch (Exception ex)
            {
                CurrentTask.Error = ex;
                CurrentTask.ReportComplete();
            }

            System.GC.Collect();
        }

        private static void DownloadCurrent()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(CurrentTask.Url);
            webRequest.Method = "GET";
            webRequest.Timeout = 3000;
            if (CurrentTask.TaskPriority == Priority.Low) webRequest.Timeout = 1500;
            webRequest.BeginGetResponse(new AsyncCallback(PlayResponeAsync), webRequest);
        }

        private static void PlayResponeAsync(IAsyncResult asyncResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asyncResult.AsyncState;
            for (int i = 0; i < CurrentTask.RetryCount; ++i)
            {
                try
                {                    
                    using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asyncResult))
                    {
                        byte[] buffer = new byte[1024];

                        FileStream fileStream = File.OpenWrite(CurrentTask.TempFileName);
                        using (Stream input = webResponse.GetResponseStream())
                        {
                            CurrentTask.Received = 0;
                            CurrentTask.ProgressPercentage = 0;
                            CurrentTask.DataSize = input.Length;
                            CurrentTask.ReportProgress();
                            CurrentTask.Status = ProgressStatus.Running;

                            int size = input.Read(buffer, 0, buffer.Length);
                            while (size > 0)
                            {
                                fileStream.Write(buffer, 0, size);

                                CurrentTask.Received += size;    //get the progress of download
                                CurrentTask.ProgressPercentage = (int)(100 * CurrentTask.Received / CurrentTask.DataSize);

                                size = input.Read(buffer, 0, buffer.Length);
                                CurrentTask.ReportProgress();

                                if (CurrentTask.Status == ProgressStatus.Cancelled)
                                    throw new OperationCanceledException();
                            }
                        }

                        fileStream.Flush();
                        fileStream.Close();
                    }
                    
                    CurrentTask.Error = null;
                    break;
                }
                catch (Exception ex)
                {
                    CurrentTask.Error = ex;
                }

                if (CurrentTask.Status == ProgressStatus.Cancelled) break;
            }

            CurrentTask.ReportComplete();
            if (CurrentTask.Error == null)
            {
                CurrentTask.Status = ProgressStatus.Completed;
                CurrentTask.Result = File.ReadAllText(CurrentTask.TempFileName);
                File.Copy(CurrentTask.TempFileName, CurrentTask.FileName);
            }
            else
            {
                CurrentTask.Status = ProgressStatus.Failed;                
            }

            File.Delete(CurrentTask.TempFileName);
            DownloadNext();
        }

        #endregion

        #region Add To Download Functions

        public static DownloadTask DownloadAsync(DownloadTask task, Priority priority = Priority.Normal)
        {
            task.TaskPriority = priority;
            task.Status = ProgressStatus.Waiting;
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
            task.ProgressChangedEvent += progress;
            task.CompletedEvent += completed;
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
            task.ProgressChangedEvent += progress;
            task.CompletedEvent += completed;
            return DownloadAsync(task, priority);
        }

        #endregion


        #region UserID Downloader

        public static void DownloadUserid(string name, Internet.DownloadCompleteHandler complete)
        {
            string url = "http://uhunt.felix-halim.net/api/uname2uid/" + name;
            DownloadTask dt = new DownloadTask(url, null, name);

            dt.RetryCount = 2;
            dt.Result = RegistryAccess.GetUserid(name);
            if (dt.Result != null)
            {
                dt.Status = ProgressStatus.Completed;
                if (complete != null) complete(dt);
            }
            else
            {
                dt.CompletedEvent += __DownloadUseridCompleted;
                if (complete != null) dt.CompletedEvent += complete;
                DownloadAsync(dt, Priority.High);
            }
        }

        private static void __DownloadUseridCompleted(DownloadTask task)
        {
            string uid = (string)task.Result;
            string name = (string)task.Token;

            if (uid == "0")
                task.Error = new Exception(name + " doesn't exist.");
            else if (string.IsNullOrEmpty(uid) || uid.Length <= 1)
                task.Error = new Exception(uid + " is not valid as userid.");
            else task.Status = ProgressStatus.Completed;

            if (task.Status == ProgressStatus.Completed)
                RegistryAccess.SetUserid(name, uid);
            else
                task.Status = ProgressStatus.Failed;
        }

        #endregion

        #region Problem Database Downloader

        public static void DownloadProblemDatabase(DownloadCompleteHandler completed, DownloadProgressHandler progress)
        {
            string url = "http://uhunt.felix-halim.net/api/p";
            string file = LocalDirectory.ProblemDataFile;
            DownloadTask task = DownloadFileAsync(url, file, null, Priority.High, progress, completed);
            task.CompletedEvent += __DownloadProblemDatabaseCompleted;
        }

        private static void __DownloadProblemDatabaseCompleted(DownloadTask task)
        {
            if (task.Status == ProgressStatus.Completed)
            {
                DefaultDatabase.LoadDatabase();
                Logger.Add("Downloaded problem databse file", "Downloader");
            }
            else if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "Downloader");
            }
        }

        #endregion

    }

    #endregion
}

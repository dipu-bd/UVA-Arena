using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

        public long Total { get; set; }
        public long Received { get; set; }
        public int ProgressPercentage { get; set; }
        public int RetryCount { get; set; }
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

        public DownloadTask(string url = null, string file = null, object token = null, int retry = 0)
        {
            FileName = file;
            Token = token;
            IsSaveToFile = (file != null);
            if (url != null) Url = new Uri(url);
            TaskPriority = Priority.Normal;
            Status = ProgressStatus.Waiting;
            RetryCount = retry;
        }

        public void Dispose()
        {
            try
            {
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
                webClient.DownloadDataCompleted += webClient_DownloadDataCompleted;
                webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36");
            }

            Status = ProgressStatus.Running;
            webClient.DownloadDataAsync(Url);
            StartedAt = DateTime.Now;
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //gather progress data
            this.Received = e.BytesReceived;
            this.Total = e.TotalBytesToReceive;
            this.ProgressPercentage = e.ProgressPercentage;
            if (ProgressChangedEvent != null)
                ProgressChangedEvent(this);

            //cancel if it is running for long time ( > 7 secs
            if (this.TimeElapsed.TotalSeconds > 7 && ProgressPercentage == 0)
                this.Cancel();
        }

        void webClient_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            //write download to file or string
            this.Error = e.Error;
            if (this.Error == null && this.Status == ProgressStatus.Running)
            {
                try
                {
                    this.Result = System.Text.Encoding.UTF8.GetString(e.Result);
                    if (this.IsSaveToFile)
                        File.WriteAllBytes(this.FileName, e.Result);
                    this.Status = ProgressStatus.Completed;
                    this.Error = null;
                }
                catch (Exception ex)
                {
                    this.Error = ex;
                }
            }
            if (this.Error != null)
            {
                System.Console.WriteLine(this.Error);
                this.Status = ProgressStatus.Failed;
            }

            //retry if couldn't complete
            if (this.RetryCount > 0 &&
                !(this.Status == ProgressStatus.Completed ||
                this.Status == ProgressStatus.Cancelled))
            {
                this.RetryCount--;
                this.Error = null;
                this.Status = ProgressStatus.Running;
                Downloader.DownloadNext();
            }
            else
            {
                //send completion report                   
                this.RetryCount = 0;
                this.ReportComplete();
                Downloader.DownloadNext();
            }
        }



        #endregion

    }
}
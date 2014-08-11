using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;
using UVA_Arena.Internet;

namespace UVA_Arena
{
    public partial class DownloadAllForm : Form
    {
        #region Primary

        public DownloadAllForm()
        {
            InitializeComponent();

            InitDownload();
            replaceBox.Checked = ReplaceOldFiles;
            TaskQueue.AddTask(SetStatus, 100);
        }

        public enum State
        {
            Running,
            Pausing,
            Cancelling,
            Paused,
            Finished,
            Cancelled,
        }

        private int progress;
        private string curstat;

        private long current = 0;
        private List<long> remaining = new List<long>();
        private List<Internet.DownloadTask> tasklist = new List<Internet.DownloadTask>();

        private State _currentState = State.Paused;
        private State CurrentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState == value) return;                                
                _currentState = value;

                this.BeginInvoke((MethodInvoker)delegate
                {
                    #region Change State
                    switch (value)
                    {
                        case State.Running:
                            startButton.Text = "Pause";
                            cancelButton.Text = "Cancel";
                            startButton.Enabled = true;
                            cancelButton.Enabled = true;
                            restartButton.Enabled = false;
                            break;
                        case State.Pausing:
                            startButton.Text = "Pausing...";
                            cancelButton.Text = "Cancel";
                            startButton.Enabled = false;
                            cancelButton.Enabled = false;
                            restartButton.Enabled = false;
                            break;
                        case State.Cancelling:
                            startButton.Text = "Pause";
                            cancelButton.Text = "Canceling...";
                            startButton.Enabled = false;
                            cancelButton.Enabled = false;
                            restartButton.Enabled = false;
                            break;
                        case State.Paused:
                            startButton.Text = "Resume";
                            cancelButton.Text = "Close";
                            startButton.Enabled = true;
                            cancelButton.Enabled = true;
                            restartButton.Enabled = true;
                            break;
                        case State.Cancelled:
                            startButton.Text = "Resume";
                            cancelButton.Text = "Close";
                            startButton.Enabled = true;
                            cancelButton.Enabled = true;
                            restartButton.Enabled = true;
                            break;
                        case State.Finished:
                            startButton.Text = "Finished";
                            cancelButton.Text = "Close";
                            startButton.Enabled = false;
                            cancelButton.Enabled = true;
                            restartButton.Enabled = true;
                            break;
                    }
                    #endregion
                });
            }
        }

        public long LastDownloadedProblem
        {
            get
            {
                object dat = RegistryAccess.GetValue("Last Downloaded Problem", null);
                if (dat == null || dat.GetType() != typeof(long)) return 0;
                return (long)dat;
            }
            set
            {
                RegistryAccess.SetValue("Last Downloaded Problem", value, null,
                    Microsoft.Win32.RegistryValueKind.QWord);
            }
        }

        public bool ReplaceOldFiles
        {
            get
            {
                object dat = RegistryAccess.GetValue("Replace Old Files", null);
                if (dat == null || dat.GetType() != typeof(string)) return false;
                return bool.Parse((string)dat);
            }
            set
            {
                replaceBox.Checked = value;
                RegistryAccess.SetValue("Replace Old Files", value.ToString());
            }
        }


        #endregion

        #region Event Listeners

        private void replaceBox_Click(object sender, EventArgs e)
        {
            ReplaceOldFiles = !replaceBox.Checked;
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            LastDownloadedProblem = 0;
            InitDownload();
            StartDownload();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case State.Running:
                    PauseDownload();
                    break;
                default:
                    StartDownload();
                    break;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            switch (CurrentState)
            {
                case State.Finished:
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;
                case State.Paused:
                case State.Cancelled:
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                    break;
                default:
                    CancelDownload();
                    break;
            }
        }

        private void DownloadAllForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CurrentState == State.Running) CancelDownload();
        }

        #endregion

        #region Prepare Download

        public void InitDownload()
        {
            remaining.Clear();

            long last = LastDownloadedProblem;
            foreach (ProblemInfo pi in ProblemDatabase.problem_list)
            {
                if (pi.pnum > last)
                    remaining.Add(pi.pnum);
            }
            remaining.Sort();

            current = last;
            CurrentState = State.Paused;
        }

        public void StartDownload()
        {
            if (!backgroundWorker1.IsBusy)
            {
                CurrentState = State.Running;
                backgroundWorker1.RunWorkerAsync();
            }
        }

        public void PauseDownload()
        {
            if (backgroundWorker1.IsBusy)
                CurrentState = State.Pausing;
            else
                CurrentState = State.Paused;
        }

        public void CancelDownload()
        {
            if (backgroundWorker1.IsBusy)
                CurrentState = State.Cancelling;
            else
                CurrentState = State.Cancelled;
        }

        #endregion

        #region Downloaders

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadNext();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (remaining.Count == 0)
            {
                LastDownloadedProblem = 0;
                CurrentState = State.Finished;
            }

            switch (CurrentState)
            {
                case State.Pausing:
                case State.Cancelling:
                    remaining.Insert(0, current);
                    CurrentState = State.Cancelled;
                    break;
            }

            System.GC.Collect();
        }

        private void DownloadNext()
        {
            if (remaining.Count == 0) return;
            if (CurrentState != State.Running) return;

            LastDownloadedProblem = current;
            current = remaining[0];
            remaining.RemoveAt(0);

            if (!ProblemDatabase.HasProblem(current))
            {
                DownloadNext();
                return;
            }

            curstat = "";
            progress = 0;
            DownloadProblem();
        }

        private void DownloadProblem()
        {
            try
            {
                //files and urls
                FileInfo pdffile = new FileInfo(LocalDirectory.GetProblemPdf(current));
                FileInfo htmlfile = new FileInfo(LocalDirectory.GetProblemHtml(current));
                string pdf = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.pdf", current / 100, current);
                string html = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", current / 100, current);

                //download html file
                if (replaceBox.Checked || htmlfile.Length < 100)
                {
                    DownloadTask task = new DownloadTask(html, htmlfile.FullName, current);
                    task.retry = 1;
                    task.progress += ProgressChanged;
                    task.completed += DownloadFinished;
                    tasklist.Add(Downloader.DownloadAsync(task, Internet.Priority.Low));
                }
                else
                {
                    DownloadHtmlContent();
                }

                //download pdf file                
                if (replaceBox.Checked || pdffile.Length < 200)
                {
                    DownloadTask task = new DownloadTask(pdf, pdffile.FullName, current);
                    task.progress += ProgressChanged;
                    task.completed += DownloadFinished;
                    tasklist.Add(Downloader.DownloadAsync(task, Internet.Priority.Low));
                }

                CheckTasklist();
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, this.Name);
            }
        }

        private void DownloadHtmlContent()
        {
            if (CurrentState != State.Running) return;

            foreach (var itm in Functions.ProcessHtmlContent(current, replaceBox.Checked))
            {
                itm.token = current;
                itm.progress += ProgressChanged;
                itm.completed += DownloadFinished;
                tasklist.Add(Downloader.DownloadAsync(itm, Internet.Priority.Low));
            }
        }

        private void ProgressChanged(int percent, DownloadTask task)
        {
            progress = percent;
            curstat = string.Format("FILE: \"{0}\" | {1} out of {2} downloaded.",
                Path.GetFileName(task.file), Functions.FormatMemory(task.received),
                Functions.FormatMemory(task.total));
        }

        private void DownloadFinished(DownloadTask task)
        {
            if (task.status == ProgressStatus.Completed)
            {
                if (task.file.EndsWith(".html")) DownloadHtmlContent();
            }
            CheckTasklist();
        }

        private void CheckTasklist()
        {
            //if not running clear list
            if (CurrentState != State.Running)
            {
                foreach (DownloadTask task in tasklist) task.Cancel();
                return;
            }

            //remove all stopped tasks
            Predicate<DownloadTask> pred = delegate(DownloadTask dt)
            {
                return (dt == null ||
                    !(dt.status == ProgressStatus.Running ||
                    dt.status == ProgressStatus.Waiting));
            };
            tasklist.RemoveAll(pred);

            //resume download
            if (tasklist.Count == 0)
            {
                if (backgroundWorker1.IsBusy) DownloadNext();
                else backgroundWorker1.RunWorkerAsync();
            }
            else if (Downloader.DownloadQueue.Count == 0)
            {
                tasklist.Clear();
                CheckTasklist();
            }
        }

        #endregion

        #region Set Status

        private void SetStatus()
        {
            if (this.Disposing || this.IsDisposed) return;

            //set status
            int tot = ProblemDatabase.problem_list.Count;
            int finished = tot - remaining.Count;
            totalProgress.Value = (int)(100.0 * finished / tot);
            totalPercentage.Text = totalProgress.Value.ToString() + "%";
            overallStatus.Text = string.Format("{0} / {1} problems downloaded.", finished, tot);

            if (CurrentState == State.Running)
            {
                StatusLabel.Text = curstat;
                currentProgress.Value = progress;
                currentPercentage.Text = currentProgress.Value.ToString() + "%";
                currentProblem.Text = string.Format("Downloading {0} - {1}... ",
                   current, ProblemDatabase.GetTitle(current));
            }
            else
            {
                currentProblem.Text = CurrentState.ToString();
                StatusLabel.Text = "";
                currentProgress.Value = 0;
                currentPercentage.Text = "";
            }

            TaskQueue.AddTask(SetStatus, 300);
            if (CurrentState == State.Running && !backgroundWorker1.IsBusy) CheckTasklist();
        }

        #endregion

    }
}

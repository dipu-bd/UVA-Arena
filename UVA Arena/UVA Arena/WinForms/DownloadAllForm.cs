using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class DownloadAllForm : Form
    {
        #region Primary

        public DownloadAllForm()
        {
            InitializeComponent();
            remaining = new List<long>();
            webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
        }

        private void DownloadAllForm_Load(object sender, EventArgs e)
        {
            InitDownload();
            //replaceCombo1.SelectedIndex = ReplaceOldFiles;
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

        private long current = 0;
        private WebClient webClient;
        private List<long> remaining;

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

        public static long LastDownloadedProblem
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
                object dat = RegistryAccess.GetValue("Replace Old Files", 0);
                if (dat == null || dat.GetType() != typeof(int)) return false;
                return (int)dat == 1;
            }
            set
            {
                replaceBox.Checked = value;
                RegistryAccess.SetValue("Replace Old Files", value ? 1 : 0);
            }
        }


        #endregion

        #region Event Listeners

        private void replaceBox_Click(object sender, EventArgs e)
        {
            if (replaceBox.Checked)
            {
                ReplaceOldFiles = false;
                //replaceCombo1.SelectedIndex = -1;
            }
            else
            {
                ReplaceOldFiles = true;
                //replaceCombo1.SelectedIndex = 0;
            }
        }

        /*
        private void replaceCombo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            replaceBox.Checked = (replaceCombo1.SelectedIndex != -1);
            ReplaceOldFiles = replaceCombo1.SelectedIndex;
        }*/

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
            foreach (ProblemInfo pi in LocalDatabase.problemList)
            {
                if (pi.pnum > last)
                    remaining.Add(pi.pnum);
            }
            remaining.Sort();

            current = last;
            SetStatus(0, "");
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

        #region Background Worker

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadNext();
        }

        private void backgroundWorker1_ProressChanged(object sender, ProgressChangedEventArgs e)
        {
            SetStatus(e.ProgressPercentage, (string)e.UserState);
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
                    remaining.Insert(0, current);
                    CurrentState = State.Paused;
                    break;
                case State.Cancelling:
                    remaining.Insert(0, current);
                    CurrentState = State.Cancelled;
                    break;
            }

            SetStatus(0, "");
        }

        void SetStatus(int percent, string status)
        {
            int tot = LocalDatabase.problemList.Count;
            int finished = tot - remaining.Count;
            totalProgress.Value = (int)(100.0 * finished / tot);
            totalPercentage.Text = totalProgress.Value.ToString() + "%";
            overallStatus.Text = string.Format("{0} / {1} problems downloaded.", finished, tot);

            if (CurrentState == State.Running)
            {
                StatusLabel.Text = status;
                currentProgress.Value = percent;
                currentPercentage.Text = currentProgress.Value.ToString() + "%";
                currentProblem.Text = string.Format("Downloading {0} - {1}... ",
                   current, LocalDatabase.GetTitle(current));
            }
            else
            {
                StatusLabel.Text = "";
                currentProgress.Value = 0;
                currentPercentage.Text = "";
                currentProblem.Text = CurrentState.ToString();
            }
        }

        #endregion

        #region Download Next

        private void DownloadNext()
        {
            while (remaining.Count > 0)
            {
                current = remaining[0];
                remaining.RemoveAt(0);

                if (!LocalDatabase.HasProblem(current)) continue;
                DownloadProblem();

                if (CurrentState != State.Running) return;
                LastDownloadedProblem = current;
            }
        }

        private void DownloadProblem()
        {
            //initial data
            int total = 2;
            int finished = 0;
            long vol = current / 100;
            string status = "";

            //download PDF file
            FileInfo pdffile = new FileInfo(LocalDirectory.GetProblemPdf(current));
            string pdf = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.pdf", vol, current);

            if (ReplaceOldFiles || !pdffile.Exists || pdffile.Length < 200)
            {
                status = "Downloading " + pdffile.Name + "... ";
                backgroundWorker1.ReportProgress(100 * finished / total, status);
                status = DownloadFile(pdf, pdffile.FullName, 200);
                ++finished;
                backgroundWorker1.ReportProgress(100 * finished / total, status);
            }

            if (!Internet.Downloader.IsInternetConnected())
            {
                status = "Not connected to the Internet";
                backgroundWorker1.ReportProgress(0, status);
                CurrentState = State.Cancelling;
            }
            if (CurrentState != State.Running) return;

            /*
            //download HTML file
            FileInfo htmlfile = new FileInfo(LocalDirectory.GetProblemHtml(current));
            string html = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", vol, current);

            if (ReplaceOldFiles == 0 || ReplaceOldFiles == 1 ||
                !htmlfile.Exists || htmlfile.Length < 100)
            {
                status = "Downloading " + htmlfile.Name + "... ";
                backgroundWorker1.ReportProgress(100 * finished / total, status);
                status = DownloadFile(html, htmlfile.FullName, 100);
                ++finished;
                backgroundWorker1.ReportProgress(100 * finished / total, status);
            }

            if (!Internet.Downloader.IsInternetConnected())
            {
                status = "Not connected to the Internet";
                backgroundWorker1.ReportProgress(0, status);
                CurrentState = State.Cancelling;
            }
            if (CurrentState != State.Running) return;
            */


            /*
            //download HTML contents
            var list = Functions.ProcessHtmlContent(current,
                ReplaceOldFiles == 0 || ReplaceOldFiles == 3);

            finished = 0;
            total = list.Count;
            foreach (var itm in list)
            {
                if (CurrentState != State.Running) return;

                if (Internet.Downloader.IsInternetConnected())
                {
                    status = "Downloading " + Path.GetFileName(itm.FileName) + "... ";
                    backgroundWorker1.ReportProgress(100 * finished / total, status);
                    status = DownloadFile(itm.Url.ToString(), itm.FileName, 10);
                    ++finished;
                    backgroundWorker1.ReportProgress(100 * finished / total, status);
                }
                else
                {
                    status = "Not connected to the Internet";
                    backgroundWorker1.ReportProgress(0, status);
                    CurrentState = State.Cancelling;
                }
            }*/
        }

        private string DownloadFile(string url, string file, int minSiz, int tryCount = 2)
        {
            try
            {
                string tmp = Path.GetTempFileName();
                webClient.DownloadFile(url, tmp);

                LocalDirectory.CreateFile(file);
                if (LocalDirectory.GetFileSize(tmp) >= minSiz)
                {
                    File.Copy(tmp, file, true);
                    File.Delete(tmp);
                    return "Success.";
                }
                else
                {
                    File.Delete(tmp);
                    return ("File doesn't have desired length.");
                }
            }
            catch (Exception ex)
            {
                if (tryCount > 0)
                {
                    System.Threading.Thread.Sleep(300);
                    return DownloadFile(url, file, minSiz, tryCount - 1);
                }
                //if couldn't be downloaded
                if (File.Exists(file) && LocalDirectory.GetFileSize(file) < minSiz)
                {
                    File.Delete(file);
                }
                Logger.Add(ex.Message, this.Name);
                return "Download Failed.";
            }
        }

        #endregion

    }
}

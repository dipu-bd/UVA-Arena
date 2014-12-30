using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using UVA_Arena.Structures;
using UVA_Arena.Internet;

namespace UVA_Arena.Elements
{
    public partial class ProblemViewer : UserControl
    {

        #region Top Level

        public ProblemViewer()
        {
            InitializeComponent();
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            titleBox1.BackColor = this.BackColor;
            catagoryInfo.BackColor = this.BackColor;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AssignAspectToSubList();
            usernameList1.SetObjects(LocalDatabase.usernames);
            dateTimePicker1.Value = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));

            showUsersRankButton.Text = "Show " + RegistryAccess.DefaultUsername + "'s Rank";
            showUserSubButton.Text = "Show " + RegistryAccess.DefaultUsername + "'s Submissions";
        }

        #endregion

        #region TabControl

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == discussTab)
            {
                if (discussWebBrowser.Tag == null || (long)discussWebBrowser.Tag != current.pnum)
                    homeDiscussButton.PerformClick();
            }
            else if (tabControl1.SelectedTab == submissionTab)
            {
                LoadSubmission();
            }
        }

        #endregion

        #region Load Problem

        ProblemInfo current = null;
        Stack<ProblemInfo> next = new Stack<ProblemInfo>();
        Stack<ProblemInfo> previous = new Stack<ProblemInfo>();

        public void LoadProblem(ProblemInfo plist)
        {
            if (plist == null)
            {
                ClearAll();
                return;
            }

            //cleanup history
            if (next.Count > 0)
            {
                next.Clear();
                nextButton.Enabled = false;
            }
            if (current != null)
            {
                previous.Push(current);
                backButton.Enabled = true;
            }

            current = plist;
            ShowCurrent();
            reloadButton.Enabled = true;
        }

        public void ClearAll()
        {
            current = null;
            titleBox1.Text = "No problem selected";
            catagoryInfo.Text = "";
            catagoryButton.Visible = false;
            problemWebBrowser.Navigate("");
        }

        private void ShowCurrent()
        {
            if (current == null) return;

            titleBox1.Text = string.Format("{0} - {1}", current.pnum, current.ptitle);
            titleBox1.Text += string.Format(" (Level : {0}{1})", current.level, current.levelstar);
            catagoryButton.Visible = true;
            ShowCurrentTags();

            string path = LocalDirectory.GetProblemHtml(current.pnum);
            problemWebBrowser.Navigate(path);
            tabControl1.SelectedTab = descriptionTab;

            markButton.Checked = RegistryAccess.FavouriteProblems.Contains(current.pnum);

            FileInfo local = new FileInfo(path);
            if (!local.Exists || local.Length < 100) DownloadHtml(current.pnum);
            else DownloadContents(current.pnum);
        }

        private void ShowCurrentTags()
        {
            if (current.tags == null) current.tags = RegistryAccess.GetTags(current.pnum);
            string txt = string.Join("; ", current.tags.ToArray());
            if (txt.Length > 0) txt = "Tags : " + txt;
            catagoryInfo.Text = txt;
        }

        #endregion

        #region Problem Downloader

        private void DownloadPdf(long pnum)
        {
            try
            {
                string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.pdf", pnum / 100, pnum);
                string file = LocalDirectory.GetProblemPdf(pnum);
                if (!reloadButton.Enabled || LocalDirectory.GetFileSize(file) < 200)
                    Downloader.DownloadFileAsync(url, file, pnum,
                        Internet.Priority.Normal, ProgressChanged, DownloadFinished);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Viewer | DownloadPdf(long pnum)");
            }
        }

        private void DownloadHtml(long pnum)
        {
            try
            {
                string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", pnum / 100, pnum);
                string file = LocalDirectory.GetProblemHtml(pnum);
                if (!reloadButton.Enabled || LocalDirectory.GetFileSize(file) < 100)
                    Downloader.DownloadFileAsync(url, file, pnum,
                        Internet.Priority.Normal, ProgressChanged, DownloadFinished);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Viewer | DownloadHtml(long pnum)");
            }
        }

        private int DownloadContents(long pnum)
        {
            List<Internet.DownloadTask> dlist = Functions.ProcessHtmlContent(pnum, !reloadButton.Enabled);
            for (int i = 0; i < dlist.Count; ++i)
            {
                dlist[i].ProgressChangedEvent += ProgressChanged;
                if (i == dlist.Count - 1) dlist[i].DownloadCompletedEvent += DownloadFinished;
                Downloader.DownloadAsync(dlist[i], Internet.Priority.Low);
            }
            return dlist.Count;
        }

        private void ProgressChanged(Internet.DownloadTask task)
        {
            string file = Path.GetFileName(task.FileName);
            string text = string.Format("Downloading \"{0}\"... {1}% [{2} out of {3}] completed.",
                    file, task.ProgressPercentage, Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.problems.Status1.Text = text;
        }

        private void DownloadFinished(DownloadTask task)
        {
            bool finish = false;
            if (task.Status != ProgressStatus.Completed) finish = true;
            if (current == null || current.pnum != (long)task.Token) finish = true;

            if (!finish)
            {
                string ext = Path.GetExtension(task.FileName);
                if (ext == ".pdf")
                {
                    TaskQueue.AddTask(Interactivity.problems.ClearStatus, 1000);
                    System.Diagnostics.Process.Start(task.FileName);
                    finish = true;
                }
                else if (ext == ".html")
                {
                    string file = LocalDirectory.GetProblemHtml(current.pnum);
                    problemWebBrowser.Navigate(file);
                    int cnt = DownloadContents(current.pnum);
                    if (cnt == 0) finish = true;
                }
                else
                {
                    finish = true;
                }
            }

            if (finish)
            {
                problemWebBrowser.Refresh();
                reloadButton.Enabled = true;
                TaskQueue.AddTask(Interactivity.problems.ClearStatus, 1000);
            }
        }

        #endregion

        #region Top bar

        private void codeButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            Interactivity.ShowCode(current.pnum);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            Interactivity.SubmitCode(current.pnum);
        }

        private void expandViewButton1_Click(object sender, EventArgs e)
        {
            if (Interactivity.problems.ExpandCollapseView())
            {
                ((Control)sender).Text = "Collapse";
            }
            else
            {
                ((Control)sender).Text = "Expand";
            }
        }

        private void catagoryInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) catagoryButton.PerformClick();
            e.SuppressKeyPress = true;
        }

        private void catagoryButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            CatagoryChange cc = new CatagoryChange(current);
            if (cc.ShowDialog() == DialogResult.OK) ShowCurrentTags();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (current != null)
            {
                next.Push(current);
                nextButton.Enabled = true;
            }
            current = previous.Pop();
            backButton.Enabled = (previous.Count > 0);

            ShowCurrent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (current != null)
            {
                previous.Push(current);
                backButton.Enabled = true;
            }
            current = next.Pop();
            nextButton.Enabled = (next.Count > 0);

            ShowCurrent();
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            reloadButton.Enabled = false;
            DownloadHtml(current.pnum);
        }

        private void pdfButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;

            string filepath = LocalDirectory.GetProblemPdf(current.pnum);
            if (!File.Exists(filepath) || (new FileInfo(filepath)).Length < 200)
            {
                DownloadPdf(current.pnum);
            }
            else
            {
                System.Diagnostics.Process.Start(filepath);
            }
        }

        private void externalButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", current.volume, current.pnum);
            System.Diagnostics.Process.Start(url);
        }

        private void markButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;

            List<long> fav = RegistryAccess.FavouriteProblems;
            if (markButton.Checked) fav.Remove(current.pnum);
            else if (!fav.Contains(current.pnum)) fav.Add(current.pnum);

            RegistryAccess.FavouriteProblems = fav;
            markButton.Checked = !markButton.Checked;

            if (Interactivity.problems.favouriteButton.Checked)
                Interactivity.problems.LoadFavourites();
        }

        #endregion

        #region Discuss Tab

        private void goDiscussButton_Click(object sender, EventArgs e)
        {
            discussWebBrowser.Stop(); 
            discussWebBrowser.Navigate(discussUrlBox.Text);
        }

        private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            discussUrlBox.Text = discussWebBrowser.Url.ToString();
            Interactivity.problems.Status1.Text = discussWebBrowser.StatusText;
        }

        private void prevDiscussButton_Click(object sender, EventArgs e)
        {
            discussWebBrowser.GoBack();
        }

        private void nextDiscussButton_Click(object sender, EventArgs e)
        {
            discussWebBrowser.GoForward();
        }
        private void homeDiscussButton_Click(object sender, EventArgs e)
        {
            string query = "";
            if (current != null) query = string.Format("search.php?keywords={0}", current.pnum);
            string discuss = string.Format("http://acm.uva.es/board/{0}", query);
            discussWebBrowser.Navigate(discuss);
            discussUrlBox.Text = discuss;
            discussWebBrowser.Tag = current.pnum;
        }

        private void webBrowser2_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            Interactivity.problems.Status1.Text = discussWebBrowser.StatusText;
            Interactivity.problems.Progress1.Value = (int)(100 * e.CurrentProgress / e.MaximumProgress);
        }

        #endregion

        #region Submission Tab

        enum SubViewType
        {
            LastSubmission,
            Ranklist,
            UsersRank,
            UsersSub
        }

        private SubViewType _curSubType = SubViewType.LastSubmission;

        private void submissionReloadButton_Click(object sender, EventArgs e)
        {
            _curSubType = SubViewType.LastSubmission;
            LoadSubmission();
        }
        private void showRanksButton_Click(object sender, EventArgs e)
        {
            _curSubType = SubViewType.Ranklist;
            LoadSubmission();
        }
        private void showUsersRankButton_Click(object sender, EventArgs e)
        {
            _curSubType = SubViewType.UsersRank;
            LoadSubmission();
        }
        private void showUserSubButton_Click(object sender, EventArgs e)
        {
            _curSubType = SubViewType.UsersSub;
            LoadSubmission();
        }

        private void usernameList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user = RegistryAccess.DefaultUsername;
            if (usernameList1.SelectedObject != null)
                user = ((KeyValuePair<string, string>)usernameList1.SelectedObject).Key;
            usernameList1.Tag = user;

            showUsersRankButton.Text = string.Format((string)showUsersRankButton.Tag, user);
            showUserSubButton.Text = string.Format((string)showUserSubButton.Tag, user);
             
            _curSubType = SubViewType.UsersSub;
            LoadSubmission();
        }

        private void LoadSubmission()
        {
            if (current == null) return;

            string user = (string)usernameList1.Tag;
            if (string.IsNullOrEmpty(user)) 
                user = RegistryAccess.DefaultUsername;
            string uid = LocalDatabase.GetUserid(user);  //uid        
    
            submissionStatus.ClearObjects();
            usernameList1.SetObjects(LocalDatabase.usernames);

            long start, stop;
            string url = "", format;
            switch (_curSubType)
            {
                case SubViewType.LastSubmission:
                    start = UnixTimestamp.ToUnixTime(dateTimePicker1.Value);
                    stop = UnixTimestamp.ToUnixTime(DateTime.Now);
                    format = "http://uhunt.felix-halim.net/api/p/subs/{0}/{1}/{2}"; //pid, unix time start, stop
                    url = string.Format(format, current.pid, start, stop);
                    Interactivity.problems.SetStatus("Downoading submissions...");
                    break;
                case SubViewType.Ranklist:
                    start = 1;
                    stop = (long)numericUpDown1.Value;
                    format = "http://uhunt.felix-halim.net/api/p/rank/{0}/{1}/{2}"; //pid, rank start, rank count
                    url = string.Format(format, current.pid, start, stop);
                    Interactivity.problems.SetStatus("Downoading ranks...");
                    break;
                case SubViewType.UsersRank:
                    start = stop = 15;
                    format = "http://uhunt.felix-halim.net/api/p/ranklist/{0}/{1}/{2}/{3}"; //pid uid, before_count, after_count
                    url = string.Format(format, current.pid, uid, start, stop);
                    Interactivity.problems.SetStatus("Downoading " + user + "'s rankdata...");
                    break;
                case SubViewType.UsersSub:
                    format = "http://uhunt.felix-halim.net/api/subs-nums/{0}/{1}/{2}"; //uid, pnum, last sid
                    url = string.Format(format, uid, current.pnum, 0);
                    Interactivity.problems.SetStatus("Downoading " + user + "'s submission...");
                    break;
            }

            Downloader.DownloadStringAsync(url, user, Priority.Normal, dt_progressChanged, dt_taskCompleted);
        }

        private void dt_progressChanged(DownloadTask task)
        {
            string status = string.Format("Downoading... [{0} of {1} received]",
               Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.problems.SetStatus(status);
            Interactivity.problems.Progress1.Value = task.ProgressPercentage;
        }

        private void dt_taskCompleted(DownloadTask task)
        {
            if (task.Status != ProgressStatus.Completed)
            {
                if (task.Error != null)
                {
                    Interactivity.problems.SetStatus("Download failed.");
                    Logger.Add(task.Error.Message, "Problem Viewer | dt_taskCompleted(DownloadTask task)");
                }
                return;
            }

            string user = (string)task.Token;

            if (_curSubType == SubViewType.UsersSub)
            {
                task.Result = task.Result.Remove(0, task.Result.IndexOf(":") + 1);
                task.Result = task.Result.Remove(task.Result.Length - 1);
                UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(task.Result);
                uinfo.Process();
                submissionStatus.ClearObjects();
                submissionStatus.SetObjects(uinfo.submissions);
            }
            else
            {
                List<SubmissionMessage> lsm =
                    JsonConvert.DeserializeObject<List<SubmissionMessage>>(task.Result);
                if (lsm == null) return;
                submissionStatus.ClearObjects();
                submissionStatus.SetObjects(lsm);
            }

            switch (_curSubType)
            {
                case SubViewType.LastSubmission:
                    submissionStatus.Sort(sidSUB, SortOrder.Descending);
                    subListLabel.Text = "Last submissions on this problem from " +
                        dateTimePicker1.Value.ToString();
                    break;
                case SubViewType.Ranklist:
                    submissionStatus.Sort(rankSUB, SortOrder.Ascending);
                    subListLabel.Text = "Ranklist of this problem showing first " +
                        numericUpDown1.Value.ToString() + "' users.";
                    break;
                case SubViewType.UsersRank:
                    submissionStatus.Sort(rankSUB, SortOrder.Ascending);
                    subListLabel.Text = user + "'s nearby users on this problem";
                    break;
                case SubViewType.UsersSub:
                    submissionStatus.Sort(sidSUB, SortOrder.Descending);
                    subListLabel.Text = user + "'s submissions on this problem";
                    break;
            }

            System.GC.Collect();
        }

        //
        //Submission Status Listview
        //
        private void AssignAspectToSubList()
        {
            subtimeSUB.AspectToStringConverter = delegate(object dat)
            {
                if (dat == null) return "";
                return UnixTimestamp.FormatUnixTime((long)dat);
            };
            lanSUB.AspectToStringConverter = delegate(object dat)
            {
                if (dat == null) return "";
                return Functions.GetLanguage((Language)((long)dat));
            };
            verSUB.AspectToStringConverter = delegate(object dat)
            {
                if (dat == null) return "";
                return Functions.GetVerdict((Verdict)((long)dat));
            };
            runSUB.AspectToStringConverter = delegate(object dat)
            {
                if (dat == null) return "";
                return Functions.FormatRuntime((long)dat);
            };
            rankSUB.AspectToStringConverter = delegate(object dat)
            {
                if (dat == null) return "";
                if ((long)dat == -1) return "-";
                return ((long)dat).ToString();
            };
        }

        private void submissionStatus_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Model == null) return;

            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;

            //get two aspect used later
            Verdict ver;
            string uname;
            if (typeof(SubmissionMessage) == e.Model.GetType())
            {
                SubmissionMessage js = (SubmissionMessage)e.Model;
                ver = (Verdict)js.ver;
                uname = js.uname;
            }
            else
            {
                UserSubmission js = (UserSubmission)e.Model;
                ver = (Verdict)js.ver; uname = js.uname;
            }

            //mark submission's with known user name
            if (_curSubType != SubViewType.UsersSub)
            {
                if (uname == RegistryAccess.DefaultUsername)
                {
                    for (int i = 0; i < e.Item.SubItems.Count; ++i)
                        e.Item.SubItems[i].BackColor = Color.Turquoise;
                }
                else if (LocalDatabase.ContainsUsers(uname))
                {
                    for (int i = 0; i < e.Item.SubItems.Count; ++i)
                        e.Item.SubItems[i].BackColor = Color.LightBlue;
                }
            }

            //format cells
            if (e.Column == sidSUB)
            {
                font = "Consolas";
                fore = Color.Teal;
                size = 8.5F;
            }
            else if (e.Column == unameSUB)
            {
                fore = Color.Navy;
                style = FontStyle.Italic;
            }
            else if (e.Column == fullnameSUB)
            {
                font = "Segoe UI Semibold";
            }
            else if (e.Column == runSUB)
            {
                fore = Color.SlateBlue;
            }
            else if (e.Column == subtimeSUB)
            {
                fore = Color.Maroon;
            }
            else if (e.Column == rankSUB)
            {
                fore = Color.Navy;
                font = "Segoe UI Semibold";
            }
            else if (e.Column == verSUB)
            {
                font = "Segoe UI";
                fore = Functions.GetVerdictColor(ver);
                style = FontStyle.Bold;
            }
            else if (e.Column == lanSUB)
            {
                style = FontStyle.Bold;
                fore = Color.Navy;
            }
            else { return; }
             
            e.SubItem.ForeColor = fore;
            e.SubItem.Font = new Font(font, size, style);
        }

        private void submissionStatus_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            SubmissionMessage list = (SubmissionMessage)e.Model;
            if (e.Column == unameSUB)
            {
                if (LocalDatabase.ContainsUsers(list.uname))
                {
                    Interactivity.ShowUserStat(list.uname);
                }
                else
                {
                    if (MessageBox.Show("Add this user to your favourite list?", "Add User",
                        MessageBoxButtons.YesNo) == DialogResult.No) return;
                    RegistryAccess.SetUserid(list.uname, list.uid.ToString());
                    Interactivity.ShowUserStat(list.uname);
                }
            }
        }

        #endregion
    }
}

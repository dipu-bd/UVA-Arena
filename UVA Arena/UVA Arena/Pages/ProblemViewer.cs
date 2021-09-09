using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UVA_Arena.Internet;
using UVA_Arena.Structures;

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
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AssignAspectToSubList();
            dateTimePicker1.Value = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));

            LoadUsernameList();

            Stylish.SetGradientBackground(titleBox1,
                new Stylish.GradientStyle(Color.LightBlue, Color.PaleTurquoise, 90F));

            Stylish.SetGradientBackground(toolStrip1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightBlue, 90F));
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
            problemMessage.Text = (string)problemMessage.Tag;
            //problemWebBrowser.GoHome();
        }

        private void setPdfFile(string file)
        {
            if (file == null)
            {
                pdfViewer1.Visible = false;
                downloadingNoticeLabel.Visible = true;
            }
            else
            {
                pdfViewer1.Load(file);
                pdfViewer1.Visible = true;
                downloadingNoticeLabel.Visible = false;
            }
        }

        private void ShowCurrent()
        {
            if (current == null) return;

            //unload previous data first
            //problemWebBrowser.Navigate(string.Empty);

            //load meta info
            LoadTopBar();
            markButton.Checked = current.Marked;

            //show data 
            //string html = LocalDirectory.GetProblemHtml(current.pnum);
            //bool htmlAvail = LocalDirectory.GetFileSize(html) > 100;
            //if (htmlAvail && this.tabControl1.SelectedTab == htmlTab)
            //{
            // problemWebBrowser.Navigate(html);
            // this.tabControl1.SelectedTab = htmlTab;
            //}

            //check pdf description
            string pdf = LocalDirectory.GetProblemPdf(current.pnum);
            bool pdfAvail = LocalDirectory.GetFileSize(pdf) > 200;
            if (pdfAvail)
            {
                setPdfFile(pdf);
                this.tabControl1.SelectedTab = pdfTab;
            }
            else
            {
                setPdfFile(null);
            }

            //download if necessary
            if (!pdfAvail) DownloadPdf(current.pnum);
            //if (!htmlAvail) DownloadHtml(current.pnum);
        }

        private void LoadTopBar()
        {
            if (current == null) return;

            //title
            titleBox1.Text = string.Format("{0} - {1}", current.pnum, current.ptitle);

            //check status
            bool tried = false, solved = false;
            if (LocalDatabase.DefaultUser != null)
            {
                solved = LocalDatabase.DefaultUser.IsSolved(current.pnum);
                tried = LocalDatabase.DefaultUser.TriedButUnsolved(current.pnum);
            }

            //check the ac ratio of the problem
            string levelstar = "";
            double ratio = 1.0;
            if (current.Total > 0)
            {
                ratio = (double)current.ac / current.Total;
            }
            if (ratio < 0.35) levelstar = "*";
            else if (ratio <= 0.15) levelstar = "**";
            if (current.Starred) levelstar += "#";

            //status message about the problem
            string msg = "You DID NOT TRY this problem.";
            if (tried) msg = "You TRIED but failed to solve this.";
            if (solved) msg = "You SOLVED this problem.";
            if (current.Marked) msg += " | This problem is MARKED.";
            msg += string.Format(" | Time-limit = {0}", Functions.FormatRuntime(current.rtl));
            msg += string.Format(" | Level = {0:0}{1}", current.Level, levelstar);
            msg += string.Format(" | Dacu = {0}", current.dacu);
            msg += string.Format(" | AC Ratio = {0:0.00}%", 100.0 * current.ac / current.Total);
            msg += string.Format(" | Status = {0}", current.Status);
            problemMessage.Text = msg;
        }

        //
        // Tab Control
        //
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (current == null)
            {
                e.Cancel = true;
                MessageBox.Show("Select a problem first");
                return;
            }

            if (e.TabPage == submissionTab)
            {
                LoadSubmission();
            }
            else if (tabControl1.SelectedTab == pdfTab)
            {
                setPdfFile(LocalDirectory.GetProblemPdf(current.pnum));
            }
        }

        #endregion

        #region Problem Downloader

        private void DownloadPdf(long pnum)
        {
            try
            {
                string format = "http://uva.onlinejudge.org/external/{0}/{1}.pdf"; // 0 = vol; 1 = pnum
                string url = string.Format(format, pnum / 100, pnum);
                string file = LocalDirectory.GetProblemPdf(pnum);
                Downloader.DownloadFileAsync(url, file, pnum,
                    Internet.Priority.Normal, ProgressChanged, DownloadFinished);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Viewer | DownloadPdf()");
            }
        }

        private void DownloadHtml(long pnum)
        {
            try
            {
                string format = "http://uva.onlinejudge.org/external/{0}/{1}.html"; // 0 = vol; 1 = pnum
                string url = string.Format(format, pnum / 100, pnum);
                string file = LocalDirectory.GetProblemHtml(pnum);
                Downloader.DownloadFileAsync(url, file, pnum,
                    Internet.Priority.Normal, ProgressChanged, DownloadFinished);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Viewer | DownloadHtml()");
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
            string text = string.Format("Downloading file : \"{0}\"... {1}% [{2} out of {3}] completed.",
                    file, task.ProgressPercentage, Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.SetStatus(text);
            Interactivity.SetProgress(task.Received, task.Total);
        }

        private void DownloadFinished(DownloadTask task)
        {
            if (task.Error != null)
            {
                Logger.Add(task.Error.Message, "ProblemViewer | DownloadFinished()");
                return;
            }

            /*
            this.BeginInvoke((MethodInvoker)delegate
            {
                problemWebBrowser.Refresh();
            });
            */

            bool finish = false;
            long pnum = (long)task.Token;
            if (current == null || current.pnum != pnum)
                finish = true;

            if (!finish) //if no error occured
            {
                string ext = Path.GetExtension(task.FileName);
                if (ext == ".pdf")
                {
                    if (LocalDirectory.GetFileSize(task.FileName) > 200)
                    {
                        setPdfFile(task.FileName);
                        finish = true;
                    }
                }
                /*
                else if (ext == ".html")
                {
                    if (LocalDirectory.GetFileSize(task.FileName) > 100)
                    {
                        problemWebBrowser.Navigate(task.FileName);
                        int cnt = DownloadContents(pnum);
                        if (cnt == 0) finish = true;
                    };

                }
                else
                {
                    finish = true;
                }
                */
            }

            if (finish)
            {
                reloadButton.Enabled = true;
            }
        }

        #endregion

        #region Top bar

        private void tagsEditorToolButton_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Application.StartupPath, "CategoryEditor.exe");
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Could not found category editor at: \r\n" + path);
            }
        }

        private void tagsOrNoteToolButton_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Select a problem first.");
                return;
            }
            ProblemCategoryViewer pcv = new ProblemCategoryViewer(current);
            pcv.ShowDialog();
        }

        private void codeButton_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Select a problem first.");
                return;
            }
            Interactivity.ShowCode(current.pnum);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Select a problem first.");
                return;
            }
            Interactivity.SubmitCode(current.pnum);
        }

        private void expandViewButton1_Click(object sender, EventArgs e)
        {
            Interactivity.problems.CollapsePanel1View();
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
            reloadButton.Enabled = false;
            if (current == null) return;
            //if (tabControl1.SelectedTab == htmlTab)
            //    DownloadHtml(current.pnum);
            if (tabControl1.SelectedTab == pdfTab)
            {
                DownloadPdf(current.pnum);
            }
        }

        private void externalButton_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Select a problem first.");
                return;
            }
            string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", current.Volume, current.pnum);
            System.Diagnostics.Process.Start(url);
        }

        private void pdfToolButton_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Select a problem first.");
                return;
            }
            string pdf = LocalDirectory.GetProblemPdf(current.pnum);
            if (LocalDirectory.GetFileSize(pdf) > 200)
            {
                System.Diagnostics.Process.Start(pdf);
            }
            else
            {
                DownloadPdf(current.pnum);
            }
        }

        private void markButton_Click(object sender, EventArgs e)
        {
            if (current == null)
            {
                MessageBox.Show("Select a problem first.");
                return;
            }

            List<long> fav = RegistryAccess.FavoriteProblems;
            if (current.Marked) fav.Remove(current.pnum);
            else fav.Add(current.pnum);

            RegistryAccess.FavoriteProblems = fav;
            markButton.Checked = !markButton.Checked;

            current.Marked = markButton.Checked;

            if (Interactivity.problems.markedButton.Checked)
                Interactivity.problems.ShowFavorites();
        }

        private void markButton_CheckedChanged(object sender, EventArgs e)
        {
            markButton.Text = markButton.Checked ? "Unmark" : "Mark";
        }

        //
        // Show hide toolbar
        //
        private void up_downButton_Click(object sender, EventArgs e)
        {
            bool val = !problemMessage.Visible;
            problemMessage.Visible = val;
            toolStrip1.Visible = val;
            if (val)
            {
                up_downButton.Image = Properties.Resources.moveup;
                this.tableLayoutPanel1.SetRowSpan(this.up_downButton, 2);
                //this.Controls.Remove(this.pdfViewer1);
                this.pdfTab.Controls.Add(this.pdfViewer1);
            }
            else
            {
                up_downButton.Image = Properties.Resources.movedown;
                this.tableLayoutPanel1.SetRowSpan(this.up_downButton, 1);
                up_downButton.Height = 28;
                //this.pdfTab.Controls.Remove(this.pdfViewer1);
                this.Controls.Add(this.pdfViewer1);
                this.pdfViewer1.BringToFront();
            }
        }
        #endregion

        #region Submission Tab

        enum SubViewType
        {
            LastSubmission,
            Ranklist,
            UsersRank,
            UsersSub,
            Comapre
        }

        private SubViewType _curSubType = SubViewType.LastSubmission;

        public void LoadUsernameList()
        {
            usernameList1.SetObjects(LocalDatabase.usernames);
            usernameList1.Sort(0);
        }

        #region  View type selection

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
        private void compareUserButton_Click(object sender, EventArgs e)
        {
            _curSubType = SubViewType.Comapre;
            LoadSubmission();
        }

        private void submissionStatus_Click(object sender, EventArgs e)
        {
            if (submissionStatus.Items.Count == 0)
                LoadSubmission();
        }

        private void usernameList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user = "user";
            if (usernameList1.SelectedObject != null)
            {
                user = ((KeyValuePair<string, string>)usernameList1.SelectedObject).Key;
                usernameList1.Tag = user;
            }
            else
            {
                usernameList1.Tag = null;
            }

            showUserSubButton.Text = string.Format((string)showUserSubButton.Tag, user);
            showUsersRankButton.Text = string.Format((string)showUsersRankButton.Tag, user);

            _curSubType = SubViewType.UsersSub;
            LoadSubmission();
        }

        #endregion

        #region Download Submission

        private void LoadSubmission()
        {
            if (current == null) return;

            string user = (string)usernameList1.Tag;
            string uid = LocalDatabase.GetUserid(user);

            submissionStatus.ClearObjects();

            long start, stop;
            string url = "", format;
            switch (_curSubType)
            {
                case SubViewType.LastSubmission:
                    start = UnixTimestamp.ToUnixTime(dateTimePicker1.Value);
                    stop = UnixTimestamp.ToUnixTime(DateTime.Now);
                    format = Config.uHuntBaseUrl + "/api/p/subs/{0}/{1}/{2}"; //pid, unix time start, stop
                    url = string.Format(format, current.pid, start, stop);
                    Interactivity.SetStatus("Downloading last submissions on current problem...");
                    break;
                case SubViewType.Ranklist:
                    start = 1;
                    stop = (long)numericUpDown1.Value;
                    format = Config.uHuntBaseUrl + "/api/p/rank/{0}/{1}/{2}"; //pid, rank start, rank count
                    url = string.Format(format, current.pid, start, stop);
                    Interactivity.SetStatus("Downloading ranks on current problem...");
                    break;
                case SubViewType.UsersRank:
                    start = stop = 10;
                    if (string.IsNullOrEmpty(uid) || uid == "-") return;
                    format = Config.uHuntBaseUrl + "/api/p/ranklist/{0}/{1}/{2}/{3}"; //pid, uid, before_count, after_count
                    url = string.Format(format, current.pid, uid, start, stop);
                    Interactivity.SetStatus("Downloading " + user + "'s rank-data on current problem...");
                    break;
                case SubViewType.UsersSub:
                    if (string.IsNullOrEmpty(uid) || uid == "-") return;
                    format = Config.uHuntBaseUrl + "/api/subs-nums/{0}/{1}/{2}"; //uid, pnum, last sid
                    url = string.Format(format, uid, current.pnum, 0);
                    Interactivity.SetStatus("Downloading " + user + "'s submission on current problem...");
                    break;
                case SubViewType.Comapre:
                    List<string> uidcol = new List<string>();
                    foreach (var val in LocalDatabase.usernames.Values) uidcol.Add(val);
                    if (uidcol.Count == 0) return;
                    format = Config.uHuntBaseUrl + "/api/subs-nums/{0}/{1}/0"; //uids(sep = comma), pnum
                    url = string.Format(format, string.Join(",", uidcol.ToArray()), current.pnum);
                    Interactivity.SetStatus("Comparing user's on current problem...");
                    break;
            }

            Downloader.DownloadStringAsync(url, user, Priority.Normal, dt_progressChanged, dt_taskCompleted, 1);
        }

        private void dt_progressChanged(DownloadTask task)
        {
            string status = string.Format("Downloading submission data on problem... [{0} of {1} received]",
               Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Interactivity.SetStatus(status);
            Interactivity.SetProgress(task.ProgressPercentage);
        }

        private void dt_taskCompleted(DownloadTask task)
        {
            //check validity of result
            if (task.Status != ProgressStatus.Completed)
            {
                if (task.Error != null)
                {
                    Interactivity.SetStatus("Failed to download submission data on problem.");
                    Logger.Add(task.Error.Message, "Problem Viewer | dt_taskCompleted(DownloadTask task)");
                }
                return;
            }

            try
            {
                string user = (string)task.Token;

                //set result to listview
                if (_curSubType == SubViewType.UsersSub)
                {
                    task.Result = task.Result.Remove(0, task.Result.IndexOf(":") + 1);
                    task.Result = task.Result.Remove(task.Result.Length - 1);
                    UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(task.Result);
                    uinfo.Process();
                    submissionStatus.SetObjects(uinfo.submissions);
                }
                else if (_curSubType == SubViewType.Comapre)
                {
                    List<UserSubmission> allsubs = new List<UserSubmission>();

                    string data = task.Result.Substring(1, task.Result.Length - 2);
                    do
                    {
                        int i = data.IndexOf("{");
                        if (i < 0) break;
                        int j = data.IndexOf("}", i);
                        if (j < 0) break;

                        string tmp = data.Substring(i, j - i + 1);
                        UserInfo uinfo = JsonConvert.DeserializeObject<UserInfo>(tmp);
                        uinfo.Process();
                        allsubs.AddRange(uinfo.submissions.ToArray());

                        data = data.Substring(j + 1);
                    }
                    while (data.Length > 0);
                    submissionStatus.SetObjects(allsubs);
                }
                else
                {
                    List<SubmissionMessage> lsm =
                        JsonConvert.DeserializeObject<List<SubmissionMessage>>(task.Result);
                    if (lsm == null) return;
                    submissionStatus.SetObjects(lsm);
                }

                //sort listview and set other flags
                submissionStatus.ShowGroups = false;
                switch (_curSubType)
                {
                    case SubViewType.LastSubmission:
                        submissionStatus.Sort(sidSUB, SortOrder.Descending);
                        subListLabel.Text = "Last submissions on this problem from " +
                            dateTimePicker1.Value.ToString();
                        break;
                    case SubViewType.Ranklist:
                        submissionStatus.Sort(rankSUB, SortOrder.Ascending);
                        subListLabel.Text = "Rank-list of this problem displaying first " +
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
                    case SubViewType.Comapre:
                        submissionStatus.BuildGroups(unameSUB, SortOrder.Ascending);
                        subListLabel.Text = "Comparison between all users in this problem";
                        break;
                }
            }
            catch (Exception ex)
            {
                Interactivity.SetStatus("Failed to download submission data on problem.");
                Logger.Add(ex.Message, "Problem Viewer | dt_taskCompleted(DownloadTask task)");
            }
        }
        #endregion

        #region Cell formatter

        private void AssignAspectToSubList()
        {
            subtimeSUB.AspectToStringConverter = delegate (object dat)
            {
                if (dat == null) return "";
                return UnixTimestamp.FormatUnixTime((long)dat);
            };
            lanSUB.AspectToStringConverter = delegate (object dat)
            {
                if (dat == null) return "";
                return Functions.GetLanguage((Language)((long)dat));
            };
            verSUB.AspectToStringConverter = delegate (object dat)
            {
                if (dat == null) return "";
                return Functions.GetVerdict((Verdict)((long)dat));
            };
            runSUB.AspectToStringConverter = delegate (object dat)
            {
                if (dat == null) return "";
                return Functions.FormatRuntime((long)dat);
            };
            rankSUB.AspectToStringConverter = delegate (object dat)
            {
                if (dat == null) return "";
                if ((long)dat == -1) return "-";
                return ((long)dat).ToString();
            };
        }

        private void submissionStatus_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            if (e.Model.GetType() != typeof(SubmissionMessage)) return;

            SubmissionMessage list = (SubmissionMessage)e.Model;
            if (e.Column == unameSUB)
            {
                if (LocalDatabase.ContainsUser(list.uname))
                {
                    Interactivity.ShowUserStat(list.uname);
                }
                else
                {
                    if (MessageBox.Show("Add \"" + list.uname + "\" to your favorite list?", "Add User",
                        MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                    RegistryAccess.AddUserid(list.uname, list.uid.ToString());
                    Interactivity.ShowUserStat(list.uname);
                }
            }
        }


        private void submissionStatus_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Model == null) return;

            //get two properties that are used later
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

            //change backcolor of items with known user name
            if (!(_curSubType == SubViewType.UsersSub ||
                    _curSubType == SubViewType.Comapre))
            {
                if (uname == RegistryAccess.DefaultUsername)
                {
                    for (int i = 0; i < e.Item.SubItems.Count; ++i)
                        e.Item.SubItems[i].BackColor = Color.Turquoise;
                }
                else if (LocalDatabase.ContainsUser(uname))
                {
                    for (int i = 0; i < e.Item.SubItems.Count; ++i)
                        e.Item.SubItems[i].BackColor = Color.LightBlue;
                }
            }

            //format other cells
            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;

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

        //
        // Username List view

        private void usernameList1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (RegistryAccess.DefaultUsername == ((KeyValuePair<string, string>)e.Model).Key)
            {
                e.SubItem.BackColor = Color.Linen;
            }

            if (e.Column == unameCol)
            {
                e.SubItem.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular);
                e.SubItem.ForeColor = Color.Navy;
            }
            else if (e.Column == uidCol)
            {
                e.SubItem.Font = new Font("Consolas", 9.0F, FontStyle.Italic);
                e.SubItem.ForeColor = Color.DarkSlateGray;
            }
        }

        #endregion

        #endregion

    }
}

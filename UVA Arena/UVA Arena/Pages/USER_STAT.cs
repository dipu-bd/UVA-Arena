using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena.Elements
{
    public partial class USER_STAT : UserControl
    {
        public UserInfo currentUser = null;
        private List<UserRanklist> worldRanks;

        #region Top Level

        private WebClient webClient1 = new WebClient();
        private WebClient webClient2 = new WebClient();

        public USER_STAT()
        {
            InitializeComponent();

            //add progress tracker
            Interactivity.progTracker = new UserProgTracker();
            Interactivity.progTracker.Dock = DockStyle.Fill;
            progtrackerTab.Controls.Add(Interactivity.progTracker);

            //add compare user
            Interactivity.compareUser = new CompareUsers();
            Interactivity.compareUser.Dock = DockStyle.Fill;
            compareTab.Controls.Add(Interactivity.compareUser);

            webClient1.DownloadProgressChanged += webClient1_DownloadProgressChanged;
            webClient1.DownloadDataCompleted += webClient1_DownloadDataCompleted;
            webClient2.DownloadProgressChanged += webClient2_DownloadProgressChanged;
            webClient2.DownloadDataCompleted += webClient2_DownloadDataCompleted;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadUsernames();
            SetViewMenu();
            AssignAspectFunctions();
            SelectUpdateRateMenu();
            lastSubmissions1.MakeColumnSelectMenu(MainContextMenu);

            ShowUserSub(RegistryAccess.DefaultUsername);

            Stylish.SetGradientBackground(titleBackPanel,
                new Stylish.GradientStyle(Color.PowderBlue, Color.PaleTurquoise, 90F));
        }

        //
        // Public Functions
        // 
        public void ShowUserSub(string user)
        {
            tabControl1.SelectedTab = submissionTab;
            LoadUserSub(user);
        }

        //
        //Private functions
        //
        private void SelectUpdateRateMenu()
        {
            string tag = UpdateInterval.ToString();
            if (AutoUpdateStatus) UserStatRefresh();
            foreach (ToolStripItem item in updateContextMenu.Items)
            {
                if (item.Tag != null && item.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tmi = (ToolStripMenuItem)item;
                    tmi.Checked = tag.Equals(tmi.Tag);
                    tmi.Enabled = AutoUpdateStatus;
                }
            }
        }

        private void SetViewMenu()
        {
            foreach (ToolStripItem tsi in viewContextMenu.Items)
            {
                if (tsi.GetType() == typeof(ToolStripMenuItem))
                    ((ToolStripMenuItem)tsi).Checked = false;
            }

            switch (ViewOption)
            {
                case -1:
                    allToolStripMenuItem.Checked = true;
                    break;
                case -10:
                    acceptedToolStripMenuItem.Checked = true;
                    break;
                case -20:
                    notAcceptedToolStripMenuItem.Checked = true;
                    break;
                case 100:
                    recent100ToolStripMenuItem.Checked = true;
                    break;
                case 500:
                    recent500ToolStripMenuItem.Checked = true;
                    break;
                case 1000:
                    recent1000ToolStripMenuItem.Checked = true;
                    break;
            }
        }

        #endregion

        #region Properties

        public static int ViewOption
        {
            get
            {
                return Properties.Settings.Default.UserStatViewOption;
            }
            set
            {
                Properties.Settings.Default.UserStatViewOption = value;
            }
        }

        public static int UpdateInterval
        {
            get
            {
                return Properties.Settings.Default.UserStatUpdateInterval;
            }
            set
            {
                Properties.Settings.Default.UserStatUpdateInterval = value;
            }
        }

        public bool AutoUpdateStatus
        {
            get
            {
                return Properties.Settings.Default.AutoUpdateUserStat;
            }
            set
            {
                Properties.Settings.Default.AutoUpdateUserStat = value;
                if (value && !_taskRunning) UserStatRefresh();
            }
        }

        #endregion

        #region Username List

        //
        // Add Username
        //
        private void usernameButton_Click(object sender, EventArgs e)
        {
            string user = usernameBox.Text;
            if (LocalDatabase.ContainsUser(user))
            {
                usernameStatus.Text = "Already added.";
            }
            else
            {
                Internet.Downloader.DownloadUserid(user, username_completed);
                usernameButton.UseWaitCursor = true;
                usernameStatus.Text = "Getting user-id...";
            }
        }

        private void username_completed(Internet.DownloadTask task)
        {
            usernameButton.UseWaitCursor = false;

            if (task.Status == Internet.ProgressStatus.Completed)
            {
                LoadUsernames();
                usernameBox.Text = "";
                usernameStatus.Text = "Added " + task.Token.ToString();
            }
            else
            {
                usernameStatus.Text = task.Error.Message;
            }
        }

        private void usernameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                usernameButton.PerformClick();
            }
            else if (e.KeyCode == Keys.Back && e.Control)
            {
                usernameBox.Clear();
                e.SuppressKeyPress = true;
            }
        }

        //
        // Username list
        //

        private void SelectUsername(string user)
        {
            string uid = LocalDatabase.GetUserid(user);
            for (int i = 0; i < usernameList.GetItemCount(); ++i)
            {
                object model = usernameList.GetModelObject(i);
                if (((KeyValuePair<string, string>)model).Value == uid)
                {
                    usernameList.EnsureModelVisible(i);
                    usernameList.SelectObject(model);
                    break;
                }
            }
        }

        public void LoadUsernames()
        {
            usernameList.SetObjects(LocalDatabase.usernames);
            if (currentUser != null && !LocalDatabase.ContainsUser(currentUser.uname))
            {
                LoadUserSub(RegistryAccess.DefaultUsername);
            }
        }

        private void usernameList_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
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

        private void usernameList_SelectionChanged(object sender, EventArgs e)
        {
            object sel = usernameList.SelectedObject;
            if (sel == null) return;
            LoadUserSub(((KeyValuePair<string, string>)sel).Key);
        }

        #endregion

        #region Show data by tab

        private void ShowDataByTab()
        {
            Interactivity.SetStatus();
            if (currentUser == null) return;

            //change view and looks
            userNameTitle.Text = string.Format(userNameTitle.Tag.ToString(),
                            currentUser.name, currentUser.uname);

            if (tabControl1.SelectedTab == submissionTab)
            {
                SetSubmissionToListView();
            }
            else if (tabControl1.SelectedTab == progtrackerTab)
            {
                Interactivity.progTracker.ShowUserInfo(currentUser);
            }
            else if (tabControl1.SelectedTab == worldrankTab)
            {
                ShowWorldRank(-1);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
                ShowDataByTab();
        }

        #endregion

        #region Load User's Submissions

        /// <summary>
        /// Load user submissions information to currentUser
        /// </summary>
        /// <param name="user">Username to load</param>
        public void LoadUserSub(string user)
        {
            //if 'user' is already loaded then do nothing
            if (currentUser != null && currentUser.uname == user) return;
            if (!LocalDatabase.ContainsUser(user)) return;

            worldRanklist.ClearObjects();
            lastSubmissions1.ClearObjects();
            Interactivity.progTracker.ShowUserInfo(null);

            try
            {
                Interactivity.SetStatus();

                //if current user is default user then get it from LocalDatabase
                if (user == RegistryAccess.DefaultUsername)
                {
                    currentUser = LocalDatabase.DefaultUser;
                }
                else
                {
                    //load current user from json data stored on documents
                    string file = LocalDirectory.GetUserSubPath(user);
                    string json = File.ReadAllText(file);
                    currentUser = JsonConvert.DeserializeObject<UserInfo>(json);
                }

                //if no data could found, create a new instance
                if (currentUser == null)
                {
                    currentUser = new UserInfo(user);
                    if (user == RegistryAccess.DefaultUsername)
                        LocalDatabase.DefaultUser = currentUser;
                }

                //process loaded data
                //-> this is very important
                currentUser.Process();

                //if no previous data exist
                if (currentUser.LastSID == 0)
                {
                    DownloadUserSubs(user);
                }
                else
                {
                    //show list 
                    ShowDataByTab();
                }

                //if auto update is off download once
                if (!AutoUpdateStatus)
                {
                    DownloadUserSubs(user);
                }

                //select user
                SelectUsername(user);
            }
            catch (Exception ex)
            {
                Interactivity.SetStatus("Error while loading user submissions.");
                Logger.Add(ex.Message, "User Statistics | LoadUserSub(string user)");
            }
        }

        public void DownloadUserSubs(string user)
        {
            if (webClient1.IsBusy || currentUser == null || currentUser.uname != user) return;
            string format = Config.uHuntBaseUrl + "/api/subs-user/{0}/{1}";
            string url = string.Format(format, currentUser.uid, currentUser.LastSID);
            Interactivity.SetStatus("Downloading " + user + "'s submissions...");
            webClient1.DownloadDataAsync(new Uri(url), currentUser.uname);
        }


        void webClient1_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Interactivity.SetProgress(e.ProgressPercentage);
            string msg = "Downloading {0}'s submissions... [{1} out of {2}]";
            Interactivity.SetStatus(string.Format(msg, e.UserState,
                Functions.FormatMemory(e.BytesReceived), Functions.FormatMemory(e.TotalBytesToReceive), true));
        }

        void webClient1_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (this.IsDisposed) return;
            try
            {
                Interactivity.SetProgress();
                if (e.Cancelled) throw new OperationCanceledException();
                if (e.Error != null) throw e.Error;

                string result = System.Text.Encoding.UTF8.GetString(e.Result);

                if (currentUser != null)
                {
                    if (currentUser.uname != (string)e.UserState) return;
                    currentUser.AddSubmissions(result);
                }
                else
                {
                    currentUser = JsonConvert.DeserializeObject<UserInfo>(result);
                    currentUser.Process();
                }

                string file = LocalDirectory.GetUserSubPath(currentUser.uname);
                string data = currentUser.GetJSONData();
                File.WriteAllText(file, data);

                ShowDataByTab();

                string msg = string.Format("Downloaded {0}'s submissions", e.UserState);
                Interactivity.SetStatus(msg);
                if (currentUser.LastSID == 0) Logger.Add(msg, "User Statistics");
            }
            catch (Exception ex)
            {
                Interactivity.SetStatus(string.Format("Error while downloading {0}'s submissions.", e.UserState));
                Logger.Add(ex.Message, "UserStat | webClient1_DownloadDataCompleted");
            }
            finally
            {
                LastUpdate = DateTime.Now;
            }
        }

        #endregion

        #region Auto Update Timer

        private bool _taskRunning = false;
        private DateTime LastUpdate = DateTime.Now;

        //
        // Timer
        // 
        private void UserStatRefresh()
        {
            _taskRunning = false;
            if (this.IsDisposed || !AutoUpdateStatus) return;
            TaskQueue.AddTask(UserStatRefresh, 800);
            _taskRunning = true;

            //check if this is focused
            if (this.tabControl1.SelectedTab != submissionTab) return;
            if (Interactivity.mainForm.customTabControl1.SelectedTab
                != Interactivity.mainForm.profileTab) return;

            //refresh list
            lastSubmissions1.Refresh();

            //check if update needed
            if (webClient1.IsBusy || !AutoUpdateStatus || currentUser == null) return;

            //update
            TimeSpan span = DateTime.Now.Subtract(LastUpdate);
            long diff = (long)span.TotalMilliseconds;
            if (diff >= UpdateInterval)
            {
                DownloadUserSubs(currentUser.uname);
            }
            else
            {
                //show status about when to update 
                long inv = (long)Math.Ceiling((UpdateInterval - diff) / 1000.0);
                string msg = Functions.FormatTimeSpan(inv);
                Interactivity.SetStatus("Updating user submissions in " + msg);
            }
        }

        #endregion

        #region Context Menu Item

        private void groupMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripItem tsi in groupContextMenu.Items)
            {
                if (tsi.GetType() == typeof(ToolStripMenuItem))
                    ((ToolStripMenuItem)tsi).Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;
            SetGroupBy();
        }

        private void viewMenuItem_Click(object sender, EventArgs e)
        {
            string tag = ((ToolStripItem)sender).Tag.ToString();
            ViewOption = int.Parse(tag);
            SetViewMenu();
            SetSubmissionToListView();
        }

        //
        // Auto Update
        //
        private void updateRateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tmi = (ToolStripMenuItem)sender;
            int inv = int.Parse(tmi.Tag.ToString());
            UpdateInterval = inv;
            SelectUpdateRateMenu();
        }

        private void autoUpdateToolMenu_Click(object sender, EventArgs e)
        {
            SelectUpdateRateMenu();
        }

        private void refreshListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadUsernames();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usernameList.SelectedObject == null) return;
            string val = ((KeyValuePair<string, string>)usernameList.SelectedObject).Key;

            if (val == RegistryAccess.DefaultUsername)
            {
                MessageBox.Show("You can not remove default user from this list.");
                return;
            }

            if (MessageBox.Show("Are you sure to delete '" + val + "' from list?",
                "Delete User?", MessageBoxButtons.YesNo) == DialogResult.No) return;

            RegistryAccess.DeleteUserid(val);
            LoadUsernames();
        }

        #endregion

        #region Submissions List

        private void refreshSubmissionsItem_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            DownloadUserSubs(currentUser.uname);
        }

        private void redownloadSubmissionsButton_Click(object sender, EventArgs e)
        {
            if (currentUser == null) return;
            currentUser.LastSID = 0;
            DownloadUserSubs(currentUser.uname);
        }

        private void lastSubmissions1_Click(object sender, EventArgs e)
        {
            if (lastSubmissions1.GetItemCount() == 0 && currentUser != null)
                DownloadUserSubs(currentUser.uname);
        }

        private void SetSubmissionToListView()
        {
            if (currentUser == null) return;

            //load submission list according to view option
            List<UserSubmission> list = new List<UserSubmission>();
            switch (ViewOption)
            {
                case -1: //all submissions
                    list = currentUser.submissions;
                    break;

                case -10: //ac submissions
                    foreach (UserSubmission usub in currentUser.submissions)
                    {
                        if (currentUser.IsSolved(usub.pnum))
                            list.Add(usub);
                    }
                    break;

                case -20: //not ac submission
                    foreach (UserSubmission usub in currentUser.submissions)
                    {
                        if (currentUser.TriedButUnsolved(usub.pnum))
                            list.Add(usub);
                    }
                    break;

                default: //ViewOption submissions
                    int cnt = ViewOption;
                    for (int i = currentUser.submissions.Count - 1; i >= 0 && cnt > 0; --i, --cnt)
                    {
                        list.Add(currentUser.submissions[i]);
                    }
                    break;
            }

            bool empty = (lastSubmissions1.GetItemCount() == 0);
            lastSubmissions1.ShowGroups = false;
            lastSubmissions1.SetObjects(list, true);
            if (empty) lastSubmissions1.Sort(sidSUB, SortOrder.Descending);
            SetGroupBy();
        }

        private void SetGroupBy()
        {
            if (noneToolStripMenuItem.Checked)
                lastSubmissions1.ShowGroups = false;
            else if (problemNameToolStripMenuItem.Checked)
                lastSubmissions1.BuildGroups(ptitleSUB, SortOrder.Ascending);
            else if (verdictToolStripMenuItem.Checked)
                lastSubmissions1.BuildGroups(verSUB, SortOrder.Ascending);
            else if (languageToolStripMenuItem.Checked)
                lastSubmissions1.BuildGroups(lanSUB, SortOrder.Ascending);
        }

        private void AssignAspectFunctions()
        {
            subtimeSUB.AspectGetter = delegate (object row)
            {
                if (row == null) return null;
                UserSubmission last = (UserSubmission)row;
                return UnixTimestamp.FormatUnixTime(last.sbt);
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

            ptitleSUB.ImageGetter = delegate (object data)
            {
                if (data == null || data.GetType() != typeof(UserSubmission))
                    return null;

                UserSubmission usub = (UserSubmission)data;
                if (LocalDatabase.DefaultUser == null)
                    return Properties.Resources.file;
                if (LocalDatabase.DefaultUser.IsSolved(usub.pnum))
                    return Properties.Resources.accept;
                else if (LocalDatabase.DefaultUser.TriedButUnsolved(usub.pnum))
                    return Properties.Resources.tried;
                else
                    return Properties.Resources.file;
            };
        }
        private void lastSubmissions1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Model == null) return;

            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;

            UserSubmission js = (UserSubmission)e.Model;

            if (e.Column == sidSUB)
            {
                font = "Consolas";
                fore = Color.Teal;
                size = 8.5F;
            }
            else if (e.Column == pnumSUB)
            {
                font = "Consolas";
                fore = Color.Navy;
                style = FontStyle.Italic;
            }
            else if (e.Column == ptitleSUB)
            {
                font = "Segoe UI Semibold";
                fore = Functions.GetProblemTitleColor(js.pnum);
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
                long ver = ((UserSubmission)e.Model).ver;
                fore = Functions.GetVerdictColor((Verdict)ver);
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

        private void lastSubmissions1_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            if (e.Model == null) return;

            UserSubmission usub = (UserSubmission)e.Model;

            if (e.Column == pnumSUB || e.Column == ptitleSUB)
            {
                Interactivity.ShowProblem(usub.pnum);
            }
        }


        #endregion

        #region World Rank

        private void reloadButton_Click(object sender, EventArgs e)
        {
            ShowWorldRank(-1);
        }

        private void showUser_Click(object sender, EventArgs e)
        {
            ShowWorldRank((int)rankSelector.Value);
        }

        private void worldRanklist_MouseClick(object sender, MouseEventArgs e)
        {
            if (worldRanklist.GetItemCount() == 0) ShowWorldRank(-1);
        }

        /// <summary>
        /// Runs a background thread to download and show world rank data.
        /// </summary>
        /// <param name="from">Integer type objects </param>
        private void ShowWorldRank(object from)
        {
            if (currentUser == null) return;

            if (webClient2.IsBusy)
            {
                webClient2.CancelAsync();
                TaskQueue.AddTask(new TaskQueue.Function2(ShowWorldRank), from, 500);
                return;
            }

            string url;
            if ((int)from <= 0)
            {
                //get current user's ranklist
                string format = Config.uHuntBaseUrl + "/api/ranklist/{0}/{1}/{2}";
                url = string.Format(format, currentUser.uid, 100, 100);
                Interactivity.SetStatus("Downloading " + currentUser.uname + "'s rank-list...");
            }
            else
            {
                //get ranklist from a specific rank
                string format = Config.uHuntBaseUrl + "/api/rank/{0}/{1}";
                url = string.Format(format, from, 200);
                Interactivity.SetStatus("Downloading rank-list from " + from.ToString() + "...");
            }

            webClient2.DownloadDataAsync(new Uri(url), from);
        }

        void webClient2_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Interactivity.SetProgress(e.ProgressPercentage);
            Interactivity.SetStatus(string.Format("Downloading rank-list... [{0} out of {1}]",
                Functions.FormatMemory(e.BytesReceived), Functions.FormatMemory(e.TotalBytesToReceive)));
        }

        void webClient2_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (this.IsDisposed) return;
            if (e.Cancelled) return;

            UserRanklist currentRank = null;

            try
            {
                if (e.Error != null) throw e.Error;

                string result = System.Text.Encoding.UTF8.GetString(e.Result);

                if (worldRanks != null) worldRanks.Clear();
                worldRanks = JsonConvert.DeserializeObject<List<UserRanklist>>(result);

                foreach (UserRanklist usub in worldRanks)
                {
                    if (LocalDatabase.ContainsUser(usub.username))
                    {
                        RegistryAccess.SetUserRank(usub);
                        if (usub.username == currentUser.uname)
                            currentRank = usub;
                    }
                }

                worldRanklist.SetObjects(worldRanks);
                worldRanklist.Sort(rankRANK, SortOrder.Ascending);


                Interactivity.SetStatus(currentUser.uname + "'s rank-list downloaded.");
                Logger.Add("World rank downloaded - " + currentUser.uname, "World Rank | webClient2_DownloadDataCompleted");
            }
            catch (Exception ex)
            {
                TaskQueue.AddTask(new TaskQueue.Function2(ShowWorldRank), e.UserState, 500);
                Interactivity.SetStatus("Rank-list download failed due to an error. Please try again.");
                Logger.Add(ex.Message, "World Rank | webClient2_DownloadDataCompleted");
            }

            if ((int)e.UserState == -1) BringUserToMiddle(currentRank);
        }

        private void BringUserToMiddle(UserRanklist usub)
        {
            if (usub == null) return;

            int midpt = worldRanklist.Height / 2;
            int item = worldRanklist.GetItemAt(1, midpt).Index;
            int indx = worldRanklist.IndexOf(usub);
            for (int i = indx; i < worldRanklist.GetItemCount() && indx > item; ++i)
            {
                worldRanklist.EnsureVisible(i);
                item = worldRanklist.GetItemAt(1, midpt).Index;
            }
        }

        private void worldRanklist_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            if (e.Column == usernameRANK)
            {
                UserRanklist js = (UserRanklist)e.Model;
                usernameBox.Text = js.username;
                usernameBox.Focus();
            }
        }

        private void worldRanklist_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Model == null) return;

            //change back-color of known users
            UserRanklist js = (UserRanklist)e.Model;
            if (js.username == RegistryAccess.DefaultUsername)
            {
                for (int i = 0; i < e.Item.SubItems.Count; ++i)
                    e.Item.SubItems[i].BackColor = Color.Turquoise;
            }
            else if (LocalDatabase.ContainsUser(js.username))
            {
                for (int i = 0; i < e.Item.SubItems.Count; ++i)
                    e.Item.SubItems[i].BackColor = Color.LightBlue;
            }

            //format other cells
            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;
            if (e.Column == rankRANK)
            {
                font = "Consolas";
                fore = Color.Teal;
            }
            else if (e.Column == usernameRANK)
            {
                fore = Color.Blue;
            }
            else if (e.Column == nameRANK)
            {
                font = "Segoe UI Semibold";
            }
            else if (e.Column == acRANK)
            {
                font = "Consolas";
                fore = Color.Navy;
            }
            else if (e.Column == nosRANK)
            {
                font = "Consolas";
                fore = Color.Maroon;
            }
            else { return; }

            e.SubItem.ForeColor = fore;
            e.SubItem.Font = new Font(font, size, style);
        }


        #endregion


    }
}

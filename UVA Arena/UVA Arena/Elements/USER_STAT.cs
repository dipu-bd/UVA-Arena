using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;
using UVA_Arena.Internet;
using Newtonsoft.Json;

namespace UVA_Arena.Elements
{
    public partial class USER_STAT : UserControl
    {
        #region Top Level

        public USER_STAT()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadUsernames();
            SetViewMenu();
            AssignAspectFunctions();
            SelectUpdateRateMenu();
            autoUpdateToolMenu.Checked = AutoUpdateStatus;
            lastSubmissions1.MakeColumnSelectMenu(MainContextMenu);
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
            bool enable = AutoUpdateStatus;
            string tag = UpdateInterval.ToString();

            timer1.Enabled = enable;
            autoUpdateToolMenu.Checked = enable;
            foreach (ToolStripItem item in updateContextMenu.Items)
            {
                if (item.GetType() == typeof(ToolStripMenuItem)
                    && autoUpdateToolMenu != item)
                {
                    ToolStripMenuItem tmi = (ToolStripMenuItem)item;
                    tmi.Checked = tag.Equals(tmi.Tag);
                    tmi.Enabled = (enable);
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

        #region Registry Access

        public static int ViewOption
        {
            get
            {
                object val = RegistryAccess.GetValue("User State View Option");
                if (val == null || val.GetType() != typeof(int)) return -1;
                return (int)val;
            }
            set
            {
                RegistryAccess.SetValue("User State View Option", value,
                    null, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        public static int UpdateInterval
        {
            get
            {
                object val = RegistryAccess.GetValue("User State Update Interval");
                if (val == null || val.GetType() != typeof(int)) return 5000;
                return (int)val;
            }
            set
            {
                RegistryAccess.SetValue("User State Update Interval", value,
                    null, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        public bool AutoUpdateStatus
        {
            get
            {
                object val = RegistryAccess.GetValue("Auto Update User State");
                if (val == null || val.GetType() != typeof(int)) return false;
                return ((int)val == 1);
            }
            set
            {
                RegistryAccess.SetValue("Auto Update User State", (value ? 1 : 0),
                    null, Microsoft.Win32.RegistryValueKind.DWord);
                timer1.Enabled = value;
            }
        }

        #endregion

        #region Status Strip

        private void refreshToolButton_Click(object sender, EventArgs e)
        {
            DownloadUserSubs(currentUser.uname);
        }

        public bool _QueueOnRun = false;
        public Queue<string> StatusQueue = new Queue<string>();

        private void ShowNextStatus()
        {
            _QueueOnRun = false;
            if (this.IsDisposed) return;
            if (StatusQueue.Count == 0)
            {
                Status1.Text = "";
                return;
            }
            try
            {
                Status1.Text = StatusQueue.Dequeue();
                _QueueOnRun = true;
                TaskQueue.AddTask(ShowNextStatus, 1000);
            }
            catch { }
        }

        public void SetStatus(string text = "", bool instant = false)
        {
            if (instant)
            {
                if (!_QueueOnRun) Status1.Text = text;
            }
            else
            {
                StatusQueue.Enqueue(text);
                if (!_QueueOnRun) ShowNextStatus();
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
            if (LocalDatabase.ContainsUsers(user))
            {
                usernameStatus.Text = "Already added.";
            }
            else 
            {
                Internet.Downloader.DownloadUserid(user, username_completed);
                usernameButton.UseWaitCursor = true;
                usernameStatus.Text = "Getting userid...";
            }
        }

        private void username_completed(Internet.DownloadTask task)
        {
            usernameButton.UseWaitCursor = false;

            if (task.Status == Internet.ProgressStatus.Completed)
            {
                LoadUsernames();
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
        public void LoadUsernames()
        {
            usernameList.SetObjects(LocalDatabase.usernames);
        }

        private void usernameList_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
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

        private void usernameList_ItemActivate(object sender, EventArgs e)
        {
            object sel = usernameList.SelectedObject;
            if (sel == null)
            {
                userProgTracker1.ShowUserInfo(null);
                return;
            }

            LoadUserSub(((KeyValuePair<string, string>)sel).Key);
        }

        #endregion

        #region Load User's Submissions

        private UserInfo currentUser = null;

        public void LoadUserSub(string user)
        {
            //if 'user' is already loaded then do nothing
            if (currentUser != null && currentUser.uname == user) return;

            try
            {
                //remove previous status
                if (!_QueueOnRun) Status1.Text = "";
                else StatusQueue.Clear();

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

                //if no data could fould, create a new instance
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
            }
            catch (Exception ex)
            {
                SetStatus("Error : " + ex.Message);
                Logger.Add(ex.Message, "User Statistics | LoadUserSub(string user)");
            }
        }

        public void DownloadUserSubs(string user)
        {
            if (currentUser == null || currentUser.uname != user) return;
            Updating = true;
            string format = "http://uhunt.felix-halim.net/api/subs-user/{0}/{1}";
            string url = string.Format(format, currentUser.uid, currentUser.LastSID);
            SetStatus("Downloading " + user + "'s submissions...");
            Internet.Downloader.DownloadStringAsync(url, user, Priority.Normal, dt_progress, dt_completed);
        }

        void dt_progress(DownloadTask Task)
        {
            if (this.IsDisposed) return;
            Progress1.Value = Task.ProgressPercentage;
            SetStatus(string.Format("Downloading {0}'s submissions... [{1} out of {2}]",
                Task.Token, Functions.FormatMemory(Task.Received), Functions.FormatMemory(Task.Total)), true);
        }

        void dt_completed(DownloadTask Task)
        {
            Updating = false;
            LastUpdate = DateTime.Now;

            if (this.IsDisposed) return;
            Progress1.Value = 0;

            try
            {
                if (currentUser != null)
                {
                    if (currentUser.uname != Task.Token.ToString()) return;
                    currentUser.AddSubmissions(Task.Result);
                }
                else
                {
                    currentUser = JsonConvert.DeserializeObject<UserInfo>(Task.Result);
                    currentUser.Process();
                }

                string file = LocalDirectory.GetUserSubPath(currentUser.uname);
                string data = currentUser.GetJsonData();
                File.WriteAllText(file, data);

                ShowDataByTab();

                SetStatus("Downloaded " + Task.Token.ToString() + "'s submissions");
                if (currentUser.LastSID == 0)
                    Logger.Add("Downloaded " + currentUser.uname + "'s submissions", "User Statistics");
            }
            catch (Exception ex)
            {
                SetStatus("Error : " + ex.Message);
                Logger.Add(ex.Message, "User Statistics | dt_completed(DownloadTask Task)");
            }
        }

        #endregion

        #region Auto Update Timer

        private bool Updating = false;
        private DateTime LastUpdate = DateTime.Now;

        //
        // Timer
        // 
        private void timer1_Tick(object sender, EventArgs e)
        {
            //check if this is focused
            if (Interactivity.mainForm.customTabControl1.SelectedTab
                != Interactivity.mainForm.profileTab) return;
            if (this.tabControl1.SelectedTab != submissionTab) return;

            //refresh items
            //lastSubmissions1.Refresh();

            //check if update needed
            if (Updating || !AutoUpdateStatus || currentUser == null) return;

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
                long remain = (long)Math.Ceiling((UpdateInterval - diff) / 1000.0);
                SetStatus("Updating in " + Functions.FormatTimeSpan(remain), true);
            }
        }

        #endregion

        #region Last Submissions List

        private void AssignAspectFunctions()
        {
            subtimeSUB.AspectGetter = delegate(object row)
            {
                if (row == null) return null;
                UserSubmission last = (UserSubmission)row;
                return UnixTimestamp.GetTimeSpan(last.sbt);
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
                if (LocalDatabase.IsProbSolved(js.pnum))
                    fore = Color.Blue;
                else
                    fore = Color.Black;
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
        // Auto Upadte
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
            AutoUpdateStatus = !AutoUpdateStatus;
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
            if (MessageBox.Show("Are you sure to delete '" + val + "' from list?",
                "Delete User?", MessageBoxButtons.YesNo) == DialogResult.No) return;
            RegistryAccess.DeleteUserid(val);
            LoadUsernames();
        }

        #endregion

        #region Show data by tab

        private void ShowDataByTab()
        {
            if (currentUser == null) return;

            if (tabControl1.SelectedTab == submissionTab)
            {
                SetSubmissionToListView();
                if (AutoUpdateStatus) DownloadUserSubs(currentUser.uname);
            }
            else if (tabControl1.SelectedTab == progtrackerTab)
            {
                userProgTracker1.ShowUserInfo(currentUser);
            }
            else if (tabControl1.SelectedTab == worldrankTab)
            {
                ShowWorldRank();
            }
            else if (tabControl1.SelectedTab == compareTab)
            {
                // implement compare tab functionality here
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1) ShowDataByTab();
        }

        #endregion

        #region Show Submissions

        private void SetSubmissionToListView()
        {
            if (currentUser == null) return;

            //set username tile
            userNameTitle.Text = string.Format(userNameTitle.Tag.ToString(), currentUser.name);

            //load submission list according to view option
            List<UserSubmission> list = new List<UserSubmission>();
            switch (ViewOption)
            {
                case -1:
                    list = currentUser.submissions;
                    break;
                case -10:
                    foreach (UserSubmission usub in currentUser.submissions)
                    {
                        if (currentUser.ACList.Contains(usub.pnum))
                            list.Add(usub);
                    }
                    break;
                case -20:
                    foreach (UserSubmission usub in currentUser.submissions)
                    {
                        if (!currentUser.ACList.Contains(usub.pnum))
                            list.Add(usub);
                    }
                    break;
                default:
                    int cnt = ViewOption;
                    for (int i = currentUser.submissions.Count - 1; i >= 0 && cnt > 0; --i, --cnt)
                        list.Add(currentUser.submissions[i]);
                    break;
            }

            lastSubmissions1.ShowGroups = false;
            lastSubmissions1.SetObjects(list);
            lastSubmissions1.Sort(0);
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

        #endregion

        #region World Rank

        private void reloadButton_Click(object sender, EventArgs e)
        {
            ShowWorldRank();
        }

        private void showUser_Click(object sender, EventArgs e)
        {
            ShowWorldRank((int)rankSelector.Value);
        }

        private void ShowWorldRank(int from = -1)
        {
            if (currentUser == null) return;

            string url;
            if (from <= 0)
            {
                //get current user's ranklist
                string format = "http://uhunt.felix-halim.net/api/ranklist/{0}/{1}/{2}";
                url = string.Format(format, currentUser.uid, 100, 100);
                SetStatus("Downloading " + currentUser.uname + "'s ranklist...");
            }
            else
            {
                //get ranklist from a specific rank
                string format = "http://uhunt.felix-halim.net/api/rank/{0}/{1}";
                url = string.Format(format, from, 200);
                SetStatus("Downloading ranklist from " + from.ToString() + "...");
            }

            Downloader.DownloadStringAsync(url, from, Priority.Normal, worldRankProgress, worldRankCompleted);
        }

        private void worldRankProgress(DownloadTask task)
        {
            if (this.IsDisposed) return;
            Progress1.Value = task.ProgressPercentage;
            SetStatus(string.Format("Downloading ranklist... [{0} out of {1}]",
                Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total)), true);
        }

        private void worldRankCompleted(DownloadTask task)
        {
            if (this.IsDisposed) return;
            if (task.Status != ProgressStatus.Completed)
            {
                if (task.Error != null)
                {
                    SetStatus("Ranklist download failed due to an error. Please retry.");
                    Logger.Add(task.Error.Message, "World Rank | worldRankCompleted(DownloadTask task)");
                }
                return;
            }

            List<UserRanklist> ranks = JsonConvert.DeserializeObject<List<UserRanklist>>(task.Result);
            if (ranks == null)
            {
                SetStatus("Boo! No data found! Please retry.");
                return;
            }

            worldRanklist.ClearObjects();
            worldRanklist.SetObjects(ranks);
            worldRanklist.Sort(rankRANK, SortOrder.Ascending);

            if ((int)task.Token < 0)
            {
                int pos = ranks.Count / 2;
                pos += worldRanklist.Height / (2 * worldRanklist.RowHeightEffective) - 2;
                if (pos < 0 || pos >= ranks.Count) pos = ranks.Count / 2;
                worldRanklist.EnsureVisible(pos);
            }

            SetStatus(currentUser.uname + "'s ranklist downloaded.");
            Logger.Add("World rank downloaded - " + currentUser.uname, "World Rank | worldRankCompleted(DownloadTask task)");
            System.GC.Collect();
        }               

        private void worldRanklist_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            if(e.Column == usernameRANK)
            {
                UserRanklist js = (UserRanklist)e.Model;
                usernameBox.Text = js.username; 
                usernameBox.Focus();
            }
        }

        private void worldRanklist_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Model == null) return;

            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;

            UserRanklist js = (UserRanklist)e.Model;

            if (js.username == RegistryAccess.DefaultUsername)
            {
                for (int i = 0; i < e.Item.SubItems.Count; ++i)
                {
                    e.Item.SubItems[i].BackColor = Color.Turquoise;
                }
            }
            else if (LocalDatabase.ContainsUsers(js.username))
            {
                for (int i = 0; i < e.Item.SubItems.Count; ++i)
                {
                    e.Item.SubItems[i].BackColor = Color.LightBlue;
                }
            }

            if (e.Column == rankRANK)
            {
                font = "Consolas";
                fore = Color.Teal;                
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

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
            timer1.Enabled = AutoUpdateStatus;
            autoUpdateToolMenu.Checked = AutoUpdateStatus;
            lastSubmissions1.MakeColumnSelectMenu(MainContextMenu);
        }

        //
        //Private functions
        //
        private void SelectUpdateRateMenu()
        {
            bool enable = AutoUpdateStatus;
            string tag = UpdateInterval.ToString();
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
                if (val == null || val.GetType() != typeof(int)) return true;
                return ((int)val == 1);
            }
            set
            {
                RegistryAccess.SetValue("Auto Update User State", (value ? 0 : 1),
                    null, Microsoft.Win32.RegistryValueKind.DWord);
                timer1.Enabled = value;
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
            Internet.Downloader.DownloadUserid(user, username_completed);
            usernameButton.UseWaitCursor = true;
            usernameStatus.Text = "Getting userid...";
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
                usernameStatus.Text = "Failed " + task.Token.ToString();
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
            usernameList.SetObjects(DefaultDatabase.usernames);
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
            if (usernameList.SelectedObject == null) return;
            ShowUserSubs(((KeyValuePair<string, string>)usernameList.SelectedObject).Key);
        }

        #endregion

        #region Show Submissions

        private UserInfo currentUser = null;

        public void ShowUserSubs(string user)
        {
            if (currentUser != null && currentUser.uname == user) return;

            try
            {
                if (user == RegistryAccess.DefaultUsername)
                {
                    currentUser = DefaultDatabase.DefaultUser;
                }
                else
                {
                    string file = LocalDirectory.GetUserSubPath(user);
                    string json = File.ReadAllText(file);
                    currentUser = JsonConvert.DeserializeObject<UserInfo>(json);
                }

                if (currentUser == null)
                {
                    currentUser = new UserInfo();
                    currentUser.uname = user;
                    currentUser.name = user;
                    if (user == RegistryAccess.DefaultUsername)
                        DefaultDatabase.DefaultUser = currentUser;
                }

                currentUser.Process();

                SetSubmissionToListView();
                userNameTitle.Text = string.Format(userNameTitle.Tag.ToString(), currentUser.name);

                //download latest data
                DownloadUserSubs(user);
            }
            catch (Exception ex)
            {
                SetStatus("Error : " + ex.Message);
                Logger.Add(ex.Message, "User Statistics");
            }
        }

        private void SetSubmissionToListView()
        {
            if (currentUser == null) return;
            List<UserSubmission> list = new List<UserSubmission>();
            switch (ViewOption)
            {
                case -1:
                    list = currentUser.submissions;
                    break;
                case -10:
                    foreach(UserSubmission usub in currentUser.submissions)
                    {
                        if(currentUser.ACList.Contains(usub.pnum)) 
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
                    for (int i = currentUser.submissions.Count - 1; cnt > 0; --i, --cnt)
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

        public void DownloadUserSubs(string user)
        {
            if (currentUser == null || currentUser.uname != user) return;

            Updating = true;
            string format = "http://uhunt.felix-halim.net/api/subs-user/{0}/{1}";
            string url = string.Format(format, currentUser.uid, currentUser.LastSID);
            Internet.Downloader.DownloadStringAsync(url, user, Priority.Normal, dt_progress, dt_completed);
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

                SetSubmissionToListView();
                userNameTitle.Text = string.Format(userNameTitle.Tag.ToString(), currentUser.name);

                SetStatus(Task.Token.ToString() + "'s submissions downloaded");

                if (currentUser.LastSID == 0)
                    Logger.Add("Downloaded " + currentUser.uname + "'s submissions", "User Statistics");
            }
            catch (Exception ex)
            {
                SetStatus("Error : " + ex.Message);
                Logger.Add(ex.Message, "User Statistics");
            }
        }

        void dt_progress(DownloadTask Task)
        {
            if (this.IsDisposed) return;

            Progress1.Value = Task.ProgressPercentage;
            SetStatus(string.Format("Downloading {0}'s submissions... [{1} out of {2}]",
                Task.Token, Functions.FormatMemory(Task.Received), Functions.FormatMemory(Task.DataSize)), true);
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
            if (this.tabControl1.SelectedTab != tabPage1) return;

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
                if (DefaultDatabase.IsProbSolved(js.pnum))
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
            autoUpdateToolMenu.Checked = !autoUpdateToolMenu.Checked;
            AutoUpdateStatus = autoUpdateToolMenu.Checked;
            SelectUpdateRateMenu();
        }

        #endregion

    }
}

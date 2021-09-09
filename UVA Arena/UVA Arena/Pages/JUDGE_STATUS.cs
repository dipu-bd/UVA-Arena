using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena.Elements
{
    public partial class STATUS : UserControl
    {
        private WebClient webClient1 = new WebClient();

        public STATUS()
        {
            InitializeComponent();
            webClient1.DownloadDataCompleted += webClient1_DownloadDataCompleted;
            webClient1.DownloadProgressChanged += webClient1_DownloadProgressChanged;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AssignAspectFunctions();
            SelectUpdateRateMenu();

            //start refresh task
            if (AutoUpdateStatus) JudgeStatusRefresh();

            Stylish.SetGradientBackground(panel1,
                new Stylish.GradientStyle(Color.LightBlue, Color.PaleTurquoise, 90F));
        }

        //
        // Registry Access
        //
        public static int UpdateInterval
        {
            get
            {
                return Properties.Settings.Default.JudgeStatUpdateInterval;
            }
            set
            {
                Properties.Settings.Default.JudgeStatUpdateInterval = value;
            }
        }

        public bool AutoUpdateStatus
        {
            get
            {
                return Properties.Settings.Default.AutoUpdateJudgeStatus;
            }
            set
            {
                Properties.Settings.Default.AutoUpdateJudgeStatus = value;
                if (value && !_taskRunning) JudgeStatusRefresh();
            }
        }

        //
        // Public functions
        //  
        private long LastSubID = 0;
        private bool _taskRunning = false;
        private DateTime LastUpdate = DateTime.Now;
        public List<JudgeStatus> StatusList = new List<JudgeStatus>();
        public Dictionary<long, JudgeStatus> SIDtoStatus = new Dictionary<long, JudgeStatus>();

        public void SetStatus(JudgeStatus status)
        {
            if (SIDtoStatus.ContainsKey(status.sid))
            {
                if (status.id > SIDtoStatus[status.sid].id)
                {
                    StatusList.Remove(SIDtoStatus[status.sid]);
                    SIDtoStatus[status.sid] = status;
                    StatusList.Add(status);
                }
            }
            else
            {
                StatusList.Add(status);
                SIDtoStatus.Add(status.sid, status);
            }
        }

        public void ClearSome()
        {
            int remcount = StatusList.Count - 100;
            if (remcount < 50) return;
            for (int i = 0; i < remcount; ++i)
            {
                long sid = StatusList[i].sid;
                SIDtoStatus.Remove(sid);
            }
            StatusList.RemoveRange(0, remcount);
        }

        public void UpdateSubmissions()
        {
            if (webClient1.IsBusy) return;

            ClearSome();
            Interactivity.SetStatus("Judge status update started...");
            string url = string.Format(Config.uHuntBaseUrl + "/api/poll/{0}.", LastSubID);
            webClient1.DownloadDataAsync(new Uri(url));
        }

        void webClient1_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Interactivity.SetStatus("Judge status updating...");
            Interactivity.SetProgress(e.ProgressPercentage);
        }

        void webClient1_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                string result = System.Text.Encoding.UTF8.GetString(e.Result);

                //update or add last submission id
                JudgeStatus[] statuslist = JsonConvert.DeserializeObject<JudgeStatus[]>(result);
                foreach (JudgeStatus status in statuslist)
                {
                    SetStatus(status);
                    if (status.id > LastSubID) LastSubID = status.id;
                }

                Interactivity.SetProgress();
                Interactivity.SetStatus("Judge status update finished.");

                //set submission status
                submissionStatus.SetObjects(StatusList, false);
                submissionStatus.Sort(sidSUB, SortOrder.Descending);
                submissionStatus.EnsureVisible(0);
            }
            catch (Exception ex)
            {
                Interactivity.SetStatus("Judge status update failed.");
                Logger.Add(ex.Message, "Judge Status | webClient1_DownloadDataCompleted");
            }
            finally
            {
                LastUpdate = DateTime.Now;
            }
        }

        //
        // Timer
        // 


        private void JudgeStatusRefresh()
        {
            _taskRunning = false;
            if (this.IsDisposed || !AutoUpdateStatus) return;
            TaskQueue.AddTask(JudgeStatusRefresh, 800);
            _taskRunning = true;

            //check if this is focused
            if (Interactivity.mainForm.customTabControl1.SelectedTab
                    != Interactivity.mainForm.statusTab) return;

            //refresh items
            submissionStatus.SetObjects(StatusList);
            submissionStatus.SelectedObject = null;

            //check if update needed
            if (!AutoUpdateStatus) return;

            //update
            TimeSpan span = DateTime.Now.Subtract(LastUpdate);
            long diff = (long)span.TotalMilliseconds;

            if (diff >= UpdateInterval)
            {
                if (webClient1.IsBusy)
                {
                    if (diff > 25000)
                        webClient1.CancelAsync();
                }
                else
                {
                    UpdateSubmissions();
                }
            }
            else
            {
                //show status about when to update 
                long inv = (long)Math.Ceiling((UpdateInterval - diff) / 1000.0);
                string msg = Functions.FormatTimeSpan(inv);
                Interactivity.SetStatus("Updating judge status in " + msg);
            }
        }

        //
        //Private functions
        //
        private void SelectUpdateRateMenu()
        {
            string tag = UpdateInterval.ToString();
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

        //
        // Event Functions
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

        private void refreshSubmission_Click(object sender, EventArgs e)
        {
            UpdateSubmissions();
        }

        //
        //Submission Status Listview
        //
        private void AssignAspectFunctions()
        {
            subtimeSUB.AspectGetter = delegate (object row)
            {
                JudgeStatus last = (JudgeStatus)row;
                return UnixTimestamp.FormatUnixTime(last.sbt);
            };
            lanSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.GetLanguage((Language)dat);
            };
            verSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.GetVerdict((Verdict)dat);
            };
            runSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.FormatRuntime((long)dat);
            };
            memSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.FormatMemory((long)dat);
            };
            rankSUB.AspectToStringConverter = delegate (object dat)
            {
                if ((long)dat == -1) return "-";
                return ((long)dat).ToString();
            };
        }

        private void submissionStatus_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;

            //highlight item 
            JudgeStatus js = (JudgeStatus)e.Model;
            if (js.uname == RegistryAccess.DefaultUsername)
            {
                for (int i = 0; i < e.Item.SubItems.Count; ++i)
                    e.Item.SubItems[i].BackColor = Color.Turquoise;
            }
            else if (LocalDatabase.ContainsUser(js.uname))
            {
                for (int i = 0; i < e.Item.SubItems.Count; ++i)
                    e.Item.SubItems[i].BackColor = Color.LightBlue;
            }

            //highlight other
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
            else if (e.Column == memSUB)
            {
                font = "Consolas";
                fore = Color.Blue;
                size = 8.5F;
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
                fore = Functions.GetVerdictColor(js.ver);
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
            JudgeStatus list = (JudgeStatus)e.Model;
            if (e.Column == pnumSUB || e.Column == ptitleSUB)
            {
                Interactivity.ShowProblem(list.pnum);
            }
            else if (e.Column == unameSUB)
            {
                if (LocalDatabase.ContainsUser(list.uname))
                {
                    Interactivity.ShowUserStat(list.uname);
                }
                else
                {
                    if (MessageBox.Show("Add \"" + list.uname + "\" to your favorite list?", "Add User",
                        MessageBoxButtons.YesNo) == DialogResult.No) return;
                    RegistryAccess.AddUserid(list.uname, list.uid.ToString());
                    Interactivity.ShowUserStat(list.uname);
                }
            }
        }
    }
}

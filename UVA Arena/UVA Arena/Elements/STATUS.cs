using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;
using Newtonsoft.Json;

namespace UVA_Arena.Elements
{
    public partial class STATUS : UserControl
    {
        public STATUS()
        {
            InitializeComponent();
            CustomStatusButton.Initialize(refreshToolButton);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            AssignAspectFunctions();
            SelectUpdateRateMenu();
            timer1.Enabled = AutoUpdateStatus;
            autoUpdateToolMenu.Checked = AutoUpdateStatus;
        }

        //
        // Registry Access
        //
        public static int UpdateInterval
        {
            get
            {
                object val = RegistryAccess.GetValue("Status Update Interval");
                if (val == null || val.GetType() != typeof(int)) return 5000;
                return (int)val;
            }
            set
            {
                RegistryAccess.SetValue("Status Update Interval", value,
                    null, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        public bool AutoUpdateStatus
        {
            get
            {
                object val = RegistryAccess.GetValue("Auto Update Status");
                if (val == null || val.GetType() != typeof(int)) return true;
                return ((int)val == 1);
            }
            set
            {
                RegistryAccess.SetValue("Auto Update Status", (value ? 1 : 0),
                    null, Microsoft.Win32.RegistryValueKind.DWord);
                timer1.Enabled = value;
            }
        }

        //
        // Public functions
        //  
        private long LastSubID = 0;
        private bool Updating = false;
        private DateTime LastUpdate = DateTime.Now;
        public List<JudgeStatus> StatusList = new List<JudgeStatus>();
        public Dictionary<long, JudgeStatus> IDtoStatus = new Dictionary<long, JudgeStatus>();

        public bool Contains(long sid)
        {
            return IDtoStatus.ContainsKey(sid);
        }
        public JudgeStatus GetStatus(long sid)
        {
            if (!Contains(sid)) return null;
            return IDtoStatus[sid];
        }
        public void SetStatus(JudgeStatus status)
        {
            //if not contains add it
            if (!Contains(status.sid))
            {
                StatusList.Add(status);
                IDtoStatus.Add(status.sid, status);
                return;
            }

            //otherwise update it
            JudgeStatus prev = IDtoStatus[status.sid];
            prev.id = status.id;
            prev.mem = status.mem;
            prev.run = status.run;
            prev.ver = status.ver;
            prev.rank = status.rank;
        }
        public void ClearSome()
        {
            while (StatusList.Count > 1000)
            {
                long id = StatusList[0].id;
                StatusList.RemoveAt(0);
                IDtoStatus.Remove(id);
            }
        }

        public void UpdateSubmissions()
        {
            if (Updating) return;
            ClearSome();
            Updating = true;
            Status1.Text = "Update started...";
            string url = string.Format("http://uhunt.felix-halim.net/api/poll/{0}.", LastSubID);
            Internet.Downloader.DownloadStringAsync(url,
                null, Internet.Priority.Low, null, DownloadComplete);
        }

        public void DownloadComplete(Internet.DownloadTask task)
        {
            try
            {
                if (!string.IsNullOrEmpty(task.Result))
                {
                    JudgeStatus[] statuslist = JsonConvert.DeserializeObject<JudgeStatus[]>(task.Result);
                    foreach (JudgeStatus status in statuslist)
                    {
                        //update or add last submission id
                        SetStatus(status);
                        if (status.id > LastSubID) LastSubID = status.id;
                        //reload submisstion status
                        submissionStatus.SetObjects(StatusList, true);
                        //sort submission status
                        submissionStatus.Sort(sidSUB, SortOrder.Descending);
                        //make first item visible
                        if (submissionStatus.SelectedItem == null)
                            submissionStatus.EnsureVisible(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Judge Status");
            }
            finally
            {
                Status1.Text = "Update finished.";
                LastUpdate = DateTime.Now;
                Updating = false;
            }
        }

        //
        // Timer
        // 
        private void timer1_Tick(object sender, EventArgs e)
        {
            //check if this is focused
            if (Interactivity.mainForm.customTabControl1.SelectedTab
                    != Interactivity.mainForm.submissionTab) return;

            //refresh items
            submissionStatus.Refresh();

            //check if update needed
            if (Updating || !AutoUpdateStatus) return;

            //update
            TimeSpan span = DateTime.Now.Subtract(LastUpdate);
            long diff = (long)span.TotalMilliseconds;
            if (diff >= UpdateInterval)
            {
                UpdateSubmissions();
            }
            else
            {
                //show status about when to update 
                long inv = (long)Math.Ceiling((UpdateInterval - diff) / 1000.0);
                string msg = Functions.FormatTimeSpan(inv);
                Status1.Text = "Updating in " + msg;
            }
        }

        //
        //Private functions
        //
        private void SelectUpdateRateMenu()
        {
            bool enable = AutoUpdateStatus;
            string tag = UpdateInterval.ToString();
            foreach (ToolStripItem item in updateToolMenu.DropDownItems)
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
            autoUpdateToolMenu.Checked = !autoUpdateToolMenu.Checked;
            AutoUpdateStatus = autoUpdateToolMenu.Checked;
            SelectUpdateRateMenu();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            UpdateSubmissions();
        }

        //
        //Submission Status Listview
        //
        private void AssignAspectFunctions()
        {
            subtimeSUB.AspectGetter = delegate(object row)
            {
                JudgeStatus last = (JudgeStatus)row;
                return UnixTimestamp.FormatUnixTime(last.sbt);
            };
            lanSUB.AspectToStringConverter = delegate(object dat)
            {
                return Functions.GetLanguage((Language)dat);
            };
            verSUB.AspectToStringConverter = delegate(object dat)
            {
                return Functions.GetVerdict((Verdict)dat);
            };
            runSUB.AspectToStringConverter = delegate(object dat)
            {
                return Functions.FormatRuntime((long)dat);
            };
            memSUB.AspectToStringConverter = delegate(object dat)
            {
                return Functions.FormatMemory((long)dat);
            };
            rankSUB.AspectToStringConverter = delegate(object dat)
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
            else if (LocalDatabase.ContainsUsers(js.uname))
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
                if (LocalDatabase.IsProbSolved(js.pnum))
                    fore = Color.Blue;
                else
                    fore = Color.Black;
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
                if (LocalDatabase.ContainsUsers(list.uname))
                {
                    Interactivity.ShowUserStat(list.uname);
                }
                else
                {
                    if (MessageBox.Show("Add \"" + list.uname + "\" to your favorite list?", "Add User",
                        MessageBoxButtons.YesNo) == DialogResult.No) return;
                    RegistryAccess.SetUserid(list.uname, list.uid.ToString());
                    Interactivity.ShowUserStat(list.uname);
                }
            }
        }
    }
}

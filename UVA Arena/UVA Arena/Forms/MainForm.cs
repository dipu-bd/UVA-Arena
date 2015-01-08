using System;
using System.Drawing;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class MainForm : Form, IMessageFilter
    {
        //private ActiveButton log = new ActiveButton();
        //private ActiveButton settings = new ActiveButton();
        //private ActiveButton help = new ActiveButton();

        public MainForm()
        {
            InitializeComponent();

            //to enable lower level mouse
            Application.AddMessageFilter(this);

            //make background transparent
            //bool set = NativeMethods.ExtendWindowsFrame(this, 3, 2, 58, 2);   //true if works

            //load images
            tabImageList.Images.Add("code", Properties.Resources.code);
            tabImageList.Images.Add("live_submission", Properties.Resources.live_submission);
            tabImageList.Images.Add("problems", Properties.Resources.problem);
            tabImageList.Images.Add("profile", Properties.Resources.profile);
            tabImageList.Images.Add("utilities", Properties.Resources.utility);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Logger.Add("UVA Arena started", "Main Form");

            //set styles
            customTabControl1.BackColor = Color.PaleTurquoise;
            Stylish.SetGradientBackground(menuStrip1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightSteelBlue, 90F));
            Stylish.SetGradientBackground(tableLayoutPanel1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightSteelBlue, 90F));

            //start status cleaner
            ClearStatus("");

            //set some properties to the form
            SetFormProperties();

            //initialize controls and add them
            DelayInitialize(true);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!Properties.Settings.Default.ShowExitDialogue)
            {
                ClosingDialogueForm cdf = new ClosingDialogueForm();
                if (cdf.ShowDialog() == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public void ClearStatus(object last)
        {
            if (Status1.Text.CompareTo(last) == 0)
            {
                Interactivity.SetStatus();
                Interactivity.SetProgress();
            }
            TaskQueue.AddTask(ClearStatus, Status1.Text, 3000);
        }

        #region mouse wheel without focus

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL)
            {
                //find the control at screen position m.LParam
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                IntPtr hWnd = NativeMethods.WindowFromPoint(pos);
                if (hWnd != IntPtr.Zero && hWnd != m.HWnd &&
                    System.Windows.Forms.Control.FromHandle(hWnd) != null)
                {
                    NativeMethods.SendMessage(hWnd, (uint)m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        #endregion mouse wheel without focus

        #region Delay Initializers

        public void SetFormProperties()
        {
            string user = RegistryAccess.DefaultUsername;
            if (LocalDatabase.ContainsUser(user))
            {
                this.Text = string.Format(this.Tag.ToString(), user, LocalDatabase.GetUserid(user));
            }
            else
            {
                string msg = "Looks like you didn't set a default user-name." + Environment.NewLine;
                msg += "It is extremely important to set a default user-name to enable many features." + Environment.NewLine;
                msg += "Press OK to set it now. Or, you can set it later from the menu bar options.";
                if (MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OKCancel)
                    == System.Windows.Forms.DialogResult.OK)
                {
                    Interactivity.ShowUserNameForm();
                }
            }
        }

        private void DelayInitialize(object background)
        {
            //run in background
            if ((bool)background)
            {
                this.Cursor = Cursors.AppStarting;
                System.Threading.ThreadPool.QueueUserWorkItem(DelayInitialize, false);
                return;
            }

            //load problem database
            LocalDatabase.RunLoadAsync(false);

            //load controls
            bool _initialized = false;
            this.BeginInvoke((MethodInvoker)delegate
            {
                //add controls
                AddControls();

                //add buttons to the top right beside control buttons
                //AddActiveButtons();

                _initialized = true;
                this.Cursor = Cursors.Default;
                Logger.Add("Initialized all controls", "Main Form");
            });

            //update problem database if not available
            if (LocalDirectory.GetFileSize(LocalDirectory.GetProblemInfoFile()) < 100)
            {
                while (!_initialized) System.Threading.Thread.Sleep(1000);
                System.Threading.Thread.Sleep(2000);
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UVA_Arena.Internet.Downloader.DownloadProblemDatabase();
                });
            }

            //update user submissions if not available
            if (LocalDatabase.ContainsUser(RegistryAccess.DefaultUsername))
            {
                string file = LocalDirectory.GetUserSubPath(RegistryAccess.DefaultUsername);
                if (LocalDirectory.GetFileSize(file) < 50)
                {
                    System.Threading.Thread.Sleep(1000);
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        Interactivity.userstat.DownloadUserSubs(RegistryAccess.DefaultUsername);
                    });
                }
            }

            //check for updates
            System.Threading.Thread.Sleep(10000);
            UpdateCheck.CheckForUpdate();
        }

        /*
        private void AddActiveButtons()
        {
            IActiveMenu menu = ActiveMenu.GetInstance(this);

            //settings
            settings.Image = Resources.tools;
            settings.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(settings, "Settings");
            settings.Click += delegate(object sender, EventArgs e) { Interactivity.ShowSettings(); };

            //log
            log.Image = Resources.log;
            log.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(log, "View Logs");
            log.Click += delegate(object sender, EventArgs e) { Interactivity.ShowLogger(); };

            //help
            help.Image = Resources.help;
            help.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(help, "Help");
            help.Click += delegate(object sender, EventArgs e) { Interactivity.ShowHelpAbout(); };

            menu.Items.Add(settings);
            menu.Items.Add(log);
            menu.Items.Add(help);
        }
        */

        private void AddControls()
        {
            customTabControl1.SuspendLayout();

            //load problems
            Interactivity.problems = new Elements.PROBLEMS();
            Interactivity.problems.Dock = DockStyle.Fill;
            problemTab.Controls.Add(Interactivity.problems);

            //load codes
            Interactivity.codes = new Elements.CODES();
            Interactivity.codes.Dock = DockStyle.Fill;
            codesTab.Controls.Add(Interactivity.codes);

            //load status
            Interactivity.status = new Elements.STATUS();
            Interactivity.status.Dock = DockStyle.Fill;
            judgeStatusTab.Controls.Add(Interactivity.status);

            //load user stat
            Interactivity.userstat = new Elements.USER_STAT();
            Interactivity.userstat.Dock = DockStyle.Fill;
            profileTab.Controls.Add(Interactivity.userstat);

            //load utilities
            //Interactivity.utilities = new Elements.UTILITIES();
            //Interactivity.utilities.Dock = DockStyle.Fill;
            //utilitiesTab.Controls.Add(Interactivity.utilities);

            customTabControl1.ResumeLayout(false);

            //set up context menu
            statusToolStripMenuItem.DropDown = Interactivity.status.updateContextMenu;
            submissionsToolStripMenuItem.DropDown = Interactivity.userstat.MainContextMenu;
        }

        #endregion Delay Initializers

        #region Less significant functions

        private void customTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customTabControl1.SelectedTab == profileTab)
            {
                Interactivity.userstat.LoadUsernames();
            } 
            else if(customTabControl1.SelectedTab == codesTab)
            {
                Interactivity.codesBrowser.CheckCodesPath();
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            Interactivity.ShowHelpAbout();
        }

        private void loggerButton_Click(object sender, EventArgs e)
        {
            Interactivity.ShowLogger();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings();
        }

        #region File Menu

        private void setDefaultUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowUserNameForm();
        }

        private void submitFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.SubmitCode(0);
        }

        private void uDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.codes.tabControl1.SelectedTab = Interactivity.codes.uDebugTab;
            customTabControl1.SelectedTab = codesTab;
        }

        private void discussForumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.tabControl1.SelectedTab = Interactivity.problemViewer.discussTab;
            customTabControl1.SelectedTab = problemTab;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings();
        }

        private void loggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowLogger();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion File Menu

        #region problems

        private void refreshDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDatabase.LoadDatabase();
        }

        private void downloadDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UVA_Arena.Internet.Downloader.DownloadProblemDatabase();
        }

        private void descriptionDownloaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = problemTab;
            Interactivity.ShowDownloadAllForm();
        }

        private void descriptionFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(LocalDirectory.GetProblemDescritionPath());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.BackupData();
        }

        private void restoreDatabseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.RestoreData();
        }

        private void expandViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = problemTab;
            Interactivity.problems.ExpandCollapseView();
        }

        private void viewCodeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.codeButton.PerformClick();
        }

        #endregion problems

        #region codes

        private void changeDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = codesTab;
            Interactivity.codesBrowser.ChangeCodeDirectory();
        }

        private void formatDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = codesTab;
            Interactivity.codesBrowser.FormatCodeDirectory(true);
        }

        private void refreshViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = codesTab;
            Interactivity.codesBrowser.LoadCodeFolder(true);
        }

        private void editorSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings(1);
        }

        private void setUpCompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings(2);
        }

        private void changePrecodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings(3);
        }

        private void submitCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.codes.submitToolButton.PerformClick();
        }

        #endregion codes

        #region user status

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.userstat.usernameBox.Focus();
            customTabControl1.SelectedTab = profileTab;
        }

        private void worldRankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.worldrankTab;
            customTabControl1.SelectedTab = profileTab;
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.compareTab;
            customTabControl1.SelectedTab = profileTab;
        }

        private void basicInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.progTracker.tabControl1.SelectedTab = Interactivity.progTracker.basicInfoTab;
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.progtrackerTab;
            customTabControl1.SelectedTab = profileTab;
        }

        private void submissionOverTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.progTracker.tabControl1.SelectedTab = Interactivity.progTracker.subPerDateTab;
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.progtrackerTab;
            customTabControl1.SelectedTab = profileTab;
        }

        private void submissionPerVerdictrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.progTracker.tabControl1.SelectedTab = Interactivity.progTracker.subPerVerTab;
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.progtrackerTab;
            customTabControl1.SelectedTab = profileTab;
        }

        private void submissionLanguagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.progTracker.tabControl1.SelectedTab = Interactivity.progTracker.subPerLanTab;
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.progtrackerTab;
            customTabControl1.SelectedTab = profileTab;
        }

        private void rankCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.progTracker.tabControl1.SelectedTab = Interactivity.progTracker.rankCloudTab;
            Interactivity.userstat.tabControl1.SelectedTab = Interactivity.userstat.progtrackerTab;
            customTabControl1.SelectedTab = profileTab;
        }

        #endregion user status

        #region help menu

        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://github.com/dipu-bd/UVA-Arena/wiki";
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "MainForm|onlineHelpToolStripMenuItem_Click");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowHelpAbout();
        }

        private void licenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowHelpAbout();
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCheck.CheckForUpdate();
        }

        private void checkForUpdateToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (UpdateCheck.IsChecking)
                checkForUpdateToolStripMenuItem.Text = "Checking for update...";
            else
                checkForUpdateToolStripMenuItem.Text = "Check for update";
        }

        #endregion help menu

        #endregion Less significant functions
    }
}
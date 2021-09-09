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

            productName.Text = Application.ProductName;
            versionLabel.Text = "Version " + Application.ProductVersion;

            //load images
            tabImageList.Images.Add("code", Properties.Resources.code);
            tabImageList.Images.Add("live_submission", Properties.Resources.live_submission);
            tabImageList.Images.Add("problems", Properties.Resources.problem);
            tabImageList.Images.Add("profile", Properties.Resources.profile);
            tabImageList.Images.Add("utilities", Properties.Resources.utility);
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

        #region Load form

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
            Stylish.SetGradientBackground(loadingPanel,
              new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightSteelBlue, 90F));

            //start status cleaner
            ClearStatus("");

            //other operations
            DelayOperations(true);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!Properties.Settings.Default.HideExitDialog)
            {
                ClosingDialogueForm cdf = new ClosingDialogueForm();
                if (cdf.ShowDialog() == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
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

        public void ClearStatus(object last)
        {
            if (Status1.Text.CompareTo(last) == 0)
            {
                Interactivity.SetStatus();
                Interactivity.SetProgress();
            }
            TaskQueue.AddTask(ClearStatus, Status1.Text, 3000);
        }

        private void DelayOperations(object background)
        {
            if ((bool)background)
            {
                customTabControl1.Visible = false;
                System.Threading.ThreadPool.QueueUserWorkItem(DelayOperations, false);
                return;
            }

            System.Threading.Thread.Sleep(200);
            this.BeginInvoke((MethodInvoker)delegate
            {
                //add controls
                AddControls();
                customTabControl1.Visible = true;

                this.Cursor = Cursors.Default;
                Logger.Add("Initialized all controls", "Main Form");

                loadingPanel.Visible = false;

                //set some properties to the form
                SetFormProperties();
            });

            //initialize controls and add them
            if (LocalDatabase.UpdateAll())
            {
                LocalDatabase.LoadDatabase();
            }

            //check for update
            if (Properties.Settings.Default.CheckForUpdate)
            {
                System.Threading.Thread.Sleep(5000);
                UpdateCheck.CheckForUpdate();
            }
        }

        private void AddControls()
        {
            customTabControl1.SuspendLayout();

            //load problems
            Interactivity.problems = new Elements.PROBLEMS();
            Interactivity.problems.Dock = DockStyle.Fill;
            problemTab.ImageIndex = 0;
            problemTab.Controls.Add(Interactivity.problems);

            //load codes
            Interactivity.codes = new Elements.CODES();
            Interactivity.codes.Dock = DockStyle.Fill;
            codesTab.ImageIndex = 1;
            codesTab.Controls.Add(Interactivity.codes);

            //load status
            Interactivity.status = new Elements.STATUS();
            Interactivity.status.Dock = DockStyle.Fill;
            statusTab.ImageIndex = 2;
            statusTab.Controls.Add(Interactivity.status);

            //load user stat
            Interactivity.userstat = new Elements.USER_STAT();
            Interactivity.userstat.Dock = DockStyle.Fill;
            profileTab.ImageIndex = 3;
            profileTab.Controls.Add(Interactivity.userstat);

            customTabControl1.ResumeLayout(false);

            //set up context menu
            statusToolStripMenuItem.DropDown = Interactivity.status.updateContextMenu;
            submissionsToolStripMenuItem.DropDown = Interactivity.userstat.MainContextMenu;
        }

        #endregion

        #region Less significant functions

        private void customTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //problemsToolStripMenuItem.Enabled = (customTabControl1.SelectedTab == problemTab);
            //codesToolStripMenuItem.Enabled = (customTabControl1.SelectedTab == codesTab);
            //statusToolStripMenuItem.Enabled = (customTabControl1.SelectedTab == judgeStatusTab);
            //profilesToolStripMenuItem.Enabled = (customTabControl1.SelectedTab == profileTab);
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            customTabControl1.Focus();
            Interactivity.ShowHelpAbout();
        }

        private void loggerButton_Click(object sender, EventArgs e)
        {
            customTabControl1.Focus();
            Interactivity.ShowLogger();
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            customTabControl1.Focus();
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
            Interactivity.problems.CollapsePanel1View();
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

        private void likeOnFacebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://www.facebook.com/uvaarena";
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "MainForm|onlineHelpToolStripMenuItem_Click");
            }
        }

        private void fAQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://github.com/dipu-bd/UVA-Arena/wiki/FAQ";
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "MainForm|fAQToolStripMenuItem_Click");
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

        #region Top Level
        private void problemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //customTabControl1.SelectedTab = problemTab;
        }
        private void codesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //customTabControl1.SelectedTab = codesTab;
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //customTabControl1.SelectedTab = judgeStatusTab;
        }

        private void profilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //customTabControl1.SelectedTab = profileTab;
        }

        #endregion

        #endregion Less significant functions

    }
}
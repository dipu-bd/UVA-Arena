using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TheCodeKing.ActiveButtons.Controls;
using UVA_Arena.Properties;

namespace UVA_Arena
{
    public partial class MainForm : Form, IMessageFilter
    {
        private ActiveButton log = new ActiveButton();
        private ActiveButton settings = new ActiveButton();
        private ActiveButton help = new ActiveButton();

        public MainForm()
        {
            InitializeComponent();

            //to enable lower level mouse
            Application.AddMessageFilter(this); 

            //make background trasparent
            // bool set = NativeMethods.ExtendWindowsFrame(this, 3, 2, 58, 2);   //true if works
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Logger.Add("UVA Arena started", "Main Form");

            customTabControl1.BackColor = Color.PaleTurquoise;
            Stylish.SetGradientBackground(menuStrip1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightSteelBlue, 90F));
            
            //set some properties to the form
            SetFormProperties();
                       
            //initialize controls and add them
            DelayInitialize(true);
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

        #endregion

        public void SetFormProperties()
        {
            this.Text = string.Format(this.Tag.ToString(),
                    RegistryAccess.DefaultUsername,
                    LocalDatabase.GetUserid(RegistryAccess.DefaultUsername));
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
            this.BeginInvoke((MethodInvoker)delegate
            {
                //add controls
                AddControls();
               
                //add buttons to the top right beside control buttons
                AddActiveButtons();

                this.Cursor = Cursors.Default;
                Logger.Add("Initialized all controls", "Main Form");
            });

            //fetch problem database from internet if not available            
            System.Threading.Thread.Sleep(3000);
            if (LocalDirectory.GetFileSize(LocalDirectory.GetProblemDataFile()) < 100)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UVA_Arena.Internet.Downloader.DownloadProblemDatabase();
                });
            }
        }

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

        private void AddControls()
        {
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
            
            //set up context menu
            statusToolStripMenuItem.DropDown = Interactivity.status.updateContextMenu;
            submissionsToolStripMenuItem.DropDown = Interactivity.userstat.MainContextMenu;
        }

        #region Menubar Actions


        //File Menu
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

        //problems
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
                System.Diagnostics.Process.Start(LocalDirectory.GetProblemPath());
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

        //codes
        private void changeDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = codesTab;
            Interactivity.codes.ChangeCodeDirectory();
        }

        private void formatDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = codesTab;
            Interactivity.codes.FormatCodeDirectory(true);
        }

        private void refreshViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customTabControl1.SelectedTab = codesTab;
            Interactivity.codes.LoadCodeFolder(true);
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

        //user status
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

        // help menu
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

        #endregion

    }
}

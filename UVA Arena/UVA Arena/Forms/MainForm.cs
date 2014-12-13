using System;
using System.Drawing;
using System.Windows.Forms;
using TheCodeKing.ActiveButtons.Controls;
using UVA_Arena.Properties;

namespace UVA_Arena
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //make background trasparent
            bool set = NativeMethods.ExtendWindowsFrame(this, 3, 2, 34, 2);
            //if it doesn't work set a color
            if (!set) customTabControl1.BackColor = Color.PowderBlue;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Logger.Add("UVA Arena started", "Main Form");

            //set some properties to the form
            SetFormProperties();

            //add buttons to the top right beside control buttons
            AddActiveButtons();

            //initialize controls and add them
            DelayInitialize(true);

            System.GC.Collect();
        }

        public void SetFormProperties()
        {
            this.Text = string.Format(this.Tag.ToString(),
                    RegistryAccess.DefaultUsername,
                    DefaultDatabase.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void DelayInitialize(object background)
        {
            //run in background
            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(DelayInitialize, false);
                return;
            }

            //first load problem database
            DefaultDatabase.RunLoadAsync(false);

            //load problems 
            this.BeginInvoke((MethodInvoker)delegate
            {
                Interactivity.problems = new Elements.PROBLEMS();
                Interactivity.problems.Dock = DockStyle.Fill;
                Interactivity.problems.BorderStyle = BorderStyle.FixedSingle;
                problemTab.Controls.Add(Interactivity.problems);
            });

            //load problem viewer
            System.Threading.Thread.Sleep(20);
            this.BeginInvoke((MethodInvoker)delegate
            {
                Interactivity.problemViewer = new Elements.ProblemViewer();
                Interactivity.problemViewer.Dock = DockStyle.Fill;
                Interactivity.problemViewer.BorderStyle = BorderStyle.None;
                Interactivity.problems.mainSplitContainer.Panel2.Controls.Add(Interactivity.problemViewer);
            });

            //load codes
            System.Threading.Thread.Sleep(100);
            this.BeginInvoke((MethodInvoker)delegate
            {
                Interactivity.codes = new Elements.CODES();
                Interactivity.codes.Dock = DockStyle.Fill;
                Interactivity.codes.BorderStyle = BorderStyle.FixedSingle;
                codesTab.Controls.Add(Interactivity.codes);
            });

            //load judge status
            System.Threading.Thread.Sleep(100);
            this.BeginInvoke((MethodInvoker)delegate
            {
                Interactivity.status = new Elements.STATUS();
                Interactivity.status.Dock = DockStyle.Fill;
                Interactivity.status.BorderStyle = BorderStyle.FixedSingle;
                submissionTab.Controls.Add(Interactivity.status);
            });

            //load user stat
            System.Threading.Thread.Sleep(100);
            this.BeginInvoke((MethodInvoker)delegate
            {
                Interactivity.userstat = new Elements.USER_STAT();
                Interactivity.userstat.Dock = DockStyle.Fill;
                profileTab.Controls.Add(Interactivity.userstat);
            });
            
            //load utilities
            System.Threading.Thread.Sleep(100);
            this.BeginInvoke((MethodInvoker)delegate
            {
                Interactivity.utilities = new Elements.UTILITIES();
                Interactivity.utilities.Dock = DockStyle.Fill;
                utilitiesTab.Controls.Add(Interactivity.utilities);
            });

            Logger.Add("Initialized all controls", "Main Form");

            //fetch prblem database from internet if not available
            System.Threading.Thread.Sleep(3000);
            if (!System.IO.File.Exists(LocalDirectory.ProblemDataFile))
            {
                Internet.DownloadTaskHandler complete =
                    delegate(Internet.DownloadTask task) { DefaultDatabase.LoadDatabase(); };
                Internet.Downloader.DownloadProblemDatabase(complete, null);
            }

            System.GC.Collect();
        }

        private void AddActiveButtons()
        {
            IActiveMenu menu = ActiveMenu.GetInstance(this);

            //settings
            ActiveButton settings = new ActiveButton();
            settings.Image = Resources.tools;
            settings.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(settings, "Settings");
            settings.Click += delegate(object sender, EventArgs e) { Interactivity.ShowSettings(); };

            //log
            ActiveButton log = new ActiveButton();
            log.Image = Resources.log;
            log.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(log, "View Logs");
            log.Click += delegate(object sender, EventArgs e) { Interactivity.ShowLogger(); };

            //help
            ActiveButton help = new ActiveButton();
            help.Image = Resources.help;
            help.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(help, "Help");
            help.Click += delegate(object sender, EventArgs e) { Interactivity.ShowHelpAbout(); };

            menu.Items.Add(settings);
            menu.Items.Add(log);
            menu.Items.Add(help);
        }
    }
}

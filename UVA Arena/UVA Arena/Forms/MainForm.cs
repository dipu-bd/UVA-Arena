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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Logger.Add("UVA Arena started", "Main Form");

            //set some properties to the form
            SetFormProperties();

            //add buttons to the top right beside control buttons
            AddActiveButtons();

            //initialize controls and add them
            DelayInitialize(true);
        }

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
                System.Threading.ThreadPool.QueueUserWorkItem(DelayInitialize, false);
                return;
            }

            //first load problem database
            LocalDatabase.RunLoadAsync(false);
             
            //load controls
            this.BeginInvoke((MethodInvoker)delegate
            {
                //load problems
                Interactivity.problems = new Elements.PROBLEMS();
                Interactivity.problems.Dock = DockStyle.Fill;
                Interactivity.problems.BorderStyle = BorderStyle.FixedSingle;
                
                //load problem viewer            
                Interactivity.problemViewer = new Elements.ProblemViewer();
                Interactivity.problemViewer.Dock = DockStyle.Fill;
                Interactivity.problemViewer.BorderStyle = BorderStyle.None;
                Interactivity.problems.mainSplitContainer.Panel2.Controls.Add(Interactivity.problemViewer);

                //load codes
                Interactivity.codes = new Elements.CODES();
                Interactivity.codes.Dock = DockStyle.Fill;
                Interactivity.codes.BorderStyle = BorderStyle.FixedSingle;
                
                //load status
                Interactivity.status = new Elements.STATUS();
                Interactivity.status.Dock = DockStyle.Fill;
                Interactivity.status.BorderStyle = BorderStyle.FixedSingle;

                //load user stat
                Interactivity.userstat = new Elements.USER_STAT();
                Interactivity.userstat.Dock = DockStyle.Fill;

                //load utilities
                Interactivity.utilities = new Elements.UTILITIES();
                Interactivity.utilities.Dock = DockStyle.Fill;

                //turn off visibility
                Interactivity.problems.Visible = false;
                Interactivity.status.Visible = false;
                Interactivity.codes.Visible = false;
                Interactivity.userstat.Visible = false;
                Interactivity.utilities.Visible = false;

                //add controls
                problemTab.Controls.Add(Interactivity.problems);
                codesTab.Controls.Add(Interactivity.codes);
                submissionTab.Controls.Add(Interactivity.status);
                profileTab.Controls.Add(Interactivity.userstat);
                utilitiesTab.Controls.Add(Interactivity.utilities);

                //turn on visibility      
                Interactivity.problems.Visible = true;
                Interactivity.codes.Visible = true;
                Interactivity.status.Visible = true;
                Interactivity.userstat.Visible = true;
                Interactivity.utilities.Visible = true;

                Logger.Add("Initialized all controls", "Main Form");
            });

            //fetch problem database from internet if not available            
            System.Threading.Thread.Sleep(3000);            
            if (LocalDirectory.GetFileSize(LocalDirectory.GetProblemDataFile()) < 100)
            {
                this.BeginInvoke((MethodInvoker)delegate
                    {
                        Internet.DownloadTaskHandler complete =
                            delegate(Internet.DownloadTask task) { LocalDatabase.LoadDatabase(); };
                        Internet.Downloader.DownloadProblemDatabase(complete, null);
                    });
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TheCodeKing.ActiveButtons.Controls;
using UVA_Arena.Properties;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class MainForm : Form
    {        
        public MainForm()
        {
            //first load main form
            InitializeComponent();
            AddActiveButtons();
            if (!NativeMethods.ExtendWindowsFrame(this, 3, 2, 36, 2))
                customTabControl1.BackColor = Color.PaleTurquoise; 
            
            //then check other
            StartupCheck();
            RefreshForm();
            InitializeOthers();

            Logger.Add("UVA Arena started", "Main Form");
            System.GC.Collect();
        }
        
        public void RefreshForm()
        {
            this.Text = string.Format(this.Tag.ToString(),
                    RegistryAccess.DefaultUsername,
                    RegistryAccess.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void InitializeOthers()
        {
            Interactivity.problems = new Elements.PROBLEMS();
            Interactivity.problems.Dock = DockStyle.Fill;
            problemTab.Controls.Add(Interactivity.problems);

            Interactivity.codes = new Elements.CODES();
            Interactivity.codes.Dock = DockStyle.Fill;
            codesTab.Controls.Add(Interactivity.codes);

            Logger.Add("Initialized all controls", "Main Form");            
        }


        private void StartupCheck()
        {
            //get default username
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername))
            {
                UsernameForm uf = new UsernameForm();
                uf.ShowDialog();
            }

            //download problem database
            if (System.IO.File.Exists(LocalDirectory.ProblemData))
            {
                ProblemDatabase.LoadDatabase();
            }
            else
            {
                Internet.DownloadCompleteHandler complete = delegate(Internet.DownloadTask task)
                {
                    ProblemDatabase.LoadDatabase();
                };
                Internet.Downloader.DownloadProblemDatabase(complete, null);
            }
        }

        private void AddActiveButtons()
        {
            IActiveMenu menu = ActiveMenu.GetInstance(this);

            //settings
            ActiveButton settings = new ActiveButton();
            settings.Image = Resources.tools;
            settings.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(settings, "Settings");
            settings.Click += delegate(object sender, EventArgs e)
            {
                Interactivity.ShowSettings();
            };

            //log
            ActiveButton log = new ActiveButton();
            log.Image = Resources.log;
            log.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(log, "View Logs");
            log.Click += delegate(object sender, EventArgs e)
            {
                Interactivity.ShowLogger();
            };

            //help
            ActiveButton help = new ActiveButton();
            help.Image = Resources.help;
            help.ImageAlign = ContentAlignment.MiddleCenter;
            menu.ToolTip.SetToolTip(help, "Help");
            help.Click += delegate(object sender, EventArgs e)
            {
                Interactivity.ShowHelpAbout();
            };


            menu.Items.Add(settings);
            menu.Items.Add(log);
            menu.Items.Add(help);
        }
    }
}

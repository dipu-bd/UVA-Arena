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
        #region Constructor

        public MainForm()
        {
            StartupCheck();

            InitializeComponent();

            AddActiveButtons();
            FormAPI.ExtendClientArea(this, new Padding(5, 40, 5, 5));

            RefreshForm();
            InitializeOthers();
        }

        public void RefreshForm()
        {
            this.Text = string.Format(this.Tag.ToString(),
                    RegistryAccess.DefaultUsername,
                    RegistryAccess.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void StartupCheck()
        {
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername))
            {
                UsernameForm uf = new UsernameForm();
                uf.ShowDialog();
            }
        }

        private void InitializeOthers()
        {
            ProblemDatabase.LoadDatabase();
            
            problems1.InitOthers();
        }

        #endregion

        #region Active Buttons

        private void AddActiveButtons()
        {
            IActiveMenu menu = ActiveMenu.GetInstance(this);

            //help
            ActiveButton help = new ActiveButton();
            help.BackgroundImage = Resources.help_icon;
            help.BackgroundImageLayout = ImageLayout.Center;
            help.Click += help_Click;

            //settings
            ActiveButton settings = new ActiveButton();
            settings.BackgroundImage = Resources.tools_icon;
            settings.Click += settings_Click;
            settings.BackgroundImageLayout = ImageLayout.Center;

            menu.Items.Add(help);
            menu.Items.Add(settings);
        }

        void settings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog(this);            
        }

        void help_Click(object sender, EventArgs e)
        {
            HelpAbout ha = new HelpAbout();
            ha.Show();
        }

        #endregion

    }
}

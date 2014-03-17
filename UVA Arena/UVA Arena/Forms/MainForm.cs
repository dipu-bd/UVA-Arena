using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text; 
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
            AddActiveButtons();            
        }

        #region Active Buttons

        private void AddActiveButtons()
        {
            IActiveMenu menu = ActiveMenu.GetInstance(this);
            
            //help
            ActiveButton help = new ActiveButton();
            help.BackgroundImage = Resources.ihelp;
            help.BackgroundImageLayout = ImageLayout.Center;
            help.Click += help_Click;

            //settings
            ActiveButton settings = new ActiveButton();
            settings.BackgroundImage = Resources.tools;
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

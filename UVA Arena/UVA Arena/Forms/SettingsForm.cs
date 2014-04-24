using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            current_username.Text = string.Format("Current: {0} ({1})",
                RegistryAccess.DefaultUsername,
                RegistryAccess.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void username_button1_Click(object sender, EventArgs e)
        {
            UsernameForm uf = new UsernameForm();
            if (uf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                current_username.Text = string.Format("Current: {0}({1})",
                      RegistryAccess.DefaultUsername,
                      RegistryAccess.GetUserid(RegistryAccess.DefaultUsername));
            }
        }
    }
}

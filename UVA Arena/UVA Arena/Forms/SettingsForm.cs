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

        public void InitializeSettings()
        {
            current_username.Text = string.Format("Current: {0} ({1})",
                RegistryAccess.DefaultUsername,
                RegistryAccess.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }         

        private void username_button1_Click(object sender, EventArgs e)
        {
            UsernameForm uf = new UsernameForm();
            uf.Show();
        }

        private void downloadAll_Click(object sender, EventArgs e)
        {
            Interactivity.ShowDownloadAllForm();
        }

        private void backupData_Click(object sender, EventArgs e)
        {
            Functions.BackupData(); 
        }

        private void restoreData_Click(object sender, EventArgs e)
        {
            Functions.RestoreData();
        }

    }
}

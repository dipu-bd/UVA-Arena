using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Data;
using System.Drawing;
using System.Web;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;
using UVA_Arena.Internet;

namespace UVA_Arena
{
    public partial class UsernameForm : Form
    {
        public UsernameForm()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername))
                username1.Text = RegistryAccess.DefaultUsername;
        }

        private void username1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) set_button1.PerformClick();
        }

        private void set_button1_Click(object sender, EventArgs e)
        {
            username1.Enabled = false;
            set_button1.Enabled = false;
            Status.Text = "Requesting server to get userid...";
            Downloader.DownloadUserid(username1.Text, DownloadCompleted);
        }

        private void cancel_button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void DownloadCompleted(Internet.DownloadTask task)
        {
            try
            {
                if (task.status != Internet.ProgressStatus.Completed) 
                    throw task.error;

                RegistryAccess.DefaultUsername = username1.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();

                Interactivity.DefaultUsernameChanged();
            }
            catch (Exception ex)
            {
                if (ex == null) ex = new Exception("Cancelled");
                Status.Text = string.Format("[Error: {0}]", ex.Message);
            }
            finally
            {
                username1.Enabled = true;
                set_button1.Enabled = true;
                username1.Focus();
            }
        }
    }
}

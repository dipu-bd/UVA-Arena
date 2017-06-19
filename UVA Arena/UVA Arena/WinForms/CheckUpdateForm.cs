using System;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class CheckUpdateForm : Form
    {
        public CheckUpdateForm()
        {
            InitializeComponent();
        }

        private void updateLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            updateButton.PerformClick();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(updateLink.Text);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //Application.Exit();
            }
            catch { }
        }

        private void checkUpdateButton_Click(object sender, EventArgs e)
        {
            UpdateCheck.CheckForUpdate();
        }

        private void checkUpdateButton_MouseHover(object sender, EventArgs e)
        {
            if (UpdateCheck.IsChecking)
            {
                if (checkUpdateButton.Enabled)
                {
                    checkUpdateButton.Enabled = false;
                    checkUpdateButton.Text = "Checking...";
                }
            }
            else
            {
                if (!checkUpdateButton.Enabled)
                {
                    checkUpdateButton.Enabled = true;
                    checkUpdateButton.Text = "Check";
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

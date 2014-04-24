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
            if (e.KeyCode == Keys.Enter)
                set_button1.PerformClick();
        }

        private void set_button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) return;
            string name = username1.Text;
            backgroundWorker1.RunWorkerAsync(name);            
        }

        private void cancel_button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = false;
            for (int i = 0; i < 3; ++i)
            {
                if (backgroundWorker1.CancellationPending) return;
                try
                {
                    backgroundWorker1.ReportProgress(0, "Requesting server to get userid...");
                    
                    string name = e.Argument.ToString();
                    string uid = RegistryAccess.GetUserid(name);
                    if (string.IsNullOrEmpty(uid))
                    {
                        uid = Internet.DownloadUserid(name);
                        if (string.IsNullOrEmpty(uid)) throw new Exception("No Data");
                        if (uid == "0")
                        {
                            e.Cancel = true;
                            backgroundWorker1.ReportProgress(1, "Username not found");
                            return;
                        }
                    }

                    RegistryAccess.SetUserid(name, uid);
                    RegistryAccess.DefaultUsername = name;
                    e.Result = true;
                    return;
                }
                catch (Exception ex)
                {
                    if (i == 3) return;
                    if (backgroundWorker1.CancellationPending) return;

                    backgroundWorker1.ReportProgress(1, ex.Message);
                    for (int t = 1; t <= 4; ++t)
                    {                        
                        System.Threading.Thread.Sleep(1000);
                        backgroundWorker1.ReportProgress(1, "Error occured. Retrying..." + t.ToString());
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Status.Text = e.UserState.ToString();
            }
            catch { }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled) return;
                if (true == (bool)e.Result)
                {
                    MainForm mf = (MainForm)Application.OpenForms["MainForm"];
                    mf.RefreshForm(); 

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Status.Text = "Check your internet connection and retry.";
                }
            }
            catch { }
        }
    }
}

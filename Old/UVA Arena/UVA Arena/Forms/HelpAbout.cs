using System;
using System.Drawing;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class HelpAbout : Form
    {
        public HelpAbout()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            productName.Text = Application.ProductName;
            versionLabel.Text = "Version " + Application.ProductVersion;
            licenceText.Text = Properties.Resources.ShortLicence;

            Stylish.SetGradientBackground(tabPage3,
                new Stylish.GradientStyle(Color.LightCyan, Color.PaleTurquoise, -90F));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                String url = (string)((LinkLabel)sender).Tag;
                System.Diagnostics.Process.Start(url);
                Clipboard.SetText(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start((string)((Control)sender).Tag);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

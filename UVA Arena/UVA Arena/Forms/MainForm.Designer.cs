namespace UVA_Arena
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.customTabControl1 = new System.Windows.Forms.CustomTabControl();
            this.problemTab = new System.Windows.Forms.TabPage();
            this.codesTab = new System.Windows.Forms.TabPage();
            this.submissionTab = new System.Windows.Forms.TabPage();
            this.profileTab = new System.Windows.Forms.TabPage();
            this.utilitiesTab = new System.Windows.Forms.TabPage();
            this.customTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabImageList
            // 
            this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
            this.tabImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.tabImageList.Images.SetKeyName(0, "live_submission");
            this.tabImageList.Images.SetKeyName(1, "problems");
            this.tabImageList.Images.SetKeyName(2, "profile");
            this.tabImageList.Images.SetKeyName(3, "code");
            this.tabImageList.Images.SetKeyName(4, "utility");
            // 
            // customTabControl1
            // 
            this.customTabControl1.Controls.Add(this.problemTab);
            this.customTabControl1.Controls.Add(this.codesTab);
            this.customTabControl1.Controls.Add(this.submissionTab);
            this.customTabControl1.Controls.Add(this.profileTab);
            this.customTabControl1.Controls.Add(this.utilitiesTab);
            this.customTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTabControl1.ImageList = this.tabImageList;
            this.customTabControl1.Location = new System.Drawing.Point(0, 0);
            this.customTabControl1.Name = "customTabControl1";
            this.customTabControl1.Overlap = 5;
            this.customTabControl1.Padding = new System.Drawing.Point(15, 5);
            this.customTabControl1.SelectedIndex = 0;
            this.customTabControl1.Size = new System.Drawing.Size(784, 411);
            this.customTabControl1.TabIndex = 0;
            // 
            // problemTab
            // 
            this.problemTab.ImageKey = "problems";
            this.problemTab.Location = new System.Drawing.Point(4, 29);
            this.problemTab.Name = "problemTab";
            this.problemTab.Padding = new System.Windows.Forms.Padding(3);
            this.problemTab.Size = new System.Drawing.Size(776, 378);
            this.problemTab.TabIndex = 1;
            this.problemTab.Text = "PROBLEMS";
            this.problemTab.UseVisualStyleBackColor = true;
            // 
            // codesTab
            // 
            this.codesTab.ImageKey = "code";
            this.codesTab.Location = new System.Drawing.Point(4, 29);
            this.codesTab.Name = "codesTab";
            this.codesTab.Padding = new System.Windows.Forms.Padding(3);
            this.codesTab.Size = new System.Drawing.Size(776, 378);
            this.codesTab.TabIndex = 3;
            this.codesTab.Text = "CODES";
            this.codesTab.UseVisualStyleBackColor = true;
            // 
            // submissionTab
            // 
            this.submissionTab.ImageKey = "live_submission";
            this.submissionTab.Location = new System.Drawing.Point(4, 29);
            this.submissionTab.Name = "submissionTab";
            this.submissionTab.Padding = new System.Windows.Forms.Padding(3);
            this.submissionTab.Size = new System.Drawing.Size(776, 378);
            this.submissionTab.TabIndex = 6;
            this.submissionTab.Text = "SUBMISSIONS";
            this.submissionTab.UseVisualStyleBackColor = true;
            // 
            // profileTab
            // 
            this.profileTab.ImageKey = "profile";
            this.profileTab.Location = new System.Drawing.Point(4, 29);
            this.profileTab.Name = "profileTab";
            this.profileTab.Padding = new System.Windows.Forms.Padding(3);
            this.profileTab.Size = new System.Drawing.Size(776, 378);
            this.profileTab.TabIndex = 2;
            this.profileTab.Text = "PROFILES";
            this.profileTab.UseVisualStyleBackColor = true;
            // 
            // utilitiesTab
            // 
            this.utilitiesTab.ImageKey = "utility";
            this.utilitiesTab.Location = new System.Drawing.Point(4, 29);
            this.utilitiesTab.Name = "utilitiesTab";
            this.utilitiesTab.Padding = new System.Windows.Forms.Padding(3);
            this.utilitiesTab.Size = new System.Drawing.Size(776, 378);
            this.utilitiesTab.TabIndex = 5;
            this.utilitiesTab.Text = "UTILITIES";
            this.utilitiesTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.customTabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Tag = "UVA Arena [{0} : {1}]";
            this.Text = "UVA Arena";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.customTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage problemTab;
        private System.Windows.Forms.TabPage profileTab;
        private System.Windows.Forms.TabPage codesTab;
        private System.Windows.Forms.TabPage utilitiesTab;
        private System.Windows.Forms.CustomTabControl customTabControl1;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.TabPage submissionTab;
          
    }
}


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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
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
            this.tabImageList.Images.SetKeyName(4, "submit");
            this.tabImageList.Images.SetKeyName(5, "utility");
            // 
            // customTabControl1
            //  
            this.customTabControl1.Controls.Add(this.tabPage1);
            this.customTabControl1.Controls.Add(this.tabPage4);
            this.customTabControl1.Controls.Add(this.tabPage5);
            this.customTabControl1.Controls.Add(this.tabPage6);
            this.customTabControl1.Controls.Add(this.tabPage7);
            this.customTabControl1.Controls.Add(this.tabPage8);
            this.customTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTabControl1.ImageList = this.tabImageList;
            this.customTabControl1.Location = new System.Drawing.Point(0, 0);
            this.customTabControl1.Name = "customTabControl1";
            this.customTabControl1.Overlap = 5;
            this.customTabControl1.Padding = new System.Drawing.Point(15, 5);
            this.customTabControl1.SelectedIndex = 0;
            this.customTabControl1.Size = new System.Drawing.Size(784, 411);
            this.customTabControl1.TabIndex = 0;
            this.customTabControl1.SizeChanged += new System.EventHandler(this.tabControl1_SizeChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.ImageKey = "live_submission";
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 378);
            this.tabPage1.TabIndex = 6;
            this.tabPage1.Text = "SUBMISSIONS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.ImageKey = "problems";
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(777, 378);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "PROBLEMS";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.ImageKey = "profile";
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(777, 378);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "PROFILES";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.ImageKey = "code";
            this.tabPage6.Location = new System.Drawing.Point(4, 29);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(777, 378);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "CODES";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.ImageKey = "submit";
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(777, 378);
            this.tabPage7.TabIndex = 4;
            this.tabPage7.Text = "SUBMIT";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tabPage8
            // 
            this.tabPage8.ImageKey = "utility";
            this.tabPage8.Location = new System.Drawing.Point(4, 29);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(777, 378);
            this.tabPage8.TabIndex = 5;
            this.tabPage8.Text = "UTILITIES";
            this.tabPage8.UseVisualStyleBackColor = true;
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
            this.Text = "UVA Arena";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.customTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.CustomTabControl customTabControl1;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.TabPage tabPage1;
          
    }
}


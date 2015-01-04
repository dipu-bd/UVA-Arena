namespace UVA_Arena 
{
    partial class SubmitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubmitForm));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.goDiscussButton = new System.Windows.Forms.Button();
            this.discussUrlBox = new System.Windows.Forms.TextBox();
            this.homeDiscussButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 32);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(684, 302);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.progress1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 334);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 28);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status1
            // 
            this.status1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(567, 23);
            this.status1.Spring = true;
            this.status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progress1
            // 
            this.progress1.Name = "progress1";
            this.progress1.Size = new System.Drawing.Size(100, 22);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.goDiscussButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.discussUrlBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.homeDiscussButton, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(684, 32);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // goDiscussButton
            // 
            this.goDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goDiscussButton.Location = new System.Drawing.Point(585, 4);
            this.goDiscussButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.goDiscussButton.Name = "goDiscussButton";
            this.goDiscussButton.Size = new System.Drawing.Size(94, 24);
            this.goDiscussButton.TabIndex = 1;
            this.goDiscussButton.Text = "GO";
            this.goDiscussButton.UseVisualStyleBackColor = true;
            this.goDiscussButton.Click += new System.EventHandler(this.goDiscussButton_Click);
            // 
            // discussUrlBox
            // 
            this.discussUrlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.discussUrlBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.discussUrlBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.discussUrlBox.BackColor = System.Drawing.Color.MintCream;
            this.discussUrlBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.discussUrlBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discussUrlBox.ForeColor = System.Drawing.Color.Black;
            this.discussUrlBox.Location = new System.Drawing.Point(45, 6);
            this.discussUrlBox.Name = "discussUrlBox";
            this.discussUrlBox.Size = new System.Drawing.Size(534, 20);
            this.discussUrlBox.TabIndex = 3;
            // 
            // homeDiscussButton
            // 
            this.homeDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeDiscussButton.Image = global::UVA_Arena.Properties.Resources.home;
            this.homeDiscussButton.Location = new System.Drawing.Point(4, 4);
            this.homeDiscussButton.Margin = new System.Windows.Forms.Padding(2);
            this.homeDiscussButton.Name = "homeDiscussButton";
            this.homeDiscussButton.Size = new System.Drawing.Size(36, 24);
            this.homeDiscussButton.TabIndex = 6;
            this.homeDiscussButton.UseVisualStyleBackColor = true;
            this.homeDiscussButton.Click += new System.EventHandler(this.homeDiscussButton_Click);
            // 
            // SubmitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(684, 362);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubmitForm";
            this.Text = "Submit Problem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status1;
        private System.Windows.Forms.ToolStripProgressBar progress1;
        public System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button goDiscussButton;
        private System.Windows.Forms.TextBox discussUrlBox;
        private System.Windows.Forms.Button homeDiscussButton;
    }
}
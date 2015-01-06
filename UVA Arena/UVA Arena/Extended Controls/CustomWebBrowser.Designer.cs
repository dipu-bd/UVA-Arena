namespace UVA_Arena.ExtendedControls
{
    partial class CustomWebBrowser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.topBar = new System.Windows.Forms.TableLayoutPanel();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.urlBox = new System.Windows.Forms.TextBox();
            this.homeButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress1 = new System.Windows.Forms.ToolStripProgressBar();
            this.topBar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 30);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(568, 267);
            this.webBrowser1.TabIndex = 5;
            this.webBrowser1.CanGoBackChanged += new System.EventHandler(this.webBrowser1_CanGoBackChanged);
            this.webBrowser1.CanGoForwardChanged += new System.EventHandler(this.webBrowser1_CanGoForwardChanged);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser1_ProgressChanged);
            this.webBrowser1.StatusTextChanged += new System.EventHandler(this.webBrowser1_StatusTextChanged);
            this.webBrowser1.DocumentTitleChanged += new System.EventHandler(this.webBrowser1_DocumentTitleChanged);
            // 
            // topBar
            // 
            this.topBar.BackColor = System.Drawing.Color.LightBlue;
            this.topBar.ColumnCount = 5;
            this.topBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.topBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topBar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.topBar.Controls.Add(this.nextButton, 0, 0);
            this.topBar.Controls.Add(this.backButton, 0, 0);
            this.topBar.Controls.Add(this.goButton, 4, 0);
            this.topBar.Controls.Add(this.urlBox, 3, 0);
            this.topBar.Controls.Add(this.homeButton, 2, 0);
            this.topBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.topBar.Location = new System.Drawing.Point(0, 0);
            this.topBar.Name = "topBar";
            this.topBar.RowCount = 1;
            this.topBar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.topBar.Size = new System.Drawing.Size(568, 30);
            this.topBar.TabIndex = 7;
            this.topBar.Enter += new System.EventHandler(this.topBox_Enter);
            this.topBar.Leave += new System.EventHandler(this.topBox_Leave);
            // 
            // nextButton
            // 
            this.nextButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextButton.Enabled = false;
            this.nextButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextButton.Image = global::UVA_Arena.Properties.Resources.next;
            this.nextButton.Location = new System.Drawing.Point(38, 1);
            this.nextButton.Margin = new System.Windows.Forms.Padding(1);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(35, 28);
            this.nextButton.TabIndex = 8;
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backButton.Enabled = false;
            this.backButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backButton.Image = global::UVA_Arena.Properties.Resources.prev;
            this.backButton.Location = new System.Drawing.Point(1, 1);
            this.backButton.Margin = new System.Windows.Forms.Padding(1);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(35, 28);
            this.backButton.TabIndex = 7;
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // goButton
            // 
            this.goButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goButton.ForeColor = System.Drawing.Color.Maroon;
            this.goButton.Location = new System.Drawing.Point(469, 1);
            this.goButton.Margin = new System.Windows.Forms.Padding(1);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(98, 28);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "GO";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // urlBox
            // 
            this.urlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.urlBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.urlBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.urlBox.BackColor = System.Drawing.Color.MintCream;
            this.urlBox.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.urlBox.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.urlBox.Location = new System.Drawing.Point(112, 2);
            this.urlBox.Margin = new System.Windows.Forms.Padding(1);
            this.urlBox.Name = "urlBox";
            this.urlBox.Size = new System.Drawing.Size(355, 26);
            this.urlBox.TabIndex = 3;
            this.urlBox.Text = "about:blank";
            this.urlBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlBox_KeyDown);
            // 
            // homeButton
            // 
            this.homeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeButton.Image = global::UVA_Arena.Properties.Resources.home;
            this.homeButton.Location = new System.Drawing.Point(75, 1);
            this.homeButton.Margin = new System.Windows.Forms.Padding(1);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(35, 28);
            this.homeButton.TabIndex = 6;
            this.homeButton.UseVisualStyleBackColor = true;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status1,
            this.toolStripStatusLabel1,
            this.progress1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 297);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(568, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status1
            // 
            this.status1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.status1.Name = "status1";
            this.status1.Size = new System.Drawing.Size(368, 21);
            this.status1.Spring = true;
            this.status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.IsLink = true;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(83, 21);
            this.toolStripStatusLabel1.Text = "Clear Cookies";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // progress1
            // 
            this.progress1.Name = "progress1";
            this.progress1.Size = new System.Drawing.Size(100, 20);
            // 
            // CustomWebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.topBar);
            this.Controls.Add(this.statusStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CustomWebBrowser";
            this.Size = new System.Drawing.Size(568, 323);
            this.topBar.ResumeLayout(false);
            this.topBar.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.TextBox urlBox;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status1;
        private System.Windows.Forms.ToolStripProgressBar progress1;
        public System.Windows.Forms.TableLayoutPanel topBar;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

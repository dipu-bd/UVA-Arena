namespace UVA_Arena.Elements
{
    partial class ProblemViewer
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.descriptionTab = new System.Windows.Forms.TabPage();
            this.problemWebBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.nextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pdfButton = new System.Windows.Forms.ToolStripButton();
            this.externalButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.codeButton = new System.Windows.Forms.ToolStripButton();
            this.submitButton = new System.Windows.Forms.ToolStripButton();
            this.markButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadButton = new System.Windows.Forms.ToolStripButton();
            this.statusTab = new System.Windows.Forms.TabPage();
            this.ranksTab = new System.Windows.Forms.TabPage();
            this.compareTab = new System.Windows.Forms.TabPage();
            this.discussTab = new System.Windows.Forms.TabPage();
            this.discussWebBrowser = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.goDiscussButton = new System.Windows.Forms.Button();
            this.discussUrlBox = new System.Windows.Forms.TextBox();
            this.prevDiscussButton = new System.Windows.Forms.Button();
            this.nextDiscussButton = new System.Windows.Forms.Button();
            this.homeDiscussButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.catagoryButton = new System.Windows.Forms.Button();
            this.titleBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.prevContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nextContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.catagoryInfo = new System.Windows.Forms.CueTextBox();
            this.tabControl1.SuspendLayout();
            this.descriptionTab.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.discussTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.descriptionTab);
            this.tabControl1.Controls.Add(this.statusTab);
            this.tabControl1.Controls.Add(this.ranksTab);
            this.tabControl1.Controls.Add(this.compareTab);
            this.tabControl1.Controls.Add(this.discussTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(35, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(718, 336);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // descriptionTab
            // 
            this.descriptionTab.BackColor = System.Drawing.Color.AliceBlue;
            this.descriptionTab.Controls.Add(this.problemWebBrowser);
            this.descriptionTab.Controls.Add(this.toolStrip1);
            this.descriptionTab.Location = new System.Drawing.Point(4, 4);
            this.descriptionTab.Name = "descriptionTab";
            this.descriptionTab.Size = new System.Drawing.Size(710, 302);
            this.descriptionTab.TabIndex = 0;
            this.descriptionTab.Text = "Description";
            // 
            // problemWebBrowser
            // 
            this.problemWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemWebBrowser.Location = new System.Drawing.Point(0, 38);
            this.problemWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.problemWebBrowser.Name = "problemWebBrowser";
            this.problemWebBrowser.ScriptErrorsSuppressed = true;
            this.problemWebBrowser.Size = new System.Drawing.Size(710, 264);
            this.problemWebBrowser.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.nextButton,
            this.toolStripSeparator1,
            this.pdfButton,
            this.externalButton,
            this.toolStripSeparator2,
            this.codeButton,
            this.submitButton,
            this.markButton,
            this.toolStripSeparator3,
            this.reloadButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(710, 38);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // backButton
            // 
            this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backButton.Enabled = false;
            this.backButton.Image = global::UVA_Arena.Properties.Resources.prev;
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(23, 35);
            this.backButton.Text = "Previous";
            this.backButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextButton.Enabled = false;
            this.nextButton.Image = global::UVA_Arena.Properties.Resources.next;
            this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(23, 35);
            this.nextButton.Text = "Next";
            this.nextButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // pdfButton
            // 
            this.pdfButton.Image = global::UVA_Arena.Properties.Resources.pdf;
            this.pdfButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pdfButton.Name = "pdfButton";
            this.pdfButton.Size = new System.Drawing.Size(32, 35);
            this.pdfButton.Text = "PDF";
            this.pdfButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.pdfButton.Click += new System.EventHandler(this.pdfButton_Click);
            // 
            // externalButton
            // 
            this.externalButton.Image = global::UVA_Arena.Properties.Resources.web;
            this.externalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.externalButton.Name = "externalButton";
            this.externalButton.Size = new System.Drawing.Size(49, 35);
            this.externalButton.Text = "Browse";
            this.externalButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.externalButton.Click += new System.EventHandler(this.externalButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // codeButton
            // 
            this.codeButton.Image = global::UVA_Arena.Properties.Resources.code;
            this.codeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(39, 35);
            this.codeButton.Text = "Code";
            this.codeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.codeButton.Click += new System.EventHandler(this.codeButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Image = global::UVA_Arena.Properties.Resources.submit;
            this.submitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(49, 35);
            this.submitButton.Text = "Submit";
            this.submitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // markButton
            // 
            this.markButton.Image = global::UVA_Arena.Properties.Resources.favourite;
            this.markButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markButton.Name = "markButton";
            this.markButton.Size = new System.Drawing.Size(38, 35);
            this.markButton.Text = "Mark";
            this.markButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.markButton.Click += new System.EventHandler(this.markButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // reloadButton
            // 
            this.reloadButton.Image = global::UVA_Arena.Properties.Resources.reload;
            this.reloadButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(47, 35);
            this.reloadButton.Text = "Reload";
            this.reloadButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // statusTab
            // 
            this.statusTab.Location = new System.Drawing.Point(4, 4);
            this.statusTab.Name = "statusTab";
            this.statusTab.Size = new System.Drawing.Size(710, 302);
            this.statusTab.TabIndex = 1;
            this.statusTab.Text = "Status";
            this.statusTab.UseVisualStyleBackColor = true;
            // 
            // ranksTab
            // 
            this.ranksTab.Location = new System.Drawing.Point(4, 4);
            this.ranksTab.Name = "ranksTab";
            this.ranksTab.Size = new System.Drawing.Size(710, 302);
            this.ranksTab.TabIndex = 2;
            this.ranksTab.Text = "Ranks";
            this.ranksTab.UseVisualStyleBackColor = true;
            // 
            // compareTab
            // 
            this.compareTab.Location = new System.Drawing.Point(4, 4);
            this.compareTab.Name = "compareTab";
            this.compareTab.Size = new System.Drawing.Size(710, 302);
            this.compareTab.TabIndex = 3;
            this.compareTab.Text = "Compare";
            this.compareTab.UseVisualStyleBackColor = true;
            // 
            // discussTab
            // 
            this.discussTab.Controls.Add(this.discussWebBrowser);
            this.discussTab.Controls.Add(this.tableLayoutPanel2);
            this.discussTab.Location = new System.Drawing.Point(4, 4);
            this.discussTab.Name = "discussTab";
            this.discussTab.Size = new System.Drawing.Size(710, 302);
            this.discussTab.TabIndex = 4;
            this.discussTab.Text = "Discuss";
            this.discussTab.UseVisualStyleBackColor = true;
            // 
            // discussWebBrowser
            // 
            this.discussWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discussWebBrowser.Location = new System.Drawing.Point(0, 28);
            this.discussWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.discussWebBrowser.Name = "discussWebBrowser";
            this.discussWebBrowser.ScriptErrorsSuppressed = true;
            this.discussWebBrowser.Size = new System.Drawing.Size(710, 274);
            this.discussWebBrowser.TabIndex = 2;
            this.discussWebBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser2_Navigated);
            this.discussWebBrowser.ProgressChanged += new System.Windows.Forms.WebBrowserProgressChangedEventHandler(this.webBrowser2_ProgressChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.LightCyan;
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.goDiscussButton, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.discussUrlBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.prevDiscussButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.nextDiscussButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.homeDiscussButton, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(710, 28);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // goDiscussButton
            // 
            this.goDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goDiscussButton.Location = new System.Drawing.Point(623, 2);
            this.goDiscussButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.goDiscussButton.Name = "goDiscussButton";
            this.goDiscussButton.Size = new System.Drawing.Size(84, 24);
            this.goDiscussButton.TabIndex = 1;
            this.goDiscussButton.Text = "GO";
            this.goDiscussButton.UseVisualStyleBackColor = true;
            this.goDiscussButton.Click += new System.EventHandler(this.goDiscussButton_Click);
            // 
            // discussUrlBox
            // 
            this.discussUrlBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.discussUrlBox.BackColor = System.Drawing.Color.White;
            this.discussUrlBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.discussUrlBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discussUrlBox.Location = new System.Drawing.Point(123, 4);
            this.discussUrlBox.Name = "discussUrlBox";
            this.discussUrlBox.Size = new System.Drawing.Size(494, 20);
            this.discussUrlBox.TabIndex = 3;
            // 
            // prevDiscussButton
            // 
            this.prevDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevDiscussButton.Image = global::UVA_Arena.Properties.Resources.prev;
            this.prevDiscussButton.Location = new System.Drawing.Point(2, 2);
            this.prevDiscussButton.Margin = new System.Windows.Forms.Padding(2);
            this.prevDiscussButton.Name = "prevDiscussButton";
            this.prevDiscussButton.Size = new System.Drawing.Size(36, 24);
            this.prevDiscussButton.TabIndex = 4;
            this.prevDiscussButton.UseVisualStyleBackColor = true;
            this.prevDiscussButton.Click += new System.EventHandler(this.prevDiscussButton_Click);
            // 
            // nextDiscussButton
            // 
            this.nextDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextDiscussButton.Image = global::UVA_Arena.Properties.Resources.next;
            this.nextDiscussButton.Location = new System.Drawing.Point(42, 2);
            this.nextDiscussButton.Margin = new System.Windows.Forms.Padding(2);
            this.nextDiscussButton.Name = "nextDiscussButton";
            this.nextDiscussButton.Size = new System.Drawing.Size(36, 24);
            this.nextDiscussButton.TabIndex = 5;
            this.nextDiscussButton.UseVisualStyleBackColor = true;
            this.nextDiscussButton.Click += new System.EventHandler(this.nextDiscussButton_Click);
            // 
            // homeDiscussButton
            // 
            this.homeDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homeDiscussButton.Image = global::UVA_Arena.Properties.Resources.home;
            this.homeDiscussButton.Location = new System.Drawing.Point(82, 2);
            this.homeDiscussButton.Margin = new System.Windows.Forms.Padding(2);
            this.homeDiscussButton.Name = "homeDiscussButton";
            this.homeDiscussButton.Size = new System.Drawing.Size(36, 24);
            this.homeDiscussButton.TabIndex = 6;
            this.homeDiscussButton.UseVisualStyleBackColor = true;
            this.homeDiscussButton.Click += new System.EventHandler(this.homeDiscussButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel1.Controls.Add(this.catagoryButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.titleBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.catagoryInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(718, 60);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // catagoryButton
            // 
            this.catagoryButton.BackColor = System.Drawing.Color.PowderBlue;
            this.catagoryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catagoryButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkTurquoise;
            this.catagoryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSeaGreen;
            this.catagoryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumTurquoise;
            this.catagoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.catagoryButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catagoryButton.Location = new System.Drawing.Point(595, 33);
            this.catagoryButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.catagoryButton.Name = "catagoryButton";
            this.catagoryButton.Size = new System.Drawing.Size(121, 26);
            this.catagoryButton.TabIndex = 2;
            this.catagoryButton.Text = "Change";
            this.catagoryButton.UseVisualStyleBackColor = false;
            this.catagoryButton.Visible = false;
            this.catagoryButton.Click += new System.EventHandler(this.catagoryButton_Click);
            // 
            // titleBox1
            // 
            this.titleBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox1.BackColor = System.Drawing.Color.LightBlue;
            this.titleBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titleBox1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBox1.ForeColor = System.Drawing.Color.Navy;
            this.titleBox1.Location = new System.Drawing.Point(6, 4);
            this.titleBox1.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.titleBox1.Name = "titleBox1";
            this.titleBox1.ReadOnly = true;
            this.titleBox1.Size = new System.Drawing.Size(584, 25);
            this.titleBox1.TabIndex = 0;
            this.titleBox1.TabStop = false;
            this.titleBox1.Text = "No problem selected";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Image = global::UVA_Arena.Properties.Resources.expand;
            this.button1.Location = new System.Drawing.Point(595, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "Expand";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.expandViewButton1_Click);
            // 
            // prevContext
            // 
            this.prevContext.Name = "prevContext";
            this.prevContext.Size = new System.Drawing.Size(61, 4);
            // 
            // nextContext
            // 
            this.nextContext.Name = "nextContext";
            this.nextContext.Size = new System.Drawing.Size(61, 4);
            // 
            // catagoryInfo
            // 
            this.catagoryInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.catagoryInfo.BackColor = System.Drawing.Color.LightBlue;
            this.catagoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.catagoryInfo.CueText = "Set tags to identify the type of the problem...";
            this.catagoryInfo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catagoryInfo.ForeColor = System.Drawing.Color.Maroon;
            this.catagoryInfo.Location = new System.Drawing.Point(4, 38);
            this.catagoryInfo.Name = "catagoryInfo";
            this.catagoryInfo.Size = new System.Drawing.Size(586, 16);
            this.catagoryInfo.TabIndex = 1;
            this.catagoryInfo.TabStop = false;
            this.catagoryInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.catagoryInfo_KeyDown);
            // 
            // ProblemViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ProblemViewer";
            this.Size = new System.Drawing.Size(718, 396);
            this.tabControl1.ResumeLayout(false);
            this.descriptionTab.ResumeLayout(false);
            this.descriptionTab.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.discussTab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage descriptionTab;
        private System.Windows.Forms.TabPage statusTab;
        private System.Windows.Forms.TabPage ranksTab;
        private System.Windows.Forms.TabPage compareTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip prevContext;
        private System.Windows.Forms.ContextMenuStrip nextContext;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton pdfButton;
        public System.Windows.Forms.ToolStripButton externalButton;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton codeButton;
        public System.Windows.Forms.ToolStripButton submitButton;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton markButton;
        public System.Windows.Forms.ToolStripButton backButton;
        public System.Windows.Forms.ToolStripButton nextButton;
        public System.Windows.Forms.ToolStripButton reloadButton;
        public System.Windows.Forms.TextBox titleBox1;
        public System.Windows.Forms.Button catagoryButton;
        public System.Windows.Forms.CueTextBox catagoryInfo;
        public System.Windows.Forms.WebBrowser problemWebBrowser;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage discussTab;
        public System.Windows.Forms.WebBrowser discussWebBrowser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button goDiscussButton;
        private System.Windows.Forms.TextBox discussUrlBox;
        private System.Windows.Forms.Button nextDiscussButton;
        private System.Windows.Forms.Button prevDiscussButton;
        private System.Windows.Forms.Button homeDiscussButton;
        private System.Windows.Forms.Button button1;
    }
}

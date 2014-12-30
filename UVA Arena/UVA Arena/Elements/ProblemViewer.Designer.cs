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
            this.submissionTab = new System.Windows.Forms.TabPage();
            this.submissionStatus = new BrightIdeasSoftware.FastObjectListView();
            this.sidSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.unameSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fullnameSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lanSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.verSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.runSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rankSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.subtimeSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel7 = new System.Windows.Forms.Panel();
            this.subListLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.usernameList1 = new BrightIdeasSoftware.FastObjectListView();
            this.unameCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.uidCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel6 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.showSubmissionButton = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.showRanksButton = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.showUsersRankButton = new System.Windows.Forms.Button();
            this.showUserSubButton = new System.Windows.Forms.Button();
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
            this.catagoryInfo = new System.Windows.Forms.CueTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.prevContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nextContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1.SuspendLayout();
            this.descriptionTab.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.submissionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.submissionStatus)).BeginInit();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usernameList1)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.discussTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.descriptionTab);
            this.tabControl1.Controls.Add(this.submissionTab);
            this.tabControl1.Controls.Add(this.compareTab);
            this.tabControl1.Controls.Add(this.discussTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(35, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 390);
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
            this.descriptionTab.Size = new System.Drawing.Size(792, 356);
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
            this.problemWebBrowser.Size = new System.Drawing.Size(792, 318);
            this.problemWebBrowser.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DarkTurquoise;
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
            this.toolStrip1.Size = new System.Drawing.Size(792, 38);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.TabStop = true;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Image = global::UVA_Arena.Properties.Resources.prev;
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(36, 35);
            this.backButton.Text = "Back";
            this.backButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Enabled = false;
            this.nextButton.Image = global::UVA_Arena.Properties.Resources.next;
            this.nextButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(35, 35);
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
            // submissionTab
            // 
            this.submissionTab.Controls.Add(this.submissionStatus);
            this.submissionTab.Controls.Add(this.panel7);
            this.submissionTab.Controls.Add(this.tableLayoutPanel4);
            this.submissionTab.Location = new System.Drawing.Point(4, 4);
            this.submissionTab.Name = "submissionTab";
            this.submissionTab.Size = new System.Drawing.Size(792, 356);
            this.submissionTab.TabIndex = 1;
            this.submissionTab.Text = "Submissions";
            this.submissionTab.UseVisualStyleBackColor = true;
            // 
            // submissionStatus
            // 
            this.submissionStatus.AllColumns.Add(this.sidSUB);
            this.submissionStatus.AllColumns.Add(this.unameSUB);
            this.submissionStatus.AllColumns.Add(this.fullnameSUB);
            this.submissionStatus.AllColumns.Add(this.lanSUB);
            this.submissionStatus.AllColumns.Add(this.verSUB);
            this.submissionStatus.AllColumns.Add(this.runSUB);
            this.submissionStatus.AllColumns.Add(this.rankSUB);
            this.submissionStatus.AllColumns.Add(this.subtimeSUB);
            this.submissionStatus.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.submissionStatus.BackColor = System.Drawing.Color.Azure;
            this.submissionStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.submissionStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sidSUB,
            this.unameSUB,
            this.fullnameSUB,
            this.lanSUB,
            this.verSUB,
            this.runSUB,
            this.rankSUB,
            this.subtimeSUB});
            this.submissionStatus.CopySelectionOnControlC = false;
            this.submissionStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.submissionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.submissionStatus.EmptyListMsg = "Refresh to view last submissions";
            this.submissionStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submissionStatus.ForeColor = System.Drawing.Color.Black;
            this.submissionStatus.FullRowSelect = true;
            this.submissionStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.submissionStatus.Location = new System.Drawing.Point(0, 31);
            this.submissionStatus.Name = "submissionStatus";
            this.submissionStatus.ShowGroups = false;
            this.submissionStatus.ShowItemToolTips = true;
            this.submissionStatus.Size = new System.Drawing.Size(598, 325);
            this.submissionStatus.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.submissionStatus.TabIndex = 2;
            this.submissionStatus.UseCellFormatEvents = true;
            this.submissionStatus.UseCompatibleStateImageBehavior = false;
            this.submissionStatus.UseCustomSelectionColors = true;
            this.submissionStatus.UseHyperlinks = true;
            this.submissionStatus.UseTranslucentSelection = true;
            this.submissionStatus.View = System.Windows.Forms.View.Details;
            this.submissionStatus.VirtualMode = true;
            this.submissionStatus.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.submissionStatus_FormatCell);
            this.submissionStatus.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.submissionStatus_HyperlinkClicked);
            // 
            // sidSUB
            // 
            this.sidSUB.AspectName = "sid";
            this.sidSUB.CellPadding = null;
            this.sidSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sidSUB.Text = "SID";
            this.sidSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sidSUB.Width = 75;
            // 
            // unameSUB
            // 
            this.unameSUB.AspectName = "uname";
            this.unameSUB.CellPadding = null;
            this.unameSUB.FillsFreeSpace = true;
            this.unameSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unameSUB.Hyperlink = true;
            this.unameSUB.MinimumWidth = 80;
            this.unameSUB.Text = "User";
            this.unameSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unameSUB.Width = 120;
            // 
            // fullnameSUB
            // 
            this.fullnameSUB.AspectName = "name";
            this.fullnameSUB.CellPadding = null;
            this.fullnameSUB.FillsFreeSpace = true;
            this.fullnameSUB.MinimumWidth = 150;
            this.fullnameSUB.Text = "Full Name";
            this.fullnameSUB.Width = 160;
            // 
            // lanSUB
            // 
            this.lanSUB.AspectName = "lan";
            this.lanSUB.CellPadding = null;
            this.lanSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lanSUB.Text = "Language";
            this.lanSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lanSUB.Width = 85;
            // 
            // verSUB
            // 
            this.verSUB.AspectName = "ver";
            this.verSUB.CellPadding = null;
            this.verSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.Text = "Verdict";
            this.verSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.Width = 110;
            // 
            // runSUB
            // 
            this.runSUB.AspectName = "run";
            this.runSUB.CellPadding = null;
            this.runSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runSUB.Text = "Runtime";
            this.runSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runSUB.Width = 80;
            // 
            // rankSUB
            // 
            this.rankSUB.AspectName = "rank";
            this.rankSUB.CellPadding = null;
            this.rankSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rankSUB.Text = "Rank";
            this.rankSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rankSUB.Width = 80;
            // 
            // subtimeSUB
            // 
            this.subtimeSUB.AspectName = "sbt";
            this.subtimeSUB.CellPadding = null;
            this.subtimeSUB.FillsFreeSpace = true;
            this.subtimeSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.subtimeSUB.MinimumWidth = 140;
            this.subtimeSUB.Text = "Submission Time";
            this.subtimeSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.subtimeSUB.Width = 140;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel7.Controls.Add(this.subListLabel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(598, 31);
            this.panel7.TabIndex = 6;
            // 
            // subListLabel
            // 
            this.subListLabel.BackColor = System.Drawing.Color.Transparent;
            this.subListLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subListLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subListLabel.ForeColor = System.Drawing.Color.Navy;
            this.subListLabel.Location = new System.Drawing.Point(0, 0);
            this.subListLabel.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.subListLabel.Name = "subListLabel";
            this.subListLabel.Size = new System.Drawing.Size(598, 31);
            this.subListLabel.TabIndex = 2;
            this.subListLabel.Text = "Last submissions on this problem";
            this.subListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.usernameList1, 0, 8);
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.showSubmissionButton, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker1, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.showRanksButton, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.showUsersRankButton, 0, 10);
            this.tableLayoutPanel4.Controls.Add(this.showUserSubButton, 0, 9);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(598, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 12;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(194, 356);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // usernameList1
            // 
            this.usernameList1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.usernameList1.AllColumns.Add(this.unameCol);
            this.usernameList1.AllColumns.Add(this.uidCol);
            this.usernameList1.BackColor = System.Drawing.Color.Azure;
            this.usernameList1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.usernameList1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.unameCol,
            this.uidCol});
            this.usernameList1.Cursor = System.Windows.Forms.Cursors.Default;
            this.usernameList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.usernameList1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameList1.FullRowSelect = true;
            this.usernameList1.Location = new System.Drawing.Point(3, 195);
            this.usernameList1.Name = "usernameList1";
            this.usernameList1.ShowGroups = false;
            this.usernameList1.Size = new System.Drawing.Size(188, 92);
            this.usernameList1.TabIndex = 12;
            this.usernameList1.UseCellFormatEvents = true;
            this.usernameList1.UseCompatibleStateImageBehavior = false;
            this.usernameList1.UseCustomSelectionColors = true;
            this.usernameList1.UseHotItem = true;
            this.usernameList1.UseTranslucentHotItem = true;
            this.usernameList1.UseTranslucentSelection = true;
            this.usernameList1.View = System.Windows.Forms.View.Details;
            this.usernameList1.VirtualMode = true;
            this.usernameList1.SelectedIndexChanged += new System.EventHandler(this.usernameList1_SelectedIndexChanged);
            // 
            // unameCol
            // 
            this.unameCol.AspectName = "Key";
            this.unameCol.CellPadding = null;
            this.unameCol.FillsFreeSpace = true;
            this.unameCol.MinimumWidth = 100;
            this.unameCol.Text = "Username";
            this.unameCol.Width = 100;
            // 
            // uidCol
            // 
            this.uidCol.AspectName = "Value";
            this.uidCol.CellPadding = null;
            this.uidCol.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uidCol.Text = "User ID";
            this.uidCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uidCol.Width = 80;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1, 168);
            this.panel6.Margin = new System.Windows.Forms.Padding(1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(192, 23);
            this.panel6.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "User\'s Rank";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // showSubmissionButton
            // 
            this.showSubmissionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showSubmissionButton.Location = new System.Drawing.Point(69, 61);
            this.showSubmissionButton.Margin = new System.Windows.Forms.Padding(1);
            this.showSubmissionButton.Name = "showSubmissionButton";
            this.showSubmissionButton.Size = new System.Drawing.Size(124, 26);
            this.showSubmissionButton.TabIndex = 4;
            this.showSubmissionButton.Text = "Show Submissions";
            this.showSubmissionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showSubmissionButton.UseVisualStyleBackColor = true;
            this.showSubmissionButton.Click += new System.EventHandler(this.submissionReloadButton_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 36);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(188, 23);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 11);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 23);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Submissions From";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 89);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 23);
            this.panel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ranks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // showRanksButton
            // 
            this.showRanksButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showRanksButton.Location = new System.Drawing.Point(69, 140);
            this.showRanksButton.Margin = new System.Windows.Forms.Padding(1);
            this.showRanksButton.Name = "showRanksButton";
            this.showRanksButton.Size = new System.Drawing.Size(124, 26);
            this.showRanksButton.TabIndex = 7;
            this.showRanksButton.Text = "Show Ranks";
            this.showRanksButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showRanksButton.UseVisualStyleBackColor = true;
            this.showRanksButton.Click += new System.EventHandler(this.showRanksButton_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.numericUpDown1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 113);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(194, 26);
            this.panel4.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Count :";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.BackColor = System.Drawing.Color.LightCyan;
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericUpDown1.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.ForeColor = System.Drawing.Color.Black;
            this.numericUpDown1.Location = new System.Drawing.Point(53, 3);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(137, 21);
            this.numericUpDown1.TabIndex = 10;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // showUsersRankButton
            // 
            this.showUsersRankButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showUsersRankButton.Location = new System.Drawing.Point(1, 319);
            this.showUsersRankButton.Margin = new System.Windows.Forms.Padding(1);
            this.showUsersRankButton.Name = "showUsersRankButton";
            this.showUsersRankButton.Size = new System.Drawing.Size(192, 26);
            this.showUsersRankButton.TabIndex = 11;
            this.showUsersRankButton.Tag = "Rank of {0}";
            this.showUsersRankButton.Text = "Rank of User";
            this.showUsersRankButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showUsersRankButton.UseVisualStyleBackColor = true;
            this.showUsersRankButton.Click += new System.EventHandler(this.showUsersRankButton_Click);
            // 
            // showUserSubButton
            // 
            this.showUserSubButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showUserSubButton.Location = new System.Drawing.Point(1, 291);
            this.showUserSubButton.Margin = new System.Windows.Forms.Padding(1);
            this.showUserSubButton.Name = "showUserSubButton";
            this.showUserSubButton.Size = new System.Drawing.Size(192, 26);
            this.showUserSubButton.TabIndex = 13;
            this.showUserSubButton.Tag = "Submissions of {0}";
            this.showUserSubButton.Text = "Submissions of User";
            this.showUserSubButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showUserSubButton.UseVisualStyleBackColor = true;
            this.showUserSubButton.Click += new System.EventHandler(this.showUserSubButton_Click);
            // 
            // compareTab
            // 
            this.compareTab.Location = new System.Drawing.Point(4, 4);
            this.compareTab.Name = "compareTab";
            this.compareTab.Size = new System.Drawing.Size(792, 356);
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
            this.discussTab.Size = new System.Drawing.Size(792, 356);
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
            this.discussWebBrowser.Size = new System.Drawing.Size(792, 328);
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
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(792, 28);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // goDiscussButton
            // 
            this.goDiscussButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goDiscussButton.Location = new System.Drawing.Point(695, 2);
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
            this.discussUrlBox.BackColor = System.Drawing.Color.White;
            this.discussUrlBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.discussUrlBox.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discussUrlBox.Location = new System.Drawing.Point(123, 4);
            this.discussUrlBox.Name = "discussUrlBox";
            this.discussUrlBox.Size = new System.Drawing.Size(566, 20);
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
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 60);
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
            this.catagoryButton.Location = new System.Drawing.Point(659, 33);
            this.catagoryButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.catagoryButton.Name = "catagoryButton";
            this.catagoryButton.Size = new System.Drawing.Size(139, 26);
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
            this.titleBox1.ForeColor = System.Drawing.Color.Indigo;
            this.titleBox1.Location = new System.Drawing.Point(6, 4);
            this.titleBox1.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.titleBox1.Name = "titleBox1";
            this.titleBox1.ReadOnly = true;
            this.titleBox1.Size = new System.Drawing.Size(648, 25);
            this.titleBox1.TabIndex = 0;
            this.titleBox1.TabStop = false;
            this.titleBox1.Text = "No problem selected";
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
            this.catagoryInfo.Size = new System.Drawing.Size(650, 16);
            this.catagoryInfo.TabIndex = 1;
            this.catagoryInfo.TabStop = false;
            this.catagoryInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.catagoryInfo_KeyDown);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Image = global::UVA_Arena.Properties.Resources.expand;
            this.button1.Location = new System.Drawing.Point(659, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 29);
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
            this.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.ResumeLayout(false);
            this.descriptionTab.ResumeLayout(false);
            this.descriptionTab.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.submissionTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.submissionStatus)).EndInit();
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usernameList1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
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
        private System.Windows.Forms.TabPage submissionTab;
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
        public BrightIdeasSoftware.FastObjectListView submissionStatus;
        private BrightIdeasSoftware.OLVColumn sidSUB;
        private BrightIdeasSoftware.OLVColumn unameSUB;
        private BrightIdeasSoftware.OLVColumn fullnameSUB;
        private BrightIdeasSoftware.OLVColumn lanSUB;
        private BrightIdeasSoftware.OLVColumn verSUB;
        private BrightIdeasSoftware.OLVColumn runSUB;
        private BrightIdeasSoftware.OLVColumn rankSUB;
        private BrightIdeasSoftware.OLVColumn subtimeSUB;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button showSubmissionButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button showRanksButton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button showUsersRankButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label4;
        public BrightIdeasSoftware.FastObjectListView usernameList1;
        private BrightIdeasSoftware.OLVColumn unameCol;
        private BrightIdeasSoftware.OLVColumn uidCol;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label subListLabel;
        private System.Windows.Forms.Button showUserSubButton;
    }
}

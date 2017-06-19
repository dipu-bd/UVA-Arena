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
            this.pdfTab = new System.Windows.Forms.TabPage();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.compareUserButton = new System.Windows.Forms.Button();
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.nextButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.codeButton = new System.Windows.Forms.ToolStripButton();
            this.submitButton = new System.Windows.Forms.ToolStripButton();
            this.markButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.externalButton = new System.Windows.Forms.ToolStripButton();
            this.pdfToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadButton = new System.Windows.Forms.ToolStripButton();
            this.tagsOrNoteToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tagsEditorToolButton = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.up_downButton = new System.Windows.Forms.Button();
            this.titleBox1 = new System.Windows.Forms.Label();
            this.problemMessage = new System.Windows.Forms.Label();
            this.expandCollapseButton = new System.Windows.Forms.Button();
            this.prevContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nextContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TopPanel = new System.Windows.Forms.Panel();
            this.downloadingNoticeLabel = new System.Windows.Forms.Label();
            this.pdfViewer1 = new UVA_Arena.Components.PDFViewer();
            this.tabControl1.SuspendLayout();
            this.pdfTab.SuspendLayout();
            this.submissionTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.submissionStatus)).BeginInit();
            this.panel7.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usernameList1)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pdfTab);
            this.tabControl1.Controls.Add(this.submissionTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(30, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 355);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // pdfTab
            // 
            this.pdfTab.BackColor = System.Drawing.Color.MediumTurquoise;
            this.pdfTab.Controls.Add(this.pdfViewer1);
            this.pdfTab.Controls.Add(this.downloadingNoticeLabel);
            this.pdfTab.Location = new System.Drawing.Point(4, 30);
            this.pdfTab.Name = "pdfTab";
            this.pdfTab.Size = new System.Drawing.Size(792, 321);
            this.pdfTab.TabIndex = 5;
            this.pdfTab.Text = "PDF";
            // 
            // submissionTab
            // 
            this.submissionTab.BackColor = System.Drawing.Color.PaleTurquoise;
            this.submissionTab.Controls.Add(this.submissionStatus);
            this.submissionTab.Controls.Add(this.panel7);
            this.submissionTab.Controls.Add(this.tableLayoutPanel4);
            this.submissionTab.Location = new System.Drawing.Point(4, 30);
            this.submissionTab.Name = "submissionTab";
            this.submissionTab.Size = new System.Drawing.Size(792, 321);
            this.submissionTab.TabIndex = 1;
            this.submissionTab.Text = "Submissions";
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
            this.submissionStatus.EmptyListMsg = "Select a problem and click here";
            this.submissionStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submissionStatus.ForeColor = System.Drawing.Color.Black;
            this.submissionStatus.FullRowSelect = true;
            this.submissionStatus.Location = new System.Drawing.Point(0, 31);
            this.submissionStatus.Name = "submissionStatus";
            this.submissionStatus.ShowGroups = false;
            this.submissionStatus.ShowItemCountOnGroups = true;
            this.submissionStatus.ShowItemToolTips = true;
            this.submissionStatus.Size = new System.Drawing.Size(592, 290);
            this.submissionStatus.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.submissionStatus.TabIndex = 0;
            this.submissionStatus.UseCellFormatEvents = true;
            this.submissionStatus.UseCompatibleStateImageBehavior = false;
            this.submissionStatus.UseCustomSelectionColors = true;
            this.submissionStatus.UseHyperlinks = true;
            this.submissionStatus.UseTranslucentSelection = true;
            this.submissionStatus.View = System.Windows.Forms.View.Details;
            this.submissionStatus.VirtualMode = true;
            this.submissionStatus.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.submissionStatus_FormatCell);
            this.submissionStatus.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.submissionStatus_HyperlinkClicked);
            this.submissionStatus.Click += new System.EventHandler(this.submissionStatus_Click);
            // 
            // sidSUB
            // 
            this.sidSUB.AspectName = "sid";
            this.sidSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sidSUB.Text = "SID";
            this.sidSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sidSUB.Width = 75;
            // 
            // unameSUB
            // 
            this.unameSUB.AspectName = "uname";
            this.unameSUB.GroupWithItemCountFormat = "\"{0}\" has following {1} submissions";
            this.unameSUB.GroupWithItemCountSingularFormat = "\"{0}\" has following submission";
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
            this.fullnameSUB.MinimumWidth = 150;
            this.fullnameSUB.Text = "Full Name";
            this.fullnameSUB.Width = 160;
            // 
            // lanSUB
            // 
            this.lanSUB.AspectName = "lan";
            this.lanSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lanSUB.Text = "Language";
            this.lanSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lanSUB.Width = 85;
            // 
            // verSUB
            // 
            this.verSUB.AspectName = "ver";
            this.verSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.Text = "Verdict";
            this.verSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.Width = 110;
            // 
            // runSUB
            // 
            this.runSUB.AspectName = "run";
            this.runSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runSUB.Text = "Runtime";
            this.runSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runSUB.Width = 80;
            // 
            // rankSUB
            // 
            this.rankSUB.AspectName = "rank";
            this.rankSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rankSUB.Text = "Rank";
            this.rankSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rankSUB.Width = 80;
            // 
            // subtimeSUB
            // 
            this.subtimeSUB.AspectName = "sbt";
            this.subtimeSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.subtimeSUB.MinimumWidth = 140;
            this.subtimeSUB.Text = "Submission Time";
            this.subtimeSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.subtimeSUB.Width = 160;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.subListLabel);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(592, 31);
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
            this.subListLabel.Size = new System.Drawing.Size(592, 31);
            this.subListLabel.TabIndex = 1;
            this.subListLabel.Text = "Last submissions on this problem";
            this.subListLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.LightBlue;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel3, 0, 10);
            this.tableLayoutPanel4.Controls.Add(this.compareUserButton, 0, 10);
            this.tableLayoutPanel4.Controls.Add(this.usernameList1, 0, 7);
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.showSubmissionButton, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.showRanksButton, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.showUsersRankButton, 0, 9);
            this.tableLayoutPanel4.Controls.Add(this.showUserSubButton, 0, 8);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(592, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 11;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(200, 321);
            this.tableLayoutPanel4.TabIndex = 2;
            this.tableLayoutPanel4.TabStop = true;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 269);
            this.panel3.Margin = new System.Windows.Forms.Padding(1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(198, 23);
            this.panel3.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "User Comparison";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // compareUserButton
            // 
            this.compareUserButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compareUserButton.Location = new System.Drawing.Point(1, 294);
            this.compareUserButton.Margin = new System.Windows.Forms.Padding(1);
            this.compareUserButton.Name = "compareUserButton";
            this.compareUserButton.Size = new System.Drawing.Size(198, 26);
            this.compareUserButton.TabIndex = 11;
            this.compareUserButton.Text = "Compare Users";
            this.compareUserButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.compareUserButton.UseVisualStyleBackColor = true;
            this.compareUserButton.Click += new System.EventHandler(this.compareUserButton_Click);
            // 
            // usernameList1
            // 
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
            this.usernameList1.ForeColor = System.Drawing.Color.Black;
            this.usernameList1.FullRowSelect = true;
            this.usernameList1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.usernameList1.Location = new System.Drawing.Point(3, 185);
            this.usernameList1.Name = "usernameList1";
            this.usernameList1.ShowGroups = false;
            this.usernameList1.Size = new System.Drawing.Size(194, 24);
            this.usernameList1.TabIndex = 7;
            this.usernameList1.UseCellFormatEvents = true;
            this.usernameList1.UseCompatibleStateImageBehavior = false;
            this.usernameList1.UseCustomSelectionColors = true;
            this.usernameList1.UseHotItem = true;
            this.usernameList1.UseTranslucentHotItem = true;
            this.usernameList1.UseTranslucentSelection = true;
            this.usernameList1.View = System.Windows.Forms.View.Details;
            this.usernameList1.VirtualMode = true;
            this.usernameList1.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.usernameList1_FormatCell);
            this.usernameList1.SelectedIndexChanged += new System.EventHandler(this.usernameList1_SelectedIndexChanged);
            // 
            // unameCol
            // 
            this.unameCol.AspectName = "Key";
            this.unameCol.FillsFreeSpace = true;
            this.unameCol.MinimumWidth = 80;
            this.unameCol.Text = "Username";
            this.unameCol.Width = 100;
            // 
            // uidCol
            // 
            this.uidCol.AspectName = "Value";
            this.uidCol.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uidCol.MinimumWidth = 60;
            this.uidCol.Text = "User ID";
            this.uidCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.uidCol.Width = 70;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Turquoise;
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(1, 158);
            this.panel6.Margin = new System.Windows.Forms.Padding(1);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(198, 23);
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
            this.label4.Size = new System.Drawing.Size(198, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "List of Users";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // showSubmissionButton
            // 
            this.showSubmissionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showSubmissionButton.Location = new System.Drawing.Point(1, 51);
            this.showSubmissionButton.Margin = new System.Windows.Forms.Padding(1);
            this.showSubmissionButton.Name = "showSubmissionButton";
            this.showSubmissionButton.Size = new System.Drawing.Size(198, 26);
            this.showSubmissionButton.TabIndex = 2;
            this.showSubmissionButton.Text = "Show Submissions";
            this.showSubmissionButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showSubmissionButton.UseVisualStyleBackColor = true;
            this.showSubmissionButton.Click += new System.EventHandler(this.submissionReloadButton_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd MMM yyyy, hh:mm tt";
            this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 26);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(194, 23);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 23);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Submissions From";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 79);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(198, 23);
            this.panel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ranks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // showRanksButton
            // 
            this.showRanksButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showRanksButton.Location = new System.Drawing.Point(1, 130);
            this.showRanksButton.Margin = new System.Windows.Forms.Padding(1);
            this.showRanksButton.Name = "showRanksButton";
            this.showRanksButton.Size = new System.Drawing.Size(198, 26);
            this.showRanksButton.TabIndex = 5;
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
            this.panel4.Location = new System.Drawing.Point(0, 103);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 26);
            this.panel4.TabIndex = 4;
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
            this.numericUpDown1.Size = new System.Drawing.Size(143, 21);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // showUsersRankButton
            // 
            this.showUsersRankButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showUsersRankButton.Location = new System.Drawing.Point(1, 241);
            this.showUsersRankButton.Margin = new System.Windows.Forms.Padding(1);
            this.showUsersRankButton.Name = "showUsersRankButton";
            this.showUsersRankButton.Size = new System.Drawing.Size(198, 26);
            this.showUsersRankButton.TabIndex = 9;
            this.showUsersRankButton.Tag = "Rank of {0}";
            this.showUsersRankButton.Text = "Show Rank of User";
            this.showUsersRankButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showUsersRankButton.UseVisualStyleBackColor = true;
            this.showUsersRankButton.Click += new System.EventHandler(this.showUsersRankButton_Click);
            // 
            // showUserSubButton
            // 
            this.showUserSubButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showUserSubButton.Location = new System.Drawing.Point(1, 213);
            this.showUserSubButton.Margin = new System.Windows.Forms.Padding(1);
            this.showUserSubButton.Name = "showUserSubButton";
            this.showUserSubButton.Size = new System.Drawing.Size(198, 26);
            this.showUserSubButton.TabIndex = 8;
            this.showUserSubButton.Tag = "Submissions of {0}";
            this.showUserSubButton.Text = "Submissions of User";
            this.showUserSubButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.showUserSubButton.UseVisualStyleBackColor = true;
            this.showUserSubButton.Click += new System.EventHandler(this.showUserSubButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 2);
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.nextButton,
            this.toolStripSeparator1,
            this.codeButton,
            this.submitButton,
            this.markButton,
            this.toolStripSeparator3,
            this.externalButton,
            this.pdfToolButton,
            this.toolStripSeparator2,
            this.reloadButton,
            this.tagsOrNoteToolButton,
            this.toolStripSeparator5,
            this.tagsEditorToolButton});
            this.toolStrip1.Location = new System.Drawing.Point(1, 54);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(766, 38);
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
            this.markButton.Image = global::UVA_Arena.Properties.Resources.favorite;
            this.markButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markButton.Name = "markButton";
            this.markButton.Size = new System.Drawing.Size(38, 35);
            this.markButton.Text = "Mark";
            this.markButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.markButton.CheckedChanged += new System.EventHandler(this.markButton_CheckedChanged);
            this.markButton.Click += new System.EventHandler(this.markButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // externalButton
            // 
            this.externalButton.Image = global::UVA_Arena.Properties.Resources.web;
            this.externalButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.externalButton.Name = "externalButton";
            this.externalButton.Size = new System.Drawing.Size(51, 35);
            this.externalButton.Text = "To Web";
            this.externalButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.externalButton.Click += new System.EventHandler(this.externalButton_Click);
            // 
            // pdfToolButton
            // 
            this.pdfToolButton.Image = global::UVA_Arena.Properties.Resources.pdf;
            this.pdfToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pdfToolButton.Name = "pdfToolButton";
            this.pdfToolButton.Size = new System.Drawing.Size(32, 35);
            this.pdfToolButton.Text = "PDF";
            this.pdfToolButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.pdfToolButton.Click += new System.EventHandler(this.pdfToolButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
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
            // tagsOrNoteToolButton
            // 
            this.tagsOrNoteToolButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tagsOrNoteToolButton.Image = global::UVA_Arena.Properties.Resources.category;
            this.tagsOrNoteToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tagsOrNoteToolButton.Name = "tagsOrNoteToolButton";
            this.tagsOrNoteToolButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tagsOrNoteToolButton.Size = new System.Drawing.Size(80, 35);
            this.tagsOrNoteToolButton.Text = "Tags + Notes";
            this.tagsOrNoteToolButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tagsOrNoteToolButton.ToolTipText = "Tags or Notes";
            this.tagsOrNoteToolButton.Click += new System.EventHandler(this.tagsOrNoteToolButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 38);
            // 
            // tagsEditorToolButton
            // 
            this.tagsEditorToolButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tagsEditorToolButton.Image = global::UVA_Arena.Properties.Resources.problem;
            this.tagsEditorToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tagsEditorToolButton.Name = "tagsEditorToolButton";
            this.tagsEditorToolButton.Size = new System.Drawing.Size(42, 35);
            this.tagsEditorToolButton.Text = "Editor";
            this.tagsEditorToolButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tagsEditorToolButton.Visible = false;
            this.tagsEditorToolButton.Click += new System.EventHandler(this.tagsEditorToolButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel1.Controls.Add(this.up_downButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.titleBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.problemMessage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(31, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 93);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.TabStop = true;
            // 
            // up_downButton
            // 
            this.up_downButton.BackColor = System.Drawing.Color.LightBlue;
            this.up_downButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.up_downButton.FlatAppearance.BorderSize = 0;
            this.up_downButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.up_downButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue;
            this.up_downButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.up_downButton.Image = global::UVA_Arena.Properties.Resources.moveup;
            this.up_downButton.Location = new System.Drawing.Point(708, 1);
            this.up_downButton.Margin = new System.Windows.Forms.Padding(0);
            this.up_downButton.Name = "up_downButton";
            this.tableLayoutPanel1.SetRowSpan(this.up_downButton, 2);
            this.up_downButton.Size = new System.Drawing.Size(59, 52);
            this.up_downButton.TabIndex = 2;
            this.up_downButton.UseVisualStyleBackColor = false;
            this.up_downButton.Click += new System.EventHandler(this.up_downButton_Click);
            // 
            // titleBox1
            // 
            this.titleBox1.AutoSize = true;
            this.titleBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBox1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBox1.ForeColor = System.Drawing.Color.Indigo;
            this.titleBox1.Location = new System.Drawing.Point(1, 1);
            this.titleBox1.Margin = new System.Windows.Forms.Padding(0);
            this.titleBox1.Name = "titleBox1";
            this.titleBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.titleBox1.Size = new System.Drawing.Size(706, 29);
            this.titleBox1.TabIndex = 0;
            this.titleBox1.Text = "No problem selected";
            // 
            // problemMessage
            // 
            this.problemMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.problemMessage.Font = new System.Drawing.Font("Cambria", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.problemMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(64)))), ((int)(((byte)(16)))));
            this.problemMessage.Location = new System.Drawing.Point(2, 32);
            this.problemMessage.Margin = new System.Windows.Forms.Padding(1);
            this.problemMessage.Name = "problemMessage";
            this.problemMessage.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.problemMessage.Size = new System.Drawing.Size(704, 20);
            this.problemMessage.TabIndex = 1;
            this.problemMessage.Tag = "You DID NOT TRY this problem.";
            this.problemMessage.Text = "You DID NOT TRY this problem.";
            this.problemMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // expandCollapseButton
            // 
            this.expandCollapseButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandCollapseButton.Image = global::UVA_Arena.Properties.Resources.hide;
            this.expandCollapseButton.Location = new System.Drawing.Point(1, 1);
            this.expandCollapseButton.Margin = new System.Windows.Forms.Padding(0);
            this.expandCollapseButton.Name = "expandCollapseButton";
            this.expandCollapseButton.Size = new System.Drawing.Size(30, 93);
            this.expandCollapseButton.TabIndex = 0;
            this.expandCollapseButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.expandCollapseButton.UseVisualStyleBackColor = true;
            this.expandCollapseButton.Click += new System.EventHandler(this.expandViewButton1_Click);
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
            // TopPanel
            // 
            this.TopPanel.AutoSize = true;
            this.TopPanel.Controls.Add(this.tableLayoutPanel1);
            this.TopPanel.Controls.Add(this.expandCollapseButton);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Padding = new System.Windows.Forms.Padding(1);
            this.TopPanel.Size = new System.Drawing.Size(800, 95);
            this.TopPanel.TabIndex = 1;
            this.TopPanel.TabStop = true;
            // 
            // downloadingNoticeLabel
            // 
            this.downloadingNoticeLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.downloadingNoticeLabel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.downloadingNoticeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.downloadingNoticeLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadingNoticeLabel.Location = new System.Drawing.Point(246, 96);
            this.downloadingNoticeLabel.Name = "downloadingNoticeLabel";
            this.downloadingNoticeLabel.Size = new System.Drawing.Size(277, 125);
            this.downloadingNoticeLabel.TabIndex = 2;
            this.downloadingNoticeLabel.Text = "Downloading PDF file. \r\nPlease wait...";
            this.downloadingNoticeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer1.Location = new System.Drawing.Point(0, 0);
            this.pdfViewer1.MinimumSize = new System.Drawing.Size(20, 20);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.ScriptErrorsSuppressed = true;
            this.pdfViewer1.Size = new System.Drawing.Size(792, 321);
            this.pdfViewer1.TabIndex = 1;
            // 
            // ProblemViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.TopPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ProblemViewer";
            this.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.ResumeLayout(false);
            this.pdfTab.ResumeLayout(false);
            this.submissionTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.submissionStatus)).EndInit();
            this.panel7.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.usernameList1)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage submissionTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip prevContext;
        private System.Windows.Forms.ContextMenuStrip nextContext;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripButton externalButton;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripButton codeButton;
        public System.Windows.Forms.ToolStripButton submitButton;
        public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton markButton;
        public System.Windows.Forms.ToolStripButton backButton;
        public System.Windows.Forms.ToolStripButton nextButton;
        public System.Windows.Forms.ToolStripButton reloadButton;
        public System.Windows.Forms.Label titleBox1;
        public System.Windows.Forms.TabControl tabControl1;
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button compareUserButton;
        private System.Windows.Forms.Panel TopPanel;
        public System.Windows.Forms.Button expandCollapseButton;
        private System.Windows.Forms.Label problemMessage;
        public System.Windows.Forms.Button up_downButton;
        private System.Windows.Forms.ToolStripButton tagsOrNoteToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton pdfToolButton;
        private System.Windows.Forms.ToolStripButton tagsEditorToolButton;
        public System.Windows.Forms.TabPage pdfTab;
        public Components.PDFViewer pdfViewer1;
        private System.Windows.Forms.Label downloadingNoticeLabel;
    }
}

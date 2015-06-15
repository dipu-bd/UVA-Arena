namespace UVA_Arena
{
    partial class ProblemListViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProblemListViewer));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.problemViewSplitContainer = new System.Windows.Forms.CustomSplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.filterBox1 = new UVA_Arena.Custom.SearchBox();
            this.button1 = new System.Windows.Forms.Button();
            this.problemListContainer = new System.Windows.Forms.TableLayoutPanel();
            this.problemListView = new BrightIdeasSoftware.FastObjectListView();
            this.pnumProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ptitleProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.levelProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dacuProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rtlProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.runProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.totalProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.acProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.waProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.priorityProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fileSizeProb = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.plistPanel = new System.Windows.Forms.Panel();
            this.plistLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.hideAccepted = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.searchBox1 = new UVA_Arena.Custom.SearchBox();
            this.deepSearchCheckBox = new System.Windows.Forms.CheckBox();
            this.cancelDeepSearchButton = new System.Windows.Forms.Button();
            this.problemViewSplitContainer.Panel1.SuspendLayout();
            this.problemViewSplitContainer.Panel2.SuspendLayout();
            this.problemViewSplitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.problemListContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.problemListView)).BeginInit();
            this.plistPanel.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "filefolder.png");
            this.imageList1.Images.SetKeyName(1, "folder.png");
            this.imageList1.Images.SetKeyName(2, "volumes.png");
            // 
            // problemViewSplitContainer
            // 
            this.problemViewSplitContainer.BackColor = System.Drawing.Color.PowderBlue;
            this.problemViewSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemViewSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.problemViewSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.problemViewSplitContainer.Name = "problemViewSplitContainer";
            // 
            // problemViewSplitContainer.Panel1
            // 
            this.problemViewSplitContainer.Panel1.BackColor = System.Drawing.Color.LightBlue;
            this.problemViewSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // problemViewSplitContainer.Panel2
            // 
            this.problemViewSplitContainer.Panel2.BackColor = System.Drawing.Color.LightBlue;
            this.problemViewSplitContainer.Panel2.Controls.Add(this.problemListContainer);
            this.problemViewSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.problemViewSplitContainer.Size = new System.Drawing.Size(507, 507);
            this.problemViewSplitContainer.SplitterDistance = 165;
            this.problemViewSplitContainer.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.filterBox1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(165, 507);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 34);
            this.label1.TabIndex = 10;
            this.label1.Text = "Categories";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filterBox1
            // 
            this.filterBox1.BackColor = System.Drawing.Color.Azure;
            this.filterBox1.ClearButtonVisible = true;
            this.filterBox1.CueText = "Filter...";
            this.filterBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterBox1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterBox1.Location = new System.Drawing.Point(3, 454);
            this.filterBox1.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.filterBox1.Name = "filterBox1";
            this.filterBox1.SearchButtonVisible = false;
            this.filterBox1.SearchText = "";
            this.filterBox1.Size = new System.Drawing.Size(161, 22);
            this.filterBox1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::UVA_Arena.Properties.Resources.reload;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(1, 478);
            this.button1.Margin = new System.Windows.Forms.Padding(1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Update List";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // problemListContainer
            // 
            this.problemListContainer.ColumnCount = 1;
            this.problemListContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.problemListContainer.Controls.Add(this.problemListView, 0, 1);
            this.problemListContainer.Controls.Add(this.plistPanel, 0, 0);
            this.problemListContainer.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.problemListContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemListContainer.Location = new System.Drawing.Point(0, 0);
            this.problemListContainer.Name = "problemListContainer";
            this.problemListContainer.RowCount = 3;
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.problemListContainer.Size = new System.Drawing.Size(338, 457);
            this.problemListContainer.TabIndex = 1;
            this.problemListContainer.Tag = "115";
            // 
            // problemListView
            // 
            this.problemListView.AllColumns.Add(this.pnumProb);
            this.problemListView.AllColumns.Add(this.ptitleProb);
            this.problemListView.AllColumns.Add(this.levelProb);
            this.problemListView.AllColumns.Add(this.dacuProb);
            this.problemListView.AllColumns.Add(this.rtlProb);
            this.problemListView.AllColumns.Add(this.runProb);
            this.problemListView.AllColumns.Add(this.totalProb);
            this.problemListView.AllColumns.Add(this.acProb);
            this.problemListView.AllColumns.Add(this.waProb);
            this.problemListView.AllColumns.Add(this.priorityProb);
            this.problemListView.AllColumns.Add(this.fileSizeProb);
            this.problemListView.AlternateRowBackColor = System.Drawing.Color.LightCyan;
            this.problemListView.BackColor = System.Drawing.Color.Azure;
            this.problemListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.problemListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.pnumProb,
            this.ptitleProb,
            this.levelProb,
            this.dacuProb,
            this.rtlProb,
            this.runProb,
            this.totalProb,
            this.acProb,
            this.waProb,
            this.priorityProb,
            this.fileSizeProb});
            this.problemListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.problemListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemListView.EmptyListMsg = "No problems in the list";
            this.problemListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.problemListView.ForeColor = System.Drawing.Color.Blue;
            this.problemListView.FullRowSelect = true;
            this.problemListView.HeaderUsesThemes = true;
            this.problemListView.HideSelection = false;
            this.problemListView.Location = new System.Drawing.Point(1, 37);
            this.problemListView.Margin = new System.Windows.Forms.Padding(1);
            this.problemListView.MultiSelect = false;
            this.problemListView.Name = "problemListView";
            this.problemListView.SelectAllOnControlA = false;
            this.problemListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.problemListView.ShowCommandMenuOnRightClick = true;
            this.problemListView.ShowGroups = false;
            this.problemListView.ShowItemCountOnGroups = true;
            this.problemListView.ShowItemToolTips = true;
            this.problemListView.Size = new System.Drawing.Size(336, 387);
            this.problemListView.TabIndex = 3;
            this.problemListView.UseAlternatingBackColors = true;
            this.problemListView.UseCellFormatEvents = true;
            this.problemListView.UseCompatibleStateImageBehavior = false;
            this.problemListView.UseCustomSelectionColors = true;
            this.problemListView.UseFiltering = true;
            this.problemListView.UseHotItem = true;
            this.problemListView.UseTranslucentHotItem = true;
            this.problemListView.UseTranslucentSelection = true;
            this.problemListView.View = System.Windows.Forms.View.Details;
            this.problemListView.VirtualMode = true;
            // 
            // pnumProb
            // 
            this.pnumProb.AspectName = "pnum";
            this.pnumProb.GroupWithItemCountFormat = "Volume {0:000} [{1} problems]";
            this.pnumProb.GroupWithItemCountSingularFormat = "Volume {0:000} [{1} problem]";
            this.pnumProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pnumProb.Text = "Number";
            this.pnumProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pnumProb.ToolTipText = "Problem number";
            // 
            // ptitleProb
            // 
            this.ptitleProb.AspectName = "ptitle";
            this.ptitleProb.Text = "Problem Title";
            this.ptitleProb.ToolTipText = "Problem title";
            this.ptitleProb.Width = 140;
            // 
            // levelProb
            // 
            this.levelProb.AspectName = "level";
            this.levelProb.AspectToStringFormat = "{0:0}";
            this.levelProb.GroupWithItemCountFormat = "Level {0:0} [{1} Problems]";
            this.levelProb.GroupWithItemCountSingularFormat = "Level {0:0} [{1} Problem]";
            this.levelProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.levelProb.Searchable = false;
            this.levelProb.Text = "Level";
            this.levelProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.levelProb.ToolTipText = "Hardness level of this problem";
            this.levelProb.UseFiltering = false;
            this.levelProb.Width = 50;
            // 
            // dacuProb
            // 
            this.dacuProb.AspectName = "dacu";
            this.dacuProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dacuProb.Searchable = false;
            this.dacuProb.Text = "DACU";
            this.dacuProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dacuProb.ToolTipText = "Distinct Accepted User";
            // 
            // rtlProb
            // 
            this.rtlProb.AspectName = "rtl";
            this.rtlProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rtlProb.Searchable = false;
            this.rtlProb.Text = "Time Limit";
            this.rtlProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rtlProb.ToolTipText = "Number of submission that got \"Time Limit Exceeded\"";
            this.rtlProb.Width = 80;
            // 
            // runProb
            // 
            this.runProb.AspectName = "run";
            this.runProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runProb.Searchable = false;
            this.runProb.Text = "Best";
            this.runProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runProb.ToolTipText = "Best Runtime";
            this.runProb.Width = 80;
            // 
            // totalProb
            // 
            this.totalProb.AspectName = "total";
            this.totalProb.Groupable = false;
            this.totalProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalProb.Searchable = false;
            this.totalProb.Text = "Total";
            this.totalProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalProb.ToolTipText = "Number of total submissions";
            this.totalProb.UseFiltering = false;
            // 
            // acProb
            // 
            this.acProb.AspectName = "ac";
            this.acProb.Groupable = false;
            this.acProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.acProb.Searchable = false;
            this.acProb.Text = "AC";
            this.acProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.acProb.ToolTipText = "Number of submission that got \"Accepted\"";
            this.acProb.UseFiltering = false;
            // 
            // waProb
            // 
            this.waProb.AspectName = "wa";
            this.waProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.waProb.Searchable = false;
            this.waProb.Text = "WA";
            this.waProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.waProb.ToolTipText = "Number of submission that got \"Wrong Answer\"";
            this.waProb.UseFiltering = false;
            // 
            // priorityProb
            // 
            this.priorityProb.AspectName = "priority";
            this.priorityProb.AspectToStringFormat = "{0} match";
            this.priorityProb.GroupWithItemCountFormat = "Problems with {0} matches [{1} items]";
            this.priorityProb.GroupWithItemCountSingularFormat = "Problem with {0} matches";
            this.priorityProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priorityProb.Searchable = false;
            this.priorityProb.Text = "Priority";
            this.priorityProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priorityProb.ToolTipText = "Match count used in deep search";
            this.priorityProb.UseFiltering = false;
            // 
            // fileSizeProb
            // 
            this.fileSizeProb.AspectName = "fileSize";
            this.fileSizeProb.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fileSizeProb.Text = "File Size";
            this.fileSizeProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // plistPanel
            // 
            this.plistPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.plistPanel.Controls.Add(this.plistLabel);
            this.plistPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plistPanel.Location = new System.Drawing.Point(0, 0);
            this.plistPanel.Margin = new System.Windows.Forms.Padding(0);
            this.plistPanel.Name = "plistPanel";
            this.plistPanel.Size = new System.Drawing.Size(338, 36);
            this.plistPanel.TabIndex = 4;
            // 
            // plistLabel
            // 
            this.plistLabel.BackColor = System.Drawing.Color.Transparent;
            this.plistLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plistLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plistLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plistLabel.ForeColor = System.Drawing.Color.Navy;
            this.plistLabel.Location = new System.Drawing.Point(0, 0);
            this.plistLabel.Name = "plistLabel";
            this.plistLabel.Size = new System.Drawing.Size(338, 36);
            this.plistLabel.TabIndex = 2;
            this.plistLabel.Text = "Problems";
            this.plistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.hideAccepted, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 425);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(338, 32);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::UVA_Arena.Properties.Resources.download;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.Location = new System.Drawing.Point(170, 1);
            this.button2.Margin = new System.Windows.Forms.Padding(1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 30);
            this.button2.TabIndex = 8;
            this.button2.Text = "Downloader";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // hideAccepted
            // 
            this.hideAccepted.Appearance = System.Windows.Forms.Appearance.Button;
            this.hideAccepted.AutoCheck = false;
            this.hideAccepted.AutoSize = true;
            this.hideAccepted.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hideAccepted.Location = new System.Drawing.Point(1, 1);
            this.hideAccepted.Margin = new System.Windows.Forms.Padding(1);
            this.hideAccepted.Name = "hideAccepted";
            this.hideAccepted.Size = new System.Drawing.Size(167, 30);
            this.hideAccepted.TabIndex = 6;
            this.hideAccepted.Tag = "Show Accepted|Hide Accepted";
            this.hideAccepted.Text = "Hide Accepted";
            this.hideAccepted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.hideAccepted.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.searchBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.deepSearchCheckBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cancelDeepSearchButton, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 457);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(338, 50);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // searchBox1
            // 
            this.searchBox1.BackColor = System.Drawing.Color.Azure;
            this.searchBox1.ClearButtonVisible = true;
            this.tableLayoutPanel2.SetColumnSpan(this.searchBox1, 2);
            this.searchBox1.CueText = "Search...";
            this.searchBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox1.Location = new System.Drawing.Point(1, 1);
            this.searchBox1.Margin = new System.Windows.Forms.Padding(1);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.SearchButtonVisible = false;
            this.searchBox1.SearchText = "";
            this.searchBox1.Size = new System.Drawing.Size(336, 24);
            this.searchBox1.TabIndex = 0;
            // 
            // deepSearchCheckBox
            // 
            this.deepSearchCheckBox.AutoSize = true;
            this.deepSearchCheckBox.Location = new System.Drawing.Point(6, 29);
            this.deepSearchCheckBox.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.deepSearchCheckBox.Name = "deepSearchCheckBox";
            this.deepSearchCheckBox.Size = new System.Drawing.Size(112, 17);
            this.deepSearchCheckBox.TabIndex = 5;
            this.deepSearchCheckBox.Text = "Use Deep Search";
            this.deepSearchCheckBox.UseVisualStyleBackColor = true;
            // 
            // cancelDeepSearchButton
            // 
            this.cancelDeepSearchButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelDeepSearchButton.Location = new System.Drawing.Point(205, 26);
            this.cancelDeepSearchButton.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cancelDeepSearchButton.Name = "cancelDeepSearchButton";
            this.cancelDeepSearchButton.Size = new System.Drawing.Size(130, 24);
            this.cancelDeepSearchButton.TabIndex = 6;
            this.cancelDeepSearchButton.Text = "Cancel";
            this.cancelDeepSearchButton.UseVisualStyleBackColor = true;
            this.cancelDeepSearchButton.Visible = false;
            // 
            // ProblemListViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.problemViewSplitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ProblemListViewer";
            this.Size = new System.Drawing.Size(507, 507);
            this.problemViewSplitContainer.Panel1.ResumeLayout(false);
            this.problemViewSplitContainer.Panel2.ResumeLayout(false);
            this.problemViewSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.problemListContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.problemListView)).EndInit();
            this.plistPanel.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomSplitContainer problemViewSplitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button2;
        private Custom.SearchBox filterBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel problemListContainer;
        private System.Windows.Forms.CheckBox hideAccepted;
        private System.Windows.Forms.Panel plistPanel;
        private System.Windows.Forms.Label plistLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Custom.SearchBox searchBox1;
        private System.Windows.Forms.CheckBox deepSearchCheckBox;
        private System.Windows.Forms.Button cancelDeepSearchButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        public BrightIdeasSoftware.FastObjectListView problemListView;
        private BrightIdeasSoftware.OLVColumn pnumProb;
        private BrightIdeasSoftware.OLVColumn ptitleProb;
        private BrightIdeasSoftware.OLVColumn levelProb;
        private BrightIdeasSoftware.OLVColumn dacuProb;
        private BrightIdeasSoftware.OLVColumn rtlProb;
        private BrightIdeasSoftware.OLVColumn runProb;
        private BrightIdeasSoftware.OLVColumn totalProb;
        private BrightIdeasSoftware.OLVColumn acProb;
        private BrightIdeasSoftware.OLVColumn waProb;
        private BrightIdeasSoftware.OLVColumn priorityProb;
        private BrightIdeasSoftware.OLVColumn fileSizeProb;

    }
}

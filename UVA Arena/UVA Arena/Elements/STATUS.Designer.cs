namespace UVA_Arena.Elements
{
    partial class STATUS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(STATUS));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.refreshToolButton = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.updateToolMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.updateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.autoUpdateToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.instantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoSecondsToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveSecondsToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenSecondsToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fifteenSecondsToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirtySecodsToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oneMinutesToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.twoMinutesToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fiveMinutesToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tenMinutesToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.submissionStatus = new BrightIdeasSoftware.FastObjectListView();
            this.sidSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.uidSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.unameSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fullnameSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pidSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pnumSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ptitleSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lanSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.verSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.runSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.memSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rankSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.subtimeSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.updateContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.submissionStatus)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolButton,
            this.Status1,
            this.updateToolMenu});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(790, 27);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // refreshToolButton
            // 
            this.refreshToolButton.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.refreshToolButton.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.refreshToolButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.refreshToolButton.Image = global::UVA_Arena.Properties.Resources.reload;
            this.refreshToolButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.refreshToolButton.Margin = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.refreshToolButton.Name = "refreshToolButton";
            this.refreshToolButton.Padding = new System.Windows.Forms.Padding(25, 3, 25, 3);
            this.refreshToolButton.Size = new System.Drawing.Size(116, 26);
            this.refreshToolButton.Text = "Refresh";
            this.refreshToolButton.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // Status1
            // 
            this.Status1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.Status1.Name = "Status1";
            this.Status1.Size = new System.Drawing.Size(599, 22);
            this.Status1.Spring = true;
            this.Status1.Text = "Status";
            this.Status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updateToolMenu
            // 
            this.updateToolMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.updateToolMenu.DropDown = this.updateContextMenu;
            this.updateToolMenu.Image = ((System.Drawing.Image)(resources.GetObject("updateToolMenu.Image")));
            this.updateToolMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateToolMenu.Name = "updateToolMenu";
            this.updateToolMenu.Size = new System.Drawing.Size(58, 25);
            this.updateToolMenu.Text = "Update";
            // 
            // updateContextMenu
            // 
            this.updateContextMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.updateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoUpdateToolMenu,
            this.toolStripSeparator1,
            this.instantToolStripMenuItem,
            this.twoSecondsToolItem,
            this.fiveSecondsToolItem,
            this.tenSecondsToolItem,
            this.fifteenSecondsToolItem,
            this.thirtySecodsToolItem,
            this.oneMinutesToolItem,
            this.twoMinutesToolItem,
            this.fiveMinutesToolItem,
            this.tenMinutesToolItem});
            this.updateContextMenu.Name = "contextMenuStrip1";
            this.updateContextMenu.OwnerItem = this.updateToolMenu;
            this.updateContextMenu.Size = new System.Drawing.Size(142, 252);
            // 
            // autoUpdateToolMenu
            // 
            this.autoUpdateToolMenu.Checked = true;
            this.autoUpdateToolMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoUpdateToolMenu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.autoUpdateToolMenu.ForeColor = System.Drawing.Color.Navy;
            this.autoUpdateToolMenu.Name = "autoUpdateToolMenu";
            this.autoUpdateToolMenu.Size = new System.Drawing.Size(141, 22);
            this.autoUpdateToolMenu.Text = "Auto Update";
            this.autoUpdateToolMenu.Click += new System.EventHandler(this.autoUpdateToolMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // instantToolStripMenuItem
            // 
            this.instantToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.instantToolStripMenuItem.Name = "instantToolStripMenuItem";
            this.instantToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.instantToolStripMenuItem.Tag = "500";
            this.instantToolStripMenuItem.Text = "Instant";
            this.instantToolStripMenuItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // twoSecondsToolItem
            // 
            this.twoSecondsToolItem.ForeColor = System.Drawing.Color.Black;
            this.twoSecondsToolItem.Name = "twoSecondsToolItem";
            this.twoSecondsToolItem.Size = new System.Drawing.Size(141, 22);
            this.twoSecondsToolItem.Tag = "2000";
            this.twoSecondsToolItem.Text = "2 seconds";
            this.twoSecondsToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // fiveSecondsToolItem
            // 
            this.fiveSecondsToolItem.Checked = true;
            this.fiveSecondsToolItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fiveSecondsToolItem.ForeColor = System.Drawing.Color.Black;
            this.fiveSecondsToolItem.Name = "fiveSecondsToolItem";
            this.fiveSecondsToolItem.Size = new System.Drawing.Size(141, 22);
            this.fiveSecondsToolItem.Tag = "5000";
            this.fiveSecondsToolItem.Text = "5 seconds";
            this.fiveSecondsToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // tenSecondsToolItem
            // 
            this.tenSecondsToolItem.ForeColor = System.Drawing.Color.Black;
            this.tenSecondsToolItem.Name = "tenSecondsToolItem";
            this.tenSecondsToolItem.Size = new System.Drawing.Size(141, 22);
            this.tenSecondsToolItem.Tag = "10000";
            this.tenSecondsToolItem.Text = "10 seconds";
            this.tenSecondsToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // fifteenSecondsToolItem
            // 
            this.fifteenSecondsToolItem.ForeColor = System.Drawing.Color.Black;
            this.fifteenSecondsToolItem.Name = "fifteenSecondsToolItem";
            this.fifteenSecondsToolItem.Size = new System.Drawing.Size(141, 22);
            this.fifteenSecondsToolItem.Tag = "15000";
            this.fifteenSecondsToolItem.Text = "15 seconds";
            this.fifteenSecondsToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // thirtySecodsToolItem
            // 
            this.thirtySecodsToolItem.ForeColor = System.Drawing.Color.Black;
            this.thirtySecodsToolItem.Name = "thirtySecodsToolItem";
            this.thirtySecodsToolItem.Size = new System.Drawing.Size(141, 22);
            this.thirtySecodsToolItem.Tag = "30000";
            this.thirtySecodsToolItem.Text = "30 seconds";
            this.thirtySecodsToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // oneMinutesToolItem
            // 
            this.oneMinutesToolItem.ForeColor = System.Drawing.Color.Black;
            this.oneMinutesToolItem.Name = "oneMinutesToolItem";
            this.oneMinutesToolItem.Size = new System.Drawing.Size(141, 22);
            this.oneMinutesToolItem.Tag = "60000";
            this.oneMinutesToolItem.Text = "1 minute";
            this.oneMinutesToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // twoMinutesToolItem
            // 
            this.twoMinutesToolItem.ForeColor = System.Drawing.Color.Black;
            this.twoMinutesToolItem.Name = "twoMinutesToolItem";
            this.twoMinutesToolItem.Size = new System.Drawing.Size(141, 22);
            this.twoMinutesToolItem.Tag = "120000";
            this.twoMinutesToolItem.Text = "2 minutes";
            this.twoMinutesToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // fiveMinutesToolItem
            // 
            this.fiveMinutesToolItem.ForeColor = System.Drawing.Color.Black;
            this.fiveMinutesToolItem.Name = "fiveMinutesToolItem";
            this.fiveMinutesToolItem.Size = new System.Drawing.Size(141, 22);
            this.fiveMinutesToolItem.Tag = "300000";
            this.fiveMinutesToolItem.Text = "5 minutes";
            this.fiveMinutesToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // tenMinutesToolItem
            // 
            this.tenMinutesToolItem.ForeColor = System.Drawing.Color.Black;
            this.tenMinutesToolItem.Name = "tenMinutesToolItem";
            this.tenMinutesToolItem.Size = new System.Drawing.Size(141, 22);
            this.tenMinutesToolItem.Tag = "600000";
            this.tenMinutesToolItem.Text = "10 minutes";
            this.tenMinutesToolItem.Click += new System.EventHandler(this.updateRateToolStripMenuItem2_Click);
            // 
            // submissionStatus
            // 
            this.submissionStatus.AllColumns.Add(this.sidSUB);
            this.submissionStatus.AllColumns.Add(this.uidSUB);
            this.submissionStatus.AllColumns.Add(this.unameSUB);
            this.submissionStatus.AllColumns.Add(this.fullnameSUB);
            this.submissionStatus.AllColumns.Add(this.pidSUB);
            this.submissionStatus.AllColumns.Add(this.pnumSUB);
            this.submissionStatus.AllColumns.Add(this.ptitleSUB);
            this.submissionStatus.AllColumns.Add(this.lanSUB);
            this.submissionStatus.AllColumns.Add(this.verSUB);
            this.submissionStatus.AllColumns.Add(this.runSUB);
            this.submissionStatus.AllColumns.Add(this.memSUB);
            this.submissionStatus.AllColumns.Add(this.rankSUB);
            this.submissionStatus.AllColumns.Add(this.subtimeSUB);
            this.submissionStatus.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.submissionStatus.BackColor = System.Drawing.Color.Azure;
            this.submissionStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.submissionStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sidSUB,
            this.unameSUB,
            this.fullnameSUB,
            this.pnumSUB,
            this.ptitleSUB,
            this.lanSUB,
            this.verSUB,
            this.runSUB,
            this.rankSUB,
            this.subtimeSUB});
            this.submissionStatus.CopySelectionOnControlC = false;
            this.submissionStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.submissionStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.submissionStatus.EmptyListMsg = "Refresh to view last submissions";
            this.submissionStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submissionStatus.ForeColor = System.Drawing.Color.Black;
            this.submissionStatus.FullRowSelect = true;
            this.submissionStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.submissionStatus.Location = new System.Drawing.Point(0, 35);
            this.submissionStatus.Name = "submissionStatus";
            this.submissionStatus.ShowGroups = false;
            this.submissionStatus.ShowItemToolTips = true;
            this.submissionStatus.Size = new System.Drawing.Size(790, 372);
            this.submissionStatus.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.submissionStatus.TabIndex = 1;
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
            this.sidSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sidSUB.Text = "SID";
            this.sidSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sidSUB.Width = 80;
            // 
            // uidSUB
            // 
            this.uidSUB.AspectName = "uid";
            this.uidSUB.DisplayIndex = 1;
            this.uidSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uidSUB.IsVisible = false;
            this.uidSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uidSUB.Width = 0;
            // 
            // unameSUB
            // 
            this.unameSUB.AspectName = "uname";
            this.unameSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unameSUB.Hyperlink = true;
            this.unameSUB.Text = "User";
            this.unameSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unameSUB.Width = 120;
            // 
            // fullnameSUB
            // 
            this.fullnameSUB.AspectName = "name";
            this.fullnameSUB.FillsFreeSpace = true;
            this.fullnameSUB.MinimumWidth = 150;
            this.fullnameSUB.Text = "Full Name";
            this.fullnameSUB.Width = 160;
            // 
            // pidSUB
            // 
            this.pidSUB.AspectName = "pid";
            this.pidSUB.DisplayIndex = 2;
            this.pidSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pidSUB.IsVisible = false;
            this.pidSUB.Text = "PID";
            this.pidSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pidSUB.Width = 0;
            // 
            // pnumSUB
            // 
            this.pnumSUB.AspectName = "pnum";
            this.pnumSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pnumSUB.Text = "Number";
            this.pnumSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pnumSUB.Width = 100;
            // 
            // ptitleSUB
            // 
            this.ptitleSUB.AspectName = "ptitle";
            this.ptitleSUB.FillsFreeSpace = true;
            this.ptitleSUB.Hyperlink = true;
            this.ptitleSUB.MinimumWidth = 150;
            this.ptitleSUB.Text = "Problem Name";
            this.ptitleSUB.Width = 180;
            // 
            // lanSUB
            // 
            this.lanSUB.AspectName = "lan";
            this.lanSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lanSUB.Text = "Language";
            this.lanSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lanSUB.Width = 90;
            // 
            // verSUB
            // 
            this.verSUB.AspectName = "ver";
            this.verSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.MinimumWidth = 100;
            this.verSUB.Text = "Verdict";
            this.verSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.Width = 140;
            // 
            // runSUB
            // 
            this.runSUB.AspectName = "run";
            this.runSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runSUB.Text = "Runtime";
            this.runSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.runSUB.Width = 80;
            // 
            // memSUB
            // 
            this.memSUB.AspectName = "mem";
            this.memSUB.DisplayIndex = 6;
            this.memSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.memSUB.IsVisible = false;
            this.memSUB.Text = "Memory";
            this.memSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.memSUB.Width = 0;
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
            this.subtimeSUB.FillsFreeSpace = true;
            this.subtimeSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.subtimeSUB.MinimumWidth = 140;
            this.subtimeSUB.Text = "Submission Time";
            this.subtimeSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.subtimeSUB.Width = 140;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 35);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Judge Status - Last Submissions";
            // 
            // timer1
            // 
            this.timer1.Interval = 800;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // STATUS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.Controls.Add(this.submissionStatus);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "STATUS";
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Size = new System.Drawing.Size(790, 434);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.updateContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.submissionStatus)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private BrightIdeasSoftware.OLVColumn sidSUB;
        private BrightIdeasSoftware.OLVColumn uidSUB;
        private BrightIdeasSoftware.OLVColumn pidSUB;
        private BrightIdeasSoftware.OLVColumn verSUB;
        private BrightIdeasSoftware.OLVColumn lanSUB;
        private BrightIdeasSoftware.OLVColumn runSUB;
        private BrightIdeasSoftware.OLVColumn memSUB;
        private BrightIdeasSoftware.OLVColumn rankSUB;
        private BrightIdeasSoftware.OLVColumn subtimeSUB;
        private BrightIdeasSoftware.OLVColumn fullnameSUB;
        private BrightIdeasSoftware.OLVColumn unameSUB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel Status1;
        private System.Windows.Forms.ToolStripStatusLabel refreshToolButton;
        private System.Windows.Forms.ToolStripDropDownButton updateToolMenu;
        private BrightIdeasSoftware.OLVColumn pnumSUB;
        private BrightIdeasSoftware.OLVColumn ptitleSUB;
        public BrightIdeasSoftware.FastObjectListView submissionStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem instantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem twoSecondsToolItem;
        private System.Windows.Forms.ToolStripMenuItem fiveSecondsToolItem;
        private System.Windows.Forms.ToolStripMenuItem tenSecondsToolItem;
        private System.Windows.Forms.ToolStripMenuItem fifteenSecondsToolItem;
        private System.Windows.Forms.ToolStripMenuItem thirtySecodsToolItem;
        private System.Windows.Forms.ToolStripMenuItem oneMinutesToolItem;
        private System.Windows.Forms.ToolStripMenuItem twoMinutesToolItem;
        private System.Windows.Forms.ToolStripMenuItem fiveMinutesToolItem;
        private System.Windows.Forms.ToolStripMenuItem tenMinutesToolItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem autoUpdateToolMenu;
        public System.Windows.Forms.ContextMenuStrip updateContextMenu;
    }
}

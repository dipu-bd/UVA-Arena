namespace UVA_Arena 
{
    partial class LoggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoggerForm));
            this.activityList = new BrightIdeasSoftware.FastObjectListView();
            this.dateTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.status = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.source = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cancelTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.forceStopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.gotoEndButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.downloadQueue1 = new BrightIdeasSoftware.FastObjectListView();
            this.nameINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.uriINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fileINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.percentINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.receiveINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.totalINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.priorityINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statINT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.taskQueue1 = new BrightIdeasSoftware.FastObjectListView();
            this.funcTask = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Status1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.activityList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.downloadQueue1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskQueue1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // activityList
            // 
            this.activityList.AllColumns.Add(this.dateTime);
            this.activityList.AllColumns.Add(this.status);
            this.activityList.AllColumns.Add(this.source);
            this.activityList.AlternateRowBackColor = System.Drawing.Color.SeaShell;
            this.activityList.BackColor = System.Drawing.Color.Azure;
            this.activityList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.activityList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dateTime,
            this.status,
            this.source});
            this.activityList.Cursor = System.Windows.Forms.Cursors.Default;
            this.activityList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityList.EmptyListMsg = "No Log Yet";
            this.activityList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activityList.ForeColor = System.Drawing.Color.Black;
            this.activityList.FullRowSelect = true;
            this.activityList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.activityList.Location = new System.Drawing.Point(0, 0);
            this.activityList.Name = "activityList";
            this.activityList.ShowGroups = false;
            this.activityList.ShowItemCountOnGroups = true;
            this.activityList.ShowItemToolTips = true;
            this.activityList.Size = new System.Drawing.Size(756, 345);
            this.activityList.TabIndex = 0;
            this.activityList.UseAlternatingBackColors = true;
            this.activityList.UseCellFormatEvents = true;
            this.activityList.UseCompatibleStateImageBehavior = false;
            this.activityList.UseCustomSelectionColors = true;
            this.activityList.UseHotItem = true;
            this.activityList.UseTranslucentHotItem = true;
            this.activityList.UseTranslucentSelection = true;
            this.activityList.View = System.Windows.Forms.View.Details;
            this.activityList.VirtualMode = true;
            this.activityList.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.objectListView1_FormatCell);
            // 
            // dateTime
            // 
            this.dateTime.AspectName = "time";
            this.dateTime.AspectToStringFormat = "{0:dd-MMM-yyyy, hh:mm:ss tt}";
            this.dateTime.Groupable = false;
            this.dateTime.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dateTime.Text = "Time";
            this.dateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dateTime.Width = 150;
            // 
            // status
            // 
            this.status.AspectName = "status";
            this.status.Sortable = false;
            this.status.Text = "Log Status";
            this.status.Width = 400;
            // 
            // source
            // 
            this.source.AspectName = "source";
            this.source.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.source.Sortable = false;
            this.source.Text = "Source";
            this.source.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.source.Width = 120;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelTaskToolStripMenuItem,
            this.toolStripSeparator1,
            this.forceStopToolStripMenuItem,
            this.forceStartToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(138, 76);
            // 
            // cancelTaskToolStripMenuItem
            // 
            this.cancelTaskToolStripMenuItem.Name = "cancelTaskToolStripMenuItem";
            this.cancelTaskToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.cancelTaskToolStripMenuItem.Text = "Cancel Task";
            this.cancelTaskToolStripMenuItem.Click += new System.EventHandler(this.cancelTaskToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // forceStopToolStripMenuItem
            // 
            this.forceStopToolStripMenuItem.Name = "forceStopToolStripMenuItem";
            this.forceStopToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.forceStopToolStripMenuItem.Text = "Force Stop";
            this.forceStopToolStripMenuItem.Click += new System.EventHandler(this.forceStopToolStripMenuItem_Click);
            // 
            // forceStartToolStripMenuItem
            // 
            this.forceStartToolStripMenuItem.Name = "forceStartToolStripMenuItem";
            this.forceStartToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.forceStartToolStripMenuItem.Text = "Force Start";
            this.forceStartToolStripMenuItem.Click += new System.EventHandler(this.forceStartToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(647, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gotoEndButton
            // 
            this.gotoEndButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gotoEndButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gotoEndButton.Location = new System.Drawing.Point(527, 3);
            this.gotoEndButton.Name = "gotoEndButton";
            this.gotoEndButton.Size = new System.Drawing.Size(114, 25);
            this.gotoEndButton.TabIndex = 2;
            this.gotoEndButton.Text = "Go to End";
            this.gotoEndButton.UseVisualStyleBackColor = true;
            this.gotoEndButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(50, 8);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 381);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.activityList);
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(756, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Activity Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.downloadQueue1);
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(756, 345);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Download Queue";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // downloadQueue1
            // 
            this.downloadQueue1.AllColumns.Add(this.nameINT);
            this.downloadQueue1.AllColumns.Add(this.uriINT);
            this.downloadQueue1.AllColumns.Add(this.fileINT);
            this.downloadQueue1.AllColumns.Add(this.percentINT);
            this.downloadQueue1.AllColumns.Add(this.receiveINT);
            this.downloadQueue1.AllColumns.Add(this.totalINT);
            this.downloadQueue1.AllColumns.Add(this.priorityINT);
            this.downloadQueue1.AllColumns.Add(this.statINT);
            this.downloadQueue1.AlternateRowBackColor = System.Drawing.Color.SeaShell;
            this.downloadQueue1.BackColor = System.Drawing.Color.Azure;
            this.downloadQueue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.downloadQueue1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameINT,
            this.uriINT,
            this.fileINT,
            this.percentINT,
            this.receiveINT,
            this.totalINT,
            this.priorityINT,
            this.statINT});
            this.downloadQueue1.ContextMenuStrip = this.contextMenuStrip1;
            this.downloadQueue1.Cursor = System.Windows.Forms.Cursors.Default;
            this.downloadQueue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.downloadQueue1.EmptyListMsg = "Queue is empty";
            this.downloadQueue1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadQueue1.ForeColor = System.Drawing.Color.Black;
            this.downloadQueue1.FullRowSelect = true;
            this.downloadQueue1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.downloadQueue1.Location = new System.Drawing.Point(0, 0);
            this.downloadQueue1.Name = "downloadQueue1";
            this.downloadQueue1.ShowGroups = false;
            this.downloadQueue1.ShowItemCountOnGroups = true;
            this.downloadQueue1.ShowItemToolTips = true;
            this.downloadQueue1.Size = new System.Drawing.Size(756, 345);
            this.downloadQueue1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.downloadQueue1.TabIndex = 2;
            this.downloadQueue1.UseAlternatingBackColors = true;
            this.downloadQueue1.UseCellFormatEvents = true;
            this.downloadQueue1.UseCompatibleStateImageBehavior = false;
            this.downloadQueue1.UseCustomSelectionColors = true;
            this.downloadQueue1.UseHotItem = true;
            this.downloadQueue1.UseTranslucentHotItem = true;
            this.downloadQueue1.UseTranslucentSelection = true;
            this.downloadQueue1.View = System.Windows.Forms.View.Details;
            this.downloadQueue1.VirtualMode = true;
            this.downloadQueue1.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.downloadQueue1_FormatCell);
            // 
            // nameINT
            // 
            this.nameINT.AspectName = "name";
            this.nameINT.Text = "Name";
            this.nameINT.Width = 100;
            // 
            // uriINT
            // 
            this.uriINT.AspectName = "Url";
            this.uriINT.Text = "Download From";
            this.uriINT.Width = 200;
            // 
            // fileINT
            // 
            this.fileINT.AspectName = "FileName";
            this.fileINT.Text = "Save To";
            this.fileINT.Width = 200;
            // 
            // percentINT
            // 
            this.percentINT.AspectName = "ProgressPercentage";
            this.percentINT.AspectToStringFormat = "{0}%";
            this.percentINT.Text = "Progress";
            this.percentINT.Width = 100;
            // 
            // receiveINT
            // 
            this.receiveINT.AspectName = "Received";
            this.receiveINT.Text = "Received";
            this.receiveINT.Width = 70;
            // 
            // totalINT
            // 
            this.totalINT.AspectName = "Total";
            this.totalINT.Text = "Total";
            this.totalINT.Width = 70;
            // 
            // priorityINT
            // 
            this.priorityINT.AspectName = "TaskPriority";
            this.priorityINT.Text = "Priority";
            // 
            // statINT
            // 
            this.statINT.AspectName = "Status";
            this.statINT.Text = "Status";
            this.statINT.Width = 80;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.taskQueue1);
            this.tabPage3.Location = new System.Drawing.Point(4, 32);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(756, 345);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pending Task";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // taskQueue1
            // 
            this.taskQueue1.AllColumns.Add(this.funcTask);
            this.taskQueue1.AlternateRowBackColor = System.Drawing.Color.SeaShell;
            this.taskQueue1.BackColor = System.Drawing.Color.Azure;
            this.taskQueue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.taskQueue1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.funcTask});
            this.taskQueue1.Cursor = System.Windows.Forms.Cursors.Default;
            this.taskQueue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskQueue1.EmptyListMsg = "Queue is empty";
            this.taskQueue1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskQueue1.ForeColor = System.Drawing.Color.Black;
            this.taskQueue1.FullRowSelect = true;
            this.taskQueue1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.taskQueue1.Location = new System.Drawing.Point(3, 3);
            this.taskQueue1.Name = "taskQueue1";
            this.taskQueue1.ShowGroups = false;
            this.taskQueue1.ShowItemCountOnGroups = true;
            this.taskQueue1.ShowItemToolTips = true;
            this.taskQueue1.Size = new System.Drawing.Size(750, 339);
            this.taskQueue1.TabIndex = 1;
            this.taskQueue1.UseAlternatingBackColors = true;
            this.taskQueue1.UseCellFormatEvents = true;
            this.taskQueue1.UseCompatibleStateImageBehavior = false;
            this.taskQueue1.UseCustomSelectionColors = true;
            this.taskQueue1.UseHotItem = true;
            this.taskQueue1.UseTranslucentHotItem = true;
            this.taskQueue1.UseTranslucentSelection = true;
            this.taskQueue1.View = System.Windows.Forms.View.Details;
            this.taskQueue1.VirtualMode = true;
            // 
            // funcTask
            // 
            this.funcTask.AspectName = "func";
            this.funcTask.Text = "Function";
            this.funcTask.Width = 300;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.gotoEndButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Status1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 381);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 31);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Status1
            // 
            this.Status1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Status1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status1.Location = new System.Drawing.Point(10, 1);
            this.Status1.Margin = new System.Windows.Forms.Padding(10, 1, 3, 1);
            this.Status1.Name = "Status1";
            this.Status1.Size = new System.Drawing.Size(511, 29);
            this.Status1.TabIndex = 3;
            this.Status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(764, 412);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoggerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Keeper";
            this.Load += new System.EventHandler(this.LoggerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.activityList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.downloadQueue1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskQueue1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.FastObjectListView activityList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button gotoEndButton;
        private BrightIdeasSoftware.OLVColumn dateTime;
        private BrightIdeasSoftware.OLVColumn source;
        private BrightIdeasSoftware.OLVColumn status;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage2;
        private BrightIdeasSoftware.FastObjectListView downloadQueue1;
        private BrightIdeasSoftware.OLVColumn nameINT;
        private BrightIdeasSoftware.OLVColumn uriINT;
        private BrightIdeasSoftware.OLVColumn fileINT;
        private BrightIdeasSoftware.OLVColumn percentINT;
        private BrightIdeasSoftware.OLVColumn receiveINT;
        private BrightIdeasSoftware.OLVColumn totalINT;
        private BrightIdeasSoftware.OLVColumn statINT;
        private BrightIdeasSoftware.OLVColumn priorityINT;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelTaskToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private BrightIdeasSoftware.FastObjectListView taskQueue1;
        private BrightIdeasSoftware.OLVColumn funcTask;
        private System.Windows.Forms.Label Status1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem forceStopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forceStartToolStripMenuItem;
    }
}
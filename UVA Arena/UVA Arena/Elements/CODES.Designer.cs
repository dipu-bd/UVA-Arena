namespace UVA_Arena.Elements
{
    partial class CODES
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CODES));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.CustomSplitContainer();
            this.problemListContainer = new System.Windows.Forms.TableLayoutPanel();
            this.searchBox1 = new UVA_Arena.Custom.SearchBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plistLabel = new System.Windows.Forms.Label();
            this.folderTree = new BrightIdeasSoftware.TreeListView();
            this.nameTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lengthTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fullnameTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.creationTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lastaccessTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.laswriteTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.attributeTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.largeImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.smallImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.problemListContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.folderTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.MintCream;
            this.splitContainer1.Panel1.Controls.Add(this.problemListContainer);
            this.splitContainer1.Size = new System.Drawing.Size(800, 400);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 0;
            // 
            // problemListContainer
            // 
            this.problemListContainer.ColumnCount = 1;
            this.problemListContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.problemListContainer.Controls.Add(this.searchBox1, 0, 2);
            this.problemListContainer.Controls.Add(this.panel1, 0, 0);
            this.problemListContainer.Controls.Add(this.folderTree, 0, 1);
            this.problemListContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemListContainer.Location = new System.Drawing.Point(0, 0);
            this.problemListContainer.Name = "problemListContainer";
            this.problemListContainer.RowCount = 3;
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.problemListContainer.Size = new System.Drawing.Size(250, 400);
            this.problemListContainer.TabIndex = 2;
            this.problemListContainer.Tag = "115";
            // 
            // searchBox1
            // 
            this.searchBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.searchBox1.ClearButton = true;
            this.searchBox1.CueText = "Search...";
            this.searchBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchBox1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox1.Location = new System.Drawing.Point(1, 373);
            this.searchBox1.Margin = new System.Windows.Forms.Padding(1);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.SearchButton = true;
            this.searchBox1.SearchText = "";
            this.searchBox1.Size = new System.Drawing.Size(248, 26);
            this.searchBox1.TabIndex = 0;
            this.searchBox1.SearchButtonClicked += new System.EventHandler<System.EventArgs>(this.searchBox1_SearchButtonClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plistLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 26);
            this.panel1.TabIndex = 4;
            // 
            // plistLabel
            // 
            this.plistLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plistLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plistLabel.ForeColor = System.Drawing.Color.Navy;
            this.plistLabel.Location = new System.Drawing.Point(0, 0);
            this.plistLabel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.plistLabel.Name = "plistLabel";
            this.plistLabel.Size = new System.Drawing.Size(248, 26);
            this.plistLabel.TabIndex = 2;
            this.plistLabel.Text = "Codes";
            this.plistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // folderTree
            // 
            this.folderTree.AllColumns.Add(this.nameTAB);
            this.folderTree.AllColumns.Add(this.lengthTAB);
            this.folderTree.AllColumns.Add(this.fullnameTAB);
            this.folderTree.AllColumns.Add(this.creationTAB);
            this.folderTree.AllColumns.Add(this.lastaccessTAB);
            this.folderTree.AllColumns.Add(this.laswriteTAB);
            this.folderTree.AllColumns.Add(this.attributeTAB);
            this.folderTree.AllowDrop = true;
            this.folderTree.BackColor = System.Drawing.Color.White;
            this.folderTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.folderTree.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.folderTree.CellEditTabChangesRows = true;
            this.folderTree.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameTAB,
            this.lengthTAB,
            this.fullnameTAB,
            this.creationTAB,
            this.lastaccessTAB,
            this.laswriteTAB,
            this.attributeTAB});
            this.folderTree.ContextMenuStrip = this.contextMenuStrip1;
            this.folderTree.Cursor = System.Windows.Forms.Cursors.Default;
            this.folderTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTree.EmptyListMsg = "No files or folders";
            this.folderTree.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderTree.LargeImageList = this.largeImageList1;
            this.folderTree.Location = new System.Drawing.Point(3, 31);
            this.folderTree.Name = "folderTree";
            this.folderTree.OwnerDraw = true;
            this.folderTree.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.folderTree.ShowCommandMenuOnRightClick = true;
            this.folderTree.ShowGroups = false;
            this.folderTree.ShowImagesOnSubItems = true;
            this.folderTree.ShowItemToolTips = true;
            this.folderTree.Size = new System.Drawing.Size(244, 338);
            this.folderTree.SmallImageList = this.smallImageList1;
            this.folderTree.TabIndex = 5;
            this.folderTree.UseCellFormatEvents = true;
            this.folderTree.UseCompatibleStateImageBehavior = false;
            this.folderTree.UseCustomSelectionColors = true;
            this.folderTree.UseFiltering = true;
            this.folderTree.UseHotItem = true;
            this.folderTree.UseTranslucentHotItem = true;
            this.folderTree.UseTranslucentSelection = true;
            this.folderTree.View = System.Windows.Forms.View.Details;
            this.folderTree.VirtualMode = true;
            this.folderTree.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.folderTree_CellEditFinishing);
            this.folderTree.CellEditValidating += new BrightIdeasSoftware.CellEditEventHandler(this.folderTree_CellEditValidating);
            // 
            // nameTAB
            // 
            this.nameTAB.AspectName = "Name";
            this.nameTAB.CellPadding = null;
            this.nameTAB.Text = "Name";
            this.nameTAB.Width = 250;
            // 
            // lengthTAB
            // 
            this.lengthTAB.AspectName = "Length";
            this.lengthTAB.CellPadding = null;
            this.lengthTAB.Text = "Size";
            this.lengthTAB.UseFiltering = false;
            // 
            // fullnameTAB
            // 
            this.fullnameTAB.AspectName = "FullName";
            this.fullnameTAB.CellPadding = null;
            this.fullnameTAB.Text = "Directory";
            this.fullnameTAB.UseFiltering = false;
            this.fullnameTAB.Width = 200;
            // 
            // creationTAB
            // 
            this.creationTAB.AspectName = "CreationTime";
            this.creationTAB.CellPadding = null;
            this.creationTAB.Text = "Creation Time";
            this.creationTAB.UseFiltering = false;
            this.creationTAB.Width = 130;
            // 
            // lastaccessTAB
            // 
            this.lastaccessTAB.AspectName = "LastAccessTime";
            this.lastaccessTAB.CellPadding = null;
            this.lastaccessTAB.Text = "Last Access Time";
            this.lastaccessTAB.UseFiltering = false;
            this.lastaccessTAB.Width = 130;
            // 
            // laswriteTAB
            // 
            this.laswriteTAB.AspectName = "LastWriteTime";
            this.laswriteTAB.CellPadding = null;
            this.laswriteTAB.Text = "Last Write Time";
            this.laswriteTAB.UseFiltering = false;
            this.laswriteTAB.Width = 130;
            // 
            // attributeTAB
            // 
            this.attributeTAB.AspectName = "Attributes";
            this.attributeTAB.CellPadding = null;
            this.attributeTAB.Text = "Attributes";
            this.attributeTAB.UseFiltering = false;
            this.attributeTAB.Width = 250;
            // 
            // largeImageList1
            // 
            this.largeImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList1.ImageStream")));
            this.largeImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList1.Images.SetKeyName(0, "folder");
            // 
            // smallImageList1
            // 
            this.smallImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList1.ImageStream")));
            this.smallImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList1.Images.SetKeyName(0, "folder");
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(154, 14);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.CharHeight = 14;
            this.fastColoredTextBox1.CharWidth = 8;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Location = new System.Drawing.Point(1, 20);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.Size = new System.Drawing.Size(544, 379);
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.Text = "fastColoredTextBox1";
            this.fastColoredTextBox1.Zoom = 100;
            // 
            // CODES
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CODES";
            this.Size = new System.Drawing.Size(800, 400);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.problemListContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.folderTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomSplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel problemListContainer;
        private Custom.SearchBox searchBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label plistLabel;
        private BrightIdeasSoftware.TreeListView folderTree;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BrightIdeasSoftware.OLVColumn nameTAB;
        private BrightIdeasSoftware.OLVColumn fullnameTAB;
        private BrightIdeasSoftware.OLVColumn creationTAB;
        private BrightIdeasSoftware.OLVColumn lastaccessTAB;
        private BrightIdeasSoftware.OLVColumn laswriteTAB;
        private BrightIdeasSoftware.OLVColumn attributeTAB;
        private BrightIdeasSoftware.OLVColumn lengthTAB;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ImageList smallImageList1;
        private System.Windows.Forms.ImageList largeImageList1;

    }
}

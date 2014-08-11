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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.CustomSplitContainer();
            this.problemListContainer = new System.Windows.Forms.TableLayoutPanel();
            this.searchBox1 = new UVA_Arena.Custom.SearchBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plistLabel = new System.Windows.Forms.Label();
            this.treeListView1 = new BrightIdeasSoftware.TreeListView();
            this.nameTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lengthTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.fullnameTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.creationTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lastaccessTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.laswriteTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.attributeTAB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.problemListContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
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
            this.problemListContainer.Controls.Add(this.button1, 0, 2);
            this.problemListContainer.Controls.Add(this.searchBox1, 0, 3);
            this.problemListContainer.Controls.Add(this.panel1, 0, 0);
            this.problemListContainer.Controls.Add(this.treeListView1, 0, 1);
            this.problemListContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemListContainer.Location = new System.Drawing.Point(0, 0);
            this.problemListContainer.Name = "problemListContainer";
            this.problemListContainer.RowCount = 4;
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.30029F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.69971F));
            this.problemListContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
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
            this.searchBox1.Location = new System.Drawing.Point(1, 372);
            this.searchBox1.Margin = new System.Windows.Forms.Padding(1);
            this.searchBox1.Name = "searchBox1";
            this.searchBox1.SearchButton = true;
            this.searchBox1.SearchText = "";
            this.searchBox1.Size = new System.Drawing.Size(248, 27);
            this.searchBox1.TabIndex = 0;
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
            // treeListView1
            // 
            this.treeListView1.AllColumns.Add(this.nameTAB);
            this.treeListView1.AllColumns.Add(this.lengthTAB);
            this.treeListView1.AllColumns.Add(this.fullnameTAB);
            this.treeListView1.AllColumns.Add(this.creationTAB);
            this.treeListView1.AllColumns.Add(this.lastaccessTAB);
            this.treeListView1.AllColumns.Add(this.laswriteTAB);
            this.treeListView1.AllColumns.Add(this.attributeTAB);
            this.treeListView1.AllowDrop = true;
            this.treeListView1.BackColor = System.Drawing.Color.AliceBlue;
            this.treeListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeListView1.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.treeListView1.CellEditTabChangesRows = true;
            this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameTAB,
            this.lengthTAB,
            this.fullnameTAB,
            this.creationTAB,
            this.lastaccessTAB,
            this.laswriteTAB,
            this.attributeTAB});
            this.treeListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListView1.EmptyListMsg = "No files or folders";
            this.treeListView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView1.Location = new System.Drawing.Point(3, 31);
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.OwnerDraw = true;
            this.treeListView1.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.treeListView1.ShowCommandMenuOnRightClick = true;
            this.treeListView1.ShowGroups = false;
            this.treeListView1.ShowImagesOnSubItems = true;
            this.treeListView1.ShowItemToolTips = true;
            this.treeListView1.Size = new System.Drawing.Size(244, 266);
            this.treeListView1.TabIndex = 5;
            this.treeListView1.UseCellFormatEvents = true;
            this.treeListView1.UseCompatibleStateImageBehavior = false;
            this.treeListView1.UseCustomSelectionColors = true;
            this.treeListView1.UseHotItem = true;
            this.treeListView1.UseTranslucentHotItem = true;
            this.treeListView1.UseTranslucentSelection = true;
            this.treeListView1.View = System.Windows.Forms.View.Details;
            this.treeListView1.VirtualMode = true;
            // 
            // nameTAB
            // 
            this.nameTAB.AspectName = "Name";
            this.nameTAB.CellPadding = null;
            this.nameTAB.Text = "Name";
            this.nameTAB.Width = 150;
            // 
            // lengthTAB
            // 
            this.lengthTAB.AspectName = "Length";
            this.lengthTAB.CellPadding = null;
            this.lengthTAB.Text = "Size";
            // 
            // fullnameTAB
            // 
            this.fullnameTAB.AspectName = "FullName";
            this.fullnameTAB.CellPadding = null;
            this.fullnameTAB.Text = "Directory";
            this.fullnameTAB.Width = 200;
            // 
            // creationTAB
            // 
            this.creationTAB.AspectName = "CreationTime";
            this.creationTAB.CellPadding = null;
            this.creationTAB.Text = "Creation Time";
            this.creationTAB.Width = 130;
            // 
            // lastaccessTAB
            // 
            this.lastaccessTAB.AspectName = "LastAccessTime";
            this.lastaccessTAB.CellPadding = null;
            this.lastaccessTAB.Text = "Last Access Time";
            this.lastaccessTAB.Width = 130;
            // 
            // laswriteTAB
            // 
            this.laswriteTAB.AspectName = "LastWriteTime";
            this.laswriteTAB.CellPadding = null;
            this.laswriteTAB.Text = "Last Write Time";
            this.laswriteTAB.Width = 130;
            // 
            // attributeTAB
            // 
            this.attributeTAB.AspectName = "Attributes";
            this.attributeTAB.CellPadding = null;
            this.attributeTAB.Text = "Attributes";
            this.attributeTAB.Width = 250;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DarkTurquoise;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkCyan;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 65);
            this.button1.TabIndex = 6;
            this.button1.Text = "Select a path for code files";
            this.button1.UseVisualStyleBackColor = false;
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.problemListContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomSplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel problemListContainer;
        private Custom.SearchBox searchBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label plistLabel;
        private BrightIdeasSoftware.TreeListView treeListView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private BrightIdeasSoftware.OLVColumn nameTAB;
        private BrightIdeasSoftware.OLVColumn fullnameTAB;
        private BrightIdeasSoftware.OLVColumn creationTAB;
        private BrightIdeasSoftware.OLVColumn lastaccessTAB;
        private BrightIdeasSoftware.OLVColumn laswriteTAB;
        private BrightIdeasSoftware.OLVColumn attributeTAB;
        private BrightIdeasSoftware.OLVColumn lengthTAB;
        private System.Windows.Forms.Button button1;

    }
}

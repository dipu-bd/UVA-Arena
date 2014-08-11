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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.titleBox1 = new System.Windows.Forms.TextBox();
            this.catagoryInfo = new System.Windows.Forms.CueTextBox();
            this.catagoryButton = new System.Windows.Forms.Button();
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
            this.expandViewButton1 = new System.Windows.Forms.ToolStripButton();
            this.reloadButton = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.prevContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nextContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(30, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(718, 396);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.AliceBlue;
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Controls.Add(this.toolStrip1);
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(710, 362);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Description";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 93);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(710, 269);
            this.webBrowser1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Controls.Add(this.titleBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.catagoryInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.catagoryButton, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(710, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // titleBox1
            // 
            this.titleBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.titleBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tableLayoutPanel1.SetColumnSpan(this.titleBox1, 2);
            this.titleBox1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBox1.ForeColor = System.Drawing.Color.Maroon;
            this.titleBox1.Location = new System.Drawing.Point(5, 3);
            this.titleBox1.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.titleBox1.Name = "titleBox1";
            this.titleBox1.Size = new System.Drawing.Size(702, 25);
            this.titleBox1.TabIndex = 0;
            this.titleBox1.TabStop = false;
            this.titleBox1.Text = "No problem selected";
            // 
            // catagoryInfo
            // 
            this.catagoryInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.catagoryInfo.BackColor = System.Drawing.Color.PaleTurquoise;
            this.catagoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.catagoryInfo.CueText = "Set tags to identify the type of the problem";
            this.catagoryInfo.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catagoryInfo.ForeColor = System.Drawing.Color.Black;
            this.catagoryInfo.Location = new System.Drawing.Point(3, 35);
            this.catagoryInfo.Name = "catagoryInfo";
            this.catagoryInfo.Size = new System.Drawing.Size(604, 16);
            this.catagoryInfo.TabIndex = 1;
            this.catagoryInfo.TabStop = false;
            this.catagoryInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.catagoryInfo_KeyDown);
            // 
            // catagoryButton
            // 
            this.catagoryButton.BackColor = System.Drawing.Color.PaleTurquoise;
            this.catagoryButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catagoryButton.FlatAppearance.BorderColor = System.Drawing.Color.Turquoise;
            this.catagoryButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumTurquoise;
            this.catagoryButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue;
            this.catagoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.catagoryButton.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catagoryButton.Location = new System.Drawing.Point(611, 31);
            this.catagoryButton.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.catagoryButton.Name = "catagoryButton";
            this.catagoryButton.Size = new System.Drawing.Size(98, 24);
            this.catagoryButton.TabIndex = 2;
            this.catagoryButton.Text = "Change";
            this.catagoryButton.UseVisualStyleBackColor = false;
            this.catagoryButton.Visible = false;
            this.catagoryButton.Click += new System.EventHandler(this.catagoryButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
            this.expandViewButton1,
            this.reloadButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 55);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
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
            // expandViewButton1
            // 
            this.expandViewButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.expandViewButton1.Image = global::UVA_Arena.Properties.Resources.expand;
            this.expandViewButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.expandViewButton1.Name = "expandViewButton1";
            this.expandViewButton1.Size = new System.Drawing.Size(49, 35);
            this.expandViewButton1.Text = "Expand";
            this.expandViewButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.expandViewButton1.Click += new System.EventHandler(this.expandViewButton1_Click);
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(710, 362);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Status";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 30);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(710, 362);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ranks";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 30);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(710, 362);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Compare";
            this.tabPage4.UseVisualStyleBackColor = true;
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
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "ProblemViewer";
            this.Size = new System.Drawing.Size(718, 396);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
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
        public System.Windows.Forms.ToolStripButton expandViewButton1;
        public System.Windows.Forms.ToolStripButton reloadButton;
        public System.Windows.Forms.TextBox titleBox1;
        public System.Windows.Forms.Button catagoryButton;
        public System.Windows.Forms.CueTextBox catagoryInfo;
        public System.Windows.Forms.WebBrowser webBrowser1;
    }
}

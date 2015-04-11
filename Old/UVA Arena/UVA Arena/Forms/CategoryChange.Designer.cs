namespace UVA_Arena 
{
    partial class CategoryChange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryChange));
            this.label1 = new System.Windows.Forms.Label();
            this.save_Button = new System.Windows.Forms.Button();
            this.cancel_Button = new System.Windows.Forms.Button();
            this.reload_All = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addNode1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameNode1 = new System.Windows.Forms.ToolStripButton();
            this.addChild1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.removeNode1 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameNode2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.addNode2 = new System.Windows.Forms.ToolStripMenuItem();
            this.addChild2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNode2 = new System.Windows.Forms.ToolStripMenuItem();
            this.nativeTreeView1 = new System.Windows.Forms.NativeTreeView();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Categories : ";
            // 
            // save_Button
            // 
            this.save_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.save_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.save_Button.ForeColor = System.Drawing.Color.Black;
            this.save_Button.Location = new System.Drawing.Point(397, 227);
            this.save_Button.Name = "save_Button";
            this.save_Button.Size = new System.Drawing.Size(75, 25);
            this.save_Button.TabIndex = 5;
            this.save_Button.Text = "Save";
            this.save_Button.UseVisualStyleBackColor = true;
            this.save_Button.Click += new System.EventHandler(this.save_Button_Click);
            // 
            // cancel_Button
            // 
            this.cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_Button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_Button.ForeColor = System.Drawing.Color.Black;
            this.cancel_Button.Location = new System.Drawing.Point(320, 227);
            this.cancel_Button.Name = "cancel_Button";
            this.cancel_Button.Size = new System.Drawing.Size(75, 25);
            this.cancel_Button.TabIndex = 6;
            this.cancel_Button.Text = "Cancel";
            this.cancel_Button.UseVisualStyleBackColor = true;
            this.cancel_Button.Click += new System.EventHandler(this.cancel_Button_Click);
            // 
            // reload_All
            // 
            this.reload_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reload_All.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reload_All.ForeColor = System.Drawing.Color.Black;
            this.reload_All.Location = new System.Drawing.Point(12, 227);
            this.reload_All.Name = "reload_All";
            this.reload_All.Size = new System.Drawing.Size(80, 25);
            this.reload_All.TabIndex = 8;
            this.reload_All.Text = "Reload All";
            this.reload_All.UseVisualStyleBackColor = true;
            this.reload_All.Click += new System.EventHandler(this.reload_All_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNode1,
            this.toolStripSeparator1,
            this.renameNode1,
            this.addChild1,
            this.toolStripSeparator3,
            this.removeNode1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(458, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addNode1
            // 
            this.addNode1.Image = global::UVA_Arena.Properties.Resources.add;
            this.addNode1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addNode1.Name = "addNode1";
            this.addNode1.Size = new System.Drawing.Size(81, 22);
            this.addNode1.Text = "Add Node";
            this.addNode1.Click += new System.EventHandler(this.addNode1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // renameNode1
            // 
            this.renameNode1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.renameNode1.Enabled = false;
            this.renameNode1.Image = ((System.Drawing.Image)(resources.GetObject("renameNode1.Image")));
            this.renameNode1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renameNode1.Name = "renameNode1";
            this.renameNode1.Size = new System.Drawing.Size(54, 22);
            this.renameNode1.Text = "Rename";
            this.renameNode1.Click += new System.EventHandler(this.renameNode1_Click);
            // 
            // addChild1
            // 
            this.addChild1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addChild1.Enabled = false;
            this.addChild1.Image = ((System.Drawing.Image)(resources.GetObject("addChild1.Image")));
            this.addChild1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addChild1.Name = "addChild1";
            this.addChild1.Size = new System.Drawing.Size(64, 22);
            this.addChild1.Text = "Add Child";
            this.addChild1.Click += new System.EventHandler(this.addChild1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // removeNode1
            // 
            this.removeNode1.Enabled = false;
            this.removeNode1.Image = global::UVA_Arena.Properties.Resources.clear;
            this.removeNode1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeNode1.Name = "removeNode1";
            this.removeNode1.Size = new System.Drawing.Size(96, 22);
            this.removeNode1.Text = "Remoe Node";
            this.removeNode1.Click += new System.EventHandler(this.removeNode1_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.nativeTreeView1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(12, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 190);
            this.panel1.TabIndex = 11;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameNode2,
            this.toolStripSeparator2,
            this.addNode2,
            this.addChild2,
            this.removeNode2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(206, 98);
            // 
            // renameNode2
            // 
            this.renameNode2.Enabled = false;
            this.renameNode2.Name = "renameNode2";
            this.renameNode2.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.renameNode2.Size = new System.Drawing.Size(220, 22);
            this.renameNode2.Text = "Rename";
            this.renameNode2.Click += new System.EventHandler(this.renameNode1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // addNode2
            // 
            this.addNode2.Image = global::UVA_Arena.Properties.Resources.add;
            this.addNode2.Name = "addNode2";
            this.addNode2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNode2.Size = new System.Drawing.Size(220, 22);
            this.addNode2.Text = "Add Node";
            this.addNode2.Click += new System.EventHandler(this.addNode1_Click);
            // 
            // addChild2
            // 
            this.addChild2.Enabled = false;
            this.addChild2.Name = "addChild2";
            this.addChild2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.addChild2.Size = new System.Drawing.Size(220, 22);
            this.addChild2.Text = "Add Child";
            this.addChild2.Click += new System.EventHandler(this.addChild1_Click);
            // 
            // removeNode2
            // 
            this.removeNode2.Enabled = false;
            this.removeNode2.Image = global::UVA_Arena.Properties.Resources.clear;
            this.removeNode2.Name = "removeNode2";
            this.removeNode2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.removeNode2.Size = new System.Drawing.Size(205, 22);
            this.removeNode2.Text = "Remove Node";
            this.removeNode2.Click += new System.EventHandler(this.removeNode1_Click);
            // 
            // nativeTreeView1
            // 
            this.nativeTreeView1.BackColor = System.Drawing.Color.Azure;
            this.nativeTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nativeTreeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.nativeTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nativeTreeView1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nativeTreeView1.ForeColor = System.Drawing.Color.Black;
            this.nativeTreeView1.LabelEdit = true;
            this.nativeTreeView1.Location = new System.Drawing.Point(0, 25);
            this.nativeTreeView1.Name = "nativeTreeView1";
            this.nativeTreeView1.Size = new System.Drawing.Size(458, 163);
            this.nativeTreeView1.TabIndex = 9;
            this.nativeTreeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.nativeTreeView1_AfterLabelEdit);
            this.nativeTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.nativeTreeView1_AfterSelect);
            // 
            // CategoryChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reload_All);
            this.Controls.Add(this.cancel_Button);
            this.Controls.Add(this.save_Button);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CategoryChange";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change Category";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button save_Button;
        private System.Windows.Forms.Button cancel_Button;
        private System.Windows.Forms.Button reload_All;
        private System.Windows.Forms.NativeTreeView nativeTreeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton addNode1;
        private System.Windows.Forms.ToolStripButton removeNode1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton renameNode1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem renameNode2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem addNode2;
        private System.Windows.Forms.ToolStripMenuItem addChild2;
        private System.Windows.Forms.ToolStripMenuItem removeNode2;
        private System.Windows.Forms.ToolStripButton addChild1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}
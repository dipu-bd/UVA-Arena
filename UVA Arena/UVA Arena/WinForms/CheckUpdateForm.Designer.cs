namespace UVA_Arena
{
    partial class CheckUpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckUpdateForm));
            this.label1 = new System.Windows.Forms.Label();
            this.curVersion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.newVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.updateLink = new System.Windows.Forms.LinkLabel();
            this.updateMessage = new System.Windows.Forms.TextBox();
            this.autoUpdateCheck = new System.Windows.Forms.CheckBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.checkUpdateButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Version :";
            // 
            // curVersion
            // 
            this.curVersion.BackColor = System.Drawing.Color.Transparent;
            this.curVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curVersion.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curVersion.ForeColor = System.Drawing.Color.Indigo;
            this.curVersion.Location = new System.Drawing.Point(118, 2);
            this.curVersion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.curVersion.Name = "curVersion";
            this.curVersion.Size = new System.Drawing.Size(339, 21);
            this.curVersion.TabIndex = 1;
            this.curVersion.Text = "label2";
            this.curVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.newVersion, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.curVersion, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.updateLink, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.updateMessage, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.autoUpdateCheck, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 39);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(460, 171);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(38, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Change Log:";
            // 
            // newVersion
            // 
            this.newVersion.BackColor = System.Drawing.Color.Transparent;
            this.newVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newVersion.Font = new System.Drawing.Font("Cambria", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newVersion.ForeColor = System.Drawing.Color.DarkRed;
            this.newVersion.Location = new System.Drawing.Point(118, 27);
            this.newVersion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newVersion.Name = "newVersion";
            this.newVersion.Size = new System.Drawing.Size(339, 21);
            this.newVersion.TabIndex = 3;
            this.newVersion.Text = "label4";
            this.newVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "New Version :";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(38, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Update link :";
            // 
            // updateLink
            // 
            this.updateLink.AutoSize = true;
            this.updateLink.BackColor = System.Drawing.Color.Transparent;
            this.updateLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateLink.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateLink.Location = new System.Drawing.Point(118, 52);
            this.updateLink.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.updateLink.Name = "updateLink";
            this.updateLink.Size = new System.Drawing.Size(339, 21);
            this.updateLink.TabIndex = 5;
            this.updateLink.TabStop = true;
            this.updateLink.Text = "linkLabel1";
            this.updateLink.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.updateLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.updateLink_LinkClicked);
            // 
            // updateMessage
            // 
            this.updateMessage.BackColor = System.Drawing.Color.LightCyan;
            this.updateMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updateMessage.ForeColor = System.Drawing.Color.Black;
            this.updateMessage.Location = new System.Drawing.Point(118, 78);
            this.updateMessage.Multiline = true;
            this.updateMessage.Name = "updateMessage";
            this.updateMessage.ReadOnly = true;
            this.updateMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.updateMessage.Size = new System.Drawing.Size(339, 65);
            this.updateMessage.TabIndex = 7;
            // 
            // autoUpdateCheck
            // 
            this.autoUpdateCheck.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.autoUpdateCheck.AutoSize = true;
            this.autoUpdateCheck.Checked = global::UVA_Arena.Properties.Settings.Default.CheckForUpdate;
            this.autoUpdateCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoUpdateCheck.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::UVA_Arena.Properties.Settings.Default, "CheckForUpdate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.autoUpdateCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoUpdateCheck.Location = new System.Drawing.Point(234, 147);
            this.autoUpdateCheck.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.autoUpdateCheck.Name = "autoUpdateCheck";
            this.autoUpdateCheck.Size = new System.Drawing.Size(223, 23);
            this.autoUpdateCheck.TabIndex = 8;
            this.autoUpdateCheck.Text = "Automatically check for updates";
            this.autoUpdateCheck.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(347, 225);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(125, 28);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.Location = new System.Drawing.Point(216, 225);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(125, 28);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Download";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label2.Location = new System.Drawing.Point(27, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(425, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "A new version has been released. Click Download to go to the link.";
            // 
            // checkUpdateButton
            // 
            this.checkUpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkUpdateButton.Location = new System.Drawing.Point(5, 225);
            this.checkUpdateButton.Name = "checkUpdateButton";
            this.checkUpdateButton.Size = new System.Drawing.Size(125, 28);
            this.checkUpdateButton.TabIndex = 4;
            this.checkUpdateButton.Text = "Check";
            this.checkUpdateButton.UseVisualStyleBackColor = true;
            this.checkUpdateButton.Click += new System.EventHandler(this.checkUpdateButton_Click);
            this.checkUpdateButton.MouseHover += new System.EventHandler(this.checkUpdateButton_MouseHover);
            // 
            // CheckUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BackgroundImage = global::UVA_Arena.Properties.Resources.backimg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.checkUpdateButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CheckUpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Update Arrived!!";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button checkUpdateButton;
        public System.Windows.Forms.Label curVersion;
        public System.Windows.Forms.Label newVersion;
        public System.Windows.Forms.TextBox updateMessage;
        public System.Windows.Forms.LinkLabel updateLink;
        private System.Windows.Forms.CheckBox autoUpdateCheck;
    }
}
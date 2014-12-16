namespace UVA_Arena.Elements
{
    partial class UserProgTracker
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
            this.subCounterPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.triednacLabel = new System.Windows.Forms.Label();
            this.acceptedLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.totalsubLabel = new System.Windows.Forms.Label();
            this.namePanel = new System.Windows.Forms.TableLayoutPanel();
            this.fullnameLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.useridLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.prevTabButton = new System.Windows.Forms.Button();
            this.nextTabButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.basicInfoTab = new System.Windows.Forms.TabPage();
            this.subPerDateTab = new System.Windows.Forms.TabPage();
            this.subPerVerTab = new System.Windows.Forms.TabPage();
            this.subPerLanTab = new System.Windows.Forms.TabPage();
            this.rankCloudTab = new System.Windows.Forms.TabPage();
            this.subCounterPanel.SuspendLayout();
            this.namePanel.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.basicInfoTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // subCounterPanel
            // 
            this.subCounterPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.subCounterPanel.BackColor = System.Drawing.Color.LightCyan;
            this.subCounterPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.subCounterPanel.ColumnCount = 3;
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.subCounterPanel.Controls.Add(this.label6, 2, 1);
            this.subCounterPanel.Controls.Add(this.label5, 1, 1);
            this.subCounterPanel.Controls.Add(this.triednacLabel, 2, 0);
            this.subCounterPanel.Controls.Add(this.acceptedLabel, 1, 0);
            this.subCounterPanel.Controls.Add(this.label1, 0, 1);
            this.subCounterPanel.Controls.Add(this.totalsubLabel, 0, 0);
            this.subCounterPanel.Location = new System.Drawing.Point(34, 121);
            this.subCounterPanel.Name = "subCounterPanel";
            this.subCounterPanel.RowCount = 2;
            this.subCounterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.subCounterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.subCounterPanel.Size = new System.Drawing.Size(588, 80);
            this.subCounterPanel.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(425, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tried But Unsolved";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(259, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Accepted";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // triednacLabel
            // 
            this.triednacLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.triednacLabel.AutoSize = true;
            this.triednacLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triednacLabel.ForeColor = System.Drawing.Color.SaddleBrown;
            this.triednacLabel.Location = new System.Drawing.Point(453, 6);
            this.triednacLabel.Name = "triednacLabel";
            this.triednacLabel.Size = new System.Drawing.Size(71, 37);
            this.triednacLabel.TabIndex = 3;
            this.triednacLabel.Text = "[0]";
            // 
            // acceptedLabel
            // 
            this.acceptedLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.acceptedLabel.AutoSize = true;
            this.acceptedLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptedLabel.ForeColor = System.Drawing.Color.Navy;
            this.acceptedLabel.Location = new System.Drawing.Point(257, 6);
            this.acceptedLabel.Name = "acceptedLabel";
            this.acceptedLabel.Size = new System.Drawing.Size(71, 37);
            this.acceptedLabel.TabIndex = 2;
            this.acceptedLabel.Text = "[0]";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Total Submission";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalsubLabel
            // 
            this.totalsubLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.totalsubLabel.AutoSize = true;
            this.totalsubLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalsubLabel.ForeColor = System.Drawing.Color.Maroon;
            this.totalsubLabel.Location = new System.Drawing.Point(62, 6);
            this.totalsubLabel.Name = "totalsubLabel";
            this.totalsubLabel.Size = new System.Drawing.Size(71, 37);
            this.totalsubLabel.TabIndex = 1;
            this.totalsubLabel.Text = "[0]";
            // 
            // namePanel
            // 
            this.namePanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.namePanel.BackColor = System.Drawing.Color.GhostWhite;
            this.namePanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.namePanel.ColumnCount = 2;
            this.namePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.namePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.namePanel.Controls.Add(this.fullnameLabel, 1, 2);
            this.namePanel.Controls.Add(this.usernameLabel, 1, 1);
            this.namePanel.Controls.Add(this.label9, 0, 0);
            this.namePanel.Controls.Add(this.label8, 0, 2);
            this.namePanel.Controls.Add(this.label7, 0, 1);
            this.namePanel.Controls.Add(this.useridLabel, 1, 0);
            this.namePanel.Location = new System.Drawing.Point(34, 14);
            this.namePanel.Name = "namePanel";
            this.namePanel.RowCount = 3;
            this.namePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.namePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.namePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.namePanel.Size = new System.Drawing.Size(588, 100);
            this.namePanel.TabIndex = 1;
            // 
            // fullnameLabel
            // 
            this.fullnameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.fullnameLabel.AutoSize = true;
            this.fullnameLabel.Font = new System.Drawing.Font("Cambria", 14.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fullnameLabel.Location = new System.Drawing.Point(125, 71);
            this.fullnameLabel.Name = "fullnameLabel";
            this.fullnameLabel.Size = new System.Drawing.Size(459, 23);
            this.fullnameLabel.TabIndex = 5;
            this.fullnameLabel.Text = "[ - ]";
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.usernameLabel.Location = new System.Drawing.Point(125, 38);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(459, 23);
            this.usernameLabel.TabIndex = 4;
            this.usernameLabel.Text = "[ - ]";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(3)));
            this.label9.ForeColor = System.Drawing.Color.Purple;
            this.label9.Location = new System.Drawing.Point(62, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 19);
            this.label9.TabIndex = 2;
            this.label9.Text = "Userid :";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(3)));
            this.label8.ForeColor = System.Drawing.Color.Purple;
            this.label8.Location = new System.Drawing.Point(37, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 19);
            this.label8.TabIndex = 1;
            this.label8.Text = "Full Name :";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(3)));
            this.label7.ForeColor = System.Drawing.Color.Purple;
            this.label7.Location = new System.Drawing.Point(39, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Username :";
            // 
            // useridLabel
            // 
            this.useridLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.useridLabel.AutoSize = true;
            this.useridLabel.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.useridLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.useridLabel.Location = new System.Drawing.Point(125, 6);
            this.useridLabel.Name = "useridLabel";
            this.useridLabel.Size = new System.Drawing.Size(459, 22);
            this.useridLabel.TabIndex = 3;
            this.useridLabel.Text = "[-]";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.LightCyan;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.prevTabButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.nextTabButton, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.tabControl1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.MinimumSize = new System.Drawing.Size(350, 300);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(735, 502);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // prevTabButton
            // 
            this.prevTabButton.BackColor = System.Drawing.Color.LightBlue;
            this.prevTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevTabButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkCyan;
            this.prevTabButton.FlatAppearance.BorderSize = 0;
            this.prevTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.prevTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue;
            this.prevTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevTabButton.Image = global::UVA_Arena.Properties.Resources.prev;
            this.prevTabButton.Location = new System.Drawing.Point(1, 1);
            this.prevTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.prevTabButton.Name = "prevTabButton";
            this.prevTabButton.Size = new System.Drawing.Size(30, 500);
            this.prevTabButton.TabIndex = 1;
            this.prevTabButton.UseVisualStyleBackColor = false;
            this.prevTabButton.Click += new System.EventHandler(this.prevTabButton_Click);
            // 
            // nextTabButton
            // 
            this.nextTabButton.BackColor = System.Drawing.Color.LightBlue;
            this.nextTabButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextTabButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkCyan;
            this.nextTabButton.FlatAppearance.BorderSize = 0;
            this.nextTabButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.nextTabButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PowderBlue;
            this.nextTabButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextTabButton.Image = global::UVA_Arena.Properties.Resources.next;
            this.nextTabButton.Location = new System.Drawing.Point(702, 1);
            this.nextTabButton.Margin = new System.Windows.Forms.Padding(0);
            this.nextTabButton.Name = "nextTabButton";
            this.nextTabButton.Size = new System.Drawing.Size(32, 500);
            this.nextTabButton.TabIndex = 0;
            this.nextTabButton.UseVisualStyleBackColor = false;
            this.nextTabButton.Click += new System.EventHandler(this.nextTabButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.basicInfoTab);
            this.tabControl1.Controls.Add(this.subPerDateTab);
            this.tabControl1.Controls.Add(this.subPerVerTab);
            this.tabControl1.Controls.Add(this.subPerLanTab);
            this.tabControl1.Controls.Add(this.rankCloudTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(35, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(16, 6);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(663, 494);
            this.tabControl1.TabIndex = 2;
            // 
            // basicInfoTab
            // 
            this.basicInfoTab.Controls.Add(this.namePanel);
            this.basicInfoTab.Controls.Add(this.subCounterPanel);
            this.basicInfoTab.Location = new System.Drawing.Point(4, 4);
            this.basicInfoTab.Name = "basicInfoTab";
            this.basicInfoTab.Size = new System.Drawing.Size(655, 462);
            this.basicInfoTab.TabIndex = 4;
            this.basicInfoTab.Text = "Basic Info";
            this.basicInfoTab.UseVisualStyleBackColor = true;
            // 
            // subPerDateTab
            // 
            this.subPerDateTab.BackColor = System.Drawing.Color.FloralWhite;
            this.subPerDateTab.Location = new System.Drawing.Point(4, 4);
            this.subPerDateTab.Name = "subPerDateTab";
            this.subPerDateTab.Size = new System.Drawing.Size(653, 460);
            this.subPerDateTab.TabIndex = 1;
            this.subPerDateTab.Text = "Submission over Date";
            // 
            // subPerVerTab
            // 
            this.subPerVerTab.BackColor = System.Drawing.Color.LightCyan;
            this.subPerVerTab.Location = new System.Drawing.Point(4, 4);
            this.subPerVerTab.Name = "subPerVerTab";
            this.subPerVerTab.Size = new System.Drawing.Size(653, 460);
            this.subPerVerTab.TabIndex = 2;
            this.subPerVerTab.Text = "Submission per Verdict";
            // 
            // subPerLanTab
            // 
            this.subPerLanTab.BackColor = System.Drawing.Color.AliceBlue;
            this.subPerLanTab.Location = new System.Drawing.Point(4, 4);
            this.subPerLanTab.Name = "subPerLanTab";
            this.subPerLanTab.Size = new System.Drawing.Size(653, 460);
            this.subPerLanTab.TabIndex = 0;
            this.subPerLanTab.Text = "Submission per Language";
            // 
            // rankCloudTab
            // 
            this.rankCloudTab.BackColor = System.Drawing.Color.Honeydew;
            this.rankCloudTab.Location = new System.Drawing.Point(4, 4);
            this.rankCloudTab.Name = "rankCloudTab";
            this.rankCloudTab.Size = new System.Drawing.Size(653, 460);
            this.rankCloudTab.TabIndex = 3;
            this.rankCloudTab.Text = "Rank Cloud";
            // 
            // UserProgTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.Controls.Add(this.tableLayoutPanel3);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "UserProgTracker";
            this.Size = new System.Drawing.Size(735, 502);
            this.subCounterPanel.ResumeLayout(false);
            this.subCounterPanel.PerformLayout();
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.basicInfoTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel subCounterPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalsubLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label triednacLabel;
        private System.Windows.Forms.Label acceptedLabel;
        private System.Windows.Forms.TableLayoutPanel namePanel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label fullnameLabel;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label useridLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button nextTabButton;
        private System.Windows.Forms.Button prevTabButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage subPerLanTab;
        private System.Windows.Forms.TabPage subPerDateTab;
        private System.Windows.Forms.TabPage subPerVerTab;
        private System.Windows.Forms.TabPage rankCloudTab;
        private System.Windows.Forms.TabPage basicInfoTab; 
    }
}

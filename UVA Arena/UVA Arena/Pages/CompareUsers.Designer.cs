namespace UVA_Arena.Elements
{
    partial class CompareUsers
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.secondUser = new System.Windows.Forms.ComboBox();
            this.firstUser = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.compareButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.secondsRank = new System.Windows.Forms.RadioButton();
            this.secondsSubs = new System.Windows.Forms.RadioButton();
            this.commonSubs = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.allsubRadio = new System.Windows.Forms.RadioButton();
            this.acceptedRadio = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.subCounterPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.acceptedLabel = new System.Windows.Forms.Label();
            this.triednacLabel = new System.Windows.Forms.Label();
            this.totalsubLabel = new System.Windows.Forms.Label();
            this.probInListLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lastSubmissions1 = new BrightIdeasSoftware.FastObjectListView();
            this.sidSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pnumSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ptitleSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.unameSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.nameSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lanSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.verSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.runSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.rankSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.subtimeSUB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.subCounterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastSubmissions1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.secondUser);
            this.panel1.Controls.Add(this.firstUser);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(737, 40);
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(340, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "and";
            // 
            // secondUser
            // 
            this.secondUser.BackColor = System.Drawing.Color.PaleTurquoise;
            this.secondUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondUser.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.secondUser.ForeColor = System.Drawing.Color.Brown;
            this.secondUser.FormattingEnabled = true;
            this.secondUser.Location = new System.Drawing.Point(388, 6);
            this.secondUser.Name = "secondUser";
            this.secondUser.Size = new System.Drawing.Size(160, 27);
            this.secondUser.TabIndex = 3;
            this.secondUser.SelectedIndexChanged += new System.EventHandler(this.user_SelectedIndexChanged);
            // 
            // firstUser
            // 
            this.firstUser.BackColor = System.Drawing.Color.PaleTurquoise;
            this.firstUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstUser.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.firstUser.ForeColor = System.Drawing.Color.Maroon;
            this.firstUser.FormattingEnabled = true;
            this.firstUser.Location = new System.Drawing.Point(175, 6);
            this.firstUser.Name = "firstUser";
            this.firstUser.Size = new System.Drawing.Size(160, 27);
            this.firstUser.TabIndex = 1;
            this.firstUser.SelectedIndexChanged += new System.EventHandler(this.user_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Compare between";
            // 
            // compareButton
            // 
            this.compareButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.compareButton.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.compareButton.ForeColor = System.Drawing.Color.Indigo;
            this.compareButton.Location = new System.Drawing.Point(40, 334);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(150, 30);
            this.compareButton.TabIndex = 4;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightBlue;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.compareButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox1.Location = new System.Drawing.Point(537, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 398);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comparison Tools";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(9, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Show submissions that-";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.PowderBlue;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.secondsRank, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.secondsSubs, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.commonSubs, 0, 0);
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 143);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(180, 180);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // secondsRank
            // 
            this.secondsRank.BackColor = System.Drawing.Color.Transparent;
            this.secondsRank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondsRank.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondsRank.Location = new System.Drawing.Point(4, 124);
            this.secondsRank.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.secondsRank.Name = "secondsRank";
            this.secondsRank.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.secondsRank.Size = new System.Drawing.Size(172, 58);
            this.secondsRank.TabIndex = 3;
            this.secondsRank.Tag = "{1} has better rank than {0}.";
            this.secondsRank.Text = "Second user has better rank than first user";
            this.secondsRank.UseVisualStyleBackColor = false;
            // 
            // secondsSubs
            // 
            this.secondsSubs.BackColor = System.Drawing.Color.Transparent;
            this.secondsSubs.Checked = true;
            this.secondsSubs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondsSubs.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondsSubs.Location = new System.Drawing.Point(4, 63);
            this.secondsSubs.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.secondsSubs.Name = "secondsSubs";
            this.secondsSubs.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.secondsSubs.Size = new System.Drawing.Size(172, 58);
            this.secondsSubs.TabIndex = 2;
            this.secondsSubs.TabStop = true;
            this.secondsSubs.Tag = "{1} has but {0} do not.";
            this.secondsSubs.Text = "Second user has but first user do not.";
            this.secondsSubs.UseVisualStyleBackColor = false;
            // 
            // commonSubs
            // 
            this.commonSubs.BackColor = System.Drawing.Color.Transparent;
            this.commonSubs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commonSubs.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commonSubs.Location = new System.Drawing.Point(4, 2);
            this.commonSubs.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.commonSubs.Name = "commonSubs";
            this.commonSubs.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.commonSubs.Size = new System.Drawing.Size(172, 58);
            this.commonSubs.TabIndex = 1;
            this.commonSubs.Tag = "Both {0} and {1} have in common.";
            this.commonSubs.Text = "Both first and second user have in common.";
            this.commonSubs.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.PowderBlue;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.allsubRadio, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.acceptedRadio, 0, 0);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(180, 60);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // allsubRadio
            // 
            this.allsubRadio.AutoSize = true;
            this.allsubRadio.BackColor = System.Drawing.Color.Transparent;
            this.allsubRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allsubRadio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allsubRadio.Location = new System.Drawing.Point(4, 33);
            this.allsubRadio.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.allsubRadio.Name = "allsubRadio";
            this.allsubRadio.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.allsubRadio.Size = new System.Drawing.Size(172, 28);
            this.allsubRadio.TabIndex = 2;
            this.allsubRadio.Text = "All Submissions";
            this.allsubRadio.UseVisualStyleBackColor = false;
            // 
            // acceptedRadio
            // 
            this.acceptedRadio.AutoSize = true;
            this.acceptedRadio.BackColor = System.Drawing.Color.Transparent;
            this.acceptedRadio.Checked = true;
            this.acceptedRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acceptedRadio.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptedRadio.Location = new System.Drawing.Point(4, 2);
            this.acceptedRadio.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.acceptedRadio.Name = "acceptedRadio";
            this.acceptedRadio.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.acceptedRadio.Size = new System.Drawing.Size(172, 28);
            this.acceptedRadio.TabIndex = 1;
            this.acceptedRadio.TabStop = true;
            this.acceptedRadio.Text = "Accepted Problems";
            this.acceptedRadio.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 19);
            this.label4.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Compare with-";
            // 
            // subCounterPanel
            // 
            this.subCounterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.subCounterPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.subCounterPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.subCounterPanel.ColumnCount = 4;
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.99999F));
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.subCounterPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.subCounterPanel.Controls.Add(this.label8, 1, 1);
            this.subCounterPanel.Controls.Add(this.label11, 2, 1);
            this.subCounterPanel.Controls.Add(this.acceptedLabel, 1, 0);
            this.subCounterPanel.Controls.Add(this.triednacLabel, 2, 0);
            this.subCounterPanel.Controls.Add(this.totalsubLabel, 3, 0);
            this.subCounterPanel.Controls.Add(this.probInListLabel, 0, 0);
            this.subCounterPanel.Controls.Add(this.label7, 0, 1);
            this.subCounterPanel.Controls.Add(this.label6, 3, 1);
            this.subCounterPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.subCounterPanel.Location = new System.Drawing.Point(0, 358);
            this.subCounterPanel.Name = "subCounterPanel";
            this.subCounterPanel.RowCount = 2;
            this.subCounterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.subCounterPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.subCounterPanel.Size = new System.Drawing.Size(537, 80);
            this.subCounterPanel.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(167, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "Accepted";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(301, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 19);
            this.label11.TabIndex = 4;
            this.label11.Text = "Unsolved";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // acceptedLabel
            // 
            this.acceptedLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.acceptedLabel.AutoSize = true;
            this.acceptedLabel.BackColor = System.Drawing.Color.Transparent;
            this.acceptedLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptedLabel.ForeColor = System.Drawing.Color.Navy;
            this.acceptedLabel.Location = new System.Drawing.Point(147, 8);
            this.acceptedLabel.Name = "acceptedLabel";
            this.acceptedLabel.Size = new System.Drawing.Size(107, 37);
            this.acceptedLabel.TabIndex = 3;
            this.acceptedLabel.Tag = "{0}|{1}";
            this.acceptedLabel.Text = "[0|0]";
            // 
            // triednacLabel
            // 
            this.triednacLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.triednacLabel.AutoSize = true;
            this.triednacLabel.BackColor = System.Drawing.Color.Transparent;
            this.triednacLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.triednacLabel.ForeColor = System.Drawing.Color.Sienna;
            this.triednacLabel.Location = new System.Drawing.Point(281, 8);
            this.triednacLabel.Name = "triednacLabel";
            this.triednacLabel.Size = new System.Drawing.Size(107, 37);
            this.triednacLabel.TabIndex = 5;
            this.triednacLabel.Tag = "{0}|{1}";
            this.triednacLabel.Text = "[0|0]";
            // 
            // totalsubLabel
            // 
            this.totalsubLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.totalsubLabel.AutoSize = true;
            this.totalsubLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalsubLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalsubLabel.ForeColor = System.Drawing.Color.Maroon;
            this.totalsubLabel.Location = new System.Drawing.Point(415, 8);
            this.totalsubLabel.Name = "totalsubLabel";
            this.totalsubLabel.Size = new System.Drawing.Size(107, 37);
            this.totalsubLabel.TabIndex = 7;
            this.totalsubLabel.Tag = "{0}|{1}";
            this.totalsubLabel.Text = "[0|0]";
            // 
            // probInListLabel
            // 
            this.probInListLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.probInListLabel.AutoSize = true;
            this.probInListLabel.BackColor = System.Drawing.Color.Transparent;
            this.probInListLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.probInListLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.probInListLabel.Location = new System.Drawing.Point(31, 8);
            this.probInListLabel.Name = "probInListLabel";
            this.probInListLabel.Size = new System.Drawing.Size(71, 37);
            this.probInListLabel.TabIndex = 1;
            this.probInListLabel.Tag = "{0}";
            this.probInListLabel.Text = "[0]";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Problem In List";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(411, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "Total Submission";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lastSubmissions1
            // 
            this.lastSubmissions1.AllColumns.Add(this.sidSUB);
            this.lastSubmissions1.AllColumns.Add(this.pnumSUB);
            this.lastSubmissions1.AllColumns.Add(this.ptitleSUB);
            this.lastSubmissions1.AllColumns.Add(this.unameSUB);
            this.lastSubmissions1.AllColumns.Add(this.nameSUB);
            this.lastSubmissions1.AllColumns.Add(this.lanSUB);
            this.lastSubmissions1.AllColumns.Add(this.verSUB);
            this.lastSubmissions1.AllColumns.Add(this.runSUB);
            this.lastSubmissions1.AllColumns.Add(this.rankSUB);
            this.lastSubmissions1.AllColumns.Add(this.subtimeSUB);
            this.lastSubmissions1.BackColor = System.Drawing.Color.MintCream;
            this.lastSubmissions1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lastSubmissions1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sidSUB,
            this.pnumSUB,
            this.ptitleSUB,
            this.unameSUB,
            this.lanSUB,
            this.verSUB,
            this.runSUB,
            this.rankSUB,
            this.subtimeSUB});
            this.lastSubmissions1.Cursor = System.Windows.Forms.Cursors.Default;
            this.lastSubmissions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lastSubmissions1.EmptyListMsg = "List is empty";
            this.lastSubmissions1.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastSubmissions1.ForeColor = System.Drawing.Color.Black;
            this.lastSubmissions1.FullRowSelect = true;
            this.lastSubmissions1.HasCollapsibleGroups = false;
            this.lastSubmissions1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lastSubmissions1.Location = new System.Drawing.Point(0, 40);
            this.lastSubmissions1.Name = "lastSubmissions1";
            this.lastSubmissions1.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lastSubmissions1.ShowCommandMenuOnRightClick = true;
            this.lastSubmissions1.ShowGroups = false;
            this.lastSubmissions1.Size = new System.Drawing.Size(537, 318);
            this.lastSubmissions1.TabIndex = 1;
            this.lastSubmissions1.UseCellFormatEvents = true;
            this.lastSubmissions1.UseCompatibleStateImageBehavior = false;
            this.lastSubmissions1.UseCustomSelectionColors = true;
            this.lastSubmissions1.UseHyperlinks = true;
            this.lastSubmissions1.UseTranslucentSelection = true;
            this.lastSubmissions1.View = System.Windows.Forms.View.Details;
            this.lastSubmissions1.VirtualMode = true;
            this.lastSubmissions1.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.lastSubmissions1_FormatCell);
            this.lastSubmissions1.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.lastSubmissions1_HyperlinkClicked);
            // 
            // sidSUB
            // 
            this.sidSUB.AspectName = "sid";
            this.sidSUB.IsVisible = false;
            this.sidSUB.Text = "SID";
            this.sidSUB.Width = 0;
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
            this.ptitleSUB.Hyperlink = true;
            this.ptitleSUB.MinimumWidth = 150;
            this.ptitleSUB.Text = "Problem Name";
            this.ptitleSUB.Width = 180;
            // 
            // unameSUB
            // 
            this.unameSUB.AspectName = "uname";
            this.unameSUB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.unameSUB.MinimumWidth = 80;
            this.unameSUB.Text = "Username";
            this.unameSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.unameSUB.Width = 140;
            // 
            // nameSUB
            // 
            this.nameSUB.AspectName = "name";
            this.nameSUB.DisplayIndex = 2;
            this.nameSUB.IsVisible = false;
            this.nameSUB.MinimumWidth = 120;
            this.nameSUB.Text = "Full Name";
            this.nameSUB.Width = 120;
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
            this.verSUB.Text = "Verdict";
            this.verSUB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.verSUB.Width = 150;
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
            this.subtimeSUB.Width = 140;
            // 
            // CompareUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.Controls.Add(this.lastSubmissions1);
            this.Controls.Add(this.subCounterPanel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "CompareUsers";
            this.Size = new System.Drawing.Size(737, 438);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.subCounterPanel.ResumeLayout(false);
            this.subCounterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastSubmissions1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox secondUser;
        private System.Windows.Forms.ComboBox firstUser;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton allsubRadio;
        private System.Windows.Forms.RadioButton acceptedRadio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton secondsSubs;
        private System.Windows.Forms.RadioButton commonSubs;
        private System.Windows.Forms.RadioButton secondsRank;
        private System.Windows.Forms.TableLayoutPanel subCounterPanel;
        private System.Windows.Forms.Label acceptedLabel;
        private System.Windows.Forms.Label triednacLabel;
        private System.Windows.Forms.Label probInListLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label totalsubLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public BrightIdeasSoftware.FastObjectListView lastSubmissions1;
        private BrightIdeasSoftware.OLVColumn pnumSUB;
        private BrightIdeasSoftware.OLVColumn ptitleSUB;
        private BrightIdeasSoftware.OLVColumn lanSUB;
        private BrightIdeasSoftware.OLVColumn verSUB;
        private BrightIdeasSoftware.OLVColumn runSUB;
        private BrightIdeasSoftware.OLVColumn rankSUB;
        private BrightIdeasSoftware.OLVColumn subtimeSUB;
        private BrightIdeasSoftware.OLVColumn sidSUB;
        private BrightIdeasSoftware.OLVColumn unameSUB;
        private BrightIdeasSoftware.OLVColumn nameSUB;
    }
}

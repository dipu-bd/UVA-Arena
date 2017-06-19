namespace UVA_Arena
{
    partial class DownloadAllForm
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
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (remaining != null) remaining.Clear(); 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadAllForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.restartButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.totalPercentage = new System.Windows.Forms.Label();
            this.currentPercentage = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.currentProblem = new System.Windows.Forms.Label();
            this.replaceBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.overallStatus = new System.Windows.Forms.Label();
            //this.replaceCombo1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.Controls.Add(this.restartButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cancelButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.startButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(222, 147);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 32);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // restartButton
            // 
            this.restartButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restartButton.Location = new System.Drawing.Point(1, 1);
            this.restartButton.Margin = new System.Windows.Forms.Padding(1);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(97, 30);
            this.restartButton.TabIndex = 2;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(202, 1);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(97, 30);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Close";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(100, 1);
            this.startButton.Margin = new System.Windows.Forms.Padding(1);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 30);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Resume";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Overall Progress :";
            // 
            // totalProgress
            // 
            this.totalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProgress.Location = new System.Drawing.Point(12, 30);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(510, 30);
            this.totalProgress.TabIndex = 2;
            // 
            // currentProgress
            // 
            this.currentProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentProgress.Location = new System.Drawing.Point(11, 93);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(510, 20);
            this.currentProgress.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current Progress :";
            // 
            // totalPercentage
            // 
            this.totalPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalPercentage.BackColor = System.Drawing.Color.Transparent;
            this.totalPercentage.Location = new System.Drawing.Point(463, 11);
            this.totalPercentage.Name = "totalPercentage";
            this.totalPercentage.Size = new System.Drawing.Size(59, 16);
            this.totalPercentage.TabIndex = 5;
            this.totalPercentage.Text = "0%";
            this.totalPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // currentPercentage
            // 
            this.currentPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPercentage.BackColor = System.Drawing.Color.Transparent;
            this.currentPercentage.Location = new System.Drawing.Point(463, 74);
            this.currentPercentage.Name = "currentPercentage";
            this.currentPercentage.Size = new System.Drawing.Size(59, 16);
            this.currentPercentage.TabIndex = 6;
            this.currentPercentage.Text = "0%";
            this.currentPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.Location = new System.Drawing.Point(12, 116);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(510, 13);
            this.StatusLabel.TabIndex = 7;
            this.StatusLabel.Text = "no status...";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // currentProblem
            // 
            this.currentProblem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentProblem.AutoSize = true;
            this.currentProblem.BackColor = System.Drawing.Color.Transparent;
            this.currentProblem.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentProblem.Location = new System.Drawing.Point(136, 74);
            this.currentProblem.Name = "currentProblem";
            this.currentProblem.Size = new System.Drawing.Size(64, 15);
            this.currentProblem.TabIndex = 8;
            this.currentProblem.Text = "no status...";
            this.currentProblem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // replaceBox
            // 
            this.replaceBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.replaceBox.AutoCheck = false;
            this.replaceBox.AutoSize = true;
            this.replaceBox.BackColor = System.Drawing.Color.Transparent;
            this.replaceBox.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceBox.Location = new System.Drawing.Point(13, 154);
            this.replaceBox.Name = "replaceBox";
            this.replaceBox.Size = new System.Drawing.Size(100, 21);
            this.replaceBox.TabIndex = 9;
            this.replaceBox.Text = "Redownload";
            this.replaceBox.UseVisualStyleBackColor = false;
            this.replaceBox.Click += new System.EventHandler(this.replaceBox_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // overallStatus
            // 
            this.overallStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overallStatus.AutoSize = true;
            this.overallStatus.BackColor = System.Drawing.Color.Transparent;
            this.overallStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overallStatus.Location = new System.Drawing.Point(132, 11);
            this.overallStatus.Name = "overallStatus";
            this.overallStatus.Size = new System.Drawing.Size(64, 15);
            this.overallStatus.TabIndex = 10;
            this.overallStatus.Text = "no status...";
            this.overallStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;            
            /*
            // 
            // replaceCombo1
            // 
            this.replaceCombo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.replaceCombo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.replaceCombo1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.replaceCombo1.FormattingEnabled = true;
            this.replaceCombo1.Items.AddRange(new object[] {
            "All Files",
            "HTML Files",
            "PDF Files",
            "Image Files"});
            this.replaceCombo1.Location = new System.Drawing.Point(109, 151);
            this.replaceCombo1.Name = "replaceCombo1";
            this.replaceCombo1.Size = new System.Drawing.Size(98, 25);
            this.replaceCombo1.TabIndex = 11;
            this.replaceCombo1.SelectedIndexChanged += new System.EventHandler(this.replaceCombo1_SelectedIndexChanged);
            */
            // 
            // DownloadAllForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::UVA_Arena.Properties.Resources.backimg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(534, 191);
            //this.Controls.Add(this.replaceCombo1);
            this.Controls.Add(this.overallStatus);
            this.Controls.Add(this.replaceBox);
            this.Controls.Add(this.currentProblem);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.currentPercentage);
            this.Controls.Add(this.totalPercentage);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DownloadAllForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Downloading Problems";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadAllForm_FormClosing);
            this.Load += new System.EventHandler(this.DownloadAllForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar totalProgress;
        private System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label totalPercentage;
        private System.Windows.Forms.Label currentPercentage;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label currentProblem;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.CheckBox replaceBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label overallStatus;
        //private System.Windows.Forms.ComboBox replaceCombo1;

    }
}
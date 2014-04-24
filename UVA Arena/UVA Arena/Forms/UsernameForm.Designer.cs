namespace UVA_Arena
{
    partial class UsernameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsernameForm));
            this.label1 = new System.Windows.Forms.Label();
            this.set_button1 = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.Label();
            this.cancel_button2 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.username1 = new System.Windows.Forms.CueTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Give your username-";
            // 
            // set_button1
            // 
            this.set_button1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.set_button1.Location = new System.Drawing.Point(240, 91);
            this.set_button1.Name = "set_button1";
            this.set_button1.Size = new System.Drawing.Size(100, 26);
            this.set_button1.TabIndex = 2;
            this.set_button1.Text = "Set";
            this.set_button1.UseVisualStyleBackColor = true;
            this.set_button1.Click += new System.EventHandler(this.set_button1_Click);
            // 
            // Status
            // 
            this.Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Status.Location = new System.Drawing.Point(12, 61);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(328, 20);
            this.Status.TabIndex = 3;
            this.Status.Text = " ";
            // 
            // cancel_button2
            // 
            this.cancel_button2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_button2.Location = new System.Drawing.Point(131, 91);
            this.cancel_button2.Name = "cancel_button2";
            this.cancel_button2.Size = new System.Drawing.Size(100, 26);
            this.cancel_button2.TabIndex = 4;
            this.cancel_button2.Text = "Cancel";
            this.cancel_button2.UseVisualStyleBackColor = true;
            this.cancel_button2.Click += new System.EventHandler(this.cancel_button2_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // username1
            // 
            this.username1.CueText = "Write your UVA username here...";
            this.username1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username1.Location = new System.Drawing.Point(12, 30);
            this.username1.Name = "username1";
            this.username1.Size = new System.Drawing.Size(328, 25);
            this.username1.TabIndex = 0;
            this.username1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.username1_KeyDown);
            // 
            // UsernameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 129);
            this.Controls.Add(this.cancel_button2);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.set_button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.username1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UsernameForm";
            this.Text = "Set Default Username";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CueTextBox username1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button set_button1;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.Button cancel_button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
namespace UVA_Arena
{
    partial class InputForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ok_button1 = new System.Windows.Forms.Button();
            this.info_text = new System.Windows.Forms.Label();
            this.inputText1 = new System.Windows.Forms.CueTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ok_button1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.info_text, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.inputText1, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.TabStop = true;
            // 
            // ok_button1
            // 
            this.ok_button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ok_button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ok_button1.Location = new System.Drawing.Point(313, 41);
            this.ok_button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ok_button1.Name = "ok_button1";
            this.ok_button1.Size = new System.Drawing.Size(94, 27);
            this.ok_button1.TabIndex = 1;
            this.ok_button1.Text = "OK";
            this.ok_button1.UseVisualStyleBackColor = true;
            this.ok_button1.Click += new System.EventHandler(this.ok_button1_Click);
            // 
            // info_text
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.info_text, 2);
            this.info_text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info_text.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.info_text.ForeColor = System.Drawing.SystemColors.GrayText;
            this.info_text.Location = new System.Drawing.Point(3, 3);
            this.info_text.Margin = new System.Windows.Forms.Padding(3);
            this.info_text.Name = "info_text";
            this.info_text.Size = new System.Drawing.Size(404, 33);
            this.info_text.TabIndex = 2;
            this.info_text.Text = "Input : ";
            this.info_text.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // inputText1
            // 
            this.inputText1.CueText = "Type your text here...";
            this.inputText1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputText1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputText1.Location = new System.Drawing.Point(3, 42);
            this.inputText1.Name = "inputText1";
            this.inputText1.Size = new System.Drawing.Size(304, 25);
            this.inputText1.TabIndex = 0;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(428, 85);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Input Form";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button ok_button1;
        public System.Windows.Forms.Label info_text;
        public System.Windows.Forms.CueTextBox inputText1;
    }
}
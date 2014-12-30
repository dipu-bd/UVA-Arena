namespace UVA_Arena
{
    partial class CodeFileCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeFileCreator));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cPPradio = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ansiCradio = new System.Windows.Forms.RadioButton();
            this.pascalRadio = new System.Windows.Forms.RadioButton();
            this.javaRadio = new System.Windows.Forms.RadioButton();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cPPradio, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ansiCradio, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pascalRadio, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.javaRadio, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 54);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(176, 117);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cPPradio
            // 
            this.cPPradio.Checked = true;
            this.cPPradio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cPPradio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cPPradio.ImageKey = ".cpp.png";
            this.cPPradio.ImageList = this.imageList1;
            this.cPPradio.Location = new System.Drawing.Point(11, 31);
            this.cPPradio.Margin = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.cPPradio.Name = "cPPradio";
            this.cPPradio.Size = new System.Drawing.Size(154, 26);
            this.cPPradio.TabIndex = 3;
            this.cPPradio.TabStop = true;
            this.cPPradio.Text = "C++";
            this.cPPradio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cPPradio.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, ".c.png");
            this.imageList1.Images.SetKeyName(1, ".cpp.png");
            this.imageList1.Images.SetKeyName(2, ".java.png");
            this.imageList1.Images.SetKeyName(3, ".pascal.png");
            // 
            // ansiCradio
            // 
            this.ansiCradio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ansiCradio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ansiCradio.ImageKey = ".c.png";
            this.ansiCradio.ImageList = this.imageList1;
            this.ansiCradio.Location = new System.Drawing.Point(11, 2);
            this.ansiCradio.Margin = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.ansiCradio.Name = "ansiCradio";
            this.ansiCradio.Size = new System.Drawing.Size(154, 26);
            this.ansiCradio.TabIndex = 2;
            this.ansiCradio.Text = "Ansi C";
            this.ansiCradio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ansiCradio.UseVisualStyleBackColor = true;
            // 
            // pascalRadio
            // 
            this.pascalRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pascalRadio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pascalRadio.ImageKey = ".pascal.png";
            this.pascalRadio.ImageList = this.imageList1;
            this.pascalRadio.Location = new System.Drawing.Point(11, 89);
            this.pascalRadio.Margin = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.pascalRadio.Name = "pascalRadio";
            this.pascalRadio.Size = new System.Drawing.Size(154, 26);
            this.pascalRadio.TabIndex = 5;
            this.pascalRadio.Text = "Pascal";
            this.pascalRadio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.pascalRadio.UseVisualStyleBackColor = true;
            // 
            // javaRadio
            // 
            this.javaRadio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.javaRadio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.javaRadio.ImageKey = ".java.png";
            this.javaRadio.ImageList = this.imageList1;
            this.javaRadio.Location = new System.Drawing.Point(11, 60);
            this.javaRadio.Margin = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.javaRadio.Name = "javaRadio";
            this.javaRadio.Size = new System.Drawing.Size(154, 26);
            this.javaRadio.TabIndex = 4;
            this.javaRadio.Text = "Java";
            this.javaRadio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.javaRadio.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(104, 180);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(85, 26);
            this.ok_button.TabIndex = 0;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(13, 180);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(85, 26);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose a language to create code file :";
            // 
            // CodeFileCreator
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BackgroundImage = global::UVA_Arena.Properties.Resources.backimg;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(200, 217);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeFileCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Langauge";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton ansiCradio;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RadioButton cPPradio;
        private System.Windows.Forms.RadioButton pascalRadio;
        private System.Windows.Forms.RadioButton javaRadio;
    }
}
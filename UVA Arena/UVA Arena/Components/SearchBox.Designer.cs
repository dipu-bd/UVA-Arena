namespace UVA_Arena.Custom
{
    partial class SearchBox
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
            this.table1 = new System.Windows.Forms.TableLayoutPanel();
            this.search_text = new System.Windows.Forms.CueTextBox();
            this.search_button = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.table1.SuspendLayout();
            this.SuspendLayout();
            // 
            // table1
            // 
            this.table1.ColumnCount = 3;
            this.table1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table1.Controls.Add(this.search_text, 0, 0);
            this.table1.Controls.Add(this.search_button, 1, 0);
            this.table1.Controls.Add(this.clear_button, 2, 0);
            this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table1.Location = new System.Drawing.Point(0, 0);
            this.table1.Margin = new System.Windows.Forms.Padding(0);
            this.table1.Name = "table1";
            this.table1.RowCount = 1;
            this.table1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table1.Size = new System.Drawing.Size(200, 26);
            this.table1.TabIndex = 0;
            this.table1.TabStop = true;
            // 
            // search_text
            // 
            this.search_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.search_text.BackColor = System.Drawing.Color.White;
            this.search_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.search_text.CueText = "Search...";
            this.search_text.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search_text.ForeColor = System.Drawing.Color.Black;
            this.search_text.Location = new System.Drawing.Point(2, 5);
            this.search_text.Margin = new System.Windows.Forms.Padding(2, 1, 1, 1);
            this.search_text.Name = "search_text";
            this.search_text.Size = new System.Drawing.Size(141, 16);
            this.search_text.TabIndex = 0;
            this.search_text.TextChanged += new System.EventHandler(this.search_text_TextChanged);
            this.search_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.search_text_KeyDown);
            // 
            // search_button
            // 
            this.search_button.AutoSize = true;
            this.search_button.BackColor = System.Drawing.Color.PaleTurquoise;
            this.search_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.search_button.FlatAppearance.BorderColor = System.Drawing.Color.MediumTurquoise;
            this.search_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.search_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise;
            this.search_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.search_button.Image = global::UVA_Arena.Properties.Resources.search;
            this.search_button.Location = new System.Drawing.Point(144, 0);
            this.search_button.Margin = new System.Windows.Forms.Padding(0);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(28, 26);
            this.search_button.TabIndex = 1;
            this.toolTip1.SetToolTip(this.search_button, "Search");
            this.search_button.UseVisualStyleBackColor = false;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // clear_button
            // 
            this.clear_button.AutoSize = true;
            this.clear_button.BackColor = System.Drawing.Color.PaleTurquoise;
            this.clear_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clear_button.FlatAppearance.BorderColor = System.Drawing.Color.MediumTurquoise;
            this.clear_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkTurquoise;
            this.clear_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Turquoise;
            this.clear_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clear_button.Image = global::UVA_Arena.Properties.Resources.clear;
            this.clear_button.Location = new System.Drawing.Point(172, 0);
            this.clear_button.Margin = new System.Windows.Forms.Padding(0);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(28, 26);
            this.clear_button.TabIndex = 2;
            this.toolTip1.SetToolTip(this.clear_button, "Clear");
            this.clear_button.UseVisualStyleBackColor = false;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // SearchBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.table1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SearchBox";
            this.Size = new System.Drawing.Size(200, 26);
            this.BackColorChanged += new System.EventHandler(this.SearchBox_BackColorChanged);
            this.table1.ResumeLayout(false);
            this.table1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table1;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.CueTextBox search_text;
        public System.Windows.Forms.Button clear_button;
        public System.Windows.Forms.Button search_button;
    }
}

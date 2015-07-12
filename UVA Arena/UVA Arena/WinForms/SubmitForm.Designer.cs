namespace UVA_Arena 
{
    partial class SubmitForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubmitForm));
            this.customWebBrowser1 = new UVA_Arena.ExtendedControls.CustomWebBrowser();
            this.SuspendLayout();
            // 
            // customWebBrowser1
            // 
            this.customWebBrowser1.BackColor = System.Drawing.Color.PowderBlue;
            this.customWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customWebBrowser1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customWebBrowser1.ForeColor = System.Drawing.Color.Black;
            this.customWebBrowser1.HomeUrl = null;
            this.customWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.customWebBrowser1.Name = "customWebBrowser1";
            this.customWebBrowser1.Size = new System.Drawing.Size(684, 362);
            this.customWebBrowser1.TabIndex = 0;
            this.customWebBrowser1.TopBarColor = System.Drawing.Color.LightBlue;
            this.customWebBrowser1.Url = new System.Uri("about:blank", System.UriKind.Absolute);
            this.customWebBrowser1.UrlBoxFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customWebBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.customWebBrowser1_DocumentCompleted);
            // 
            // SubmitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(684, 362);
            this.Controls.Add(this.customWebBrowser1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubmitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Submit Problem";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.CustomWebBrowser customWebBrowser1;



    }
}
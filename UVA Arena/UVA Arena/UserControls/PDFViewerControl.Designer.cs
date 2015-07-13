namespace UVA_Arena
{
    partial class PDFViewerControl
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
                ClearPageHolder();
                pdfWrapper.Dispose();
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
            this.pdfPageHolder = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // pdfPageHolder
            // 
            this.pdfPageHolder.AutoScroll = true;
            this.pdfPageHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfPageHolder.Location = new System.Drawing.Point(0, 0);
            this.pdfPageHolder.Name = "pdfPageHolder";
            this.pdfPageHolder.Size = new System.Drawing.Size(629, 429);
            this.pdfPageHolder.TabIndex = 0;
            // 
            // PDFViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pdfPageHolder);
            this.Name = "PDFViewerControl";
            this.Size = new System.Drawing.Size(629, 429);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pdfPageHolder;
    }
}

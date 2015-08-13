namespace UVA_Arena 
{
    partial class PdfViewer
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
            this._toolStrip = new System.Windows.Forms.ToolStrip();
            this._zoomInButton = new System.Windows.Forms.ToolStripButton();
            this._zoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._fitWidthButton = new System.Windows.Forms.ToolStripButton();
            this._fitHeightButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._rotateLeftButton = new System.Windows.Forms.ToolStripButton();
            this._rotateRightButton = new System.Windows.Forms.ToolStripButton();
            this._renderer = new PdfiumViewer.PdfRenderer();
            this._toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolStrip
            // 
            this._toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._zoomInButton,
            this._zoomOutButton,
            this.toolStripSeparator2,
            this._fitWidthButton,
            this._fitHeightButton,
            this.toolStripSeparator3,
            this._rotateLeftButton,
            this._rotateRightButton});
            this._toolStrip.Location = new System.Drawing.Point(0, 0);
            this._toolStrip.Name = "_toolStrip";
            this._toolStrip.Size = new System.Drawing.Size(723, 25);
            this._toolStrip.TabIndex = 1;
            this._toolStrip.TabStop = true;
            // 
            // _zoomInButton
            // 
            this._zoomInButton.Image = global::UVA_Arena.Properties.Resources.zoom_in;
            this._zoomInButton.Name = "_zoomInButton";
            this._zoomInButton.Size = new System.Drawing.Size(72, 22);
            this._zoomInButton.Text = "Zoom In";
            this._zoomInButton.Click += new System.EventHandler(this._zoomInButton_Click);
            // 
            // _zoomOutButton
            // 
            this._zoomOutButton.Image = global::UVA_Arena.Properties.Resources.zoom_out;
            this._zoomOutButton.Name = "_zoomOutButton";
            this._zoomOutButton.Size = new System.Drawing.Size(82, 22);
            this._zoomOutButton.Text = "Zoom Out";
            this._zoomOutButton.Click += new System.EventHandler(this._zoomOutButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _fitWidthButton
            // 
            this._fitWidthButton.Image = global::UVA_Arena.Properties.Resources.fit_width;
            this._fitWidthButton.Name = "_fitWidthButton";
            this._fitWidthButton.Size = new System.Drawing.Size(75, 22);
            this._fitWidthButton.Text = "Fit Width";
            this._fitWidthButton.Click += new System.EventHandler(this._fitWidthButton_Click);
            // 
            // _fitHeightButton
            // 
            this._fitHeightButton.Image = global::UVA_Arena.Properties.Resources.fit_height;
            this._fitHeightButton.Name = "_fitHeightButton";
            this._fitHeightButton.Size = new System.Drawing.Size(79, 22);
            this._fitHeightButton.Text = "Fit Height";
            this._fitHeightButton.Click += new System.EventHandler(this._fitHeightButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _rotateLeftButton
            // 
            this._rotateLeftButton.Image = global::UVA_Arena.Properties.Resources.rotate_left;
            this._rotateLeftButton.Name = "_rotateLeftButton";
            this._rotateLeftButton.Size = new System.Drawing.Size(84, 22);
            this._rotateLeftButton.Text = "Rotate Left";
            this._rotateLeftButton.Click += new System.EventHandler(this._rotateLeftButton_Click);
            // 
            // _rotateRightButton
            // 
            this._rotateRightButton.Image = global::UVA_Arena.Properties.Resources.rotate_right;
            this._rotateRightButton.Name = "_rotateRightButton";
            this._rotateRightButton.Size = new System.Drawing.Size(92, 22);
            this._rotateRightButton.Text = "Rotate Right";
            this._rotateRightButton.Click += new System.EventHandler(this._rotateRightButton_Click);
            // 
            // _renderer
            // 
            this._renderer.Cursor = System.Windows.Forms.Cursors.Default;
            this._renderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._renderer.Location = new System.Drawing.Point(0, 25);
            this._renderer.Name = "_renderer";
            this._renderer.Page = 0;
            this._renderer.Rotation = PdfiumViewer.PdfRotation.Rotate0;
            this._renderer.Size = new System.Drawing.Size(723, 383);
            this._renderer.TabIndex = 0;
            this._renderer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitWidth;
            // 
            // PdfViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._renderer);
            this.Controls.Add(this._toolStrip);
            this.DoubleBuffered = true;
            this.Name = "PdfViewer";
            this.Size = new System.Drawing.Size(723, 408);
            this._toolStrip.ResumeLayout(false);
            this._toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolStrip;
        private global::PdfiumViewer.PdfRenderer _renderer;
        private System.Windows.Forms.ToolStripButton _zoomInButton;
        private System.Windows.Forms.ToolStripButton _zoomOutButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _fitWidthButton;
        private System.Windows.Forms.ToolStripButton _fitHeightButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton _rotateLeftButton;
        private System.Windows.Forms.ToolStripButton _rotateRightButton;
    }
}

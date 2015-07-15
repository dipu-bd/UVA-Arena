using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PdfiumViewer
{
    /// <summary>
    /// Control to host PDF documents with support for printing.
    /// </summary>
    public partial class PdfViewer : UserControl
    {
        private PdfDocument _document;

        /// <summary>
        /// Gets or sets the PDF document.
        /// </summary>
        public PdfDocument Document
        {
            get { return _document; }
            set
            {
                if (_document != value)
                {
                    _document = value;

                    if (_document != null)
                        _renderer.Load(_document);

                    UpdateEnabled(); 
                }
            }
        }

        /// <summary>
        /// Get the <see cref="PdfRenderer"/> that renders the PDF document.
        /// </summary>
        public PdfRenderer Renderer
        {
            get { return _renderer; }
        }

        /// <summary>
        /// Gets or sets the default document name used when saving the document.
        /// </summary>
        public string DefaultDocumentName { get; set; }

        /// <summary>
        /// Gets or sets the default print mode.
        /// </summary>
        [DefaultValue(PdfPrintMode.CutMargin)]
        public PdfPrintMode DefaultPrintMode { get; set; }

        /// <summary>
        /// Gets or sets the way the document should be zoomed initially.
        /// </summary>
        public PdfViewerZoomMode ZoomMode
        {
            get { return _renderer.ZoomMode; }
            set
            {
                _renderer.ZoomMode = value;
                _fitWidthButton.Checked = (value == PdfViewerZoomMode.FitWidth);
                _fitHeightButton.Checked = (value == PdfViewerZoomMode.FitHeight);
            }
        }

        /// <summary>
        /// Initializes a new instance of the PdfViewer class.
        /// </summary>
        public PdfViewer()
        {
            DefaultPrintMode = PdfPrintMode.CutMargin;
                        
            InitializeComponent();
            
            UpdateEnabled(); 
        }
        
        private void FitPage(PdfViewerZoomMode zoomMode)
        {
            int page = _renderer.Page;
            this.ZoomMode = zoomMode;
            _renderer.Zoom = 1;
            _renderer.Page = page;
        }

        private void UpdateEnabled()
        {
            _toolStrip.Enabled = (_document != null);
        }

        private void _zoomInButton_Click(object sender, EventArgs e)
        {
            _renderer.ZoomIn();
        }

        private void _zoomOutButton_Click(object sender, EventArgs e)
        {
            _renderer.ZoomOut();
        }

        private void _fitWidthButton_Click(object sender, EventArgs e)
        {
            FitPage(PdfViewerZoomMode.FitWidth);
        }

        private void _fitHeightButton_Click(object sender, EventArgs e)
        { 
            FitPage(PdfViewerZoomMode.FitHeight);
        }

        private void _rotateLeftButton_Click(object sender, EventArgs e)
        {
            _renderer.RotateLeft();
        }

        private void _rotateRightButton_Click(object sender, EventArgs e)
        {
            _renderer.RotateRight();
        }
         
    }
}

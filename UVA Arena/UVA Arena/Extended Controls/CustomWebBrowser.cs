using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UVA_Arena.ExtendedControls
{
    [DefaultEvent("DocumentCompleted")]
    public partial class CustomWebBrowser : UserControl
    {
        public CustomWebBrowser()
        {
            InitializeComponent();
        }


        /// <summary>
        /// URL of the home page and initial page
        /// </summary>
        [Category("Custom Property")]
        public string HomeUrl
        {
            get;
            set;
        }
         
        /// <summary>
        /// Gets or sets the URL of the current document
        /// </summary>
        [Category("Custom Property")]
        public Uri Url
        {
            get { return webBrowser1.Url; }
            set { webBrowser1.Url = value; }
        }

        /// <summary>
        /// Gets or sets the visibility of status bar
        /// </summary>
        [Category("Custom Property"), DefaultValue(true)]
        public bool StatusBarVisible
        {
            get { return statusStrip1.Visible; }
            set { statusStrip1.Visible = value; }
        }


        /// <summary>
        /// Document title of the web browser
        /// </summary>
        [Category("Custom Property")]
        public string Title
        {
            get { return webBrowser1.DocumentTitle; }
        }

        /// <summary>
        /// Color of the top bar wher address box contains
        /// </summary>
        [DefaultValue(typeof(SystemColors), "Control")]
        [Category("Custom Property")]
        public Color TopBarColor
        {
            get { return topBar.BackColor; }
            set { topBar.BackColor = value; }
        }

        /// <summary>
        /// Address bar's font
        /// </summary>
        [Category("Custom Property")]
        public Font UrlBoxFont
        {
            get { return urlBox.Font; }
            set { urlBox.Font = value; }
        }

        /// <summary>
        /// Web Browser control
        /// </summary>
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Category("Custom Property")]
        public WebBrowser Browser
        {
            get { return webBrowser1; }
        }

        /// <summary>
        /// HTML document of web browser
        /// </summary>        
        [Category("Custom Property")]
        public HtmlDocument Document
        {
            get { return webBrowser1.Document; }
        }


        /// <summary>
        /// True to show previous and next button
        /// </summary>
        [DefaultValue(true)]
        [Category("Custom Property")]
        public bool KeepHistory
        {
            get { return (nextButton.Visible); }
            set
            {
                nextButton.Visible = value;
                backButton.Visible = value;
            }
        }

        /// <summary>
        /// Navigate to the given url address
        /// </summary>
        /// <param name="url">URL to navigate</param>
        public void Navigate(string url)
        {
            webBrowser1.Navigate(url);
            HomeUrl = url;
            SetUrlBox();
        }


        /// <summary>
        /// Clear all previous cookies of internet explorer
        /// </summary>          

        public void ClearCookies()
        {
            WebBrowserHelper.ClearCache();
            //webBrowser1.Navigate(Properties.Resources.ClearCookies);
            MessageBox.Show("Cache and cookies cleared.");
        }


        //
        // Event Handlers
        //
        public class StatusChangedEventArgs : EventArgs
        {
            public StatusChangedEventArgs() { }
            public StatusChangedEventArgs(string _status) { this._Status = _status; }

            private string _Status = "";

            /// <summary>
            /// Gets the current status text
            /// </summary>
            public string Status
            {
                get { return _Status; }
            }
        }

        /// <summary>
        /// Handels Staus Changed event occured when web browser status is changed
        /// </summary>
        /// <param name="sender">object parameter to source of the action</param>
        /// <param name="e">StatusChangedEventArgs</param>
        public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

        /// <summary>
        /// Raises when a staus update occured
        /// </summary>
        [Category("Custom Browser")]
        public event StatusChangedEventHandler StatusChanged;
        /// <summary>
        /// Raises whenever progress state changed
        /// </summary>
        [Category("Custom Browser")]
        public event WebBrowserProgressChangedEventHandler ProgressChanged;
        /// <summary>
        /// Raises when web-browser has completed loading the document from url
        /// </summary>
        [Category("Custom Browser")]
        public event WebBrowserDocumentCompletedEventHandler DocumentCompleted;


        #region Less significant event functions

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                if (DocumentCompleted != null)
                    DocumentCompleted(sender, e);
            }
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            try
            {
                Progress1.Value = (int)e.CurrentProgress;
                if (ProgressChanged != null) ProgressChanged(sender, e);
            }
            catch { }
        }

        void webBrowser1_StatusTextChanged(object sender, EventArgs e)
        {
            try
            {
                Status1.Text = webBrowser1.StatusText;
                if (StatusChanged != null)
                    StatusChanged(sender, new StatusChangedEventArgs(webBrowser1.StatusText));
            }
            catch { }
        }

        void webBrowser1_CanGoForwardChanged(object sender, EventArgs e)
        {
            nextButton.Enabled = webBrowser1.CanGoForward;
        }

        void webBrowser1_CanGoBackChanged(object sender, EventArgs e)
        {
            backButton.Enabled = webBrowser1.CanGoBack;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (urlBox.Focused)
            {
                string url = urlBox.Text;
                webBrowser1.Navigate(url);
                Progress1.Value = 0;
            }
            else
            {
                webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
            }
        }

        private void urlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goButton.PerformClick();
            }
        }

        private void topBox_Enter(object sender, EventArgs e)
        {
            urlBox.Text = webBrowser1.Url.ToString();
            if (urlBox.Focused)
                urlBox.ForeColor = Color.Black;
            else
                urlBox.ForeColor = Color.DarkSlateGray;
        }

        private void topBox_Leave(object sender, EventArgs e)
        {
            SetUrlBox();
            if (urlBox.Focused)
                urlBox.ForeColor = Color.Black;
            else
                urlBox.ForeColor = Color.DarkSlateGray;
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            if (!urlBox.Focused)
            {
                SetUrlBox();
            }
        }

        private void SetUrlBox()
        {
            if (webBrowser1.DocumentTitle.Length > 2)
                urlBox.Text = webBrowser1.DocumentTitle;
            else
                if (webBrowser1.Url != null)
                    urlBox.Text = webBrowser1.Url.ToString();
                else
                    urlBox.Text = "";
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(HomeUrl);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }
        private void clearCookie_Click(object sender, EventArgs e)
        {
            ClearCookies();
        }


        #endregion

    }
}

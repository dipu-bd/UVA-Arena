using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UVA_Arena.Custom
{
    [DefaultEvent("SearchTextChanged")]
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        public bool ClearButtonVisible
        {
            get
            {
                return clear_button.Visible;
            }
            set
            {
                if (value)
                {
                    clear_button.Visible = true;
                    table1.ColumnStyles[2].Width = 32;
                }
                else
                {
                    clear_button.Visible = false;
                    table1.ColumnStyles[2].Width = 0;
                }
            }
        }

        public bool SearchButtonVisible
        {
            get
            {
                return search_button.Visible;
            }
            set
            {
                if (value)
                {
                    search_button.Visible = true;
                    table1.ColumnStyles[1].Width = 32;
                }
                else
                {
                    search_button.Visible = false;
                    table1.ColumnStyles[1].Width = 0;
                }
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.search_text.Font = this.Font;
        }

        public string SearchText
        {
            get { return search_text.Text; }
            set { search_text.Text = value; }
        }

        public string CueText
        {
            get { return search_text.CueText; }
            set { search_text.CueText = value; }
        }

        public event EventHandler<KeyEventArgs> SearchKeyDown;
        public event EventHandler<EventArgs> SearchTextChanged;
        public event EventHandler<EventArgs> SearchButtonClicked;
        public event EventHandler<EventArgs> ClearButtonClicked;

        private void search_text_KeyDown(object sender, KeyEventArgs e)
        {
            if (SearchKeyDown != null) SearchKeyDown(sender, e);
            if (e.KeyCode == Keys.Enter) search_button.PerformClick();
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if (SearchButtonClicked != null) SearchButtonClicked(sender, e);
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            search_text.Text = "";
            if (ClearButtonClicked != null) ClearButtonClicked(sender, e);
        }

        private void search_text_TextChanged(object sender, EventArgs e)
        {
            if (SearchTextChanged != null) SearchTextChanged(sender, e);
        }

        private void SearchBox_BackColorChanged(object sender, EventArgs e)
        {
            search_text.BackColor = this.BackColor;
        }
    }
}

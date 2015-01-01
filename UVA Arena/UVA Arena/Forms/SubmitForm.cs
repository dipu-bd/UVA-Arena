using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class SubmitForm : Form
    {
        public SubmitForm()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private long pnum;
        private string code;
        private Language lang;
        private const string QUICK = "http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";

        public void LoadSubmit(long pnum, string code = null, Language lang = Language.CPP)
        {
            this.pnum = pnum;
            this.code = code;
            this.lang = lang;

            //submit problem
            if(!ProcessPage())
                webBrowser1.Navigate(QUICK); 
        }

        private bool ProcessPage()
        {
            bool result = false;

            try
            {
                HtmlDocument hdoc = webBrowser1.Document;
                if (hdoc == null) return false;
                foreach (HtmlElement helem in hdoc.Forms)
                {
                    if (helem == null) continue;
                    HtmlElementCollection hcol = helem.GetElementsByTagName("input");
                    HtmlElementCollection tarea = helem.GetElementsByTagName("textarea");

                    string check = "3";
                    if (lang == Language.C) check = "1";
                    if (lang == Language.CPP) check = "3";
                    if (lang == Language.CPP11) check = "5";
                    if (lang == Language.Java) check = "2";
                    if (lang == Language.Pascal) check = "4";

                    //set code
                    foreach (HtmlElement inpbox in tarea)
                    {
                        string name = inpbox.GetAttribute("name");
                        if (name == "code")
                        {
                            inpbox.InnerText = this.code;
                            result = true;
                            break;
                        }
                    }

                    //set other values
                    foreach (HtmlElement inpbox in hcol)
                    {
                        string name = inpbox.GetAttribute("name");
                        if (name == "remember")
                        {
                            inpbox.SetAttribute("checked", "true");
                            result = false;
                        }
                        else if (name == "localid")
                        {
                            inpbox.SetAttribute("value", pnum.ToString());
                        }
                        else if (name == "language")
                        {
                            string value = inpbox.GetAttribute("value");
                            if (value == check) inpbox.SetAttribute("checked", "1");
                        }
                    }
                }
            }
            catch { }

            return result;
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            status1.Text = webBrowser1.StatusText;
            progress1.Value = (int)(100 * e.CurrentProgress / e.MaximumProgress);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ProcessPage();

            //set new data
            string url = e.Url.ToString();
            discussUrlBox.Text = url;
            status1.Text = webBrowser1.StatusText;

            //check if a submission occured            
            string msg = "mosmsg=Submission+received+with+ID+";
            if(url.Contains(msg))
            {
                int tmp;
                url = url.Substring(url.IndexOf(msg) + msg.Length);
                if (int.TryParse(url, out tmp))
                {
                    Interactivity.ShowJudgeStatus();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Submission Error", "Submit Problem", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void goDiscussButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(discussUrlBox.Text);
        }

        private void homeDiscussButton_Click(object sender, EventArgs e)
        {
            discussUrlBox.Text = QUICK;
            webBrowser1.Navigate(QUICK);
        }
    }
}

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

        private long pnum;
        private string code;
        private Language lang;
        private const string QUICK = "http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25&page=submit_problem";

        public void LoadSubmit(long pnum, string code = null, Language lang = Language.CPP)
        {
            this.pnum = pnum;
            this.code = code;
            this.lang = lang;

            //submit problem
            webBrowser1.Navigate(QUICK);
        }

        private void ProcessPage(HtmlElement helem)
        {
            if (helem == null) return;
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

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            status1.Text = webBrowser1.StatusText;
            progress1.Value = (int)(100 * e.CurrentProgress / e.MaximumProgress);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            status1.Text = webBrowser1.StatusText;
            discussUrlBox.Text = webBrowser1.Url.ToString();
            HtmlDocument hdoc = webBrowser1.Document;
            foreach (HtmlElement helem in hdoc.Forms) ProcessPage(helem);
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

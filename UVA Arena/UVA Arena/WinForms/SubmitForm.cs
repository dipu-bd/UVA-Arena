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
        private string QUICK = "http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25";

        public void LoadSubmit(long prob_num, string source_code = null, Language language = Language.CPP)
        {
            this.pnum = prob_num;
            this.code = source_code;
            this.lang = language;

            //submit problem  
            if (!ProcessPage())
            {
                customWebBrowser1.Navigate(QUICK);
            }
        }

        private bool ProcessPage()
        {
            bool result = false;
            try
            {
                HtmlDocument hdoc = customWebBrowser1.Document;

                foreach (HtmlElement helem in hdoc.Forms)
                {
                    if (helem == null) continue;
                    HtmlElementCollection hcol = helem.GetElementsByTagName("input");
                    HtmlElementCollection tarea = helem.GetElementsByTagName("textarea");

                    string check = "5"; //C++ and C++11
                    if (lang == Language.C) check = "1";
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
                        }
                        else if (name == "localid")
                        {
                            inpbox.SetAttribute("value", pnum.ToString());
                            result = true;
                        }
                        else if (name == "language")
                        {
                            string value = inpbox.GetAttribute("value");
                            if (value == check) inpbox.SetAttribute("checked", "1");
                            result = true;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }

            //show this forms after processing 
            return result;
        }

        private void customWebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ProcessPage();

            try
            {
                //check if a submission occured            
                if (customWebBrowser1.Url == null) return;
                string url = customWebBrowser1.Url.ToString();
                string msg = "mosmsg=Submission+received+with+ID+";
                if (url.Contains(msg))
                {
                    url = url.Substring(url.IndexOf(msg) + msg.Length);
                    if (int.Parse(url) > 10)
                    {
                        Interactivity.ShowJudgeStatus();
                        this.Hide();
                    }
                }
            }
            catch { }
        }
    }
}

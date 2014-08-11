using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
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

            probnameCol.AspectGetter = delegate(object row)
            {
                string name = Path.GetFileNameWithoutExtension(((FileInfo)row).Name);
                return long.Parse(name.Substring(0, name.IndexOf('.')));
            };
            probnameCol.AspectToStringConverter = delegate(object key)
            {
                return string.Format("{0} - {1}", key,
                    ProblemDatabase.GetTitle((long)key));
            };
            langCol.AspectGetter = delegate(object row)
            {
                string ext = ((FileInfo)row).Extension.ToLower();
                if (ext == ".c") return "Ansi C";
                if (ext == ".cpp") return "C++";
                if (ext == ".java") return "Java";
                if (ext == ".pascal") return "Pascal";
                return "Unknown";
            };
        }

        private long pnum;
        private FileInfo pending;
        private const string QUICK = "http://uva.onlinejudge.org/index.php?option=com_onlinejudge&Itemid=25&page=submit_problem";

        public void LoadSubmit(long pnum, string code = null, Language lang = Language.CPP)
        {
            this.pnum = pnum;
            string path = LocalDirectory.LastSubmitPath;
            //get last submitted problems
            List<FileInfo> flist = new List<FileInfo>();
            foreach (string file in Directory.GetFiles(LocalDirectory.LastSubmitPath))
            {
                flist.Add(new FileInfo(file));
            }

            if (!string.IsNullOrEmpty(code))
            {
                //save current submit data
                for (int i = 1; ; ++i)
                {
                    string name = string.Format("{0}.{1:00}.{2}", pnum, i, lang.ToString().ToLower());
                    pending = new FileInfo(Path.Combine(path, name));
                    if (!pending.Exists) break;
                }
                File.WriteAllText(pending.FullName, code);
                flist.Add(pending);
            }

            //set list
            objectListView1.SetObjects(flist);
            objectListView1.Sort(datetimeCol, SortOrder.Descending);
            objectListView1.SelectedObject = pending;

            //submit problem
            webBrowser1.Navigate(QUICK);
        }
        
        private void ProcessPage(HtmlElement helem)
        {
            if (helem == null) return;
            HtmlElementCollection hcol = helem.GetElementsByTagName("input");
            HtmlElementCollection tarea = helem.GetElementsByTagName("textarea");

            string check = "3";
            if (pending != null)
            {
                string ext = pending.Extension.ToLower();
                if (ext == ".c") check = "1";
                if (ext == ".cpp") check = "3";
                if (ext == ".java") check = "2";
                if (ext == ".pascal") check = "4";

                foreach (HtmlElement inpbox in tarea)
                {
                    string name = inpbox.GetAttribute("name");
                    if (name == "code")
                    {
                        inpbox.InnerText = File.ReadAllText(pending.FullName);
                        break;
                    }
                }
            }

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
            if (progress1.Maximum != e.MaximumProgress)
            {
                progress1.Value = 0;
                progress1.Maximum = (int)e.MaximumProgress;
            }

            progress1.Value = (int)e.CurrentProgress;
            status1.Text = webBrowser1.StatusText;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url != new Uri(QUICK)) return;
            HtmlDocument hdoc = webBrowser1.Document;
            foreach (HtmlElement helem in hdoc.Forms) ProcessPage(helem);
        }
    }
}

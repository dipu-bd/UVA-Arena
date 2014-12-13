using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using UVA_Arena.Structures;
using UVA_Arena.Internet;

namespace UVA_Arena.Elements
{
    public partial class ProblemViewer : UserControl
    {
        public ProblemViewer()
        {
            InitializeComponent();
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            titleBox1.BackColor = this.BackColor;
            catagoryInfo.BackColor = this.BackColor;
        }

        #region Load Problem

        ProblemInfo current = null;
        Stack<ProblemInfo> next = new Stack<ProblemInfo>();
        Stack<ProblemInfo> previous = new Stack<ProblemInfo>();

        public void LoadProblem(ProblemInfo plist)
        {
            if (plist == null)
            {
                ClearAll();
                return;
            }

            //cleanup history
            if (next.Count > 0)
            {
                next.Clear();
                nextButton.Enabled = false;
            }
            if (current != null)
            {
                previous.Push(current);
                backButton.Enabled = true;
            }

            current = plist;
            ShowCurrent();
            reloadButton.Enabled = true;
        }

        public void ClearAll()
        {
            current = null;
            titleBox1.Text = "No problem selected";
            catagoryInfo.Text = "";
            catagoryButton.Visible = false;
            webBrowser1.Navigate("");
        }

        private void ShowCurrent()
        {
            if (current == null) return;

            titleBox1.Text = string.Format("{0} - {1}", current.pnum, current.ptitle);
            titleBox1.Text += string.Format(" (Level : {0}{1})", current.level, current.star);
            catagoryButton.Visible = true;
            ShowCurrentTags();

            string path = LocalDirectory.GetProblemHtml(current.pnum);
            webBrowser1.Navigate(path);
            tabControl1.SelectedTab = descriptionTab;
                       
            markButton.Checked = RegistryAccess.FavouriteProblems.Contains(current.pnum);

            FileInfo local = new FileInfo(path);
            if (!local.Exists || local.Length < 100) DownloadHtml(current.pnum);
            else DownloadContents(current.pnum);
        }

        private void ShowCurrentTags()
        {
            if (current.tags == null) current.tags = RegistryAccess.GetTags(current.pnum);
            string txt = string.Join("; ", current.tags.ToArray());
            if (txt.Length > 0) txt = "Tags : " + txt;
            catagoryInfo.Text = txt;
        }

        #endregion
        
        #region Problem Downloader

        private void DownloadPdf(long pnum)
        {
            try
            {
                string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.pdf", pnum / 100, pnum);
                string file = LocalDirectory.GetProblemPdf(pnum);
                if (!reloadButton.Enabled || LocalDirectory.GetFileSize(file) < 200)
                    Downloader.DownloadFileAsync(url, file, pnum,
                        Internet.Priority.Normal, ProgressChanged, DownloadFinished);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Viewer");
            }
        }

        private void DownloadHtml(long pnum)
        {
            try
            {
                string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", pnum / 100, pnum);
                string file = LocalDirectory.GetProblemHtml(pnum);
                if (!reloadButton.Enabled || LocalDirectory.GetFileSize(file) < 100)
                    Downloader.DownloadFileAsync(url, file, pnum,
                        Internet.Priority.Normal, ProgressChanged, DownloadFinished);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Problem Viewer");
            }
        }

        private int DownloadContents(long pnum)
        {
            List<Internet.DownloadTask> dlist = Functions.ProcessHtmlContent(pnum, !reloadButton.Enabled);
            for (int i = 0; i < dlist.Count; ++i)
            {
                dlist[i].ProgressChangedEvent += ProgressChanged;
                if (i == dlist.Count - 1) dlist[i].DownloadCompletedEvent += DownloadFinished;
                Downloader.DownloadAsync(dlist[i], Internet.Priority.Low);
            }
            return dlist.Count;
        }

        private void ProgressChanged(Internet.DownloadTask task)
        {
            string file = Path.GetFileName(task.FileName);
            string text = string.Format("Downloading \"{0}\"... {1}% [{2} out of {3}] completed.",
                    file, task.ProgressPercentage, Functions.FormatMemory(task.Received), Functions.FormatMemory(task.DataSize));
            Interactivity.problems.Status1.Text = text;
        }

        private void DownloadFinished(DownloadTask task)
        {
            bool finish = false;
            if (task.Status != ProgressStatus.Completed) finish = true;
            if (current == null || current.pnum != (long)task.Token) finish = true;

            if (!finish)
            {
                string ext = Path.GetExtension(task.FileName);
                if (ext == ".pdf")
                {
                    TaskQueue.AddTask(Interactivity.problems.ClearStatus, 1000);
                    System.Diagnostics.Process.Start(task.FileName);
                    finish = true;
                }
                else if (ext == ".html")
                {
                    string file = LocalDirectory.GetProblemHtml(current.pnum);
                    webBrowser1.Navigate(file);
                    int cnt = DownloadContents(current.pnum);
                    if (cnt == 0) finish = true;
                }
                else
                {
                    finish = true;
                }
            }

            if (finish)
            {
                webBrowser1.Refresh();
                reloadButton.Enabled = true;
                TaskQueue.AddTask(Interactivity.problems.ClearStatus, 1000);
            }
        }

        #endregion

        #region Top bar

        private void codeButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            Interactivity.ShowCode(current.pnum);
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            Interactivity.SubmitCode(current.pnum);
        }

        private void expandViewButton1_Click(object sender, EventArgs e)
        {            
            if(Interactivity.problems.ExpandCollapseView())
            {
                ((Control)sender).Text = "Collapse";
            }
            else
            {
                ((Control)sender).Text = "Expand";
            }
        }

        private void catagoryInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) catagoryButton.PerformClick();
            e.SuppressKeyPress = true;
        }

        private void catagoryButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            CatagoryChange cc = new CatagoryChange(current);
            if (cc.ShowDialog() == DialogResult.OK) ShowCurrentTags();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (current != null)
            {
                next.Push(current);
                nextButton.Enabled = true;
            }
            current = previous.Pop();
            backButton.Enabled = (previous.Count > 0);

            ShowCurrent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (current != null)
            {
                previous.Push(current);
                backButton.Enabled = true;
            }
            current = next.Pop();
            nextButton.Enabled = (next.Count > 0);

            ShowCurrent();
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            reloadButton.Enabled = false;
            DownloadHtml(current.pnum);
        }

        private void pdfButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;

            string filepath = LocalDirectory.GetProblemPdf(current.pnum);
            if (!File.Exists(filepath) || (new FileInfo(filepath)).Length < 200)
            {
                DownloadPdf(current.pnum);
            }
            else
            {
                System.Diagnostics.Process.Start(filepath);
            }
        }

        private void externalButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;
            string url = string.Format("http://uva.onlinejudge.org/external/{0}/{1}.html", current.volume, current.pnum);
            System.Diagnostics.Process.Start(url);
        }

        private void markButton_Click(object sender, EventArgs e)
        {
            if (current == null) return;

            List<long> fav = RegistryAccess.FavouriteProblems;
            if (markButton.Checked) fav.Remove(current.pnum);
            else if (!fav.Contains(current.pnum)) fav.Add(current.pnum);

            RegistryAccess.FavouriteProblems = fav;
            markButton.Checked = !markButton.Checked;

            if (Interactivity.problems.favouriteButton.Checked)
                Interactivity.problems.LoadFavourites();
        }

        #endregion

        #region TabControl

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == discussTab)
            {
                homeDiscussButton.PerformClick();
            }
        }

        #endregion

        #region Discuss Tab

        private void goDiscussButton_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate(discussUrlBox.Text);
        }

        private void reloadDiscussButton_Click(object sender, EventArgs e)
        {
            webBrowser2.Refresh();
            discussUrlBox.Text = webBrowser2.Url.ToString();
        }

        private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            discussUrlBox.Text = e.Url.ToString();
        }

        private void prevDiscussButton_Click(object sender, EventArgs e)
        {
            webBrowser2.GoBack();
        }

        private void nextDiscussButton_Click(object sender, EventArgs e)
        {
            webBrowser2.GoForward();
        }
        private void homeDiscussButton_Click(object sender, EventArgs e)
        {
            string query = "";
            if (current != null) query = string.Format("search.php?keywords={0}", current.pnum);
            string discuss = string.Format("http://acm.uva.es/board/{0}", query);
            webBrowser2.Navigate(discuss);
            discussUrlBox.Text = discuss;
        }

        #endregion

    }
}

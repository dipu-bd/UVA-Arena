using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;
using UVA_Arena.Internet;
using Newtonsoft.Json;

namespace UVA_Arena.Elements
{
    public partial class ProblemViewer : UserControl
    {
        public ProblemViewer()
        {
            InitializeComponent();
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
                dlist[i].progress += ProgressChanged;
                if (i == dlist.Count - 1) dlist[i].completed += DownloadFinished;
                Downloader.DownloadAsync(dlist[i], Internet.Priority.Low);
            }
            return dlist.Count;
        }

        private void ProgressChanged(int percent, Internet.DownloadTask task)
        {
            string file = Path.GetFileName(task.file);
            string text = string.Format("Downloading \"{0}\"... {1}% [{2} out of {3}] completed.",
                    file, percent, Functions.FormatMemory(task.received), Functions.FormatMemory(task.total));
            Interactivity.problems.Status1.Text = text;
        }

        private void DownloadFinished(DownloadTask task)
        {
            bool finish = false;
            if (task.status != ProgressStatus.Completed) finish = true;
            if (current == null || current.pnum != (long)task.token) finish = true;

            if (!finish)
            {
                string ext = Path.GetExtension(task.file);
                if (ext == ".pdf")
                {
                    TaskQueue.AddTask(Interactivity.problems.ClearStatus, 1000);
                    System.Diagnostics.Process.Start(task.file);
                    finish = true;
                }
                else if (ext == ".html")
                {
                    webBrowser1.Refresh();
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
            expandViewButton1.Checked = !expandViewButton1.Checked;
            Interactivity.problems.ExpandView(expandViewButton1.Checked);
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

    }
}

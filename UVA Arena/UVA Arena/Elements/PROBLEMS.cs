using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena.Elements
{
    public partial class PROBLEMS : UserControl
    {
        public PROBLEMS()
        {
            InitializeComponent();
        }

        public void InitOthers()
        {
            LoadProblems();
        }

        #region ProblemDownloader

        private void problemWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string text = Internet.DownloadProblemDatabase();
                string file = LocalDirectory.ProblemDataFile;
                System.IO.File.WriteAllText(file, text);
                if(ProblemDatabase.LoadDatabase()) e.Result = null;
                else e.Result = "Failed to load problem database";
            }
            catch(Exception ex)
            {
                e.Result = "Failed to update problem database [" + ex.Message + "]";
            }
        }
        private void problemWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                SetStatus((string)e.Result);
                return;
            }

            SetStatus("Problem database is successfully updated.");
            LoadProblems();
        }

        #endregion

        #region StatusBar

        private void updateToolButton_Click(object sender, EventArgs e)
        {
            if (problemWorker.IsBusy) return;
            Status1.Text = "Updating problem database...";
            problemWorker.RunWorkerAsync();
        }

        public void SetStatus(string text, int timeout = 1000)
        {
            Status1.Text = text;
            TaskQueue.AddTask(ClearStatus, timeout);
        }
        public void ClearStatus()
        {
            Status1.Text = "";
        }

        private void toolButton1_MouseDown(object sender, MouseEventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.SunkenInner;
            updateToolButton.BackColor = Color.SeaShell;
        }
        private void toolButton1_MouseUp(object sender, MouseEventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.RaisedInner;
            updateToolButton.BackColor = Color.Snow;
        }
        private void toolButton1_MouseEnter(object sender, EventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.RaisedInner;
            updateToolButton.BackColor = Color.Snow;
        }
        private void toolButton1_MouseLeave(object sender, EventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.RaisedOuter;
            updateToolButton.BackColor = Color.MintCream;
        }

        #endregion

        void LoadProblems()
        {            
            problemListView.SetObjects(ProblemDatabase.problem_list);
            problemListView.Sort(0);
        }

        private void problemListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            ProblemList plist = (ProblemList)e.Model;
            if(e.Column == ptitleProb)
            {
                FontStyle fs = FontStyle.Regular;
                if (plist.stat == 2) fs = FontStyle.Italic;
                else if (plist.stat == 0) fs = FontStyle.Strikeout;
                e.SubItem.Font = new Font("Segoe UI Semibold", problemListView.Font.Size, fs);
            }
        }
    }
}

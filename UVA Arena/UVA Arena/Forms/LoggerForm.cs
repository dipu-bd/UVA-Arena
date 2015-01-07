using System;
using System.Drawing;
using System.Windows.Forms;
using UVA_Arena.Internet;

namespace UVA_Arena
{
    public partial class LoggerForm : Form
    {
        #region Loader Functions

        public LoggerForm()
        {
            InitializeComponent();
        }

        private void LoggerForm_Load(object sender, EventArgs e)
        {
            InitOthers();
        }

        private void InitOthers()
        {
            TaskQueue.AddTask(RefreshList, 100);

            nameINT.AspectGetter = delegate(object row)
            {
                if (row == null) return null;
                Internet.DownloadTask task = (Internet.DownloadTask)row;
                if (task.IsSaveToFile) return System.IO.Path.GetFileName(task.FileName);
                else return task.Url.AbsolutePath;
            };
            receiveINT.AspectToStringConverter =
                totalINT.AspectToStringConverter = delegate(object key)
                {
                    if (key == null) return "";
                    return Functions.FormatMemory((long)key);
                };
            funcTask.AspectToStringConverter = delegate(object key)
            {
                if (key == null) return "";
                TaskQueue.Function func = (TaskQueue.Function)key;
                string target = func.Target.ToString();
                string name = func.Method.ReturnType.Name.ToLower() + " " + func.Method.Name + "()";
                string other = func.Method.Attributes.ToString();
                return target + " | " + name + " | " + other;
            };
        }

        private void RefreshList()
        {
            if (this.Disposing || this.IsDisposed) return;
            
            Status1.Text = "";
            if (tabControl1.SelectedIndex == 0 && Logger.LOG != null)
            {
                activityList.SetObjects(Logger.LOG);
                activityList.Sort(dateTime, SortOrder.Ascending);
                source.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                status.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                if (activityList.SelectedIndex == 0) //goto end enabled                
                    activityList.GetLastItemInDisplayOrder().EnsureVisible();                               

                Status1.Text = "Log size : " + Logger.LOG.Count.ToString();
            }

            if (tabControl1.SelectedIndex == 1 && Downloader.DownloadQueue != null)
            { 
                downloadQueue1.SetObjects(Downloader.DownloadQueue);
                
                downloadQueue1.BuildGroups();
                downloadQueue1.ShowGroups = true;
                downloadQueue1.Sort(statINT);

                if (Downloader.IsBusy()) Status1.Text = "Downloading...";
                else Status1.Text = "Stopped";
            }

            if (tabControl1.SelectedIndex == 2 && TaskQueue.queue != null)
            {
                taskQueue1.SetObjects(TaskQueue.queue);
                funcTask.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); 

                Status1.Text = "Interval = " + Functions.FormatRuntime(TaskQueue.timer1.Interval);
            }

            TaskQueue.AddTask(new TaskQueue.Task(RefreshList, 100));
        }

        #endregion

        #region Activity Log

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            gotoEndButton.Visible = (tabControl1.SelectedIndex == 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            activityList.SelectedIndices.Clear();
        }

        private void objectListView1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Column == dateTime)
            {
                e.SubItem.Font = new Font(activityList.Font, FontStyle.Italic);
                e.SubItem.ForeColor = Color.DarkSlateBlue;
            }
            else if (e.Column == source)
            {
                e.SubItem.Font = new Font("Segoe UI Semibold", 9.0F, FontStyle.Regular);
                e.SubItem.ForeColor = Color.DarkGreen;
            }
        }

        #endregion

        #region Download Queue

        private void downloadQueue1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Column == uriINT)
            {
                e.SubItem.ForeColor = Color.Blue;
            }
            else if (e.Column == fileINT)
            {
                e.SubItem.ForeColor = Color.Red;
            }
            else if (e.Column == percentINT)
            {
                e.SubItem.ForeColor = Color.Navy;
            }
            else if (e.Column == priorityINT)
            {
                e.SubItem.ForeColor = Color.Maroon;
            }
        } 

        private void cancelTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (downloadQueue1.SelectedObject == null) return;
            Internet.DownloadTask task = (Internet.DownloadTask)downloadQueue1.SelectedObject;
            task.Cancel();
        }

        private void forceStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Downloader.StopDownload();
        }

        private void forceStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Downloader.StartDownload();
        }

        #endregion

    }
}

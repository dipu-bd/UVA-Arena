using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Net;
using System.Windows.Forms;
using UVA_Arena.Structures;
using UVA_Arena.Internet;
using Microsoft.Win32;
using BrightIdeasSoftware;

namespace UVA_Arena.Elements
{
    public partial class PROBLEMS : UserControl
    {
        #region STARTUP

        public PROBLEMS()
        {
            InitializeComponent();
            SetObjectListView();
            problemListView.MakeColumnSelectMenu(problemContextMenu);
            //Stylish.SetGradientBackground(plistLabel, new Stylish.GradientStyle(
            //        Color.PaleTurquoise, Color.LightBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical));
        }

        public void ExpandView(bool val)
        {
            splitContainer1.Panel1Collapsed = val;
        }

        private void SetObjectListView()
        {
            pnumProb.GroupKeyGetter = delegate(object row)
            {
                return ((ProblemInfo)row).volume;
            };
            pnumProb.GroupKeyToTitleConverter = delegate(object key)
            {
                return string.Format("Volume {0:000}", key);
            };

            levelProb.AspectGetter = delegate(object row)
            {
                ProblemInfo pl = (ProblemInfo)row;
                return string.Format("{0:00}{1}", pl.level, pl.star);
            };
            levelProb.AspectToStringConverter = delegate(object key)
            {
                string txt = (string)key;
                if (txt[0] == '0') txt = txt.Remove(0, 1);
                return txt;
            };
            levelProb.GroupKeyToTitleConverter = delegate(object key)
            {
                string txt = levelProb.AspectToStringConverter(key);
                return string.Format("Level {0}", txt);
            };

            rtlProb.AspectToStringConverter = delegate(object key)
            {
                return Functions.FormatRuntime((long)key);
            };
            runProb.AspectToStringConverter = delegate(object key)
            {
                if ((long)key < 0) return "(?)";
                else return Functions.FormatRuntime((long)key);
            };
            memProb.AspectToStringConverter = delegate(object key)
            {
                if ((long)key < 0) return "(512MB)";
                else return Functions.FormatMemory((long)key);
            };
        }

        #endregion

        #region StatusBar

        public void SetStatus(string text, int timeout = 1000)
        {
            Status1.Text = text;
            TaskQueue.AddTask(ClearStatus, timeout);
            Logger.Add(text, "Problems");
        }
        public void ClearStatus()
        {
            Status1.Text = "";
        }


        //
        //Problem List Downloader
        //
        private void updateToolButton_Click(object sender, EventArgs e)
        {
            Status1.Text = "Updating problem database...";
            Downloader.DownloadProblemDatabase(problemWorkerStateChanged, problemWorkerProgress);
        }

        private void problemWorkerProgress(int percent, DownloadTask task)
        {
            Status1.Text = string.Format("Downloading problem database... ({0}/{1} completed)",
                Functions.FormatMemory(task.received), Functions.FormatMemory(task.total));
            Progress1.Value = percent;
        }
        private void problemWorkerStateChanged(DownloadTask task)
        {
            if (task.status == ProgressStatus.Completed)
                SetStatus("Problem database is successfully updated.");
            else
                SetStatus("Failed to update problem database. See log for details.");
        }
        
        //
        // Update Tool Button
        //
        private void toolButton1_MouseDown(object sender, MouseEventArgs e)
        {            
            updateToolButton.BorderStyle = Border3DStyle.SunkenInner;
            updateToolButton.BackColor = Color.MediumTurquoise;
        }
        private void toolButton1_MouseUp(object sender, MouseEventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.RaisedInner;
            updateToolButton.BackColor = Color.SeaShell;
            updateToolButton.BackgroundImage = null;
        }
        private void toolButton1_MouseEnter(object sender, EventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.RaisedInner;

            Stylish.GradientStyle style = new Stylish.GradientStyle(
                System.Drawing.Drawing2D.HatchStyle.Trellis, Color.Linen, Color.PaleTurquoise);
            updateToolButton.BackgroundImage = style.GetImage(updateToolButton.Size);                
        }
        private void toolButton1_MouseLeave(object sender, EventArgs e)
        {
            updateToolButton.BorderStyle = Border3DStyle.RaisedOuter;
            updateToolButton.BackColor = Color.Linen;
            updateToolButton.BackgroundImage = null;
        }

        #endregion

        #region Problem List Loader

        //
        // Loaders
        //
        public void LoadVolumes()
        {
            volumesButton.Checked = true;
            catagoryButton.Checked = false;

            if (ProblemDatabase.problem_vol == null) return;

            List<CatagoryList> volumes = new List<CatagoryList>();
            var it = ProblemDatabase.problem_vol.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Value.Count == 0) continue;
                CatagoryList cl = new CatagoryList();
                cl.name = string.Format("Volume {0:000}", it.Current.Key);
                cl.tag = it.Current.Key;
                cl.count = it.Current.Value.Count;
                volumes.Add(cl);
            }

            catagoryListView.SetObjects(volumes);
            catagoryListView.Sort(0);
        }

        public void LoadCatagory()
        {
            volumesButton.Checked = false;
            catagoryButton.Checked = true;

            if (ProblemDatabase.problem_cat == null) return;

            List<CatagoryList> catagory = new List<CatagoryList>();
            var it = ProblemDatabase.problem_cat.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Value.Count == 0) continue;
                CatagoryList cl = new CatagoryList();
                cl.name = it.Current.Key;
                cl.tag = it.Current.Key;
                cl.count = it.Current.Value.Count;
                catagory.Add(cl);
            }

            catagoryListView.SetObjects(catagory);
            catagoryListView.Sort(0);
        }

        public void LoadProblems()
        {
            searchBox1.SearchText = "";
            allProbButton.Checked = true;
            favouriteButton.Checked = false;
            plistLabel.Text = "All Problems";

            if (ProblemDatabase.problem_list == null) return;

            problemListView.SetObjects(ProblemDatabase.problem_list);
            problemListView.Sort(pnumProb);
        }

        public void LoadFavourites()
        {
            searchBox1.SearchText = "";
            allProbButton.Checked = false;
            favouriteButton.Checked = true;
            plistLabel.Text = "Marked Problems";

            List<long> fav = RegistryAccess.FavouriteProblems;
            List<ProblemInfo> plist = new List<ProblemInfo>();
            foreach (long pnum in fav)
            {
                if (ProblemDatabase.HasProblem(pnum))
                    plist.Add(ProblemDatabase.GetProblem(pnum));
            }
            if (plist.Count > 0)
            {
                problemListView.ShowGroups = false;
                problemListView.SetObjects(plist);
            }
        }

        public void ShowVolume(long vol)
        {
            searchBox1.SearchText = "";
            allProbButton.Checked = false;
            favouriteButton.Checked = false;
            plistLabel.Text = string.Format("Volume {0:000}", vol);

            if (ProblemDatabase.problem_vol == null) return;

            problemListView.SetObjects(ProblemDatabase.GetVolume(vol));
            countVol.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            problemListView.Sort(0);
        }

        public void ShowCatagory(string cat)
        {
            if (string.IsNullOrEmpty(cat)) return;

            searchBox1.SearchText = "";
            allProbButton.Checked = false;
            favouriteButton.Checked = false;
            plistLabel.Text = cat;

            if (ProblemDatabase.problem_cat == null) return;

            problemListView.SetObjects(ProblemDatabase.GetCatagory(cat));
            countVol.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            problemListView.Sort(0);
        }

        //
        // Radio Buttons
        //
        private void volumesButton_Click(object sender, EventArgs e)
        {
            if (!volumesButton.Checked) LoadVolumes();
        }

        private void catagoryButton_Click(object sender, EventArgs e)
        {
            if (!catagoryButton.Checked) LoadCatagory();
        }

        private void favouriteButton_Click(object sender, EventArgs e)
        {
            if (!favouriteButton.Checked) LoadFavourites();
        }

        private void allProbButton_Click(object sender, EventArgs e)
        {
            if (!allProbButton.Checked) LoadProblems();
        }

        #endregion

        #region List View Events

        //
        // Volume List Events
        //
        private void volumeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object sel = catagoryListView.SelectedObject;
            if (sel == null) return;

            if (volumesButton.Checked)
                ShowVolume((long)((CatagoryList)sel).tag);
            else
                ShowCatagory((string)((CatagoryList)sel).tag);
        }

        //
        // Problem List Events
        //
        private void problemListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object sel = problemListView.SelectedObject;
            if (sel == null) return;
            this.problemViewer1.LoadProblem((ProblemInfo)sel);
        }

        private void problemListView_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {
            try
            {
                if (e.ColumnToSort == pnumProb || e.ColumnToSort == levelProb)
                {
                    problemListView.ShowGroups = true;
                }
                else
                {
                    problemListView.ShowGroups = false;
                }
            }
            catch { }
        }

        private void problemListView_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            ProblemInfo plist = (ProblemInfo)e.Model;
            e.Title = string.Format("{0} - {1}", plist.pnum, plist.ptitle);
            e.Text = string.Format("Timelimit : {1}\nStatus : {2}\n" +
                "Hardness Level : {0}\nAccepted Percentage : {3:0.00}%",
                plist.level, runProb.AspectToStringConverter(plist.run),
                plist.status, 100 * (double)plist.ac / plist.total);
        }

        //
        // Filter Problem List
        //
        private void searchBox1_SearchTextChanged(object sender, EventArgs e)
        {
            if (searchBox1.SearchText.Length == 0)
            {
                problemListView.DefaultRenderer = null;
                problemListView.AdditionalFilter = null;
                allProbButton.Checked = true;
                if (problemListView.Groups.Count > 0)
                    problemListView.ShowGroups = true;
            }
            else
            {
                TextMatchFilter filter = new TextMatchFilter(problemListView,
                    searchBox1.SearchText, StringComparison.OrdinalIgnoreCase);
                allProbButton.Checked = false;
                problemListView.ShowGroups = false;
                problemListView.DefaultRenderer = new HighlightTextRenderer(filter);
                problemListView.AdditionalFilter = filter;
            }
        }

        //
        // Listview formatter
        //
        private void ListView_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            float size = 8.5F;
            FontStyle fs = FontStyle.Regular;
            string font = "Consolas";
            Color fore = Color.DarkCyan;

            if (e.Column == ptitleProb)
            {
                ProblemInfo plist = (ProblemInfo)e.Model;
                size = 9.0F;
                fore = Color.Black;
                font = "Segoe UI Semibold";
                if (plist.stat == 0) fs = FontStyle.Strikeout;
                if (plist.stat == 1) fs = FontStyle.Regular;
                if (plist.stat == 2) fs = FontStyle.Italic;
            }
            else if (e.Column == levelProb)
            {
                font = "Segoe UI Semibold";
                size = 9.0F;
                fore = Color.DarkRed;
            }
            else if (e.Column == pnumProb)
            {
                size = 9.0F;
                fs = FontStyle.Bold;
                fore = Color.DarkSlateBlue;
            }
            else if (e.Column == dacuProb)
            {
                size = 9.0F;
                fs = FontStyle.Bold;
                fore = Color.Navy;
            }
            else if (e.Column == totalProb)
            {
                size = 9.0F;
                fore = Color.DarkBlue;
            }
            else if (e.Column == rtlProb || e.Column == waProb)
            {
                size = 9.0F;
                fore = Color.Maroon;
            }
            else if (e.Column == runProb || e.Column == acProb)
            {
                size = 9.0F;
                fore = Color.Blue;
            }
            else if (e.Column == statProb)
            {
                size = 8.0F;
                fore = Color.Black;
            }
            else if (e.Column == nameVol)
            {
                size = 8.25F;
                font = "Segoe UI Semibold";
                fore = Color.Black;
            }
            else if (e.Column == countVol)
            {
                size = 8.0F;
            }

            e.SubItem.Font = new Font(font, size, fs);
            e.SubItem.ForeColor = fore;
        }


        #endregion

        #region Problem List Context

        //
        // Major Tasks
        //
        private void downloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowDownloadAllForm();
        }

        private void exportDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.BackupData();
        }

        private void importDownloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions.RestoreData();
        }

        //
        //other simple tasks
        //
        private void viewPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.pdfButton.PerformClick();
        }

        private void openInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.externalButton.PerformClick();
        }

        private void viewSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.codeButton.PerformClick();
        }

        private void submitCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.submitButton.PerformClick();
        }

        private void markAsFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.markButton.PerformClick();
            markAsFavouriteToolStripMenuItem.Checked = Interactivity.problemViewer.markButton.Checked;
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.reloadButton.PerformClick();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            problemListView.Refresh();
        }

        #endregion

    }
}

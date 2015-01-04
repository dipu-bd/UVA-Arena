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

            //other initial codes
            SetAspectValues();
            problemListView.MakeColumnSelectMenu(problemContextMenu);             

            //add problem viewer
            Interactivity.problemViewer = new Elements.ProblemViewer();
            Interactivity.problemViewer.Dock = DockStyle.Fill;
            mainSplitContainer.Panel2.Controls.Add(Interactivity.problemViewer);            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //load volume and problems data at once
            if (LocalDatabase.IsReady)
                Interactivity.ProblemDatabaseUpdated();

            CustomStatusButton.Initialize(updateToolButton); 

            Stylish.SetGradientBackground(plistPanel, 
                new Stylish.GradientStyle(Color.LightSteelBlue, Color.PowderBlue, 90F));
        }

        public bool ExpandCollapseView()
        {
            bool val = !mainSplitContainer.Panel1Collapsed;
            if (val)
            {
                Interactivity.problemViewer.expandCollapseButton.Image
                            = UVA_Arena.Properties.Resources.next;
            }
            else
            {
                Interactivity.problemViewer.expandCollapseButton.Image
                            = UVA_Arena.Properties.Resources.prev;
            }
            mainSplitContainer.Panel1Collapsed = val;
            return val;
        }

        public void RefreshProblemList()
        {
            if (plistLabel.Tag == null)
            {
                if (favoriteButton.Checked)
                {
                    if (hideAccepted.Checked)
                        hideAccepted.Checked = false;
                }
                else
                {
                    ShowAllProblems();
                }
            }
            else
            {
                if (plistLabel.Tag.GetType() == typeof(long))
                    ShowVolume((long)plistLabel.Tag);
                else
                    ShowCategory((string)plistLabel.Tag);
            }
        }

        #endregion

        #region StatusBar

        public void SetStatus(string text, int timeout = 1000)
        {
            if (this.IsDisposed) return;
            Status1.Text = text;
            TaskQueue.AddTask(ClearStatus, timeout);
        }
        public void ClearStatus()
        {
            if (this.IsDisposed) return;
            Status1.Text = "";
        }


        //
        //Problem List Downloader
        //
        private void updateToolButton_Click(object sender, EventArgs e)
        {
            Downloader.DownloadProblemDatabase();
        }

        public void problemWorkerProgress(DownloadTask task)
        {
            if (this.IsDisposed) return;
            Status1.Text = string.Format("Downloading problem database... ({0}/{1} completed)",
                Functions.FormatMemory(task.Received), Functions.FormatMemory(task.Total));
            Progress1.Value = task.ProgressPercentage;
        }

        public void problemWorkerCompleted(DownloadTask task)
        {
            string msg = "";
            if (task.Status == ProgressStatus.Completed)
                msg = "Problem database is successfully updated.";
            else
                msg = "Failed to update problem database. See log for details.";

            SetStatus(msg);
            Logger.Add(msg, "Problems|problemWorkerCompleted");
        }

        #endregion

        #region Problem Loader

        //
        // Loaders
        //
        public void LoadVolumes()
        {
            filterBox1.SearchText = "";
            volumesButton.Checked = true;
            categoryButton.Checked = false;

            if (LocalDatabase.problem_vol == null) return;

            List<CategoryList> volumes = new List<CategoryList>();
            var it = LocalDatabase.problem_vol.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Value.Count == 0) continue;
                CategoryList cl = new CategoryList();
                cl.name = string.Format("Volume {0:000}", it.Current.Key);
                cl.tag = it.Current.Key;
                cl.count = it.Current.Value.Count;
                volumes.Add(cl);
            }


            categoryListView.SetObjects(volumes);
            categoryListView.Sort(0);

            countVol.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void LoadCategory()
        {
            filterBox1.SearchText = "";
            volumesButton.Checked = false;
            categoryButton.Checked = true;

            if (LocalDatabase.problem_cat == null) return;

            List<CategoryList> category = new List<CategoryList>();
            var it = LocalDatabase.problem_cat.GetEnumerator();
            while (it.MoveNext())
            {
                if (it.Current.Value.Count == 0) continue;
                CategoryList cl = new CategoryList();
                cl.name = it.Current.Key;
                cl.tag = it.Current.Key;
                cl.count = it.Current.Value.Count;
                category.Add(cl);
            }
            it.Dispose();

            categoryListView.SetObjects(category);
            categoryListView.Sort(0);
        }

        #endregion

        #region Show Problem List

        //
        // Show List of problems
        //

        public void ShowAllProblems()
        {
            searchBox1.SearchText = "";
            allProbButton.Checked = true;
            favoriteButton.Checked = false;
            plistLabel.Text = "All Problems";
            plistLabel.Tag = null;

            if (LocalDatabase.problem_list == null) return;

            _SetObjects(LocalDatabase.problem_list, true);
        }

        public void ShowFavorites()
        {
            searchBox1.SearchText = "";
            allProbButton.Checked = false;
            favoriteButton.Checked = true;
            plistLabel.Text = "Marked Problems";
            plistLabel.Tag = null;

            List<ProblemInfo> favorite = new List<ProblemInfo>();
            foreach (long pnum in RegistryAccess.FavoriteProblems)
            {
                if (LocalDatabase.HasProblem(pnum))
                    favorite.Add(LocalDatabase.GetProblem(pnum));
            }

            hideAccepted.Checked = false;
            problemListView.SetObjects(favorite);
        }

        public void ShowVolume(long vol)
        {
            searchBox1.SearchText = "";
            allProbButton.Checked = false;
            favoriteButton.Checked = false;
            plistLabel.Tag = vol;
            plistLabel.Text = string.Format("Volume {0:000}", vol);

            _SetObjects(LocalDatabase.GetVolume(vol));
        }

        public void ShowCategory(string cat)
        {
            if (string.IsNullOrEmpty(cat)) return;

            searchBox1.SearchText = "";
            allProbButton.Checked = false;
            favoriteButton.Checked = false;
            plistLabel.Tag = cat;
            plistLabel.Text = cat;

            _SetObjects(LocalDatabase.GetCategory(cat));
        }

        private void _SetObjects(List<ProblemInfo> list, bool regroup = false)
        {
            List<ProblemInfo> display = new List<ProblemInfo>();
            problemListView.SetObjects(display);

            if (list == null) return;

            if (LocalDatabase.DefaultUser == null || !hideAccepted.Checked)
            {
                problemListView.SetObjects(list);
            }
            else
            {
                foreach (ProblemInfo prob in list)
                {
                    if (!LocalDatabase.DefaultUser.IsSolved(prob.pnum))
                        display.Add(prob);
                }
                problemListView.SetObjects(display);
            }

            if (regroup) problemListView.Sort(0);
            else problemListView.Sort();
        }

        #endregion

        #region View Problem Events


        //
        // Radio Buttons
        //
        private void volumesButton_Click(object sender, EventArgs e)
        {
            if (!volumesButton.Checked) LoadVolumes();
        }

        private void categoryButton_Click(object sender, EventArgs e)
        {
            if (!categoryButton.Checked) LoadCategory();
        }

        private void favoriteButton_Click(object sender, EventArgs e)
        {
            if (!favoriteButton.Checked) ShowFavorites();
        }

        private void allProbButton_Click(object sender, EventArgs e)
        {
            if (!allProbButton.Checked) ShowAllProblems();
        }

        private void hideAccepted_CheckedChanged(object sender, EventArgs e)
        {
            RefreshProblemList();

            //set text
            int indx = hideAccepted.Checked ? 1 : 0;
            string txt = (string)hideAccepted.Tag;
            hideAccepted.Text = txt.Split(new char[] { '|' })[indx];
        }



        //
        // Volume List Events
        //
        private void volumeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object sel = categoryListView.SelectedObject;
            if (sel == null) return;

            if (volumesButton.Checked)
                ShowVolume((long)((CategoryList)sel).tag);
            else
                ShowCategory((string)((CategoryList)sel).tag);
        }

        #endregion

        #region Problem and Catagory List View

        //
        // Problem List Events
        //
        private void problemListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            object sel = problemListView.SelectedObject;
            if (sel == null) return;
            Interactivity.problemViewer.LoadProblem((ProblemInfo)sel);
        }

        private void problemListView_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {
            try
            {
                if (e.ColumnToSort == pnumProb ||
                    e.ColumnToSort == levelProb ||
                    e.ColumnToSort == priorityProb)
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

        //
        // Listview formatter
        //

        private void SetAspectValues()
        {
            pnumProb.GroupKeyGetter = delegate(object row)
            {
                return ((ProblemInfo)row).volume;
            };
            levelProb.GroupKeyGetter = delegate(object row)
            {
                return Math.Round(((ProblemInfo)row).level);
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
                font = "Segoe UI Semibold";
                //set color 
                fore = Functions.GetProblemTitleColor(plist.pnum);
                //set style
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

        #region  Context Menu

        private void problemContextMenu_Opening(object sender, CancelEventArgs e)
        {
            bool marked = false;
            if (problemListView.SelectedObject != null)
            {
                ProblemInfo pinfo = (ProblemInfo)problemListView.SelectedObject;
                marked = pinfo.marked;
            }
            if (marked) markAsFavorite.Text = "Remove From Favorite";
            else markAsFavorite.Text = "Mark As Favorite";
        }

        //
        // Major Tasks
        //
        private void downloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowDownloadAllForm();
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

        private void markAsFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.problemViewer.markButton.PerformClick();
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

        #region Search and Filter


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
                if (!deepSearchCheckBox.Checked)
                    problemListView.AdditionalFilter = filter;
            }
        }

        //
        // Filter Category and Volume list
        //
        private void searchBox2_SearchTextChanged(object sender, EventArgs e)
        {
            if (filterBox1.SearchText.Length == 0)
            {
                categoryListView.DefaultRenderer = null;
                categoryListView.AdditionalFilter = null;
            }
            else
            {
                TextMatchFilter filter = new TextMatchFilter(categoryListView,
                    filterBox1.SearchText, StringComparison.OrdinalIgnoreCase);
                categoryListView.DefaultRenderer = new HighlightTextRenderer(filter);
                categoryListView.AdditionalFilter = filter;
            }
        }


        //
        // Deep Saerch
        //

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            searchBox1.SearchButtonVisible = deepSearchCheckBox.Checked;
        }

        private void searchBox1_SearchButtonClicked(object sender, EventArgs e)
        {
            if (!LocalDatabase.IsReady) return;
            if (_InDeepSearch) return;

            string src = searchBox1.SearchText;
            if (src.Length < 3) return;
            if (!src.StartsWith("*")) src = "\\b" + src;
            if (!src.EndsWith("*")) src += "\\b";

            //run new thread
            _InDeepSearch = true;
            _CancelSearch = false;
            ShowDeepSearchStatus();
            cancelDeepSearchButton.Visible = true;
            System.Threading.ThreadPool.QueueUserWorkItem(LoadDeepSearch, src);
        }
        private void cancelDeepSearchButton_Click(object sender, EventArgs e)
        {
            if (_InDeepSearch) _CancelSearch = true;
        }

        private void searchBox1_ClearButtonClicked(object sender, EventArgs e)
        {
            _CancelSearch = true;
        }

        private bool _InDeepSearch = false;
        private bool _CancelSearch = false;
        private long _DeepSearchProgress = 0;
        List<ProblemInfo> _deepSearchList = new List<ProblemInfo>();

        public void LoadDeepSearch(object state)
        {
            _InDeepSearch = true;

            if (LocalDatabase.problem_list == null) return;

            //search string
            string src = (string)state;
            var regex = new System.Text.RegularExpressions.Regex(src,
                            System.Text.RegularExpressions.RegexOptions.Compiled |
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //clear data
            _DeepSearchProgress = 0;
            _deepSearchList.Clear();

            //set data
            this.BeginInvoke((MethodInvoker)delegate
            {
                allProbButton.Checked = false;
                favoriteButton.Checked = false;
                plistLabel.Text = "Deep Search Result";

                problemListView.SetObjects(_deepSearchList);
                problemListView.Sort(priorityProb, SortOrder.Descending);
            });

            //search
            foreach (ProblemInfo prob in LocalDatabase.problem_list)
            {
                prob.priority = 0;

                //search in problem data
                prob.priority += regex.Matches(prob.ToString()).Count;

                //search in problem description
                try
                {
                    string file = LocalDirectory.GetProblemHtml(prob.pnum);
                    if (LocalDirectory.GetFileSize(file) > 100)
                    {
                        var hdoc = new HtmlAgilityPack.HtmlDocument();
                        hdoc.Load(file);
                        string dat = hdoc.DocumentNode.InnerText;
                        //string dat = System.IO.File.ReadAllText(file);
                        prob.priority += regex.Matches(dat).Count;
                    }
                }
                catch { }

                //add to list
                if (prob.priority > 0)
                    _deepSearchList.Add(prob);

                //complete
                _DeepSearchProgress++;
                if (_CancelSearch) break;
            }

            this.BeginInvoke((MethodInvoker)delegate
            {
                problemListView.SetObjects(_deepSearchList);
                cancelDeepSearchButton.Visible = false;
            });

            _InDeepSearch = false;
        }

        private void ShowDeepSearchStatus()
        {
            if (this.IsDisposed) return;

            if (_InDeepSearch)
            {
                int total = LocalDatabase.problem_list.Count;
                Progress1.Value = (int)(100 * _DeepSearchProgress / total);
                Status1.Text = "Searching... ";
                Status1.Text += string.Format("[{0} of {1} problems completed.]", _DeepSearchProgress, total);

                problemListView.SetObjects(_deepSearchList);
                problemListView.Sort(priorityProb, SortOrder.Descending);

                TaskQueue.AddTask(ShowDeepSearchStatus, 1000);
            }
            else
            {
                Progress1.Value = 0;
                if (_CancelSearch)
                    SetStatus("Search Cancelled.");
                else
                    SetStatus("Search Completed.");
            }

        }

        #endregion

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {            
        }

    }
}

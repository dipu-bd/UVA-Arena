using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UVA_Arena.Internet;
using UVA_Arena.Structures;

namespace UVA_Arena.Elements
{
    public partial class PROBLEMS : UserControl
    {
        #region STARTUP

        public PROBLEMS()
        {
            InitializeComponent();

            //other initial codes
            InitializeProblemList();
            InitializeCategoryList();

            problemViewSplitContainer.SplitterDistance = Properties.Settings.Default.ProblemSubSplitterDistance;

            mainSplitContainer.SplitterDistance =
                (int)Math.Round(mainSplitContainer.Width * Properties.Settings.Default.ProblemMainSplitterDistance);

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
            {
                Interactivity.ProblemDatabaseUpdated();
                Interactivity.CategoryDataUpdated();
            }

            Stylish.SetGradientBackground(plistPanel,
                new Stylish.GradientStyle(Color.LightCyan, Color.PaleTurquoise, -90F));
        }


        void InitializeCategoryList()
        {
            categoryListView.CanExpandGetter = delegate(object row)
            {
                return ((CategoryNode)row).branches.Count > 0;
            };
            categoryListView.ChildrenGetter = delegate(object row)
            {
                return ((CategoryNode)row).branches;
            };
            categoryListView.CellToolTipGetter = delegate(OLVColumn col, object row)
            {
                return ((CategoryNode)row).note;
            };

            // You can change the way the connection lines are drawn by changing the pen
            var renderer = categoryListView.TreeColumnRenderer;
            renderer.LinePen = new Pen(Color.Indigo, 0.5f);
            renderer.LinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
        }

        private void InitializeProblemList()
        {
            problemListView.MakeColumnSelectMenu(problemContextMenu);

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
            fileSizeProb.AspectToStringConverter = delegate(object key)
            {
                if ((long)key < 100) return "-";
                else return Functions.FormatMemory((long)key);
            };
        }

        #endregion

        #region Global Task

        public bool ExpandCollapseView()
        {
            bool val = !mainSplitContainer.Panel1Collapsed;
            if (val)
            {
                Interactivity.problemViewer.expandCollapseButton.Image
                            = UVA_Arena.Properties.Resources.show;
            }
            else
            {
                Interactivity.problemViewer.expandCollapseButton.Image
                            = UVA_Arena.Properties.Resources.hide;
            }
            mainSplitContainer.Panel1Collapsed = val;
            return val;
        }

        public void RefreshProblemList()
        {
            if (deepSearchCheckBox.Checked)
            {
                if (_deepSearchRes != null)
                {
                    _SetObjects(_deepSearchRes);
                    problemListView.Sort(priorityProb, SortOrder.Descending);
                }
                return;
            }
            if (plistLabel.Tag == null)
            {
                if (favoriteButton.Checked)
                    ShowFavorites();
                else
                    ShowAllProblems();
            }
            else
            {
                if (categoryListView.SelectedObject != null)
                {
                    var nod = (CategoryNode)categoryListView.SelectedObject;
                    _SetObjects(nod.allProbs.Values);
                }
            }
        }

        private void mainSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Properties.Settings.Default.ProblemMainSplitterDistance =
                (double)mainSplitContainer.SplitterDistance / mainSplitContainer.Width;
        }

        private void problemViewSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Properties.Settings.Default.ProblemSubSplitterDistance = problemViewSplitContainer.SplitterDistance;
        }

        #endregion

        #region Load and Show Problem List

        //
        // Loaders
        // 
        public void LoadCategory()
        {
            try
            {
                filterBox1.SearchText = "";
                if (LocalDatabase.categoryRoot == null) return;

                categoryListView.Roots = LocalDatabase.categoryRoot.branches;
                foreach (var b in categoryListView.Roots)
                {
                    categoryListView.Expand(b);
                }
                categoryListView.EnsureVisible(0);
                categoryListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            catch { }
        }

        public void ShowCategory()
        {
            object sel = categoryListView.SelectedObject;
            if (sel == null) return;

            allProbButton.Checked = false;
            favoriteButton.Checked = false;

            var nod = (CategoryNode)sel;
            plistLabel.Tag = nod;
            plistLabel.Text = nod.name;

            _SetObjects(nod.allProbs.Values);
        }

        //
        // Show List of problems
        //

        public void ShowAllProblems()
        {
            deepSearchCheckBox.Checked = false;
            searchBox1.SearchText = "";
            allProbButton.Checked = true;
            favoriteButton.Checked = false;
            plistLabel.Text = "All Problems";
            plistLabel.Tag = null;

            if (LocalDatabase.problemList == null) return;

            _SetObjects(LocalDatabase.problemList, true);
        }

        public void ShowFavorites()
        {
            deepSearchCheckBox.Checked = false;
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

            _SetObjects(favorite);
        }

        private void _SetObjects(IEnumerable<ProblemInfo> list, bool regroup = false)
        {
            problemListView.ClearObjects();
            if (list == null) return;

            if (LocalDatabase.DefaultUser == null || !hideAccepted.Checked)
            {
                problemListView.SetObjects(list);
            }
            else
            {
                List<ProblemInfo> display = new List<ProblemInfo>();
                foreach (ProblemInfo prob in list)
                {
                    if (!LocalDatabase.DefaultUser.IsSolved(prob.pnum))
                    {
                        display.Add(prob);
                    }
                }
                problemListView.SetObjects(display);
            }

            if (regroup)
            {
                problemListView.Sort(pnumProb, SortOrder.Ascending);
            }
            else
            {
                problemListView.Sort();
            }
        }

        #endregion

        #region View Problem Events

        //
        // Volume List Events
        //
        private void volumeListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCategory();
        }

        //
        // Radio Buttons
        //

        private void favoriteButton_Click(object sender, EventArgs e)
        {
            if (!favoriteButton.Checked) ShowFavorites();
        }

        private void allProbButton_Click(object sender, EventArgs e)
        {
            if (!allProbButton.Checked) ShowAllProblems();
        }

        private void hideAccepted_Click(object sender, EventArgs e)
        {
            hideAccepted.Checked = !hideAccepted.Checked;
            RefreshProblemList();

            //set text
            int indx = hideAccepted.Checked ? 1 : 0;
            string txt = (string)hideAccepted.Tag;
            hideAccepted.Text = txt.Split(new char[] { '|' })[indx];
        }

        #endregion

        #region Problem and Category List View

        private void categoryListView_Expanded(object sender, TreeBranchExpandedEventArgs e)
        {
            nameCat.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        //
        // Problem List Events
        //
        private void problemListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //object sel = problemListView.SelectedObject;
            //if (sel == null) return;
            //Interactivity.problemViewer.LoadProblem((ProblemInfo)sel);
        }
        private void problemListView_CellClick(object sender, CellClickEventArgs e)
        {
            object sel = problemListView.SelectedObject;
            if (sel == null) return;
            Interactivity.problemViewer.LoadProblem((ProblemInfo)sel);
        }

        private void problemListView_ItemActivate(object sender, EventArgs e)
        {
            object sel = problemListView.SelectedObject;
            if (sel == null) return;
            Interactivity.problemViewer.LoadProblem((ProblemInfo)sel);
        }

        private void problemListView_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {
            try
            {
                if (e.ColumnToSort == pnumProb)
                {
                    problemListView.ShowGroups = true;
                }
                else if (e.ColumnToSort == priorityProb)
                {
                    problemListView.ShowGroups = true;
                    e.SecondaryColumnToSort = pnumProb;
                    e.SecondarySortOrder = SortOrder.Ascending;
                }
                else if (e.ColumnToSort == levelProb)
                {
                    e.SecondaryColumnToSort = pnumProb;
                    e.SecondarySortOrder = SortOrder.Descending;
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
            else if (e.Column == nameCat)
            {
                size = 8.25F;
                font = "Segoe UI Semibold";
                fore = Color.Black;
                if (((CategoryNode)e.Model).Level == 0)
                {
                    size = 10F;
                    fore = Color.Maroon;
                    e.SubItem.BackColor = Color.PaleTurquoise;
                }
            }
            else if (e.Column == countCat)
            {
                size = 8.0F;
                if (((CategoryNode)e.Model).Level == 0)
                {
                    e.SubItem.BackColor = Color.PaleTurquoise;
                }
            }

            e.SubItem.Font = new Font(font, size, fs);
            e.SubItem.ForeColor = fore;
        }


        #endregion

        #region  Context Menu

        private void updateToolButton_Click(object sender, EventArgs e)
        {
            Downloader.DownloadProblemDatabase();
            Downloader.DownloadCategoryIndexFiles();
        }

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
        // Filter Category list
        //
        private void searchBox2_SearchTextChanged(object sender, EventArgs e)
        {
            categoryListView.SelectedObjects.Clear();
            if (filterBox1.SearchText.Length == 0)
            {
                categoryListView.DefaultRenderer = null;
                categoryListView.AdditionalFilter = null;
                categoryListView.CollapseAll();
                foreach (var b in categoryListView.Roots)
                {
                    categoryListView.Expand(b);
                }
                categoryListView.EnsureVisible(0);
            }
            else
            {
                TextMatchFilter filter = new TextMatchFilter(categoryListView,
                    filterBox1.SearchText, StringComparison.OrdinalIgnoreCase);
                categoryListView.ExpandAll();
                categoryListView.DefaultRenderer = new HighlightTextRenderer(filter);
                categoryListView.AdditionalFilter = filter;
            }
        }

        //
        // Filter Problem List
        //
        private void searchBox1_SearchTextChanged(object sender, EventArgs e)
        {
            problemListView.SelectedObjects.Clear();
            if (deepSearchCheckBox.Checked)
            {
                return;
            }

            if (searchBox1.SearchText.Length == 0)
            {
                ClearSearch();
            }
            else
            {
                problemListView.ShowGroups = false;

                TextMatchFilter filter = new TextMatchFilter(problemListView,
                   searchBox1.SearchText, StringComparison.OrdinalIgnoreCase);
                problemListView.DefaultRenderer = new HighlightTextRenderer(filter);
                problemListView.AdditionalFilter = filter;
            }
        }

        private void searchBox1_ClearButtonClicked(object sender, EventArgs e)
        {
            ClearSearch();
        }

        public void ClearSearch()
        {
            problemListView.DefaultRenderer = null;
            problemListView.AdditionalFilter = null;
            if (problemListView.Groups.Count > 0)
                problemListView.ShowGroups = true;

            if (deepSearchCheckBox.Checked)
            {
                if (_InDeepSearch)
                    _CancelSearch = true;
                else
                    ShowAllProblems();
            }
        }

        //
        // Deep Search
        //

        private void deepSearchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            searchBox1.SearchButtonVisible = deepSearchCheckBox.Checked;
        }

        private void searchBox1_SearchButtonClicked(object sender, EventArgs e)
        {
            if (!LocalDatabase.IsReady) return;
            if (_InDeepSearch) return;

            string src = searchBox1.SearchText;
            if (src.Length < 2) return;

            //run new thread
            _InDeepSearch = true;
            _CancelSearch = false;
            ShowDeepSearchStatus();
            System.Threading.ThreadPool.QueueUserWorkItem(LoadDeepSearch, src);
        }

        private void cancelDeepSearchButton_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private bool _InDeepSearch = false;
        private bool _CancelSearch = false;
        private long _DeepSearchProgress = 0;
        List<ProblemInfo> _deepSearchRes = new List<ProblemInfo>();

        public void LoadDeepSearch(object state)
        {
            if (LocalDatabase.problemList == null) return;

            _InDeepSearch = true;

            //clear data
            _deepSearchRes.Clear();
            _DeepSearchProgress = 0;

            //set data
            this.BeginInvoke((MethodInvoker)delegate
            {
                allProbButton.Checked = false;
                favoriteButton.Checked = false;
                plistLabel.Text = "Deep Search Result";
                problemListView.AdditionalFilter = null;

                searchBox1.search_text.ReadOnly = true;
                cancelDeepSearchButton.Visible = true;
                problemViewSplitContainer.Panel1.Enabled = false;

                _SetObjects(_deepSearchRes);
                problemListView.Sort(priorityProb, SortOrder.Descending);
            });

            //search string
            string src = (string)state;
            if (!src.StartsWith("*")) src = "\\b" + src;
            if (!src.EndsWith("*")) src += "\\b";
            var regex = new System.Text.RegularExpressions.Regex(src,
                            System.Text.RegularExpressions.RegexOptions.Compiled |
                            System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //search
            foreach (ProblemInfo prob in LocalDatabase.problemList)
            {
                if (_CancelSearch) break;
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
                        string dat = hdoc.DocumentNode.Element("body").InnerText;
                        //string dat = System.IO.File.ReadAllText(file);
                        prob.priority += regex.Matches(dat).Count;
                    }
                }
                catch { }

                //add to list
                if (prob.priority > 0)
                    _deepSearchRes.Add(prob);
                _DeepSearchProgress++;
            }

            //complete
            _InDeepSearch = false;
            this.BeginInvoke((MethodInvoker)delegate
            {
                _SetObjects(_deepSearchRes);

                searchBox1.search_text.ReadOnly = false;
                cancelDeepSearchButton.Visible = false;
                problemViewSplitContainer.Panel1.Enabled = true;

                if (_CancelSearch) //if canceled
                {
                    ClearSearch();
                }
            });
        }

        private void ShowDeepSearchStatus()
        {
            if (this.IsDisposed) return;

            if (_InDeepSearch)
            {
                int total = LocalDatabase.problemList.Count;
                Interactivity.SetProgress(_DeepSearchProgress, total);
                string msg = "Searching for problems... [{0} of {1} problems completed.]";
                Interactivity.SetStatus(string.Format(msg, _DeepSearchProgress, total));

                _SetObjects(_deepSearchRes);
                problemListView.Sort(priorityProb, SortOrder.Descending);

                TaskQueue.AddTask(ShowDeepSearchStatus, 1000);
            }
            else
            {
                Interactivity.SetProgress(0);
                if (_CancelSearch)
                    Interactivity.SetStatus("Deep search cancelled.");
                else
                    Interactivity.SetStatus("Deep search completed.");
            }

        }

        #endregion


    }
}

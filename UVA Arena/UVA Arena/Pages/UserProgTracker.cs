using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena.Elements
{
    public partial class UserProgTracker : UserControl
    {
        public UserProgTracker()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Stylish.SetGradientBackground(basicInfoTab,
                new Stylish.GradientStyle(Color.LightCyan, Color.LightBlue, 90));
        }

        public UserInfo currentUser = null;

        public void ShowUserInfo(UserInfo uinfo)
        {
            currentUser = uinfo;
            LoadCurrentUser(true);
        }

        private void prevTabButton_Click(object sender, EventArgs e)
        {
            int indx = (tabControl1.SelectedIndex - 1 + tabControl1.TabCount) % tabControl1.TabCount;
            tabControl1.SelectedIndex = indx;
        }

        private void nextTabButton_Click(object sender, EventArgs e)
        {
            int indx = (tabControl1.SelectedIndex + 1) % tabControl1.TabCount;
            tabControl1.SelectedIndex = indx;
        }

        #region Process List

        private long _firstSub;
        private UserRanklist userRank;
        private int _solvedCount, _unsolvedCount, _tryCount, _totalSubmission;
        private int _subInAnsiC, _subInCPP, _subInCPP11, _subInJava, _subInPascal;
        private double _acCount, _waCount, _tleCount, _reCount, _peCount, _ceCount, _oleCount, _subeCount, _mleCount;
        private ZedGraph.PointPairList _subOverTime = new ZedGraph.PointPairList();
        private ZedGraph.PointPairList _acOverTime = new ZedGraph.PointPairList();
        private ZedGraph.PointPairList _RankCount = new ZedGraph.PointPairList();

        private void ProcessList()
        {
            if (currentUser == null)
            {
                //currentUser = LocalDatabase.DefaultUser;
                currentUser = new UserInfo();
                currentUser.uid = currentUser.uname = currentUser.name = "[-]";
                currentUser.subs = new List<List<long>>();
                currentUser.Process();
            }

            userRank = RegistryAccess.GetUserRank(currentUser.uname);
            if (userRank == null)
            {
                userRank = new UserRanklist();
                userRank.username = currentUser.uname;
                userRank.rank = 0;
                userRank.activity = new List<long>();
                for (int i = 0; i < 5; ++i) userRank.activity.Add(0);
            }

            _acOverTime.Clear();
            _subOverTime.Clear();
            _RankCount.Clear();

            _firstSub = long.MaxValue;
            _solvedCount = _unsolvedCount = _tryCount = _totalSubmission = 0;
            _subInAnsiC = _subInCPP = _subInCPP11 = _subInJava = _subInPascal = 0;
            _acCount = _waCount = _tleCount = _reCount = _peCount = 0;
            _ceCount = _oleCount = _subeCount = _mleCount = 0;

            List<long> solved = new List<long>();

            foreach (UserSubmission usub in currentUser.submissions)
            {
                //total submission count
                _totalSubmission++;

                //age meter
                if (_firstSub > usub.sbt)
                    _firstSub = usub.sbt;

                //add rank count
                if (usub.IsAccepted())
                {
                    _RankCount.Add(usub.rank, usub.pid);

                    //solve count
                    if (!solved.Contains(usub.pnum))
                    {
                        _solvedCount++;
                        solved.Add(usub.pnum);
                    }
                }


                //language
                switch ((Language)usub.lan)
                {
                    case Language.C: _subInAnsiC++; break;
                    case Language.Java: _subInJava++; break;
                    case Language.Pascal: _subInPascal++; break;
                    case Language.CPP: _subInCPP++; break;
                    case Language.CPP11: _subInCPP11++; break;
                }

                //submissionPerTime
                double xval = new ZedGraph.XDate(UnixTimestamp.FromUnixTime(usub.sbt));
                if (_totalSubmission == 1) _subOverTime.Add(xval, 0);

                _subOverTime.Add(xval, _totalSubmission);
                _acOverTime.Add(xval, _solvedCount);

                //verdict
                switch ((Verdict)usub.ver)
                {
                    case Structures.Verdict.CompileError: _ceCount++; break;
                    case Structures.Verdict.RuntimeError: _reCount++; break;
                    case Structures.Verdict.OutputLimit: _oleCount++; break;
                    case Structures.Verdict.TimLimit: _tleCount++; break;
                    case Structures.Verdict.MemoryLimit: _mleCount++; break;
                    case Structures.Verdict.WrongAnswer: _waCount++; break;
                    case Structures.Verdict.PresentationError: _peCount++; break;
                    case Structures.Verdict.Accepted: _acCount++; break;
                }
            }

            //finalize
            _tryCount = currentUser.TryList.Count;
            _unsolvedCount = _tryCount - _solvedCount;
            _subOverTime.Add(new ZedGraph.XDate(DateTime.Now), _totalSubmission);
            _acOverTime.Add(new ZedGraph.XDate(DateTime.Now), _solvedCount);
        }

        #endregion

        #region Load Processed Data

        public void LoadCurrentUser(object background)
        {
            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(LoadCurrentUser, false);
                return;
            }

            //process data
            ProcessList();

            this.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    //normal text
                    useridLabel.Text = currentUser.uid;
                    usernameLabel.Text = currentUser.uname;
                    fullnameLabel.Text = currentUser.name;
                    totalsubLabel.Text = _totalSubmission.ToString();
                    acceptedLabel.Text = _solvedCount.ToString();
                    triednacLabel.Text = _unsolvedCount.ToString();
                    totalTriedLabel.Text = _tryCount.ToString();

                    //account age
                    if (_firstSub == long.MaxValue) accountAge.Text = "[-]";
                    else accountAge.Text = Functions.FormatTimeSpan(UnixTimestamp.GetTimeSpan(_firstSub));

                    //user rank
                    worldRankLabel.Text = userRank.rank.ToString();
                    last2DaysLabel.Text = userRank.day2.ToString();
                    last7Dayslabel.Text = userRank.day7.ToString();
                    lastMonthLabel.Text = userRank.day31.ToString();
                    last3MonthLabel.Text = userRank.month3.ToString();

                    //draw graphs
                    BuildSubPerLangGraph();
                    BuildSubPerDateGraph();
                    BuildSubPerVerGraph();
                    //BuildRankCloud();
                }
                catch { }
            });
        }

        private void BuildSubPerLangGraph()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.IsEnableZoom = false;
            zg1.IsShowPointValues = false;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.Azure, Color.FromArgb(245, 220, 250), 90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.Snow, Color.FromArgb(225, 240, 250), -90);

            pane.Title.Text = "Submission per Language";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Navy;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.FontSpec.Size = 12;
            pane.Legend.IsShowLegendSymbols = true;
            pane.Legend.Position = ZedGraph.LegendPos.TopCenter;

            double[] value = { _subInCPP, _subInJava, _subInAnsiC, _subInCPP11, _subInPascal };
            string[] label = { "C++", "Java", "ANSI C", "C++11", "Pascal" };
            Color[] color1 = { Color.Lime, Color.Blue, Color.Red, Color.Green, Color.Yellow };
            Color[] color2 = { Color.DarkOrange, Color.DarkBlue, Color.DarkRed, Color.DarkGreen, Color.YellowGreen };

            for (int i = 0; i < value.Length; ++i)
            {
                ZedGraph.PieItem pie = pane.AddPieSlice(value[i], color2[i], color1[i], 0, 0.04, label[i]);
                pie.LabelType = ZedGraph.PieLabelType.Name_Value;
                pie.LabelDetail.FontSpec.Border.IsVisible = false;
                pie.LabelDetail.FontSpec.Family = "Segoe UI Semibold";
                pie.LabelDetail.FontSpec.Size = 12;
                pie.LabelDetail.FontSpec.IsAntiAlias = false;
                pie.IsSelectable = true;
            }

            pane.AxisChange();

            this.subPerLanTab.Controls.Clear();
            this.subPerLanTab.Controls.Add(zg1);
        }

        private void BuildSubPerDateGraph()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.IsShowPointValues = true;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.MintCream, Color.FromArgb(245, 250, 210), -90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.Snow, Color.FromArgb(245, 250, 230), 90);

            pane.Title.Text = "Submission and Accepted over Time";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Navy;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.FontSpec.Size = 12;
            pane.Legend.Position = ZedGraph.LegendPos.TopCenter;

            pane.XAxis.Title.Text = "Date-Time";
            pane.XAxis.Title.FontSpec.Family = "Courier New";
            pane.XAxis.Title.FontSpec.FontColor = Color.Maroon;
            pane.XAxis.Title.FontSpec.IsBold = false;
            pane.XAxis.Type = ZedGraph.AxisType.Date;

            pane.YAxis.Title.Text = "Submissions";
            pane.YAxis.Title.FontSpec.Family = "Courier New";
            pane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            pane.YAxis.Title.FontSpec.IsBold = false;

            // Generate a red curve 
            var a = pane.AddCurve("Submissions / Time", _subOverTime, Color.DarkGoldenrod, ZedGraph.SymbolType.VDash);
            var b = pane.AddCurve("Accepted / Time", _acOverTime, Color.Navy, ZedGraph.SymbolType.VDash);
            a.Symbol.Size = 2;
            b.Symbol.Size = 2;

            pane.AxisChange();
            this.subPerDateTab.Controls.Clear();
            this.subPerDateTab.Controls.Add(zg1);
        }

        private void BuildSubPerVerGraph()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.IsEnableZoom = false;
            zg1.IsShowPointValues = false;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.Azure, Color.FromArgb(230, 220, 250), 90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.FromArgb(225, 220, 245), Color.LightCyan, -90);

            pane.Title.Text = "Submission per Verdict";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Maroon;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.IsVisible = false;

            double[] yval = new double[] { _acCount, _waCount, _tleCount, _reCount, _peCount, _ceCount, _oleCount, _subeCount, _mleCount };
            string[] labels = new string[] { "AC", "WA", "TLE", "RE", "PE", "CE", "OLE", "SUBE", "MLE" };
            for (int i = 0; i < yval.Length; ++i)
            {
                labels[i] += string.Format("\n({0})", yval[i].ToString());
            }

            pane.XAxis.Title.Text = "Verdicts";
            pane.XAxis.Title.FontSpec.Family = "Courier New";
            pane.XAxis.Title.FontSpec.FontColor = Color.Maroon;
            pane.XAxis.Title.FontSpec.IsBold = false;
            pane.XAxis.Scale.TextLabels = labels;
            pane.XAxis.Type = ZedGraph.AxisType.Text;

            pane.YAxis.Title.Text = "Submissions";
            pane.YAxis.Title.FontSpec.Family = "Courier New";
            pane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            pane.YAxis.Title.FontSpec.IsBold = false;

            // Generate a bar chart                   
            ZedGraph.BarItem bar = pane.AddBar("Verdicts", null, yval, Color.DarkTurquoise);
            bar.Label.IsVisible = true;

            pane.AxisChange();

            this.subPerVerTab.Controls.Clear();
            this.subPerVerTab.Controls.Add(zg1);
        }

        /* 
         * Rank cloud is diabled
         *
        private void BuildRankCloud()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.IsShowPointValues = true;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.Azure, Color.FromArgb(230, 220, 250), 90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.FromArgb(225, 220, 245), Color.LightCyan, -90);

            pane.Title.Text = "Rank Cloud";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Maroon;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.IsVisible = false;

            pane.XAxis.Title.Text = "Ranks";
            pane.XAxis.Title.FontSpec.Family = "Courier New";
            pane.XAxis.Title.FontSpec.FontColor = Color.Maroon;
            pane.XAxis.Title.FontSpec.IsBold = false;

            pane.YAxis.Title.Text = "Problems";
            pane.YAxis.Title.FontSpec.Family = "Courier New";
            pane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            pane.YAxis.Title.FontSpec.IsBold = false;
            pane.YAxis.Type = ZedGraph.AxisType.Linear;

            ZedGraph.LineItem curve = pane.AddCurve("Ranks", _RankCount, Color.Black, ZedGraph.SymbolType.Circle);
            curve.Symbol.Fill = new ZedGraph.Fill(Color.LightCyan, Color.DarkTurquoise);
            curve.Symbol.Border.IsVisible = true;
            curve.Symbol.Size = 5;
            curve.Line.IsVisible = false;

            pane.AxisChange();

            this.rankCloudTab.Controls.Clear();
            this.rankCloudTab.Controls.Add(zg1);
        }
        */

        #endregion

    }
}

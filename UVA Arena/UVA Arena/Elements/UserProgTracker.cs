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
    public partial class UserProgTracker : UserControl
    {
        public UserProgTracker()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        UserInfo currentUser = null;

        public void ShowUserInfo(UserInfo uinfo)
        {
            currentUser = uinfo;
            LoadCurrentUser();
        }

        public void LoadCurrentUser()
        {
            ProcessList();

            //normal text
            useridLabel.Text = currentUser.uid;
            usernameLabel.Text = currentUser.uname;
            fullnameLabel.Text = currentUser.name;
            totalsubLabel.Text = _totalSubmission.ToString();
            acceptedLabel.Text = _solvedCount.ToString();
            triednacLabel.Text = _unsolvedCount.ToString();

            //draw graphs
            BuildSubPerLangGraph();
            BuildSubPerDateGraph();
            BuildSubPerVerGraph();
            BuildRankCloud();
        }

        #region Chart Builder

        private int _solvedCount, _unsolvedCount, _totalSubmission;
        private int _subInAnsiC, _subInCPP, _subInCPP11, _subInJava, _subInPascal;
        double _acCount, _waCount, _tleCount, _reCount, _peCount, _ceCount, _oleCount, _subeCount, _mleCount;
        private ZedGraph.PointPairList _subOverTime = new ZedGraph.PointPairList();
        private ZedGraph.PointPairList _acOverTime = new ZedGraph.PointPairList();
        private ZedGraph.PointPairList _RankCount = new ZedGraph.PointPairList();

        private void ProcessList()
        {
            if (currentUser == null)
            {
                currentUser = new UserInfo();
                currentUser.uid = currentUser.uname = currentUser.name = "[-]";
                currentUser.subs = new List<List<long>>();
                currentUser.Process();
            }

            _acOverTime.Clear();
            _subOverTime.Clear();
            _RankCount.Clear();
            currentUser.ACList.Clear();
            List<long> unsolved = new List<long>();

            _solvedCount = _unsolvedCount = _totalSubmission = 0;
            _subInAnsiC = _subInCPP = _subInCPP11 = _subInJava = _subInPascal = 0;
            _acCount = _waCount = _tleCount = _reCount = _peCount = _ceCount = _oleCount = _subeCount = _mleCount = 0;

            foreach (UserSubmission usub in currentUser.submissions)
            {
                //total submission count
                _totalSubmission++;

                //add rank count
                if (usub.IsAccepted())
                {
                    _RankCount.Add(usub.rank, usub.pnum);
                }

                //solved unsolved count
                bool isInAClist = currentUser.ACList.Contains(usub.pnum);
                if (usub.IsAccepted() && !isInAClist)
                {
                    _solvedCount++;
                    currentUser.ACList.Add(usub.pnum);
                }
                if (!isInAClist && !unsolved.Contains(usub.pnum))
                {
                    _unsolvedCount++;
                    unsolved.Add(usub.pnum);
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
            _subOverTime.Add(new ZedGraph.XDate(DateTime.Now), _totalSubmission);
            _acOverTime.Add(new ZedGraph.XDate(DateTime.Now), _solvedCount);
        } 

        private void BuildSubPerLangGraph()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.Azure, Color.FromArgb(245, 220, 250), 90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.Snow, Color.FromArgb(225, 240, 250), -90);

            pane.Title.Text = "Submissions per Languages";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Navy;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.FontSpec.Size = 12;
            pane.Legend.Position = ZedGraph.LegendPos.TopCenter;

            double[] value = { _subInCPP, _subInJava, _subInAnsiC, _subInCPP11, _subInPascal };
            string[] label = { "C++", "Java", "Ansi C", "C++11", "Pascal" };
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
            System.GC.Collect();
        }

        private void BuildSubPerDateGraph()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            zg1.Cursor = Cursors.Arrow;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.MintCream, Color.FromArgb(245, 250, 210), -90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.Snow, Color.FromArgb(245, 250, 230), 90);

            pane.Title.Text = "Submissions per Languages";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Navy;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.FontSpec.Size = 12;
            pane.Legend.Position = ZedGraph.LegendPos.TopCenter;

            pane.XAxis.Title.Text = "Submission Time";
            pane.XAxis.Title.FontSpec.Family = "Courier New";
            pane.XAxis.Title.FontSpec.FontColor = Color.Maroon;
            pane.XAxis.Title.FontSpec.IsBold = false;
            pane.XAxis.Type = ZedGraph.AxisType.Date;

            pane.YAxis.Title.Text = "Submissions";
            pane.YAxis.Title.FontSpec.Family = "Courier New";
            pane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            pane.YAxis.Title.FontSpec.IsBold = false;

            // Generate a red curve 
            pane.AddCurve("Submissions / Time", _subOverTime, Color.DarkGoldenrod, ZedGraph.SymbolType.None);
            pane.AddCurve("Accepted / Time", _acOverTime, Color.Navy, ZedGraph.SymbolType.None);

            pane.AxisChange();

            this.subPerDateTab.Controls.Clear();
            this.subPerDateTab.Controls.Add(zg1);
            System.GC.Collect();
        }

        private void BuildSubPerVerGraph()
        {

            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.Azure, Color.FromArgb(230, 220, 250), 90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.FromArgb(225, 220, 245), Color.LightCyan, -90);

            pane.Title.Text = "Submissions per Verdict";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Navy;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.IsVisible = false;

            pane.XAxis.Title.Text = "Verdicts";
            pane.XAxis.Title.FontSpec.Family = "Courier New";
            pane.XAxis.Title.FontSpec.FontColor = Color.Maroon;
            pane.XAxis.Title.FontSpec.IsBold = false;
            pane.XAxis.Scale.TextLabels = new string[] { "AC", "WA", "TLE", "RE", "PE", "CE", "OLE", "SUBE", "MLE" };
            pane.YAxis.Scale.IsVisible = true;

            pane.YAxis.Title.Text = "Submissions";
            pane.YAxis.Title.FontSpec.Family = "Courier New";
            pane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            pane.YAxis.Title.FontSpec.IsBold = false;            

            // Generate a bar chart       
            double[] yval = new double[] { _acCount, _waCount, _tleCount, _reCount, _peCount, _ceCount, _oleCount, _subeCount, _mleCount };
            pane.AddBar("Verdicts", null, yval, Color.DarkTurquoise);

            pane.AxisChange();

            this.subPerVerTab.Controls.Clear();
            this.subPerVerTab.Controls.Add(zg1);
            System.GC.Collect();
        }

        private void BuildRankCloud()
        {
            ZedGraph.ZedGraphControl zg1 = new ZedGraph.ZedGraphControl();
            zg1.Dock = DockStyle.Fill;
            zg1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            ZedGraph.GraphPane pane = zg1.GraphPane;

            pane.Fill = new ZedGraph.Fill(Color.Azure, Color.FromArgb(230, 220, 250), 90);
            pane.Chart.Fill = new ZedGraph.Fill(Color.FromArgb(225, 220, 245), Color.LightCyan, -90);

            pane.Title.Text = "Rank Cloud";
            pane.Title.FontSpec.Size = 18;
            pane.Title.FontSpec.FontColor = Color.Maroon;
            pane.Title.FontSpec.Family = "Segoe UI Semibold";
            pane.Title.FontSpec.IsBold = false;

            pane.Legend.IsVisible = false;

            pane.XAxis.Title.Text = "Rank";
            pane.XAxis.Title.FontSpec.Family = "Courier New";
            pane.XAxis.Title.FontSpec.FontColor = Color.Maroon;
            pane.XAxis.Title.FontSpec.IsBold = false;

            pane.YAxis.Title.Text = "Submissions";
            pane.YAxis.Title.FontSpec.Family = "Courier New";
            pane.YAxis.Title.FontSpec.FontColor = Color.Navy;
            pane.YAxis.Title.FontSpec.IsBold = false;
            pane.YAxis.Type = ZedGraph.AxisType.LinearAsOrdinal;

            ZedGraph.LineItem curve = pane.AddCurve("Ranks", _RankCount, Color.Black, ZedGraph.SymbolType.Circle);
            curve.Symbol.Fill = new ZedGraph.Fill(Color.LightCyan, Color.DarkTurquoise);
            curve.Symbol.Border.IsVisible = true;
            curve.Symbol.Size = 5;
            curve.Line.IsVisible = false;

            pane.AxisChange();

            this.rankCloudTab.Controls.Clear();
            this.rankCloudTab.Controls.Add(zg1);
            System.GC.Collect();
        }

        #endregion

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
    }
}

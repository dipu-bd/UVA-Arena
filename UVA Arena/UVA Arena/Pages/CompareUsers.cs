using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena.Elements
{
    public partial class CompareUsers : UserControl
    {
        public CompareUsers()
        {
            InitializeComponent();
            SetAspectValues();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadUsersList();

            Stylish.SetGradientBackground(panel1,
                new Stylish.GradientStyle(Color.DarkTurquoise, Color.LightBlue, 90F));

            Stylish.SetGradientBackground(groupBox1,
                new Stylish.GradientStyle(Color.MediumTurquoise, Color.LightBlue));
        }

        public void LoadUsersList()
        {
            try
            {
                string[] unames = new string[LocalDatabase.usernames.Count];
                LocalDatabase.usernames.Keys.CopyTo(unames, 0);

                int fi = firstUser.SelectedIndex;
                firstUser.Items.Clear();
                firstUser.Items.AddRange(unames);

                int si = secondUser.SelectedIndex;
                secondUser.Items.Clear();
                secondUser.Items.AddRange(unames);

                firstUser.SelectedIndex = fi;
                secondUser.SelectedIndex = si;
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "CompareUsers|LoadUserList()");
            }
        }

        private void user_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstUser.SelectedIndex == secondUser.SelectedIndex)
            {
                if (firstUser.SelectedIndex == -1) return;
                MessageBox.Show("You can't compare between same user.");
                ((ComboBox)sender).SelectedIndex = -1;
                return;
            }


            string first = "first user";
            string second = "Second user";
            if (!(firstUser.SelectedIndex == -1 || secondUser.SelectedIndex == -1))
            {
                first = (string)firstUser.SelectedItem;
                second = (string)secondUser.SelectedItem;
            }

            commonSubs.Text = string.Format((string)commonSubs.Tag, first, second);
            secondsSubs.Text = string.Format((string)secondsSubs.Tag, first, second);
            secondsRank.Text = string.Format((string)secondsRank.Tag, first, second);
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            if (firstUser.SelectedIndex == -1 || secondUser.SelectedIndex == -1)
            {
                MessageBox.Show("Select two users to compare.");
            }
            else
            {
                RunComparer((string)firstUser.SelectedItem, (string)secondUser.SelectedItem);
            }

        }

        public void RunComparer(string user1, string user2)
        {
            try
            {
                //get first user                
                string data1 = System.IO.File.ReadAllText(LocalDirectory.GetUserSubPath(user1));
                UserInfo first = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(data1);
                first.Process();

                //get second user                
                string data2 = System.IO.File.ReadAllText(LocalDirectory.GetUserSubPath(user2));
                UserInfo second = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(data2);
                second.Process();

                ShowCompareResult(first, second);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Comparison Failed. May be you did not download submission list of selected user.");
                Logger.Add(ex.Message, "CompareUsers|RunComparer");
            }
        }

        private void ShowCompareResult(UserInfo first, UserInfo second)
        {
            int firstac = 0, secondac = 0;
            foreach (var val in first.TryList.Values)
            {
                if (val.IsAccepted()) firstac++;
            }
            foreach (var val in second.TryList.Values)
            {
                if (val.IsAccepted()) secondac++;
            }

            //set some labels
            acceptedLabel.Text = string.Format((string)acceptedLabel.Tag, firstac, secondac);
            triednacLabel.Text = string.Format((string)triednacLabel.Tag,
               first.TryList.Count - firstac, second.TryList.Count - secondac);
            totalsubLabel.Text = string.Format((string)totalsubLabel.Tag,
                first.submissions.Count, second.submissions.Count);

            //build up list
            List<UserSubmission> usub = new List<UserSubmission>();

            //enumerate all seconds submissions
            var it = second.TryList.GetEnumerator();
            while (it.MoveNext())
            {
                UserSubmission sub = it.Current.Value;

                //---- check if current submission can be taken ----
                //if only accepted need to be shown and second did not solve this,
                if (acceptedRadio.Checked && !sub.IsAccepted()) continue;
                if (commonSubs.Checked) //show problems that are common in both user.
                {
                    //if first doesn't have this, 
                    if (!first.HasTried(sub.pnum)) continue;
                    //if only accepted need to be shown and first did not solve this,
                    if (acceptedRadio.Checked && !first.IsSolved(sub.pnum)) continue;
                }
                else if (secondsSubs.Checked) //second has but first doesn't
                {
                    //if first solved this problem,
                    if (first.IsSolved(sub.pnum)) continue;
                    //if compare between all submission, and first tried it,
                    if (allsubRadio.Checked && first.HasTried(sub.pnum)) continue;
                }
                else if (secondsRank.Checked) //second is better in rank than first
                {
                    //if either second or first did not solve this problem,
                    if (!sub.IsAccepted() || !first.IsSolved(sub.pnum)) continue;
                    if (sub.rank > first.TryList[sub.pnum].rank) continue;
                }
                else continue;

                //--- take second user's submission ----
                usub.Add(sub);
                if (!secondsSubs.Checked && first.HasTried(sub.pnum))
                {
                    //take second user's submission
                    usub.Add(first.TryList[sub.pnum]);
                }
            }
            it.Dispose();

            //set list to listview
            lastSubmissions1.ClearObjects();
            lastSubmissions1.SetObjects(usub);
            lastSubmissions1.BuildGroups(pnumSUB, SortOrder.Ascending);
            lastSubmissions1.ShowGroups = !secondsSubs.Checked;

            probInListLabel.Text = string.Format((string)probInListLabel.Tag, lastSubmissions1.OLVGroups.Count);
        }

        private void SetAspectValues()
        {
            subtimeSUB.AspectGetter = delegate (object row)
            {
                return UnixTimestamp.FormatUnixTime(((UserSubmission)row).sbt);
            };
            lanSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.GetLanguage((Language)(long)dat);
            };
            verSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.GetVerdict((Verdict)(long)dat);
            };
            runSUB.AspectToStringConverter = delegate (object dat)
            {
                return Functions.FormatRuntime((long)dat);
            };
            rankSUB.AspectToStringConverter = delegate (object dat)
            {
                if ((long)dat == -1) return "-";
                return ((long)dat).ToString();
            };
            pnumSUB.GroupKeyGetter = delegate (object row)
            {
                UserSubmission usub = (UserSubmission)row;
                return string.Format("{0} - {1}", usub.pnum, usub.ptitle);
            };
        }

        private void lastSubmissions1_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            string font = "Segoe UI";
            float size = 9.0F;
            FontStyle style = FontStyle.Regular;
            Color fore = Color.Black;


            //highlight other
            if (e.Column == sidSUB)
            {
                font = "Consolas";
                fore = Color.Teal;
                size = 8.5F;
            }
            else if (e.Column == unameSUB)
            {
                fore = Color.Navy;
                style = FontStyle.Italic;
            }
            else if (e.Column == nameSUB)
            {
                font = "Segoe UI Semibold";
            }
            else if (e.Column == pnumSUB)
            {
                font = "Consolas";
                fore = Color.Navy;
                style = FontStyle.Italic;
            }
            else if (e.Column == ptitleSUB)
            {
                font = "Segoe UI Semibold";
                fore = Functions.GetProblemTitleColor(((UserSubmission)e.Model).pnum);
            }
            else if (e.Column == runSUB)
            {
                fore = Color.SlateBlue;
            }
            else if (e.Column == subtimeSUB)
            {
                fore = Color.Maroon;
            }
            else if (e.Column == rankSUB)
            {
                fore = Color.Navy;
                font = "Segoe UI Semibold";
            }
            else if (e.Column == verSUB)
            {
                font = "Segoe UI";
                fore = Functions.GetVerdictColor((Verdict)((UserSubmission)e.Model).ver);
                style = FontStyle.Bold;
            }
            else if (e.Column == lanSUB)
            {
                style = FontStyle.Bold;
                fore = Color.Navy;
            }
            else { return; }

            e.SubItem.ForeColor = fore;
            e.SubItem.Font = new Font(font, size, style);
        }

        private void lastSubmissions1_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            if (e.Column == pnumSUB || e.Column == ptitleSUB)
            {
                Interactivity.ShowProblem(((UserSubmission)e.Model).pnum);
            }
        }
    }
}

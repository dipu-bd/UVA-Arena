using System.Windows.Forms;
using UVA_Arena.Elements;
using UVA_Arena.Structures;
using System.Collections.Generic;

namespace UVA_Arena
{
    internal static class Interactivity
    {
        public static MainForm mainForm;
        public static SettingsForm settingsForm;
        public static LoggerForm loggerForm;
        public static HelpAbout helpaboutForm;
        public static SubmitForm submitForm;
        public static DownloadAllForm downloadAllForm;
        public static PROBLEMS problems;
        public static ProblemViewer problemViewer;
        public static CODES codes;
        public static STATUS status;
        public static USER_STAT userstat;
        public static UTILITIES utilities;

        public static void SubmitCode(long pnum, string code = null, Language lang = Language.CPP)
        {
            if (submitForm == null || submitForm.IsDisposed)
                submitForm = new SubmitForm();
            submitForm.LoadSubmit(pnum, code, lang);
            submitForm.BringToFront();
            submitForm.Show();
        }

        public static void ShowLogger()
        {
            if (loggerForm == null || loggerForm.IsDisposed)
                loggerForm = new LoggerForm();
            loggerForm.BringToFront();
            loggerForm.Show();
        }

        public static void ShowHelpAbout()
        {
            if (helpaboutForm == null || helpaboutForm.IsDisposed)
                helpaboutForm = new HelpAbout();
            helpaboutForm.BringToFront();
            helpaboutForm.Show();
        }

        public static void ShowSettings(int tabindex = 0)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm();
            settingsForm.BringToFront();
            settingsForm.Show();
            settingsForm.tabControl1.SelectedIndex = tabindex;
        }

        public static void ShowDownloadAllForm()
        {
            if (downloadAllForm == null || downloadAllForm.IsDisposed)
                downloadAllForm = new DownloadAllForm();
            downloadAllForm.BringToFront();
            downloadAllForm.Show();
        }

        public static void DefaultUsernameChanged()
        {
            try
            {
                mainForm.BeginInvoke(new MethodInvoker(mainForm.SetFormProperties));
                settingsForm.BeginInvoke(new MethodInvoker(settingsForm.SetCurrentUsername));
                LocalDatabase.LoadDefaultUser();
            }
            catch (System.Exception ex) { Logger.Add(ex.Message, "Interactivity"); }
        }

        public static void ShowUserStat(string user)
        {
            try
            {
                if (!LocalDatabase.ContainsUsers(user)) return;

                userstat.BeginInvoke((MethodInvoker)delegate
                {
                    userstat.ShowUserSub(user);
                });

                mainForm.BeginInvoke((MethodInvoker)delegate
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.profileTab;
                    mainForm.BringToFront();
                });
            }
            catch (System.Exception ex) { Logger.Add(ex.Message, "Interactivity|ShowUserStat()"); }
        }

        public static void ShowJudgeStatus()
        {
            try
            {
                mainForm.BeginInvoke((MethodInvoker)delegate
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.judgeStatusTab;
                    mainForm.BringToFront();
                });
            }
            catch (System.Exception ex) { Logger.Add(ex.Message, "Interactivity|ShowJudgeStatus()"); }
        }

        public static void ShowProblem(long pnum)
        {
            try
            {
                if (!LocalDatabase.HasProblem(pnum)) return;
                ProblemInfo pinfo = LocalDatabase.GetProblem(pnum);

                problems.BeginInvoke((MethodInvoker)delegate
                {
                    problems.ShowAllProblems();
                    problems.problemListView.SelectObject(pinfo, true);
                    problems.problemListView.EnsureVisible(problems.problemListView.SelectedIndex);
                });

                mainForm.BeginInvoke((MethodInvoker)delegate
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.problemTab;
                    mainForm.BringToFront();
                });
            }
            catch (System.Exception ex) { Logger.Add(ex.Message, "Interactivity|ShowProblem()"); }
        }

        public static void ShowCode(long pnum)
        {
            try
            {
                if (!LocalDatabase.HasProblem(pnum)) return;

                codes.BeginInvoke((MethodInvoker)delegate
                {
                    codes.ShowCode(pnum);
                });

                mainForm.BeginInvoke((MethodInvoker)delegate
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.codesTab;
                    mainForm.BringToFront();
                });
            }
            catch (System.Exception ex) { Logger.Add(ex.Message, "Interactivity|ShowCode()"); }
        }

        public static void ProblemDatabaseUpdated()
        {
            if (problems == null || problems.IsDisposed) return;

            try
            {
                problems.BeginInvoke((MethodInvoker)delegate
                {
                    //load volumes or category
                    if (!problems.categoryButton.Checked)
                    {
                        problems.LoadVolumes();
                    }
                    else
                    {
                        problems.LoadCategory();
                    }

                    //load problems or refresh
                    if (problems.problemListView.Items.Count == 0)
                    {
                        problems.ShowAllProblems();
                    }
                    else
                    {
                        problems.RefreshProblemList();
                    }
                });
            }
            catch (System.Exception ex) { Logger.Add(ex.Message, "Interactivity|ProblemDatabaseUpdated()"); }
        }
    }
}

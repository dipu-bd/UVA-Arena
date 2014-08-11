using System;
using System.Collections.Generic;
using System.Text;
using UVA_Arena.Elements;
using UVA_Arena.Structures;
using System.Windows.Forms;
using UVA_Arena.Custom;

namespace UVA_Arena
{
    internal sealed class Interactivity
    {
        public static MainForm mainForm;
        public static SettingsForm settingsForm;
        public static LoggerForm loggerForm;
        public static HelpAbout helpaboutForm;
        public static SubmitForm submitForm;
        public static DownloadAllForm downloadAllForm;
        public static PROBLEMS problems;
        public static CODES codes;
        
        public static ProblemViewer problemViewer
        {
            get { return problems.problemViewer1; }
        }


        public static void ShowProblem(long pnum)
        {
            if (!ProblemDatabase.HasProblem(pnum)) return;
            ProblemInfo pinfo = ProblemDatabase.GetProblem(pnum);

            problems.BeginInvoke((MethodInvoker)delegate
            {
                problems.LoadProblems();
                problems.problemListView.SelectObject(pinfo, true);
            });

            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                mainForm.customTabControl1.SelectedTab = mainForm.problemTab;
                mainForm.BringToFront();
            });
        }

        public static void ShowCode(long pnum)
        {
            if (!ProblemDatabase.HasProblem(pnum)) return;

            codes.BeginInvoke((MethodInvoker)delegate
            {

            });

            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                mainForm.customTabControl1.SelectedTab = mainForm.codesTab;
                mainForm.BringToFront();
            });
        }

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

        public static void ShowSettings()
        {
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm();

            settingsForm.BringToFront();
            settingsForm.Show();
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
            mainForm.BeginInvoke(new MethodInvoker(mainForm.RefreshForm));
            if (settingsForm != null && !settingsForm.IsDisposed)
                settingsForm.BeginInvoke(new MethodInvoker(settingsForm.InitializeSettings));
        }

        public static void ProblemDatabaseUpdated()
        {
            if (problems == null || problems.IsDisposed) return;
            problems.BeginInvoke((MethodInvoker)delegate
            {                
                //load volumes or catagory
                if (!problems.catagoryButton.Checked)
                    problems.LoadVolumes();
                else
                    problems.LoadCatagory();

                if (problems.problemListView.Items.Count == 0)                
                    problems.LoadProblems();                
                else 
                    problems.problemListView.Refresh();                
            });
        }
    }
}

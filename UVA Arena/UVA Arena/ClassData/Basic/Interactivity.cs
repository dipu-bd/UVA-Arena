using System.Windows.Forms;
using UVA_Arena.Elements;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    /// <summary>
    /// Allows interactivity among forms and controls during runtime
    /// </summary>
    internal static class Interactivity
    {
        //forms
        public static MainForm mainForm;
        public static SettingsForm settingsForm;
        public static LoggerForm loggerForm;
        public static HelpAbout helpaboutForm;
        public static SubmitForm submitForm;
        public static DownloadAllForm downloadAllForm;
        public static UsernameForm usernameForm;
        public static CheckUpdateForm checkUpdateForm;
        //controls
        public static PROBLEMS problems;
        public static ProblemViewer problemViewer;
        public static CODES codes;
        public static STATUS status;
        public static USER_STAT userstat;
        public static UserProgTracker progTracker;
        public static CompareUsers compareUser;
        public static CodesBrowser codesBrowser;

        /// <summary>
        /// Close a opened form safely.
        /// </summary>
        /// <param name="form"> Form to close </param>
        public static void CloseForm(Form form)
        {
            if (form == null || form.IsDisposed) return;
            form.Close();
            form.Dispose();
        }

        public static void OpenForm(Form form)
        {
            if (form == null || form.IsDisposed) return;
            if (form.Visible) form.BringToFront();
            else form.Show();
        }

        /// <summary>
        /// Close all opened forms used by this application.
        /// </summary>
        public static void CloseAllOpenedForms()
        {
            try
            {
                CloseForm(mainForm);
                CloseForm(settingsForm);
                CloseForm(loggerForm);
                CloseForm(helpaboutForm);
                CloseForm(submitForm);
                CloseForm(downloadAllForm);
                CloseForm(usernameForm);
                CloseForm(checkUpdateForm);
            }
            catch { }
        }

        /// <summary>
        /// Show Submit problem dialog.
        /// </summary>
        /// <param name="pnum">Problem Number</param>
        /// <param name="code">Problem's Code to submit</param>
        /// <param name="lang">Language the code is written on</param>
        public static void SubmitCode(long pnum, string code = null, Language lang = Language.CPP)
        {
            if (submitForm == null || submitForm.IsDisposed)
                submitForm = new SubmitForm();
            submitForm.Show();
            submitForm.LoadSubmit(pnum, code, lang);
        }

        /// <summary>
        /// Show Logger Form
        /// </summary>
        public static void ShowLogger()
        {
            if (loggerForm == null || loggerForm.IsDisposed)
                loggerForm = new LoggerForm();
            OpenForm(loggerForm);
        }

        /// <summary>
        /// Show Help and About form
        /// </summary>
        public static void ShowHelpAbout()
        {
            if (helpaboutForm == null || helpaboutForm.IsDisposed)
                helpaboutForm = new HelpAbout();
            OpenForm(helpaboutForm);
        }

        /// <summary>
        /// Show Setting Form
        /// </summary>
        /// <param name="tabindex">Selected tab index in settings form</param>
        public static void ShowSettings(int tabindex = 0)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
                settingsForm = new SettingsForm();
            settingsForm.tabControl1.SelectedIndex = tabindex;
            OpenForm(settingsForm);
        }


        /// <summary>
        /// Show problem description downloader
        /// </summary>
        public static void ShowDownloadAllForm()
        {
            if (downloadAllForm == null || downloadAllForm.IsDisposed)
                downloadAllForm = new DownloadAllForm();
            OpenForm(downloadAllForm);
        }


        /// <summary>
        /// Show UserName form to change default user-name
        /// </summary>
        public static void ShowUserNameForm()
        {
            if (usernameForm == null || usernameForm.IsDisposed)
                usernameForm = new UsernameForm();
            OpenForm(usernameForm);
        }


        /// <summary>
        /// Show Check for updates form 
        /// </summary>
        public static void ShowCheckUpdateForm()
        {
            if (checkUpdateForm == null || checkUpdateForm.IsDisposed)
                checkUpdateForm = new CheckUpdateForm();
            OpenForm(checkUpdateForm);
        }

        /// <summary>
        /// It get's called when an update is downloaded
        /// </summary>
        /// <param name="update">Update message that got downloaded</param>
        public static void UpdateFound(UpdateCheck.UpdateMessage update)
        {
            int prev = parseUpdate(update.version);
            int current = parseUpdate(Application.ProductVersion);
            if (prev > current)
            {
                mainForm.BeginInvoke((MethodInvoker)delegate
                {
                    ShowCheckUpdateForm();
                    checkUpdateForm.updateLink.Text = update.link;
                    checkUpdateForm.newVersion.Text = update.version;
                    checkUpdateForm.curVersion.Text = Application.ProductVersion;
                    checkUpdateForm.updateMessage.Text = update.message;
                });
            }
            else
            {
                Logger.Add("Update Checked : This is the latest version.", "Interactivity|UpdateFound()");
            }
        }


        private static int parseUpdate(string text)
        {
            int version = 0;
            try
            {
                int cnt = 0;
                foreach (string s in text.Split(new char[] { '.' }))
                {
                    version = version * 100 + int.Parse(s);
                    cnt++;
                }
                while (cnt < 4)
                {
                    version *= 100;
                    cnt++;
                }
            }
            catch { }
            return version;
        }


        /// <summary>
        /// This method is called when default user-name is changed
        /// </summary>
        public static void DefaultUsernameChanged()
        {
            try
            {
                LocalDatabase.LoadDefaultUser();
                mainForm.BeginInvoke(new MethodInvoker(mainForm.SetFormProperties));
                settingsForm.BeginInvoke(new MethodInvoker(settingsForm.SetCurrentUsername));
            }
            catch (System.Exception ex)
            {
                Logger.Add(ex.Message, "Interactivity|DefaultUsernameChanged()");
            }
        }

        public static void UserNameListChanged()
        {
            try
            {
                userstat.BeginInvoke((MethodInvoker)userstat.LoadUsernames);
                compareUser.BeginInvoke((MethodInvoker)compareUser.LoadUsersList);
                problemViewer.BeginInvoke((MethodInvoker)problemViewer.LoadUsernameList);
            }
            catch { }
        }

        /// <summary>
        /// Set a status to default status bar
        /// </summary>
        /// <param name="status">Status to set</param>
        public static void SetStatus(string status = "")
        {
            if (mainForm == null || mainForm.IsDisposed) return;
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    mainForm.Status1.Text = status;
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|SetStatus()");
                }
            });
        }

        /// <summary>
        /// Set the progress to the main for
        /// </summary>
        /// <param name="progress">Value of current progress </param>
        /// <param name="maximum">Maximum value of the progress</param>
        public static void SetProgress(float progress = 0, float maximum = 100)
        {
            if (mainForm == null || mainForm.IsDisposed) return;
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    float val = progress;
                    if (maximum != 0) val = 100 * progress / maximum;
                    if (!(val >= 0 && val <= 100)) val = 0;
                    mainForm.Progress1.Value = (int)val;
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|SetProgress()");
                }
            });
        }

        /// <summary>
        /// Select User Status tab page and show profile of the user
        /// </summary>
        /// <param name="user">User-name to show statistics </param>
        public static void ShowUserStat(string user)
        {
            if (!LocalDatabase.ContainsUser(user)) return;

            userstat.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    userstat.ShowUserSub(user);
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowUserStat()");
                }
            });

            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.profileTab;
                    mainForm.BringToFront();
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowUserStat()");
                }
            });
        }

        /// <summary>
        /// Select Judge Status tab page
        /// </summary>
        public static void ShowJudgeStatus()
        {
            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.statusTab;
                    mainForm.BringToFront();
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowJudgeStatus()");
                }
            });
        }

        /// <summary>
        /// Select Problems tab page and open a specific problem description
        /// </summary>
        /// <param name="pnum">Problem Number</param>
        public static void ShowProblem(long pnum)
        {
            if (!LocalDatabase.HasProblem(pnum)) return;

            problems.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    ProblemInfo pinfo = LocalDatabase.GetProblem(pnum);
                    problems.ShowAllProblems();
                    problems.problemListView.SelectedObject = pinfo;
                    problems.problemListView.EnsureVisible(problems.problemListView.SelectedIndex);
                    problemViewer.LoadProblem(pinfo);
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowProblem()");
                }
            });

            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.problemTab;
                    mainForm.BringToFront();
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowProblem()");
                }
            });
        }

        /// <summary>
        /// Select Codes tab and open a specific problem
        /// </summary>
        /// <param name="pnum">Problem Number</param>
        public static void ShowCode(long pnum)
        {
            if (!LocalDatabase.HasProblem(pnum)) return;

            codes.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    codesBrowser.ShowCode(pnum);
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowCode()");
                }
            });

            mainForm.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    mainForm.customTabControl1.SelectedTab = mainForm.codesTab;
                    mainForm.BringToFront();
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ShowCode()");
                }
            });
        }

        /// <summary>
        /// This method is called after problem database is loaded.
        /// </summary>
        public static void ProblemDatabaseUpdated()
        {
            if (problems == null || problems.IsDisposed) return;

            problems.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    //load problems or refresh
                    if (problems.problemListView.Items.Count == 0)
                    {
                        problems.ShowAllProblems();
                    }
                    else
                    {
                        problems.RefreshProblemList();
                    }
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ProblemDatabaseUpdated()");
                }
            });
        }

        /// <summary>
        /// This method is called after category data is updated.
        /// </summary>
        public static void CategoryDataUpdated()
        {
            if (problems == null || problems.IsDisposed) return;

            problems.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    //load volumes or category
                    problems.LoadCategory();
                }
                catch (System.Exception ex)
                {
                    Logger.Add(ex.Message, "Interactivity|ProblemDatabaseUpdated()");
                }
            });
        }
    }
}

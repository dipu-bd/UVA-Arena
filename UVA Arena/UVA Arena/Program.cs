using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UVA_Arena
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //enable application styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Properties.Settings.Default.Reload();

            //add header to log file
            string dat = Environment.NewLine;
            for (int i = 0; i < 80; ++i) dat += '*';
            dat += Environment.NewLine;
            System.IO.File.AppendAllText(LocalDirectory.GetLogFile(), dat);

            //load usernames
            LocalDatabase.usernames = RegistryAccess.GetAllUsers();
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername))
            {
                UsernameForm uf = new UsernameForm();
                Application.Run(uf);
            }

            //task queue
            TaskQueue.StartTimer();
            
            //under a try catch loop, so that application can restart if anything goes wrong
            while (true)
            {
                try
                {
                    //lauch application
                    Interactivity.mainForm = new MainForm();                    
                    Application.Run(Interactivity.mainForm);
                    break;
                }
                catch (Exception ex)
                {
                    Logger.Add(ex.Message, "Program");
                    string msg = "Looks like something went wrong with this application" + Environment.NewLine;
                    msg += "Error message : " + ex.Message + Environment.NewLine + Environment.NewLine;
                    msg += "Relaunch this application now?";
                    if (MessageBox.Show(msg, "UVA Arena", MessageBoxButtons.YesNo) == DialogResult.No) break;
                }
            }
            
            //end of application works
            Interactivity.CloseAllOpenedForms();
            Properties.Settings.Default.Save();
        }
    }
}

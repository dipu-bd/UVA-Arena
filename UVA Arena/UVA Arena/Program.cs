using System;
using System.Net;
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

            //add header to log file
            string dat = Environment.NewLine;
            for (int i = 0; i < 80; ++i) dat += '*';
            dat += Environment.NewLine;
            System.IO.File.AppendAllText(LocalDirectory.GetLogFile(), dat);

            //enable TLS v1.2
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)0x00000C00;
            }
            catch (Exception ex)
            {
                Logger.Add("Could not enable TLS 1.2 => ", ex.Message + " => " + ex.StackTrace);
            }

            //load user-names
            LocalDatabase.usernames = RegistryAccess.GetAllUsers();
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername) ||
                Properties.Settings.Default.AskForUsernameEverytime)
            {
                UsernameForm uf = new UsernameForm();
                Application.Run(uf);
            }

            //task queue
            TaskQueue.StartTimer();

            try
            {
                //launch application
                Interactivity.mainForm = new MainForm();
                Application.Run(Interactivity.mainForm);
            }
            catch (Exception ex)
            {
                Logger.Add("Error in main form => ", ex.Message + " => " + ex.StackTrace);
                Application.Exit();
            }

            //end of application works
            Interactivity.CloseAllOpenedForms();
            Properties.Settings.Default.Save();

            UVA_Arena.Elements.CodeCompiler.ForceStopTask();
        }
    }
}

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

            //lauch application
            Interactivity.mainForm = new MainForm();
            Application.Run(Interactivity.mainForm);

            //end of application works
            Interactivity.CloseAllOpenedForms();
            Properties.Settings.Default.Save();
        }
    }
}

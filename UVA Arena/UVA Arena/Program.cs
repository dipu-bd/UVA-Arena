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
            string dat = "";
            for(int i = 0; i < 80; ++i) dat += '*';
            dat += Environment.NewLine;
            System.IO.File.AppendAllText(LocalDirectory.GetLogFile(), Environment.NewLine + dat);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DefaultDatabase.usernames = RegistryAccess.GetAllUsers();

            //get default username
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername))
            {
                UsernameForm uf = new UsernameForm();
                Application.Run(uf);
            }

            while (true)
            {
                try
                {
                    //run main program
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

            System.IO.File.AppendAllText(LocalDirectory.GetLogFile(), dat);
        } 
    }
}

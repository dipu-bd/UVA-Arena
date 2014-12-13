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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DefaultDatabase.usernames = RegistryAccess.GetAllUsers();

            //get default username
            if (string.IsNullOrEmpty(RegistryAccess.DefaultUsername))
            {
                UsernameForm uf = new UsernameForm();
                Application.Run(uf);
            }

            //run main program
            Interactivity.mainForm = new MainForm(); 
            Application.Run(Interactivity.mainForm);
        } 
    }
}

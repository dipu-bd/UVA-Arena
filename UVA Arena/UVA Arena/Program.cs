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

            Interactivity.mainForm = new MainForm(); 
            Application.Run(Interactivity.mainForm);
        } 
    }
}

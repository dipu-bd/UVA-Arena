using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UVA_Arena
{
    /// <summary>
    /// Main Form in display
    /// </summary>
    public partial class MainForm : Form, IMessageFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UVA_Arena.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitOthers();                        
        }
        
		/// <summary>
		/// Raises the form closing event.
		/// </summary>
		/// <param name="e">Event</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //save data
            StringCollection sc = new StringCollection();
            foreach (TabPage tp in customTabControl1.TabPages)
            {
                sc.Add(tp.Name);
            }
            Properties.Settings.Default.TabPageOrder = sc;
        }

        #region mouse wheel without focus
 
		/// <summary>
		/// Filters out a message before it is dispatched to enable lower level mouse.
		/// </summary>
		/// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next
		/// filter or control.</returns>
		/// <param name="m">The message to be dispatched. You cannot modify this message.</param>
		/// <filterpriority>1</filterpriority>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_MOUSEWHEEL)
            {
                //find the control at screen position m.LParam
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                IntPtr hWnd = NativeMethods.WindowFromPoint(pos);
                if (hWnd != IntPtr.Zero && hWnd != m.HWnd &&
                    System.Windows.Forms.Control.FromHandle(hWnd) != null)
                {
                    NativeMethods.SendMessage(hWnd, (uint)m.Msg, m.WParam, m.LParam);
                    return true;
                }
            }
            return false;
        }

        #endregion mouse wheel without focus

		/// <summary>
		/// Inits the others.
		/// </summary>
        void InitOthers()
        {
            //display welcome screen
            productName.Text = Application.ProductName;
            string[] versions = Application.ProductVersion.Split('.');
            versionLabel.Text = "Version " + versions[0] + "." + versions[1];

            this.SuspendLayout();

            //to enable lower level mouse
            Application.AddMessageFilter(this);

            //set styles
            customTabControl1.BackColor = Color.PaleTurquoise;
            Stylish.SetGradientBackground(menuStrip1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightSteelBlue, 90F));
            Stylish.SetGradientBackground(quickButtonPanel,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightSteelBlue, 90F));

            //load tab images
            tabImageList.Images.Add("problems", Properties.Resources.problems);
            tabImageList.Images.Add("codes", Properties.Resources.code);
            tabImageList.Images.Add("status", Properties.Resources.status);
            tabImageList.Images.Add("profiles", Properties.Resources.profile);
            tabImageList.Images.Add("utilities", Properties.Resources.utility);
            problemsTab.ImageKey = "problems";
            codesTab.ImageKey = "codes";
            statusTab.ImageKey = "status";
            profilesTab.ImageKey = "profiles";

            //change orders of tab pages
			StringCollection sc = Properties.Settings.Default.TabPageOrder;
            if (sc != null)
            {
                customTabControl1.TabPages.Clear();
                for (int i = 0; i < sc.Count; ++i)
                {
                    if (sc[i] == problemsTab.Name)
                        customTabControl1.TabPages.Add(problemsTab);
                    else if (sc[i] == codesTab.Name)
                        customTabControl1.TabPages.Add(codesTab);
                    else if (sc[i] == statusTab.Name)
                        customTabControl1.TabPages.Add(statusTab);
                    else if (sc[i] == profilesTab.Name)
                        customTabControl1.TabPages.Add(profilesTab);
                }
            }

            //add controls to tabs

            this.ResumeLayout(false);
            loadingPanel.Visible = false;
        }
    }
}

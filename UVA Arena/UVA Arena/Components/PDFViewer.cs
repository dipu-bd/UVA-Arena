using System;
using System.IO;
using System.Windows.Forms;

namespace UVA_Arena.Components
{
    public class PDFViewer : WebBrowser
    {
        private string currentFile = null;

        public PDFViewer()
        {
            this.ScriptErrorsSuppressed = true;
            this.Refresh();
        }

        public override void Refresh()
        {
            string viewer = Path.GetFullPath(@"pdf.js\web\viewer.html");
            if (!File.Exists(viewer))
            {
                string projectDir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
                viewer = Path.Combine(projectDir, @"pdf.js\web\viewer.html");
            }
            if (!File.Exists(viewer)) return;
            this.Navigate(viewer);
        }

        public void Load(string file)
        {
            if (file == null || file == currentFile) return;

            // base64 data of pdf
            byte[] bytes = File.ReadAllBytes(file);
            string data = Convert.ToBase64String(bytes);

            // display viewer
            if (this.Document == null) return;
            this.Document.InvokeScript("openPdfFromBase64", new object[] { data });

            currentFile = file;
        }
    }
}

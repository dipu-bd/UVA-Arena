using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
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
            if (File.Exists(viewer))
            {
                this.Navigate(viewer);
            }
        }

        public void Load(string file)
        {
            if (file == null || file == currentFile)
            {
                return;
            }

            currentFile = file;
            
            // base64 data of pdf
            Byte[] bytes = File.ReadAllBytes(file);
            string data = Convert.ToBase64String(bytes);

            // display viewer 
            this.Document.InvokeScript("openPdfFromBase64", new object[] { data });            
        }        
    }
}


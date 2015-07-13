using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class PDFViewerControl : UserControl
    {
        public PDFViewerControl()
        {
            InitializeComponent();

            RenderDPI = 120F;
        }

        public string FileName { get; set; }

        [DefaultValue(typeof(float), "120.0")]
        public float RenderDPI { get; set; }

        private PDFLibNet.PDFWrapper pdfWrapper;
        private Queue<String> tempQueue = new Queue<string>();

        public void CloseFile()
        {
            FileName = null;
            ClearPageHolder();
        }

        public void ReloadFile()
        {
            LoadFile(FileName);
        }

        public void LoadFile(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                return;
            }

            //load pdf wrapper
            if (pdfWrapper == null)
            {
                pdfWrapper = new PDFLibNet.PDFWrapper();
                pdfWrapper.UseMuPDF = pdfWrapper.SupportsMuPDF;
                pdfWrapper.PDFLoadCompeted += pdfWrapper_PDFLoadCompeted;
                pdfWrapper.ExportJpgFinished += pdfWrapper_ExportJpgFinished;
                pdfWrapper.RenderDPI = 120;
            }

            //wait to load file
            FileName = file;
            System.Threading.ThreadPool.QueueUserWorkItem(WaitToLoad, file);
        }


        void WaitToLoad(object file)
        {
            while (pdfWrapper.IsBusy || pdfWrapper.IsJpgBusy)
            {
                this.BeginInvoke((MethodInvoker)pdfWrapper.CancelJpgExport);
                System.Threading.Thread.Sleep(100);
            }

            Interactivity.SetStatus("Loading pdf file...");
            this.BeginInvoke((MethodInvoker)delegate
            {
                pdfWrapper.LoadPDF((string)file);
            });
        }


        void pdfWrapper_PDFLoadCompeted()
        {
            Interactivity.SetStatus("Loading pdf pages...");
            Interactivity.SetProgress(1, pdfWrapper.PageCount + 1);

            //dispose previous controls
            this.BeginInvoke((MethodInvoker)ClearPageHolder);

            //export and show pages
            ExportPages();
        }

        public void ClearPageHolder()
        {
            foreach (var c in pdfPageHolder.Controls)
            {
                ((PictureBox)c).Image.Dispose();
                ((PictureBox)c).Dispose();
            }
            pdfPageHolder.Controls.Clear();
        }

        void ExportPages(int index = 1)
        {
            if (index > pdfWrapper.PageCount) return;

            //get temporary file
            string file = Path.GetTempFileName();
            File.Delete(file); //delete original temp file
            file += "." + index.ToString(); //new temp file to write

            //enqueue task
            tempQueue.Enqueue(file);
            pdfWrapper.ExportJpg(file, index, index, RenderDPI, 100, 1000);
        }

        void pdfWrapper_ExportJpgFinished()
        {
            try
            {
                string file = tempQueue.Dequeue();

                Image img = Bitmap.FromFile(file);   //take image on hold               
                Bitmap copiedImage = new Bitmap(img.Width, img.Height); //to copy image here
                Graphics.FromImage((Image)copiedImage).DrawImageUnscaled(img, 0, 0); //draw image here
                img.Dispose(); //dispose and release resource on use

                //get current page number
                int index = int.Parse(Path.GetExtension(file).Substring(1));

                //begin operation
                this.BeginInvoke((MethodInvoker)delegate
                {
                    //show picture 
                    PictureBox pb = new PictureBox();
                    pb.Margin = new Padding(0, 0, 0, 0);
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    pb.Image = copiedImage;
                    pdfPageHolder.Controls.Add(pb);

                    Interactivity.SetStatus(index + " PDF page loaded.");
                    Interactivity.SetProgress(index + 1, pdfWrapper.PageCount + 1);

                    //delete used file
                    File.Delete((string)file);

                    //load next page
                    ExportPages(index + 1);
                });
            }
            catch (Exception ex)
            {
                Interactivity.SetStatus("PDF loading failed. " + ex.Message);
            }
        }
    }
}

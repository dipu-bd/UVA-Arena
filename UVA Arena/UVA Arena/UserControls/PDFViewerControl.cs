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

        private bool cancellationPending = false;
        private PDFLibNet.PDFWrapper pdfWrapper;
        private Queue<String> tempQueue = new Queue<string>();

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

        public void CloseFile()
        {
            foreach (var c in pdfPageHolder.Controls)
            {
                ((PictureBox)c).Image.Dispose();
                ((PictureBox)c).Dispose();
            }
            pdfPageHolder.Controls.Clear();
        }


        void WaitToLoad(object file)
        {
            while (pdfWrapper.IsBusy || pdfWrapper.IsJpgBusy)
            {
                cancellationPending = true;
                this.BeginInvoke((MethodInvoker)pdfWrapper.CancelJpgExport);
                System.Threading.Thread.Sleep(100);
            }

            cancellationPending = false;
            if (FileName != (string)file) return;

            Interactivity.SetStatus("Loading pdf file...");
            this.BeginInvoke((MethodInvoker)delegate
            {
                try
                {
                    pdfWrapper.LoadPDF((string)file);
                }
                catch
                {
                    CloseFile();
                }
            });
        }


        void pdfWrapper_PDFLoadCompeted()
        {
            Interactivity.SetStatus("Loading pdf pages...");
            Interactivity.SetProgress(1, pdfWrapper.PageCount + 1);

            //dispose previous controls
            this.BeginInvoke((MethodInvoker)CloseFile);

            //check if cancellation is pending
            if (cancellationPending) return;

            //export and show pages 
            ExportPages();
        }

        void ExportPages(int index = 1)
        {
            if (index > pdfWrapper.PageCount) return;
            try
            {
                //get temporary file
                string file = Path.GetTempFileName();
                File.Delete(file); //delete original temp file
                file += "." + index.ToString(); //new temp file to write

                //enqueue task
                tempQueue.Enqueue(file);
                pdfWrapper.ExportJpg(file, index, index, RenderDPI, 100, 1000);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "PdfViewer | Export Pages");
            }
        }

        void pdfWrapper_ExportJpgFinished()
        {
            if (cancellationPending) return;

            try
            {
                string file = tempQueue.Dequeue();

                //get exported image from file
                Image img = Bitmap.FromFile(file);   //take image on hold               
                Bitmap copiedImage = new Bitmap(img.Width, img.Height); //to copy image here
                Graphics.FromImage((Image)copiedImage).DrawImageUnscaled(img, 0, 0); //draw image here
                img.Dispose(); //dispose and release resource on use

                //get current page number
                int index = int.Parse(Path.GetExtension(file).Substring(1));

                //begin operation
                this.BeginInvoke((MethodInvoker)delegate
                {
                    try
                    {
                        //show picture 
                        PictureBox pb = new PictureBox();
                        pb.Margin = new Padding(1, 1, 1, 1);
                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                        pb.Image = copiedImage;
                        pdfPageHolder.Controls.Add(pb);

                        Interactivity.SetStatus(index + " PDF page loaded. (" + FileName + ")");
                        Interactivity.SetProgress(index + 1, pdfWrapper.PageCount + 1);

                        //delete used file
                        File.Delete((string)file);

                        //load next page
                        ExportPages(index + 1);
                    }
                    catch (Exception ex)
                    {
                        Logger.Add("PDF loading failed. " + ex.Message, "PdfViewerControl");
                    }
                });
            }
            catch (Exception ex)
            {
                Interactivity.SetStatus("PDF loading failed. " + ex.Message);
                Logger.Add("PDF loading failed. " + ex.Message, "PdfViewerControl");
            }
        }
    }
}

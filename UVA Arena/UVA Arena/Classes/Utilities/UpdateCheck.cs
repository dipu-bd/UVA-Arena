using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;

namespace UVA_Arena
{
    public static class UpdateCheck
    {
        public class UpdateMessage
        {
            public UpdateMessage()
            {
            }

            /// <summary>
            /// Get the updated version
            /// </summary>
            public string version { get; set; }

            /// <summary>
            /// Get the link of update file
            /// </summary>
            public string link { get; set; }

            /// <summary>
            /// Get the change log in this update
            /// </summary>
            public string message { get; set; }
        }

        public delegate void UpdateFoundHandler(UpdateMessage update);

        /// <summary>
        /// Check if any update is available for the current version
        /// </summary>
        public static void CheckForUpdate()
        {
            if (IsChecking) return;
            ThreadPool.QueueUserWorkItem(downloadUpdateFile);
        }

        public static bool IsChecking = false;

        private static void downloadUpdateFile(object state)
        {
            try
            {
                IsChecking = true;
                string url = "https://raw.githubusercontent.com/dipu-bd/UVA-Arena/master/VERSION";

                WebClient wc = new WebClient();
                byte[] raw = wc.DownloadData(url);
                string data = System.Text.Encoding.UTF8.GetString(raw);
                UpdateMessage um = JsonConvert.DeserializeObject<UpdateMessage>(data);

                //this applications functions
                if (um != null) Interactivity.UpdateFound(um);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "UpdateCheck|downloadUpdateFile()");
            }
            finally
            {
                IsChecking = false;
            }
        }
    }
}
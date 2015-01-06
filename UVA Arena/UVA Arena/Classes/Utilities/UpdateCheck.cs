using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using Newtonsoft.Json;

namespace UVA_Arena 
{
    public class UpdateCheck
    {
        public class UpdateMessage
        {
            public UpdateMessage() { }

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
        /// <param name="caller"> Function to call when any update found </param>        
        public static void CheckForUpdate(UpdateFoundHandler caller = null)
        {
            if (IsChecking) return;
            ThreadPool.QueueUserWorkItem(downloadUpdateFile, caller);
        }

        public static bool IsChecking = false;

        private static void downloadUpdateFile(object state)
        {
            try
            {
                IsChecking = true;

                var caller = (UpdateFoundHandler)state;
                string url = "https://github.com/dipu-bd/UVA-Arena/blob/master/VERSION";

                WebClient wc = new WebClient();
                byte[] raw = wc.DownloadData(url);
                string data = System.Text.Encoding.UTF8.GetString(raw);
                UpdateMessage um = JsonConvert.DeserializeObject<UpdateMessage>(data);
                
                if (um != null && caller != null) caller(um);

                //this applications functions
                Interactivity.UpdateFound(um);
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

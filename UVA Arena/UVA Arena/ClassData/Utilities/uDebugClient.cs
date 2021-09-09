using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace UVA_Arena.Utilities
{
    public abstract class uDebugClient
    {
        public static FormData ExtractInputUsers(long pnum)
        {
            Interactivity.SetStatus("Loading uDebug user's input on problem " + pnum + "...");

            FormData form = new FormData();
            form.pnum = pnum;

            // Get page
            var request = (HttpWebRequest)WebRequest.Create("https://www.udebug.com/UVa/" + pnum);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Parse page
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseString);

            // Problem NID
            var pidPath = "//input[@name='problem_nid']";
            var probNodes = htmlDoc.DocumentNode.SelectNodes(pidPath);
            if (probNodes != null)
            {
                form.ProblemNID = probNodes[0].GetAttributeValue("value", "");
            }

            // Form Build ID
            var buildIdPath = "//input[@name='form_build_id']";
            var buildIdNodes = htmlDoc.DocumentNode.SelectNodes(buildIdPath);
            if (buildIdNodes != null)
            {
                form.BuildID = buildIdNodes[0].GetAttributeValue("value", "");
            }

            // Input Nodes
            var inpPath = "//a[@href='https://www.udebug.com/input-description']";
            var inpNodes = htmlDoc.DocumentNode.SelectNodes(inpPath);
            if (inpNodes != null)
            {
                foreach (var node in inpNodes)
                {
                    try
                    {
                        DateTime result = DateTime.Now;
                        if (DateTime.TryParse(node.InnerText, out result))
                        {
                            continue;
                        }

                        var user = new UserInput();
                        user.UserName = node.InnerText;
                        user.InputNID = long.Parse(node.GetAttributeValue("data-id", ""));

                        form.inputs.Add(user);
                    }
                    catch { }
                }
            }

            Interactivity.SetStatus(form.inputs.Count + " users found on problem " + pnum + ".");
            return form;
        }

        public static string GetInputdata(UserInput user)
        {
            Interactivity.SetStatus("Loading user input data...");

            var url = "https://www.udebug.com/udebug-custom-get-selected-input-ajax";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var postData = "input_nid=" + user.InputNID;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<InputValue>(responseString);

            Interactivity.SetStatus("User's input data downloaded.");
            return result.input_value;

        }

        public static string GetOutputData(string input, FormData form)
        {
            Interactivity.SetStatus("Loading user's output data...");

            var url = "https://www.udebug.com/UVa/" + form.pnum;
            var request = (HttpWebRequest)WebRequest.Create(url);


            // build request data
            var postData = "problem_nid=" + form.ProblemNID +
                            "&input_data=" + input +
                            "&node_nid=&op=Get+Accepted+Output&output_data=&user_output=" +
                            "&form_build_id=" + form.BuildID +
                            "&form_id=udebug_custom_problem_view_input_output_form";
            var data = Encoding.ASCII.GetBytes(postData);

            // send request and read response
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            // Parse page
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseString);

            // Read Accepted output data
            var node = htmlDoc.GetElementbyId("edit-output-data");

            string output = "";
            if (node != null)
            {
                output = node.InnerText;
            }

            Interactivity.SetStatus("Download output data from uDebug.");
            return output;
        }


        public class InputValue
        {
            public string input_delete_link { get; set; }
            public string input_value { get; set; }
        }

        public class UserInput
        {
            public string UserName { get; set; }
            public long InputNID { get; set; }

            public override string ToString()
            {
                return UserName;
            }
        }

        public class FormData
        {
            public long pnum { get; set; }
            public string BuildID { get; set; }
            public string ProblemNID { get; set; }

            public List<UserInput> inputs = new List<UserInput>();

            public FormData()
            {
                BuildID = "form-93X6w3BN-xieVFPa9-mui-viUNS6Mf2vHY7bZqn6fy0";
            }
        }

    }
}

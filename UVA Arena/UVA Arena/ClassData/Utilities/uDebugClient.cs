using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Net;
using HtmlAgilityPack;

namespace UVA_Arena.Utilities
{
    public abstract class uDebugClient
    {
        public static List<UserInput> ExtractInputUsers(long pnum)
        {
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

            string pid = "";
            if (probNodes != null)
            {
                pid = probNodes[0].GetAttributeValue("value", "");
            }

            // Form Build ID
            var buildIdPath = "//input[@name='form_build_id']";
            var buildIdNodes = htmlDoc.DocumentNode.SelectNodes(buildIdPath);

            string build = "";
            if (buildIdNodes != null)
            {
                pid = buildIdNodes[0].GetAttributeValue("value", "");
            }

            // Input Nodes
            var inpPath = "//a[@href='https://www.udebug.com/input-description']";
            var inpNodes = htmlDoc.DocumentNode.SelectNodes(inpPath);

            List<UserInput> users = new List<UserInput>();
            if (inpNodes == null) return users;

            foreach (var node in inpNodes)
            {
                try
                {
                    var user = new UserInput();
                    user.UserName = node.InnerText;

                    DateTime result = DateTime.Now;
                    if (DateTime.TryParse(user.UserName, out result))
                    {
                        continue;
                    }

                    user.pnum = pnum;
                    user.BuildID = build;
                    user.ProblemNID = pid;
                    user.InputNID = long.Parse(node.GetAttributeValue("data-id", ""));
                    Console.WriteLine(user.UserName + "  " + user.InputNID);
                    users.Add(user);
                }
                catch { }
            }
            return users;
        }

        public static string GetInputdata(ref UserInput user)
        {
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
            return result.input_value;
        }

        public static string GetOutputData(UserInput user, string input)
        { 
            var url = "https://www.udebug.com/UVa/" + user.pnum;
            var request = (HttpWebRequest)WebRequest.Create(url);

            // build request data
            var postData = "problem_nid=" + user.InputNID +
                            "&input_data=" + input +
                            "&node_nid=&op=Get+Accepted+Output&output_data=&user_output=" +
                            "&form_build_id=" + user.BuildID +
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
            
            return output;
        }


        public class InputValue
        {
            public string input_delete_link { get; set; }
            public string input_value { get; set; }
        }

        public class UserInput
        {
            public long pnum { get; set; }
            public string BuildID { get; set; }
            public string ProblemNID { get; set; }
            public string UserName { get; set; }
            public long InputNID { get; set; }

            public override string ToString()
            {
                return "prob=" + ProblemNID + ", nid=" + InputNID + ", user=" + UserName;
            }
        }

    }
}

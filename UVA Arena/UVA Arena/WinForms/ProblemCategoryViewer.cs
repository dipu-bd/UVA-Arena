using System;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class ProblemCategoryViewer : Form
    {
        public ProblemCategoryViewer(ProblemInfo pinfo)
        {
            InitializeComponent();
            loadData(pinfo);
        }

        const string title = "{0} - {1}";
        const string head = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033"
                          + @"{\fonttbl{\f0\fnil\fcharset0 Tahoma;}{\f1\fnil\fcharset0 Consolas;}}"
                          + @"{\colortbl ;\red52\green70\blue37;\red130\green130\blue150;\red110\green100\blue130;}\lang9\n";
        const string tail = "\n}\0";
        const string catpath = @"\pard\cf3\f1\ul\fs22 Number: #{1:00}:\ul0\par\n\pard\cf1 {0}\cf0\par\n";
        const string probnote = @"\pard\sa200\li720\qj\cf2\f0\fs19 {0}\cf0\par\n";

        void loadData(ProblemInfo pinfo)
        {
            titleLabel.Text = string.Format(title, pinfo.pnum, pinfo.ptitle);

            int num = 1;
            string data = head;
            foreach (var node in pinfo.categories)
            {
                string path = getPath(node).Replace(Environment.NewLine, @"\par\n\pard ");
                data += string.Format(catpath, path, num++);
                data += string.Format(probnote, node.GetCategoryNote(pinfo.pnum));
            }
            data += tail;
            richTextBox1.Rtf = data;
        }

        /// <summary>
        /// Gets the path to current category node
        /// </summary> 
        string getPath(CategoryNode nod)
        {
            if (nod.Parent == null)
            {
                return nod.name;
            }
            else
            {
                string path = getPath(nod.Parent) + Environment.NewLine;
                for (int i = nod.Level; i > 0; --i)
                {
                    path += " ";
                }
                path += "=> " + nod.name;
                return path;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}

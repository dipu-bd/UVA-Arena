using System;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class InputForm : Form
    {
        public InputForm(
            string description = "Input : ",
            string deftext = "Type your text here...",
            string title = "Input Form",
            int width = 450, int height = 130)
        {
            InitializeComponent();

            this.Text = deftext;
            this.Title = title;
            this.Width = width;
            this.Height = height;
            info_text.Text = description;
        }

        public static string GetInput(
            string description = "Input : ",
            string deftext = "Type your text here...",
            string title = "Input Form",
            int width = 450, int height = 130)
        {
            using (InputForm inf = new InputForm(description, deftext, title, width, height))
            {
                if (inf.ShowDialog() == DialogResult.OK) return inf.Text;
                return deftext;
            }
        }

        public override string Text
        {
            get
            {
                return info_text.Text;
            }
            set
            {
                info_text.Text = value;
            }
        }

        public string Title
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        private void ok_button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}

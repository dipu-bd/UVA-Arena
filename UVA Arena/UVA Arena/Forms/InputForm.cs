using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class InputForm : Form
    {
        public InputForm(
            string description = "Input : ",
            string cue_text = "Type your text here...",
            string form_name = "Input Form",
            int width = 450, int height = 130)
        {
            InitializeComponent();

            info_text.Text = description;
            inputText1.CueText = cue_text;
            this.Text = form_name;
            this.Width = width;
            this.Height = height;
        }

        public string InputText
        {
            get { return inputText1.Text; }
            set { inputText1.Text = ""; }
        }


        private void ok_button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}

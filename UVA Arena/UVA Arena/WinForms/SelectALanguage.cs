using System;
using System.Windows.Forms;

namespace UVA_Arena
{
    public partial class CodeFileCreator : Form
    {
        public CodeFileCreator()
        {
            InitializeComponent();
        }

        public UVA_Arena.Structures.Language Language
        {
            get
            {
                if (javaRadio.Checked)
                    return Structures.Language.Java;
                else if (ansiCradio.Checked)
                    return Structures.Language.C;
                else if (cPPradio.Checked)
                    return Structures.Language.CPP;
                else if (pascalRadio.Checked)
                    return Structures.Language.Pascal;
                return Structures.Language.CPP;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

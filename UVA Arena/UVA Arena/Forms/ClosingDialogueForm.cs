namespace UVA_Arena
{
    public partial class ClosingDialogueForm : System.Windows.Forms.Form
    {
        public ClosingDialogueForm()
        {
            InitializeComponent();
        }

        private void yesButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void noButton_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }
    }
}

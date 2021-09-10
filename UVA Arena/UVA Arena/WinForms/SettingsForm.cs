using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class SettingsForm : Form
    {

        //
        // Load Settings
        //

        public SettingsForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitializeAll();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public void InitializeAll()
        {
            //general settings
            SetCurrentUsername();
            currentCodeDir.Text = RegistryAccess.CodesPath;

            //editor
            LoadEditorSettings();

            //compiler
            LoadCompilerSettings();

            //precode
            LoadPrecode();
        }

        //
        // General Settings
        //
        public void SetCurrentUsername()
        {
            //general settings
            current_username.Text = string.Format("Current: {0} ({1})",
                RegistryAccess.DefaultUsername,
                LocalDatabase.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void username_button1_Click(object sender, EventArgs e)
        {
            Interactivity.ShowUserNameForm();
        }

        private void downloadAll_Click(object sender, EventArgs e)
        {
            Interactivity.ShowDownloadAllForm();
        }

        private void backupData_Click(object sender, EventArgs e)
        {
            Functions.BackupData();
        }

        private void restoreData_Click(object sender, EventArgs e)
        {
            Functions.RestoreData();
        }

        private void backupRegistryButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "UVA_Arena_backup.reg";
                sfd.Filter = "Registry File|*.reg";
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Functions.BackupRegistryData(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void changeCodeButton_Click(object sender, EventArgs e)
        {
            if (Interactivity.codesBrowser != null &&
                !Interactivity.codesBrowser.IsDisposed)
            {
                Interactivity.codesBrowser.ChangeCodeDirectory();
                currentCodeDir.Text = RegistryAccess.CodesPath;
            }
        }

        private void formatCodeButton_Click(object sender, EventArgs e)
        {
            if (Interactivity.codesBrowser != null &&
                !Interactivity.codesBrowser.IsDisposed)
                Interactivity.codesBrowser.FormatCodeDirectory(true);
        }

        private void linklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start(((LinkLabel)sender).Text); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //
        // Editor Settings
        //
        private void LoadEditorSettings()
        {
            if (Interactivity.codes == null) return;
            if (Interactivity.codes.IsDisposed) return;

            Font font = Interactivity.codes.codeTextBox.Font;
            fontname.Tag = font;
            fontname.Text = font.ToString();
            fontname.Font = new Font(font.FontFamily, 8.5F, font.Style);
            showHints.Checked = Properties.Settings.Default.EditorShowHints;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.FontMustExist = true;
            fd.Font = fontname.Font;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Interactivity.codes.codeTextBox.Font = fd.Font;
                fontname.Tag = fd.Font;
                fontname.Text = fd.Font.ToString();
                fontname.Font = new Font(fd.Font.FontFamily, 8.5F, fd.Font.Style);
                Properties.Settings.Default.EditorFont = fd.Font;
            }
            fd.Dispose();
        }

        private void showHints_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.EditorShowHints = showHints.Checked;
        }

        private void changeShortcuts_Click(object sender, EventArgs e)
        {
            try
            {
                var hotkey = new FastColoredTextBoxNS.HotkeysEditorForm(
                    Interactivity.codes.codeTextBox.HotkeysMapping);
                if (hotkey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Interactivity.codes.codeTextBox.HotkeysMapping = hotkey.GetHotkeys();
                }
                hotkey.Dispose();
            }
            catch { }
        }

        private void currentLineColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = currentLineColor.BackColor;
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentLineColor.BackColor = cd.Color;
            }
        }

        //
        // Compiler Settings
        //

        private void LoadCompilerSettings()
        {
            minGWLocation.Text = RegistryAccess.MinGWCompilerPath;
            jdkLocation.Text = RegistryAccess.JDKCompilerPath;

            cCompilerOptions.Text = RegistryAccess.CCompilerOption;
            cppCompilerOptions.Text = RegistryAccess.CPPCompilerOption;
            javaCompilerOptions.Text = RegistryAccess.JavaCompilerOption;
        }

        private void CompilerOptions_TextChanged(object sender, EventArgs e)
        {
            RegistryAccess.CCompilerOption = cCompilerOptions.Text;
            RegistryAccess.CPPCompilerOption = cppCompilerOptions.Text;
            RegistryAccess.JavaCompilerOption = javaCompilerOptions.Text;
        }

        private void minGW_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the path where MinGW is installed.";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //check validity
                string path = Path.Combine(fbd.SelectedPath, "bin");
                string gcc = Path.Combine(path, @"gcc.exe");
                string gpp = Path.Combine(path, @"g++.exe");
                if (!(File.Exists(gcc) && File.Exists(gpp)))
                {
                    MessageBox.Show("Selected path doesn't seem to be valid.");
                    return;
                }
                //set data 
                RegistryAccess.MinGWCompilerPath = fbd.SelectedPath;
            }
            fbd.Dispose();
        }

        private void jdkLOC_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the path where JDK is installed.";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //check validity
                string path = Path.Combine(fbd.SelectedPath, "bin");
                string javac = Path.Combine(path, @"javac.exe");
                string java = Path.Combine(path, @"java.exe");
                if (!(File.Exists(javac) && File.Exists(java)))
                {
                    MessageBox.Show("Selected path doesn't seem to be valid.");
                    return;
                }
                //set data 
                RegistryAccess.JDKCompilerPath = fbd.SelectedPath;
            }
            fbd.Dispose();
        }

        //
        // Precode Settings
        //
        private string getFile()
        {
            Structures.Language lang = Language.CPP;
            if (ansiCradioButton.Checked)
                lang = Language.C;
            else if (JavaRadioButton.Checked)
                lang = Language.Java;
            else if (PascalRadioButton.Checked)
                lang = Language.Pascal;
            else
                lang = Language.CPP;
            return LocalDirectory.GetPrecode(lang);
        }

        private void SavePrecode()
        {
            codeTextBox.SaveToFile(getFile(), Encoding.UTF8);
        }

        private void LoadPrecode()
        {
            codeTextBox.OpenFile(getFile());
            codeTextBox.Tag = codeTextBox.Text;
        }

        private void codeTextBox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            SavePrecode();
            if (ansiCradioButton.Checked || cppRadioButton.Checked)
                HighlightSyntax.HighlightCPPSyntax(codeTextBox.Range);
            else if (JavaRadioButton.Checked)
                HighlightSyntax.HighlightJavaSyntax(codeTextBox.Range);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadPrecode();
        }

        private void cancelCodeButton_Click(object sender, EventArgs e)
        {
            if (codeTextBox.Tag == null) codeTextBox.Tag = "";
            codeTextBox.Text = (string)codeTextBox.Tag;
        }

        private void saveCodeButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Precode";
            sfd.Filter = "All Files|*.*";
            if (cppRadioButton.Checked) sfd.Filter = "CPP Files|*.cpp|Header Files|*.h*|All Files|*.*";
            if (ansiCradioButton.Checked) sfd.Filter = "C Files|*.c|Header Files|*.h*|All Files|*.*";
            if (JavaRadioButton.Checked) sfd.Filter = "Java Files|*.java|All Files|*.*";
            if (PascalRadioButton.Checked) sfd.Filter = "Pascal Files|*.pascal|All Files|*.*";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                codeTextBox.SaveToFile(sfd.FileName, Encoding.UTF8);
            }
        }

        private void openCodeFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "All Files|*.*";
            if (cppRadioButton.Checked) ofd.Filter = "CPP Files|*.cpp|Header Files|*.h*|All Files|*.*";
            if (ansiCradioButton.Checked) ofd.Filter = "C Files|*.c|Header Files|*.h*|All Files|*.*";
            if (JavaRadioButton.Checked) ofd.Filter = "Java Files|*.java|All Files|*.*";
            if (PascalRadioButton.Checked) ofd.Filter = "Pascal Files|*.pascal|All Files|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (LocalDirectory.GetFileSize(ofd.FileName) < 1024 * 512)
                {
                    codeTextBox.OpenFile(ofd.FileName);
                }
                else
                {
                    MessageBox.Show("File is too big. Please select a valid file");
                }
            }
        }


    }
}

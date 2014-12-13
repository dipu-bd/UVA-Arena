using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class SettingsForm : Form
    {
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            restoreButton.Visible = (tabControl1.SelectedTab == editorTab);
        }

        #region Load Settings

        public void InitializeAll()
        {
            //general settings
            SetCurrentUsername();
            currentCodeDir.Text = Elements.CODES.CodesPath;

            //compiler settings
            minGWLocation.Text = Elements.CODES.MinGWLocation;
            jdkLocation.Text = Elements.CODES.JDKLocation;
            cCompilerOptions.Text = Elements.CODES.CCompilerOptions;
            cppCompilerOptions.Text = Elements.CODES.CPPCompilerOptions;
            javaCompilerOptions.Text = Elements.CODES.JavaCompilerOptions;

            //editor
            if (Interactivity.codes != null && !Interactivity.codes.IsDisposed)
            {
                Font font = Interactivity.codes.codeTextBox.Font;
                fontname.Text = font.ToString();
                fontname.Tag = font;
                fontname.Font = new Font(font.FontFamily, 8.5F, font.Style);
                autoCompleteBrackets.Checked = Interactivity.codes.codeTextBox.AutoCompleteBrackets;
                autoIndent.Checked = Interactivity.codes.codeTextBox.AutoIndent;
                autoIndentChars.Checked = Interactivity.codes.codeTextBox.AutoIndentChars;
                showLineNumbers.Checked = Interactivity.codes.codeTextBox.ShowLineNumbers;
                showFoldingLines.Checked = Interactivity.codes.codeTextBox.ShowFoldingLines;
                wordWraps.Checked = Interactivity.codes.codeTextBox.WordWrap;
                highlightFoldingIndicator.Checked = Interactivity.codes.codeTextBox.HighlightFoldingIndicator;
                showHints.Checked = Elements.CODES.ShowHints;
                autoIndentChars.Enabled = autoIndent.Checked;
            }

        }
        #endregion

        #region General Settings

        public void SetCurrentUsername()
        {
            //general settings
            current_username.Text = string.Format("Current: {0} ({1})",
                RegistryAccess.DefaultUsername,
                DefaultDatabase.GetUserid(RegistryAccess.DefaultUsername));
        }

        private void username_button1_Click(object sender, EventArgs e)
        {
            UsernameForm uf = new UsernameForm();
            uf.Show();
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

        private void changeCodeButton_Click(object sender, EventArgs e)
        {
            if (Interactivity.codes != null && !Interactivity.codes.IsDisposed)
            {
                Interactivity.codes.browseFolderButton.PerformClick();
                currentCodeDir.Text = Elements.CODES.CodesPath;
            }
        }

        private void formatCodeButton_Click(object sender, EventArgs e)
        {
            if (Interactivity.codes != null && !Interactivity.codes.IsDisposed)
                Interactivity.codes.FormatCodeDirectory(true);
        }

        private void linklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try { Process.Start(((LinkLabel)sender).Text); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region Editor Settings

        private FontStyle[] StyleIndex = { 
                    FontStyle.Regular, FontStyle.Italic, FontStyle.Bold, FontStyle.Underline, 
                    FontStyle.Bold | FontStyle.Italic, FontStyle.Underline | FontStyle.Italic,
                    FontStyle.Underline | FontStyle.Bold, FontStyle.Underline | FontStyle.Bold | FontStyle.Italic };

        private int ConvertFromStyle(FontStyle fs)
        {
            for (int i = 7; i >= 0; --i) if (fs == StyleIndex[i]) return i;
            return 0;
        }

        private FontStyle ConvertToStyle(int i)
        {
            return StyleIndex[i];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = (Font)fontname.Tag;
            fd.FontMustExist = true;
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    Interactivity.codes.codeTextBox.Font = fd.Font;
                    fontname.Text = fd.Font.ToString();
                    fontname.Tag = fd.Font;
                    fontname.Font = new Font(fd.Font.FontFamily, 8.5F, fd.Font.Style);
                }
                catch { }
            }
        }

        private void changeShortcuts_Click(object sender, EventArgs e)
        {
            try
            {
                FastColoredTextBoxNS.HotkeysEditorForm hotkey = new FastColoredTextBoxNS.HotkeysEditorForm(Interactivity.codes.codeTextBox.HotkeysMapping);
                if (hotkey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Interactivity.codes.codeTextBox.HotkeysMapping = hotkey.GetHotkeys();
                }
            }
            catch { }
        }

        private void CheckValue_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox cb = (CheckBox)sender;
                cb.Checked = !cb.Checked;
                bool check = cb.Checked;
                if (cb == autoCompleteBrackets) Interactivity.codes.codeTextBox.AutoCompleteBrackets = check;
                else if (cb == autoIndent) Interactivity.codes.codeTextBox.AutoIndent = check;
                else if (cb == autoIndentChars) Interactivity.codes.codeTextBox.AutoIndentChars = check;
                else if (cb == showLineNumbers) Interactivity.codes.codeTextBox.ShowLineNumbers = check;
                else if (cb == showFoldingLines) Interactivity.codes.codeTextBox.ShowFoldingLines = check;
                else if (cb == wordWraps) Interactivity.codes.codeTextBox.WordWrap = check;
                else if (cb == highlightFoldingIndicator) Interactivity.codes.codeTextBox.HighlightFoldingIndicator = check;
                else if (cb == showHints) Elements.CODES.ShowHints = check;
                autoIndentChars.Enabled = autoIndent.Checked;
            }
            catch { }
        }

        #endregion

        #region Compiler Settings

        private void minGW_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the path where MinGW is installed.";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //check validity
                string path = Path.Combine(fbd.SelectedPath, "bin");
                string gcc = Path.Combine(path, @"mingw32-gcc.exe");
                string gpp = Path.Combine(path, @"mingw32-g++.exe");
                if (!(File.Exists(gcc) && File.Exists(gpp)))
                {
                    MessageBox.Show("Selected path doesn't seems to be valid.");
                    return;
                }
                //set data
                minGWLocation.Text = fbd.SelectedPath;
                Elements.CODES.MinGWLocation = fbd.SelectedPath;
            }
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
                    MessageBox.Show("Selected path doesn't seems to be valid.");
                    return;
                }
                //set data
                jdkLocation.Text = fbd.SelectedPath;
                Elements.CODES.JDKLocation = fbd.SelectedPath;
            }
        }

        private void cCompilerOptions_TextChanged(object sender, EventArgs e)
        {
            Elements.CODES.CCompilerOptions = cCompilerOptions.Text;
        }

        private void cppCompilerOptions_TextChanged(object sender, EventArgs e)
        {
            Elements.CODES.CPPCompilerOptions = cppCompilerOptions.Text;
        }

        private void javaCompilerOptions_TextChanged(object sender, EventArgs e)
        {
            Elements.CODES.JavaCompilerOptions = javaCompilerOptions.Text;
        }

        #endregion

        #region Precode

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            codeTextBox.Enabled = true;
            cancelCodeButton.PerformClick();
        }

        private void codeTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                case 1:
                    HighlightSyntax.HighlightCPPSyntax(e.ChangedRange);
                    break;
                case 2:
                    HighlightSyntax.HighlightJavaSyntax(e.ChangedRange);
                    break;
            }

        }

        private void saveCodeButton_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Elements.CODES.CPrecode = codeTextBox.Text;
                    break;
                case 1:
                    Elements.CODES.CPPPrecode = codeTextBox.Text;
                    break;
                case 2:
                    Elements.CODES.JavaPrecode = codeTextBox.Text;
                    break;
                default:
                    Elements.CODES.PascalPrecode = codeTextBox.Text;
                    break;
            }
        }

        private void cancelCodeButton_Click(object sender, EventArgs e)
        {
            string text = "";
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    text = Elements.CODES.CPrecode;
                    break;
                case 1:
                    text = Elements.CODES.CPPPrecode;
                    break;
                case 2:
                    text = Elements.CODES.JavaPrecode;
                    break;
                default:
                    text = Elements.CODES.PascalPrecode;
                    break;
            }
            codeTextBox.Text = text;
        }

        #endregion

    }
}

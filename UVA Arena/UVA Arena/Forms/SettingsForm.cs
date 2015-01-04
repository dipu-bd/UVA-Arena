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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            restoreButton.Visible = (tabControl1.SelectedTab == editorTab);
        }
        public void InitializeAll()
        {
            //general settings
            SetCurrentUsername();
            currentCodeDir.Text = Elements.CODES.CodesPath;
             
            //editor
            LoadEditorSettings();

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
                var hotkey = new FastColoredTextBoxNS.HotkeysEditorForm(Interactivity.codes.codeTextBox.HotkeysMapping);
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
        private void minGW_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select the path where MinGW is installed.";
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //check validity
                    string path = Path.Combine(fbd.SelectedPath, "bin");
                    string gcc = Path.Combine(path, @"mingw32-gcc.exe");
                    string gpp = Path.Combine(path, @"mingw32-g++.exe");
                    if (!(File.Exists(gcc) && File.Exists(gpp)))
                    {
                        MessageBox.Show("Selected path doesn't seem to be valid.");
                        return;
                    }
                    //set data 
                    Properties.Settings.Default.MinGWLocation = fbd.SelectedPath;
                }
            }
        }

        private void jdkLOC_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
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
                    Properties.Settings.Default.JDKLocation = fbd.SelectedPath;
                }
            }
        }
         
        //
        // Precode Settings
        //
        private void codeTextBox_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (ansiCradioButton.Checked || cppRadioButton.Checked)
                HighlightSyntax.HighlightCPPSyntax(e.ChangedRange);
            else if (JavaRadioButton.Checked)
                HighlightSyntax.HighlightJavaSyntax(e.ChangedRange);
        }

        private void saveCodeButton_Click(object sender, EventArgs e)
        {
            string text = codeTextBox.Text;
            if (ansiCradioButton.Checked)
                Properties.Settings.Default.CPrecode = text;
            else if (cppRadioButton.Checked)
                Properties.Settings.Default.CPPPrecode = text;
            else if (JavaRadioButton.Checked)
                Properties.Settings.Default.JavaPrecode = text;
            else if (PascalRadioButton.Checked)
                Properties.Settings.Default.PascalPrecode = text;
        }

        private void LoadPrecode()
        {
            string text = "";
            if (ansiCradioButton.Checked)
                text = Properties.Settings.Default.CPrecode;
            else if (cppRadioButton.Checked)
                text = Properties.Settings.Default.CPPPrecode;
            else if (JavaRadioButton.Checked)
                text = Properties.Settings.Default.JavaPrecode;
            else if (PascalRadioButton.Checked)
                text = Properties.Settings.Default.PascalPrecode;
            codeTextBox.Text = text;
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadPrecode();
        }

        private void cancelCodeButton_Click(object sender, EventArgs e)
        {
            LoadPrecode();
        }


    }
}

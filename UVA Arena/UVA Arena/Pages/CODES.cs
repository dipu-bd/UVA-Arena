using FastColoredTextBoxNS;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using UVA_Arena.Utilities;

namespace UVA_Arena.Elements
{
    public partial class CODES : UserControl
    {

        #region Startup Functions

        public CODES()
        {
            InitializeComponent();

            compilerSplitContainer1.SplitterDistance =
                    (int)Math.Round(Properties.Settings.Default.CompilerSplitterRatio * compilerSplitContainer1.Height);

            // mainContainer.SplitterDistance =
            //     (int)Math.Round(Properties.Settings.Default.CodesSplitterRatio * mainContainer.Width);

            Interactivity.codesBrowser = new CodesBrowser();
            Interactivity.codesBrowser.Dock = DockStyle.Fill;
            mainContainer.Panel1.Controls.Add(Interactivity.codesBrowser);

            codeTextBox.Font = Properties.Settings.Default.EditorFont;
            CustomLang = Structures.Language.CPP;
            SelectedPNUM = -1;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Stylish.SetGradientBackground(filenamePanel,
                new Stylish.GradientStyle(Color.LightBlue, Color.PaleTurquoise, 90F));

            Stylish.SetGradientBackground(toolStrip1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightBlue, -90F));
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //Properties.Settings.Default.CodesSplitterRatio =
            //    (double)mainContainer.SplitterDistance / mainContainer.Width;
        }

        #endregion

        #region Object Properties

        private long MaxFileSIZ = 3 * 1024 * 1024;
        private AutocompleteMenu autoCompleteMenu;

        public long SelectedPNUM { get; set; }
        public FileInfo CurrentFile { get; set; }

        public Structures.Language CustomLang { get; set; }

        public string OpenedInput { get; set; }

        public string OpenedOutput { get; set; }

        public string OpenedCorrect { get; set; }

        #endregion


        #region Open Code File

        //
        // Open File
        //
        public void OpenFile(FileInfo file, bool history = true)
        {
            //check if file is okay, manage history keeping and clear data
            if (!PrecheckOpenFile(file, history)) return;

            //open file normally
            OpenCodeFile(file.FullName);
            codeTextBox.ReadOnly = false;
            CurrentFile = file; //set current file info
            fileNameLabel.Text = file.Name;

            //load code text box
            SetLanguage(file.Extension);
            HighlightCodebox(codeTextBox.Range); //highlight immediately after loading language
            MakeAutoCompleteMenu();

            //select and expand to opened node if not already selected
            CodesBrowser.ExpandAndSelect(Interactivity.codesBrowser.GetNode(file));

            //set problem number
            if (CanHaveIOFiles(codeTextBox.Language))
            {
                SelectedPNUM = LocalDatabase.GetProblemNumber(file.Name);
            }

            //open input-output for valid problem number
            if (SelectedPNUM == -1) return;

            //change file name label text to include problem number and title
            fileNameLabel.Text = string.Format("Problem {0} - {1} ({2})",
                  SelectedPNUM, LocalDatabase.GetTitle(SelectedPNUM), fileNameLabel.Text);
            //as there are IO files we can run tests
            runtestToolButton.Enabled = true;
            //make input output files
            MakeInputOutput(file.DirectoryName);

            //set runtime limit
            long timelim = LocalDatabase.GetProblem(SelectedPNUM).rtl;
            timeLimitCombo.Text = (timelim / 1000.0).ToString("F2");

            // load udebug
            this.LoadUDebug();
        }

        private bool PrecheckOpenFile(FileInfo file, bool history = true)
        {
            //check if already opened 
            if (CurrentFile == file) return false;

            //add to history
            if (history && CurrentFile != null)
            {
                AddToPrev();
                nextToolMenu.Enabled = false;
                nextToolMenu.DropDownItems.Clear();
            }

            //clear previous values
            ClearPrevOpenedFiles();

            //check file validity
            if (file == null) return false;

            //check extension validity
            Regex invalid = new Regex(@".(exe|dll|o|class)");
            return !invalid.IsMatch(file.Extension.ToLower());
        }

        private void ClearPrevOpenedFiles()
        {
            tabControl1.SelectedTab = codeTAB;

            SelectedPNUM = -1;
            CurrentFile = null;
            fileNameLabel.Text = "No Opened File";
            CustomLang = Structures.Language.Other;

            compilerOutput.Clear();
            runtestToolButton.Enabled = false;

            codeTextBox.Clear();
            codeTextBox.ReadOnly = true;

            OpenedInput = null;
            inputTextBox.Clear();
            inputTextBox.ReadOnly = true;

            OpenedOutput = null;
            outputTextBox.Clear();
            outputTextBox.Clear();

            OpenedCorrect = null;
            correctOutputTextBox.Clear();
            correctOutputTextBox.ReadOnly = true;
        }


        private void SetLanguage(string ext)
        {
            ext = ext.ToLower();
            if (ext == ".cs")
                codeTextBox.Language = Language.CSharp;
            else if (ext == ".vb")
                codeTextBox.Language = Language.VB;
            else if (ext == ".php")
                codeTextBox.Language = Language.PHP;
            else if (ext == ".sql")
                codeTextBox.Language = Language.SQL;
            else if (ext == ".lua")
                codeTextBox.Language = Language.Lua;
            else if (ext == ".js")
                codeTextBox.Language = Language.JS;
            else if (ext.StartsWith(".xml"))
                codeTextBox.Language = Language.XML;
            else if (ext.StartsWith(".htm"))
                codeTextBox.Language = Language.HTML;
            else
            {
                codeTextBox.Language = Language.Custom;
                if (ext == ".c")
                    CustomLang = Structures.Language.C;
                else if (ext == ".java")
                    CustomLang = Structures.Language.Java;
                else if (ext == ".cpp" || ext == ".h")
                    CustomLang = Structures.Language.CPP;
                else if (ext == ".pascal")
                    CustomLang = Structures.Language.Pascal;
                else
                    CustomLang = Structures.Language.Other;
            }
        }

        private void MakeAutoCompleteMenu()
        {
            string keyword = null;
            if (CustomLang == Structures.Language.CPP)
                keyword = Properties.Resources.CPPKeyword;
            else if (CustomLang == Structures.Language.Java)
                keyword = Properties.Resources.JavaKeyword;

            if (keyword == null)
            {
                if (autoCompleteMenu != null)
                {
                    autoCompleteMenu.Items.SetAutocompleteItems(new string[] { });
                }
            }
            else
            {
                string[] items = keyword.Split(new char[] { '|' });
                autoCompleteMenu = new AutocompleteMenu(codeTextBox);
                autoCompleteMenu.MinFragmentLength = 1;
                autoCompleteMenu.DropShadowEnabled = true;
                autoCompleteMenu.Items.SetAutocompleteItems(items);
                autoCompleteMenu.Items.MaximumSize = new System.Drawing.Size(200, 300);
                autoCompleteMenu.Items.Width = 200;
                autoCompleteMenu.AllowTransparency = true;
                autoCompleteMenu.Opacity = 0.8;
                autoCompleteMenu.AllowTabKey = true;
            }
        }

        private bool CanHaveIOFiles(Language lang)
        {
            //determine whether input-output file need to be created.
            switch (lang)
            {
                case Language.CSharp:
                case Language.VB:
                    return true;
                case Language.Custom:
                    return (CustomLang != Structures.Language.Other);
                default:
                    return false;
            }
        }

        private void MakeInputOutput(string dir)
        {
            try
            {
                //open input file
                string input = Path.Combine(dir, "input.txt");
                LocalDirectory.CreateFile(input);
                inputTextBox.ReadOnly = false;
                OpenedInput = input;
                OpenInputFile(input);

                //open output file
                string output = Path.Combine(dir, "output.txt");
                LocalDirectory.CreateFile(output);
                OpenedOutput = output;
                OpenOutputFile(output);

                //open correct output file
                string correct = Path.Combine(dir, "correct.txt");
                LocalDirectory.CreateFile(correct);
                correctOutputTextBox.ReadOnly = false;
                OpenedCorrect = correct;
                OpenCorrectFile(correct);
            }
            catch { }
        }

        #endregion

        #region History Keeping

        private static ToolStripItem GetToolItem(FileInfo file)
        {
            ToolStripMenuItem tmi = new ToolStripMenuItem();
            tmi.Text = file.Name;
            tmi.Tag = file;
            tmi.ToolTipText = file.FullName;
            return tmi;
        }

        private void AddToPrev()
        {
            if (CurrentFile == null) return;
            ToolStripItem tmi = GetToolItem(CurrentFile);
            tmi.Click += prevItemClick;
            prevToolMenu.DropDownItems.Insert(0, tmi);
            prevToolMenu.Enabled = true;
        }

        private void AddToNext()
        {
            if (CurrentFile == null) return;
            ToolStripItem tmi = GetToolItem(CurrentFile);
            tmi.Click += nextItemClick;
            nextToolMenu.DropDownItems.Insert(0, tmi);
            nextToolMenu.Enabled = true;
        }

        private void prevToolMenu_ButtonClick(object sender, EventArgs e)
        {
            if (!prevToolMenu.Enabled) return;
            AddToNext();
            ToolStripItem tmi = prevToolMenu.DropDownItems[0];
            OpenFile((FileInfo)tmi.Tag, false);
            prevToolMenu.DropDownItems.RemoveAt(0);
            prevToolMenu.Enabled = (prevToolMenu.DropDownItems.Count > 0);
        }

        private void nextToolMenu_ButtonClick(object sender, EventArgs e)
        {
            if (!nextToolMenu.Enabled) return;
            AddToPrev();
            ToolStripItem tmi = nextToolMenu.DropDownItems[0];
            OpenFile((FileInfo)tmi.Tag, false);
            nextToolMenu.DropDownItems.RemoveAt(0);
            nextToolMenu.Enabled = (nextToolMenu.DropDownItems.Count > 0);
        }

        private void prevItemClick(object sender, EventArgs e)
        {
            //add current problem to next
            AddToNext();
            //add all except tmi to next
            ToolStripItem tmi = (ToolStripItem)sender;
            while (tmi != prevToolMenu.DropDownItems[0])
            {
                ToolStripItem ti = GetToolItem((FileInfo)prevToolMenu.DropDownItems[0].Tag);
                ti.Click += nextItemClick;
                nextToolMenu.DropDownItems.Insert(0, ti);
                prevToolMenu.DropDownItems.RemoveAt(0);
            }
            //show tmi
            prevToolMenu.PerformClick();
        }

        private void nextItemClick(object sender, EventArgs e)
        {
            //add current problem to prev
            AddToPrev();
            //add all except tmi to prev
            ToolStripItem tmi = (ToolStripItem)sender;
            while (tmi != nextToolMenu.DropDownItems[0])
            {
                ToolStripItem ti = GetToolItem((FileInfo)nextToolMenu.DropDownItems[0].Tag);
                ti.Click += prevItemClick;
                prevToolMenu.DropDownItems.Insert(0, ti);
                nextToolMenu.DropDownItems.RemoveAt(0);
            }
            //show tmi
            nextToolMenu.PerformClick();
        }

        #endregion

        #region File Opening Functions

        private void _ShowMaxExceedMessage(FastColoredTextBox fctb)
        {
            string msg = "Error : Too large to open. ( > {0})";
            fctb.Text = string.Format(msg, Functions.FormatMemory(MaxFileSIZ));
        }

        //
        // File Open
        //
        public void OpenCodeFile(string file)
        {
            long siz = LocalDirectory.GetFileSize(file);
            if (siz <= 1) return;

            //do not open very large files
            if (siz > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(codeTextBox);
            }
            else
            {
                if (file.CompareTo(codeTextBox.Tag) == 0)
                {
                    codeTextBox.Text = File.ReadAllText(file);
                }
                else
                {
                    codeTextBox.Clear();
                    codeTextBox.Text = File.ReadAllText(file);
                    codeTextBox.ClearUndo();
                    codeTextBox.Tag = file;
                }
            }
        }

        public void OpenInputFile(string file)
        {
            long siz = LocalDirectory.GetFileSize(file);
            if (siz <= 1) return;

            if (OpenedInput != file) inputTextBox.Clear();

            //do not open very large files
            if (siz > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(inputTextBox);
            }
            else
            {
                inputTextBox.Text = File.ReadAllText(file);
            }
        }

        public void OpenOutputFile(string file)
        {
            long siz = LocalDirectory.GetFileSize(file);
            if (siz <= 1) return;

            if (OpenedOutput != file)
            {
                outputTextBox.Clear();
                outputTextBox.Clear();
            }

            //do not open very large files
            if (siz > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(outputTextBox);
            }
            else
            {
                outputTextBox.Text = File.ReadAllText(file);
            }

            outputTextBox.Text = outputTextBox.Text;
        }

        public void OpenCorrectFile(string file)
        {
            long siz = LocalDirectory.GetFileSize(file);
            if (siz <= 1) return;

            if (OpenedCorrect != file) correctOutputTextBox.Clear();

            //do not open very large files
            if (siz > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(correctOutputTextBox);
            }
            else
            {
                correctOutputTextBox.Text = File.ReadAllText(file);
            }
        }

        public void ClearOutputFile()
        {
            if (File.Exists(OpenedOutput))
                File.WriteAllText(OpenedOutput, "");
            OpenOutputFile(OpenedOutput);
        }

        #endregion


        #region Code Text Box

        //
        // Custom Actions
        //
        private void codeTextBox_CustomAction(object sender, CustomActionEventArgs e)
        {
            switch (e.Action)
            {
                case FCTBAction.CustomAction1: //save
                    saveToolButton.PerformClick();
                    break;
                case FCTBAction.CustomAction2: //compile
                    compileToolButton.PerformClick();
                    break;
                case FCTBAction.CustomAction3: //execute
                    runtestToolButton.PerformClick();
                    break;
                case FCTBAction.CustomAction4: //compile and execute
                    buildRunToolButton.PerformClick();
                    break;
                case FCTBAction.CustomAction5: //hide or show compiler
                    ToggleCompilerOutput();
                    break;
            }
        }

        /// <summary>
        /// Process all information currently in compilerOutput
        /// </summary>
        private void ProcessErrorData()
        {
            //clear previous hints and styles
            codeTextBox.ClearHints();
            codeTextBox.Range.ClearStyle(new Style[] { HighlightSyntax.LineErrorStyle,
                HighlightSyntax.LineNoteStyle, HighlightSyntax.LineWarningStyle});

            //focus
            bool focus = true;
            for (int i = 0; i < compilerOutput.Lines.Count; ++i)
            {
                if (SetCodeError(i, focus)) focus = false;
            }
        }

        //
        // Set code error marker
        //
        private bool SetCodeError(int line, bool focus = false)
        {
            //get range from line-number
            int len = compilerOutput.GetLineLength(line);
            Place start = new Place(0, line); //start of the line
            Place stop = new Place(len, line); //end of the line
            Range range = compilerOutput.GetRange(start, stop);

            //process if valid language is selected
            if (CustomLang == Structures.Language.C
                || CustomLang == Structures.Language.CPP)
            {
                //process c or c++ error message
                return HighlightSyntax.SetCPPCodeError(codeTextBox, range, focus);
            }
            else if (CustomLang == Structures.Language.Java)
            {
                //process java error message
                return HighlightSyntax.SetJavaCodeError(codeTextBox, range, focus);
            }

            return false;
        }

        //
        // Set Highlight when needed
        //
        private void HighlightCodebox(Range range)
        {
            //highlight built in languages
            switch (codeTextBox.Language)
            {
                case Language.CSharp:
                    codeTextBox.SyntaxHighlighter.CSharpSyntaxHighlight(range);
                    return;
                case Language.HTML:
                    codeTextBox.SyntaxHighlighter.HTMLSyntaxHighlight(range);
                    return;
                case Language.JS:
                    codeTextBox.SyntaxHighlighter.JScriptSyntaxHighlight(range);
                    return;
                case Language.Lua:
                    codeTextBox.SyntaxHighlighter.LuaSyntaxHighlight(range);
                    return;
                case Language.PHP:
                    codeTextBox.SyntaxHighlighter.PHPSyntaxHighlight(range);
                    return;
                case Language.SQL:
                    codeTextBox.SyntaxHighlighter.SQLSyntaxHighlight(range);
                    return;
                case Language.VB:
                    codeTextBox.SyntaxHighlighter.VBSyntaxHighlight(range);
                    return;
                case Language.XML:
                    codeTextBox.SyntaxHighlighter.XMLSyntaxHighlight(range);
                    return;
            }

            //highlight custom languages
            switch (CustomLang)
            {
                case Structures.Language.C:
                case Structures.Language.CPP:
                case Structures.Language.CPP11:
                    HighlightSyntax.HighlightCPPSyntax(range);
                    return;
                case Structures.Language.Java:
                    HighlightSyntax.HighlightJavaSyntax(range);
                    return;
            }
        }


        private Range GetMessageRangeFromPlace(Place cur)
        {
            //do not check when file in on build
            if (!buildRunToolButton.Enabled) return null;

            //check is error message contains
            bool message = false;
            foreach (Style style in codeTextBox.GetStylesOfChar(cur))
            {
                if (style == HighlightSyntax.LineWarningStyle
                    || style == HighlightSyntax.LineErrorStyle
                    || style == HighlightSyntax.LineNoteStyle)
                {
                    message = true;
                    break;
                }
            }
            if (!message) return null;

            //select message in compiler output
            string pat = (1 + cur.iLine).ToString() + ":";
            for (int i = 0; i < compilerOutput.Lines.Count; ++i)
            {
                if (compilerOutput.Lines[i].StartsWith(pat))
                {
                    Place start = new Place(0, i);
                    Place stop = new Place(compilerOutput.GetLineLength(i), i);
                    return compilerOutput.GetRange(start, stop);
                }
            }

            return null;
        }

        //
        // CodeTextBox events
        //

        private void codeTextBox_SelectionChangedDelayed(object sender, EventArgs e)
        {
            HighlightSyntax.SelectSameWords(codeTextBox);
        }

        private void FCTB_SelectionChanged(object sender, EventArgs e)
        {
            //selection start
            FastColoredTextBox tb = (FastColoredTextBox)sender;
            Place cur = tb.Selection.Start;
            //show current line and column position
            int line = cur.iLine + 1;
            int col = cur.iChar + 1;
            CurLnLabel.Text = string.Format((string)CurLnLabel.Tag, line);
            CurColLabel.Text = string.Format((string)CurColLabel.Tag, col);
        }

        private void codeTextBox_ToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            Range range = GetMessageRangeFromPlace(e.Place);
            if (range != null)
            {
                e.ToolTipText = range.Text;
            }
        }

        private void codeTextBox_HintClick(object sender, HintClickEventArgs e)
        {
            try
            {
                //get range where the error is
                Range range = (Range)e.Hint.Tag;
                //select the error
                compilerOutput.Selection = range;
                //make error visible 
                compilerOutput.DoRangeVisible(range, true);
            }
            catch { }
        }

        private void codeTextBox_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            HighlightCodebox(codeTextBox.Range);
        }

        private void fctb1_AutoIndentNeeded(object sender, AutoIndentEventArgs e)
        {
            //auto index <-- also enables format option
            if (codeTextBox.Language == Language.Custom)
            {
                //currently indent indifferently for all languages 
                // <-- need some extra work for individuals
                HighlightSyntax.AutoIndent(sender, e);
            }
        }

        #endregion

        #region Code Toolbar / Context Menu / Tab Control

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!LocalDatabase.HasProblem(SelectedPNUM))
            {
                e.Cancel = !(e.TabPage == codeTAB);
            }
            if (e.Cancel)
            {
                MessageBox.Show("Select a problem's source code to enable this feature.");
            }
        }

        private void undoToolButton_Click(object sender, EventArgs e)
        {
            codeTextBox.Undo();
        }

        private void redoToolButton_Click(object sender, EventArgs e)
        {
            codeTextBox.Redo();
        }

        private void codeTextBox_UndoRedoStateChanged(object sender, EventArgs e)
        {
            undoToolButton.Enabled = codeTextBox.UndoEnabled;
            redoToolButton.Enabled = codeTextBox.RedoEnabled;
        }

        private void saveToolButton_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null) return;
            codeTextBox.SaveToFile(CurrentFile.FullName, Encoding.Default);
        }

        private void copyToolButton_Click(object sender, EventArgs e)
        {
            if (codeTextBox.Text.Length > 0)
            {
                Clipboard.SetText(codeTextBox.Text, TextDataFormat.Text);
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            codeTextBox.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (codeTextBox.ReadOnly) return;
            codeTextBox.Cut();
        }

        private void pasteToolButton_Click(object sender, EventArgs e)
        {
            if (codeTextBox.ReadOnly) return;
            codeTextBox.Paste();
        }

        private void findToolButton_Click(object sender, EventArgs e)
        {
            if (codeTextBox.ReadOnly) return;
            codeTextBox.ShowFindDialog(codeTextBox.SelectedText);
        }

        private void reloadToolButton_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null) return;
            codeTextBox.Clear();
            OpenCodeFile(CurrentFile.FullName);
        }

        private void formatAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeTextBox.DoAutoIndent();
        }


        private void exploreToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentFile == null) return;
                string file = CurrentFile.FullName;
                string folder = Path.GetDirectoryName(file);
                System.Diagnostics.Process.Start(folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void externalToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentFile == null) return;
                string file = CurrentFile.FullName;
                System.Diagnostics.Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void problemToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SelectedPNUM == -1)
                {
                    MessageBox.Show("No problem is selected.");
                    return;
                }
                Interactivity.ShowProblem(SelectedPNUM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void submitToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentFile == null || !LocalDatabase.HasProblem(SelectedPNUM))
                {
                    MessageBox.Show("Select code of a problem first.");
                    return;
                }
                string code = File.ReadAllText(CurrentFile.FullName);
                Interactivity.SubmitCode(SelectedPNUM, code, CustomLang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        #region Compilation Tool Bar

        //
        // Compile and Run
        //
        private double RunTimeLimit = 3.000;

        private void BuildAndRun(object state)
        {
            //if no file opened
            if (Interactivity.codes.CurrentFile == null) return;

            //clear and initiate
            Interactivity.codes.BeginInvoke((MethodInvoker)delegate
            {
                ClearOutputFile(); //clear output file
                saveInputOutputData(); //save input output data
                saveToolButton.PerformClick(); //save code
                codeTextBox.ClearHints(); //clear hints                
                compilerOutput.Clear(); //clear prev compiler report
                compileToolButton.Enabled = false;
                buildRunToolButton.Enabled = false;
                runtestToolButton.Enabled = false;
                forceStopToolButton.Enabled = true;
                //show compiler output
                if (compilerOutputIsHidden) ToggleCompilerOutput();
            });

            //run task
            bool ok = CodeCompiler.BuildAndRun((BuildRunType)state,
                CurrentFile, SelectedPNUM, RunTimeLimit);


            //re-enable all data
            Interactivity.codes.BeginInvoke((MethodInvoker)delegate
            {
                //enable the build and run buttons
                compileToolButton.Enabled = true;
                buildRunToolButton.Enabled = true;
                forceStopToolButton.Enabled = false;
                //if no problem is selected do not enable runtest button
                runtestToolButton.Enabled = (SelectedPNUM != -1);
                //go to end of output
                compilerOutput.GoEnd();
                //wait a little before processing message data                
                Thread.Sleep(100);
                this.BeginInvoke((MethodInvoker)delegate { ProcessErrorData(); });
                //if no error and runtest
                if ((BuildRunType)state == BuildRunType.RunTest && ok)
                {
                    tabControl1.SelectedTab = ioTAB;
                    inputOutputTabControl.SelectedTab = inputTab;
                }
            });
        }

        //
        // Events in compilation toolbar
        //

        private void timeLimitCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timeLimitCombo_Leave(null, EventArgs.Empty);
            }
        }

        private void timeLimitCombo_Leave(object sender, EventArgs e)
        {
            double val = 0;
            string txt = timeLimitCombo.Text;
            if (!double.TryParse(txt, out val)) val = RunTimeLimit;
            if (val > 300) val = 300;
            if (val <= 0) val = 1;
            timeLimitCombo.Text = val.ToString("F2");
            RunTimeLimit = val;
        }

        private void buildRunToolButton_Click(object sender, EventArgs e)
        {
            if (!buildRunToolButton.Enabled) return;
            ThreadPool.QueueUserWorkItem(BuildAndRun, BuildRunType.BuildAndRun);
        }

        private void compileToolButton_Click(object sender, EventArgs e)
        {
            if (!compileToolButton.Enabled) return;
            ThreadPool.QueueUserWorkItem(BuildAndRun, BuildRunType.BuildOnly);
        }

        private void runtestToolButton_Click(object sender, EventArgs e)
        {
            if (!runtestToolButton.Enabled) return;
            if (SelectedPNUM == -1)
            {
                MessageBox.Show("Run test is only available for problems' code.");
                return;
            }
            ThreadPool.QueueUserWorkItem(BuildAndRun, BuildRunType.RunTest);
        }

        private void forceStopToolButton_Click(object sender, EventArgs e)
        {
            CodeCompiler.ForceStopTask();
        }

        //show hints
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ProcessErrorData();
        }

        //
        // Precode
        //
        private void precodeToolButton_ButtonClick(object sender, EventArgs e)
        {
            if (CurrentFile == null)
            {
                MessageBox.Show("Create a code file first.");
            }
            else
            {
                string file = LocalDirectory.GetPrecode(CustomLang);
                codeTextBox.Text = File.ReadAllText(file);
                saveToolButton.PerformClick();
            }
        }

        private void changePrecodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings(3);
        }

        #endregion

        #region Compiler Output

        private bool compilerOutputIsHidden = false;
        private void ToggleCompilerOutput()
        {
            if (compilerOutputIsHidden)
            {
                compilerOutputIsHidden = false;
                showHideOutput.Image = Properties.Resources.minimize;
                compilerSplitContainer1.FixedPanel = FixedPanel.None;
                compilerSplitContainer1.SplitterDistance =
                    (int)Math.Round(Properties.Settings.Default.CompilerSplitterRatio * compilerSplitContainer1.Height);
            }
            else
            {
                compilerOutputIsHidden = true;
                showHideOutput.Image = Properties.Resources.maximize;
                compilerSplitContainer1.FixedPanel = FixedPanel.Panel2;
                compilerSplitContainer1.SplitterDistance =
                    compilerSplitContainer1.Height - compilerSplitContainer1.Panel2MinSize;
            }
        }

        private void showOrHideOutput_Click(object sender, EventArgs e)
        {
            ToggleCompilerOutput();
        }

        private void compilerSplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (!compilerOutputIsHidden)
            {
                Properties.Settings.Default.CompilerSplitterRatio =
                   (double)compilerSplitContainer1.SplitterDistance / compilerSplitContainer1.Height;
            }

            if (compilerSplitContainer1.Panel2.Height > compilerSplitContainer1.Panel2MinSize)
            {
                if (compilerOutputIsHidden)
                {
                    compilerOutputIsHidden = false;
                    showHideOutput.Image = Properties.Resources.minimize;
                    compilerSplitContainer1.FixedPanel = FixedPanel.None;
                }
            }
            else
            {
                if (!compilerOutputIsHidden)
                {
                    compilerOutputIsHidden = true;
                    showHideOutput.Image = Properties.Resources.maximize;
                    compilerSplitContainer1.FixedPanel = FixedPanel.Panel2;
                }
            }
        }

        private void compilerOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            HighlightSyntax.HighlightCompilerOutput(e.ChangedRange);
        }

        private void compilerOutput_Click(object sender, EventArgs e)
        {
            codeTextBox.ClearHints();
            Place cur = compilerOutput.Selection.Start;
            tabControl1.SelectedTab = codeTAB;
            SetCodeError(cur.iLine, true);
        }

        #endregion

        #region Input Output

        private void saveInputOutputData()
        {
            try
            {
                //input
                string path = OpenedInput;
                if (!string.IsNullOrEmpty(path))
                {
                    File.WriteAllText(path, inputTextBox.Text);
                }
                //correct output
                path = OpenedCorrect;
                if (!string.IsNullOrEmpty(path))
                {
                    File.WriteAllText(path, correctOutputTextBox.Text);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        // 
        // Input Text Box
        //
        private void inputTextBox_CustomAction(object sender, CustomActionEventArgs e)
        {
            if (e.Action == FCTBAction.CustomAction1)
            {
                saveInputOutputData();
            }
        }

        private void openInputTool_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenInputFile(ofd.FileName);
                }
                ofd.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void saveInputTool_Click(object sender, EventArgs e)
        {
            try
            {
                string path = OpenedInput;
                if (string.IsNullOrEmpty(path)) return;
                File.WriteAllText(path, inputTextBox.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cutInputTool_Click(object sender, EventArgs e)
        {
            inputTextBox.Cut();
        }

        private void copyInputTool_Click(object sender, EventArgs e)
        {
            inputTextBox.Copy();
        }

        private void pasteInputTool_Click(object sender, EventArgs e)
        {
            inputTextBox.Paste();
        }

        //
        //Load Default 
        // 
        private void loadDefaultInput_Click(object sender, EventArgs e)
        {
            string inp = OpenedInput;
            string correct = OpenedCorrect;
            if (!(File.Exists(inp) && File.Exists(correct))) return;
            if (!CodesBrowser.ParseInputOutput(SelectedPNUM, inp, correct, true))
            {
                MessageBox.Show("Can't load input-output automatically. [Parsing failed]");
            }
            else
            {
                MessageBox.Show("Input/Output loaded. " + Environment.NewLine +
                    "[Warning: It might be wrong. Please check to be sure.]");
            }
        }

        //
        // Output Text Box
        //
        private void saveOutputTool_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "All Files|*.*";
                sfd.FileName = "input.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, outputTextBox.Text);
                }
                sfd.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void copyOutputTool_Click(object sender, EventArgs e)
        {
            outputTextBox.Copy();
        }

        //
        // Correct Output
        //

        private void correctOutputTextBox_CustomAction(object sender, CustomActionEventArgs e)
        {
            if (e.Action == FCTBAction.CustomAction1)
            {
                saveInputOutputData();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            correctOutputTextBox.Cut();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            correctOutputTextBox.Copy();
        }

        private void pasteCorrectToolButton_Click(object sender, EventArgs e)
        {
            correctOutputTextBox.Paste();
        }

        private void openCorrectToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenCorrectFile(ofd.FileName);
                }
                ofd.Dispose();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void saveCorrectToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = OpenedCorrect;
                if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;
                File.WriteAllText(path, correctOutputTextBox.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void refreshCompareButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OpenedInput) && File.Exists(OpenedInput))
            {
                OpenInputFile(OpenedInput);
            }

            if (!string.IsNullOrEmpty(OpenedOutput) && File.Exists(OpenedOutput))
            {
                OpenOutputFile(OpenedOutput);
            }

            if (!string.IsNullOrEmpty(OpenedCorrect) && File.Exists(OpenedCorrect))
            {
                OpenCorrectFile(OpenedCorrect);
            }
        }

        #endregion

        #region Compare Outputs

        private void compareOutputButton_Click(object sender, EventArgs e)
        {
            saveInputOutputData();
            inputOutputTabControl.SelectedTab = correctOutputTab;

            bool res = CompareOutputTexts();
            if (res)
            {
                MessageBox.Show("File matched.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("File did not match.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private int __updating = 0;
        private bool CompareOutputTexts()
        {
            //get file names
            string file2 = OpenedOutput;
            string file1 = OpenedCorrect;
            if (file1 == null || !File.Exists(file1)) return false;
            if (file2 == null || !File.Exists(file2)) return false;

            //load lines from file
            var source1 = DiffMergeStuffs.Lines.Load(file1);
            var source2 = DiffMergeStuffs.Lines.Load(file2);
            source1.Merge(source2);

            //begin update
            __updating++;

            //show lines
            outputTextBox.Clear();
            correctOutputTextBox.Clear();
            bool res = _ProcessDiff(source1, correctOutputTextBox, outputTextBox);

            //end update
            __updating--;

            return res;
        }

        private bool _ProcessDiff(DiffMergeStuffs.Lines lines, FastColoredTextBox fctb1, FastColoredTextBox fctb2)
        {
            bool match = true;
            foreach (var line in lines)
            {
                switch (line.state)
                {
                    case DiffMergeStuffs.DiffType.None:
                        fctb1.AppendText(line.line + Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine);
                        break;
                    case DiffMergeStuffs.DiffType.Deleted:
                        fctb1.AppendText(line.line + Environment.NewLine, HighlightSyntax.RedLineStyle);
                        fctb2.AppendText(Environment.NewLine);
                        match = false;
                        break;
                    case DiffMergeStuffs.DiffType.Inserted:
                        fctb1.AppendText(Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine, HighlightSyntax.GreenLineStyle);
                        match = false;
                        break;
                }
                if (line.subLines != null)
                {
                    bool res = _ProcessDiff(line.subLines, fctb1, fctb2);
                    match = match && res;
                }
            }
            return match;
        }

        private void tb_VisibleRangeChanged(object sender, EventArgs e)
        {
            if (__updating > 0) return;

            var fctb = (FastColoredTextBox)sender;
            var vPos = fctb.VerticalScroll.Value;
            var curLine = fctb.Selection.Start.iLine;
            var curChar = fctb.Selection.Start.iChar;

            CurLnLabel.Text = string.Format((string)CurLnLabel.Tag, curLine);
            CurColLabel.Text = string.Format((string)CurColLabel.Tag, curChar);

            if (sender == outputTextBox)
                _UpdateScroll(correctOutputTextBox, vPos, curLine);
            else
                _UpdateScroll(outputTextBox, vPos, curLine);

            outputTextBox.Refresh();
            correctOutputTextBox.Refresh();
        }

        private void _UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
        {
            if (__updating > 0) return;

            //Begin Update
            __updating++;
            //
            if (vPos <= tb.VerticalScroll.Maximum)
            {
                tb.VerticalScroll.Value = vPos;
                tb.UpdateScrollbars();
            }

            if (curLine < tb.LinesCount)
                tb.Selection = new Range(tb, 0, curLine, 0, curLine);
            //End update
            __updating--;
        }

        #endregion

        #region uDebug

        uDebugClient.FormData mFormData;

        public void LoadUDebug()
        {
            if (SelectedPNUM <= 0)
            {
                return;
            }

            System.Threading.ThreadPool.QueueUserWorkItem((WaitCallback)delegate
            {
                // get data
                try
                {
                    mFormData = uDebugClient.ExtractInputUsers(SelectedPNUM);
                }
                catch (Exception ex)
                {
                    Logger.Add(ex.Message, ex.Source);
                    return;
                }
                // update UI
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (mFormData != null && mFormData.inputs.Count > 0)
                    {
                        uDebugUser.Items.Clear();
                        uDebugUser.Items.AddRange(mFormData.inputs.ToArray());
                        uDebugPane1.Visible = true;
                    }
                    uDebugPane2.Visible = (mFormData != null);
                });
            });
        }

        public void DownloadInputOutput(uDebugClient.UserInput user = null)
        {
            if (mFormData == null)
            {
                MessageBox.Show("Not Avaiable");
            }


            System.Threading.ThreadPool.QueueUserWorkItem((WaitCallback)delegate
            {
                try
                {
                    // download input
                    string input = user == null ? inputTextBox.Text : uDebugClient.GetInputdata(user);

                    // download output
                    string output = uDebugClient.GetOutputData(input, mFormData);

                    // update UI and save input-output data
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        inputTextBox.Text = input;
                        correctOutputTextBox.Text = output;
                        saveInputOutputData();
                    });
                }
                catch (Exception ex)
                {
                    Logger.Add(ex.Message, ex.Source);
                }
                finally
                {
                    // reenable input output
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        inputTextBox.Enabled = true;
                        correctOutputTextBox.Enabled = true;
                    });
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uDebugPane1.Visible = false;
            LoadUDebug();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            correctOutputTextBox.Enabled = false;
            DownloadInputOutput();
        }

        private void uDebugUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputTextBox.Enabled = false;
            correctOutputTextBox.Enabled = false;
            DownloadInputOutput((uDebugClient.UserInput)uDebugUser.SelectedItem);
        }

        #endregion

        private void expandCollapseButton_Click(object sender, EventArgs e)
        {
            if (SelectedPNUM <= 0)
            {
                return;
            }
            if (showProblemButton.Tag == null)
            {
                problemToolButton.PerformClick();
                mainContainer.Panel1.Controls.Add(Interactivity.problemViewer.pdfViewer1);
                Interactivity.problemViewer.pdfViewer1.BringToFront();
                mainContainer.SplitterDistance = mainContainer.Width * 9 / 19;
                Interactivity.mainForm.Controls.Add(this);
                this.BringToFront();
                showProblemButton.Tag = true;
            }
            else
            {
                Interactivity.problemViewer.pdfTab.Controls.Add(Interactivity.problemViewer.pdfViewer1);
                mainContainer.SplitterDistance = mainContainer.Width / 4;
                Interactivity.mainForm.codesTab.Controls.Add(this);
                showProblemButton.Tag = null;
            }

        }


    }
}

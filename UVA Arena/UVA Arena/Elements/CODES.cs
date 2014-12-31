using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace UVA_Arena.Elements
{
    public partial class CODES : UserControl
    {
        #region Loader Functions

        public CODES()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            folderTreeView.PathSeparator = Path.DirectorySeparatorChar.ToString();

            string path = CodesPath;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                fileSystemWatcher1.Path = path;
                fileSystemWatcher1.EnableRaisingEvents = true;
                try { LoadCodeFolder(true); }
                catch (Exception ex) { Logger.Add(ex.Message, "Codes"); }
            }
        }

        public void ShowCode(object pnum)
        {
            if (IsReady)
            {
                //create code file if doesn't exist
                string path = LocalDirectory.GetCodesPath((long)pnum);
                if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        CodeFileCreator cfc = new CodeFileCreator();
                        if (cfc.ShowDialog() == DialogResult.OK)
                        {
                            AddProblem((long)pnum, cfc.Language);
                            return;
                        }
                    });
                }

                //select code file path
                TreeNode tn = GetNode(new DirectoryInfo(path));
                ExpandAndSelect(tn, ExpandSelectType.SelecFirstChild);
            }
            else
            {
                TaskQueue.AddTask(ShowCode, pnum, 1000);
            }
        }

        #endregion

        #region Registry Entry

        /// <summary> default user name </summary>
        public static string CodesPath
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("Codes Path", null);
                if (dat != null && Directory.Exists(dat)) return dat;
                return null;
            }
            set
            {
                RegistryAccess.SetValue("Codes Path", value);
            }
        }

        /// <summary> MinGW Path </summary>
        public static string MinGWLocation
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("MinGW Compiler", null);
                if (dat != null && Directory.Exists(dat)) return dat;

                string path = @"C:\MinGW";
                string gcc = Path.Combine(path, @"bin\mingw32-gcc.exe");
                string gpp = Path.Combine(path, @"bin\mingw32-g++.exe");
                if (!(File.Exists(gcc) && File.Exists(gpp))) return "";

                RegistryAccess.SetValue("MinGW Compiler", path);
                return path;
            }
            set
            {
                RegistryAccess.SetValue("MinGW Compiler", value);
            }
        }

        /// <summary> JDK Path </summary>
        public static string JDKLocation
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("JDK Compiler", null);
                if (dat != null && Directory.Exists(dat)) return dat;

                string key = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\TypedPaths";
                string path = (string)Microsoft.Win32.Registry.GetValue(key, "url1", "");
                if (path == null) return "";
                string javac = Path.Combine(path, @"javac.exe");
                string java = Path.Combine(path, @"java.exe");
                if (!(File.Exists(javac) && File.Exists(java))) return "";

                path = Path.GetDirectoryName(path);
                RegistryAccess.SetValue("JDK Compiler", path);
                return path;
            }
            set
            {
                RegistryAccess.SetValue("JDK Compiler", value);
            }
        }

        /// <summary> C Compiler Options </summary>
        public static string CCompilerOptions
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("C Compiler Options", null);
                if (string.IsNullOrEmpty(dat)) return "-ansi -std=c89 -Wall";
                return dat;
            }
            set
            {
                RegistryAccess.SetValue("C Compiler Options", value);
            }
        }
        /// <summary> C++ Compiler Options </summary>
        public static string CPPCompilerOptions
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("C++ Compiler Options", null);
                if (string.IsNullOrEmpty(dat)) return "";
                return dat;
            }
            set
            {
                RegistryAccess.SetValue("C++ Compiler Options", value);
            }
        }
        /// <summary> Java Compiler Options </summary>
        public static string JavaCompilerOptions
        {
            get
            {
                string dat = (string)RegistryAccess.GetValue("Java Compiler Options", null);
                if (string.IsNullOrEmpty(dat)) return "-g";
                return dat;
            }
            set
            {
                RegistryAccess.SetValue("Java Compiler Options", value);
            }
        }

        /// <summary> Show Hints </summary>
        public static bool ShowHints
        {
            get
            {
                object dat = RegistryAccess.GetValue("Show Hints", null);
                if (dat == null || dat.GetType() != typeof(int)) return false;
                return ((int)dat == 1);
            }
            set
            {
                RegistryAccess.SetValue("Show Hints", (value ? 1 : 0), null, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

        #endregion

        #region Load Folder Tree

        public bool IsReady = false;

        public void LoadCodeFolder(object background)
        {
            IsReady = false;

            //get code path
            string path = CodesPath;
            if (path == null || !Directory.Exists(path)) return;

            if ((bool)background)
            {
                selectDirectoryPanel.Visible = false;
                folderTreeView.UseWaitCursor = true;
                System.Threading.ThreadPool.QueueUserWorkItem(LoadCodeFolder, false);
                return;
            }

            //recursively add all folders and files
            System.Threading.Thread.Sleep(20);
            List<TreeNode> parent = new List<TreeNode>();
            foreach (string folder in Directory.GetDirectories(path))
            {
                TreeNode nod = AddTreeNode(new DirectoryInfo(folder));
                AddChildNodes(nod);
                parent.Add(nod);
            }
            foreach (string folder in Directory.GetFiles(path))
            {
                parent.Add(AddTreeNode(new FileInfo(folder)));
            }

            //add codes
            if (folderTreeView == null || folderTreeView.IsDisposed) return;
            folderTreeView.BeginInvoke((MethodInvoker)delegate
            {
                FileSystemInfo last = null;
                if (folderTreeView.SelectedNode != null)
                {
                    last = (FileSystemInfo)folderTreeView.SelectedNode.Tag;
                }

                folderTreeView.Sort();
                folderTreeView.Nodes.Clear();
                folderTreeView.Nodes.AddRange(parent.ToArray());

                folderTreeView.UseWaitCursor = false;
                selectDirectoryPanel.Visible = false;

                if (last != null) ExpandAndSelect(GetNode(last));
                else
                {
                    int cnt = folderTreeView.Nodes.Count;
                    if (cnt > 0) folderTreeView.Nodes[0].Expand();
                }
            });

            IsReady = true;
            System.GC.Collect();
        }

        public void AddChildNodes(TreeNode parent)
        {
            DirectoryInfo dir = (DirectoryInfo)parent.Tag;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                AddChildNodes(AddTreeNode(d, parent));
            }
            foreach (FileInfo f in dir.GetFiles())
            {
                AddTreeNode(f, parent);
            }
        }

        private string GetKey(FileSystemInfo info)
        {
            string key = info.Name.ToLower() + ".png";
            try { if (imageList1.Images.ContainsKey(key)) return key; }
            catch { }

            key = info.Extension.ToLower() + ".png";
            try { if (imageList1.Images.ContainsKey(key)) return key; }
            catch { }

            if (info.GetType() == typeof(FileInfo)) return "file.png";

            string name = info.FullName.Substring(CodesPath.Length + 1);
            if (!name.StartsWith("Volume"))
            {
                if (name.Contains(Path.DirectorySeparatorChar.ToString())) return key = "folder.png";
                return "root.png";
            }

            if (name.Contains(Path.DirectorySeparatorChar.ToString())) return "problem.png";
            return "volume.png";
        }

        private TreeNode AddTreeNode(FileSystemInfo info, TreeNode parent = null)
        {
            if (info == null) return null;
            TreeNode tn = new TreeNode();
            tn.Name = info.Name;
            tn.Text = info.Name;
            tn.Tag = info;
            tn.ImageKey = GetKey(info);
            tn.SelectedImageKey = tn.ImageKey;
            if (parent != null) parent.Nodes.Add(tn);
            return tn;
        }


        #endregion

        #region Code Path Selector

        public void FormatCodeDirectory(object background)
        {
            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(FormatCodeDirectory, false);
                return;
            }

            //gather all files
            string path = CodesPath;
            if (!Directory.Exists(path)) return;

            //create codespath and check them
            if (!LocalDatabase.IsReady)
            {
                Logger.Add("Problem Database is not ready yet.", "Codes : FormatCodeDirectory()");
                return;
            }

            //just call codesPath. it will create directory automatically
            foreach (Structures.ProblemInfo prob in LocalDatabase.problem_list)
            {
                LocalDirectory.GetCodesPath(prob.pnum);
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder that stores code files.";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                CodesPath = fbd.SelectedPath;
                try
                {
                    FormatCodeDirectory(true);
                    LoadCodeFolder(true);
                    fileSystemWatcher1.Path = fbd.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    CodesPath = null;
                    selectDirectoryPanel.Visible = true;
                }
            }
        }

        private void cancelBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CodesPath) || !Directory.Exists(CodesPath))
                {
                    CodesPath = LocalDirectory.DefaultCodesPath();
                    FormatCodeDirectory(true);
                }
                LoadCodeFolder(true);
                fileSystemWatcher1.Path = CodesPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                CodesPath = null;
                selectDirectoryPanel.Visible = true;
            }
        }

        #endregion

        #region Seach Text Box

        private TreeNode SelectNextNode(TreeNode node)
        {
            bool next = true;
            if (node.Nodes.Count > 0)
            {
                return node.Nodes[0];
            }
            while (next)
            {
                if (node == null)
                {
                    break;
                }
                else if (node.NextNode == null)
                {
                    node = node.Parent;
                }
                else
                {
                    next = false;
                    node = node.NextNode;
                }
            }
            return node;
        }

        public bool SearchProblem(string pat, long pnum = -1)
        {
            try
            {
                //select starting node
                TreeNode node = folderTreeView.SelectedNode;
                if (node == null) node = folderTreeView.Nodes[0];
                else node = SelectNextNode(node);

                //search for a match
                string src = string.Format(@"(^({0}))|(({0})$)|(\b({0}))|(({0})\b)", pat.ToLower());
                while (node != null)
                {
                    //check match
                    string text = node.Text.ToLower();
                    if (Regex.IsMatch(text, src))
                    {
                        ExpandAndSelect(node);
                        return true;
                    }

                    //select next
                    node = SelectNextNode(node);
                }

                //not match
                folderTreeView.SelectedNode = null;
                return false;
            }
            catch { return false; }
        }

        private void searchBox1_SearchButtonClicked(object sender, EventArgs e)
        {
            bool match = SearchProblem(searchBox1.SearchText);
            if (!match) MessageBox.Show("No match found. Reached end of the tree.");
        }

        private void searchBox1_ClearButtonClicked(object sender, EventArgs e)
        {
            SearchProblem("");
        }

        #endregion

        #region Folder Tree List

        private void folderTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn != null && tn.Tag.GetType() == typeof(FileInfo))
                    System.Diagnostics.Process.Start(((FileInfo)tn.Tag).FullName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void folderTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift && !e.Control)
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null || !(e.Alt || CurrentProblem != tn.Tag)) return;
                if (tn.Tag.GetType() == typeof(FileInfo))
                {
                    try { System.Diagnostics.Process.Start(((FileInfo)tn.Tag).FullName); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (folderTreeView.SelectedNode == null) OpenFile(null);
            if (folderTreeView.SelectedNode.Tag.GetType() == typeof(FileInfo))
            {
                OpenFile((FileInfo)folderTreeView.SelectedNode.Tag);
            }
            else OpenFile(null);
        }

        private void folderTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Clicks != 1) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                folderTreeView.SelectedNode = e.Node;
            }
        }

        #endregion

        #region Useful functions for Folder Tree

        public TreeNode GetNode(FileSystemInfo finfo)
        {
            try
            {
                string path = finfo.FullName.Substring(CodesPath.Length + 1);
                string[] comp = path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
                TreeNode nod = folderTreeView.Nodes[comp[0]];
                for (int i = 1; i < comp.Length; ++i)
                {
                    nod = nod.Nodes[comp[i]];
                }
                return nod;
            }
            catch { return null; }
        }

        public void ExpandToNode(TreeNode node)
        {
            if (node == null) return;
            TreeNode par = node.Parent;
            while (par != null)
            {
                par.Expand();
                par = par.Parent;
            }
            node.EnsureVisible();
        }

        public enum ExpandSelectType
        {
            ExpandToNode,
            ExpandWithNode,
            SelecFirstChild
        }

        public void ExpandAndSelect(TreeNode node, ExpandSelectType type = ExpandSelectType.ExpandToNode)
        {
            if (node == null) return;

            ExpandToNode(node);
            folderTreeView.SelectedNode = node;

            if (type != ExpandSelectType.ExpandToNode) node.Expand();
            if (type == ExpandSelectType.SelecFirstChild)
            {
                if (node.Nodes.Count > 0)
                    folderTreeView.SelectedNode = node.Nodes[0];
            }
        }

        public TreeNode LocateProblem(long pnum)
        {
            string path = LocalDirectory.GetCodesPath(pnum);
            return GetNode(new DirectoryInfo(path));
        }

        public void AddProblem(long pnum, Structures.Language lang = Structures.Language.CPP)
        {
            //get code path
            string path = LocalDirectory.GetCodesPath(pnum);
            if (string.IsNullOrEmpty(path)) return;

            //get file extenstion
            string ext = ".cpp";
            if (lang == Structures.Language.C) ext = ".c";
            else if (lang == Structures.Language.Java) ext = ".java";
            else if (lang == Structures.Language.Pascal) ext = ".pascal";

            //create code file
            string name = Path.GetFileName(path);
            CreateFile(path, name, ext);

            //create input-output
            string input = Path.Combine(path, "input.txt");
            string output = Path.Combine(path, "output.txt");
            string correct = Path.Combine(path, "correct.txt");
            LocalDirectory.CreateFile(input);
            LocalDirectory.CreateFile(output);
            LocalDirectory.CreateFile(correct);
            ParseInputOutput(pnum, input, correct);

            //select created problem
            this.BeginInvoke((MethodInvoker)delegate
            {
                ExpandAndSelect(LocateProblem(pnum), ExpandSelectType.SelecFirstChild);
            });
        }

        private void ParseInputOutput(long pnum, string inpfile, string outfile)
        {
            try
            {
                string file = LocalDirectory.GetProblemHtml(pnum);
                if (LocalDirectory.GetFileSize(file) < 100) return;

                //get input
                string html = File.ReadAllText(file);
                string low = html.ToLower();
                int indx = low.IndexOf("sample input");
                if (indx < 0) return;
                int start = low.IndexOf("<pre>", indx);
                if (start < 0) return;
                int stop = low.IndexOf("</pre>", start);
                if (stop <= start) return;

                string xml = html.Substring(start, stop - start + 6);
                System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
                xdoc.LoadXml(xml);
                string data = xdoc.DocumentElement.InnerText.TrimStart(new char[] { ' ', '\r', '\n' });
                File.WriteAllText(inpfile, data);

                //get output
                indx = low.IndexOf("sample output", stop);
                if (indx < 0) return;
                start = low.IndexOf("<pre>", indx);
                if (start < 0) return;
                stop = low.IndexOf("</pre>", start);
                if (stop <= start) return;

                xml = html.Substring(start, stop - start + 6);
                xdoc.LoadXml(xml);
                data = xdoc.DocumentElement.InnerText.TrimStart(new char[] { ' ', '\r', '\n' });
                if (!data.EndsWith("\n")) data += "\n";
                File.WriteAllText(outfile, data);
            }
            catch (Exception ex)
            {
                Logger.Add("Failed to write Input/Output data. Error: " + ex.Message, "CODES| ParseInputOutput()");
            }
        }

        private long GetProblemNumber(string name)
        {
            long res = -1;
            Match m = Regex.Match(name, @"\d+");
            if (m.Success)
            {
                string num = name.Substring(m.Index, m.Length);
                long.TryParse(num, out res);
                if (!LocalDatabase.HasProblem(res)) res = -1;
            }
            return res;
        }

        private string GetSelectedPath()
        {
            TreeNode tn = folderTreeView.SelectedNode;
            if (tn == null) return null;

            string dir = ((FileSystemInfo)tn.Tag).FullName;
            if (tn.Tag.GetType() == typeof(FileInfo))
                dir = ((FileInfo)tn.Tag).DirectoryName;

            return dir;
        }

        private void CreateFile(string par, string name, string ext, bool trial = true)
        {
            if (par == null) return;

            int tcount = 1;
            string path = Path.Combine(par, name + ext);
            while (trial && File.Exists(path))
            {
                path = Path.Combine(par, string.Format("{0} ({1}){2}", name, tcount, ext));
                ++tcount;
            }
            LocalDirectory.CreateFile(path);
        }

        private void CreateDirectory(string par, string name, bool trial = true)
        {
            if (par == null) par = CodesPath;

            int tcount = 1;
            string path = Path.Combine(par, name);
            while (trial && Directory.Exists(path))
            {
                path = Path.Combine(par, string.Format("{0} ({1})", name, tcount));
                ++tcount;
            }
            LocalDirectory.CreateDirectory(path);
        }

        #endregion

        #region Folder Tree Context Menu Items

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderTreeView.CollapseAll();
        }

        private void formatDir_Click(object sender, EventArgs e)
        {
            FormatCodeDirectory(true);
        }

        private void openExternallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null) return;
                string path = ((FileSystemInfo)tn.Tag).FullName;
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshTool_Click(object sender, EventArgs e)
        {
            LoadCodeFolder(true);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tn = folderTreeView.SelectedNode;
            if (tn == null) return;
            string path = ((FileSystemInfo)tn.Tag).FullName;
            StringCollection clip = new StringCollection();
            clip.Add(path);
            Clipboard.SetFileDropList(clip);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            List<string> from = new List<string>();
            StringCollection strcol = Clipboard.GetFileDropList();
            foreach (string file in strcol)
            {
                if (Directory.Exists(file)) from.Add(file);
                else if (File.Exists(file)) from.Add(file);
            }
            LocalDirectory.CopyFilesOrFolders(from.ToArray(), path);
            Clipboard.Clear();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null) return;
                string path = ((FileSystemInfo)tn.Tag).FullName;
                LocalDirectory.DeleteFilesOrFolders(new string[] { path });
                tn.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null) return;
                folderTreeView.LabelEdit = true;
                tn.BeginEdit();
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Codes");
            }
        }

        private void folderTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                folderTreeView.LabelEdit = false;
                FileSystemInfo fsi = (FileSystemInfo)e.Node.Tag;
                string directory = Path.GetDirectoryName(fsi.FullName);
                string newpath = Path.Combine(directory, e.Label);
                string ext = Path.GetExtension(newpath);
                if (ext.ToLower() != fsi.Extension.ToLower())
                {
                    if (MessageBox.Show("Change extension from " + fsi.Extension + " to " + ext + "?",
                            "Renaming File", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        e.CancelEdit = true;
                        return;
                    }
                }

                File.Move(fsi.FullName, newpath);
                e.Node.Name = e.Label;
                if (e.Node.Tag.GetType() == typeof(FileInfo))
                    e.Node.Tag = new FileInfo(newpath);
                else e.Node.Tag = new DirectoryInfo(newpath);
            }
            catch (Exception ex)
            {
                e.CancelEdit = true;
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region New Context Menu Item

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            CreateDirectory(dir, "New Folder");
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            CreateFile(dir, "New Text File", ".txt");
        }

        private void cFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".c");
            else AddProblem(pnum, Structures.Language.C);
        }

        private void cPPFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".cpp");
            else AddProblem(pnum, Structures.Language.CPP);
        }

        private void javaFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".java");
            else AddProblem(pnum, Structures.Language.Java);
        }

        private void pascalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".pascal");
            else AddProblem(pnum, Structures.Language.Pascal);
        }

        private void inputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            CreateFile(dir, "input", ".txt", false);
        }

        private void outputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            CreateFile(dir, "output", ".txt", false);
        }

        #endregion

        #region Open File And History Keeping

        public long SelectedPNUM = -1;
        private Structures.Language CustomLang = Structures.Language.CPP;
        private FileInfo CurrentProblem = null;

        private void ClearPrevOpenedFiles()
        {
            SelectedPNUM = -1;
            CurrentProblem = null;
            fileNameLabel.Text = "No Opened File";
            CustomLang = Structures.Language.Other;

            compilerOutput.Clear();
            runtestToolButton.Enabled = false;
            tabControl1.SelectedTab = codeTAB;

            codeTextBox.Clear();
            codeTextBox.ReadOnly = true;

            inputTextBox.Tag = null;
            inputTextBox.ReadOnly = true;
            inputTextBox.Clear();

            outputTextBox.Tag = null;
            outputTextBox.Clear();

            correctOutputTextBox.Clear();
            correctOutputTextBox.Tag = null;
            correctOutputTextBox.ReadOnly = true;
        }

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
            CurrentProblem = file; //set current file info
            fileNameLabel.Text = file.Name;

            //load highlighters
            SetLanguage(file.Extension);
            HighlightCodebox(codeTextBox.Range); //highlight immediately after loading language

            //select and expand to opened node if not already selected
            ExpandAndSelect(GetNode(file));

            //set problem number
            if (CanHaveIOFiles(codeTextBox.Language))
            {
                SelectedPNUM = GetProblemNumber(file.Name);
            }

            //open input-output for valid problem number
            if (SelectedPNUM == -1) return;

            //make input output files
            MakeInputOutput(file.DirectoryName);
            //as there are IO files we can run tests
            runtestToolButton.Enabled = true;
            //change file name label text to include problem numbre and title
            fileNameLabel.Text = string.Format("Problem {0} - {1} ({2})",
                  SelectedPNUM, LocalDatabase.GetTitle(SelectedPNUM), fileNameLabel.Text);
        }

        private bool PrecheckOpenFile(FileInfo file, bool history = true)
        {
            //check if already opened 
            if (CurrentProblem == file) return false;

            //add to history
            if (history && CurrentProblem != null)
            {
                AddToPrev();
                nextToolMenu.Enabled = false;
                nextToolMenu.DropDownItems.Clear();
            }

            //clear prev values
            ClearPrevOpenedFiles();

            //check file validity
            if (file == null) return false;

            //check extension validity
            Regex invalid = new Regex(@".(exe|dll|o|class)");
            return (!invalid.IsMatch(file.Extension.ToLower()));
        }

        private void SetLanguage(string ext)
        {
            ext = ext.ToLower();
            if (ext == ".cs") codeTextBox.Language = Language.CSharp;
            else if (ext == ".vb") codeTextBox.Language = Language.VB;
            else if (ext == ".php") codeTextBox.Language = Language.PHP;
            else if (ext == ".sql") codeTextBox.Language = Language.SQL;
            else if (ext == ".lua") codeTextBox.Language = Language.Lua;
            else if (ext == ".js") codeTextBox.Language = Language.JS;
            else if (ext.StartsWith(".xml")) codeTextBox.Language = Language.XML;
            else if (ext.StartsWith(".htm")) codeTextBox.Language = Language.HTML;
            else
            {
                codeTextBox.Language = Language.Custom;
                if (ext == ".c") CustomLang = Structures.Language.C;
                else if (ext == ".java") CustomLang = Structures.Language.Java;
                else if (ext == ".cpp" || ext == ".h") CustomLang = Structures.Language.CPP;
                else if (ext == ".pascal") CustomLang = Structures.Language.Pascal;
                else CustomLang = Structures.Language.Other;
            }
        }

        private bool CanHaveIOFiles(Language lang)
        {
            //detemine whether input-output file need to be created.
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
                inputTextBox.Tag = input;
                OpenInputFile(input);

                //open output file
                string output = Path.Combine(dir, "output.txt");
                LocalDirectory.CreateFile(output);
                outputTextBox.Tag = output;
                OpenOutputFile(output);

                //open correct output file
                string correct = Path.Combine(dir, "correct.txt");
                LocalDirectory.CreateFile(correct);
                correctOutputTextBox.ReadOnly = false;
                correctOutputTextBox.Tag = correct;
                OpenCorrectFile(correct);
            }
            catch { }
        }


        //
        // History Keeping
        //
        private ToolStripItem GetToolItem(FileInfo file)
        {
            ToolStripMenuItem tmi = new ToolStripMenuItem();
            tmi.Text = file.Name;
            tmi.Tag = file;
            tmi.ToolTipText = file.FullName;
            return tmi;
        }

        private void AddToPrev()
        {
            if (CurrentProblem == null) return;
            ToolStripItem tmi = GetToolItem(CurrentProblem);
            tmi.Click += prevItemClick;
            prevToolMenu.DropDownItems.Insert(0, tmi);
            prevToolMenu.Enabled = true;
        }

        private void AddToNext()
        {
            if (CurrentProblem == null) return;
            ToolStripItem tmi = GetToolItem(CurrentProblem);
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
            //get range from linenumber
            int len = compilerOutput.GetLineLength(line);
            Place start = new Place(0, line); //start of the line
            Place stop = new Place(len, line); //end of the line
            Range range = compilerOutput.GetRange(start, stop);

            //process if valid language is selectd
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
            //show current line and coloumn position
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
                // <-- need some extra work for indivisuals
                HighlightSyntax.AutoIndent(sender, e);
            }
        }

        #endregion

        #region Code Toolbar and Context Menu

        private void saveToolButton_Click(object sender, EventArgs e)
        {
            if (CurrentProblem == null) return;
            codeTextBox.SaveToFile(CurrentProblem.FullName, Encoding.Default);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeTextBox.Cut();
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


        private void pasteToolButton_Click(object sender, EventArgs e)
        {
            codeTextBox.Paste();
        }

        private void findToolButton_Click(object sender, EventArgs e)
        {
            codeTextBox.ShowFindDialog(codeTextBox.SelectedText);
        }

        private void reloadToolButton_Click(object sender, EventArgs e)
        {
            if (CurrentProblem == null) return;
            codeTextBox.Clear();
            OpenCodeFile(CurrentProblem.FullName);
        }

        private void formatAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codeTextBox.DoAutoIndent();
        }


        private void exploreToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentProblem == null) return;
                string file = CurrentProblem.FullName;
                string folder = Path.GetDirectoryName(file);
                System.Diagnostics.Process.Start(folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentProblem == null) return;
                string file = CurrentProblem.FullName;
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
                if (SelectedPNUM == -1) return;
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
                if (SelectedPNUM == -1 || CurrentProblem == null) return;
                string code = File.ReadAllText(CurrentProblem.FullName);
                Interactivity.SubmitCode(SelectedPNUM, code, CustomLang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void settingsToolButton_Click(object sender, EventArgs e)
        {
            Interactivity.ShowSettings(1);
        }

        #endregion

        #region File Open Functions

        private long MaxFileSIZ = 10 * 1024 * 1024;

        private void _ShowMaxExceedMessage(FastColoredTextBox fctb)
        {
            fctb.Text = "Error : Too large to open. " +
                    string.Format("( > {0})", Functions.FormatMemory(MaxFileSIZ));
        }

        //
        // File Open
        //
        private void OpenCodeFile(string file)
        {
            FileInfo fi = new FileInfo(file);
            //do not open any file larger than 10MB
            if (fi.Length > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(codeTextBox);
            }
            else
            {
                codeTextBox.Text = File.ReadAllText(file);
                if (file.CompareTo(codeTextBox.Tag) != 0)
                    codeTextBox.ClearUndo();
                codeTextBox.Tag = file;
            }
        }

        private void OpenInputFile(string file)
        {
            inputTextBox.Clear();
            FileInfo fi = new FileInfo(file);
            //do not open any file larger than 10MB
            if (fi.Length > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(inputTextBox);
            }
            else
            {
                inputTextBox.Text = File.ReadAllText(file);
            }
        }

        private void OpenOutputFile(string file)
        {
            outputTextBox.Clear();
            FileInfo fi = new FileInfo(file);
            //do not open any file larger than 10MB
            if (fi.Length > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(outputTextBox);
            }
            else
            {
                outputTextBox.Text = File.ReadAllText(file);
            }
            progOutputTextBox.Text = outputTextBox.Text;
        }

        private void ClearOutputFile()
        {
            outputTextBox.Clear();
            string file = (string)outputTextBox.Tag;
            if (string.IsNullOrEmpty(file) || !File.Exists(file)) return;
            File.WriteAllText(file, "");
            OpenOutputFile(file);
        }

        private void OpenCorrectFile(string file)
        {
            correctOutputTextBox.Clear();
            FileInfo fi = new FileInfo(file);
            //do not open any file larger than 10MB
            if (fi.Length > MaxFileSIZ)
            {
                _ShowMaxExceedMessage(correctOutputTextBox);
            }
            else
            {
                correctOutputTextBox.Text = File.ReadAllText(file);
            }
        }

        #endregion

        #region Input Output

        // 
        // Input Text Box
        //
        private void inputTextBox_CustomAction(object sender, CustomActionEventArgs e)
        {
            if (e.Action == FCTBAction.CustomAction1)
            {
                saveInputTool.PerformClick();
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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void saveInputTool_Click(object sender, EventArgs e)
        {
            try
            {
                string path = (string)inputTextBox.Tag;
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
                saveCorrectToolButton.PerformClick();
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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void saveCorrectToolButton_Click(object sender, EventArgs e)
        {
            try
            {
                string path = (string)correctOutputTextBox.Tag;
                if (string.IsNullOrEmpty(path)) return;
                File.WriteAllText(path, correctOutputTextBox.Text);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void refreshCompareButton_Click(object sender, EventArgs e)
        {
            string path = (string)correctOutputTextBox.Tag;
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                OpenCorrectFile(path);
            }

            string file = (string)outputTextBox.Tag;
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                OpenOutputFile(file);
            }
        }

        #endregion

        #region FileSystemWatcher

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (!IsReady) return;

            try
            {
                string path = Path.GetDirectoryName(e.FullPath);

                TreeNode tn = null;
                if (File.Exists(e.FullPath))
                    tn = AddTreeNode(new FileInfo(e.FullPath));
                else
                    tn = AddTreeNode(new DirectoryInfo(e.FullPath));

                if (path == CodesPath)
                {
                    folderTreeView.Nodes.Add(tn);
                }
                else
                {
                    TreeNode par = GetNode(new DirectoryInfo(path));
                    if (par != null) par.Nodes.Add(tn);
                }
            }
            catch { }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            if (!IsReady) return;

            try
            {
                if (CodesPath == e.FullPath)
                {
                    folderTreeView.Nodes.Clear();
                    selectDirectoryPanel.Visible = true;
                    return;
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(e.FullPath);
                    TreeNode tn = GetNode(dir);
                    if (tn != null) tn.Remove();
                    if (CurrentProblem == null) return;
                    if (CurrentProblem.FullName == e.FullPath) OpenFile(null);
                }
            }
            catch { }
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            if (!IsReady) return;

            try
            {
                if (CodesPath == e.FullPath)
                {
                    CodesPath = e.FullPath;
                }
                else
                {
                    FileSystemInfo dir = null;
                    if (File.Exists(e.FullPath))
                        dir = new FileInfo(e.OldFullPath);
                    else
                        dir = new DirectoryInfo(e.OldFullPath);

                    TreeNode tn = GetNode(dir);
                    if (tn != null)
                    {
                        FileInfo f = new FileInfo(e.FullPath);
                        tn.Name = f.Name;
                        tn.Text = f.Name;
                        tn.Tag = f;
                    }
                    if (CurrentProblem != null &&
                        CurrentProblem.FullName == e.OldFullPath)
                    {
                        OpenFile(new FileInfo(e.FullPath));
                    }
                }
            }
            catch { }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (CurrentProblem == null) return;
                if (CurrentProblem.FullName == e.FullPath)
                {
                    OpenCodeFile(e.FullPath);
                }
                else if ((string)inputTextBox.Tag == e.FullPath)
                {
                    OpenInputFile(e.FullPath);
                }
                else if ((string)outputTextBox.Tag == e.FullPath)
                {
                    OpenOutputFile(e.FullPath);
                }
            }
            catch { }
        }

        #endregion

        #region Compilation Tool Bar

        //
        // Compile and Run
        //
        private double RunTimeLimit = 3.000;
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
            if (!LocalDatabase.HasProblem(SelectedPNUM))
            {
                MessageBox.Show("Create a code file first.");
                return;
            }

            switch (CustomLang)
            {
                case Structures.Language.C:
                    codeTextBox.Text = RegistryAccess.CPrecode;
                    break;
                case Structures.Language.CPP:
                    codeTextBox.Text = RegistryAccess.CPPPrecode;
                    break;
                case Structures.Language.Java:
                    codeTextBox.Text = RegistryAccess.JavaPrecode;
                    break;
                default:
                    codeTextBox.Text = "";
                    break;
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
                showHideOutput.Image = Properties.Resources.hide;
                compilerSplitContainer1.FixedPanel = FixedPanel.None;
                compilerSplitContainer1.SplitterDistance = 4 * compilerSplitContainer1.Height / 5;
            }
            else
            {
                compilerOutputIsHidden = true;
                showHideOutput.Image = Properties.Resources.show;
                compilerSplitContainer1.FixedPanel = FixedPanel.Panel2;
                compilerSplitContainer1.SplitterDistance = compilerSplitContainer1.Height - compilerSplitContainer1.Panel2MinSize;
            }
        }

        private void showOrHideOutput_Click(object sender, EventArgs e)
        {
            ToggleCompilerOutput();
        }

        private void compilerSplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (compilerSplitContainer1.Panel2.Height > compilerSplitContainer1.Panel2MinSize)
            {
                if (compilerOutputIsHidden)
                {
                    compilerOutputIsHidden = false;
                    showHideOutput.Image = Properties.Resources.hide;
                    compilerSplitContainer1.FixedPanel = FixedPanel.None;
                }
            }
            else
            {
                if (!compilerOutputIsHidden)
                {
                    compilerOutputIsHidden = true;
                    showHideOutput.Image = Properties.Resources.show;
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

        #region Compile and Run

        public enum BuildRunType
        {
            BuildOnly,
            RunTest,
            BuildAndRun
        }

        private void BuildAndRun(object state)
        {
            //if no file opened
            if (CurrentProblem == null) return;

            //type of task to be done
            bool buildonly = ((BuildRunType)state == BuildRunType.BuildOnly);
            bool runtest = ((BuildRunType)state == BuildRunType.RunTest);

            try
            {
                BeforeBuildRunTask();

                //compile task
                RunBuildTask();

                //run builded file
                if (!buildonly) RunRunTask(runtest);
            }
            catch (Exception ex)
            {
                runtest = false;
                Logger.Add(ex.Message, "Codes");
                this.BeginInvoke((MethodInvoker)delegate
                {
                    compilerOutput.AppendText(ex.Message + "\n", HighlightSyntax.CommentStyle);
                });
            }
            finally
            {
                AfterBuildRunTask(runtest);
            }
        }

        private void BeforeBuildRunTask()
        {
            //clear and initiate
            this.BeginInvoke((MethodInvoker)delegate
            {
                ClearOutputFile(); //clear output file
                saveInputTool.PerformClick(); //save input
                saveToolButton.PerformClick(); //save code
                codeTextBox.ClearHints(); //clear hints                
                compilerOutput.Clear(); //clear prev compiler report
                compileToolButton.Enabled = false;
                buildRunToolButton.Enabled = false;
                runtestToolButton.Enabled = false;
                //show compiler output
                if (compilerOutputIsHidden) ToggleCompilerOutput();
            });
        }

        public void AfterBuildRunTask(bool runtest)
        {
            //re-enable all data
            this.BeginInvoke((MethodInvoker)delegate
            {
                //enable the build and run buttons
                compileToolButton.Enabled = true;
                buildRunToolButton.Enabled = true;
                //if no problem is selected do not enable runtest button
                runtestToolButton.Enabled = (SelectedPNUM != -1);
                //go to end of output
                compilerOutput.GoEnd();
                //wait a little before processing message data                
                Thread.Sleep(100);
                this.BeginInvoke((MethodInvoker)delegate { ProcessErrorData(); });
                //if no error and runtest
                if (runtest) tabControl1.SelectedTab = ioTAB;
            });

        }

        private void RunBuildTask()
        {
            //get extension of opened file                
            string ext = CurrentProblem.Extension.ToLower();

            //show building messege
            this.BeginInvoke((MethodInvoker)delegate
            {
                compilerOutput.AppendText("Building...\n\n", HighlightSyntax.CommentStyle);
            });

            bool success = false;
            if (ext == ".c") success = C_Compile();
            else if (ext == ".cpp") success = CPP_Compile();
            else if (ext == ".java") success = Java_Compile();
            else throw new Exception("Language is not supported.");

            //show compilation result
            if (!success)
            {
                throw new Exception("Compilation Failed " + CurrentProblem.FullName);
            }

            this.BeginInvoke((MethodInvoker)delegate
            {
                compilerOutput.AppendText("Successfully Compiled.\n", HighlightSyntax.CommentStyle);
            });
        }

        private void RunRunTask(bool runtest)
        {
            //get extension of opened file                
            string ext = CurrentProblem.Extension.ToLower();

            //show running message
            this.BeginInvoke((MethodInvoker)delegate
            {
                compilerOutput.AppendText("Running...\n", HighlightSyntax.CommentStyle);
            });

            //run compiled code
            bool success = false;
            if (ext == ".c" || ext == ".cpp") success = C_CPP_Run(runtest);
            else if (ext == ".java") success = Java_Run(runtest);

            //show run result
            if (!success) throw new Exception("Process Failed.\n");

            this.BeginInvoke((MethodInvoker)delegate
            {
                compilerOutput.AppendText("Process Completed.\n", HighlightSyntax.CommentStyle);
            });
        }

        #endregion

        #region Task Executors

        //
        // Execution Process
        //

        void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string data = e.Data;
            if (data == null) return;
            this.BeginInvoke((MethodInvoker)delegate
            {
                //remove current file name at the beginning                
                if (CurrentProblem != null) data = data.Replace(CurrentProblem.FullName + ":", "");
                //add message to compiler output
                compilerOutput.AppendText(data.Trim() + "\n");
            });
        }

        private bool ExecuteProcess(string args, int timelim = -1, string procname = null)
        {
            //command prompt
            string cmd = Path.Combine(Environment.SystemDirectory, "cmd.exe");

            //prepare process
            Process proc = new Process();
            proc.ErrorDataReceived += proc_OutputDataReceived;
            proc.OutputDataReceived += proc_OutputDataReceived;
            proc.StartInfo = new ProcessStartInfo()
            {
                FileName = cmd,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/c \"" + args + "\""
            };

            //start process
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            if (timelim == -1) proc.WaitForExit();
            else proc.WaitForExit(timelim);

            //generate ending report
            bool tle = !proc.HasExited; //check if time limit has exceeded
            if (tle) ForceKill(procname); //force kill tle tasks by process-name
            int exitcode = tle ? -1 : proc.ExitCode; //get exitcode
            double total = tle ? (timelim / 1000.0) : proc.TotalProcessorTime.TotalSeconds;
            string runtime = string.Format("Runtime = {0:0.000} sec.", total);
            string verdict = string.Format("Exit Code = {0} ({1}).", exitcode,
                (exitcode == -1 ? "tle" : (exitcode == 0 ? "Successfull" : "Error")));

            //show output
            this.BeginInvoke((MethodInvoker)delegate
            {
                compilerOutput.AppendText("\n" + runtime + "\n", HighlightSyntax.CommentStyle);
                compilerOutput.AppendText(verdict + "\n", HighlightSyntax.CommentStyle);
            });

            //return result
            return (exitcode == 0);
        }

        private void ForceKill(string procname)
        {
            if (procname == null) return;

            //command prompt
            string cmd = Path.Combine(Environment.SystemDirectory, "cmd.exe");

            Process killer = new Process();
            //command prompt arguments                
            string taskkill = string.Format("/c \"taskkill /IM \"{0}\"\" /T /F", procname);
            killer.StartInfo = new ProcessStartInfo(cmd, taskkill)
            {
                //command prompt will be hidden
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            killer.Start();
            killer.WaitForExit();
        }

        private bool RunInBatch(string args, string title)
        {
            //get bat file
            string file = Path.GetTempFileName();
            string bat = file + ".bat";
            File.Move(file, bat);

            string commands =
                  "@cls\n"
                + "@title " + title + "\n"
                + "@" + args + "\n"
                + "@echo.\n"
                + "@set /p exit=Press Enter to exit...%=%\n"
                + "@exit";

            //write commands to batch file
            File.WriteAllText(bat, commands);

            //start batch file
            Process p = System.Diagnostics.Process.Start(bat);
            p.WaitForExit(); //wait till finish

            //delete batch file
            File.Delete(bat);

            //always task is completed
            return true;
        }

        #endregion

        #region Compilers

        //
        // C Compiler
        // 
        private bool C_Compile()
        {
            //file to compile
            string filename = CurrentProblem.FullName;

            //get compiler
            string compiler = MinGWLocation;
            compiler = Path.Combine(compiler, "bin");
            compiler = Path.Combine(compiler, "mingw32-gcc.exe");
            if (!File.Exists(compiler)) throw new Exception("Invalid MinGW path. Select one from settings.");

            //filename
            string name = Path.GetFileNameWithoutExtension(filename);
            string exec = Path.Combine(Path.GetDirectoryName(filename), name + ".exe");

            //options
            string options = CCompilerOptions;

            //run process
            string arguments = string.Format("\"{0}\" \"{1}\" {2} -o \"{3}", compiler, filename, options, exec);
            return ExecuteProcess(arguments);
        }

        //
        // C++ Compiler
        // 
        private bool CPP_Compile()
        {
            //file to compile
            string filename = CurrentProblem.FullName;

            //get compiler
            string compiler = MinGWLocation;
            compiler = Path.Combine(compiler, "bin");
            compiler = Path.Combine(compiler, "mingw32-g++.exe");
            if (!File.Exists(compiler)) throw new Exception("Invlaid MinGW path. Select one from settings.");

            //filename
            string name = Path.GetFileNameWithoutExtension(filename);
            string exec = Path.Combine(Path.GetDirectoryName(filename), name + ".exe");

            //options
            string options = CPPCompilerOptions;

            //run process
            string arguments = string.Format("\"{0}\" \"{1}\" {2} -o \"{3}", compiler, filename, options, exec);
            return ExecuteProcess(arguments);
        }

        //
        // Java Compiler
        //
        private bool Java_Compile()
        {
            //file to compile
            string filename = CurrentProblem.FullName;

            //get compiler
            string compiler = JDKLocation;
            compiler = Path.Combine(compiler, "bin");
            compiler = Path.Combine(compiler, "javac.exe");
            if (!File.Exists(compiler)) throw new Exception("Invalid JDK path. Select one from settings.");

            //filename
            string dir = Path.GetDirectoryName(filename);

            //options
            string options = JavaCompilerOptions;

            //run process
            string arguments = string.Format("\"{0}\" {1} -d \"{2}\" \"{3}\"", compiler, options, dir, filename);
            return ExecuteProcess(arguments);
        }

        #endregion

        #region Run builded file

        //
        // C and C++ Run
        //
        private bool C_CPP_Run(bool runtest)
        {
            //determine if custom input output is needed
            if (SelectedPNUM == -1) runtest = false;
            if (runtest && SelectedPNUM == -1) return false;

            //get exe file name
            string filename = Path.GetFileNameWithoutExtension(CurrentProblem.Name);
            string exec = Path.Combine(CurrentProblem.DirectoryName, filename + ".exe");

            //get argument parameters
            int runtime = (int)(RunTimeLimit * 1000);
            string input = Path.Combine(CurrentProblem.DirectoryName, "input.txt");
            string output = Path.Combine(CurrentProblem.DirectoryName, "output.txt");

            if (runtest)
            {
                //run process with predefined input
                string format = "\"{0}\" < \"{1}\" > \"{2}\"";
                string arguments = string.Format(format, exec, input, output);
                return ExecuteProcess(arguments, runtime, Path.GetFileName(exec));
            }
            else
            {
                //run process from a batch file
                string arguments = "\"" + exec + "\"";
                return RunInBatch(arguments, Path.GetFileNameWithoutExtension(exec));
            }
        }

        //
        // Java Run
        //
        private bool Java_Run(bool runtest)
        {
            //determine if custom input output is needed
            if (runtest && SelectedPNUM == -1) return false;
            if (SelectedPNUM == -1) runtest = false;

            //get compiler
            int runtime = (int)(RunTimeLimit * 1000);
            string exec = JDKLocation;
            exec = Path.Combine(exec, "bin");
            exec = Path.Combine(exec, "java.exe");
            if (!File.Exists(exec)) throw new Exception("Invalid JDK path. Select one from settings.");

            //file names and directories
            string dir = CurrentProblem.DirectoryName;
            string input = Path.Combine(CurrentProblem.DirectoryName, "input.txt");
            string output = Path.Combine(CurrentProblem.DirectoryName, "output.txt");

            //format of the arguments 
            string format = "\"{0}\" -classpath \"{1}\" Main < \"{2}\" > \"{3}\" ";
            if (!runtest) format = "\"{0}\" -classpath \"{1}\" Main";
            string arguments = string.Format(format, exec, dir, input, output);

            if (runtest)
            {
                //run process with predefined input-output
                return ExecuteProcess(arguments, runtime, Path.GetFileName(exec));
            }
            {
                //run from a batch file
                return RunInBatch(arguments, Path.GetFileNameWithoutExtension(CurrentProblem.Name));
            }

        }

        #endregion

        #region Compare Tool

        private void compareOutputButton_Click(object sender, EventArgs e)
        {
            if (CompareOutputTexts())
            {
                MessageBox.Show("Files matched.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Files did not match.",
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private int __updating = 0;
        private bool CompareOutputTexts()
        {
            //get file names
            string file1 = (string)correctOutputTextBox.Tag;
            string file2 = (string)outputTextBox.Tag;
            if (file1 == null || !File.Exists(file1)) return false;
            if (file2 == null || !File.Exists(file2)) return false;

            //first clear prev result
            progOutputTextBox.Clear();
            correctOutputTextBox.Clear();
            progOutputTextBox.Cursor = Cursors.WaitCursor;

            //load lines from file
            var source1 = DiffMergeStuffs.Lines.Load(file1);
            var source2 = DiffMergeStuffs.Lines.Load(file2);
            source1.Merge(source2);

            //begin update
            __updating++;

            //add lines
            _Process(source1, correctOutputTextBox, progOutputTextBox);

            //end update
            __updating--;

            progOutputTextBox.Cursor = Cursors.Default;
            return (File.ReadAllText(file1) == File.ReadAllText(file2));
        }

        private void _Process(DiffMergeStuffs.Lines lines, FastColoredTextBox fctb1, FastColoredTextBox fctb2)
        {
            foreach (var line in lines)
            {
                switch (line.state)
                {
                    case DiffMergeStuffs.DiffType.None:
                        fctb1.AppendText(line.line + Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine);
                        break;
                    case DiffMergeStuffs.DiffType.Inserted:
                        fctb1.AppendText(Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine, HighlightSyntax.GreenLineStyle);
                        break;
                    case DiffMergeStuffs.DiffType.Deleted:
                        fctb1.AppendText(line.line + Environment.NewLine, HighlightSyntax.RedLineStyle);
                        fctb2.AppendText(Environment.NewLine);
                        break;
                }
                if (line.subLines != null)
                    _Process(line.subLines, fctb1, fctb2);
            }
        }

        void tb_VisibleRangeChanged(object sender, EventArgs e)
        {
            if (__updating > 0) return;

            var vPos = (sender as FastColoredTextBox).VerticalScroll.Value;
            var curLine = (sender as FastColoredTextBox).Selection.Start.iLine;
            var curChar = (sender as FastColoredTextBox).Selection.Start.iChar;

            CurLnLabel.Text = string.Format((string)CurLnLabel.Tag, curLine);
            CurColLabel.Text = string.Format((string)CurColLabel.Tag, curChar);

            if (sender == progOutputTextBox)
                _UpdateScroll(correctOutputTextBox, vPos, curLine);
            else
                _UpdateScroll(progOutputTextBox, vPos, curLine);

            progOutputTextBox.Refresh();
            correctOutputTextBox.Refresh();
        }

        void _UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
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

        #region udebug


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == uDebugTab)
            {
                if (webBrowser1.Tag == null ||
                    (long)webBrowser1.Tag != SelectedPNUM)
                    homeDiscussButton.PerformClick();

                if (!compilerOutputIsHidden)
                    ToggleCompilerOutput();
            }
        }

        private void homeDiscussButton_Click(object sender, EventArgs e)
        {
            string url = "http://www.udebug.com";
            if (LocalDatabase.HasProblem(SelectedPNUM))
                url += "/UVa/" + SelectedPNUM;
            discussUrlBox.Text = url;
            webBrowser1.Tag = SelectedPNUM;
            webBrowser1.Navigate(url);
        }

        private void goDiscussButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Tag = SelectedPNUM;
            webBrowser1.Navigate(discussUrlBox.Text);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            discussUrlBox.Text = webBrowser1.Url.ToString();
            status1.Text = webBrowser1.StatusText;
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            status1.Text = webBrowser1.StatusText;
            progress1.Value = (int)(100 * e.CurrentProgress / e.MaximumProgress);
        }

        #endregion
    }
}

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

        #region Startup Functions

        public CODES()
        {
            InitializeComponent();

            codeTextBox.Font = Properties.Settings.Default.EditorFont;
            folderTreeView.PathSeparator = Path.DirectorySeparatorChar.ToString();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            string path = CodesPath;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                fileSystemWatcher1.Path = path;
                fileSystemWatcher1.EnableRaisingEvents = true;
                try { LoadCodeFolder(true); }
                catch (Exception ex) { Logger.Add(ex.Message, "Codes"); }
            }

            Stylish.SetGradientBackground(filenamePanel,
                new Stylish.GradientStyle(Color.LightBlue, Color.PaleTurquoise, 90F));

            Stylish.SetGradientBackground(toolStrip1,
                new Stylish.GradientStyle(Color.PaleTurquoise, Color.LightBlue, -90F));
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
                        using (CodeFileCreator cfc = new CodeFileCreator())
                        {
                            if (cfc.ShowDialog() == DialogResult.OK)
                            {
                                AddProblem((long)pnum, cfc.Language);
                                return;
                            }
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

        #region Load Folder Tree

        public bool IsReady = true;

        /// <summary>
        /// Create a list of tree nodes to display in the folderTreeView  
        /// It recursive track all files and folders stored in CodesPath
        /// </summary>
        /// <param name="background">True to run the loading process as a separate thread</param>
        public void LoadCodeFolder(object background)
        {
            if (!IsReady) return;

            //get code path
            string path = CodesPath;
            if (path == null || !Directory.Exists(path)) return;

            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(LoadCodeFolder, false);
                return;
            }

            //fist turn off some values
            IsReady = false;
            this.BeginInvoke((MethodInvoker)delegate
            {
                selectDirectoryPanel.Visible = false;
                folderTreeView.UseWaitCursor = false;
            });

            //list of new tree nodes
            List<TreeNode> parent = new List<TreeNode>();

            //top level folders
            foreach (string folder in Directory.GetDirectories(path))
            {
                DirectoryInfo difo = new DirectoryInfo(folder);
                //if (difo.Name.StartsWith(".")) continue;
                TreeNode nod = AddTreeNode(difo);
                AddChildNodes(nod);
                parent.Add(nod);
            }

            //top level files
            foreach (string file in Directory.GetFiles(path))
            {
                FileInfo fifo = new FileInfo(file);
                //if (fifo.Name.StartsWith(".")) continue;
                parent.Add(AddTreeNode(fifo));
            }

            //add codes
            if (this.IsDisposed || folderTreeView.IsDisposed) return;
            this.BeginInvoke((MethodInvoker)delegate
            {
                FileSystemInfo last = null;
                if (folderTreeView.SelectedNode != null)
                {
                    last = (FileSystemInfo)folderTreeView.SelectedNode.Tag;
                }

                folderTreeView.BeginUpdate();
                folderTreeView.Sort();
                folderTreeView.Nodes.Clear();
                folderTreeView.Nodes.AddRange(parent.ToArray());
                folderTreeView.EndUpdate();

                folderTreeView.UseWaitCursor = false;

                if (last != null) //restore last selection
                {
                    ExpandAndSelect(GetNode(last));
                }
                else if (parent.Count > 0)
                {
                    folderTreeView.Nodes[0].Expand();
                    folderTreeView.Nodes[0].Collapse();
                }
            });

            IsReady = true;
        }

        /// <summary>
        /// Recursively add child-nodes ignoring files and folders
        /// </summary>
        /// <param name="parent">Parent tree-node to add childs</param>
        public void AddChildNodes(TreeNode parent)
        {
            parent.Nodes.Clear();

            DirectoryInfo dir = (DirectoryInfo)parent.Tag;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                //if (d.Name.StartsWith(".")) continue;
                TreeNode child = AddTreeNode(d, parent);
                AddChildNodes(child);
            }
            foreach (FileInfo f in dir.GetFiles())
            {
                //if (f.Name.StartsWith(".")) continue;
                AddTreeNode(f, parent);
            }
        }

        /// <summary>
        /// Create and add a new tree node 
        /// (If parent any parent node is given, add tree node to the parent)
        /// </summary>
        /// <param name="info">FileInfo for file and DirectoryInfo for folder to create treenode upon</param>
        /// <param name="parent">Parent to add the TreeNode. If it is NULL</param>
        /// <returns>A reference to the creatd TreeNode.</returns>
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

        /// <summary>
        /// Get image key for given folder of file
        /// </summary>
        /// <param name="info">FileInfo for a file or DirectoryInfo for directory</param>
        /// <returns>Image keys from imagelist1</returns>
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

        #endregion

        #region Code Path Selector

        public void FormatCodeDirectory(object background)
        {
            //gather all files
            string path = CodesPath;
            if (!Directory.Exists(path)) return;

            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(FormatCodeDirectory, false);
                return;
            }

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

            //now create files for precodes
            LocalDirectory.GetPrecode(Structures.Language.C);
            LocalDirectory.GetPrecode(Structures.Language.CPP);
            LocalDirectory.GetPrecode(Structures.Language.Java);
            LocalDirectory.GetPrecode(Structures.Language.Pascal);
        }

        public void ChangeCodeDirectory()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
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
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            ChangeCodeDirectory();
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

        private static TreeNode SelectNextNode(TreeNode node)
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

        public bool SearchProblem(string pat)
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


        #region Useful functions for Folder Tree

        /// <summary>
        /// Try to get respective tree node from FileSystemInfo
        /// </summary>
        /// <param name="finfo">FileInfo for file and DirectoryInfo for folder tree node</param>
        /// <returns>If error occured a null value if returned</returns>
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

        private string GetSelectedPath()
        {
            TreeNode tn = folderTreeView.SelectedNode;
            if (tn == null) return null;

            string dir = ((FileSystemInfo)tn.Tag).FullName;
            if (tn.Tag.GetType() == typeof(FileInfo))
                dir = ((FileInfo)tn.Tag).DirectoryName;

            return dir;
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

        private static bool ParseInputOutput(long pnum, string inpfile, string outfile, bool replace = false)
        {
            try
            {
                //get html
                string file = LocalDirectory.GetProblemHtml(pnum);
                if (LocalDirectory.GetFileSize(file) < 100) return false;

                int start, stop = 0, indx;
                string html, low, data;
                var xdoc = new System.Xml.XmlDocument();
                char[] white = { ' ', '\r', '\n' };

                html = File.ReadAllText(file);
                low = html.ToLower();

                bool ok = false;

                //get input
                while (true)
                {
                    if (LocalDirectory.GetFileSize(inpfile) > 2 && !replace) break;
                    indx = low.IndexOf("sample input");
                    if (indx < 0) break;
                    start = low.IndexOf("<pre>", indx);
                    if (start < 0) break;
                    stop = low.IndexOf("</pre>", start);
                    if (stop <= start) break;

                    xdoc.LoadXml(html.Substring(start, stop - start + 6));
                    data = xdoc.DocumentElement.InnerText.TrimStart(white);
                    data = data.Replace("\n\r", "\n");
                    File.WriteAllText(inpfile, data);
                    ok = true;
                    break;
                }

                //get output
                while (true)
                {
                    if (LocalDirectory.GetFileSize(outfile) > 2 && !replace) break;
                    indx = low.IndexOf("sample output", stop);
                    if (indx < 0) break;
                    start = low.IndexOf("<pre>", indx);
                    if (start < 0) break;
                    stop = low.IndexOf("</pre>", start);
                    if (stop <= start) break;

                    xdoc.LoadXml(html.Substring(start, stop - start + 6));
                    data = xdoc.DocumentElement.InnerText.TrimStart(white);
                    data = data.Replace("\n\r", "\n");
                    if (!data.EndsWith("\n")) data += "\n";
                    File.WriteAllText(outfile, data);
                    ok = true;
                    break;
                }

                return ok;
            }
            catch (Exception ex)
            {
                Logger.Add("Failed to write Input/Output data. Error: " + ex.Message, "CODES| ParseInputOutput()");
                return false;
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
                    //check if current problem is changed
                    if (CurrentProblem != null &&
                        CurrentProblem.FullName == e.OldFullPath)
                    {
                        OpenFile(new FileInfo(e.FullPath));
                        return;
                    }

                    //get file sustem info
                    FileSystemInfo dir = null;
                    FileSystemInfo cur = null;
                    if (File.Exists(e.FullPath))
                    {
                        dir = new FileInfo(e.OldFullPath);
                        cur = new FileInfo(e.FullPath);
                    }
                    else
                    {
                        dir = new DirectoryInfo(e.OldFullPath);
                        cur = new DirectoryInfo(e.FullPath);
                    }

                    //update all child properties
                    TreeNode child = AddTreeNode(cur);
                    AddChildNodes(child);
                    TreeNode tn = GetNode(dir);
                    if (tn == null || tn.Parent == null)
                    {
                        folderTreeView.Nodes.Add(child);
                    }
                    else
                    {
                        tn.Parent.Nodes.Add(child);
                    }
                    if (tn != null) tn.Remove();
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
                else if ((string)correctOutputTextBox.Tag == e.FullPath)
                {
                    OpenCorrectFile(e.FullPath);
                }
            }
            catch { }
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
                //check for name validity
                if (!LocalDirectory.IsValidFileName(e.Label))
                {
                    throw new InvalidDataException("File/Folder name is not valid.");
                }

                //get new path
                FileSystemInfo fsi = (FileSystemInfo)e.Node.Tag;
                string directory = Path.GetDirectoryName(fsi.FullName);
                string newpath = Path.Combine(directory, e.Label);

                //check extension change
                string ext = Path.GetExtension(newpath);
                if (ext.ToLower() != fsi.Extension.ToLower())
                {
                    string msg = "Change the extension from {0} to {1}?";
                    if (MessageBox.Show(string.Format(msg, fsi.Extension, ext),
                        Application.ProductName, MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        e.CancelEdit = true;
                        return;
                    }
                }

                //rename operatior
                LocalDirectory.RenameFileOrFolder(fsi.FullName, newpath);
            }
            catch (Exception ex)
            {
                e.CancelEdit = true;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                folderTreeView.LabelEdit = false;
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
            if (dir == null) return;
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".c");
            else AddProblem(pnum, Structures.Language.C);
        }

        private void cPPFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            if (dir == null) return;
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".cpp");
            else AddProblem(pnum, Structures.Language.CPP);
        }

        private void javaFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            if (dir == null) return;
            long pnum = GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".java");
            else AddProblem(pnum, Structures.Language.Java);
        }

        private void pascalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            if (dir == null) return;
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
        public FileInfo CurrentProblem = null;
        public Structures.Language CustomLang = Structures.Language.CPP;

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
            progOutputTextBox.Clear();

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

        #region Code Toolbar / Context Menu / Tab Control

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = (!LocalDatabase.HasProblem(SelectedPNUM));
            if (e.Cancel)
            {
                MessageBox.Show("Select a problem's code to enable this feature.");
            }
        }

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


        #region Compilation Tool Bar

        //
        // Compile and Run
        //
        private double RunTimeLimit = 3.000;

        private void BuildAndRun(object state)
        {
            //if no file opened
            if (Interactivity.codes.CurrentProblem == null) return;

            //clear and initiate
            Interactivity.codes.BeginInvoke((MethodInvoker)delegate
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

            //run task
            bool ok = CodeCompiler.BuildAndRun((BuildRunType)state,
                CurrentProblem, SelectedPNUM, RunTimeLimit);


            //re-enable all data
            Interactivity.codes.BeginInvoke((MethodInvoker)delegate
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
                if ((BuildRunType)state == BuildRunType.RunTest && ok)
                    tabControl1.SelectedTab = compareTAB;
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
            if (CurrentProblem == null)
            {
                MessageBox.Show("Create a code file first.");
            }
            else
            {
                codeTextBox.OpenFile(LocalDirectory.GetPrecode(CustomLang));
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
                compilerSplitContainer1.SplitterDistance = 7 * compilerSplitContainer1.Height / 10;
            }
            else
            {
                compilerOutputIsHidden = true;
                showHideOutput.Image = Properties.Resources.maximize;
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
                ofd.Dispose();
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
        //Load Default 
        // 
        private void loadDefaultInput_Click(object sender, EventArgs e)
        {
            if (!LocalDatabase.HasProblem(SelectedPNUM)) return;
            string path = LocalDirectory.GetCodesPath(SelectedPNUM);
            string inp = Path.Combine(path, "input.txt");
            string correct = Path.Combine(path, "correct.txt");
            if (!ParseInputOutput(SelectedPNUM, inp, correct, true))
            {
                MessageBox.Show("Can't load input-output automatically. Parsing failed.");
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
                ofd.Dispose();
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

        #region Compare Outputs

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
            bool res = _ProcessDiff(source1, correctOutputTextBox, progOutputTextBox);

            //end update
            __updating--;

            progOutputTextBox.Cursor = Cursors.Default;
            return res;
        }

        private bool _ProcessDiff(DiffMergeStuffs.Lines lines, FastColoredTextBox fctb1, FastColoredTextBox fctb2)
        {
            bool match = true;
            foreach (var line in lines)
            {
                switch (line.state)
                {
                    case DiffMergeStuffs.DiffTypes.None:
                        fctb1.AppendText(line.line + Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine);
                        break;
                    case DiffMergeStuffs.DiffTypes.Inserted:
                        fctb1.AppendText(Environment.NewLine);
                        fctb2.AppendText(line.line + Environment.NewLine, HighlightSyntax.GreenLineStyle);
                        match = false;
                        break;
                    case DiffMergeStuffs.DiffTypes.Deleted:
                        fctb1.AppendText(line.line + Environment.NewLine, HighlightSyntax.RedLineStyle);
                        fctb2.AppendText(Environment.NewLine);
                        match = false;
                        break;
                }
                if (line.subLines != null)
                    match = match && _ProcessDiff(line.subLines, fctb1, fctb2);
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

            if (sender == progOutputTextBox)
                _UpdateScroll(correctOutputTextBox, vPos, curLine);
            else
                _UpdateScroll(progOutputTextBox, vPos, curLine);

            progOutputTextBox.Refresh();
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


        #region STATIC FUNCTIONS

        public enum ExpandSelectType
        {
            ExpandToNode,
            ExpandWithNode,
            SelecFirstChild
        }

        public static void ExpandAndSelect(TreeNode node, ExpandSelectType type = ExpandSelectType.ExpandToNode)
        {
            if (node == null) return;

            ExpandToNode(node);
            node.TreeView.SelectedNode = node;

            if (type != ExpandSelectType.ExpandToNode) node.Expand();
            if (type == ExpandSelectType.SelecFirstChild)
            {
                if (node.Nodes.Count > 0)
                    node.TreeView.SelectedNode = node.Nodes[0];
            }
        }
        public static void ExpandToNode(TreeNode node)
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

        private static long GetProblemNumber(string name)
        {
            if (string.IsNullOrEmpty(name)) return -1;
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

        private static void CreateFile(string par, string name, string ext, bool trial = true)
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

        private static void CreateDirectory(string par, string name, bool trial = true)
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

    }
}

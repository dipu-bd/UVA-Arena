using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UVA_Arena.Elements
{
    public partial class CodesBrowser : UserControl
    {
        #region Startup

        public CodesBrowser()
        {
            InitializeComponent();
            folderTreeView.PathSeparator = Path.DirectorySeparatorChar.ToString();
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            string path = RegistryAccess.CodesPath;
            if (Directory.Exists(path))
            {
                fileSystemWatcher1.Path = path;
                fileSystemWatcher1.EnableRaisingEvents = true;
                try { LoadCodeFolder(true); }
                catch (Exception ex) { Logger.Add(ex.Message, "Codes"); }
            }
        }

        #endregion

        #region Code Path Selector

        public void FormatCodeDirectory(object background)
        {
            //gather all files
            string path = RegistryAccess.CodesPath;
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
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a folder that stores code files.";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                RegistryAccess.CodesPath = fbd.SelectedPath;
                try
                {
                    FormatCodeDirectory(true);
                    LoadCodeFolder(true);
                    fileSystemWatcher1.Path = fbd.SelectedPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    RegistryAccess.CodesPath = null;
                    selectDirectoryPanel.Visible = true;
                }
            }
            fbd.Dispose();
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            ChangeCodeDirectory();
        }

        private void cancelBrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(RegistryAccess.CodesPath) || !Directory.Exists(RegistryAccess.CodesPath))
                {
                    RegistryAccess.CodesPath = LocalDirectory.DefaultCodesPath();
                    FormatCodeDirectory(true);
                }
                LoadCodeFolder(true);
                fileSystemWatcher1.Path = RegistryAccess.CodesPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                RegistryAccess.CodesPath = null;
                selectDirectoryPanel.Visible = true;
            }
        }

        #endregion

        #region Folder Tree List

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (folderTreeView.SelectedNode == null) Interactivity.codes.OpenFile(null);
            if (folderTreeView.SelectedNode.Tag.GetType() == typeof(FileInfo))
            {
                Interactivity.codes.OpenFile((FileInfo)folderTreeView.SelectedNode.Tag);
            }
            else Interactivity.codes.OpenFile(null);
        }

        private void folderTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Clicks != 1) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                folderTreeView.SelectedNode = e.Node;
            }
        }

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
                if (tn == null || !(e.Alt || Interactivity.codes.CurrentProblem != tn.Tag)) return;
                if (tn.Tag.GetType() == typeof(FileInfo))
                {
                    try { System.Diagnostics.Process.Start(((FileInfo)tn.Tag).FullName); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
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
            string path = RegistryAccess.CodesPath;
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
                folderTreeView.UseWaitCursor = true;
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

            string name = info.FullName.Substring(RegistryAccess.CodesPath.Length + 1);
            if (!name.StartsWith("Volume"))
            {
                if (name.Contains(Path.DirectorySeparatorChar.ToString())) return key = "folder.png";
                return "root.png";
            }

            if (name.Contains(Path.DirectorySeparatorChar.ToString())) return "problem.png";
            return "volume.png";
        }

        #endregion

        #region Useful functions

        public enum ExpandSelectType
        {
            ExpandToNode,
            ExpandWithNode,
            SelecFirstChild
        }

        /// <summary>
        /// Try to get respective tree node from FileSystemInfo
        /// </summary>
        /// <param name="finfo">FileInfo for file and DirectoryInfo for folder tree node</param>
        /// <returns>If error occured a null value if returned</returns>
        public TreeNode GetNode(FileSystemInfo finfo)
        {
            try
            {
                string path = finfo.FullName.Substring(RegistryAccess.CodesPath.Length + 1);
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

        public void ExpandAndSelect(TreeNode node, ExpandSelectType type = ExpandSelectType.ExpandToNode)
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


        public static void CreateFile(string par, string name, string ext, bool trial = true)
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

        public static void CreateDirectory(string par, string name, bool trial = true)
        {
            if (par == null) par = RegistryAccess.CodesPath;

            int tcount = 1;
            string path = Path.Combine(par, name);
            while (trial && Directory.Exists(path))
            {
                path = Path.Combine(par, string.Format("{0} ({1})", name, tcount));
                ++tcount;
            }
            LocalDirectory.CreateDirectory(path);
        }


        public string GetSelectedPath()
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

        public static bool ParseInputOutput(long pnum, string inpfile, string outfile, bool replace = false)
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

        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            if (!IsReady) return;

            try
            {
                if (RegistryAccess.CodesPath == e.FullPath)
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
                    if (Interactivity.codes.CurrentProblem == null) return;
                    if (Interactivity.codes.CurrentProblem.FullName == e.FullPath)
                        Interactivity.codes.OpenFile(null);
                }
            }
            catch { }
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            if (!IsReady) return;

            try
            {
                if (RegistryAccess.CodesPath == e.FullPath)
                {
                    RegistryAccess.CodesPath = e.FullPath;
                }
                else
                {
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

                    //update current node and its childs
                    TreeNode tn = GetNode(dir);
                    if (tn != null)
                    {
                        tn.Name = cur.Name;
                        tn.Text = cur.Name;
                        tn.Tag = cur;
                        tn.ImageKey = GetKey(cur);
                        tn.SelectedImageKey = tn.ImageKey;
                        AddChildNodes(tn);
                    }
                }
            }
            catch { }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (Interactivity.codes.CurrentProblem == null) return;
                if (Interactivity.codes.CurrentProblem.FullName == e.FullPath)
                {
                    Interactivity.codes.OpenCodeFile(e.FullPath);
                }
                else if ((string)Interactivity.codes.OpenedInput == e.FullPath)
                {
                    Interactivity.codes.OpenInputFile(e.FullPath);
                }
                else if ((string)Interactivity.codes.OpenedOutput == e.FullPath)
                {
                    Interactivity.codes.OpenOutputFile(e.FullPath);
                }
                else if ((string)Interactivity.codes.OpenedCorrect == e.FullPath)
                {
                    Interactivity.codes.OpenCorrectFile(e.FullPath);
                }
            }
            catch { }
        }

        #endregion

        #region Cyclick Seach Box

        private void searchBox1_SearchButtonClicked(object sender, EventArgs e)
        {
            bool match = SearchProblem(searchBox1.SearchText);
            if (!match) MessageBox.Show("No match found. Reached end of the tree.");
        }

        private void searchBox1_ClearButtonClicked(object sender, EventArgs e)
        {
            SearchProblem("");
        }

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


        #endregion

        #region Context Menu Items

        //
        // New Context Menu Item
        //
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
            long pnum = LocalDatabase.GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".c");
            else AddProblem(pnum, Structures.Language.C);
        }

        private void cPPFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            if (dir == null) return;
            long pnum = LocalDatabase.GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".cpp");
            else AddProblem(pnum, Structures.Language.CPP);
        }

        private void javaFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            if (dir == null) return;
            long pnum = LocalDatabase.GetProblemNumber(Path.GetFileName(dir));
            if (pnum == -1) CreateFile(dir, "New Program", ".java");
            else AddProblem(pnum, Structures.Language.Java);
        }

        private void pascalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dir = GetSelectedPath();
            if (dir == null) return;
            long pnum = LocalDatabase.GetProblemNumber(Path.GetFileName(dir));
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

        //
        // Other context menu
        //

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


        #endregion

    }
}

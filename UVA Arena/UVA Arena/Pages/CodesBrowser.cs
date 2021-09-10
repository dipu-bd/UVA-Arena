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

            //load images
            imageList1.Images.Add("root", Properties.Resources.root);
            imageList1.Images.Add("volume", Properties.Resources.volumes);
            imageList1.Images.Add("problem", Properties.Resources.problem);
            imageList1.Images.Add("folder", Properties.Resources.folder);
            imageList1.Images.Add("file", Properties.Resources.file);
            imageList1.Images.Add(".c", Properties.Resources.ansi_c);
            imageList1.Images.Add(".cpp", Properties.Resources.cpp);
            imageList1.Images.Add(".java", Properties.Resources.java);
            imageList1.Images.Add(".pascal", Properties.Resources.pascal);
            imageList1.Images.Add("correct.txt", Properties.Resources.correct);
            imageList1.Images.Add("input.txt", Properties.Resources.input);
            imageList1.Images.Add("output.txt", Properties.Resources.output);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadCodeFolder(true);
        }

        public void CheckCodesPath()
        {
            if (!Directory.Exists(RegistryAccess.CodesPath))
            {
                folderTreeView.Nodes.Clear();
                fileSystemWatcher1.EnableRaisingEvents = false;
                selectDirectoryPanel.Visible = true;
            }
        }

        #endregion

        #region Load Folder Tree

        public bool IsReady = true;

        /// <summary>
        /// Formats the code directory with default files and folders
        /// </summary>
        /// <param name="background">True to run this process on background</param>
        public void FormatCodeDirectory(object background)
        {
            if (!IsReady) return;

            //gather all files
            string path = RegistryAccess.CodesPath;
            if (!Directory.Exists(path)) return;

            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(FormatCodeDirectory, false);
                return;
            }

            IsReady = false;

            Interactivity.SetStatus("Formatting code directory started...");

            this.BeginInvoke((MethodInvoker)delegate
            {
                selectDirectoryPanel.Visible = false;
                folderTreeView.UseWaitCursor = true;
            });

            //create codes-path and check them
            if (!LocalDatabase.IsReady)
            {
                Logger.Add("Problem Database is not ready.", "Codes | FormatCodeDirectory()");
                return;
            }

            //just call codesPath. it will create directory automatically
            foreach (Structures.ProblemInfo prob in LocalDatabase.problemList)
            {
                LocalDirectory.GetCodesPath(prob.pnum);
            }

            //now create files for precode
            LocalDirectory.GetPrecode(Structures.Language.C);
            LocalDirectory.GetPrecode(Structures.Language.CPP);
            LocalDirectory.GetPrecode(Structures.Language.Java);
            LocalDirectory.GetPrecode(Structures.Language.Pascal);

            IsReady = true;
            LoadCodeFolder(false);
            Interactivity.SetStatus("Formatting code directory finished.");
        }

        /// <summary>
        /// Create a list of tree nodes to display in the folderTreeView  
        /// It recursive track all files and folders stored in CodesPath
        /// </summary>
        /// <param name="background">True to run the loading process as a separate thread</param>
        public void LoadCodeFolder(object background)
        {
            //go to background
            if (!IsReady) return;
            if ((bool)background)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(LoadCodeFolder, false);
                return;
            }
            IsReady = false;

            try
            {
                //get code path
                string path = RegistryAccess.CodesPath;
                if (path == null || !Directory.Exists(path)) return;

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
                    if (nod != null)
                    {
                        AddChildNodes(nod);
                        parent.Add(nod);
                    }
                }

                //top level files
                foreach (string file in Directory.GetFiles(path))
                {
                    FileInfo fifo = new FileInfo(file);
                    //if (fifo.Name.StartsWith(".")) continue;
                    TreeNode nod = AddTreeNode(fifo);
                    if (nod != null) parent.Add(nod);
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

                    fileSystemWatcher1.Path = path;
                    fileSystemWatcher1.EnableRaisingEvents = true;

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
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Codes | LoadCodeFolder");
            }
            finally
            {
                IsReady = true;
                Interactivity.SetStatus("Code directory loaded.");
            }
        }

        /// <summary>
        /// Import old uva codes into new folder
        /// </summary> 
        public void ImportOldCodes(object state)
        {
            if (!IsReady) return;

            object[] data = (object[])state;
            bool background = (bool)data[0];
            string oldpath = (string)data[1];
            if (background)
            {
                data[0] = false;
                System.Threading.ThreadPool.QueueUserWorkItem(ImportOldCodes, data);
                return;
            }
            IsReady = false;

            try
            {
                Interactivity.SetStatus("Importing codes...");

                //get current path
                string path = RegistryAccess.CodesPath;
                if (!Directory.Exists(path)) return;

                //copy all files
                foreach (string file in Directory.GetFiles(oldpath, "*.*", SearchOption.AllDirectories))
                {
                    //get problem number guesses
                    List<int> guesses = new List<int>();
                    int tmp = 0;
                    foreach (char ch in Path.GetFileNameWithoutExtension(file))
                    {
                        if (char.IsNumber(ch))
                        {
                            tmp = tmp * 10 + ch - '0';
                        }
                        else
                        {
                            if (tmp > 0) guesses.Add(tmp);
                            tmp = 0;
                        }
                    }

                    //check if a guess matches
                    Structures.ProblemInfo pinfo = null;
                    foreach (int item in guesses)
                    {
                        pinfo = LocalDatabase.GetProblem(item);
                        if (pinfo != null) break;
                    }
                    if (pinfo == null) continue;

                    //copy this file to guessed problem directory
                    string code = CreateFile(
                        LocalDirectory.GetCodesPath(pinfo.pnum),
                        Path.GetFileNameWithoutExtension(file),
                        Path.GetExtension(file));
                    if (code == null) continue;
                    File.Copy(file, code, true);
                    Interactivity.SetStatus("Importing codes... [" + pinfo.pnum.ToString() + "]");
                }
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "ImportCodes()|CodesBrowser");
            }
            finally
            {
                Interactivity.SetStatus("Finished importing codes.");
                IsReady = true;
            }

            LoadCodeFolder(false);
        }

        /// <summary>
        /// Recursively add child-nodes ignoring files and folders
        /// </summary>
        /// <param name="parent">Parent tree-node to add children</param>
        public void AddChildNodes(TreeNode parent)
        {
            if (parent == null || parent.Tag.GetType() != typeof(DirectoryInfo)) return;

            parent.Nodes.Clear();
            DirectoryInfo dir = (DirectoryInfo)parent.Tag;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                //if (d.Name.StartsWith(".")) continue;
                TreeNode child = AddTreeNode(d, parent);
                if (child != null) AddChildNodes(child);
            }
            foreach (FileInfo f in dir.GetFiles())
            {
                //if (f.Name.StartsWith(".")) continue;
                AddTreeNode(f, parent);
            }
        }

        /// <summary>
        /// Checks whether given file system info contains in the nodes collection
        /// </summary>
        /// <param name="finfo">File system info of child nodes</param>
        /// <param name="nodes">Tree Node Collection of parent's childs</param>
        /// <returns></returns>
        private static bool CanAddNode(FileSystemInfo finfo, TreeNodeCollection nodes)
        {
            if (!finfo.Exists) return false;
            foreach (TreeNode nod in nodes)
            {
                var fs = (FileSystemInfo)nod.Tag;
                if (!fs.Exists)
                    nod.Remove();
                else if (finfo.FullName == fs.FullName)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Create and add a new tree node 
        /// (If parent any parent node is given, add tree node to the parent)
        /// </summary>
        /// <param name="info">FileInfo for file and DirectoryInfo for folder to create tree node upon</param>
        /// <param name="parent">Parent to add the TreeNode. If it is NULL</param>
        /// <returns>A reference to the created TreeNode.</returns>
        private TreeNode AddTreeNode(FileSystemInfo info, TreeNode parent = null)
        {
            try
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
            catch { return null; }
        }

        /// <summary>
        /// Get image key for given folder of file
        /// </summary>
        /// <param name="info">FileInfo for a file or DirectoryInfo for directory</param>
        /// <returns>Image keys from imagelist1</returns>
        private string GetKey(FileSystemInfo info)
        {
            string key = info.Name.ToLower();
            try { if (imageList1.Images.ContainsKey(key)) return key; }
            catch { }

            key = info.Extension.ToLower();
            try { if (imageList1.Images.ContainsKey(key)) return key; }
            catch { }

            if (info.GetType() == typeof(FileInfo)) return "file";

            string name = info.FullName.Substring(RegistryAccess.CodesPath.Length + 1);
            if (!name.StartsWith("Volume"))
            {
                if (name.Contains(Path.DirectorySeparatorChar.ToString())) return key = "folder";
                return "root";
            }

            if (name.Contains(Path.DirectorySeparatorChar.ToString())) return "problem";
            return "volume";
        }

        #endregion

        #region Code Path Selector


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
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Logger.Add(ex.Message, "Codes|ChangeCodeDirectory()");

                    RegistryAccess.CodesPath = null;
                    selectDirectoryPanel.Visible = true;
                    fileSystemWatcher1.Path = null;
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
                RegistryAccess.CodesPath = LocalDirectory.DefaultCodesPath();
                FormatCodeDirectory(true);
            }
            catch (Exception ex)
            {
                this.BeginInvoke((MethodInvoker)CheckCodesPath);
                Logger.Add(ex.Message, "Codes|CancelBrowserButton()");
            }
        }


        #endregion

        #region Folder Tree List

        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (folderTreeView.SelectedNode == null)
            {
                Interactivity.codes.OpenFile(null);
            }

            var tag = folderTreeView.SelectedNode.Tag;
            if (tag.GetType() == typeof(FileInfo))
            {
                Interactivity.codes.OpenFile((FileInfo)folderTreeView.SelectedNode.Tag);
            }
            else if (folderTreeView.SelectedNode.Nodes.Count > 0)
            {
                var child = folderTreeView.SelectedNode.Nodes[0];
                FileSystemInfo fsi = (FileSystemInfo)child.Tag;
                if (fsi.GetType() == typeof(FileInfo))
                {
                    folderTreeView.SelectedNode = child;
                }
            }
            else
            {
                Interactivity.codes.OpenFile(null);
            }
        }

        private void folderTreeView_Enter(object sender, EventArgs e)
        {
            if (!Directory.Exists(RegistryAccess.CodesPath))
            {
                CheckCodesPath();
            }
            else if (folderTreeView.Nodes.Count == 0)
            {
                FormatCodeDirectory(true);
            }
        }

        private void folderTreeView_MouseMove(object sender, MouseEventArgs e)
        {
            CheckCodesPath();
        }

        private void folderTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            CheckCodesPath();

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
                if (tn == null) return;

                FileSystemInfo fsi = (FileSystemInfo)tn.Tag;
                if (fsi.GetType() == typeof(FileInfo))
                {
                    System.Diagnostics.Process.Start(fsi.FullName);
                }
                else if (tn.Nodes.Count == 0 && tn.ImageKey == "problem")
                {
                    long number = LocalDatabase.GetProblemNumber(fsi.Name);
                    AddProblem(number);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void folderTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift && !e.Control)
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null || !(e.Alt || Interactivity.codes.CurrentFile != tn.Tag)) return;
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

                //rename operation
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


        #region Useful functions

        public enum ExpandSelectType
        {
            ExpandToNode,
            ExpandWithNode,
            SelecFirstChild
        }

        /// <summary>
        /// Expand up to path of given problem number and select top file inside that folder
        /// Prompt for new file if none exist
        /// </summary>
        /// <param name="pnum">Problem number</param>
        public void ShowCode(object pnum)
        {
            if (!Directory.Exists(RegistryAccess.CodesPath)) return;
            if (!IsReady || folderTreeView.Nodes.Count == 0)
            {
                if (this.IsDisposed) return;
                TaskQueue.AddTask(ShowCode, pnum, 1000);
                return;
            }

            //create code file if doesn't exist
            string path = LocalDirectory.GetCodesPath((long)pnum);
            if (!Directory.Exists(path) || Directory.GetFiles(path).Length == 0)
            {
                this.BeginInvoke((MethodInvoker)(() => AddProblem((long)pnum)));
                return;
            }

            this.BeginInvoke((MethodInvoker)delegate
            {
                //select code file path
                TreeNode tn = GetNode(new DirectoryInfo(path));
                CodesBrowser.ExpandAndSelect(tn, CodesBrowser.ExpandSelectType.SelecFirstChild);
            });
        }

        /// <summary>
        /// Try to get respective tree node from FileSystemInfo
        /// </summary>
        /// <param name="finfo">FileInfo for file and DirectoryInfo for folder tree node</param>
        /// <returns>If error occurred a null value if returned</returns>
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

        /// <summary>
        /// Creates a new file in the directory
        /// </summary>
        /// <param name="par">Full path to directory</param>
        /// <param name="name">Name of the file</param>
        /// <param name="ext">Extension of the file</param>
        /// <param name="trial">True to run trial check if a file already exist</param>
        public static string CreateFile(string par, string name, string ext, bool trial = true)
        {
            if (string.IsNullOrEmpty(par)) return null;

            int tcount = 1;
            string path = Path.Combine(par, name + ext);
            while (trial && File.Exists(path))
            {
                path = Path.Combine(par, string.Format("{0} ({1}){2}", name, tcount, ext));
                ++tcount;
            }
            LocalDirectory.CreateFile(path);

            return path;
        }

        /// <summary>
        /// Creates a new directory in the parent directory
        /// </summary>
        /// <param name="par">Full path to parent directory</param>
        /// <param name="name">Name of the directory</param> 
        /// <param name="trial">True to run trial check if a file already exist</param>
        public static string CreateDirectory(string par, string name, bool trial = true)
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

            return path;
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

        public void AddProblem(long pnum)
        {
            //get code path
            string path = LocalDirectory.GetCodesPath(pnum);
            if (string.IsNullOrEmpty(path)) return;

            //get language
            CodeFileCreator cfc = new CodeFileCreator();
            if (cfc.ShowDialog() != DialogResult.OK) return;
            Structures.Language lang = cfc.Language;
            cfc.Dispose();

            //get file extension
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
                //get HTML
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
                if (Directory.Exists(e.FullPath))
                {
                    tn = AddTreeNode(new DirectoryInfo(e.FullPath));
                    AddChildNodes(tn);
                }
                else if (File.Exists(e.FullPath))
                {
                    tn = AddTreeNode(new FileInfo(e.FullPath));
                }
                if (tn == null) return;

                if (path == RegistryAccess.CodesPath)
                {
                    if (CanAddNode((FileSystemInfo)tn.Tag, folderTreeView.Nodes))
                    {
                        folderTreeView.Nodes.Add(tn);
                    }
                }
                else
                {
                    TreeNode par = GetNode(new DirectoryInfo(path));
                    if (par != null && CanAddNode((FileSystemInfo)tn.Tag, par.Nodes))
                    {
                        par.Nodes.Add(tn);
                    }
                }
            }
            catch { }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            if (!IsReady) return;

            try
            {
                if (RegistryAccess.CodesPath == e.FullPath)
                {
                    folderTreeView.Nodes.Clear();
                    CheckCodesPath();
                    return;
                }
                else
                {
                    DirectoryInfo dir = new DirectoryInfo(e.FullPath);
                    TreeNode tn = GetNode(dir);
                    if (tn != null) tn.Remove();

                    if (Interactivity.codes.CurrentFile == null) return;
                    if (Interactivity.codes.CurrentFile.FullName == e.FullPath)
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
                    //get file system info
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

                    //update current node and its children
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
                if (Interactivity.codes.CurrentFile == null) return;
                if (Interactivity.codes.CurrentFile.FullName == e.FullPath)
                {
                    Interactivity.codes.OpenCodeFile(e.FullPath);
                }
                else if (Interactivity.codes.OpenedInput == e.FullPath)
                {
                    Interactivity.codes.OpenInputFile(e.FullPath);
                }
                else if (Interactivity.codes.OpenedOutput == e.FullPath)
                {
                    Interactivity.codes.OpenOutputFile(e.FullPath);
                }
                else if (Interactivity.codes.OpenedCorrect == e.FullPath)
                {
                    Interactivity.codes.OpenCorrectFile(e.FullPath);
                }
            }
            catch { }
        }

        #endregion

        #region Cyclic Search Box

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
            string path = GetSelectedPath();
            if (path == null) return;
            CreateDirectory(path, "New Folder");
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            CreateFile(path, "New Text File", ".txt");
        }

        private void codeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            long pnum = LocalDatabase.GetProblemNumber(Path.GetFileName(path));
            if (LocalDatabase.HasProblem(pnum))
            {
                AddProblem(pnum);
            }
        }

        private void cFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            CreateFile(path, "New Program", ".c");
        }

        private void cPPFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            CreateFile(path, "New Program", ".cpp");
        }

        private void javaFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Interactivity.codes.CurrentFile == null) return;
            string path = Interactivity.codes.CurrentFile.DirectoryName;
            CreateFile(path, "New Program", ".java");
        }

        private void pascalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Interactivity.codes.CurrentFile == null) return;
            string path = Interactivity.codes.CurrentFile.DirectoryName;
            CreateFile(path, "New Program", ".pascal");
        }

        private void inputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            CreateFile(path, "input", ".txt", false);
        }

        private void outputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            CreateFile(path, "output", ".txt", false);
        }

        private void correctOutputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetSelectedPath();
            if (path == null) return;
            CreateFile(path, "correct", ".txt", false);
        }

        //
        // Other context menu
        //

        private void folderTreeContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            StringCollection strcol = Clipboard.GetFileDropList();
            pasteToolStripMenuItem.Enabled = !(strcol == null || strcol.Count == 0);
        }

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
                string path = RegistryAccess.CodesPath;
                if (tn != null) path = ((FileSystemInfo)tn.Tag).FullName;
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(RegistryAccess.CodesPath))
            {
                CheckCodesPath();
            }
            else if (folderTreeView.Nodes.Count == 0)
            {
                FormatCodeDirectory(true);
            }
            else
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null)
                    LoadCodeFolder(true);
                else
                    AddChildNodes(tn);
            }
        }


        private void refreshTool_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(RegistryAccess.CodesPath))
            {
                CheckCodesPath();
            }
            else if (folderTreeView.Nodes.Count == 0)
            {
                FormatCodeDirectory(true);
            }
            else
            {
                LoadCodeFolder(true);
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null) return;
                string path = ((FileSystemInfo)tn.Tag).FullName;
                StringCollection clip = new StringCollection();
                clip.Add(path);

                byte[] moveEffect = new byte[] { 2, 0, 0, 0 }; //cut = 0x2000
                MemoryStream dropEffect = new MemoryStream();
                dropEffect.Write(moveEffect, 0, moveEffect.Length);

                DataObject data = new DataObject();
                data.SetFileDropList(clip);
                data.SetData("Preferred DropEffect", dropEffect);

                Clipboard.Clear();
                Clipboard.SetDataObject(data, true);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "CodesBrowser|cutToolStripMenuItem_Click");
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = folderTreeView.SelectedNode;
                if (tn == null) return;
                string path = ((FileSystemInfo)tn.Tag).FullName;
                StringCollection clip = new StringCollection();
                clip.Add(path);

                byte[] moveEffect = new byte[] { 5, 0, 0, 0 }; //copy = 0x5000
                MemoryStream dropEffect = new MemoryStream();
                dropEffect.Write(moveEffect, 0, moveEffect.Length);

                DataObject data = new DataObject();
                data.SetFileDropList(clip);
                data.SetData("Preferred DropEffect", dropEffect);

                Clipboard.Clear();
                Clipboard.SetDataObject(data, true);
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "CodesBrowser|copyToolStripMenuItem_Click");
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = GetSelectedPath();
                List<string> from = new List<string>();
                StringCollection strcol = Clipboard.GetFileDropList();
                foreach (string file in strcol)
                {
                    if (Directory.Exists(file)) from.Add(file);
                    else if (File.Exists(file)) from.Add(file);
                }

                int byt = ((MemoryStream)Clipboard.GetData("Preferred DropEffect")).ReadByte();
                if (byt == 2) //cut            
                {
                    LocalDirectory.MoveFilesOrFolders(from.ToArray(), path);
                }
                else if (byt == 5) //copy
                {
                    LocalDirectory.CopyFilesOrFolders(from.ToArray(), path);
                }

                Clipboard.Clear();
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "CodesBrowser|pasteToolStripMenuItem_Click");
            }
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

        private void importOldCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Import old codes";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                ImportOldCodes(new object[] { true, fbd.SelectedPath });
            }
        }

        #endregion
    }
}

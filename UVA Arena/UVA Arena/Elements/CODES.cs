using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using BrightIdeasSoftware;

namespace UVA_Arena.Elements
{
    public partial class CODES : UserControl
    {
        public CODES()
        {
            InitializeComponent();
            AddDelegatesToTreeView();
            LoadCodeFolder();
        }

        void AddDelegatesToTreeView()
        {
            folderTree.CanExpandGetter = delegate(object row)
            {
                if (row.GetType() == typeof(FileInfo)) return false;
                if (((DirectoryInfo)row).GetDirectories().Length > 0) return true;
                if (((DirectoryInfo)row).GetFiles().Length > 0) return true;
                return false;
            };
            folderTree.ChildrenGetter = delegate(object row)
            {
                if (row == null) return null;
                if (row.GetType() == typeof(FileInfo)) return null;
                List<FileSystemInfo> list = new List<FileSystemInfo>();
                list.AddRange(((DirectoryInfo)row).GetDirectories());
                list.AddRange(((DirectoryInfo)row).GetFiles());
                return list;
            };
            lengthTAB.AspectGetter = delegate(object row)
            {
                if (row == null) return null;
                if (row.GetType() != typeof(FileInfo)) return "";
                return Functions.FormatMemory(((FileInfo)row).Length);
            };
            nameTAB.ImageGetter = delegate(object row)
            {
                if (row == null) return null;
                if (row.GetType() == typeof(DirectoryInfo)) return "folder";
                string ext = ((FileInfo)row).Extension;
                if (smallImageList1.Images.ContainsKey(ext)) return ext;
                Icon ic = Icon.ExtractAssociatedIcon(((FileInfo)row).FullName);
                smallImageList1.Images.Add(ext, ic);
                largeImageList1.Images.Add(ext, ic);
                return ext;
            };
        }

        public void LoadCodeFolder()
        {
            folderTree.SuspendLayout();
            DirectoryInfo[] fsi = { new DirectoryInfo(RegistryAccess.CodesPath) };
            folderTree.SetObjects(fsi);
            folderTree.ExpandAll();
            folderTree.ResumeLayout(true);
        }

        private void selectPathButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select a path to save code files.";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                RegistryAccess.CodesPath = fbd.SelectedPath;
                LoadCodeFolder();
            }
        }
        private void searchBox1_SearchButtonClicked(object sender, EventArgs e)
        {
            string txt = searchBox1.SearchText.Trim();
            TextMatchFilter filter = null;
            if (!string.IsNullOrEmpty(txt))
                filter = TextMatchFilter.Contains(folderTree, txt);
            if (filter == null) folderTree.DefaultRenderer = null;
            else folderTree.DefaultRenderer = new HighlightTextRenderer(filter);
            folderTree.AdditionalFilter = filter;
        }

        private void folderTree_CellEditValidating(object sender, CellEditEventArgs e)
        {
            e.Cancel = !LocalDirectory.IsValidFileName((string)e.NewValue);
        }

        private void folderTree_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            e.Cancel = true;
            if (e.Cancel) return;
        }
    }
}

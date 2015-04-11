using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UVA_Arena.Structures;

namespace UVA_Arena
{
    public partial class CategoryChange : Form
    {
        public CategoryChange(ProblemInfo pinfo)
        {
            InitializeComponent();

            this.problem = pinfo;
            if (pinfo == null)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.Close();
                return;
            }

            LoadAllCategory();
            nativeTreeView1.PathSeparator = CategoryNode.SEPARATOR.ToString();
        }

        private ProblemInfo problem = null;
        private string root = LocalDatabase.NodeRoot + CategoryNode.SEPARATOR;
        private string lead = LocalDatabase.CatRoot + CategoryNode.SEPARATOR;

        private void LoadAllCategory()
        {
            foreach (string path in problem.categories)
            {
                string tmp = path.Substring(lead.Length + root.Length);
                string[] parts = tmp.Split(new char[] { CategoryNode.SEPARATOR },
                    StringSplitOptions.RemoveEmptyEntries);

                TreeNodeCollection col = nativeTreeView1.Nodes;
                foreach (string part in parts)
                {
                    col = col.Add(part).Nodes;
                }
            }

            nativeTreeView1.ExpandAll();
        }

        private void SaveAllCategory(TreeNode cur = null)
        {
            TreeNodeCollection col = null;
            if (cur == null)
                col = nativeTreeView1.Nodes;
            else if (cur.Nodes.Count > 0)
                col = cur.Nodes;

            if (col != null)
            {
                foreach (TreeNode tn in col)
                {
                    SaveAllCategory(tn);
                }
            }
            else
            {
                LocalDatabase.category_root[LocalDatabase.CatRoot][cur.FullPath].AddProblem(problem);
            }
        }

        private void RemoveEmptyNodes(CategoryNode cur)
        {
            for (int i = 0; i < cur.Nodes.Count; ++i)
            {
                CategoryNode c = cur.Nodes[i];
                if (c.Problems.Count == 0)
                {
                    c.Delete();
                    --i;
                }
                else
                {
                    RemoveEmptyNodes(c);
                }
            }
        }

        private void reload_All_Click(object sender, EventArgs e)
        {
            LoadAllCategory();
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            foreach (string cat in problem.categories)
            {
                string path = cat.Substring(root.Length);
                LocalDatabase.category_root[path].RemoveProblem(problem);
            }

            problem.categories.Clear();
            SaveAllCategory();

            RemoveEmptyNodes(LocalDatabase.category_root);
            RegistryAccess.SetTags(problem.pnum, problem.categories);

            LocalDatabase.LoadCategories();
            Interactivity.problems.LoadCategory();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cancel_Button_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void nativeTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool enable = (nativeTreeView1.SelectedNode != null);
            addChild1.Enabled = enable;
            addChild2.Enabled = enable;
            renameNode1.Enabled = enable;
            renameNode2.Enabled = enable;
            removeNode1.Enabled = enable;
            removeNode2.Enabled = enable;
        }

        private void nativeTreeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.CancelEdit = (string.IsNullOrEmpty(e.Label) ||
                e.Label.Contains(CategoryNode.SEPARATOR.ToString()));
        }

        private void addNode1_Click(object sender, EventArgs e)
        {
            TreeNode nod = nativeTreeView1.SelectedNode;
            if (nod == null || nod.Parent == null)
                nod = nativeTreeView1.Nodes.Add("New Node");
            else
                nod = nod.Parent.Nodes.Add("New Node");
            nod.BeginEdit();
        }

        private void renameNode1_Click(object sender, EventArgs e)
        {
            TreeNode nod = nativeTreeView1.SelectedNode;
            if (nod != null) nod.BeginEdit();
        }

        private void addChild1_Click(object sender, EventArgs e)
        {
            TreeNode nod = nativeTreeView1.SelectedNode;
            if (nod != null)
            {
                TreeNode tn = nod.Nodes.Add("New Node");
                nod.Expand();
                tn.BeginEdit();
            }
        }

        private void removeNode1_Click(object sender, EventArgs e)
        {
            TreeNode nod = nativeTreeView1.SelectedNode;
            if (nod != null) nod.Remove();
        }

    }
}

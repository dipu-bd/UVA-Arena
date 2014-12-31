using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
        }
        
        private void CategoryChange_Load(object sender, EventArgs e)
        {
            if (problem.tags == null)
            {
                problem.tags = RegistryAccess.GetTags(problem.pnum);
            }            

            listView1.Items.Clear();
            foreach (string itm in problem.tags)
            {
                listView1.Items.Add(new ListViewItem(itm));
            }

            List<string> category = new List<string>();
            var it = LocalDatabase.problem_cat.GetEnumerator();
            while (it.MoveNext()) category.Add(it.Current.Key);
            textBox1.Items.AddRange(category.ToArray());

        }

        private ProblemInfo problem = null;

        private void button1_Click(object sender, EventArgs e)
        {
            string txt = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(txt)) return;
            if (problem.tags.Contains(txt)) return;
            listView1.Items.Add(new ListViewItem(txt));
            problem.tags.Add(txt);
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.PerformClick();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
                listView1.FocusedItem.BeginEdit();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                foreach (ListViewItem lvi in listView1.SelectedItems)
                {
                    problem.tags.Remove(lvi.Text);
                    lvi.Remove();
                }
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label.Trim().Length < 2)
            {
                e.CancelEdit = true;
            }
            else
            {
                string old = listView1.Items[e.Item].Text;
                int indx = problem.tags.IndexOf(old);
                problem.tags[indx] = e.Label;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            problem.tags.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            problem.tags = RegistryAccess.GetTags(problem.pnum);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
            System.GC.Collect();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> old = RegistryAccess.GetTags(problem.pnum);
            try
            { 
                //remove old tags
                foreach (string tag in old) LocalDatabase.GetCategory(tag).Remove(problem);
                //add new tags
                RegistryAccess.SetTags(problem.pnum, problem.tags);
                foreach (string tag in problem.tags) LocalDatabase.GetCategory(tag).Add(problem);
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                if (Interactivity.problems.categoryButton.Checked)
                {
                    Interactivity.problems.LoadCategory();
                    Interactivity.problems.problemListView.Refresh();
                }
            }
            catch (Exception ex)
            {
                Logger.Add(ex.Message, "Category Change");   

                problem.tags = old;
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }

            this.Close();
            System.GC.Collect();
        }

    }
}

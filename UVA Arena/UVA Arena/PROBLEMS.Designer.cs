namespace UVA_Arena
{
    partial class PROBLEMS
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.customSplitContainer1 = new System.Windows.Forms.CustomSplitContainer();
            this.problemListViewer1 = new UVA_Arena.ProblemListViewer();
            this.problemDescriptionViewer1 = new UVA_Arena.ProblemDescriptionViewer();
            this.customSplitContainer1.Panel1.SuspendLayout();
            this.customSplitContainer1.Panel2.SuspendLayout();
            this.customSplitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customSplitContainer1
            // 
            this.customSplitContainer1.BackColor = System.Drawing.Color.LightBlue;
            this.customSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.customSplitContainer1.Name = "customSplitContainer1";
            // 
            // customSplitContainer1.Panel1
            // 
            this.customSplitContainer1.Panel1.Controls.Add(this.problemListViewer1);
            // 
            // customSplitContainer1.Panel2
            // 
            this.customSplitContainer1.Panel2.Controls.Add(this.problemDescriptionViewer1);
            this.customSplitContainer1.Size = new System.Drawing.Size(737, 409);
            this.customSplitContainer1.SplitterDistance = 275;
            this.customSplitContainer1.TabIndex = 0;
            // 
            // problemListViewer1
            // 
            this.problemListViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemListViewer1.Location = new System.Drawing.Point(0, 0);
            this.problemListViewer1.Name = "problemListViewer1";
            this.problemListViewer1.Size = new System.Drawing.Size(275, 409);
            this.problemListViewer1.TabIndex = 0;
            // 
            // problemDescriptionViewer1
            // 
            this.problemDescriptionViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemDescriptionViewer1.Location = new System.Drawing.Point(0, 0);
            this.problemDescriptionViewer1.Name = "problemDescriptionViewer1";
            this.problemDescriptionViewer1.Size = new System.Drawing.Size(458, 409);
            this.problemDescriptionViewer1.TabIndex = 0;
            // 
            // PROBLEMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customSplitContainer1);
            this.Name = "PROBLEMS";
            this.Size = new System.Drawing.Size(737, 409);
            this.customSplitContainer1.Panel1.ResumeLayout(false);
            this.customSplitContainer1.Panel2.ResumeLayout(false);
            this.customSplitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CustomSplitContainer customSplitContainer1;
        private ProblemListViewer problemListViewer1;
        private ProblemDescriptionViewer problemDescriptionViewer1;
    }
}

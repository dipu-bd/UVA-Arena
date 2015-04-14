using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;
using UVA_Arena;

namespace System.Windows.Forms
{
    /// <summary>
    /// Custom made owner drawn Tree View control
    /// </summary>
    [DefaultProperty("Nodes")]
    [Description("Custom made owner drawn Tree View control")]
    public class CustomTreeView : NativeTreeView
    {
        /// <summary>
        /// Instanciate new custom tree view control
        /// </summary>
        public CustomTreeView()
        {
            this.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.ForeColor = System.Drawing.Color.Black;
            this.ShowLines = false;
            this.ShowNodeToolTips = true;
            this.ShowPlusMinus = true;
            this.ShowRootLines = false;
            this.ItemHeight = 24;

            this.TopNodeBorder = Color.FromArgb(206, 212, 223);
            this.SelectedTopBorder = Color.FromArgb(196, 210, 223);
            this.SecondaryNodeBorder = Color.FromArgb(210, 216, 228);
            this.SelectedSecondaryBorder = Color.FromArgb(229, 195, 120);
            this.TopNodeBackgroud = Color.FromArgb(240, 240, 240);
            this.SelectedTopBackground = Color.FromArgb(235, 230, 240);
            this.SecondaryNodeBackgroud = Color.FromArgb(255, 240, 210);
            this.SelectedSecondaryBackground = Color.FromArgb(250, 229, 190);

            //load state images            
            this.StateImageList = new ImageList();
            this.StateImageList.ColorDepth = ColorDepth.Depth32Bit;
            this.StateImageList.Images.Add("blank", UALib.Properties.Resources.blank);
            this.StateImageList.Images.Add("collapse", UALib.Properties.Resources.collapse);
            this.StateImageList.Images.Add("collapse_focused", UALib.Properties.Resources.collapse_focused);
            this.StateImageList.Images.Add("expand", UALib.Properties.Resources.expand);
            this.StateImageList.Images.Add("expand_focused", UALib.Properties.Resources.expand_focused);

            //set other styles 
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        /// <summary>
        /// Border color for the top nodes
        /// </summary>
        [Editor(typeof(Pen), typeof(Pen)), Category("Appearance")]
        [Description("Border color for the top nodes")]
        public Color TopNodeBorder { get; set; }

        /// <summary>
        /// Border color for the selected top nodes
        /// </summary>
        [Category("Appearance")]
        [Description("Border color for the selected top nodes")]
        public Color SelectedTopBorder { get; set; }

        /// <summary>
        /// Border color for the second level nodes
        /// </summary>
        [Category("Appearance")]
        [Description("Border color for the second level nodes")]
        public Color SelectedSecondaryBorder { get; set; }

        /// <summary>
        /// Border color for the second level nodes
        /// </summary>
        [Category("Appearance")]
        [Description("Border color for the second level nodes")]
        public Color SecondaryNodeBorder { get; set; }

        /// <summary>
        /// GradientStyle brush generator for TopNode's normal state background
        /// </summary> 
        [Category("Appearance")]
        [Description("GradientStyle brush generator for TopNode's normal state background")]
        public Color TopNodeBackgroud { get; set; }

        /// <summary>
        /// GradientStyle brush generator for TopNode's selected state background 
        /// </summary>         
        [Category("Appearance")]
        [Description("GradientStyle brush generator for TopNode's selected state background")]

        public Color SelectedTopBackground { get; set; }
        /// <summary>
        /// GradientStyle brush generator for secondary node's  normal state background
        /// </summary> 
        [Category("Appearance")]
        [Description("GradientStyle brush generator for secondary node's normal state background")]
        public Color SecondaryNodeBackgroud { get; set; }

        /// <summary>
        /// GradientStyle brush generator for secondary node's selected state background 
        /// </summary>         
        [Category("Appearance")]
        [Description("GradientStyle brush generator for secondary node's selected state background")]
        public Color SelectedSecondaryBackground { get; set; }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            if (e.Button == Forms.MouseButtons.Left)
            {
                if (e.Node.Level <= 1)
                {
                    e.Node.Expand();
                }
                else
                {
                    e.Node.Toggle();
                }
            }
            this.SelectedNode = e.Node;
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            base.OnBeforeExpand(e);

            foreach (TreeNode t in this.Nodes)
            {
                if (t.IsExpanded)
                {
                    t.Collapse();
                    break;
                }
            }
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            base.OnDrawNode(e);
            if (e.Node.Level > 1)
            {
                e.DrawDefault = true;
                return;
            }

            if (this.IsDisposed) return;

            try
            {
                Image buffer = new Bitmap(e.Bounds.Width, e.Bounds.Height);
                Graphics g = Graphics.FromImage(buffer);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle bound = e.Bounds;
                bound.X = 0;
                bound.Y = 0;
                var custom = new DrawTreeNodeEventArgs(g, e.Node, bound, e.State);

                try
                {
                    DrawBackground(custom);
                    DrawText(custom);
                    DrawStateImage(custom);
                    DrawImage(custom);
                }
                catch { }

                e.Graphics.DrawImageUnscaled(buffer, e.Bounds.X, e.Bounds.Y);
            }
            catch { }
        }

        private void DrawBackground(DrawTreeNodeEventArgs e)
        {
            //get rectangle to fill
            Rectangle bound = e.Bounds;
            bound.Inflate(-1, -1);
            bound.Width -= 1;

            //get pen and brush
            Pen pen = new Pen(Color.FromArgb(229, 195, 120), 1);
            Brush brush = new SolidBrush(Color.FromArgb(255, 239, 200));
            if (e.Node.Level == 0)
            {
                if (e.Node.IsSelected)
                {
                    pen = new Pen(SelectedTopBorder, 1);
                    brush = new SolidBrush(SelectedTopBackground);
                }
                else
                {
                    pen = new Pen(TopNodeBorder, 1);
                    brush = new SolidBrush(TopNodeBackgroud);
                }
            }
            else
            {
                bound.X += this.Indent;
                bound.Width -= this.Indent;
                if (e.Node.IsSelected)
                {
                    pen = new Pen(SelectedSecondaryBorder, 1);
                    brush = new SolidBrush(SelectedSecondaryBackground);
                }
                else
                {
                    pen = new Pen(SecondaryNodeBorder, 1);
                    brush = new SolidBrush(SecondaryNodeBackgroud);
                }
            }

            //fill rectangle
            e.Graphics.FillRectangle(brush, bound);  
            e.Graphics.DrawRectangle(pen, bound);
        }

        private void DrawText(DrawTreeNodeEventArgs e)
        {
            //draw text
            Rectangle nodBound = e.Bounds;
            nodBound.X = e.Node.Bounds.X;
            nodBound.Y += (e.Bounds.Height - this.Font.Height) / 2;
            e.Graphics.DrawString(e.Node.Text, this.Font, new SolidBrush(this.ForeColor), nodBound);
        }

        private void DrawImage(DrawTreeNodeEventArgs e)
        {
            //get image
            if (this.ImageList == null)
            {
                return;
            }
            Image img = null;
            if (e.Node.IsSelected)
            {
                if (!string.IsNullOrEmpty(e.Node.SelectedImageKey))
                {
                    img = this.ImageList.Images[e.Node.SelectedImageKey];
                }
                else if (e.Node.SelectedImageIndex >= 0)
                {
                    img = this.ImageList.Images[e.Node.SelectedImageIndex];
                }
            }
            
            if(!e.Node.IsSelected || img == null)
            {
                if (!string.IsNullOrEmpty(e.Node.ImageKey))
                {
                    img = this.ImageList.Images[e.Node.ImageKey];
                }
                else if (e.Node.ImageIndex >= 0)
                {
                    img = this.ImageList.Images[e.Node.ImageIndex];
                }
            }
            if (img == null)
            {
                return;
            }

            //draw image
            Rectangle imgBound = e.Bounds;
            imgBound.X = e.Node.Bounds.X - img.Width - 2;
            imgBound.Y += (imgBound.Height - img.Height) / 2;
            e.Graphics.DrawImageUnscaled(img, imgBound);
        }

        private void DrawStateImage(DrawTreeNodeEventArgs e)
        {
            //get state image
            if (this.StateImageList == null) return;
            if (e.Node.StateImageIndex == -1)
            {
                e.Node.StateImageIndex = 0;
                return;
            }

            int index = 0;
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.IsExpanded)
                {
                    index = e.Node.IsSelected ? 4 : 3;
                }
                else
                {
                    index = e.Node.IsSelected ? 2 : 1;
                }
            }
            Image img = this.StateImageList.Images[index];
            if (img == null) return;

            //draw image
            Rectangle imgBound = e.Bounds;
            imgBound.Y += (imgBound.Height - img.Height) / 2;
            imgBound.X = e.Node.Bounds.X - img.Width - 2;
            if (this.ImageList != null)
            {
                imgBound.X -= this.ImageList.ImageSize.Width;
            }

            e.Graphics.DrawImageUnscaled(img, imgBound);
        }
    }
}

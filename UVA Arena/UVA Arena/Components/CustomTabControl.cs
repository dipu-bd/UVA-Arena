using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using UVA_Arena;

namespace System.Windows.Forms
{
    /// <summary>
    /// OwnerDrawn custom made enhanced tab control
    /// Owner Draw: http://www.codeproject.com/Articles/91387/Painting-Your-Own-Tabs-Second-Edition
    /// Draggable Tab: http://dotnetrix.co.uk/tabcontrol.htm#tip7
    /// </summary> 
    [DefaultProperty("TabPages")]
    public class CustomTabControl : TabControl
    {
        private TabPage _DragTab = null;
        private Point DragStartPosition = Point.Empty;
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="System.Windows.Forms.CustomTabControl"/> class.
        /// </summary>
        public CustomTabControl()
        {
            components = new System.ComponentModel.Container();

            this.AllowTabRedorder = true;
            this.BackColor = Color.PaleTurquoise;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);


        }

        /// <summary>
        /// Realease / Dispose all resources used by this control.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }





        /// <summary>
        /// Gets or sets how much overlapping should occur with the previous tab
        /// </summary>
        [Category("Custom"), DefaultValue(0)]
        public int Overlap { get; set; }

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        [Category("Custom"), DefaultValue(typeof(Color), "PaleTurquoise")]
        new public Color BackColor { get; set; }

        /// <summary>
        /// Gets or sets the selected font style.
        /// </summary>
        /// <value>The selected font style.</value>
        [Category("Custom"), DefaultValue(FontStyle.Regular)]
        public FontStyle SelectedFontStyle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="System.Windows.Forms.CustomTabControl"/> allow tab redorder.
        /// It is not supported on non-windows platforms.
        /// </summary>
        /// <value><c>true</c> if allow tab redorder; otherwise, <c>false</c>.</value>
        [Category("Custom"), DefaultValue(false)]
        [Description("Not supported on non-windows platforms")]
        public bool AllowTabRedorder { get; set; }





        /// <summary>
        /// Raises the selected event.
        /// </summary>
        /// <param name="e">Drag Event Args</param>
        protected override void OnSelected(TabControlEventArgs e)
        {
            base.OnSelected(e);
            this.Invalidate();
        }

        /// <summary>
        /// Raises the mouse down event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            DragStartPosition = e.Location;
            _DragTab = GetHoveredTab(e.Location);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _DragTab = null;
            this.Cursor = Cursors.Default;
            DragStartPosition = Point.Empty;
        }

        /// <summary>
        /// Raises the mouse move event.
        /// </summary>
        /// <param name="e">Drag Event Args</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!AllowTabRedorder || _DragTab == null ||
                e.Button != MouseButtons.Left) return;

            //do drag drop when mouse moved a fix amount of distance away from the origin
            int xd = Math.Abs(e.X - DragStartPosition.X);
            if (xd >= SystemInformation.DragSize.Width)
            {
                this.Cursor = Cursors.NoMoveHoriz;

                TabPage cur_tab = GetHoveredTab(e.Location);
                if (cur_tab == null || _DragTab == cur_tab) return;

                //swap tabs
                int Index1 = this.TabPages.IndexOf(_DragTab);
                int Index2 = this.TabPages.IndexOf(cur_tab);
                this.TabPages[Index1] = cur_tab;
                this.TabPages[Index2] = _DragTab;
                this.SelectedTab = _DragTab;
                DragStartPosition = e.Location;
            }

        }

        /// <summary>
        /// Overrides the paint method of TabControl to custom draw the tab page
        /// </summary>
        /// <param name="e">Pain Event Arg passed by the invoker </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //draw on buffer image first to reduce flickering
            Bitmap BufferImage = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(BufferImage);

            graphics.SmoothingMode = SmoothingMode.HighQuality;

            //paint background with back color
            graphics.Clear(this.BackColor);

            //draw tab buttons
            if (this.TabCount > 0)
            {
                //currently selected tab button should be drawn later 
                // so that it can appear on top
                for (int index = this.TabCount - 1; index >= 0; --index)
                {
                    if (index != this.SelectedIndex)
                    {
                        DrawTabPage(index, graphics);
                    }
                }
                if (this.SelectedIndex != -1)
                {
                    DrawTabPage(this.SelectedIndex, graphics);
                }
            }

            //draw buffer image on screen
            graphics.Flush();
            e.Graphics.DrawImageUnscaled(BufferImage, 0, 0);

            //dispose
            graphics.Dispose();
            BufferImage.Dispose();
        }





        /// <summary>
        /// Draw a tab and all of its contents
        /// </summary>
        /// <param name="index">Index of tab page needed to be drawn</param>
        /// <param name="graphics">Graphics in which to draw </param>
        private void DrawTabPage(int index, Graphics graphics)
        {
            //gets if the current tab page has mouse focus
            bool mouseFocus = false;
            Rectangle mouse = new Rectangle(MousePosition.X, MousePosition.Y, 1, 1);
            if (RectangleToScreen(GetTabRect(index)).IntersectsWith(mouse))
            {
                mouseFocus = true;
            }

            //get brush for tab button 
            Pen borderPen = null;
            Brush fillbrush = null;
            if (this.SelectedIndex == index)
            {
                borderPen = new Pen(Color.FromArgb(147, 177, 205));
                Color fore = Color.FromArgb(215, 210, 230);
                Color back = Color.FromArgb(235, 220, 240);
                Stylish.GradientStyle style = new Stylish.GradientStyle(HatchStyle.DottedGrid, fore, back);
                fillbrush = style.GetBrush();
            }
            else
            {
                borderPen = new Pen(Color.FromArgb(167, 197, 235));
                Color up = Color.FromArgb(215, 208, 230);
                Color down = Color.FromArgb(246, 242, 252);
                if (mouseFocus)
                {
                    up = Color.FromArgb(225, 219, 242);
                    down = Color.FromArgb(236, 238, 248);
                }
                Stylish.GradientStyle style = new Stylish.GradientStyle(up, down, 90F);
                Rectangle tabBounds = GetTabRectAdjusted(index);
                fillbrush = style.GetBrush(tabBounds.Width, tabBounds.Height + 1);
            }

            //draw tab button back color
            GraphicsPath path = GetTabPageBorderAdjusted(index);
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            //draw tab button and text and image on it 
            graphics.FillPath(fillbrush, path); //tab button
            this.DrawTabTextAndImage(index, graphics); //tab text and Image
            graphics.DrawPath(borderPen, path); // tab border

            path.Dispose();
            fillbrush.Dispose();
            borderPen.Dispose();
        }

        /// <summary>
        /// Draw text and image of the tab page
        /// </summary>
        /// <param name="index">Index of the tab to draw</param>
        /// <param name="graphics">Graphics in which to draw</param>
        private void DrawTabTextAndImage(int index, Graphics graphics)
        {
            //get the font
            Font f = this.Font;
            if (this.SelectedIndex == index)
            {
                f = new Font(f, SelectedFontStyle);
            }

            //get the text size after drawn to screen
            string measureString = TabPages[index].Text;
            SizeF stringSize = graphics.MeasureString(measureString, f);

            //get the rectangle to draw text
            Rectangle tabBounds = this.GetTabRectAdjusted(index);
            tabBounds.X += (int)(tabBounds.Width - stringSize.Width) / 2;
            tabBounds.Y += (int)(tabBounds.Height - stringSize.Height) / 2;
            if (this.ImageList != null && (this.TabPages[index].ImageIndex != -1
                || !string.IsNullOrEmpty(this.TabPages[index].ImageKey)))
            {
                int extra = (int)(this.ImageList.ImageSize.Width * 1.5);
                tabBounds.X += extra;
            }

            //get other tab bound to enable glow
            Rectangle tabBounds2 = tabBounds;
            tabBounds2.X += 1;
            tabBounds2.Y += 1;
            Rectangle tabBounds3 = tabBounds;
            tabBounds3.X -= 1;
            tabBounds3.Y -= 1;

            //draw text on the tab
            graphics.DrawString(this.TabPages[index].Text, f, Brushes.LightBlue, tabBounds2); //shadow
            graphics.DrawString(this.TabPages[index].Text, f, Brushes.PaleTurquoise, tabBounds3); //shadow
            graphics.DrawString(this.TabPages[index].Text, f, Brushes.Black, tabBounds); //main

            //get the image to draw
            if (this.ImageList == null) return;
            Image tabImage = null;
            if (this.TabPages[index].ImageIndex != -1)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageIndex];
            }
            else if (!string.IsNullOrEmpty(this.TabPages[index].ImageKey))
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageKey];
            }
            if (tabImage == null) return;

            //get the rectangle to draw text
            Rectangle imgRect = this.GetTabRect(index);
            imgRect.X = tabBounds.X - tabImage.Width - 5;
            imgRect.Y += (imgRect.Height - tabImage.Height) / 2;
            imgRect.Width = tabImage.Width + 2;
            imgRect.Height = tabImage.Height + 2;

            //draw image
            graphics.DrawImageUnscaled(tabImage, imgRect);
            tabImage.Dispose();

        }

        /// <summary>
        /// Get adjusted tab rectangle area with overlapping to draw a tabpage
        /// </summary>
        /// <param name="index">Index of the tab</param>
        /// <returns>Adjusted Tab Rectagle</returns>
        Rectangle GetTabRectAdjusted(int index)
        {
            Rectangle tabBounds = this.GetTabRect(index);

            tabBounds.Height += 1;
            if (index == 0)
            {
                tabBounds.X += 1;
                tabBounds.Width -= 1;
            }
            else
            {
                tabBounds.X -= this.Overlap;
                tabBounds.Width += this.Overlap;
            }

            return tabBounds;
        }

        /// <summary>
        /// Get the adjusted border of a tab page
        /// </summary>
        /// <param name="index">Index of the tab page</param>
        /// <returns>Graphics path representing the border of the whole tabpage</returns>
        GraphicsPath GetTabPageBorderAdjusted(int index)
        {
            GraphicsPath path = new GraphicsPath();

            Rectangle tabBounds = this.GetTabRectAdjusted(index);
            Rectangle pageBounds = this.TabPages[index].Bounds;
            pageBounds.X -= 1;
            pageBounds.Y -= 1;
            pageBounds.Width += 3;
            pageBounds.Height += 2;

            //---> google chrome style tab border <---
            // bottom left of tab up the leading slope
            path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + tabBounds.Height - 4, tabBounds.Y + 2);
            // along the top, leaving a gap that is auto completed for us
            path.AddLine(tabBounds.X + tabBounds.Height, tabBounds.Y, tabBounds.Right - 3, tabBounds.Y);
            // round the top right corner
            path.AddArc(tabBounds.Right - 6, tabBounds.Y, 6, 6, 270, 90);
            // back down the right end
            path.AddLine(tabBounds.Right, tabBounds.Y + 3, tabBounds.Right, tabBounds.Bottom);
            // no need to complete the figure as this path joins into the border for the entire tab            

            //add page border to the path
            path.AddRectangle(pageBounds);

            path.CloseFigure();
            return path;
        }


        /// <summary>
        /// Gets the hovered tab / tab which is currently under the mouse pointer 
        /// </summary> 
        /// <param name="pos">Position of the mouse pointer in client coordinate</param>
        /// <returns>Hovered TabPage, or null if none exist</returns>
        public TabPage GetHoveredTab(Point pos)
        {
            for (int index = 0; index < this.TabCount; index++)
            {
                if (this.GetTabRect(index).Contains(pos))
                {
                    return this.TabPages[index];
                }
            }
            return null;
        }

    }
}
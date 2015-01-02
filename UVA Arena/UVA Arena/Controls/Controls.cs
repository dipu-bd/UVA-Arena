using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using UVA_Arena;

namespace System.Windows.Forms
{
    #region CueTextBox : Show a message cue when textbox text is empty

    public class CueTextBox : TextBox
    {
        public CueTextBox() { }

        private string _cueText;
        public string CueText
        {
            get { return _cueText; }
            set
            {
                _cueText = value;

                IntPtr lparam = new IntPtr(1);
                IntPtr wparam = Marshal.StringToBSTR(value);
                NativeMethods.SendMessage(this.Handle, NativeMethods.EM_SETCUEBANNER, lparam, wparam);
                Marshal.FreeCoTaskMem(lparam);
                Marshal.FreeBSTR(wparam);
            }
        }
    }

    #endregion

    #region CustomSplitContainer : Supports contents movement on splitter move

    public class CustomSplitContainer : SplitContainer
    {
        public CustomSplitContainer()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private Point _initial;
        private DateTime _lastMove;
        private bool _moving = false;

        private bool _MoveSplitter(int diff, int maxsiz)
        {
            if (Math.Abs(diff) == 0) return false;
            if ((DateTime.Now - _lastMove).TotalMilliseconds < 50) return false;

            int newdis = SplitterDistance + diff;
            if (newdis < 0 || newdis > maxsiz) return false;

            SplitterDistance = newdis;
            _lastMove = DateTime.Now;

            return true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!IsSplitterFixed)
            {
                _moving = true;
                _lastMove = DateTime.Now;
                _initial = e.Location;
                IsSplitterFixed = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_moving)
            {
                _moving = false;
                IsSplitterFixed = false;
            }
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_moving)
            {
                if (Orientation == Forms.Orientation.Vertical)
                {
                    if (_MoveSplitter(e.X - _initial.X, Width))
                        _initial = e.Location;
                }
                else
                {
                    if (_MoveSplitter(e.Y - _initial.Y, Height))
                        _initial = e.Location;
                }
            }
            base.OnMouseMove(e);
        }
    }

    #endregion

    #region NativeTreeView : Use windows style on TreeView

    public class NativeTreeView : TreeView
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            IntPtr lparam = Marshal.StringToBSTR("explorer");
            NativeMethods.SetWindowTheme(this.Handle, lparam, IntPtr.Zero);
        }
    }

    #endregion

    #region NativeListView : Use windows style on TreeView

    public class NativeListView : ListView
    {
        protected override void CreateHandle()
        {
            base.CreateHandle();
            IntPtr lparam = Marshal.StringToBSTR("explorer");
            NativeMethods.SetWindowTheme(this.Handle, lparam, IntPtr.Zero);
        }
    }

    #endregion

    #region CustomTabControl : Nice looking, user painted tab control

    public class CustomTabControl : TabControl
    {
        //
        // Contructor and Properties
        //
        public CustomTabControl()
        {
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        [Category("Appearance"), DefaultValue(0)]
        public int Overlap { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "Transparent")]
        new public Color BackColor { get; set; }

        //
        // Events
        //

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == Forms.MouseButtons.Left)
            {
                NativeMethods.MoveWithMouse(Interactivity.mainForm.Handle);
            }
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSelectedIndexChanged(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == NativeMethods.WM_HSCROLL) this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //draw on buffer image first to recudce flickering
            Bitmap BufferImage = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(BufferImage);

            graphics.SmoothingMode = SmoothingMode.HighQuality;

            //paint background with backcolor
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

        //
        // Draw TabPage
        //
        private void DrawTabPage(int index, Graphics graphics)
        {
            //get brush for tab button 
            Pen borderPen = null;
            Brush fillbrush = null;
            if (this.SelectedIndex == index)
            {
                borderPen = new Pen(Color.FromArgb(147, 177, 205));
                Color fore = Color.FromArgb(217, 220, 235);
                Color back = Color.FromArgb(242, 246, 252);
                Stylish.GradientStyle style = new Stylish.GradientStyle(HatchStyle.Shingle, fore, back);
                fillbrush = style.GetBrush();
            }
            else
            {
                borderPen = new Pen(Color.FromArgb(167, 197, 235));
                Color up = Color.FromArgb(215, 208, 230);
                Color down = Color.FromArgb(246, 242, 252);
                Stylish.GradientStyle style = new Stylish.GradientStyle(up, down, LinearGradientMode.Vertical);
                Rectangle tabBounds = GetTabRectAdjusted(index);
                fillbrush = style.GetBrush(tabBounds.Width, tabBounds.Height + 1);
            }

            //draw tab button back color
            GraphicsPath path = GetTabPageBorder(index);
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            //draw tab button and text and image on it 
            graphics.FillPath(fillbrush, path); //tab button
            this.DrawTabText(index, graphics); //tab text
            this.DrawTabImage(index, graphics); //tab image
            graphics.DrawPath(borderPen, path); // tab border

            path.Dispose();
            fillbrush.Dispose();
            borderPen.Dispose();
        }

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

        GraphicsPath GetTabPageBorder(int index)
        {
            GraphicsPath path = new GraphicsPath();

            Rectangle tabBounds = this.GetTabRectAdjusted(index);
            Rectangle pageBounds = this.TabPages[index].Bounds;
            pageBounds.X -= 1;
            pageBounds.Y -= 1;
            pageBounds.Width += 3;
            pageBounds.Height += 2;

            //--> google chrome style tab border
            // bottom left of tab up the leading slope
            path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + tabBounds.Height - 4, tabBounds.Y + 2);
            // along the top, leaving a gap that is auto completed for us
            path.AddLine(tabBounds.X + tabBounds.Height, tabBounds.Y, tabBounds.Right - 3, tabBounds.Y);
            // round the top right corner
            path.AddArc(tabBounds.Right - 6, tabBounds.Y, 6, 6, 270, 90);
            // back down the right end
            path.AddLine(tabBounds.Right, tabBounds.Y + 3, tabBounds.Right, tabBounds.Bottom);
            // no need to complete the figure as this path joins into the border for the entire tab
            /* Courtesy : http://www.codeproject.com/Articles/91387/Painting-Your-Own-Tabs-Second-Edition */

            //add page border
            path.AddRectangle(pageBounds);

            path.CloseFigure();
            return path;
        }

        private void DrawTabText(int index, Graphics graphics)
        {
            Rectangle tabBounds = this.GetTabRect(index);

            tabBounds.X += 16 + this.Padding.X;
            tabBounds.Width -= 16 + this.Padding.X;
            tabBounds.Y += (int)(tabBounds.Height - this.Font.Height) / 2;

            if (this.ImageList != null && (this.TabPages[index].ImageIndex != -1
                || !String.IsNullOrEmpty(this.TabPages[index].ImageKey)))
            {
                int extra = (int)(this.ImageList.ImageSize.Width * 1.5);
                tabBounds.X += extra;
                tabBounds.Width -= extra;
            }

            graphics.DrawString(this.TabPages[index].Text, this.Font, Brushes.Black, tabBounds);
        }

        private void DrawTabImage(int index, Graphics graphics)
        {
            if (this.ImageList == null) return;

            Image tabImage = null;
            int imgidx = this.TabPages[index].ImageIndex;
            String imgkey = this.TabPages[index].ImageKey;

            if (imgidx >= 0) tabImage = this.ImageList.Images[imgidx];
            else if (!string.IsNullOrEmpty(imgkey)) tabImage = this.ImageList.Images[imgkey];

            if (tabImage == null) return;

            Rectangle imgRect = this.GetTabRect(index);
            imgRect.X += 20 + this.Padding.X;
            imgRect.Y = imgRect.Y + (imgRect.Height - tabImage.Height) / 2;
            imgRect.Width = tabImage.Width;
            imgRect.Height = tabImage.Height;

            graphics.DrawImageUnscaled(tabImage, imgRect);
            tabImage.Dispose();
        }
    }

    #endregion

    #region CustomStatusButton : Function to make status button looks cool

    public static class CustomStatusButton
    {
        public static void Initialize(StatusStrip statusbar)
        {
            foreach (ToolStripItem ti in statusbar.Items)
            {
                if (ti.GetType() == typeof(ToolStripStatusLabel)
                    && ti.Name.ToLower().EndsWith("button"))
                {
                    Initialize((ToolStripStatusLabel)ti);
                }
            }
        }

        public static void Initialize(ToolStripStatusLabel tsi)
        {
            tsi.BorderSides = ToolStripStatusLabelBorderSides.All;
            tsi.BorderStyle = Border3DStyle.Etched;

            tsi.MouseDown += StatusButton_MouseDown;
            tsi.MouseUp += StatusButton_MouseUp;
            tsi.MouseEnter += StatusButton_MouseEnter;
            tsi.MouseLeave += StatusButton_MouseLeave;
        }

        //
        // Tool Button
        //
        public static void StatusButton_MouseDown(object sender, MouseEventArgs e)
        {
            ToolStripStatusLabel tsi = (ToolStripStatusLabel)sender;
            tsi.BorderStyle = Border3DStyle.Sunken;
        }
        public static void StatusButton_MouseUp(object sender, MouseEventArgs e)
        {
            ToolStripStatusLabel tsi = (ToolStripStatusLabel)sender;
            tsi.BorderStyle = Border3DStyle.Raised;
        }
        public static void StatusButton_MouseEnter(object sender, EventArgs e)
        {
            ToolStripStatusLabel tsi = (ToolStripStatusLabel)sender;
            tsi.BorderStyle = Border3DStyle.Raised;
        }
        public static void StatusButton_MouseLeave(object sender, EventArgs e)
        {
            ToolStripStatusLabel tsi = (ToolStripStatusLabel)sender;
            tsi.BorderStyle = Border3DStyle.Etched;
        }

    }

    #endregion

}
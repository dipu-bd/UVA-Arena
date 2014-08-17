using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Reflection;
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
                System.GC.Collect();
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

            Bitmap BufferImage = new Bitmap(this.Width, this.Height);
            Graphics graphics = Graphics.FromImage(BufferImage);
            graphics.Clear(this.BackColor);

            if (this.TabCount > 0)
            {
                for (int index = this.TabCount - 1; index >= 0; --index)
                {
                    if (index != this.SelectedIndex)
                        DrawTabPage(index, graphics);
                }
                if (this.SelectedIndex != -1)
                {
                    DrawTabPage(this.SelectedIndex, graphics);
                }
            }

            graphics.Flush();
            e.Graphics.DrawImageUnscaled(BufferImage, 0, 0);

            graphics.Dispose();
            BufferImage.Dispose();
            System.GC.Collect();
        }

        //
        // Draw TabPage
        //
        private void DrawTabPage(int index, Graphics graphics)
        {

            //tab page and border color
            Pen borderPen = null;
            Brush fillbrush = null;
            if (this.SelectedIndex == index)
            {
                borderPen = new Pen(Color.FromArgb(147, 177, 205));
                Color fore = Color.FromArgb(207, 210, 225);
                Color back = Color.FromArgb(242, 246, 252);
                Stylish.GradientStyle style = new Stylish.GradientStyle(HatchStyle.Trellis, fore, back);
                fillbrush = style.GetBrush();
                style.Dispose();
            }
            else
            {
                borderPen = new Pen(Color.FromArgb(167, 197, 235));
                Color dark = Color.FromArgb(207, 210, 225);
                Color light = Color.FromArgb(242, 246, 252);
                Stylish.GradientStyle style = new Stylish.GradientStyle(light, dark, LinearGradientMode.Vertical);
                Rectangle tabBounds = GetTabRectAdjusted(index);
                fillbrush = style.GetBrush(tabBounds.Width, tabBounds.Height + 1);
                style.Dispose();
            }

            //draw image
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath path = GetTabPageBorder(index);
            graphics.FillPath(fillbrush, path);

            this.DrawTabText(index, graphics);
            this.DrawTabImage(index, graphics);
            graphics.DrawPath(borderPen, path);

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

            //add tab border
            path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + tabBounds.Height - 4, tabBounds.Y + 2);
            path.AddLine(tabBounds.X + tabBounds.Height, tabBounds.Y, tabBounds.Right - 3, tabBounds.Y);
            path.AddArc(tabBounds.Right - 6, tabBounds.Y, 6, 6, 270, 90);
            path.AddLine(tabBounds.Right, tabBounds.Y + 2, tabBounds.Right, tabBounds.Bottom);

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

}
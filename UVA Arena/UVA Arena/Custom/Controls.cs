using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    #region CueTextBox

    public class CueTextBox : TextBox
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private string _cueText;
        public string CueText
        {
            get { return _cueText; }
            set
            {
                _cueText = value;
                SendMessage(this.Handle, EM_SETCUEBANNER, 1, value);
            }
        }
    }

    #endregion

    #region TabControl

    public class CustomTabControl : TabControl
    {
        #region Private Variables

        private Bitmap _BackBuffer;
        private Graphics _BufferGraphics; 

        #endregion

        #region Contructors

        public CustomTabControl()
        {
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this._BackBuffer = new Bitmap(this.Width, this.Height);
            this._BufferGraphics = Graphics.FromImage(this._BackBuffer);
        }

        #endregion

        #region Property

        [Category("Appearance"), DefaultValue(0)]
        public int Overlap { get; set; } 
        
        #endregion

        #region Events

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this._BackBuffer.Size != this.Size)
            {
                this._BackBuffer = new Bitmap(this.Width, this.Height);
                this._BufferGraphics = Graphics.FromImage(this._BackBuffer);
            }

            this._BufferGraphics.Clear(Color.Transparent);
            if (this.TabCount > 0)
            {
                for (int index = this.TabCount - 1; index >= 0; --index)
                {
                    if (index != this.SelectedIndex)
                        this.DrawTabPage(index, this._BufferGraphics);
                }
                if (this.SelectedIndex != -1)
                {
                    this.DrawTabPage(this.SelectedIndex, this._BufferGraphics);
                }
            }

            this._BufferGraphics.Flush();

            e.Graphics.DrawImageUnscaled(this._BackBuffer, 0, 0);
        }

        #endregion

        #region Drawing elements

        private void DrawTabPage(int index, Graphics graphics)
        {
            Color dark = Color.FromArgb(207, 210, 215);
            Color light = Color.FromArgb(242, 244, 246);
            Rectangle tabBounds = this.GetTabRectAdjusted(index);
            Brush fillBrush = new LinearGradientBrush(tabBounds, light, dark, LinearGradientMode.Vertical);
            if (this.SelectedIndex == index) fillBrush = new SolidBrush(light);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath path = this.GetTabPageBorder(index); ;
            graphics.FillPath(fillBrush, path);

            this.DrawTabImage(index, graphics);
            this.DrawTabText(index, graphics);

            Pen borderPen = new Pen(Color.FromArgb(147, 177, 205));
            if (this.SelectedIndex == index)
                borderPen = new Pen(Color.FromArgb(127, 157, 185));

            graphics.DrawPath(borderPen, path);
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
        }

        #endregion

        #region Get Borders

        GraphicsPath GetTabPageBorder(int index)
        {
            GraphicsPath path = new GraphicsPath();

            Rectangle tabBounds = this.GetTabRectAdjusted(index);
            Rectangle pageBounds = this.TabPages[index].Bounds;
            pageBounds.X -= 1;
            pageBounds.Y -= 1;
            pageBounds.Width += 4;
            pageBounds.Height += 4;

            this.AddTabBorder(path, tabBounds);
            this.AddPageBorder(path, pageBounds, tabBounds);

            path.CloseFigure();
            return path;
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

        void AddPageBorder(GraphicsPath path, Rectangle pageBounds, Rectangle tabBounds)
        {
            path.AddLine(tabBounds.Right, pageBounds.Y, pageBounds.Right, pageBounds.Y);
            path.AddLine(pageBounds.Right, pageBounds.Y, pageBounds.Right, pageBounds.Bottom);
            path.AddLine(pageBounds.Right, pageBounds.Bottom, pageBounds.X, pageBounds.Bottom);
            path.AddLine(pageBounds.X, pageBounds.Bottom, pageBounds.X, pageBounds.Y);
            path.AddLine(pageBounds.X, pageBounds.Y, tabBounds.X, pageBounds.Y);
        }

        protected void AddTabBorder(GraphicsPath path, Rectangle tabBounds)
        {
            path.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + tabBounds.Height - 4, tabBounds.Y + 2);
            path.AddLine(tabBounds.X + tabBounds.Height, tabBounds.Y, tabBounds.Right - 3, tabBounds.Y);
            path.AddArc(tabBounds.Right - 6, tabBounds.Y, 6, 6, 270, 90);
            path.AddLine(tabBounds.Right, tabBounds.Y + 3, tabBounds.Right, tabBounds.Bottom);

        }

        #endregion

    }

    #endregion
}
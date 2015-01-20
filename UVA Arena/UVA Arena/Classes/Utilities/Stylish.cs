using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UVA_Arena
{
    public static class Stylish
    {
        public enum GradientType
        {
            LinearGradientAngled,
            LinearGradientMode,
            HatchStyleGradient
        }

        public class GradientStyle
        {
            public HatchStyle Hatch { get; set; }
            public Color FirstColor { get; set; }
            public Color SecondColor { get; set; }
            public float Angle { get; set; }
            public bool IsAngleScalable { get; set; }
            public LinearGradientMode GradMode { get; set; }
            public GradientType Type { get; set; }

            public GradientStyle(Color light, Color dark, LinearGradientMode mode)
            {
                Type = GradientType.LinearGradientMode;
                FirstColor = dark;
                SecondColor = light;
                GradMode = mode;
            }
            public GradientStyle(Color light, Color dark, float angle = 0.0F, bool scalable = true)
            {
                Type = GradientType.LinearGradientAngled;
                FirstColor = dark;
                SecondColor = light;
                Angle = angle;
                IsAngleScalable = scalable;
            }
            public GradientStyle(HatchStyle hatch, Color fore, Color back)
            {
                Type = GradientType.HatchStyleGradient;
                FirstColor = fore;
                SecondColor = back;
                Hatch = hatch;
            }

            public Brush GetBrush(int width = 0, int height = 0)
            {
                Rectangle rect = new Rectangle(0, 0, width, height);
                switch (Type)
                {
                    case GradientType.HatchStyleGradient:
                        return new HatchBrush(Hatch, FirstColor, SecondColor);
                    case GradientType.LinearGradientAngled:
                        return new LinearGradientBrush(rect, FirstColor, SecondColor, Angle, IsAngleScalable);
                    case GradientType.LinearGradientMode:
                        return new LinearGradientBrush(rect, FirstColor, SecondColor, GradMode);
                }
                return Brushes.Black;
            }

            public Image GetImage(int width, int height)
            {
                if (width < 5 || height < 5) return null;
                Image img = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(img))
                {
                    Brush brush = GetBrush(width, height);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillRectangle(brush, 0, 0, width, height);
                }
                return img;
            }
            public Image GetImage(Size size)
            {
                return GetImage(size.Width, size.Height);
            }

        };

        public static Color DefaultBackColor = Color.PaleTurquoise;
        public static Font DefaultFont = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        public static Color DefaultForeColor = Color.Black;

        public static void SetGradientBackground(Control control, GradientStyle style)
        {
            if (control == null || control.IsDisposed) return;
            control.BackgroundImageLayout = ImageLayout.Stretch;
            control.BackgroundImage = style.GetImage(control.Size);
        }
    }

}

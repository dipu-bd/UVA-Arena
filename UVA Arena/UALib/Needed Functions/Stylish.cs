using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

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

        [DefaultProperty("Type")]
        [Description("Set the style for obtaining gradient style color")]
        public class GradientStyle
        {
            [Editor(typeof(HatchStyle), typeof(HatchStyle))]
            [Description("Valid when the Type is set to HatchStyleGradient")]
            public HatchStyle Hatch { get; set; }
            
            [Editor(typeof(Color), typeof(Color))]
            [Description("First color used. Fore color for Hatchstyle")]
            public Color FirstColor { get; set; }

            [Editor(typeof(Color), typeof(Color))]            
            [Description("Second color used. Back color for Hatchstyle")]
            public Color SecondColor { get; set; }

            [Description("Angle for the gradient style")]
            public float Angle { get; set; }

            [Description("True to specify that the angle is affected by the transform associated with brush in gradient style; otherwise, false.")]
            public bool IsAngleScalable { get; set; }

            [Editor(typeof(LinearGradientMode), typeof(LinearGradientMode))]
            [Description("Gets of sets the linear gradient mode for linear gradient styles")]
            public LinearGradientMode GradMode { get; set; }

            [Editor(typeof(GradientType), typeof(GradientType))]
            [Description("Gets of sets which type of brush should be used")]
            public GradientType Type { get; set; }

            /// <summary>
            /// Gradient Style to generate linear gradient brush.
            /// </summary>
            /// <param name="light">Light color</param>
            /// <param name="dark">Dark color</param>
            /// <param name="mode">Linear gradient mode</param>
            public GradientStyle(Color light, Color dark, LinearGradientMode mode)
            {
                Type = GradientType.LinearGradientMode;
                FirstColor = dark;
                SecondColor = light;
                GradMode = mode;
            }

            /// <summary>
            /// Gradient Style to generate linear gradient brush.
            /// </summary>
            /// <param name="light">Light color</param>
            /// <param name="dark">Dark color</param>
            /// <param name="angle">Angle in degree for gradients orientation line </param>
            /// <param name="scalable">Set to true to specify that the angle is affected by the transform associated with this System.Drawing.Drawing2D.LinearGradientBrush; otherwise, false.</param>
            public GradientStyle(Color light, Color dark, float angle = 0.0F, bool scalable = true)
            {
                Type = GradientType.LinearGradientAngled;
                FirstColor = dark;
                SecondColor = light;
                Angle = angle;
                IsAngleScalable = scalable;
            }
            /// <summary>
            /// Gradient Style to generate hatch style brush.
            /// </summary>
            /// <param name="hatch">Hatch style for brush</param>
            /// <param name="fore">Line color</param>
            /// <param name="back">Back color</param>
            public GradientStyle(HatchStyle hatch, Color fore, Color back)
            {
                Type = GradientType.HatchStyleGradient;
                FirstColor = fore;
                SecondColor = back;
                Hatch = hatch;
            }

            /// <summary>
            /// Generate the brush associated with this class
            /// </summary>
            /// <param name="width">Target width. Needed in linear gradient mode</param>
            /// <param name="height">Target height. Needed in linear gradient mode</param>
            /// <returns>LinearGradientBrush/HatchBrush as per Type</returns>
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

            /// <summary>
            /// Get a painted image from this class
            /// </summary>
            /// <param name="width">Widt of the image</param>
            /// <param name="height">Height of the image</param>
            /// <returns>Image painted with associated brush </returns>
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
            
            /// <summary>
            /// Get a painted image from this class
            /// </summary>
            /// <param name="size">Size of the image</param>
            /// <returns>Image painted with associated brush </returns>
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

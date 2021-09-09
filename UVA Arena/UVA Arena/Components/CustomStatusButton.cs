namespace System.Windows.Forms
{
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
}

namespace System.Windows.Forms
{
    public class CustomSplitContainer : SplitContainer
    {
        public CustomSplitContainer()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private DateTime _lastMove;
        private bool _moving = false;
        private System.Drawing.Point _initial;

        private bool _MoveSplitter(int diff, int maxsiz)
        {
            if ((DateTime.Now - _lastMove).TotalMilliseconds < 100) return false;
            if (Math.Abs(diff) == 2) return false;

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

}
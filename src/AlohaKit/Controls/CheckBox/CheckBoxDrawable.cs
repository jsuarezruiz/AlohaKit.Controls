namespace AlohaKit.Controls
{
    public class CheckBoxDrawable : IDrawable
    {
        public bool IsChecked { get; set; }
        public Paint CheckedPaint { get; set; }
        public Paint UncheckedPaint { get; set; }
        public Paint StrokePaint { get; set; }
        public double StrokeThickness { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawBackground(canvas, dirtyRect);
            DrawCheck(canvas, dirtyRect);
        }

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var size = Math.Min(dirtyRect.Height, dirtyRect.Width);

            if (IsChecked)
            {
                if (CheckedPaint != null)
                    canvas.SetFillPaint(CheckedPaint, dirtyRect);
                else
                    canvas.FillColor = Colors.Black;

                canvas.FillRoundedRectangle(x, y, size, size, 3);
            }
            else
            {
                if (UncheckedPaint != null)
                    canvas.SetFillPaint(UncheckedPaint, dirtyRect);
                else
                    canvas.FillColor = Colors.Transparent;

                canvas.FillRoundedRectangle(x, y, size, size, 3);

                float strokeWidth = (float)StrokeThickness;

                canvas.StrokeSize = strokeWidth;

                if (CheckedPaint is SolidPaint solidPaint)
                    canvas.StrokeColor = solidPaint.Color;
                else
                    canvas.StrokeColor = Colors.Black;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth, 3);
            }

            canvas.RestoreState();
        }

		public virtual void DrawCheck(ICanvas canvas, RectF dirtyRect)
        {
            if (IsChecked)
            {
                canvas.SaveState();

                const string mark = "M0.00195312 3.49805C0.00195312 3.36133 0.0507812 3.24414 0.148438 3.14648C0.246094 3.04883 0.363281 3 0.5 3C0.636719 3 0.753906 3.04883 0.851562 3.14648L3.5 5.79492L9.14844 0.146484C9.24609 0.0488281 9.36328 0 9.5 0C9.57031 0 9.63477 0.0136719 9.69336 0.0410156C9.75586 0.0644531 9.80859 0.0996094 9.85156 0.146484C9.89844 0.189453 9.93555 0.242187 9.96289 0.304688C9.99023 0.363281 10.0039 0.427734 10.0039 0.498047C10.0039 0.634766 9.95312 0.753906 9.85156 0.855469L3.85156 6.85547C3.75391 6.95312 3.63672 7.00195 3.5 7.00195C3.36328 7.00195 3.24609 6.95312 3.14844 6.85547L0.148438 3.85547C0.0507812 3.75781 0.00195312 3.63867 0.00195312 3.49805Z";

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(mark);

                float strokeWidth = (float)StrokeThickness;

                canvas.StrokeSize = strokeWidth;

                canvas.StrokeColor = Colors.White;

                Point center = new Point(dirtyRect.Width / 2, dirtyRect.Height / 2);
                canvas.Translate((float)center.X - path.Bounds.Width / 2, (float)center.Y - path.Bounds.Height / 2);

                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }
    }
}
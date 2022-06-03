namespace AlohaKit.Controls
{
    public class ProgressRadialDrawable : IDrawable
    {
        public Color BackgroundColor { get; set; }
        public Color StrokeColor { get; set; }
        public Color ProgressColor { get; set; }
        public Color TextColor { get; set; }
        public double FontSize { get; set; }

        public string ProgressText { get; set; }
        public float ProgressAngle { get; set; }
        public ProgressRadialDirection Direction { get; set; }

        public bool IsAnimating { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.Antialias = true;

            DrawBackground(canvas, dirtyRect);

            DrawStroke(canvas, dirtyRect);

            DrawProgress(canvas, dirtyRect);

            DrawText(canvas, dirtyRect);
        }

        public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor;

            canvas.FillRectangle(dirtyRect);

            canvas.RestoreState();
        }

        public virtual void DrawStroke(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var rX = dirtyRect.Width / 2;
            var rY = dirtyRect.Height / 2;

            // Rotate the canvas
            canvas.Rotate((float)45.0f, rX, rY);

            canvas.StrokeColor = StrokeColor;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeSize = 8;

            RectF strokeRect = new RectF
            {
                Size = new Size(dirtyRect.Width - 24, dirtyRect.Height - 24),
                Location = new Point(12, 12)
            };

            PathF progressPath = new PathF();
            progressPath.AddArc(strokeRect.X, strokeRect.Y, strokeRect.Width, strokeRect.Height, 0, 270, false);

            // Draw the background arc
            canvas.DrawPath(progressPath);

            canvas.RestoreState();
        }

        public virtual void DrawProgress(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var rX = dirtyRect.Width / 2;
            var rY = dirtyRect.Height / 2;

            // Rotate the canvas
            var degrees = Direction == ProgressRadialDirection.RightToLeft ? (float)45.0f : (float)135.0f;
            rX = Direction == ProgressRadialDirection.RightToLeft ? rX : rX - (float)2.4f;
            rY = Direction == ProgressRadialDirection.RightToLeft ? rY : rY - (float)6f;
            canvas.Rotate(degrees, rX, rY);

            canvas.StrokeColor = ProgressColor;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeSize = 8;

            RectF progressRect = new RectF
            {
                Size = new Size(dirtyRect.Width - 24, dirtyRect.Height - 24),
                Location = new Point(12, 12)
            };

            PathF progressCurrentPath = new PathF();
            var startAngle = Direction == ProgressRadialDirection.RightToLeft ? (float)0.0f : (float)ProgressAngle * -1;
            var endAngle = Direction == ProgressRadialDirection.RightToLeft ? (float)ProgressAngle : (float)0.0f;
            progressCurrentPath.AddArc(progressRect.X, progressRect.Y, progressRect.Width, progressRect.Height, startAngle, endAngle, false);

            // Draw the progress arc
            canvas.DrawPath(progressCurrentPath);

            canvas.RestoreState();
        }

        public virtual void DrawText(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = TextColor;
            canvas.FontSize = (float)FontSize;

            var x = dirtyRect.Width / 2;
            var y = dirtyRect.Height / 2;

            // Draw the progress value text
            canvas.DrawString(ProgressText, x, y, HorizontalAlignment.Center);

            canvas.RestoreState();
        }
    }
}
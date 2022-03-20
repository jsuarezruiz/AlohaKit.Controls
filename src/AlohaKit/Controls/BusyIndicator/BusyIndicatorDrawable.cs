namespace AlohaKit.Controls
{
    public class BusyIndicatorDrawable : IDrawable
    {
        public Color BackgroundColor { get; set; }
        public Color Color { get; set; }
        public bool HasShadow { get; set; }
        public Color ShadowColor { get; set; }

        public double Rotation { get; set; }
        public double Progress { get; set; }

        internal float ShadowOffset = 1f;

        public BusyIndicatorDrawable()
        {
            Rotation = 0d;
            Progress = 0d;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawBackground(canvas, dirtyRect);

            DrawArc(canvas, dirtyRect);
        }

        void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            // Draw the background
            float cX = dirtyRect.Width / 2.0f + ShadowOffset;
            float cY = dirtyRect.Height / 2.0f + ShadowOffset;
            float radius = dirtyRect.Width  * 0.5f;

            canvas.FillColor = BackgroundColor;

            DrawShadow(canvas, dirtyRect);

            canvas.FillCircle(cX, cY, radius);

            canvas.RestoreState();

            canvas.SaveState();
        }

        void DrawShadow(ICanvas canvas, RectF dirtyRect)
        {
            if (HasShadow)
            {
                canvas.Scale(0.90f, 0.90f);
                canvas.SetShadow(new SizeF(1f, 1f), 5f, ShadowColor.WithAlpha(0.75f));
            }
        }

        void DrawArc(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (HasShadow)
                canvas.Scale(0.90f, 0.90f);

            // Translate the canvas to the center
            float tX = dirtyRect.Width / 2.0f + ShadowOffset;
            float tY = dirtyRect.Height / 2.0f + ShadowOffset;
            canvas.Translate(tX, tY);

            // Rotate the canvas
            canvas.Rotate((float)(Rotation * 360.0f));

            // Draw the progress arc
            float progressArcPadding = 0.65f;

            var progressArcBoundingRect = new RectF(
                -dirtyRect.Width * progressArcPadding * 0.5f,
                -dirtyRect.Height * progressArcPadding * 0.5f,
                dirtyRect.Width * progressArcPadding * 0.5f,
                dirtyRect.Height * progressArcPadding * 0.5f);

            canvas.StrokeSize = dirtyRect.Width * 0.1f;
            canvas.StrokeColor = Color;

            using (var arcPath = new PathF())
            {
                float startAngle = 0.0f;
                float sweepAngle = 90.0f;
                bool clockwise = false;

                startAngle = (float)-Progress * 360.0f;

                arcPath.AddArc(progressArcBoundingRect.X + ShadowOffset, progressArcBoundingRect.Y + ShadowOffset, progressArcBoundingRect.Width, progressArcBoundingRect.Height, startAngle, sweepAngle, clockwise);
                canvas.DrawPath(arcPath);
            }

            canvas.RestoreState();
        }
    }
}
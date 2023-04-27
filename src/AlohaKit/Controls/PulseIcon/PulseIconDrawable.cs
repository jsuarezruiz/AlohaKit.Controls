namespace AlohaKit.Controls
{
    public class PulseIconDrawable : IDrawable
    {
        public string Source { get; set; }
        public Paint BackgroundPaint { get; set; }
        public Color PulseColor { get; set; }

        internal float[] Pulses { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawPulse(canvas, dirtyRect);
            DrawBackground(canvas, dirtyRect);
            DrawIcon(canvas, dirtyRect);
        }

		public virtual void DrawPulse(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (Pulses != null)
            {
                Point center = new Point(dirtyRect.Width / 2, dirtyRect.Height / 2);

                byte r = (byte)(PulseColor.Red * 255);
                byte g = (byte)(PulseColor.Green * 255);
                byte b = (byte)(PulseColor.Blue * 255);

                for (int i = 0; i < Pulses.Length; i++)
                {
                    var radius = dirtyRect.Width / 2 * (Pulses[i]);
                    canvas.FillColor = new Color(r, g, b, (byte)(255 * (1 - Pulses[i])));
                    canvas.FillCircle((float)center.X, (float)center.Y, radius);
                }
            }

            canvas.RestoreState();
        }

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            Point center = new Point(dirtyRect.Width / 2, dirtyRect.Height / 2);
            float radius = dirtyRect.Width / 4;

            if (BackgroundPaint != null)
                canvas.SetFillPaint(BackgroundPaint, dirtyRect);
            else
                canvas.FillColor = Colors.Black;

            canvas.FillCircle((float)center.X, (float)center.Y, radius);

            canvas.RestoreState();
        }

		public virtual void DrawIcon(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (!string.IsNullOrEmpty(Source))
            {
                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(Source);

                canvas.FillColor = Colors.White;

                Point center = new Point(dirtyRect.Width / 2, dirtyRect.Height / 2);
                canvas.Translate((float)center.X - path.Bounds.Width / 2, (float)center.Y  - path.Bounds.Height / 2);

                canvas.FillPath(path);
            }

            canvas.RestoreState();
        }
    }
}
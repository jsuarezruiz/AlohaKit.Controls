namespace AlohaKit.Controls
{
    public class HorizontalProgressBarDrawable : IDrawable
    {
        public Paint BackgroundPaint { get; set; }
        public Paint ProgressPaint { get; set; }
        public double Progress { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawTrack(canvas, dirtyRect);

            DrawProgress(canvas, dirtyRect);
        }

        void DrawTrack(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(BackgroundPaint, dirtyRect);

            var x = dirtyRect.X;
            var y = (float)(dirtyRect.Height / 2);

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        void DrawProgress(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(ProgressPaint, dirtyRect);

            var x = dirtyRect.X;
            var y = (float)(dirtyRect.Height / 2);

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRectangle(x, y, (float)(width * Progress), height);

            canvas.RestoreState();
        }
    }
}
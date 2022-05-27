
namespace AlohaKit.Controls
{
    public class VerticalProgressBarDrawable : BaseProgressBarDrawable
    {
        public override void DrawChart(ICanvas canvas, RectF dirtyRect)
        {
            canvas.Antialias = true;

            DrawTrack(canvas, dirtyRect);

            DrawProgress(canvas, dirtyRect);
        }

        public virtual void DrawTrack(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(BackgroundPaint, dirtyRect);

            var x = (float)(dirtyRect.Width / 2);
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            if (Style == ProgressBarStyle.Square)
                canvas.FillRectangle(x, y, width, height);
            else
                canvas.FillRoundedRectangle(x, y, width, height, CornerRadius);

            canvas.RestoreState();
        }

        public virtual void DrawProgress(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(ProgressPaint, dirtyRect);

            var x = (float)(dirtyRect.Width / 2);
            var y = dirtyRect.Y + dirtyRect.Height;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            if (Style == ProgressBarStyle.Square)
                canvas.FillRectangle(x, y, width, -(float)(height * Progress));
            else
                canvas.FillRoundedRectangle(x, y, width, -(float)(height * Progress), CornerRadius);

            canvas.RestoreState();
        }
    }
}
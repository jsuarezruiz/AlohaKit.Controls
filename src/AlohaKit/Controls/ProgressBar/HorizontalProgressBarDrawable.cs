using AlohaKit.Controls.ProgressBar;

namespace AlohaKit.Controls
{
    public class HorizontalProgressBarDrawable : BaseProgressBarDrawable
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

            var x = dirtyRect.X;
            var y = (float)(dirtyRect.Height / 2);

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;


            if (Style == ProgressBarStyle.Squared)
                canvas.FillRectangle(x, y, width, height);
            else
                canvas.FillRoundedRectangle(x, y, width, height, CornerRadius);

            canvas.RestoreState();
        }

        public virtual void DrawProgress(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(ProgressPaint, dirtyRect);

            var x = dirtyRect.X;
            var y = (float)(dirtyRect.Height / 2);

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            if (Style == ProgressBarStyle.Squared)
                canvas.FillRectangle(x, y, (float)(width * Progress), height);
            else
                canvas.FillRoundedRectangle(x, y, (float)(width * Progress), height, CornerRadius);

            canvas.RestoreState();
        }
    }
}
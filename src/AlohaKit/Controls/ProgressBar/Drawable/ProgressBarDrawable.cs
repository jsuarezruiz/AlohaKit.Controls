
namespace AlohaKit.Controls
{
    public class ProgressBarDrawable : IDrawable
	{
		public Paint StrokePaint { get; set; }

		public Paint ProgressPaint { get; set; }

		public double Progress { get; set; }
		public CornerRadius CornerRadius { get; set; } = 6f;
		public ProgressBarStyle Style { get; set; }
		public bool IsAnimating { get; set; }
		public bool IsVertical { get; set; }

		public void DrawChart(ICanvas canvas, RectF dirtyRect)
        {
            canvas.Antialias = true;

            DrawTrack(canvas, dirtyRect);

            DrawProgress(canvas, dirtyRect);
        }

		public void Draw(ICanvas canvas, RectF dirtyRect)
		{ 
			DrawChart(canvas, dirtyRect); 
		}
		public virtual void DrawTrack(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			canvas.SetFillPaint(StrokePaint, dirtyRect);

			if (Style == ProgressBarStyle.Square)
				canvas.FillRectangle(dirtyRect);
			else
				canvas.FillRoundedRectangle(dirtyRect,
					CornerRadius.TopLeft,
					CornerRadius.TopRight,
					CornerRadius.BottomLeft,
					CornerRadius.BottomRight);
			
			canvas.RestoreState();
		}

		public virtual void DrawProgress(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			RectF rect;
			if (IsVertical)
			{

				var progressHeight = dirtyRect.Height * Progress;
				var progressY = dirtyRect.Y + dirtyRect.Height - progressHeight;

				rect = new Rect(
					dirtyRect.X,
					progressY,
					dirtyRect.Width,
					progressHeight);
			}
			else
			{
				rect = new Rect(
					dirtyRect.X,
					dirtyRect.Y,
					dirtyRect.Width * Progress,
					dirtyRect.Height);
			}

			canvas.SetFillPaint(ProgressPaint, dirtyRect);

			if (Style == ProgressBarStyle.Square)
				canvas.FillRectangle(rect);
			else
				canvas.FillRoundedRectangle(rect,
				CornerRadius.TopLeft,
				CornerRadius.TopRight,
				CornerRadius.BottomLeft,
				CornerRadius.BottomRight);

			canvas.RestoreState();
		}
	}
}
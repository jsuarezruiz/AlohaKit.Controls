
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
		/*
        public virtual void DrawTrack(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

			//canvas.FillColor = StrokeColor;
			canvas.SetFillPaint(StrokePaint, dirtyRect);

			float x;
			float y;
			float width;
			float height;
			
			if (IsVertical)
			{
				x = (float)(dirtyRect.Width / 2);
				y = dirtyRect.Y;

				width = (float)(dirtyRect.Width / 2);
				height = dirtyRect.Height;
			}
			else
			{
				x = dirtyRect.X;
				y = (float)(dirtyRect.Height / 2);

				width = dirtyRect.Width;
				height = (float)(dirtyRect.Height / 2);
			}

			if (Style == ProgressBarStyle.Square)
				canvas.FillRectangle(x, y, width, height);
			else
				canvas.FillRoundedRectangle(x, y, width, height, CornerRadius);

			canvas.RestoreState();
        }

        public virtual void DrawProgress(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

			//canvas.FillColor = ProgressColor;


			float x;
			float y;
			float width;
			float height;

			if (IsVertical)
			{

				x = (float)(dirtyRect.Width / 2);
				y = dirtyRect.Y + dirtyRect.Height;

				width = (float)(dirtyRect.Width / 2);
				height = dirtyRect.Height;

				RectF barRect = new RectF(x, y, width, -(float)(height * Progress));
				canvas.SetFillPaint(ProgressPaint, barRect);

				if (Style == ProgressBarStyle.Square)
					canvas.FillRectangle(x, y, width, -(float)(height * Progress));
				else
					canvas.FillRoundedRectangle(x, y, width, -(float)(height * Progress), CornerRadius);
			}
			else
			{
				x = dirtyRect.X;
				y = (float)(dirtyRect.Height / 2);

				width = dirtyRect.Width;
				height = dirtyRect.Height / 2;
				canvas.SetFillPaint(ProgressPaint, dirtyRect);

				if (Style == ProgressBarStyle.Square)
					canvas.FillRectangle(x, y, (float)(width * Progress), height);
				else
					canvas.FillRoundedRectangle(x, y, (float)(width * Progress), height, CornerRadius);
			}

            canvas.RestoreState();
        }*/
	}
}
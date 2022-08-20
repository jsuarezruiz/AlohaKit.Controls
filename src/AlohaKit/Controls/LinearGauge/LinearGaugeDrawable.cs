using Microsoft.Maui.Graphics;

namespace AlohaKit.Controls
{
	public class LinearGaugeDrawable : IDrawable
	{
		// TODO: Expose these properties 
		Color Stroke = Colors.Black;
		const float StrokeThickness = 2.0f;
		const float TicksWidth = 20.0f;

		public Paint BackgroundPaint { get; set; }
		public int RangeStart { get; set; }
		public int RangeEnd { get; set; }
		public int Value { get; set; }
		public CornerRadius CornerRadius { get; set; }

		public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			DrawBackground(canvas, dirtyRect);
			DrawProgress(canvas, dirtyRect);
			DrawTicks(canvas, dirtyRect);
		}

		void DrawBackground(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			canvas.StrokeColor = Stroke;
			canvas.StrokeSize = StrokeThickness;

			canvas.DrawRoundedRectangle(
				dirtyRect.X + TicksWidth,
				dirtyRect.Y,
				dirtyRect.Width - TicksWidth,
				dirtyRect.Height,
				(float)CornerRadius.TopLeft,
				(float)CornerRadius.TopRight,
				(float)CornerRadius.BottomLeft,
				(float)CornerRadius.BottomRight);

			canvas.RestoreState();
		}

		void DrawProgress(ICanvas canvas, RectF dirtyRect)
		{
			if (BackgroundPaint != null)
			{
				canvas.SaveState();

				int value = Value;

				if (value > RangeEnd)
					value = RangeEnd;

				if (value < RangeStart)
					value = RangeStart;

				var percentage = (double)value / RangeEnd;
				var progressHeight = dirtyRect.Height * percentage;
				var progressY = dirtyRect.Y + dirtyRect.Height - progressHeight;

				var rect = new Rect(
					dirtyRect.X + TicksWidth + StrokeThickness / 2,
					progressY + StrokeThickness / 2,
					dirtyRect.Width - TicksWidth - StrokeThickness,
					progressHeight - StrokeThickness);

				canvas.SetFillPaint(BackgroundPaint, rect);

				canvas.FillRoundedRectangle(
					rect,
					CornerRadius.TopLeft,
					CornerRadius.TopRight,
					CornerRadius.BottomLeft,
					CornerRadius.BottomRight);

				canvas.RestoreState();
			}
		}

		void DrawTicks(ICanvas canvas, RectF dirtyRect)
		{
			int steps = 10;

			for (int i = 0; i < steps; i++)
			{
				var stepScale = (double)i / steps;
				Point nextLine = new Point(dirtyRect.X + TicksWidth, dirtyRect.Y + dirtyRect.Height * stepScale);

				double defaultTickWidthh = 10.0d;
				double tickWidth = defaultTickWidthh;

				if (i != 0)
				{
					if (i == (steps / 2))
						tickWidth = defaultTickWidthh * 2;

					canvas.DrawLine(nextLine, nextLine.Offset(tickWidth, 0));

					canvas.Font = Microsoft.Maui.Graphics.Font.Default;
					var strValue = (int)(((double)RangeEnd / steps) * (steps - i));
					PointF stringPosition = nextLine.Offset(-10, 0);

					canvas.DrawString(strValue.ToString(), stringPosition.X, stringPosition.Y, HorizontalAlignment.Center);
				}
			}
		}
	}
}
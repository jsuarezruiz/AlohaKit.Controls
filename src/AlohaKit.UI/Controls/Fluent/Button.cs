using AlohaKit.UI.Extensions;

namespace AlohaKit.UI.Fluent
{
	public class Button : ButtonBase
	{
		const string BackgroundColor = "#2A2A2A";
		const float CornerRadius = 4.0f;
		const float MinimumHeight = 32f;
		
		public override void Draw(ICanvas canvas, RectF bounds)
		{
			canvas.SaveState();

			base.Draw(canvas, bounds);

			DrawBackground(canvas, bounds);
			DrawText(canvas, bounds);

			canvas.RestoreState();
		}

		public virtual void DrawBackground(ICanvas canvas, RectF bounds)
		{
			canvas.SaveState();

			var x = X;
			var y = Y;

			var width = WidthRequest;
			var height = MinimumHeight;

			if (!float.IsNaN(HeightRequest))
				height = HeightRequest;

			var backgroundColor = Color.FromArgb(BackgroundColor);

			if (Background is SolidColorBrush backgroundBrush)
			{
				if (backgroundBrush.Color != null)
					backgroundColor = backgroundBrush.Color;
			}

			var border = new LinearGradientPaint
			{
				GradientStops = new PaintGradientStop[]
				{
					new PaintGradientStop(0.0f, backgroundColor.Lighter()),
					new PaintGradientStop(0.9f, backgroundColor.Darker())
				},
				StartPoint = new Point(0, 0),
				EndPoint = new Point(0, 1)
			};

			canvas.SetFillPaint(border, bounds);

			canvas.FillRoundedRectangle(x, y, width, height, CornerRadius);

			canvas.RestoreState();

			canvas.SaveState();

			canvas.StrokeColor = Colors.Black;

			if (Background is SolidColorBrush borderBrush)
			{
				if (borderBrush.Color != null)
					canvas.FillColor = borderBrush.Color;
				else
					canvas.FillColor = backgroundColor;
			}
			else
				canvas.SetFillPaint(Background, bounds);

			var strokeWidth = 1;
			float margin = strokeWidth * 2;
			canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, CornerRadius);

			canvas.RestoreState();
		}

		public virtual void DrawText(ICanvas canvas, RectF bounds)
		{
			canvas.SaveState();

			canvas.FontColor = TextColor;
			canvas.FontSize = (float)FontSize;

			float height = MinimumHeight;

			if (!float.IsNaN(HeightRequest))
				height = HeightRequest;

			canvas.DrawString(Text, X, Y, WidthRequest, height, HorizontalAlignment.Center, VerticalAlignment.Center);

			canvas.RestoreState();
		}
	}
}
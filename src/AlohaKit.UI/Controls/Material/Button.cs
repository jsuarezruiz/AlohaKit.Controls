namespace AlohaKit.UI.Material
{
	public class Button : ButtonBase
	{
		const string BackgroundColor = "#2196f3";
		const float MinimumHeight = 36f;
		const float CornerRadius = 2.0f;

		public override void Draw(ICanvas canvas, RectF bounds)
		{
			canvas.SaveState();

			base.Draw(canvas, bounds);

			DrawBackground(canvas, bounds);
			DrawText(canvas, bounds);

			canvas.RestoreState();
		}

		public void DrawBackground(ICanvas canvas, RectF bounds)
		{
			canvas.SaveState();

			if (Background is SolidColorBrush solidColorBrush)
			{
				if (solidColorBrush.Color != null)
					canvas.FillColor = solidColorBrush.Color;
				else
					canvas.FillColor = Color.FromArgb(BackgroundColor);
			}
			else
				canvas.SetFillPaint(Background, bounds);

			float height = MinimumHeight;

			if (!float.IsNaN(HeightRequest))
				height = HeightRequest;

			canvas.FillRoundedRectangle(X, Y, WidthRequest, height, CornerRadius);

			canvas.RestoreState();
		}

		public void DrawText(ICanvas canvas, RectF bounds)
		{
			canvas.SaveState();

			canvas.FontColor = TextColor;
			canvas.FontSize = (float)FontSize;

			float height = MinimumHeight;

			if (!float.IsNaN(HeightRequest))
				height = HeightRequest;

			canvas.DrawString(Text.ToUpper(), X, Y, WidthRequest, height, HorizontalAlignment.Center, VerticalAlignment.Center);

			canvas.RestoreState();
		}
	}
}
namespace AlohaKit.UI.Cupertino
{
	public class Button : ButtonBase
	{
		const string BackgroundColor = "#007AFF";
		const float CornerRadius = 2.0f;
		const float MinimumHeight = 44f;

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
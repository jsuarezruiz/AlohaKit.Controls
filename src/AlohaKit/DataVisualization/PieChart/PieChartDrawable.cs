namespace AlohaKit.Controls
{
	public class PieChartDrawable : IDrawable
	{
		readonly List<Color> ChartPalette = new List<Color>
		{
			Color.FromArgb("#A6CEE3"),
			Color.FromArgb("#1F78B4"),
			Color.FromArgb("#B2DF8A"),
			Color.FromArgb("#33A02C"),
			Color.FromArgb("#FB9A99"),
			Color.FromArgb("#E31A1C"),
			Color.FromArgb("#FDBF6F"),
			Color.FromArgb("#FF7F00"),
			Color.FromArgb("#CAB2D6"),
			Color.FromArgb("#6A3D9A"),
			Color.FromArgb("#FFFF99"),
			Color.FromArgb("#B15928"),
		};

		public Paint BackgroundPaint { get; set; }
		public Dictionary<string, float> ItemsSource { get; set; }
		public bool ShowLabels { get; set; }

		public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			DrawBackground(canvas, dirtyRect);
			DrawSlices(canvas, dirtyRect);
			DrawLabels(canvas, dirtyRect);
		}

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			var radius = dirtyRect.Width / 4;
			var center = new PointF(dirtyRect.Center.X, dirtyRect.Center.Y);

			var radialRectangle = new RectF(dirtyRect.Center.X - radius, dirtyRect.Center.Y - radius, radius * 2, radius * 2);
			canvas.SetFillPaint(BackgroundPaint, radialRectangle);
			canvas.FillCircle(center, radius);

			canvas.RestoreState();
		}

		public virtual void DrawSlices(ICanvas canvas, RectF dirtyRect)
		{
			if (ItemsSource == null)
				return;

			float totalValues = 0;

			for (var i = 0; i < ItemsSource.Count; i++)
			{
				var item = ItemsSource.ElementAt(i);
				totalValues += item.Value;
			}

			float startAngle = 0;
			PointF center = new PointF(dirtyRect.Center.X, dirtyRect.Center.Y);
			float radius = dirtyRect.Width / 4;

			canvas.SaveState();

			for (var i = 0; i < ItemsSource.Count; i++)
			{
				var item = ItemsSource.ElementAt(i);
				float sweepAngle = 360f * item.Value / totalValues;

				var path = new PathF();
				path.MoveTo(center);
				var rect = new RectF(dirtyRect.Center.X - radius, dirtyRect.Center.Y - radius, dirtyRect.Center.X + radius, dirtyRect.Center.Y + radius);
				path.AddArc(rect.X, rect.Y, rect.Width, rect.Height, 0, sweepAngle, false);
				path.Close();

				canvas.FillColor = ChartPalette[i];

				startAngle += sweepAngle;

				canvas.Rotate(startAngle, center.X, center.Y);

				canvas.FillPath(path);

				canvas.RestoreState();
			}

			canvas.RestoreState();
		}

		public virtual void DrawLabels(ICanvas canvas, RectF dirtyRect)
		{
			if (!ShowLabels || ItemsSource == null)
				return;

			canvas.SaveState();

			var center = new PointF(dirtyRect.Center.X, dirtyRect.Center.Y);
			var radius = dirtyRect.Width / 4;
			var scale = 100f / ItemsSource.Select(x => x.Value).Sum();

			var degrees = 0f;
			var radiusPadding = Convert.ToInt32(dirtyRect.Width / 10);

			for (var i = 0; i < ItemsSource.Count; i++)
			{
				var item = ItemsSource.ElementAt(i);
				degrees += 360 * (item.Value * scale / 100) / 2;

				var x = (float)(dirtyRect.Center.X + (radius + radiusPadding) * Math.Cos(degrees * (Math.PI / 180)));
				var y = (float)(dirtyRect.Center.Y + (radius + radiusPadding) * Math.Sin(degrees * (Math.PI / 180)));

				var textPoint = new PointF(x, y);
				var valuePoint = new PointF(textPoint.X, textPoint.Y + 16);

				canvas.FontColor = ChartPalette[i];

				canvas.DrawString(item.Key,
					textPoint.X,
					textPoint.Y,
					HorizontalAlignment.Center);

				canvas.DrawString(item.Value.ToString(),
					valuePoint.X,
					valuePoint.Y,
				   HorizontalAlignment.Center);

				degrees += 360 * (item.Value * scale / 100) / 2;
			}

			canvas.RestoreState();
		}
	}
}
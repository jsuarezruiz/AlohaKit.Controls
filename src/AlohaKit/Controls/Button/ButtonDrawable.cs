using Microsoft.Maui.Animations;

namespace AlohaKit.Controls
{
	public class ButtonDrawable : IDrawable
	{
		const float ShadowOffset = 2.0f;

		public ButtonDrawable()
		{
			Scale = 1.0f;
		}

		public CornerRadius CornerRadius { get; set; }
		public Paint BackgroundPaint { get; set; }
		public Paint StrokePaint { get; set; }
		public double StrokeThickness { get; set; }
		public Color TextColor { get; set; }
		public string Text { get; set; }
		public TextAlignment HorizontalTextAlignment { get; set; }
		public TextAlignment VerticalTextAlignment { get; set; }
		public bool HasShadow { get; set; }
		public Color ShadowColor { get; set; }
		public float FontSize { get; set; }

		internal PointF TouchPoint { get; set; }
		internal double AnimationPercent { get; set; }
		internal float Scale { get; set; }

		public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			canvas.Antialias = true;

			DrawShadow(canvas, dirtyRect);
			DrawStroke(canvas, dirtyRect);
			DrawBackground(canvas, dirtyRect);
			DrawText(canvas, dirtyRect);

			DrawRippleEffect(canvas, dirtyRect);
		}

		public virtual void DrawShadow(ICanvas canvas, RectF dirtyRect)
		{
			if (HasShadow)
			{
				canvas.SaveState();

				canvas.Scale(Scale, Scale);

				canvas.FillColor = ShadowColor;

				var x = dirtyRect.X + ShadowOffset / 4;
				var y = dirtyRect.Y + ShadowOffset / 4;
				var width = dirtyRect.Width - (ShadowOffset * 2);
				var height = dirtyRect.Height - (ShadowOffset * 2);

				var topLeftRadius = (float)CornerRadius.TopLeft;
				var topRightRadius = (float)CornerRadius.TopRight;
				var bottomLeftRadius = (float)CornerRadius.BottomLeft;
				var bottomRightRadius = (float)CornerRadius.BottomRight;

				canvas.EnableDefaultShadow();

				canvas.SetShadow(new SizeF(ShadowOffset, ShadowOffset), 4, ShadowColor);

				canvas.FillRoundedRectangle(x, y, width, height, topLeftRadius, topRightRadius, bottomLeftRadius, bottomRightRadius);

				canvas.RestoreState();
			}
		}

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			canvas.Scale(Scale, Scale);

			canvas.SetFillPaint(BackgroundPaint, dirtyRect);

			var x = dirtyRect.X;
			var y = dirtyRect.Y;
			var width = dirtyRect.Width;
			var height = dirtyRect.Height;

			var topLeftRadius = (float)CornerRadius.TopLeft;
			var topRightRadius = (float)CornerRadius.TopRight;
			var bottomLeftRadius = (float)CornerRadius.BottomLeft;
			var bottomRightRadius = (float)CornerRadius.BottomRight;

			if (StrokePaint != null)
			{
				x += (float)StrokeThickness / 2;
				y += (float)StrokeThickness / 2;
				width -= (float)StrokeThickness;
				height -= (float)StrokeThickness;

				topLeftRadius -= (float)StrokeThickness / 2;
				topRightRadius -= (float)StrokeThickness / 2;
				bottomLeftRadius -= (float)StrokeThickness / 2;
				bottomRightRadius -= (float)StrokeThickness / 2;
			}

			if (HasShadow)
			{
				width -= ShadowOffset * 2;
				height -= ShadowOffset * 2;
			}

			canvas.FillRoundedRectangle(x, y, width, height, topLeftRadius, topRightRadius, bottomLeftRadius, bottomRightRadius);

			canvas.RestoreState();
		}

		public virtual void DrawStroke(ICanvas canvas, RectF dirtyRect)
		{
			if (StrokePaint != null && StrokeThickness > 0)
			{
				canvas.SaveState();

				canvas.Scale(Scale, Scale);

				canvas.SetFillPaint(StrokePaint, dirtyRect);

				var x = dirtyRect.X;
				var y = dirtyRect.Y;
				var width = dirtyRect.Width;
				var height = dirtyRect.Height;

				if (HasShadow)
				{
					width -= ShadowOffset * 2;
					height -= ShadowOffset * 2;
				}

				var topLeftRadius = (float)CornerRadius.TopLeft;
				var topRightRadius = (float)CornerRadius.TopRight;
				var bottomLeftRadius = (float)CornerRadius.BottomLeft;
				var bottomRightRadius = (float)CornerRadius.BottomRight;

				canvas.FillRoundedRectangle(x, y, width, height, topLeftRadius, topRightRadius, bottomLeftRadius, bottomRightRadius);

				canvas.RestoreState();
			}
		}

		public virtual void DrawText(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			canvas.Scale(Scale, Scale);

			canvas.FontSize = FontSize;
			canvas.FontColor = TextColor;

			var x = 0;
			var y = 0;
			var width = dirtyRect.Width;
			var height = dirtyRect.Height;

			if (HasShadow)
			{
				width -= ShadowOffset * 2;
				height -= ShadowOffset * 2;
			}

			var horizontalAlignment = HorizontalAlignment.Center;

			switch (HorizontalTextAlignment)
			{
				case TextAlignment.Start:
					horizontalAlignment = HorizontalAlignment.Left;
					break;
				case TextAlignment.Center:
					horizontalAlignment = HorizontalAlignment.Center;
					break;
				case TextAlignment.End:
					horizontalAlignment = HorizontalAlignment.Right;
					break;
			}

			var verticalAlignment = VerticalAlignment.Center;

			switch (VerticalTextAlignment)
			{
				case TextAlignment.Start:
					verticalAlignment = VerticalAlignment.Top;
					break;
				case TextAlignment.Center:
					verticalAlignment = VerticalAlignment.Center;
					break;
				case TextAlignment.End:
					verticalAlignment = VerticalAlignment.Bottom;
					break;
			}

			canvas.DrawString(Text, x, y, width, height, horizontalAlignment, verticalAlignment);

			canvas.RestoreState();
		}

		public virtual void DrawRippleEffect(ICanvas canvas, RectF dirtyRect)
		{
			if (dirtyRect.Contains(TouchPoint))
			{
				canvas.SaveState();

				var x = dirtyRect.X;
				var y = dirtyRect.Y;
				var width = dirtyRect.Width;
				var height = dirtyRect.Height;

				if (HasShadow)
				{
					width -= ShadowOffset * 2;
					height -= ShadowOffset * 2;
				}

				var clippingPath = new PathF();
				clippingPath.AppendRoundedRectangle(x, y, width, height, (float)CornerRadius.TopLeft, (float)CornerRadius.TopRight, (float)CornerRadius.BottomLeft, (float)CornerRadius.BottomRight);

				canvas.ClipPath(clippingPath);

				canvas.FillColor = Colors.White.WithAlpha(0.75f);

				canvas.Alpha = 0.25f;

				float minimumRippleEffectSize = 0.0f;

				var rippleEffectSize = minimumRippleEffectSize.Lerp(dirtyRect.Width, AnimationPercent);

				canvas.FillCircle((float)TouchPoint.X, (float)TouchPoint.Y, rippleEffectSize);

				canvas.RestoreState();
			}
		}
	}
}
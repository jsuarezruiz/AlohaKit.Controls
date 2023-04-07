namespace AlohaKit.Controls
{
	public class CaptchaDrawable : IDrawable
	{
		public string Word { get; set; }
		public CaptchaLevel Level { get; set; }
		public Color TextColor { get; set; }

		public void Draw(ICanvas canvas, RectF dirtyRect)
		{
			DrawText(canvas, dirtyRect);
			DrawArtifacts(canvas, dirtyRect);
		}

		void DrawText(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();

			var height = dirtyRect.Height;
			var width = dirtyRect.Width;

			int minLetterDistanceY = 0;
			int maxLetterDistanceY = (int)height / Word.Length;

			int minLetterDistanceX = 6;
			int maxLetterDistanceX = (int)width / Word.Length;

			var coordRandom = new Random();

			var letterPoint = new PointF(coordRandom.Next(minLetterDistanceY, maxLetterDistanceX), height / 2);

			foreach (var l in Word)
			{
				letterPoint.Y += coordRandom.Next(0, maxLetterDistanceY);

				canvas.FontSize = 24;
				canvas.FontColor = TextColor;

				canvas.DrawString(l.ToString(), letterPoint.X, letterPoint.Y,HorizontalAlignment.Center);

				letterPoint.X += coordRandom.Next(minLetterDistanceX, maxLetterDistanceX);
			}

			canvas.RestoreState();
		}

		void DrawArtifacts(ICanvas canvas, RectF dirtyRect)
		{
			canvas.SaveState();
			
			var randomLines = new Random();

			for (var i = 0; i < GetArtifactLength(Level); i++)
			{
				var x1 = randomLines.Next(0, (int)dirtyRect.Width);
				var y1 = randomLines.Next(0, (int)dirtyRect.Height);
				var x2 = randomLines.Next(0, (int)dirtyRect.Width);
				var y2 = randomLines.Next(0, (int)dirtyRect.Height);

				canvas.StrokeColor = TextColor.WithAlpha(0.8f);

				var randomStrokeSize = new Random();
				canvas.StrokeSize = randomStrokeSize.Next(1, GetArtifactWidth(Level));

				var p1 = new PointF(x1, y1);
				var p2 = new PointF(x2, y2);

				canvas.DrawLine(p1, p2);
			}

			canvas.RestoreState();
		}

		int GetArtifactLength(CaptchaLevel level)
		{
			switch (level)
			{
				case CaptchaLevel.Weak:
					return 4;
				default:
				case CaptchaLevel.Normal:
					return 6;
				case CaptchaLevel.Strong:
					return 8;
			}
		}
			
		int GetArtifactWidth(CaptchaLevel level)
		{
			switch (level)
			{
				case CaptchaLevel.Weak:
					return 2;
				default:
				case CaptchaLevel.Normal:
					return 3;
				case CaptchaLevel.Strong:
					return 4;
			}
		}
	}
}
namespace AlohaKit.Controls
{
    public class NumericUpDownDrawable : IDrawable
    {
        public Paint BackgroundPaint { get; set; }
        public Color Color { get; set; }
        public Paint MinimumColorPaint { get; set; }
        public Paint MaximumColorPaint { get; set; }
        public Color MinimumTextColor { get; set; }
        public Color MaximumTextColor { get; set; }
        public Color TextColor { get; set; }
        public double FontSize { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Value { get; set; }

        internal Point TouchPoint { get; set; }
        internal Rect MinusRectangle { get; set; }
        internal Rect PlusRectangle { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawBackground(canvas, dirtyRect);
            DrawBorder(canvas, dirtyRect);
            DrawMinus(canvas, dirtyRect);
            DrawMaximum(canvas, dirtyRect);
            DrawValue(canvas, dirtyRect);
        }

		public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (BackgroundPaint != null)
            {
                canvas.SetFillPaint(BackgroundPaint, dirtyRect);

                canvas.FillRectangle(dirtyRect);
            }

            canvas.RestoreState();
        }

		public virtual void DrawBorder(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;
            float radius = height / 2;

            canvas.StrokeColor = Color;

            float strokeThickness = 4.0f;

            canvas.StrokeSize = strokeThickness;

            float margin = strokeThickness;

            canvas.DrawRoundedRectangle(margin, margin, width - margin * 2, height - margin * 2, radius);

            canvas.RestoreState();
        }

		public virtual void DrawMinus(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (MinimumColorPaint != null)
            {
                canvas.SetFillPaint(MinimumColorPaint, dirtyRect);
            }

            float strokeThickness = 4.0f;

            canvas.StrokeSize = strokeThickness;

            float margin = 6.0f;
            float radius = (dirtyRect.Height - strokeThickness * 2) / 2 - margin;
            float cX = dirtyRect.X + strokeThickness + radius + margin;
            float cY = dirtyRect.Y + strokeThickness + radius + margin;

            canvas.FillCircle(cX, cY, radius);

            MinusRectangle = new Rect(cX - radius, cY - radius, radius * 2, radius * 2);

			canvas.RestoreState();

			canvas.SaveState();

			const string minusIcon = "M0,0L32,0 32,5.3 0,5.3z";

			var vBuilder = new PathBuilder();
			var path = vBuilder.BuildPath(minusIcon).AsScaledPath(0.5f);

			canvas.FillColor = MinimumTextColor;

			Point center = new Point(MinusRectangle.X + MinusRectangle.Width / 2, MinusRectangle.Y + MinusRectangle.Height / 2);
			canvas.Translate((float)center.X - path.Bounds.Width / 2, (float)center.Y - path.Bounds.Height / 2);

			canvas.FillPath(path);

			canvas.RestoreState();
		}

		public virtual void DrawMaximum(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (MaximumColorPaint != null)
            {
                canvas.SetFillPaint(MaximumColorPaint, dirtyRect);
            }

            float strokeThickness = 4.0f;

            canvas.StrokeSize = strokeThickness;

            float margin = 6.0f;
            float radius = (dirtyRect.Height - strokeThickness * 2) / 2 - margin;
            float cX = dirtyRect.Width - (strokeThickness + radius + margin);
            float cY = dirtyRect.Y + strokeThickness + radius + margin;

            canvas.FillCircle(cX, cY, radius);

            PlusRectangle = new Rect(cX - radius, cY - radius, radius * 2, radius * 2);

            canvas.RestoreState();

            canvas.SaveState();

			const string plusIcon = "M13.55896,0L18.461914,0 18.461914,13.557983 32,13.557983 32,18.481018 18.5,18.481018 18.5,32 13.55896,32 13.55896,18.481018 0,18.481018 0,13.557983 13.55896,13.557983z";

			var vBuilder = new PathBuilder();
			var path = vBuilder.BuildPath(plusIcon).AsScaledPath(0.5f);

			canvas.FillColor = MaximumTextColor;

			Point center = new Point(PlusRectangle.X + PlusRectangle.Width / 2, PlusRectangle.Y + PlusRectangle.Height / 2);
			canvas.Translate((float)center.X - path.Bounds.Width / 2, (float)center.Y - path.Bounds.Height / 2);

			canvas.FillPath(path);

			canvas.RestoreState();
        }

		public virtual void DrawValue(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = TextColor;
            canvas.FontSize = (float)FontSize;

            float margin = 6.0f;
            float x = dirtyRect.Width / 2;
            float y = dirtyRect.Height / 2;

            canvas.DrawString(Value.ToString(), x, y + margin, HorizontalAlignment.Center);

            canvas.RestoreState();
        }
    }
}
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

        void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (BackgroundPaint != null)
            {
                canvas.SetFillPaint(BackgroundPaint, dirtyRect);

                canvas.FillRectangle(dirtyRect);
            }

            canvas.RestoreState();
        }

        void DrawBorder(ICanvas canvas, RectF dirtyRect)
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

        void DrawMinus(ICanvas canvas, RectF dirtyRect)
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

            canvas.FillColor = Colors.Red;
            canvas.FontColor = MinimumTextColor;

            canvas.FontSize = 24.0f;

            canvas.DrawString("-", cX, cY + margin, HorizontalAlignment.Center);

            canvas.RestoreState();
        }

        void DrawMaximum(ICanvas canvas, RectF dirtyRect)
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

            canvas.FontColor = MaximumTextColor;

            canvas.FontSize = 24.0f;

            canvas.DrawString("+", cX, cY + margin, HorizontalAlignment.Center);

            canvas.RestoreState();
        }

        void DrawValue(ICanvas canvas, RectF dirtyRect)
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
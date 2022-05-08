namespace AlohaKit.Controls
{
    public class RatingDrawable : IDrawable
    {
        public int ItemsCount { get; set; }
        public int Value { get; set; }
        public Paint BackgroundPaint { get; set; }
        public Color SelectedFillColor { get; set; }
        public Color UnSelectedFillColor { get; set; }
        public Color SelectedStrokeColor { get; set; }
        public Color UnSelectedStrokeColor { get; set; }
        public double SelectedStrokeWidth { get; set; }
        public double UnSelectedStrokeWidth { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.Antialias = true;

            DrawBackground(canvas, dirtyRect);

            for (int i = 0; i < ItemsCount; i++)
            {
                DrawRatingItem(canvas, dirtyRect, i);
            }
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

        public virtual void DrawRatingItem(ICanvas canvas, RectF dirtyRect, int index)
        {
            canvas.SaveState();

            canvas.StrokeColor = (index >= Value) ? UnSelectedStrokeColor : SelectedStrokeColor;
            canvas.StrokeSize = (index >= Value) ? (float)UnSelectedStrokeWidth : (float)SelectedStrokeWidth;
            canvas.FillColor = (index >= Value) ? UnSelectedFillColor : SelectedFillColor;

            float scale = 0.85f;
            float itemSize = 36.0f;

            canvas.Scale(scale, scale);
            canvas.Translate(index * itemSize, 0);

            string star = "M16.001007,0L20.944,10.533997 32,12.223022 23.998993,20.421997 25.889008,32 16.001007,26.533997 6.1109924,32 8,20.421997 0,12.223022 11.057007,10.533997z";

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(star);

            canvas.DrawPath(path);
            canvas.FillPath(path);

            canvas.RestoreState();
        }
    }
}
namespace AlohaKit.UI
{
    public class RoundRectangle : Shape
    {
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(RoundRectangle), new CornerRadius());

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public override void Draw(ICanvas canvas, RectF bounds)
        {
            canvas.SaveState();

            base.Draw(canvas, bounds);

            var rect = new Rect(X, Y, WidthRequest, HeightRequest);

            if (Fill != null)
            {
                if (Fill is SolidColorBrush solidColorBrush)
                    canvas.FillColor = solidColorBrush.Color;
                else
                    canvas.SetFillPaint(Fill, rect);

                canvas.FillRoundedRectangle(rect, CornerRadius.TopLeft, CornerRadius.TopRight, CornerRadius.BottomLeft, CornerRadius.BottomRight);
            }

            if (Stroke != null)
            {
                if (Stroke is SolidColorBrush solidColorBrush)
                    canvas.StrokeColor = solidColorBrush.Color;

                canvas.StrokeSize = (float)StrokeThickness;

                canvas.DrawRoundedRectangle(rect, CornerRadius.TopLeft, CornerRadius.TopRight, CornerRadius.BottomLeft, CornerRadius.BottomRight);
            }

            canvas.RestoreState();
        }
    }
}
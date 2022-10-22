namespace AlohaKit.UI
{
    public class Ellipse : Shape
    {
        public override void Draw(ICanvas canvas, RectF bounds)
        {      
            canvas.SaveState();

            var rect = new Rect(X, Y, WidthRequest, HeightRequest);

            if (Fill != null)
            {
                if (Fill is SolidColorBrush solidColorBrush)
                    canvas.FillColor = solidColorBrush.Color;
                else
                    canvas.SetFillPaint(Fill, rect);

                canvas.FillEllipse(rect);
            }

            if (Stroke != null)
            {
                if (Stroke is SolidColorBrush solidColorBrush)
                    canvas.StrokeColor = solidColorBrush.Color;

                canvas.StrokeSize = (float)StrokeThickness;

                canvas.DrawEllipse(rect);
            }

            canvas.RestoreState();
        }
    }
}

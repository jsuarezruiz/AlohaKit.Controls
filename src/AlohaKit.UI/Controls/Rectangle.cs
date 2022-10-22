namespace AlohaKit.UI
{
    public class Rectangle : Shape
    {
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

                canvas.FillRectangle(rect);
            }

            if(Stroke != null)
            {
                if (Stroke is SolidColorBrush solidColorBrush)
                    canvas.StrokeColor = solidColorBrush.Color;
                         
                canvas.StrokeSize = (float)StrokeThickness;

                canvas.DrawRectangle(rect);
            }

            canvas.RestoreState();
        }
    }
}

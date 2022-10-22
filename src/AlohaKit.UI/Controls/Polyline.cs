using AlohaKit.UI.Extensions;

namespace AlohaKit.UI
{
    public class Polyline : Shape
    {
        public static readonly BindableProperty PointsProperty =
            BindableProperty.Create(nameof(Points), typeof(PointCollection), typeof(Polyline), null, defaultValueCreator: bindable => new PointCollection());

        public PointCollection Points
        {
            set { SetValue(PointsProperty, value); }
            get { return (PointCollection)GetValue(PointsProperty); }
        }

        public override void Draw(ICanvas canvas, RectF bounds)
        {
            canvas.SaveState();

            base.Draw(canvas, bounds);

            if (Stroke != null)
            {
                canvas.Translate(X, Y);

                if (Stroke is SolidColorBrush solidColorBrush)
                    canvas.StrokeColor = solidColorBrush.Color;

                canvas.StrokeSize = (float)StrokeThickness;
                canvas.StrokeLineCap = StrokeLineCap.ToLineJoin();
                canvas.StrokeLineJoin = StrokeLineJoin.ToLineJoin();
                canvas.StrokeDashPattern = StrokeDashArray.ToStrokeDashPattern();
                canvas.StrokeDashOffset = (float)StrokeDashOffset;
                canvas.MiterLimit = (float)StrokeMiterLimit;

                var path = GetPath();

                canvas.DrawPath(path);
            }

            canvas.RestoreState();
        }

        PathF GetPath()
        {
            var path = new PathF();

            if (Points?.Count > 0)
            {
                path.MoveTo((float)Points[0].X, (float)Points[0].Y);

                for (int index = 1; index < Points.Count; index++)
                    path.LineTo((float)Points[index].X, (float)Points[index].Y);
            }

            return path;
        }
    }
}

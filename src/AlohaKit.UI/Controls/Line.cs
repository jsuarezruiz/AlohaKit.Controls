using AlohaKit.UI.Extensions;

namespace AlohaKit.UI
{
    public class Line : Shape
    {
        public static readonly BindableProperty X1Property =
            BindableProperty.Create(nameof(X1), typeof(double), typeof(Line), 0.0d);

        public static readonly BindableProperty Y1Property =
            BindableProperty.Create(nameof(Y1), typeof(double), typeof(Line), 0.0d);

        public static readonly BindableProperty X2Property =
            BindableProperty.Create(nameof(X2), typeof(double), typeof(Line), 0.0d);

        public static readonly BindableProperty Y2Property =
            BindableProperty.Create(nameof(Y2), typeof(double), typeof(Line), 0.0d);

        public double X1
        {
            set { SetValue(X1Property, value); }
            get { return (double)GetValue(X1Property); }
        }

        public double Y1
        {
            set { SetValue(Y1Property, value); }
            get { return (double)GetValue(Y1Property); }
        }

        public double X2
        {
            set { SetValue(X2Property, value); }
            get { return (double)GetValue(X2Property); }
        }
     
        public double Y2
        {
            set { SetValue(Y2Property, value); }
            get { return (double)GetValue(Y2Property); }
        }

        public override void Draw(ICanvas canvas, RectF bounds)
        {
            canvas.SaveState();

            base.Draw(canvas, bounds);

            if (Stroke != null)
            {
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
            path.MoveTo((float)X1, (float)Y1);
            path.LineTo((float)X2, (float)Y2);

            return path;
        }
    }
}

using AlohaKit.UI.Extensions;
using Microsoft.Maui.Controls.Shapes;
using System.ComponentModel;

namespace AlohaKit.UI
{
    public class Path : Shape
    {
        public static readonly BindableProperty DataProperty =
            BindableProperty.Create(nameof(Data), typeof(Geometry), typeof(Path), null);

        [TypeConverter(typeof(PathGeometryConverter))]
        public Geometry Data
        {
            set { SetValue(DataProperty, value); }
            get { return (Geometry)GetValue(DataProperty); }
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

            Data?.AppendPath(path);

            return path;
        }
    }
}
using Microsoft.Maui.Controls.Shapes;

namespace AlohaKit.UI.Extensions
{
    public static class ShapeExtensions
    {
        public static T Fill<T>(this T shape, Brush fill) where T : Shape
        {
            shape.Fill = fill;

            return shape;
        }

        public static T Stroke<T>(this T shape, Brush stroke) where T : Shape
        {
            shape.Stroke = stroke;

            return shape;
        }

        public static T StrokeThickness<T>(this T shape, double strokeThickness) where T : Shape
        {
            shape.StrokeThickness = strokeThickness;

            return shape;
        }

        public static LineCap ToLineJoin(this PenLineCap penLineCap)
        {
            switch (penLineCap)
            {
                case PenLineCap.Flat:
                    return LineCap.Butt;
                case PenLineCap.Round:
                    return LineCap.Round;
                case PenLineCap.Square:
                    return LineCap.Square;
                default:
                    return LineCap.Butt;
            };
        }

        public static LineJoin ToLineJoin(this PenLineJoin penLineJoin)
        {
            switch(penLineJoin)
            {
                case PenLineJoin.Round:
                    return LineJoin.Round;
                case PenLineJoin.Bevel:
                    return LineJoin.Bevel;
                case PenLineJoin.Miter:
                    return LineJoin.Miter;
                default:
                    return LineJoin.Miter;
            };
        }

        public static float[] ToStrokeDashPattern(this DoubleCollection strokeDashArray)
        {
            float[] result = new float[strokeDashArray.Count];
            
            for(int i = 0; i < strokeDashArray.Count; i++)
            {
                result[i] = (float)strokeDashArray[i];
            }

            return result;
        }
    }
}

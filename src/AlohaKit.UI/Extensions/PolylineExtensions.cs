namespace AlohaKit.UI.Extensions
{
    public static class PolylineExtensions
    {
        public static T Points<T>(this T polyline, PointCollection points) where T : Polyline
        {
            polyline.Points = points;

            return polyline;
        }

        public static T Points<T>(this T polyline, Point[] points) where T : Polyline
        {
            polyline.Points = new PointCollection(points);

            return polyline;
        }
    }
}

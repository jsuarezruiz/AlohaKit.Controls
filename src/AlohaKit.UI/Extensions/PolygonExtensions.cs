namespace AlohaKit.UI.Extensions
{
    public static class PolygonExtensions
    {
        public static T Points<T>(this T polygon, PointCollection points) where T : Polygon
        {
            polygon.Points = points;

            return polygon;
        }

        public static T Points<T>(this T polygon, Point[] points) where T : Polygon
        {
            polygon.Points = new PointCollection(points);

            return polygon;
        }
    }
}

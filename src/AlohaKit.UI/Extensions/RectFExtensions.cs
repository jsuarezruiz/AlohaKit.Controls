namespace AlohaKit.UI.Extensions
{
	public static class RectFExtensions
	{
		public static bool Contains(this RectF rect, Point point) =>
			point.X >= 0 && point.X <= rect.Width &&
			point.Y >= 0 && point.Y <= rect.Height;

		public static bool ContainsAny(this RectF rect, Point[] points)
			=> points.Any(x => rect.Contains(x));

		public static bool ContainsAny(this RectF rect, PointF[] points)
			=> points.Any(rect.Contains);
	}
}
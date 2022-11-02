namespace AlohaKit.UI.Extensions
{
	public static class GestureExtensions
	{
		public static bool TouchInside<T>(this T view, PointF touchPoint) where T : View
		{
			var bounds = new RectF(view.X, view.Y, view.WidthRequest, view.HeightRequest);

			if (bounds.Contains(touchPoint))
				return true;

			return false;
		}
	}
}
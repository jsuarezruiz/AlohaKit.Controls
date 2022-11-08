namespace AlohaKit.UI.Extensions
{
	public static class GestureExtensions
	{
		public static bool IsInsideBounds<T>(this T view, PointF touchPoint) where T : View
		{
			if (view == null)
				return false;

			var minimumTouchSize = 24f;

			var width = view.WidthRequest;
			if (float.IsNaN(width))
				width = minimumTouchSize;

			var height = view.HeightRequest;
			if (float.IsNaN(height))
				height = minimumTouchSize;

			var bounds = new RectF(view.X, view.Y, width, height);

			if (bounds.Contains(touchPoint))
				return true;

			return false;
		}
	}
}
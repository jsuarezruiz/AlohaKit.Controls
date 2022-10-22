namespace AlohaKit.UI.Extensions
{
    public static class ViewExtensions
    {
        public static T IsVisible<T>(this T view, bool isVisible) where T : View
        {
            view.IsVisible = isVisible;

            return view;
        }

        public static T X<T>(this T view, float x) where T : View
        {
            view.X = x;

            return view;
        }

        public static T Y<T>(this T view, float y) where T : View
        {
            view.Y = y;

            return view;
        }

        public static T Height<T>(this T view, float height) where T : View
        {
            view.HeightRequest = height;

            return view;
        }

        public static T Width<T>(this T view, float width) where T : View
        {
            view.WidthRequest = width;

            return view;
        }

        public static T Background<T>(this T view, Brush background) where T : View
        {
            view.Background = background;

            return view;
        }
    }
}

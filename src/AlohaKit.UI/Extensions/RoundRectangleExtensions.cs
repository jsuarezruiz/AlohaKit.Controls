namespace AlohaKit.UI.Extensions
{
    public static class RoundRectangleExtensions
    {
        public static T CornerRadius<T>(this T roundRectangle, CornerRadius cornerRadius) where T : RoundRectangle
        {
            roundRectangle.CornerRadius = cornerRadius;

            return roundRectangle;
        }
    }
}

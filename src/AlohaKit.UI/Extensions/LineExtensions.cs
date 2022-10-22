namespace AlohaKit.UI.Extensions
{
    public static class LineExtensions
    {
        public static T X1<T>(this T line, double x1) where T : Line
        {
            line.X1 = x1;

            return line;
        }

        public static T X2<T>(this T line, double x2) where T : Line
        {
            line.X2 = x2;

            return line;
        }

        public static T Y1<T>(this T line, double y1) where T : Line
        {
            line.Y1 = y1;

            return line;
        }

        public static T Y2<T>(this T line, double y2) where T : Line
        {
            line.Y2 = y2;

            return line;
        }
    }
}

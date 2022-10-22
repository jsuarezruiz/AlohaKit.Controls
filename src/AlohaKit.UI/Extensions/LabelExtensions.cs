namespace AlohaKit.UI.Extensions
{
    public static class LabelExtensions
    {
        public static T Text<T>(this T label, string text) where T : Label
        {
            label.Text = text;

            return label;
        }

        public static T TextColor<T>(this T label, Color textColor) where T : Label
        {
            label.TextColor = textColor;

            return label;
        }

        public static T FontSize<T>(this T label, double fontSize) where T : Label
        {
            label.FontSize = fontSize;

            return label;
        }
    }
}
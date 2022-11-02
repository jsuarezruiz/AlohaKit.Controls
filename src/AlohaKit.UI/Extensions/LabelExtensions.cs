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

		public static HorizontalAlignment ToHorizontalAlignment(this TextAlignment textAlignment)
		{
			switch(textAlignment)
            {
                case TextAlignment.Start:
                    return HorizontalAlignment.Left;
				case TextAlignment.Center:
					return HorizontalAlignment.Center;
				case TextAlignment.End:
					return HorizontalAlignment.Right;
                default:
					return HorizontalAlignment.Left;
			}
		}

		public static VerticalAlignment ToVerticalAlignment(this TextAlignment textAlignment)
		{
			switch (textAlignment)
			{
				case TextAlignment.Start:
					return VerticalAlignment.Top;
				case TextAlignment.Center:
					return VerticalAlignment.Center;
				case TextAlignment.End:
					return VerticalAlignment.Bottom;
				default:
					return VerticalAlignment.Top;
			}
		}
	}
}
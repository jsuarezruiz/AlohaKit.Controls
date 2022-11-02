using AlohaKit.UI.Extensions;

namespace AlohaKit.UI
{
    public class Label : View
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Label), string.Empty,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Label), null,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty FontSizeProperty =
           BindableProperty.Create(nameof(FontSize), typeof(double), typeof(Label), 12.0d,
               propertyChanged: InvalidatePropertyChanged);

		public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(Label), TextAlignment.Start,
                propertyChanged: InvalidatePropertyChanged);

		public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(Label), TextAlignment.Start,
                propertyChanged: InvalidatePropertyChanged);

		public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

		public TextAlignment HorizontalTextAlignment
		{
			get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
			set { SetValue(HorizontalTextAlignmentProperty, value); }
		}

		public TextAlignment VerticalTextAlignment
		{
			get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
			set { SetValue(VerticalTextAlignmentProperty, value); }
		}

		public override void Draw(ICanvas canvas, RectF bounds)
        {
            canvas.SaveState();

            base.Draw(canvas, bounds);

            if (Text != null)
            {
                canvas.FontColor = TextColor;
                canvas.FontSize = (float)FontSize;

                canvas.DrawString(Text, new Rect(X, Y, WidthRequest, HeightRequest), HorizontalTextAlignment.ToHorizontalAlignment(), VerticalTextAlignment.ToVerticalAlignment());
            }

            canvas.RestoreState();
        }
    }
}
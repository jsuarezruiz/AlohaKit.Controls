namespace AlohaKit.Gallery;

public partial class SliderView : ContentPage
{
	public SliderView()
	{
		InitializeComponent();

        UpdateBrushes();
    }

    void OnMinimumBrushStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnMinimumBrushEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnMaximumBrushStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnMaximumBrushEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnThumbBrushStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnThumbBrushEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void UpdateBrushes()
    {
        var minimumStartColor = GetColorFromString(MinimumBrushStartColorEntry.Text);
        var minimumEndColor = GetColorFromString(MinimumBrushEndColorEntry.Text);

        if (minimumStartColor != null && minimumEndColor != null)
        {
            MinimumBrushStartColorEntry.BackgroundColor = minimumStartColor;
            MinimumBrushEndColorEntry.BackgroundColor = minimumEndColor;

            Slider.MinimumBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = minimumStartColor, Offset = 0 },
                    new GradientStop { Color = minimumEndColor, Offset = 1 }
                }
            };
        }

        var maximumStartColor = GetColorFromString(MaximumBrushStartColorEntry.Text);
        var maximumEndColor = GetColorFromString(MaximumBrushEndColorEntry.Text);

        if (maximumStartColor != null && maximumEndColor != null)
        {
            MaximumBrushStartColorEntry.BackgroundColor = maximumStartColor;
            MaximumBrushEndColorEntry.BackgroundColor = maximumEndColor;

            Slider.MaximumBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = maximumStartColor, Offset = 0 },
                    new GradientStop { Color = maximumEndColor, Offset = 1 }
                }
            };
        }

        var thumbStartColor = GetColorFromString(ThumbBrushStartColorEntry.Text);
        var thumbEndColor = GetColorFromString(ThumbBrushEndColorEntry.Text);

        if (thumbStartColor != null && thumbEndColor != null)
        {
            ThumbBrushStartColorEntry.BackgroundColor = thumbStartColor;
            ThumbBrushEndColorEntry.BackgroundColor = thumbEndColor;

            Slider.ThumbBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = thumbStartColor, Offset = 0 },
                    new GradientStop { Color = thumbEndColor, Offset = 1 }
                }
            };
        }
    }

    Color GetColorFromString(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        try
        {
            return Color.FromArgb(value[0].Equals('#') ? value : $"#{value}");
        }
        catch (Exception)
        {
            return null;
        }
    }
}
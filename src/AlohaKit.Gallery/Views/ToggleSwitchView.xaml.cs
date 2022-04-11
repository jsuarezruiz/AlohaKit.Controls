namespace AlohaKit.Gallery;

public partial class ToggleSwitchView : ContentPage
{
    public ToggleSwitchView()
    {
        InitializeComponent();

        UpdateBrushes();
    }

    void OnTrackStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnTrackEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnThumbStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnThumbEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void UpdateBrushes()
    {
        var trackStartColor = GetColorFromString(TrackStartColorEntry.Text);
        var trackEndColor = GetColorFromString(TrackEndColorEntry.Text);

        if (trackStartColor != null && trackEndColor != null)
        {
            TrackStartColorEntry.BackgroundColor = trackStartColor;
            TrackEndColorEntry.BackgroundColor = trackEndColor;

            ToggleSwitch.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = trackStartColor, Offset = 0 },
                    new GradientStop { Color = trackEndColor, Offset = 1 }
                }
            };
        }

        var thumbStartColor = GetColorFromString(ThumbStartColorEntry.Text);
        var thumbEndColor = GetColorFromString(ThumbEndColorEntry.Text);

        if (thumbStartColor != null && thumbEndColor != null)
        {
            ThumbStartColorEntry.BackgroundColor = thumbStartColor;
            ThumbEndColorEntry.BackgroundColor = thumbEndColor;

            ToggleSwitch.ThumbBrush = new LinearGradientBrush
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
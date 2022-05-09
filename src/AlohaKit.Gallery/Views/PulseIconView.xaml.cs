namespace AlohaKit.Gallery;

public partial class PulseIconView : ContentPage
{
	public PulseIconView()
	{
		InitializeComponent();

        UpdateColors();
    }

    void OnPulseColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnBackgroundColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void UpdateColors()
    {
        var backgroundColor = GetColorFromString(BackgroundColorEntry.Text);

        if (backgroundColor != null)
        {
            BackgroundColorEntry.BackgroundColor = backgroundColor;
            PulseIcon.Background = new SolidColorBrush(backgroundColor);
        }

        var pulseColor = GetColorFromString(PulseColorEntry.Text);

        if (pulseColor != null)
        {
            PulseIcon.PulseColor = PulseColorEntry.BackgroundColor = pulseColor;
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
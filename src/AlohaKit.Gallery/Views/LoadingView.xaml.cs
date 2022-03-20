namespace AlohaKit.Gallery;

public partial class LoadingView : ContentPage
{
    public LoadingView()
    {
        InitializeComponent();

        UpdateColors();
    }

    void OnBackgroundColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnShadowColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void UpdateColors()
    {
        var backgroundColor = GetColorFromString(BackgroundColorEntry.Text);

        if (backgroundColor != null)
        {
            BackgroundColorEntry.BackgroundColor = BusyIndicator.BackgroundColor = backgroundColor;
        }

        var color = GetColorFromString(ColorEntry.Text);

        if (color != null)
        {
            ColorEntry.BackgroundColor = BusyIndicator.Color = color;
        }

        var shadowColor = GetColorFromString(ShadowColorEntry.Text);

        if (shadowColor != null)
        {
            ShadowColorEntry.BackgroundColor = BusyIndicator.ShadowColor = shadowColor;
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
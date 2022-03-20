namespace AlohaKit.Gallery;

public partial class ProgressRadialView : ContentPage
{
	public ProgressRadialView()
	{
		InitializeComponent();

        UpdateColors();
    }

    void OnBackgroundColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnProgressColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnTextColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void UpdateColors()
    {
        var backgroundColor = GetColorFromString(BackgroundColorEntry.Text);

        if (backgroundColor != null)
        {
            BackgroundColorEntry.BackgroundColor = ProgressRadial.BackgroundColor = backgroundColor;
        }

        var progressColor = GetColorFromString(ProgressColorEntry.Text);

        if (progressColor != null)
        {
            ProgressColorEntry.BackgroundColor = ProgressRadial.ProgressColor = progressColor;
        }

        var textColor = GetColorFromString(TextColorEntry.Text);

        if (textColor != null)
        {
            TextColorEntry.BackgroundColor = ProgressRadial.TextColor = textColor;
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
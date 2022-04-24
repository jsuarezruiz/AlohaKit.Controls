namespace AlohaKit.Gallery;

public partial class NumericUpDownView : ContentPage
{
	public NumericUpDownView()
	{
		InitializeComponent();

        UpdateColors();
    }

    void OnColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnTextColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void UpdateColors()
    {
        var color = GetColorFromString(ColorEntry.Text);

        if (color != null)
        {
            ColorEntry.BackgroundColor = color;

            NumericUpDown.Color = color;
        }

        var textColor = GetColorFromString(TextColorEntry.Text);

        if (textColor != null)
        {
            TextColorEntry.BackgroundColor = textColor;

            NumericUpDown.TextColor = textColor;
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
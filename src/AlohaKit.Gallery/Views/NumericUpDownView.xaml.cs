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

    void OnMaximumColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnMinusColorEntryTextChanged(object sender, TextChangedEventArgs e)
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

        var maximumColor = GetColorFromString(MaximumColorEntry.Text);

        if (maximumColor != null)
        {
            MaximumColorEntry.BackgroundColor = maximumColor;

            NumericUpDown.ColorMaximum = maximumColor;
        }

        var minusColor = GetColorFromString(MinusColorEntry.Text);

        if (minusColor != null)
        {
            MinusColorEntry.BackgroundColor = minusColor;

            NumericUpDown.ColorMinus = minusColor;
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
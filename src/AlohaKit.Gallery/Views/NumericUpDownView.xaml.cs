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

    void OnMinimumColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }    
    
    void OnMaximumTextColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }    
    
    void OnMinimumTextColorTextChanged(object sender, TextChangedEventArgs e)
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

            NumericUpDown.MaximumColor = maximumColor;
        }

        var minimumColor = GetColorFromString(MinimumColorEntry.Text);

        if (minimumColor != null)
        {
            MinimumColorEntry.BackgroundColor = minimumColor;

            NumericUpDown.MinimumColor = minimumColor;
        }        
        
        var maximumTextColor = GetColorFromString(MaximumTextColorEntry.Text);

        if (maximumTextColor != null)
        {
            MaximumTextColorEntry.BackgroundColor = maximumTextColor;

            NumericUpDown.MaximumTextColor = maximumTextColor;
        }

        var minimumTextColor = GetColorFromString(MinimumTextColorEntry.Text);

        if (minimumTextColor != null)
        {
            MinimumTextColorEntry.BackgroundColor = minimumTextColor;

            NumericUpDown.MinimumTextColor = minimumTextColor;
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
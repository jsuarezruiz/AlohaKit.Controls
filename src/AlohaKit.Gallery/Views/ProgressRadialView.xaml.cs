using AlohaKit.Controls;

namespace AlohaKit.Gallery;

public partial class ProgressRadialView : ContentPage
{
    public ProgressRadialView()
    {
        InitializeComponent();
        UpdateColors();
        checkBox.CheckedChanged += CheckBox_CheckedChanged;
        checkBox.IsChecked = ProgressRadial.Direction == ProgressRadialDirection.LeftToRight ? true : false;
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        ProgressRadial.Direction = e.Value ? ProgressRadialDirection.LeftToRight : ProgressRadialDirection.RightToLeft;
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
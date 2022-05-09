namespace AlohaKit.Gallery;

public partial class CheckBoxView : ContentPage
{
	public CheckBoxView()
	{
		InitializeComponent();
        
        UpdateColors();
    }

    void OnCheckedColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnUncheckedColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnStrokeColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void UpdateColors()
    {
        var checkedColor = GetColorFromString(CheckedColorEntry.Text);

        if (checkedColor != null)
        {
            CheckedColorEntry.BackgroundColor = checkedColor;

            CheckBox.CheckedBrush = new SolidColorBrush(checkedColor);
        }

        var uncheckedColor = GetColorFromString(UncheckedColorEntry.Text);

        if (uncheckedColor != null)
        {
            UncheckedColorEntry.BackgroundColor = uncheckedColor;

            CheckBox.UncheckedBrush = new SolidColorBrush(uncheckedColor);
        }

        var strokeColor = GetColorFromString(StrokeColorEntry.Text);

        if (uncheckedColor != null)
        {
            StrokeColorEntry.BackgroundColor = strokeColor;

            CheckBox.Stroke = new SolidColorBrush(strokeColor);
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
namespace AlohaKit.Gallery;

public partial class RatingView : ContentPage
{
	public RatingView()
	{
		InitializeComponent();
        
        UpdateColors();
    }

    void OnSelectedFillColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnUnSelectedFillColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnSelectedStrokeColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void OnUnSelectedStrokeColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateColors();
    }

    void UpdateColors()
    {
        var selectedFillColor = GetColorFromString(SelectedFillColorEntry.Text);

        if (selectedFillColor != null)
        {
            Rating.SelectedFill = SelectedFillColorEntry.BackgroundColor = selectedFillColor;
        }

        var unSelectedFillColor = GetColorFromString(UnSelectedFillColorEntry.Text);

        if (unSelectedFillColor != null)
        {
            Rating.UnSelectedFill = UnSelectedFillColorEntry.BackgroundColor = unSelectedFillColor;
        }

        var selectedStrokeColor = GetColorFromString(SelectedStrokeColorEntry.Text);

        if (selectedStrokeColor != null)
        {
            Rating.SelectedStroke = SelectedStrokeColorEntry.BackgroundColor = selectedStrokeColor;
        }

        var unSelectedStrokeColor = GetColorFromString(UnSelectedStrokeColorEntry.Text);

        if (unSelectedStrokeColor != null)
        {
            Rating.UnSelectedStroke = UnSelectedStrokeColorEntry.BackgroundColor = unSelectedStrokeColor;
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
namespace AlohaKit.Gallery;

public partial class LinearGaugeView : ContentPage
{
	public LinearGaugeView()
	{
		InitializeComponent();

        UpdateBrushes();
    }

    void OnFillStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnFillEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void UpdateBrushes()
    {
        var fillStartColor = GetColorFromString(FillStartColorEntry.Text);
        var fillEndColor = GetColorFromString(FillEndColorEntry.Text);

        if (fillStartColor != null && fillEndColor != null)
        {
            FillStartColorEntry.BackgroundColor = fillStartColor;
            FillEndColorEntry.BackgroundColor = fillEndColor;

            LinearGauge.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 1),
                EndPoint = new Point(0, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = fillStartColor, Offset = 0 },
                    new GradientStop { Color = fillEndColor, Offset = 1 }
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
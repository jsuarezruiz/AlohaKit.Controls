namespace AlohaKit.Gallery;

public partial class ProgressBarView : ContentPage
{
    public ProgressBarView()
    {
        InitializeComponent();

        UpdateBrushes();
        StylePicker.SelectedItem = "Rounded";
        CheckAnimate.IsChecked = true;
    }

    void OnBackgroundStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnBackgroundEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnProgressStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void OnProgressEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateBrushes();
    }

    void UpdateBrushes()
    {
        var backgroundStartColor = GetColorFromString(BackgroundStartColorEntry.Text);
        var backgroundEndColor = GetColorFromString(BackgroundEndColorEntry.Text);

        if (backgroundStartColor != null && backgroundEndColor != null)
        {
            BackgroundStartColorEntry.BackgroundColor = backgroundStartColor;
            BackgroundEndColorEntry.BackgroundColor = backgroundEndColor;

            HorizontalProgressBar.Background = VerticalProgressBar.Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new Microsoft.Maui.Controls.GradientStop { Color = backgroundStartColor, Offset = 0 },
                    new Microsoft.Maui.Controls.GradientStop { Color = backgroundEndColor, Offset = 1 }
                }
            };
        }
        var progressStartColor = GetColorFromString(ProgressStartEntry.Text);
        var progressEndColor = GetColorFromString(ProgressEndEntry.Text);

        if (progressStartColor != null && progressEndColor != null)
        {
            ProgressStartEntry.BackgroundColor = progressStartColor;
            ProgressEndEntry.BackgroundColor = progressEndColor;

            HorizontalProgressBar.ProgressBrush = VerticalProgressBar.ProgressBrush = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new Microsoft.Maui.Controls.GradientStop { Color = progressStartColor, Offset = 0 },
                    new Microsoft.Maui.Controls.GradientStop { Color = progressEndColor, Offset = 1 }
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

    private void stylePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = (sender as Picker).SelectedItem as string;
        HorizontalProgressBar.RoundCorners = selectedItem == "Rounded" ? true : false;
        VerticalProgressBar.RoundCorners = selectedItem == "Rounded" ? true : false;
    }

    private void CheckAnimate_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var isChecked = (sender as CheckBox).IsChecked;
        HorizontalProgressBar.EnableAnimations = isChecked;
        VerticalProgressBar.EnableAnimations = isChecked;
    }
}
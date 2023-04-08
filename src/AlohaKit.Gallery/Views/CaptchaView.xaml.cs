namespace AlohaKit.Gallery.Views;

public partial class CaptchaView : ContentPage
{
	public CaptchaView()
	{
		InitializeComponent();
		UpdateColors();
	}

	void OnTextColorEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateColors();
	}

	void UpdateColors()
	{
		var textColor = GetColorFromString(TextColorEntry.Text);

		if (textColor != null)
		{
			TextColorEntry.BackgroundColor = textColor;

			Captcha.TextColor = textColor;
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
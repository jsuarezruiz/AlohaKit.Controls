using System.Diagnostics;

namespace AlohaKit.Gallery;

public partial class ButtonView : ContentPage
{
	public ButtonView()
	{
		InitializeComponent();

		UpdateBrushes();
		UpdateShadowColor();
	}

	void OnButtonClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("Button Clicked");
	}

	void OnBackgroundStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateBrushes();
	}

	void OnBackgroundEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateBrushes();
	}

	void OnStrokeStartColorEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateBrushes();
	}

	void OnStrokeEndColorEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateBrushes();
	}

	void OnShadowColorEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateShadowColor();
	}

	void OnHorizontalAlignPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		UpdateTextAlignment();
	}

	void OnVerticalAlignPickerSelectedIndexChanged(object sender, EventArgs e)
	{
		UpdateTextAlignment();
	}

	void OnFontSizeEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateFontSize();
	}

	void UpdateBrushes()
	{
		var backgroundStartColor = GetColorFromString(BackgroundStartColorEntry.Text);
		var backgroundEndColor = GetColorFromString(BackgroundEndColorEntry.Text);

		if (backgroundStartColor != null && backgroundEndColor != null)
		{
			BackgroundStartColorEntry.BackgroundColor = backgroundStartColor;
			BackgroundEndColorEntry.BackgroundColor = backgroundEndColor;

			Button.Background = new LinearGradientBrush
			{
				StartPoint = new Point(0, 0),
				EndPoint = new Point(1, 0),
				GradientStops = new GradientStopCollection
				{
					new GradientStop { Color = backgroundStartColor, Offset = 0 },
					new GradientStop { Color = backgroundEndColor, Offset = 1 }
				}
			};
		}

		var strokeStartColor = GetColorFromString(StrokeStartColorEntry.Text);
		var strokeEndColor = GetColorFromString(StrokeEndColorEntry.Text);

		if (strokeStartColor != null && strokeEndColor != null)
		{
			StrokeStartColorEntry.BackgroundColor = strokeStartColor;
			StrokeEndColorEntry.BackgroundColor = strokeEndColor;

			Button.Stroke = new LinearGradientBrush
			{
				StartPoint = new Point(0, 0),
				EndPoint = new Point(1, 0),
				GradientStops = new GradientStopCollection
				{
					new GradientStop { Color = strokeStartColor, Offset = 0 },
					new GradientStop { Color = strokeEndColor, Offset = 1 }
				}
			};
		}
	}

	void UpdateShadowColor()
	{
		var shadowColor = GetColorFromString(ShadowColorEntry.Text);

		if (shadowColor != null)
		{
			Button.ShadowColor = ShadowColorEntry.BackgroundColor = shadowColor;
		}
	}

	void UpdateTextAlignment()
	{
		var horizontalAlignSelectedIndex = HorizontalAlignPicker.SelectedIndex;

		TextAlignment horizontalTextAlignment = TextAlignment.Center;

		switch (horizontalAlignSelectedIndex)
		{
			case 0:
				horizontalTextAlignment = TextAlignment.Start;
				break;
			case 1:
				horizontalTextAlignment = TextAlignment.Center;
				break;
			case 2:
				horizontalTextAlignment = TextAlignment.End;
				break;
		}

		Button.HorizontalTextAlignment = horizontalTextAlignment;

		var verticalAlignSelectedIndex = VerticalAlignPicker.SelectedIndex;

		TextAlignment verticalTextAlignment = TextAlignment.Center;

		switch (verticalAlignSelectedIndex)
		{
			case 0:
				verticalTextAlignment = TextAlignment.Start;
				break;
			case 1:
				verticalTextAlignment = TextAlignment.Center;
				break;
			case 2:
				verticalTextAlignment = TextAlignment.End;
				break;
		}

		Button.VerticalTextAlignment = verticalTextAlignment;
	}

	void UpdateFontSize()
	{
		if (float.TryParse(FontSizeEntry.Text, out var fontSize))
		{
			Button.FontSize = fontSize;
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
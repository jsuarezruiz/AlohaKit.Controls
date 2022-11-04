using System.Diagnostics;
using AlohaKit.UI.Gallery.Helpers;

namespace AlohaKit.UI.Gallery.Views;

public partial class LolBenchmarkPage : ContentPage
{
	public LolBenchmarkPage()
	{
		InitializeComponent();
	}

	volatile bool breakTest = false;
	const int Max = 500;

	void StartTestCanvasView()
	{
		var rand = new Random2(0);

		breakTest = false;

		var width = CanvasView.Width;
		var height = CanvasView.Height;

		const int step = 20;
		var labels = new Label[step * 2];

		var processed = 0;

		long prevTicks = 0;
		long prevMs = 0;
		int prevProcessed = 0;
		double avgSum = 0;
		int avgN = 0;
		var sw = new Stopwatch();

		Action loop = null;

		loop = () =>
		{
			var now = sw.ElapsedMilliseconds;

			if (breakTest)
			{
				var avg = avgSum / avgN;
				LolLabel.Text = string.Format("{0:0.00} LOL/s (AVG)", avg).PadLeft(21);
				return;
			}

			// 60hz, 16ms to build the frame
			while (sw.ElapsedMilliseconds - now < 16)
			{
				var label = new Label()
				{
					Text = "lol?",
					TextColor = new Color((float)rand.NextDouble(), (float)rand.NextDouble(), (float)rand.NextDouble()),
					Rotation = (float)rand.NextDouble() * 360
				};

				label.X = (float)(rand.NextDouble() * width);
				label.Y = (float)(rand.NextDouble() * height);
				label.WidthRequest = 80f;
				label.HeightRequest = 24f;

				if (processed > Max)
				{
					CanvasView.Children.RemoveAt(0);
				}

				CanvasView.Children.Add(label);
				CanvasView.Invalidate();

				processed++;

				if (sw.ElapsedMilliseconds - prevMs > 500)
				{

					var r = (processed - prevProcessed) / ((double)(sw.ElapsedTicks - prevTicks) / Stopwatch.Frequency);
					prevTicks = sw.ElapsedTicks;
					prevProcessed = processed;

					if (processed > Max)
					{
						LolLabel.Text = string.Format("{0:0.00} LOL/s", r).PadLeft(15);
						avgSum += r;
						avgN++;
					}

					prevMs = sw.ElapsedMilliseconds;
				}
			}

			Dispatcher.Dispatch(loop);
		};

		sw.Start();

		loop();
	}


	void SetControlsAtStart()
	{
		StartButton.IsVisible = false;
		StopButton.IsVisible = LolLabel.IsVisible = true;
		CanvasView.Children.Clear();
		GridLayout.Children.Clear();
		LolLabel.Text = "Warming up...";
	}

	void OnStopButtonClicked(object sender, EventArgs e)
	{
		breakTest = true;
		StopButton.IsVisible = false;
		StartButton.IsVisible = true;
	}

	async void OnStartButtonClicked(object sender, EventArgs e)
	{
		int testLengthMs = 60000;
		int pauseLengthMs = 100;

		SetControlsAtStart();
		StartTestCanvasView();

		await Task.Delay(testLengthMs);
		OnStopButtonClicked(default, default);
		await Task.Delay(pauseLengthMs);
		_ = decimal.TryParse(LolLabel.Text.Replace(" LOL/s (AVG)", "").Trim(), out var resultST);

		var platformVersion = "AlohaKit UI";

#if ANDROID
		var operatingSystem = "Android";
#elif IOS
        var operatingSystem = "iOS";
#elif MACCATALYST
        var operatingSystem = "MacCatalyst";
#elif WINDOWS
        var operatingSystem = "WinUI";
#else
        var operatingSystem = "Unknown";
#endif

		var results = new { OS = operatingSystem, Platform = platformVersion, Build = resultST, Reuse = 0, Grid = 0 };
		LolLabel.Text = $"Build: {results.Build}";
	}
}
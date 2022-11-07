using System;

namespace AlohaKit.UI.Extensions
{
	public static class ColorExtensions
	{
		const float LighterFactor = 1.1f;
		const float DarkerFactor = 0.9f;

		public static Color Lighter(this Color color)
		{
			return new Color(
				color.Red * LighterFactor,
				color.Green * LighterFactor,
				color.Blue * LighterFactor,
				color.Alpha);
		}

		public static Color Darker(this Color color)
		{
			return new Color(
				color.Red * DarkerFactor,
				color.Green * DarkerFactor,
				color.Blue * DarkerFactor,
				color.Alpha);
		}
	}
}
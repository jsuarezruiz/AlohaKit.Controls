using System;
using Microsoft.Maui.Handlers;

namespace AlohaKit.UI.Hosting
{
	public static class AppHostBuilderExtensions
	{
		public static MauiAppBuilder ConfigureAlohaKitUI(this MauiAppBuilder builder)
		{
			builder.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler(typeof(SkiaGraphicsView), typeof(SkiaGraphicsViewHandler));
			});

			return builder;
		}
	}
}
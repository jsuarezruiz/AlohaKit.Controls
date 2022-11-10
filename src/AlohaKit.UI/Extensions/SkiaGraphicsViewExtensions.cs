#if IOS || MACCATALYST || ANDROID || WINDOWS
namespace AlohaKit.UI.Extensions
{
	public static class SkiaGraphicsViewExtensions
	{
		public static void UpdateDrawable(this PlatformSkiaView PlatformGraphicsView, ISkiaGraphicsView graphicsView)
		{
			PlatformGraphicsView.Drawable = graphicsView.Drawable;
		}
	}
}
#endif
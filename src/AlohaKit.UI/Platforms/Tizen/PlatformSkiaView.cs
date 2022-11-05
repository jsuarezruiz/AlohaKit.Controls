namespace AlohaKit.UI
{
    public class PlatformSkiaView : Microsoft.Maui.Graphics.Skia.Views.SkiaGraphicsView
    {
		ISkiaGraphicsView _graphicsView;

		public PlatformSkiaView(IDrawable drawable = null) : base(drawable)
        {

		}

		public void Connect(ISkiaGraphicsView graphicsView) => _graphicsView = graphicsView;

		public void Disconnect() => _graphicsView = null;

		// TODO: Implement Touch Events
	}
}
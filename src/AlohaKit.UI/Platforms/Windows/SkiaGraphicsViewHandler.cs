using Microsoft.Maui.Handlers;
using AlohaKit.UI.Extensions;

namespace AlohaKit.UI
{
    public partial class SkiaGraphicsViewHandler : ViewHandler<ISkiaGraphicsView, PlatformSkiaView>
    {
        protected override PlatformSkiaView CreatePlatformView()
        {
            return new PlatformSkiaView();
        }

        public static void MapDrawable(SkiaGraphicsViewHandler handler, ISkiaGraphicsView graphicsView)
        {
            handler.PlatformView?.UpdateDrawable(graphicsView);
        }

        public static void MapInvalidate(SkiaGraphicsViewHandler handler, ISkiaGraphicsView graphicsView, object? arg)
        {
            handler.PlatformView?.Invalidate();
        }
    }
}

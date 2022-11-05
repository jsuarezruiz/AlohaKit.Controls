#nullable enable
#if NET && !__IOS__ && !__ANDROID__ && !WINDOWS
using Microsoft.Maui.Handlers;
using System;

namespace AlohaKit.UI
{
    public partial class SkiaGraphicsViewHandler : ViewHandler<ISkiaGraphicsView, object>
    {
        protected override object CreatePlatformView() => throw new NotImplementedException();

        public static void MapDrawable(SkiaGraphicsViewHandler handler, ISkiaGraphicsView graphicsView) { }

        public static void MapInvalidate(SkiaGraphicsViewHandler handler, ISkiaGraphicsView graphicsView, object? arg) { }
    }
}
#endif
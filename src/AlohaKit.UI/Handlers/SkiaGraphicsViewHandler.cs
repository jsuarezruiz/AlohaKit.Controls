#nullable enable
using Microsoft.Maui.Handlers;

namespace AlohaKit.UI
{
    public partial class SkiaGraphicsViewHandler
    {
        public static IPropertyMapper<ISkiaGraphicsView, SkiaGraphicsViewHandler> Mapper = new PropertyMapper<ISkiaGraphicsView, SkiaGraphicsViewHandler>(ViewHandler.ViewMapper)
        {
            [nameof(ISkiaGraphicsView.Drawable)] = MapDrawable
        };

        public static CommandMapper<ISkiaGraphicsView, SkiaGraphicsViewHandler> CommandMapper = new(ViewHandler.ViewCommandMapper)
        {
            [nameof(ISkiaGraphicsView.Invalidate)] = MapInvalidate
        };

        public SkiaGraphicsViewHandler() : base(Mapper, CommandMapper)
        {
        }

        public SkiaGraphicsViewHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null)
            : base(mapper ?? Mapper, commandMapper ?? CommandMapper)
        {

		}

#if IOS || MACCATALYST || ANDROID || WINDOWS
		protected override void ConnectHandler(PlatformSkiaView platformView)
		{
			platformView.Connect(VirtualView);

			base.ConnectHandler(platformView);
		}

		protected override void DisconnectHandler(PlatformSkiaView platformView)
		{
			platformView.Disconnect();

			base.DisconnectHandler(platformView);
		}
#endif
	}
}
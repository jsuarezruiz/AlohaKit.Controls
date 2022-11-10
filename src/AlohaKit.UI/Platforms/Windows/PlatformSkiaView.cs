using Microsoft.UI.Xaml.Input;

namespace AlohaKit.UI
{
	public class PlatformSkiaView : Microsoft.Maui.Graphics.Skia.Views.SkiaGraphicsView
	{
		ISkiaGraphicsView _graphicsView;
		bool _isTouching;
		bool _isInBounds;

		public PlatformSkiaView(IDrawable drawable = null) : base(drawable)
		{

		}

		public void Connect(ISkiaGraphicsView graphicsView) => _graphicsView = graphicsView;

		public void Disconnect() => _graphicsView = null;

		protected override void OnPointerEntered(PointerRoutedEventArgs e)
		{
			_isInBounds = true;
			_graphicsView?.StartHoverInteraction(GetViewPoints(e));
		}

		protected override void OnPointerCanceled(PointerRoutedEventArgs e)
		{
			if (_isTouching)
			{
				_isTouching = false;
				_graphicsView?.EndInteraction(GetViewPoints(e), _isInBounds);
				_graphicsView?.CancelInteraction();
			}
		}

		protected override void OnPointerExited(PointerRoutedEventArgs e)
		{
			_isInBounds = false;

			_graphicsView?.EndHoverInteraction();

			if (_isTouching)
			{
				_isTouching = false;
				_graphicsView?.EndInteraction(GetViewPoints(e), _isInBounds);
			}
		}

		protected override void OnPointerMoved(PointerRoutedEventArgs e)
		{
			var points = GetViewPoints(e);

			_graphicsView?.MoveHoverInteraction(points);

			if (_isTouching)
				_graphicsView?.DragInteraction(points);
		}

		protected override void OnPointerPressed(PointerRoutedEventArgs e)
		{
			var points = GetViewPoints(e);
			_isTouching = true;
			_graphicsView?.StartInteraction(points);
		}

		protected override void OnPointerReleased(PointerRoutedEventArgs e)
		{
			var points = GetViewPoints(e);

			if (_isTouching)
			{
				_isTouching = false;
			}
		}

		PointF[] GetViewPoints(PointerRoutedEventArgs e)
		{
			var point = e.GetCurrentPoint(this).Position;
			return new[] { new PointF((float)point.X, (float)point.Y) };
		}
	}
}
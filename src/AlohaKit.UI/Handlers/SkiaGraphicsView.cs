namespace AlohaKit.UI
{
    public class SkiaGraphicsView : Microsoft.Maui.Controls.View, ISkiaGraphicsView
	{
        public IDrawable Drawable { get; set; }

		public event EventHandler<TouchEventArgs> StartHoverInteraction;
		public event EventHandler<TouchEventArgs> MoveHoverInteraction;
		public event EventHandler EndHoverInteraction;
		public event EventHandler<TouchEventArgs> StartInteraction;
		public event EventHandler<TouchEventArgs> DragInteraction;
		public event EventHandler<TouchEventArgs> EndInteraction;
		public event EventHandler CancelInteraction;

		public void Invalidate()
        {
            Handler?.Invoke(nameof(IGraphicsView.Invalidate));
		}

		void ISkiaGraphicsView.CancelInteraction() => CancelInteraction?.Invoke(this, EventArgs.Empty);

		void ISkiaGraphicsView.DragInteraction(PointF[] points) => DragInteraction?.Invoke(this, new TouchEventArgs(points, true));

		void ISkiaGraphicsView.EndHoverInteraction() => EndHoverInteraction?.Invoke(this, EventArgs.Empty);

		void ISkiaGraphicsView.EndInteraction(PointF[] points, bool isInsideBounds) => EndInteraction?.Invoke(this, new TouchEventArgs(points, isInsideBounds));

		void ISkiaGraphicsView.StartHoverInteraction(PointF[] points) => StartHoverInteraction?.Invoke(this, new TouchEventArgs(points, true));

		void ISkiaGraphicsView.MoveHoverInteraction(PointF[] points) => MoveHoverInteraction?.Invoke(this, new TouchEventArgs(points, true));

		void ISkiaGraphicsView.StartInteraction(PointF[] points) => StartInteraction?.Invoke(this, new TouchEventArgs(points, true));
	}
}

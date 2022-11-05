namespace AlohaKit.UI
{
	public interface ISkiaGraphicsView : Microsoft.Maui.IView
	{
		IDrawable Drawable { get; }

		void Invalidate();

		void StartHoverInteraction(PointF[] points);
		void MoveHoverInteraction(PointF[] points);
		void EndHoverInteraction();
		void StartInteraction(PointF[] points);
		void DragInteraction(PointF[] points);
		void EndInteraction(PointF[] points, bool isInsideBounds);
		void CancelInteraction();
	}
}
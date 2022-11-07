using AlohaKit.UI.Extensions;

namespace AlohaKit.UI
{
    class CanvasViewDrawable : IDrawable
    {
        readonly CanvasView _owner;

        public CanvasViewDrawable(CanvasView owner)
        {
            _owner = owner;
        }

        public void Draw(ICanvas canvas, RectF bounds)
        {
            canvas.SaveState();

            _owner.DrawCore(canvas, bounds);

            canvas.RestoreState();
        }
    }

    public interface ICanvasView
    {
        void Draw(ICanvas canvas, RectF bounds);
        void Invalidate();
	}

    [ContentProperty(nameof(Children))]
    public class CanvasView : GraphicsView, ICanvasView, IDisposable
    {
        public CanvasView()
        {
            Children = new ElementsCollection(this);

            Drawable = new CanvasViewDrawable(this);

            StartInteraction += OnCanvasViewStartInteraction;
            EndInteraction += OnCanvasViewEndInteraction;
            CancelInteraction += OnCanvasViewCancelInteraction;
		}

		public ElementsCollection Children { get; internal set; }

        internal void DrawCore(ICanvas canvas, RectF bounds)
        {
            Draw(canvas, bounds);
        }

        public virtual void Draw(ICanvas canvas, RectF bounds)
        {
            foreach (var child in Children)
            {
                if (child.IsVisible)
                {
                    child.Draw(canvas, bounds);
                }
            }
        }

		void IDisposable.Dispose()
		{
			StartInteraction -= OnCanvasViewStartInteraction;
			EndInteraction -= OnCanvasViewEndInteraction;
			CancelInteraction -= OnCanvasViewCancelInteraction;
		}

        void OnCanvasViewStartInteraction(object sender, TouchEventArgs e)
        {
            var touchPoint = e.Touches[0];

            foreach (var child in Children)
            {
                if (child.IsVisible && child is View view && view.IsInsideBounds(touchPoint))
                {
                    view.StartInteraction(e.Touches);

                    foreach (var gesture in view.GestureRecognizers)
                    {
                        if (gesture is TapGestureRecognizer tapGestureRecognizer)
                            tapGestureRecognizer.SendTapped(view);
                    }
                }
            }
        }

        void OnCanvasViewEndInteraction(object sender, TouchEventArgs e)
		{
			var touchPoint = e.Touches[0];

			foreach (var child in Children)
            {
                if (child.IsVisible && child is View view && view.IsInsideBounds(touchPoint))
                {
                    view.EndInteraction(e.Touches, e.IsInsideBounds);
                }
            }
		}

        void OnCanvasViewCancelInteraction(object sender, EventArgs e)
		{
			foreach (var child in Children)
			{
				if (child.IsVisible && child is View view)
				{
					view.CancelInteraction();
				}
			}
		}
	}
}
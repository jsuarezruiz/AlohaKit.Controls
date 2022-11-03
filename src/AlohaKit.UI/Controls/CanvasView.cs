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

    [ContentProperty(nameof(Children))]
    public class CanvasView : GraphicsView
    {
        public CanvasView()
        {
            Children = new ElementsCollection();

            Drawable = new CanvasViewDrawable(this);

            StartInteraction += OnCanvasViewStartInteraction;
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

        void OnCanvasViewStartInteraction(object sender, TouchEventArgs e)
        {
            var touchPoint = e.Touches[0];

            foreach (var child in Children)
            {
                if (child.IsVisible && child is View view && view.TouchInside(touchPoint))
                {
                    foreach (var gesture in view.GestureRecognizers)
                    {
                        if (gesture is TapGestureRecognizer tapGestureRecognizer)
                            tapGestureRecognizer.SendTapped(view);
                    }
                }
            }
        }
    }
}
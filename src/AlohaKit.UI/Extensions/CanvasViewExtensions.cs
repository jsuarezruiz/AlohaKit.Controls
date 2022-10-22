namespace AlohaKit.UI.Extensions
{
    public static class CanvasViewExtensions
    {
        public static T Drawable<T>(this T canvasView, IDrawable drawable) where T : CanvasView
        {
            canvasView.Drawable = drawable;

            return canvasView;
        }

        public static T Children<T>(this T canvasView, ElementsCollection children) where T : CanvasView
        {
            canvasView.Children = children;

            return canvasView;
        }

        public static T Children<T>(this T canvasView, Element[] children) where T : CanvasView
        {
            var elementsCollection = new ElementsCollection();

            foreach (var element in children)
            {
                elementsCollection.Add(element);
            }

            canvasView.Children = elementsCollection;

            return canvasView;
        }
    }
}

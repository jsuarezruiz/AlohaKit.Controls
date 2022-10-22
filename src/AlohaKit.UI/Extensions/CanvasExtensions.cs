namespace AlohaKit.UI.Extensions
{
    public static class CanvasExtensions
    {
        public static void Transform(this ICanvas canvas, float tX, float tY, float sX, float sY)
        {
            canvas.Translate(tX, tY);
            canvas.Scale(sX, sY);
        }
    }
}
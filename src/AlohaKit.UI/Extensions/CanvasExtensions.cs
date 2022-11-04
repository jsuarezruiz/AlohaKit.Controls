namespace AlohaKit.UI.Extensions
{
    public static class CanvasExtensions
    {
        public static void Transform(this ICanvas canvas, float rotation, float tX, float tY, float sX, float sY)
        {
            canvas.Rotate(rotation);
            canvas.Translate(tX, tY);
            canvas.Scale(sX, sY);
        }
    }
}
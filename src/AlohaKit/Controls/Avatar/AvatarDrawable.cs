namespace AlohaKit.Controls
{
    public class AvatarDrawable : IDrawable
    {
        public Paint BackgroundPaint { get; set; }
        public Paint FillPaint { get; set; }
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public double FontSize { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawBackground(canvas, dirtyRect);
            DrawFill(canvas, dirtyRect);
            DrawInitials(canvas, dirtyRect);
        }

        public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();
            
            if (BackgroundPaint != null)
                canvas.SetFillPaint(BackgroundPaint, dirtyRect);

            canvas.FillRectangle(dirtyRect);

            canvas.RestoreState();
        }

        public virtual void DrawFill(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (FillPaint != null)
                canvas.SetFillPaint(FillPaint, dirtyRect);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.FillEllipse(x, y, width, height);

            canvas.RestoreState();
        }

        void DrawInitials(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = TextColor;

            canvas.FontSize = (float)FontSize;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}
using Microsoft.Maui.Animations;

namespace AlohaKit.Controls
{
    public class ToggleSwitchDrawable : IDrawable
    {
        const float ToggleSwitchShadowBlur = 2.0f;

        public Paint BackgroundPaint { get; set; }
        public Paint ThumbPaint { get; set; }
        public bool IsOn { get; set; }
        public bool HasShadow { get; set; }

        internal double AnimationPercent { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.Antialias = true;

            DrawBackground(canvas, dirtyRect);
            DrawThumb(canvas, dirtyRect);
        }

        public virtual void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(BackgroundPaint, dirtyRect);
                       
            var x = dirtyRect.X ;
            var y = dirtyRect.Y;
            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            if(HasShadow)
            {
                canvas.SetShadow(new SizeF(0, 1), ToggleSwitchShadowBlur, CanvasDefaults.DefaultShadowColor);

                x += ToggleSwitchShadowBlur / 2;
                y += ToggleSwitchShadowBlur / 2;
                height -= ToggleSwitchShadowBlur;
                width -= ToggleSwitchShadowBlur;
            }

            canvas.FillRoundedRectangle(x, y, width, height, 16.0f);

            canvas.RestoreState();
        }

        public virtual void DrawThumb(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            canvas.SetFillPaint(ThumbPaint, dirtyRect);

            var margin = 3;
            var radius = 12;

            var y = dirtyRect.Y + margin + radius;
            
            if (HasShadow)
            {
                canvas.SetShadow(new SizeF(0, 1), 2, CanvasDefaults.DefaultShadowColor);
            }

            var thumbOffPosition = 15f;
            var thumbOnPosition = 35f;
            var thumbPosition = thumbOffPosition.Lerp(thumbOnPosition, AnimationPercent);

            canvas.FillCircle(thumbPosition, y, radius);

            canvas.RestoreState();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaKit.Controls
{
    public abstract class BaseProgressBarDrawable : IDrawable
    {
        public Paint BackgroundPaint { get; set; }
        public Paint ProgressPaint { get; set; }
        public double Progress { get; set; }
        public float CornerRadius { get; set; } = 6f;
        public ProgressBarStyle Style { get; set; }

        public abstract void DrawChart(ICanvas canvas, RectF dirtyRect);
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawChart(canvas, dirtyRect);
        }
    }
}

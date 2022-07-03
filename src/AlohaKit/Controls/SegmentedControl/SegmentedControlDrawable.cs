using AlohaKit.Extensions;
using System.Collections;

namespace AlohaKit.Controls
{
    public class SegmentedControlDrawable : IDrawable
    {
        public Paint BackgroundPaint { get; set; }
        public Paint ActiveBackgroundPaint { get; set; }
        public IEnumerable ItemsSource { get; set; }
        public int SelectedIndex { get; set; }
        public Color TextColor { get; set; }
        public Color ActiveTextColor { get; set; }
        public float FontSize { get; set; }
        public float ActiveFontSize { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            DrawBackground(canvas, dirtyRect);
            DrawActiveTab(canvas, dirtyRect);
            DrawTabs(canvas, dirtyRect);
        }

        void DrawBackground(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (BackgroundPaint != null)
            {
                canvas.SetFillPaint(BackgroundPaint, dirtyRect);

                float tabItemRadius = dirtyRect.Height / 2;
                canvas.FillRoundedRectangle(0, 0, dirtyRect.Width, dirtyRect.Height, tabItemRadius);
            }

            canvas.RestoreState();
        }

        void DrawActiveTab(ICanvas canvas, RectF dirtyRect)
        {
            canvas.SaveState();

            if (ActiveBackgroundPaint != null)
            {
                var tabItemWidth = dirtyRect.Width / ItemsSource.Count();

                float tabItemRadius = dirtyRect.Height / 2;

                canvas.SetFillPaint(ActiveBackgroundPaint, dirtyRect);

                canvas.FillRoundedRectangle(SelectedIndex * tabItemWidth, 0, tabItemWidth, dirtyRect.Height, tabItemRadius);
            }

            canvas.RestoreState();
        }

        void DrawTabs(ICanvas canvas, RectF dirtyRect)
        {
            var tabItemWidth = dirtyRect.Width / ItemsSource.Count();

            for (int i = 0; i < ItemsSource.Count(); i++)
            {
                string title = (string)ItemsSource.ElementAt(i);

                var x = tabItemWidth * i;

                canvas.FontSize = (i == SelectedIndex) ? ActiveFontSize : FontSize;
                canvas.FontColor = (i == SelectedIndex) ? ActiveTextColor : TextColor;

                canvas.DrawString(title, x, 0, tabItemWidth, dirtyRect.Height, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds, 0);
            }
        }
    }
}
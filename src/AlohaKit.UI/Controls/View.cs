using AlohaKit.UI.Extensions;
using Microsoft.Maui.Graphics.Converters;
using System.ComponentModel;

namespace AlohaKit.UI
{
    public interface IView : IElement
    {
        Brush Background { get; set; }
    }

    public class View : Element, IView
    {
        protected bool _drawBackground;

        public static readonly BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(View), null, propertyChanged: BackgroundPropertyChanged);

        public static void BackgroundPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            ((View)bindableObject).Invalidate();
            ((View)bindableObject)._drawBackground = true;
        }

        [TypeConverter(typeof(ColorTypeConverter))]
        public Brush Background
        {
            get => (Color)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public override void Draw(ICanvas canvas, RectF bounds)
        {
            if (IsVisible)
            {
                if (_drawBackground)
                {
                    DrawBackground(canvas, bounds);
                }

                canvas.Alpha = (float)Opacity;
                canvas.Transform(TranslationX, TranslationY, ScaleX, ScaleY);
                
                DrawShadow(canvas, bounds);

                base.Draw(canvas, bounds); 
            }
        }

        void DrawBackground(ICanvas canvas, RectF bounds)
        {
            if (Background is SolidColorBrush solidColorBrush)
                canvas.FillColor = solidColorBrush.Color;
            else
                canvas.SetFillPaint(Background, bounds);

            canvas.FillRectangle(bounds);
        }

        void DrawShadow(ICanvas canvas, RectF bounds)
        {
            if (Shadow != null)
            {
                var offset = new SizeF((float)Shadow.Offset.X, (float)Shadow.Offset.Y);
                var radius = Shadow.Radius;
                var color = Shadow.Color;

                canvas.SetShadow(offset, radius, color);
            }
        }
    }
}
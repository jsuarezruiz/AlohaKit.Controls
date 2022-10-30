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
                    if (Background is SolidColorBrush solidColorBrush)
                        canvas.FillColor = solidColorBrush.Color;
                    else
                        canvas.SetFillPaint(Background, bounds);

                    canvas.FillRectangle(bounds);
                }

                canvas.Alpha = (float)Opacity;
                canvas.Transform(TranslationX, TranslationY, ScaleX, ScaleY);

                base.Draw(canvas, bounds);
            }
        }
    }
}
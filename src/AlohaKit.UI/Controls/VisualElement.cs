using Microsoft.Maui.Devices.Sensors;

namespace AlohaKit.UI
{
    public interface IVisualElement
    {
        bool IsVisible { get; set; }
        double Opacity { get; set; }

        float X { get; set; }
        float Y { get; set; }
        float WidthRequest { get; set; }
        float HeightRequest { get; set; }

        Shadow Shadow { get; set; }

        float TranslationX { get; set; }
        float TranslationY { get; set; }
        float ScaleX { get; set; }
        float ScaleY { get; set; }

        // Draw
        void Draw(ICanvas canvas, RectF bounds);
        void Invalidate();
    }

    public abstract class VisualElement : BindableObject, IVisualElement
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(VisualElement), true,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty OpacityProperty = 
            BindableProperty.Create(nameof(Opacity), typeof(double), typeof(VisualElement), 1d,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty XProperty =
            BindableProperty.Create(nameof(X), typeof(float), typeof(VisualElement), 0.0f,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty YProperty =
            BindableProperty.Create(nameof(Y), typeof(float), typeof(VisualElement), 0.0f,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty WidthRequestProperty =
            BindableProperty.Create(nameof(WidthRequest), typeof(float), typeof(VisualElement), float.NaN,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty HeightRequestProperty =
            BindableProperty.Create(nameof(HeightRequest), typeof(float), typeof(VisualElement), float.NaN,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty ShadowProperty =
            BindableProperty.Create(nameof(Shadow), typeof(Shadow), typeof(VisualElement), null,
                propertyChanged: InvalidatePropertyChanged);

		public static readonly BindableProperty RotationProperty =
			BindableProperty.Create(nameof(Rotation), typeof(float), typeof(VisualElement), 0f,
				propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty TranslationXProperty =
            BindableProperty.Create(nameof(TranslationX), typeof(float), typeof(VisualElement), 0f,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty TranslationYProperty =
            BindableProperty.Create(nameof(TranslationY), typeof(float), typeof(VisualElement), 0f,
                propertyChanged: InvalidatePropertyChanged);

        public static readonly BindableProperty ScaleXProperty =
            BindableProperty.Create(nameof(ScaleX), typeof(float), typeof(VisualElement), 1f);

        public static readonly BindableProperty ScaleYProperty =
            BindableProperty.Create(nameof(ScaleY), typeof(float), typeof(VisualElement), 1f);

        public static void InvalidatePropertyChanged(BindableObject bo, object oldValue, object newValue)
        {
            ((VisualElement)bo).Invalidate();
        }

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

        public float X
        {
            get => (float)GetValue(XProperty);
            set => SetValue(XProperty, value);
        }

        public float Y
        {
            get => (float)GetValue(YProperty);
            set => SetValue(YProperty, value);
        }

        public float WidthRequest
        {
            get => (float)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        public float HeightRequest
        {
            get => (float)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        public Shadow Shadow
        {
            get => (Shadow)GetValue(ShadowProperty);
            set => SetValue(ShadowProperty, value);
        }

		public float Rotation
		{
			get => (float)GetValue(RotationProperty);
			set => SetValue(RotationProperty, value);
		}

        public float TranslationX
        {
            get => (float)GetValue(TranslationXProperty);
            set => SetValue(TranslationXProperty, value);
        }

        public float TranslationY
        {
            get => (float)GetValue(TranslationYProperty);
            set => SetValue(TranslationYProperty, value);
        }

        public float ScaleX
        {
            get => (float)GetValue(ScaleXProperty);
            set => SetValue(ScaleXProperty, value);
        }

        public float ScaleY
        {
            get => (float)GetValue(ScaleYProperty);
            set => SetValue(ScaleYProperty, value);
        }

        public virtual void Draw(ICanvas canvas, RectF bounds)
        {

        }

        public virtual void Invalidate()
        {
          
        }
    }
}
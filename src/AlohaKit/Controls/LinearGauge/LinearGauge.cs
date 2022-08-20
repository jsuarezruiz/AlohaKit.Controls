using Microsoft.Maui.Platform;

namespace AlohaKit.Controls
{    
    // TODO: Include the Orientation property 
    public class LinearGauge : GraphicsView
    {
        public LinearGauge()
        {
            HeightRequest = 200;
            WidthRequest = 60;

            Drawable = LinearGaugeDrawable = new LinearGaugeDrawable();
        }

        public LinearGaugeDrawable LinearGaugeDrawable { get; set; }
       
        public static readonly new BindableProperty BackgroundProperty =  
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(Button), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is LinearGauge linearGauge)
                    {
                        linearGauge.UpdateBackground();
                    }
                });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty RangeStartProperty =
           BindableProperty.Create(nameof(RangeStart), typeof(int), typeof(LinearGauge), 0,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is LinearGauge linearGauge)
                   {
                       linearGauge.UpdateBackground();
                   }
               });

        public int RangeStart
        {
            get => (int)GetValue(RangeStartProperty);
            set => SetValue(RangeStartProperty, value);
        }

        public static readonly BindableProperty RangeEndProperty = 
            BindableProperty.Create(nameof(RangeEnd), typeof(int), typeof(LinearGauge), 100,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is LinearGauge linearGauge)
                    {
                        linearGauge.UpdateRangeEnd();
                    }
                });

        public int RangeEnd
        {
            get => (int)GetValue(RangeEndProperty);
            set => SetValue(RangeEndProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(int), typeof(LinearGauge), 0,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is LinearGauge linearGauge)
                    {
                        linearGauge.UpdateValue();
                        linearGauge.ValueChanged?.Invoke(linearGauge, new ValueChangedEventArgs((double)oldValue, (double)newValue));
                    }
                });

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =   
            BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(Button), new CornerRadius(),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is LinearGauge linearGauge)
                    {
                        linearGauge.UpdateCornerRadius();
                    }
                });

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                UpdateBackground();
                UpdateRangeStart();
                UpdateRangeEnd();
                UpdateValue();
                UpdateCornerRadius();
            }
        }

        void UpdateBackground()
        {
            if (LinearGaugeDrawable == null)
                return;

            LinearGaugeDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateRangeStart()
        {
            if (LinearGaugeDrawable == null)
                return;

            LinearGaugeDrawable.RangeStart = RangeStart;

            Invalidate();
        }

        void UpdateRangeEnd()
        {
            if (LinearGaugeDrawable == null)
                return;

            LinearGaugeDrawable.RangeEnd = RangeEnd;

            Invalidate();
        }

        void UpdateValue()
        {
            if (LinearGaugeDrawable == null)
                return;

            LinearGaugeDrawable.Value = Value;

            Invalidate();
        }

        void UpdateCornerRadius()
        {
            if (LinearGaugeDrawable == null)
                return;

            LinearGaugeDrawable.CornerRadius = CornerRadius;

            Invalidate();
        }
    }
}
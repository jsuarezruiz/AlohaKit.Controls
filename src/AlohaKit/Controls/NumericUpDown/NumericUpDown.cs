using AlohaKit.Extensions;

namespace AlohaKit.Controls
{
    public class NumericUpDown : GraphicsView
    {
        public NumericUpDown()
        {
            HeightRequest = 48;
            WidthRequest = 120;

            Drawable = NumericUpDownDrawable = new NumericUpDownDrawable();

            StartInteraction += OnNumericUpDownStartInteraction;
        }

        public NumericUpDownDrawable NumericUpDownDrawable { get; set; }

        public static readonly new BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(Button), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateBackground();
                    }
                });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(NumericUpDown), Colors.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateColor();
                    }
                });

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty ColorMinusProperty =
            BindableProperty.Create(nameof(ColorMinus), typeof(Color), typeof(NumericUpDown), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateColorMinus();
                    }
                });

        public Color ColorMinus
        {
            get { return (Color)GetValue(ColorMinusProperty); }
            set { SetValue(ColorMinusProperty, value); }
        }

        public static readonly BindableProperty ColorMaximumProperty =
            BindableProperty.Create(nameof(ColorMinus), typeof(Color), typeof(NumericUpDown), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateColorMaximum();
                    }
                });

        public Color ColorMaximum
        {
            get { return (Color)GetValue(ColorMaximumProperty); }
            set { SetValue(ColorMaximumProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(NumericUpDown), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateTextColor();
                    }
                });

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(NumericUpDown), 18.0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateFontSize();
                    }
                });

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty MinimumProperty =
            BindableProperty.Create(nameof(Minimum), typeof(double), typeof(NumericUpDown), 0d,
                validateValue: (bindable, value) => (double)value < ((NumericUpDown)bindable).Maximum,
                coerceValue: (bindable, value) =>
                {
                    var numericUpDown = (NumericUpDown)bindable;
                    numericUpDown.Value = numericUpDown.Value.Clamp((double)value, numericUpDown.Maximum);
                    return value;
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateMinimum();
                    }
                });

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly BindableProperty MaximumProperty =
            BindableProperty.Create(nameof(Maximum), typeof(double), typeof(NumericUpDown), 100d,
                validateValue: (bindable, value) => (double)value > ((Stepper)bindable).Minimum,
                coerceValue: (bindable, value) =>
                {
                    var numericUpDown = (NumericUpDown)bindable;
                    numericUpDown.Value = numericUpDown.Value.Clamp(numericUpDown.Minimum, (double)value);
                    return value;
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateMaximum();
                    }
                });

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly BindableProperty IntervalProperty =
          BindableProperty.Create(nameof(Interval), typeof(double), typeof(NumericUpDown), 1d);

        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(NumericUpDown), 0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateValue();
                        numericUpDown.ValueChanged?.Invoke(numericUpDown, new ValueChangedEventArgs((double)oldValue, (double)newValue));
                    }
                });

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                UpdateBackground();
                UpdateColor();
                UpdateTextColor();
                UpdateFontSize();
                UpdateMinimum();
                UpdateMaximum();
                UpdateValue();
            }
        }

        void UpdateBackground()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateColor()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.Color = Color;

            Invalidate();
        }

        void UpdateColorMinus()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.ColorMinus = ColorMinus;

            Invalidate();
        }

        void UpdateColorMaximum()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.ColorMaximum = ColorMaximum;

            Invalidate();
        }

        void UpdateTextColor()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.TextColor = TextColor;

            Invalidate();
        }

        void UpdateFontSize()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.FontSize = FontSize;

            Invalidate();
        }

        void UpdateMinimum()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.Minimum = Minimum;

            Invalidate();
        }

        void UpdateMaximum()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.Maximum = Maximum;

            Invalidate();
        }

        void UpdateValue()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.Value = Value;

            Invalidate();
        }

        void OnNumericUpDownStartInteraction(object sender, TouchEventArgs e)
        {
            if (!IsEnabled)
                return;

            var point = e.Touches[0];

            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.TouchPoint = point;

            if (NumericUpDownDrawable.MinusRectangle.Contains(point))
                Value -= Interval;

            if (NumericUpDownDrawable.PlusRectangle.Contains(point))
                Value += Interval;
        }
    }
}
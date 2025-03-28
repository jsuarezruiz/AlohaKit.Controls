using AlohaKit.Extensions;

namespace AlohaKit.Controls
{
	/// <summary>
	/// The NumericUpDown is a drawn control for selecting numeric values by incrementing or decrementing them with 
	/// interactive buttons or input.
	/// </summary>
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

        public static readonly BindableProperty MinimumColorProperty =
            BindableProperty.Create(nameof(MinimumColor), typeof(Brush), typeof(NumericUpDown), new SolidColorBrush(Colors.Green),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateMinimumColor();
                    }
                });

        public Brush MinimumColor
        {
            get { return (Brush)GetValue(MinimumColorProperty); }
            set { SetValue(MinimumColorProperty, value); }
        }

        public static readonly BindableProperty MaximumColorProperty =
            BindableProperty.Create(nameof(MaximumColor), typeof(Brush), typeof(NumericUpDown), new SolidColorBrush(Colors.Red),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateMaximumColor();
                    }
                });

        public Brush MaximumColor
        {
            get { return (Brush)GetValue(MaximumColorProperty); }
            set { SetValue(MaximumColorProperty, value); }
        }

        public static readonly BindableProperty MinimumTextColorProperty =
            BindableProperty.Create(nameof(MinimumTextColor), typeof(Color), typeof(NumericUpDown), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateMinimumTextColor();
                    }
                });

        public Color MinimumTextColor
        {
            get { return (Color)GetValue(MinimumTextColorProperty); }
            set { SetValue(MinimumTextColorProperty, value); }
        }

        public static readonly BindableProperty MaximumTextColorProperty =
            BindableProperty.Create(nameof(MaximumTextColor), typeof(Color), typeof(NumericUpDown), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is NumericUpDown numericUpDown)
                    {
                        numericUpDown.UpdateMaximumTextColor();
                    }
                });

        public Color MaximumTextColor
        {
            get { return (Color)GetValue(MaximumTextColorProperty); }
            set { SetValue(MaximumTextColorProperty, value); }
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
                validateValue: (bindable, value) => (double)value > ((NumericUpDown)bindable).Minimum,
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
				UpdateMinimumColor();
				UpdateMaximumColor();
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

        void UpdateMinimumColor()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.MinimumColorPaint = MinimumColor;

            Invalidate();
        }

        void UpdateMaximumColor()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.MaximumColorPaint = MaximumColor;

            Invalidate();
        }

        void UpdateMinimumTextColor()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.MinimumTextColor = MinimumTextColor;

            Invalidate();
        }

        void UpdateMaximumTextColor()
        {
            if (NumericUpDownDrawable == null)
                return;

            NumericUpDownDrawable.MaximumTextColor = MaximumTextColor;

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

            if (NumericUpDownDrawable.MinusRectangle.Contains(point) && (Value - Interval) >= Minimum)
                Value -= Interval;

            if (NumericUpDownDrawable.PlusRectangle.Contains(point) && (Value + Interval) <= Maximum)
                Value += Interval;
        }
    }
}
namespace AlohaKit.Controls
{
    public class Slider : GraphicsView
    {
        public Slider()
        {
            HeightRequest = 20;
            WidthRequest = 120;

            Drawable = SliderDrawable = new SliderDrawable();

            StartInteraction += OnSliderStartInteraction;
            DragInteraction += OnSliderDragInteraction;
        }

        public SliderDrawable SliderDrawable { get; set; }

        public static readonly new BindableProperty BackgroundProperty =
          BindableProperty.Create(nameof(Background), typeof(Brush), typeof(Button), null,
              propertyChanged: (bindableObject, oldValue, newValue) =>
              {
                  if (newValue != null && bindableObject is Slider slider)
                  {
                      slider.UpdateBackground();
                  }
              });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty MinimumProperty =    
            BindableProperty.Create(nameof(Minimum), typeof(double), typeof(Slider), 0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateMinimum();
                    }
                });

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public static readonly BindableProperty MaximumProperty =
            BindableProperty.Create(nameof(Maximum), typeof(double), typeof(Slider), 10d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateMaximum();
                    }
                });

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public static readonly BindableProperty ValueProperty = 
            BindableProperty.Create(nameof(Value), typeof(double), typeof(Slider), 0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateValue();
                        slider.ValueChanged?.Invoke(slider, new ValueChangedEventArgs((double)oldValue, (double)newValue));
                    }
                });

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty MinimumBrushProperty =
            BindableProperty.Create(nameof(MinimumBrush), typeof(Brush), typeof(Slider), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateMinimumBrush();
                    }
                });

        public Brush MinimumBrush
        {
            get => (Brush)GetValue(MinimumBrushProperty);
            set => SetValue(MinimumBrushProperty, value);
        }

        public static readonly BindableProperty MaximumBrushProperty =
            BindableProperty.Create(nameof(MaximumBrush), typeof(Brush), typeof(Slider), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateMaximumBrush();
                    }
                });

        public Brush MaximumBrush
        {
            get => (Brush)GetValue(MaximumBrushProperty);
            set => SetValue(MaximumBrushProperty, value);
        }

        public static readonly BindableProperty ThumbBrushProperty =
            BindableProperty.Create(nameof(ThumbBrush), typeof(Brush), typeof(Slider), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateThumbBrush();
                    }
                });

        public Brush ThumbBrush
        {
            get => (Brush)GetValue(ThumbBrushProperty);
            set => SetValue(ThumbBrushProperty, value);
        }

        public static readonly BindableProperty ThumbShapeProperty =
            BindableProperty.Create(nameof(ThumbShape), typeof(ThumbShape), typeof(Slider), ThumbShape.Circle,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Slider slider)
                    {
                        slider.UpdateThumbShape();
                    }
                });        

        public ThumbShape ThumbShape
        {
            get => (ThumbShape)GetValue(ThumbShapeProperty);
            set => SetValue(ThumbShapeProperty, value);
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        protected override void OnParentSet()
        {
            base.OnParentSet();
        
            if(Parent != null)
            {
                UpdateBackground();
                UpdateMinimum();
                UpdateMaximum();
                UpdateValue();
                UpdateMinimumBrush();
                UpdateMaximumBrush(); 
                UpdateThumbBrush();
                UpdateThumbShape();
            }
        }

        void UpdateBackground()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateMinimum()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.Minimum = Minimum;

            Invalidate();
        }

        void UpdateMaximum()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.Maximum = Maximum;

            Invalidate();
        }

        void UpdateValue()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.Value = Value;

            Invalidate();
        }

        void UpdateMinimumBrush()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.MinimumPaint = MinimumBrush;

            Invalidate();
        }

        void UpdateMaximumBrush()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.MaximumPaint = MaximumBrush;

            Invalidate();
        }

        void UpdateThumbBrush()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.ThumbPaint = ThumbBrush;

            Invalidate();
        }

        private void UpdateThumbShape()
        {
            if (SliderDrawable == null)
                return;

            SliderDrawable.ThumbShape = ThumbShape;

            Invalidate();
        }

        void OnSliderStartInteraction(object sender, TouchEventArgs args)
        {
            var touchPoint = args.Touches[0];
            UpdateValueFromInteraction(touchPoint);
        }

        void OnSliderDragInteraction(object sender, TouchEventArgs args)
        {
            var touchPoint = args.Touches[0];
            UpdateValueFromInteraction(touchPoint);
        }

        void UpdateValueFromInteraction(PointF touchPoint)
        {
            Value = touchPoint.X * Maximum / Width;
        }
    }
}
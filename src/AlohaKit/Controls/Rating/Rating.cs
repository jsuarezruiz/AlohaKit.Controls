namespace AlohaKit.Controls
{
    public class Rating : GraphicsView
    {
        public Rating()
        {
            HeightRequest = 30;
            WidthRequest = 150;

            Drawable = RatingDrawable = new RatingDrawable();

            StartInteraction += OnRatingDrawableStartInteraction;
        }

        public RatingDrawable RatingDrawable { get; set; }

        public static readonly BindableProperty ItemsCountProperty =
          BindableProperty.Create(nameof(ItemsCount), typeof(int), typeof(Rating), 5,
              propertyChanged: (bindableObject, oldValue, newValue) =>
              {
                  if (newValue != null && bindableObject is Rating rating)
                  {
                      rating.UpdateItemsCount();
                  }
              });

        public int ItemsCount
        {
            get => (int)GetValue(ItemsCountProperty);
            set => SetValue(ItemsCountProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
          BindableProperty.Create(nameof(ItemsCount), typeof(int), typeof(Rating), 0,
              propertyChanged: (bindableObject, oldValue, newValue) =>
              {
                  if (newValue != null && bindableObject is Rating rating)
                  {
                      rating.UpdateValue();
                  }
              });

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty SelectedFillProperty =
            BindableProperty.Create(nameof(SelectedFill), typeof(Color), typeof(Rating), Color.FromArgb("#F6C602"),
                  propertyChanged: (bindableObject, oldValue, newValue) =>
                  {
                      if (newValue != null && bindableObject is Rating rating)
                      {
                          rating.UpdateSelectedFill();
                      }
                  });

        public Color SelectedFill
        {
            get => (Color)GetValue(SelectedFillProperty);
            set => SetValue(SelectedFillProperty, value);
        }

        public static readonly BindableProperty UnSelectedFillProperty =    
            BindableProperty.Create(nameof(UnSelectedFill), typeof(Color), typeof(Rating), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Rating rating)
                    {
                        rating.UpdateUnSelectedFill();
                    }
                });

        public Color UnSelectedFill
        {
            get => (Color)GetValue(UnSelectedFillProperty);
            set => SetValue(UnSelectedFillProperty, value);
        }

        public static readonly BindableProperty SelectedStrokeProperty =  
            BindableProperty.Create(nameof(SelectedStroke), typeof(Color), typeof(Rating), Color.FromArgb("#F6C602"),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Rating rating)
                    {
                        rating.UpdateSelectedStroke();
                    }
                });

        public Color SelectedStroke
        {
            get => (Color)GetValue(SelectedStrokeProperty);
            set => SetValue(SelectedStrokeProperty, value);
        }

        public static readonly BindableProperty UnSelectedStrokeProperty =
            BindableProperty.Create(nameof(UnSelectedStroke), typeof(Color), typeof(Rating), Colors.Black, 
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Rating rating)
                    {
                        rating.UpdateUnSelectedStroke();
                    }
                });

        public Color UnSelectedStroke
        {
            get => (Color)GetValue(UnSelectedStrokeProperty);
            set => SetValue(UnSelectedStrokeProperty, value);
        }

        public static readonly BindableProperty SelectedStrokeWidthProperty =
            BindableProperty.Create(nameof(SelectedStrokeWidth), typeof(double), typeof(Rating), 1.0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Rating rating)
                    {
                        rating.UpdateSelectedStrokeWidth();
                    }
                });

        public double SelectedStrokeWidth
        {
            get => (double)GetValue(SelectedStrokeWidthProperty);
            set => SetValue(SelectedStrokeWidthProperty, value);
        }

        public static readonly BindableProperty UnSelectedStrokeWidthProperty =
            BindableProperty.Create(nameof(UnSelectedStrokeWidth), typeof(double), typeof(Rating), 1.0d, 
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Rating rating)
                    {
                        rating.UpdateUnSelectedStrokeWidth();
                    }
                });

        public double UnSelectedStrokeWidth
        {
            get => (double)GetValue(UnSelectedStrokeWidthProperty);
            set => SetValue(UnSelectedStrokeWidthProperty, value);
        }

        public static readonly BindableProperty IsReadOnlyProperty =
            BindableProperty.Create(nameof(IsReadOnly), typeof(bool), typeof(Rating), false);

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public event EventHandler<RatingValueChangedEventArgs> ValueChanged;

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if(Parent != null)
            {
                UpdateItemsCount();
                UpdateValue();
                UpdateSelectedFill();
                UpdateUnSelectedFill();
                UpdateSelectedStroke();
                UpdateUnSelectedStroke();
                UpdateSelectedStrokeWidth();
                UpdateUnSelectedStrokeWidth();
            }
        }

        void UpdateItemsCount()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.ItemsCount = ItemsCount;

            Invalidate();
        }

        void UpdateValue()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.Value = Value;
            
            ValueChanged?.Invoke(this, new RatingValueChangedEventArgs(Value));

            Invalidate();
        }

        void UpdateSelectedFill()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.SelectedFillColor = SelectedFill;

            Invalidate();
        }

        void UpdateUnSelectedFill()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.UnSelectedFillColor = UnSelectedFill;

            Invalidate();
        }

        void UpdateSelectedStroke()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.SelectedStrokeColor = SelectedStroke;

            Invalidate();
        }

        void UpdateUnSelectedStroke()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.UnSelectedStrokeColor = UnSelectedStroke;

            Invalidate();
        }

        void UpdateSelectedStrokeWidth()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.SelectedStrokeWidth = SelectedStrokeWidth;

            Invalidate();
        }

        void UpdateUnSelectedStrokeWidth()
        {
            if (RatingDrawable == null)
                return;

            RatingDrawable.UnSelectedStrokeWidth = UnSelectedStrokeWidth;

            Invalidate();
        }

        void OnRatingDrawableStartInteraction(object sender, TouchEventArgs args)
        {
            if (IsReadOnly)
                return;

            var touchPoint = args.Touches[0];
            var touchX = touchPoint.X;

            Value = (int)(touchX * ItemsCount / Width);
        }
    }
}
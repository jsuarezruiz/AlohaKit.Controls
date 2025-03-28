using AlohaKit.Extensions;
using System.Collections;
using System.Windows.Input;

namespace AlohaKit.Controls
{
	/// <summary>
	/// The SegmentedControl is a drawn control that allows users to make a single selection from a set of segmented options. 
	/// It is ideal for scenarios where a user needs to choose between mutually exclusive options, such as switching between tabs or filtering content.
	/// </summary>
	public class SegmentedControl : GraphicsView
	{
		public SegmentedControl()
		{
			HeightRequest = 48;

			Drawable = SegmentedControlDrawable = new SegmentedControlDrawable();

            StartInteraction += OnSegmentedControlStartInteraction;
		}

        public SegmentedControlDrawable SegmentedControlDrawable { get; set; }

        public static readonly new BindableProperty BackgroundProperty =
          BindableProperty.Create(nameof(Background), typeof(Brush), typeof(SegmentedControl), new SolidColorBrush(Colors.Gray),
             propertyChanged: (bindableObject, oldValue, newValue) =>
             {
                 if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                 {
                     segmentedControl.UpdateBackground();
                 }
             });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty ActiveBackgroundProperty =  
            BindableProperty.Create(nameof(ActiveBackground), typeof(Brush), typeof(SegmentedControl), new SolidColorBrush(Colors.Pink),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateActiveBackground();
                    }
                });

        public Brush ActiveBackground
        {
            get => (Brush)GetValue(ActiveBackgroundProperty);
            set => SetValue(ActiveBackgroundProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty =    
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SegmentedControl), null,      
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateItemsSource();
                    }
                });

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(SegmentedControl), 0,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateSelectedIndex();
                    }
                });

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SegmentedControl), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateTextColor();
                    }
                });

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty ActiveTextColorProperty =   
            BindableProperty.Create(nameof(ActiveTextColor), typeof(Color), typeof(SegmentedControl), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateActiveTextColor();
                    }
                });

        public Color ActiveTextColor
        {
            get { return (Color)GetValue(ActiveTextColorProperty); }
            set { SetValue(ActiveTextColorProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(SegmentedControl), 18.0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateFontSize();
                    }
                });

        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty ActiveFontSizeProperty =
            BindableProperty.Create(nameof(ActiveFontSize), typeof(double), typeof(SegmentedControl), 20.0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is SegmentedControl segmentedControl)
                    {
                        segmentedControl.UpdateActiveFontSize();
                    }
                });

        public double ActiveFontSize
        {
            get { return (double)GetValue(ActiveFontSizeProperty); }
            set { SetValue(ActiveFontSizeProperty, value); }
		}

		public static readonly BindableProperty CornerRadiusProperty =
			BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(SegmentedControl), 24.0d,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is SegmentedControl segmentedControl)
					{
						segmentedControl.UpdateCornerRadius();
					}
				});

		public double CornerRadius
		{
			get { return (double)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		public static readonly BindableProperty SelectedIndexChangedCommandProperty =	
			BindableProperty.Create(nameof(SelectedIndexChangedCommand), typeof(ICommand), typeof(SegmentedControl), null);

		public ICommand SelectedIndexChangedCommand
		{
			get { return (ICommand)GetValue(SelectedIndexChangedCommandProperty); }
			set { SetValue(SelectedIndexChangedCommandProperty, value); }
		}

		public event EventHandler<SelectedIndexEventArgs> SelectedIndexChanged;

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                UpdateBackground();
                UpdateActiveBackground();
                UpdateItemsSource();
                UpdateSelectedIndex();
                UpdateTextColor();
                UpdateActiveTextColor();
                UpdateFontSize();
                UpdateActiveFontSize();
				UpdateCornerRadius();
			}
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            WidthRequest = width;
            HeightRequest = height;
        }

        void UpdateBackground()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateActiveBackground()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.ActiveBackgroundPaint = ActiveBackground;

            Invalidate();
        }

        void UpdateItemsSource()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.ItemsSource = ItemsSource;

            Invalidate();
        }

        void UpdateSelectedIndex()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.SelectedIndex = SelectedIndex;

            Invalidate();
        }

        void UpdateTextColor()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.TextColor = TextColor;

            Invalidate();
        }

        void UpdateActiveTextColor()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.ActiveTextColor = ActiveTextColor;

            Invalidate();
        }

        void UpdateFontSize()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.FontSize = (float)FontSize;

            Invalidate();
        }

        void UpdateActiveFontSize()
        {
            if (SegmentedControlDrawable == null)
                return;

            SegmentedControlDrawable.ActiveFontSize = (float)ActiveFontSize;

            Invalidate();
        }

		void UpdateCornerRadius()
		{
			if (SegmentedControlDrawable == null)
				return;

			SegmentedControlDrawable.CornerRadius = CornerRadius;

			Invalidate();
		}

		void OnSegmentedControlStartInteraction(object sender, TouchEventArgs e)
        {
            float positionX = e.Touches[0].X;
            var tabItemWidth = Width / ItemsSource.Count();

            for (int i = 0; i < ItemsSource.Count(); i++)
            {
                float tabPositionX = (float)(i * tabItemWidth);

                if (positionX >= tabPositionX
                    && positionX <= (tabPositionX + tabItemWidth))
                {
                    SelectedIndex = i;
					OnSelectedIndexChanged(SelectedIndex);
				}
            }
        }

		void OnSelectedIndexChanged(int selectedIndex)
		{
			SelectedIndexChanged?.Invoke(this, new SelectedIndexEventArgs(selectedIndex));
			SelectedIndexChangedCommand?.Execute(selectedIndex);
		}
	}
}
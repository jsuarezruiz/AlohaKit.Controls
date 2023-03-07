using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaKit.Controls
{
    public class ProgressBar : GraphicsView
    {
        protected ProgressBarDrawable ProgressBarDrawable { get; set; }
		private bool IsInitialized = false;

		public ProgressBar()
		{
			Opacity = 0;
			Drawable = ProgressBarDrawable = new ProgressBarDrawable();
			Loaded += ProgressBar_Loaded;

		}
		private void ProgressBar_Loaded(object sender, EventArgs e)
		{
			ProgressBarDrawable.IsAnimating = true;
			IsInitialized = true;
			this.FadeTo(1, 1000, Easing.SinIn);
			AnimateProgress(Value);
		}

		public static readonly BindableProperty IsVerticalProperty = BindableProperty.Create(nameof(IsVertical), typeof(bool), typeof(ProgressBar), false,
		propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			if (newValue != null && bindableObject is ProgressBar progressBar)
			{
				progressBar.UpdateIsVertical();
				
				progressBar.Invalidate();
			}
		});

		public bool IsVertical
		{
			get => (bool)GetValue(IsVerticalProperty);
			set => SetValue(IsVerticalProperty, value);
		}

		public static readonly BindableProperty EasingProperty = BindableProperty.Create(nameof(Easing), typeof(Easing), typeof(ProgressBar), Easing.BounceOut);
        
		public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        public static readonly BindableProperty EasingIntervalProperty = BindableProperty.Create(nameof(EasingInterval), typeof(int), typeof(ProgressBar), 1000);
        public int EasingInterval
        {
            get => (int)GetValue(EasingIntervalProperty);
            set => SetValue(EasingIntervalProperty, value);
        }

        public static readonly BindableProperty EnableAnimationsProperty = BindableProperty.Create(nameof(EnableAnimations), typeof(bool), typeof(ProgressBar), true);
        public bool EnableAnimations
        {
            get => (bool)GetValue(EnableAnimationsProperty);
            set => SetValue(EnableAnimationsProperty, value);
        }


        public static readonly BindableProperty RoundCornersProperty =
        BindableProperty.Create(nameof(RoundCorners), typeof(bool), typeof(ProgressBar), false,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is ProgressBar progressBar)
            {
                progressBar.UpdateRoundCorners();
            }
        });

        public bool RoundCorners
        {
            get { return (bool)GetValue(RoundCornersProperty); }
            set { SetValue(RoundCornersProperty, value); }
        }

        public static readonly BindableProperty CornerRadiusProperty =
    BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(ProgressBar), new CornerRadius(6f),
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is ProgressBar progressBar)
            {
                progressBar.UpdateCornerRadius();
            }
        });

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

		public static readonly BindableProperty ProgressBrushProperty =
			BindableProperty.Create(nameof(ProgressBrush), typeof(Brush), typeof(ProgressBar), new SolidColorBrush(Colors.Blue),
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is ProgressBar progressBar)
					{
						progressBar.UpdateProgressBrush();
					}
				});

		public Brush ProgressBrush
		{
			get { return (Brush)GetValue(ProgressBrushProperty); }
			set { SetValue(ProgressBrushProperty, value); }
		}

		public static readonly BindableProperty StrokeBrushProperty =
			BindableProperty.Create(nameof(StrokeBrush), typeof(Brush), typeof(ProgressBar), new SolidColorBrush(Colors.Gray),
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is ProgressBar progressBar)
					{
						progressBar.UpdateStrokeBrush();
					}
				});

		public Brush StrokeBrush
		{
			get { return (Brush)GetValue(StrokeBrushProperty); }
			set { SetValue(StrokeBrushProperty, value); }
		}

		public static readonly BindableProperty ValueProperty =
			BindableProperty.Create(nameof(Value), typeof(double), typeof(ProgressBar), 0.0, BindingMode.TwoWay,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is ProgressBar progressBar)
					{
						progressBar.UpdateValue();
						progressBar.ValueChanged?.Invoke(progressBar, new ValueChangedEventArgs((double)oldValue, (double)newValue));
					}
				});

		public double Value
		{
			get => (double)GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

		public event EventHandler<ValueChangedEventArgs> ValueChanged;

		void AnimateProgress(double progress)
        {
            var animation = new Animation(v =>
            {
                ProgressBarDrawable.Progress = v;
				Invalidate();

				ProgressBarDrawable.IsAnimating = false;
			}, 0, progress, easing: Easing);

            animation.Commit(this, "Progress", length: (uint)250);
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
			{
				UpdateIsVertical();
				UpdateStrokeBrush();
				UpdateProgressBrush();
				UpdateValue();
			}
        }

		void UpdateIsVertical()
		{
			if (ProgressBarDrawable == null)
				return;

			ProgressBarDrawable.IsVertical = IsVertical;
			Invalidate();
		}

		void UpdateStrokeBrush()
		{
			if (ProgressBarDrawable == null)
				return;

			ProgressBarDrawable.StrokePaint = StrokeBrush;
			Invalidate();
		}

		void UpdateProgressBrush()
		{
			if (ProgressBarDrawable == null)
				return;

			ProgressBarDrawable.ProgressPaint = ProgressBrush;
			Invalidate();
		}
		protected void UpdateCornerRadius()
        {
            if (ProgressBarDrawable == null)
                return;

            ProgressBarDrawable.CornerRadius = CornerRadius;
            Invalidate();
        }

        protected void UpdateRoundCorners()
        {
            if (ProgressBarDrawable == null)
                return;

            ProgressBarDrawable.Style = RoundCorners ? ProgressBarStyle.Rounded : ProgressBarStyle.Square;
            ProgressBarDrawable.CornerRadius = CornerRadius;
            Invalidate();
        }

        protected void UpdateValue()
        {
            if (ProgressBarDrawable == null)
                return;


			ProgressBarDrawable.Progress = Value;

			if (!ProgressBarDrawable.IsAnimating && IsInitialized)
				AnimateProgress(Value);
		}
    }
}

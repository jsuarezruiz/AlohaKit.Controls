using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaKit.Controls.ProgressBar
{
    public abstract class BaseProgressBar : GraphicsView
    {
        protected BaseProgressBarDrawable ProgressBarDrawable { get; set; }

        public static readonly BindableProperty EasingProperty = BindableProperty.Create(nameof(Easing), typeof(Easing), typeof(BaseProgressBar), Easing.BounceOut);
        public Easing Easing
        {
            get => (Easing)GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        public static readonly BindableProperty EasingIntervalProperty = BindableProperty.Create(nameof(EasingInterval), typeof(int), typeof(BaseProgressBar), 1000);
        public int EasingInterval
        {
            get => (int)GetValue(EasingIntervalProperty);
            set => SetValue(EasingIntervalProperty, value);
        }

        public static readonly BindableProperty EnableAnimationsProperty = BindableProperty.Create(nameof(EnableAnimations), typeof(bool), typeof(BaseProgressBar), true);
        public bool EnableAnimations
        {
            get => (bool)GetValue(EnableAnimationsProperty);
            set => SetValue(EnableAnimationsProperty, value);
        }


        public static readonly BindableProperty RoundCornersProperty =
        BindableProperty.Create(nameof(RoundCorners), typeof(bool), typeof(BaseProgressBar), false,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is BaseProgressBar progressBar)
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
    BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(BaseProgressBar), 6f,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is BaseProgressBar progressBar)
            {
                progressBar.UpdateCornerRadius();
            }
        });

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly BindableProperty ProgressBrushProperty =
            BindableProperty.Create(nameof(ProgressBrush), typeof(Brush), typeof(BaseProgressBar), new SolidColorBrush(Colors.Blue),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is BaseProgressBar progressBar)
                    {
                        progressBar.UpdateBrush();
                    }
                });

        public Brush ProgressBrush
        {
            get { return (Brush)GetValue(ProgressBrushProperty); }
            set { SetValue(ProgressBrushProperty, value); }
        }

        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(BaseProgressBar), 0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is BaseProgressBar progressBar)
                    {
                        progressBar.UpdateProgress();
                    }
                });

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        void AnimateProgress(double progress)
        {
            var animation = new Animation(v =>
            {
                ProgressBarDrawable.Progress = v;
                Invalidate();
            }, 0, progress, easing: Easing);

            animation.Commit(this, "Progress", length: (uint)EasingInterval);
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
                UpdateBrush();
                UpdateProgress();
            }
        }

        protected void UpdateBrush()
        {
            if (ProgressBarDrawable == null)
                return;

            ProgressBarDrawable.BackgroundPaint = Background;
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

            ProgressBarDrawable.Style = RoundCorners ? ProgressBarStyle.Rounded : ProgressBarStyle.Squared;
            ProgressBarDrawable.CornerRadius = CornerRadius;
            Invalidate();
        }

        protected void UpdateProgress()
        {
            if (ProgressBarDrawable == null)
                return;


            if (EnableAnimations)
                AnimateProgress(Progress);
            else
            {
                ProgressBarDrawable.Progress = Progress;
                Invalidate();
            }
        }
    }
}

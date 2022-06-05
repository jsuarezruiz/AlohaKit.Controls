using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlohaKit.Controls
{
    public class ProgressBar : GraphicsView
    {
        protected BaseProgressBarDrawable ProgressBarDrawable { get; set; }

        public ProgressBar()
        {
            if (IsVertical)
            {
                Drawable = ProgressBarDrawable = new VerticalProgressBarDrawable();
            }
        }

        public static readonly BindableProperty IsVerticalProperty = BindableProperty.Create(nameof(IsVertical), typeof(bool), typeof(ProgressBar), true,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is ProgressBar progressBar)
            {
                if ((bool)newValue)
                {
                    progressBar.Drawable = progressBar.ProgressBarDrawable = new VerticalProgressBarDrawable();
                }
                else
                {
                    progressBar.Drawable = progressBar.ProgressBarDrawable = new HorizontalProgressBarDrawable();
                }

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
    BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(ProgressBar), 6f,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            if (newValue != null && bindableObject is ProgressBar progressBar)
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
            BindableProperty.Create(nameof(ProgressBrush), typeof(Brush), typeof(ProgressBar), new SolidColorBrush(Colors.Blue),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressBar progressBar)
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
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(ProgressBar), 0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is ProgressBar progressBar)
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

            ProgressBarDrawable.Style = RoundCorners ? ProgressBarStyle.Rounded : ProgressBarStyle.Square;
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

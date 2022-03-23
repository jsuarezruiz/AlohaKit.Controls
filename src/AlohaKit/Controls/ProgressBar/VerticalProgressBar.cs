namespace AlohaKit.Controls
{
    public class VerticalProgressBar : GraphicsView
    {
        public VerticalProgressBar()
        {
            HeightRequest = 120;
            WidthRequest = 20;

            Drawable = ProgressBarDrawable = new VerticalProgressBarDrawable();
        }

        public VerticalProgressBarDrawable ProgressBarDrawable { get; set; }

        public static readonly BindableProperty ProgressBrushProperty =
            BindableProperty.Create(nameof(ProgressBrush), typeof(Brush), typeof(VerticalProgressBar), new SolidColorBrush(Colors.Blue),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is VerticalProgressBar progressBar)
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
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(VerticalProgressBar), 0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is VerticalProgressBar progressBar)
                    {
                        progressBar.UpdateProgress();
                    }
                });

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public Task<bool> ProgressTo(double value, uint length, Easing easing)
        {
            var tcs = new TaskCompletionSource<bool>();

            this.Animate("Progress", d => Progress = d, Progress, value, length: length, easing: easing, finished: (d, finished) => tcs.SetResult(finished));

            return tcs.Task;
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

        void UpdateBrush()
        {
            if (ProgressBarDrawable == null)
                return;

            ProgressBarDrawable.BackgroundPaint = Background;
            ProgressBarDrawable.ProgressPaint = ProgressBrush;

            Invalidate();
        }

        void UpdateProgress()
        {
            if (ProgressBarDrawable == null)
                return;

            ProgressBarDrawable.Progress = Progress;

            Invalidate();
        }
    }
}
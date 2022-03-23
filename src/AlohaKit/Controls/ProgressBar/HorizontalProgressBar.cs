namespace AlohaKit.Controls
{
    public class HorizontalProgressBar : GraphicsView
    {
        public HorizontalProgressBar()
        {
            HeightRequest = 20;
            WidthRequest = 120;

            Drawable = ProgressBarDrawable = new HorizontalProgressBarDrawable();
        }

        public HorizontalProgressBarDrawable ProgressBarDrawable { get; set; }

        public static readonly BindableProperty ProgressBrushProperty =
            BindableProperty.Create(nameof(ProgressBrush), typeof(Brush), typeof(HorizontalProgressBar), new SolidColorBrush(Colors.Blue),
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is HorizontalProgressBar progressBar)
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
            BindableProperty.Create(nameof(Progress), typeof(double), typeof(HorizontalProgressBar), 0d, 
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is HorizontalProgressBar progressBar)
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
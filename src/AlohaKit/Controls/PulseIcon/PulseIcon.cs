using Microsoft.Maui.Dispatching;
using System.Diagnostics;

namespace AlohaKit.Controls
{
    // TODO:
    // - Render Source Image
    // - Include PulseCount BindableProperty.
    public class PulseIcon : GraphicsView
    {
        readonly Stopwatch _stopwatch;
        readonly float[] _pulses;
        readonly double _cycleTime;

        public PulseIcon()
        {
            HeightRequest = 100;
            WidthRequest = 100;

            Drawable = PulseIconDrawable = new PulseIconDrawable();

            _stopwatch = new Stopwatch();
            _pulses = new float[3]; 
            _cycleTime = 5000;
        }

        public PulseIconDrawable PulseIconDrawable { get; set; }

        public static readonly BindableProperty SourceProperty =
           BindableProperty.Create(nameof(Source), typeof(string), typeof(PulseIcon), string.Empty);

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly BindableProperty IsPulsingProperty =
            BindableProperty.Create(nameof(IsPulsing), typeof(bool), typeof(PulseIcon), true,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is PulseIcon pulseIcon)
                    {
                        pulseIcon.UpdateIsPulsing();
                    }
                });

        public bool IsPulsing
        {
            get => (bool)GetValue(IsPulsingProperty);
            set => SetValue(IsPulsingProperty, value);
        }

        public static new readonly BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(PulseIcon), Brush.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is PulseIcon pulseIcon)
                    {
                        pulseIcon.UpdateBackground();
                    }
                });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty PulseColorProperty =
            BindableProperty.Create(nameof(PulseColor), typeof(Color), typeof(PulseIcon), Colors.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is PulseIcon pulseIcon)
                    {
                        pulseIcon.UpdatePulseColor();
                    }
                });

        public Color PulseColor
        {
            get => (Color)GetValue(PulseColorProperty);
            set => SetValue(PulseColorProperty, value);
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
                UpdateSource();
                UpdateBackground();
                UpdatePulseColor();
                UpdateIsPulsing();
            }
        }

        void UpdateSource()
        {
            if (PulseIconDrawable == null)
                return;

            PulseIconDrawable.Source = Source;

            Invalidate();
        }

        void UpdatePulseColor()
        {
            if (PulseIconDrawable == null)
                return;

            PulseIconDrawable.PulseColor = PulseColor;

            Invalidate();
        }

        void UpdateBackground()
        {
            if (PulseIconDrawable == null)
                return;

            PulseIconDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateIsPulsing()
        {
            _stopwatch.Start();

            Dispatcher.StartTimer(TimeSpan.FromMilliseconds(33), () =>
            {
                _pulses[0] = (float)(_stopwatch.Elapsed.TotalMilliseconds % _cycleTime / _cycleTime);
                if (_stopwatch.Elapsed.TotalMilliseconds > _cycleTime / 3)
                    _pulses[1] = (float)((_stopwatch.Elapsed.TotalMilliseconds - _cycleTime / 3) % _cycleTime / _cycleTime);
                if (_stopwatch.Elapsed.TotalMilliseconds > _cycleTime * 2 / 3)
                    _pulses[2] = (float)((_stopwatch.Elapsed.TotalMilliseconds - _cycleTime * 2 / 3) % _cycleTime / _cycleTime);

                if (PulseIconDrawable == null)
                    return false;

                PulseIconDrawable.Pulses = _pulses;

                Invalidate();

                if (!IsPulsing)
                {
                    _stopwatch.Stop();
                    _stopwatch.Reset();
                }

                return IsPulsing;
            });
        }
    }
}
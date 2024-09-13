using Microsoft.Maui.Animations;

namespace AlohaKit.Controls
{
    // TODO:
    // - Include ThumbImage BindableProperty.
    public class ToggleSwitch : GraphicsView
    {
        IAnimationManager _animationManager;

        public ToggleSwitch()
        {
            HeightRequest = 30;
            WidthRequest = 50;

            Drawable = ToggleSwitchDrawable = new ToggleSwitchDrawable();

            StartInteraction += OnToggleSwitchStartInteraction;
        }

        public ToggleSwitchDrawable ToggleSwitchDrawable { get; set; }

        public static readonly new BindableProperty BackgroundProperty =
           BindableProperty.Create(nameof(Background), typeof(Brush), typeof(ToggleSwitch), null,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is ToggleSwitch toggleSwitch)
                   {
                       toggleSwitch.UpdateBackground();
                   }
               });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty ThumbBrushProperty =
           BindableProperty.Create(nameof(ThumbBrush), typeof(Brush), typeof(ToggleSwitch), null,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is ToggleSwitch toggleSwitch)
                   {
                       toggleSwitch.UpdateThumbBrush();
                   }
               });

        public Brush ThumbBrush
        {
            get => (Brush)GetValue(ThumbBrushProperty);
            set => SetValue(ThumbBrushProperty, value);
        }

        public static readonly BindableProperty IsOnProperty =
           BindableProperty.Create(nameof(IsOn), typeof(bool), typeof(ToggleSwitch), false, defaultBindingMode: BindingMode.TwoWay,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is ToggleSwitch toggleSwitch)
                   {
                       toggleSwitch.UpdateIsOn();
                   }
               });

        public bool IsOn
        {
            get => (bool)GetValue(IsOnProperty);
            set => SetValue(IsOnProperty, value);
        }

        public static readonly BindableProperty HasShadowProperty =
           BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(ToggleSwitch), true,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is ToggleSwitch toggleSwitch)
                   {
                       toggleSwitch.UpdateHasShadow();
                   }
               });

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public event EventHandler<ToggledEventArgs> Toggled;

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
#if __ANDROID__
                _animationManager = new AnimationManager(new PlatformTicker(new Microsoft.Maui.Platform.EnergySaverListenerManager()));
#else
                _animationManager = new AnimationManager(new PlatformTicker());
#endif

                UpdateBackground();
                UpdateThumbBrush();
                UpdateIsOn();
                UpdateHasShadow();
            }
        }

        void UpdateBackground()
        {
            if (ToggleSwitchDrawable == null)
                return;

            ToggleSwitchDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateThumbBrush()
        {
            if (ToggleSwitchDrawable == null)
                return;

            ToggleSwitchDrawable.ThumbPaint = ThumbBrush;

            Invalidate();
        }

        void UpdateIsOn()
        {
            if (ToggleSwitchDrawable == null)
                return;

            ToggleSwitchDrawable.IsOn = IsOn;
            Toggled?.Invoke(this, new ToggledEventArgs(IsOn));

            Invalidate();

            AnimateToggle();
        }

        void UpdateHasShadow()
        {
            if (ToggleSwitchDrawable == null)
                return;

            ToggleSwitchDrawable.HasShadow = HasShadow;

            Invalidate();
        }

        void OnToggleSwitchStartInteraction(object sender, TouchEventArgs e)
        {
            if (IsEnabled)
            {
                IsOn = !IsOn;
            }
        }

        void AnimateToggle()
        {
            if (ToggleSwitchDrawable == null)
                return;

            float start = IsOn ? 0 : 1;
            float end = IsOn ? 1 : 0;

            _animationManager?.Add(new Microsoft.Maui.Animations.Animation(callback: (progress) =>
            {
                ToggleSwitchDrawable.AnimationPercent = start.Lerp(end, progress);
                Invalidate();
            }, duration: 0.1, easing: Easing.Linear));
        }
    }
}

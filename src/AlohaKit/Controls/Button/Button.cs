using Microsoft.Maui.Animations;
using System.Windows.Input;

namespace AlohaKit.Controls
{
    public class Button : GraphicsView
    {
        IAnimationManager _animationManager;

        public Button()
        {
            HeightRequest = 48;
            WidthRequest= 120;

            Drawable = ButtonDrawable = new ButtonDrawable();

            StartInteraction += OnButtonStartInteraction;
            EndInteraction += OnButtonEndInteraction;
        }

        public ButtonDrawable ButtonDrawable { get; set; }

        public static readonly new BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(Button), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateBackground();
                    }
                });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty StrokeProperty =
            BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(Button), null,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateStroke();
                    }
                });

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(Button), 2.0d,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateStrokeThickness();
                    }
                });

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(Button), new CornerRadius(12d),
            propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue != null && bindableObject is Button button)
                {
                    button.UpdateCornerRadius();
                }
            });

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty TextColorProperty =
           BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Button), null,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is Button button)
                   {
                       button.UpdateTextColor();
                   }
               });

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(Button), string.Empty,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateText();
                    }
                });

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(Button), TextAlignment.Center,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateHorizontalTextAlignment();
                    }
                });

        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }

        public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(Button), TextAlignment.Center,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateVerticalTextAlignment();
                    }
                });

        public TextAlignment VerticalTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
            set => SetValue(VerticalTextAlignmentProperty, value);
        }

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(Button), true,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateHasShadow();
                    }
                });


        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
        }

        public static readonly BindableProperty ShadowColorProperty =
            BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(Button), Colors.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Button button)
                    {
                        button.UpdateShadowColor();
                    }
                });


        public Color ShadowColor
        {
            get => (Color)GetValue(ShadowColorProperty);
            set => SetValue(ShadowColorProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(Button), null);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(Button), null);

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public event EventHandler Clicked;

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
#if __ANDROID__
                _animationManager = new AnimationManager(new PlatformTicker(new Microsoft.Maui.Platform.EnergySaverListenerManager()));
#else
                _animationManager = new AnimationManager(new PlatformTicker());
#endif

                UpdateBackground();
                UpdateStroke();
                UpdateStrokeThickness();
                UpdateCornerRadius();
                UpdateTextColor();
                UpdateText(); 
                UpdateHorizontalTextAlignment(); 
                UpdateVerticalTextAlignment();
                UpdateHasShadow();
                UpdateShadowColor();
            }
        }

        void UpdateBackground()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateStroke()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.StrokePaint = Stroke;

            Invalidate();
        }

        void UpdateStrokeThickness()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.StrokeThickness = StrokeThickness;

            Invalidate();
        }

        void UpdateCornerRadius()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.CornerRadius = CornerRadius;

            Invalidate();
        }

        void UpdateTextColor()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.TextColor = TextColor;

            Invalidate();
        }

        void UpdateText()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.Text = Text;

            Invalidate();
        }

        void UpdateHorizontalTextAlignment()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.HorizontalTextAlignment = HorizontalTextAlignment;

            Invalidate();
        }

        void UpdateVerticalTextAlignment()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.VerticalTextAlignment = VerticalTextAlignment;

            Invalidate();
        }

        void UpdateHasShadow()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.HasShadow = HasShadow;

            Invalidate();
        }

        void UpdateShadowColor()
        {
            if (ButtonDrawable == null)
                return;

            ButtonDrawable.ShadowColor = ShadowColor;

            Invalidate();
        }

        void OnButtonStartInteraction(object sender, TouchEventArgs e)
        {
            if (ButtonDrawable != null)
            {
                ButtonDrawable.TouchPoint = e.Touches[0];
                ButtonDrawable.Scale = 0.99f;
                Invalidate();
            }

            AnimateRippleEffect();
            ButtonClicked();
        }

        void OnButtonEndInteraction(object sender, TouchEventArgs e)
        {
            if (ButtonDrawable != null)
            {
                ButtonDrawable.Scale = 1.0f;
                Invalidate();
            }
        }
        
        void AnimateRippleEffect()
        {
            if (Drawable is not ButtonDrawable buttonDrawable)
                return;

            float start = 0;
            float end = 1;

            _animationManager?.Add(new Microsoft.Maui.Animations.Animation(callback: (progress) =>
            {
                buttonDrawable.AnimationPercent = start.Lerp(end, progress);
                Invalidate();
            }, 
            duration: 0.25, 
            easing: Easing.SinInOut,
            finished: () =>
            {
                buttonDrawable.AnimationPercent = 0;
            }));
        }

        void ButtonClicked()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
            Command?.Execute(CommandParameter);
        }
    }
}
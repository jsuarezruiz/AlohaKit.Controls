namespace AlohaKit.UI.Gallery.Controls
{
    public class TemplatedToggleSwitch : TemplatedView
    {
        const string ElementCanvasView = "Part_Canvas";
        const string ElementThumbBorder = "Part_ThumbBorder";
        const string ElementThumb = "Part_Thumb";

        CanvasView _canvasView;
        Path _thumbBorder;
        Path _thumb;

        public static readonly BindableProperty IsOnProperty =
            BindableProperty.Create(nameof(IsOn), typeof(bool), typeof(TemplatedToggleSwitch), true,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is TemplatedToggleSwitch toggleSwitch)
                    {
                        toggleSwitch.UpdateIsOn();
                    }
                });

        public bool IsOn
        {
            get => (bool)GetValue(IsOnProperty);
            set => SetValue(IsOnProperty, value);
        }

        public event EventHandler<ToggledEventArgs> Toggled;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _canvasView = GetTemplateChild(ElementCanvasView) as CanvasView;
            _thumbBorder = GetTemplateChild(ElementThumbBorder) as Path;
            _thumb = GetTemplateChild(ElementThumb) as Path;

            if(_canvasView != null)
                _canvasView.StartInteraction += OnToggleSwitchStartInteraction;

            UpdateIsOn();
        }

        void UpdateIsOn()
        {
            if (IsOn)
            {
                _thumbBorder.X = 43.2f;
                _thumb.X = 43.6f;
            }
            else
            {
                _thumbBorder.X = 3.2f;
               _thumb.X = 3.6f;
            }

            Toggled?.Invoke(this, new ToggledEventArgs(IsOn));

            _canvasView.Invalidate();
        }

        void OnToggleSwitchStartInteraction(object sender, TouchEventArgs e)
        {
            IsOn = !IsOn;
        }
    }
}
namespace AlohaKit.UI.Gallery.Controls;

// https://www.figma.com/community/file/1154687984126303513
public partial class ToggleSwitch : ContentView, IDisposable
{
    public ToggleSwitch()
    {
        InitializeComponent();

        CanvasView.StartInteraction += OnToggleSwitchStartInteraction;

        UpdateIsOn();
    }

    public void Dispose()
    {
        if (CanvasView != null)
            CanvasView.StartInteraction += OnToggleSwitchStartInteraction;
    }

    public static readonly BindableProperty IsOnProperty =
        BindableProperty.Create(nameof(IsOn), typeof(bool), typeof(ToggleSwitch), true,
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

    public event EventHandler<ToggledEventArgs> Toggled;

    void UpdateIsOn()
    {
        if (IsOn)
        {
            ThumbBorder.X = 43.2f;
            Thumb.X = 43.6f;
        }
        else
        {
            ThumbBorder.X = 3.2f;
            Thumb.X = 3.6f;
        }

        Toggled?.Invoke(this, new ToggledEventArgs(IsOn));

        CanvasView.Invalidate();
    }

    void OnToggleSwitchStartInteraction(object sender, TouchEventArgs e)
    {
        IsOn = !IsOn;
    }
}
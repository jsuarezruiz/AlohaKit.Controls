using System.Windows.Input;

namespace AlohaKit.Controls
{
    public class CheckBox : GraphicsView
    {
        public CheckBox()
        {
            HeightRequest = 24;
            WidthRequest= 24;

            Drawable = CheckBoxDrawable = new CheckBoxDrawable();

            StartInteraction += OnCheckBoxStartInteraction;
        }

        public CheckBoxDrawable CheckBoxDrawable { get; set; }

        public static readonly BindableProperty IsCheckedProperty =  
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is CheckBox checkBox)
                    {
                        checkBox.UpdateIsChecked();
                    }
                });

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public static readonly BindableProperty CheckedBrushProperty =
            BindableProperty.Create(nameof(Color), typeof(Brush), typeof(CheckBox), Brush.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is CheckBox checkBox)
                    {
                        checkBox.UpdateCheckedBrush();
                    }
                });

        public Brush CheckedBrush
        {
            get => (Brush)GetValue(CheckedBrushProperty);
            set => SetValue(CheckedBrushProperty, value);
        }

        public static readonly BindableProperty UncheckedBrushProperty =
            BindableProperty.Create(nameof(Color), typeof(Brush), typeof(CheckBox), Brush.Transparent,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is CheckBox checkBox)
                    {
                        checkBox.UpdateUncheckedBrush();
                    }
                });

        public Brush UncheckedBrush
        {
            get => (Brush)GetValue(UncheckedBrushProperty);
            set => SetValue(UncheckedBrushProperty, value);
        }

        public static readonly BindableProperty StrokeProperty = 
            BindableProperty.Create(nameof(Stroke), typeof(Brush), typeof(CheckBox), Brush.Black,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is CheckBox checkBox)
                    {
                        checkBox.UpdateStroke();
                    }
                });

        public Brush Stroke
        {
            get => (Brush)GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public static readonly BindableProperty StrokeThicknessProperty = 
            BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(CheckBox), 3.0d, 
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is CheckBox checkBox)
                    {
                        checkBox.UpdateStrokeThickness();
                    }
                });

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public static readonly BindableProperty CheckedCommandProperty =
            BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(CheckBox), null);

        public ICommand CheckedCommand
        {
            get => (ICommand)GetValue(CheckedCommandProperty);
            set => SetValue(CheckedCommandProperty, value);
        }

        public static readonly BindableProperty CheckedCommandParameterProperty = 
            BindableProperty.Create(nameof(CheckedCommandParameter), typeof(object), typeof(CheckBox), null);

        public object CheckedCommandParameter
        {
            get => GetValue(CheckedCommandParameterProperty);
            set => SetValue(CheckedCommandParameterProperty, value);
        }

        public static readonly BindableProperty UncheckedCommandProperty =
            BindableProperty.Create(nameof(UncheckedCommand), typeof(ICommand), typeof(CheckBox), null);

        public ICommand UncheckedCommand
        {
            get => (ICommand)GetValue(UncheckedCommandProperty);
            set => SetValue(UncheckedCommandProperty, value);
        }

        public static readonly BindableProperty UncheckedCommandParameterProperty =
            BindableProperty.Create(nameof(UncheckedCommandParameter), typeof(object), typeof(CheckBox), null);

        public object UncheckedCommandParameter
        {
            get => GetValue(UncheckedCommandParameterProperty);
            set => SetValue(UncheckedCommandParameterProperty, value);
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (Parent != null)
            {
                UpdateIsChecked();
                UpdateCheckedBrush();
                UpdateUncheckedBrush();
                UpdateStroke();
                UpdateStrokeThickness();
            }
        }

        void UpdateIsChecked()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.IsChecked = IsChecked;

            Invalidate();
        }

        void UpdateCheckedBrush()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.CheckedPaint = CheckedBrush;

            Invalidate();
        }

        void UpdateUncheckedBrush()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.UncheckedPaint = UncheckedBrush;

            Invalidate();
        }

        void UpdateStroke()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.StrokePaint = Stroke;

            Invalidate();
        }

        void UpdateStrokeThickness()
        {
            if (CheckBoxDrawable == null)
                return;

            CheckBoxDrawable.StrokeThickness = StrokeThickness;

            Invalidate();
        }

        void OnCheckBoxStartInteraction(object sender, TouchEventArgs e)
        {
            IsChecked = !IsChecked;

            UpdateIsChecked();

            CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(IsChecked));

            if(IsChecked)
                CheckedCommand?.Execute(CheckedCommandParameter);
            else
                UncheckedCommand?.Execute(UncheckedCommandParameter);
        }
    }
}
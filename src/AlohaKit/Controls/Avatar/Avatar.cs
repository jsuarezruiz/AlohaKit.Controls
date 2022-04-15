namespace AlohaKit.Controls
{
    public class Avatar : GraphicsView
    {
        public Avatar()
        {
            Drawable = PersonaDrawable = new AvatarDrawable();
        }

        public AvatarDrawable PersonaDrawable { get; set; }

        public static readonly new BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(Avatar), null,
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is Avatar avatar)
                   {
                       avatar.UpdateBackground();
                   }
               });

        public new Brush Background
        {
            get => (Brush)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty FillProperty =
            BindableProperty.Create(nameof(Fill), typeof(Brush), typeof(Avatar), new SolidColorBrush(Color.FromArgb("#4967F5")),
               propertyChanged: (bindableObject, oldValue, newValue) =>
               {
                   if (newValue != null && bindableObject is Avatar avatar)
                   {
                       avatar.UpdateFill();
                   }
               });

        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(Avatar), string.Empty,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Avatar avatar)
                    {
                        avatar.UpdateName();
                    }
                });

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Avatar), Colors.White,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Avatar avatar)
                    {
                        avatar.UpdateTextColor();
                    }
                });

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty AvatarSizeProperty =
            BindableProperty.Create(nameof(AvatarSize), typeof(AvatarSize), typeof(Avatar), AvatarSize.Small,
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    if (newValue != null && bindableObject is Avatar avatar)
                    {
                        avatar.UpdateAvatarSize();
                    }
                });

        public AvatarSize AvatarSize
        {
            get { return (AvatarSize)GetValue(AvatarSizeProperty); }
            set { SetValue(AvatarSizeProperty, value); }
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
            {
                UpdateAvatarSize();
                UpdateBackground();
                UpdateFill();
                UpdateName();
                UpdateTextColor();
            }
        }

        void UpdateAvatarSize()
        {
            HeightRequest = WidthRequest = AvatarSize.GetAvatarSize();
        }

        void UpdateBackground()
        {
            if (PersonaDrawable == null)
                return;

            PersonaDrawable.BackgroundPaint = Background;

            Invalidate();
        }

        void UpdateFill()
        {
            if (PersonaDrawable == null)
                return;

            PersonaDrawable.FillPaint = Fill;

            Invalidate();
        }

        void UpdateName()
        {
            if (PersonaDrawable == null)
                return;

            PersonaDrawable.Text = GetInitials(Name);
            PersonaDrawable.FontSize =  AvatarSize.GetInitialsFontSize();

            Invalidate();
        }

        void UpdateTextColor()
        {
            if (PersonaDrawable == null)
                return;

            PersonaDrawable.TextColor = TextColor;

            Invalidate();
        }

        string GetInitials(string text)
        {
            string result = string.Empty;

            bool v = true;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    v = true;
                else if (text[i] != ' ' && v)
                {
                    result += text[i];
                    v = false;
                }
            }

            return result;
        }
    }
}
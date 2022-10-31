namespace AlohaKit.UI
{
    public class Shadow : Element
    {
        public static readonly BindableProperty RadiusProperty = 
            BindableProperty.Create(nameof(Radius), typeof(float), typeof(Shadow), 10f);

        public static readonly BindableProperty ColorProperty = 
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(Shadow), Colors.Black);

        public static readonly BindableProperty OffsetProperty = 
            BindableProperty.Create(nameof(Offset), typeof(Point), typeof(Shadow), null);

        public float Radius
        {
            get { return (float)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public Point Offset
        {
            get { return (Point)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }
    }
}
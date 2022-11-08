using Microsoft.Maui;
using Microsoft.Maui.Graphics.Text;

namespace AlohaKit.UI
{
	public abstract class ButtonBase : View
	{
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(ButtonBase), string.Empty,
				propertyChanged: InvalidatePropertyChanged);

		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ButtonBase), null,
				propertyChanged: InvalidatePropertyChanged);

		public static readonly BindableProperty FontSizeProperty =
		   BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ButtonBase), 14.0d,
			   propertyChanged: InvalidatePropertyChanged);

		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		public Color TextColor
		{
			get => (Color)GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		public double FontSize
		{
			get => (double)GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		public event EventHandler Clicked;
		public event EventHandler Pressed;
		public event EventHandler Released;

		public override void StartInteraction(PointF[] points)
		{
			base.StartInteraction(points);

			Pressed?.Invoke(this, EventArgs.Empty);
			Clicked?.Invoke(this, EventArgs.Empty);
		}

		public override void EndInteraction(PointF[] points, bool isInsideBounds)
		{
			base.EndInteraction(points, isInsideBounds);

			Released?.Invoke(this, EventArgs.Empty);
		}
	}

	public class Button :
#if ANDROID
		Material.Button
#elif IOS || MACCATALYST
		Cupertino.Button
#elif WINDOWS
		Fluent.Button
#else
		Material.Button
#endif
	{

	}
}
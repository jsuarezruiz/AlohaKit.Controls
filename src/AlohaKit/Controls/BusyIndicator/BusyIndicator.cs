using Microsoft.Maui.Animations;

namespace AlohaKit.Controls
{
	/// <summary>
	/// The BusyIndicator is a drawn control that provides a graphical representation to indicate 
	/// an application or process is busy.
	/// </summary>
	public class BusyIndicator : GraphicsView
	{
		// TODO:
		// - Include IsRunning BindableProperty.
		// - Include AnimationDuration BindableProperty.
		IAnimationManager _animationManager;

		public BusyIndicator()
		{
			HeightRequest = 48;
			WidthRequest = 48;

			Drawable = BusyIndicatorDrawable = new BusyIndicatorDrawable();
		}

		public BusyIndicatorDrawable BusyIndicatorDrawable { get; set; }

		public static readonly new BindableProperty BackgroundColorProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BusyIndicator), Colors.White,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is BusyIndicator loading)
					{
						loading.UpdateBackgroundColor();
					}
				});


		public new Color BackgroundColor
		{
			get => (Color)GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create(nameof(Color), typeof(Color), typeof(BusyIndicator), Colors.Blue,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is BusyIndicator loading)
					{
						loading.UpdateColor();
					}
				});

		public Color Color
		{
			get => (Color)GetValue(ColorProperty);
			set => SetValue(ColorProperty, value);
		}

		public static readonly BindableProperty HasShadowProperty =
		 BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(BusyIndicator), true,
			 propertyChanged: (bindableObject, oldValue, newValue) =>
			 {
				 if (newValue != null && bindableObject is BusyIndicator loading)
				 {
					 loading.UpdateShadow();
				 }
			 });

		public bool HasShadow
		{
			get => (bool)GetValue(HasShadowProperty);
			set => SetValue(HasShadowProperty, value);
		}

		public static readonly BindableProperty ShadowColorProperty =
			BindableProperty.Create(nameof(ShadowColor), typeof(Color), typeof(BusyIndicator), Colors.Black,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is BusyIndicator loading)
					{
						loading.UpdateShadow();
					}
				});

		public Color ShadowColor
		{
			get => (Color)GetValue(ShadowColorProperty);
			set => SetValue(ShadowColorProperty, value);
		}

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

				var rotationAnimation = new Microsoft.Maui.Animations.Animation(progress =>
				{
					if (Drawable is BusyIndicatorDrawable busyIndicatorDrawable)
						busyIndicatorDrawable.Rotation = progress;

					Invalidate();
				},
				start: 0.0f,
				duration: 3.0f,
				easing: Easing.Linear);

				rotationAnimation.Repeats = true;

				_animationManager?.Add(rotationAnimation);

				var progressAnimation = new Microsoft.Maui.Animations.Animation(progress =>
				{
					if (Drawable is BusyIndicatorDrawable busyIndicatorDrawable)
						busyIndicatorDrawable.Progress = progress;

					Invalidate();
				},
				start: 0.0f,
				duration: 1.5f,
				easing: Easing.CubicInOut);

				progressAnimation.Repeats = true;

				_animationManager?.Add(progressAnimation);

				UpdateBackgroundColor();
				UpdateColor();
				UpdateShadow();
			}
		}

		void UpdateBackgroundColor()
		{
			if (BusyIndicatorDrawable == null)
				return;

			BusyIndicatorDrawable.BackgroundColor = BackgroundColor;
			Invalidate();
		}

		void UpdateColor()
		{
			if (BusyIndicatorDrawable == null)
				return;

			BusyIndicatorDrawable.Color = Color;
			Invalidate();
		}

		void UpdateShadow()
		{
			if (BusyIndicatorDrawable == null)
				return;

			BusyIndicatorDrawable.HasShadow = HasShadow;
			BusyIndicatorDrawable.ShadowColor = ShadowColor;

			Invalidate();
		}
	}
}
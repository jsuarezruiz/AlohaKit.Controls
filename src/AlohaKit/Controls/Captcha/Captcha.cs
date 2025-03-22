namespace AlohaKit.Controls
{
	/// <summary>
	/// The Captcha is a drawn control used to render and manage CAPTCHA (Completely Automated Public Turing test to 
	/// tell Computers and Humans Apart) challenges. 
	/// It leverages .NET MAUI Graphics for efficient graphical rendering and provides a flexible way to generate and validate 
	/// CAPTCHAs in an application.
	/// </summary>
	public class Captcha : GraphicsView
	{
		public Captcha()
		{
			HeightRequest = 50;
			WidthRequest = 150;

			Drawable = CaptchaDrawable = new CaptchaDrawable();
		}

		public CaptchaDrawable CaptchaDrawable { get; set; }

		public static readonly BindableProperty LevelProperty =
			BindableProperty.Create(nameof(Level), typeof(CaptchaLevel), typeof(Captcha), CaptchaLevel.Normal,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is Captcha captcha)
					{
						captcha.UpdateLevel();
					}
				});

		public CaptchaLevel Level
		{
			get => (CaptchaLevel)GetValue(LevelProperty);
			set => SetValue(LevelProperty, value);
		}

		public static readonly BindableProperty TextColorProperty =
		   BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(Captcha), Colors.Black,
			   propertyChanged: (bindableObject, oldValue, newValue) =>
			   {
				   if (newValue != null && bindableObject is Captcha captcha)
				   {
					   captcha.UpdateTextColor();
				   }
			   });

		public Color TextColor
		{
			get => (Color)GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		protected override void OnParentChanged()
		{
			base.OnParentChanged();

			if (Parent != null)
			{
				UpdateLevel();
				UpdateTextColor();
			}
		}

		void UpdateLevel()
		{
			if (CaptchaDrawable == null)
				return;

			if (CaptchaDrawable.Level != Level)
			{
				CaptchaDrawable.Level = Level;

				var word = GenerateRandomWord(GetWordLength(Level));
				CaptchaDrawable.Word = word;

				Invalidate();
			}
		}

		void UpdateTextColor()
		{
			if (CaptchaDrawable == null)
				return;

			CaptchaDrawable.TextColor = TextColor;

			Invalidate();
		}

		string GenerateRandomWord(int length)
		{
			var random = new Random();

			const string chars = "abcdefghijklmnopqrstuvwxyz" +
								 "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
								 "0123456789";

			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		int GetWordLength(CaptchaLevel level)
		{
			switch (level)
			{
				case CaptchaLevel.Weak:
					return 4;
				default:
				case CaptchaLevel.Normal:
					return 6;
				case CaptchaLevel.Strong:
					return 8;
			}
		}
	}
}
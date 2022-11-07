namespace AlohaKit.UI.Gallery.Views.XAML
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

		void OnEllipseTapped(object sender, EventArgs e)
		{
            DisplayAlert("Gestures", "Ellipse Tapped.", "Ok");
		}

		void OnButtonClicked(object sender, EventArgs e)
		{
			DisplayAlert("Button", "Button Clicked.", "Ok");
		}
	}
}
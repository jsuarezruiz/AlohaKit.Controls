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
            DisplayAlert("Gestures", "Ellipse tapped.", "Ok");
		}
	}
}
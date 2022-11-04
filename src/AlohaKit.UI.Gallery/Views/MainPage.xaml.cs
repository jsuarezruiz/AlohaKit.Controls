namespace AlohaKit.UI.Gallery.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        void OnCSharpButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CSharp.MainPage());
        }

        void OnXamlButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new XAML.MainPage());
        }

        void OnCustomControlsButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomControlsPage());
		}

		void OnBenchmarkButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new LolBenchmarkPage());
		}
	}
}
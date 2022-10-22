namespace AlohaKit.UI.Gallery.Views
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
		}

        private void OnCSharpButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CSharp.MainPage());
        }

        private void OnXamlButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new XAML.MainPage());
        }
    }
}
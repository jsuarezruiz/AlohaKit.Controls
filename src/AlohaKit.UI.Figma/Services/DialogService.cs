namespace AlohaKit.UI.Figma.Services
{
    public class DialogService
    {
        static DialogService _instance;

        public static DialogService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DialogService();

                return _instance;
            }
        }

        public void DisplayAlert(string title, string message)
        {
            var mainPage = Application.Current.MainPage;

            if (mainPage == null)
                return;

            mainPage.DisplayAlert(title, message, "Ok");
        }
    }
}
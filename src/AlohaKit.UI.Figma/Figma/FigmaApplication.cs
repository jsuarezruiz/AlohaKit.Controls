namespace AlohaKit.UI.Figma
{
    public class FigmaApplication
    {
        public static void Init(string token)
        {
            var applicationDelegate = new FigmaDelegate();

            FigmaSharp.AppContext.Current.Configuration(applicationDelegate, token);
        }
    }
}
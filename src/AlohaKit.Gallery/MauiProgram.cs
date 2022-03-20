namespace AlohaKit.Gallery
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Aloha.ttf", "Aloha");
                });

            return builder.Build();
        }
    }
}
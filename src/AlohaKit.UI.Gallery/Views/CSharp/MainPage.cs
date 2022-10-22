using AlohaKit.UI.Extensions;
using static AlohaKit.UI.UIStatics;

namespace AlohaKit.UI.Gallery.Views.CSharp
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "AlohaKit UI from C#";

            Content =
                CanvasView()
                    .Children(new Element[] {
                        Rectangle()
                            .X(10).Y(10)
                            .Height(80).Width(80)
                            .Fill(Colors.Red),
                        Ellipse() 
                            .X(10).Y(100)
                            .Height(80).Width(80)
                            .Fill(Colors.Orange),
                        Label()
                            .X(10).Y(200)
                            .Height(20).Width(100)
                            .Text("Label"),
                    });
        }
    }
}

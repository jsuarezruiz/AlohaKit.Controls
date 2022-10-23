using AlohaKit.UI.Figma.Converters;
using FigmaSharp;
using FigmaSharp.Converters;
using FigmaSharp.PropertyConfigure;
using FigmaSharp.Views;
using System.Reflection;

namespace AlohaKit.UI.Figma
{
    public class FigmaDelegate : IFigmaDelegate
    {
        public bool IsVerticalAxisFlipped => false;

        public void BeginInvoke(Action handler)
        {
            throw new NotImplementedException();
        }

        public FigmaSharp.Views.IView CreateEmptyView()
        {
            throw new NotImplementedException();
        }

        public CodePropertyConfigureBase GetCodePropertyConfigure()
        {
            throw new NotImplementedException();
        }

        public NodeConverter[] GetFigmaConverters()
        {
            return new NodeConverter[]{
                new ElipseConverter(),
                new FrameConverter(),  
                new ImageConverter(),
                new LineConverter(),
                new PolygonConverter(),
                new RectangleConverter(),
                new TextConverter()
            };
        }

        public FigmaSharp.Views.IImage GetImage(string url)
        {
            throw new NotImplementedException();
        }

        public FigmaSharp.Views.IImage GetImageFromFilePath(string filePath)
        {
            throw new NotImplementedException();
        }

        public FigmaSharp.Views.IImage GetImageFromManifest(Assembly assembly, string imageRef)
        {
            throw new NotImplementedException();
        }

        public IImageView GetImageView(FigmaSharp.Views.IImage image)
        {
            throw new NotImplementedException();
        }

        public string GetManifestResource(Assembly assembly, string file)
        {
            throw new NotImplementedException();
        }

        public string GetSvgData(string url)
        {
            throw new NotImplementedException();
        }

        public ViewPropertyConfigureBase GetViewPropertyConfigure()
        {
            throw new NotImplementedException();
        }
    }
}

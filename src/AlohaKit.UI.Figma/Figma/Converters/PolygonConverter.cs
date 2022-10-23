using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;

namespace AlohaKit.UI.Figma.Converters
{
    internal class PolygonConverter : RegularPolygonConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            return "<!-- Polygon -->";
        }

        public override FigmaSharp.Views.IView ConvertToView(FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService)
        {
            throw new NotImplementedException();
        }

        public override Type GetControlType(FigmaNode currentNode)
            => typeof(View);
    }
}

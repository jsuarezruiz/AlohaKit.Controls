using FigmaSharp.Converters;
using FigmaSharp.PropertyConfigure;
using FigmaSharp.Services;

namespace AlohaKit.UI.Figma.PropertyConfigure
{
    public class CodePropertyConfigure : CodePropertyConfigureBase
    {
        public override string ConvertToCode(string propertyName, CodeNode currentNode, CodeNode parentNode, NodeConverter converter, CodeRenderService rendererService)
        {
            return string.Empty;
        }
    }
}
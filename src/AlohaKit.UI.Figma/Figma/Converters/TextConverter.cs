using FigmaSharp.Converters;
using AlohaKit.UI.Figma.Extensions;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace AlohaKit.UI.Figma.Converters
{
    internal class TextConverter : TextConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaText textNode)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<alohakit:Label");

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            var bounds = textNode.absoluteBoundingBox;

            builder.AppendLine($"\tX=\"{bounds.X.ToString(nfi)}\"");
            builder.AppendLine($"\tY=\"{bounds.Y.ToString(nfi)}\"");

            builder.AppendLine($"\tWidthRequest=\"{bounds.Width.ToString(nfi)}\"");
            builder.AppendLine($"\tHeightRequest=\"{bounds.Height.ToString(nfi)}\"");

            if (textNode.opacity != 1)
                builder.AppendLine($"\tOpacity=\"{textNode.opacity.ToString(nfi)}\"");

            if (!textNode.visible)
                builder.AppendLine($"\tIsVisible=\"{textNode.visible}\"");


            if (textNode.HasFills)
            {
                var textPaint = textNode.fills.FirstOrDefault();

                if (textPaint.color != null)
                {
                    builder.AppendLine($"\tTextColor=\"{textPaint.color.ToCodeString()}\"");
                }
            }

            var textStyle = textNode.style;

            if (textStyle != null)
            {
                var fontSize = textStyle.fontSize;
                builder.AppendLine($"\tFontSize=\"{fontSize}\"");
            }

            string text = textNode.characters ?? textNode.name;
            builder.AppendLine($"\tText=\"{text}\"");

            builder.Append("\t/>");

            return builder.ToString();
        }

        public override FigmaSharp.Views.IView ConvertToView(FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService)
        {
            throw new NotImplementedException();
        }

        public override Type GetControlType(FigmaNode currentNode) 
            => typeof(View);
    }
}

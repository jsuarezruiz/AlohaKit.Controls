using FigmaSharp.Converters;
using AlohaKit.UI.Figma.Extensions;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace AlohaKit.UI.Figma.Converters
{
    internal class ElipseConverter : ElipseConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaElipse elipseNode)
            {
                return string.Empty;
            }
           
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<alohakit:Ellipse");

            var bounds = elipseNode.absoluteBoundingBox;

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            builder.AppendLine($"\tX=\"{bounds.X.ToString(nfi)}\"");
            builder.AppendLine($"\tY=\"{bounds.Y.ToString(nfi)}\"");

            builder.AppendLine($"\tWidthRequest=\"{bounds.Width.ToString(nfi)}\"");
            builder.AppendLine($"\tHeightRequest=\"{bounds.Height.ToString(nfi)}\"");

            if (elipseNode.opacity != 1)
                builder.AppendLine($"\tOpacity=\"{elipseNode.opacity.ToString(nfi)}\"");

            if (!elipseNode.visible)
                builder.AppendLine($"\tIsVisible=\"{elipseNode.visible}\"");

            if (elipseNode.HasStrokes)
            {
                var strokePaint = elipseNode.strokes.FirstOrDefault();

                if (strokePaint.color != null)
                {
                    builder.AppendLine($"\tStroke=\"{strokePaint.color.ToCodeString()}\"");
                }

                if (elipseNode.strokeWeight != 0)
                {
                    var strokeSize = elipseNode.strokeWeight;
                    builder.AppendLine($"\tStrokeThickness=\"{strokeSize}\"");
                }
            }

            builder.Append("\t>");

            if (elipseNode.HasFills)
            {
                var backgroundPaint = elipseNode.fills.FirstOrDefault();

                if (backgroundPaint != null && backgroundPaint.visible)
                {
                    builder.AppendLine("\n\t<alohakit:Ellipse.Fill>");

                    if (backgroundPaint.color != null)
                    {
                        builder.AppendLine($"\t\t<SolidColorBrush Color=\"{backgroundPaint.color.ToCodeString()}\" />");
                    }

                    if (backgroundPaint.gradientStops != null)
                    {
                        if (backgroundPaint.type.Equals("GRADIENT_LINEAR", StringComparison.CurrentCultureIgnoreCase))
                            builder.AppendLine($"{backgroundPaint.gradientStops.ToLinearGradientPaint()}");

                        if (backgroundPaint.type.Equals("GRADIENT_RADIAL", StringComparison.CurrentCultureIgnoreCase))
                            builder.AppendLine($"{backgroundPaint.gradientStops.ToRadialGradientPaint()}");
                    }

                    if (backgroundPaint.imageRef != null)
                        builder.AppendLine("\t\t<SolidColorBrush Color=\"White\" />");

                    builder.AppendLine("\t</alohakit:Ellipse.Fill>");
                }
            }

            if (elipseNode.effects != null && elipseNode.effects.Length > 0)
            {
                var dropShadow = elipseNode.effects
                    .Where(e => e.type.Equals("DROP_SHADOW", StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

                if (dropShadow != null)
                {
                    builder.AppendLine("\t<alohakit:Ellipse.Shadow>");

                    builder.AppendLine($"{dropShadow.ToShadow()}");

                    builder.AppendLine("\t</alohakit:Ellipse.Shadow>");
                }
            }

            builder.AppendLine("</alohakit:Ellipse>");

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
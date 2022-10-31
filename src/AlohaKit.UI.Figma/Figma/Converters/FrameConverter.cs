using AlohaKit.UI.Figma.Extensions;
using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace AlohaKit.UI.Figma.Converters
{
    public class FrameConverter : FrameConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaFrame frameNode)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<alohakit:RoundRectangle");

            var bounds = frameNode.absoluteBoundingBox;

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            builder.AppendLine($"\tX=\"{bounds.X.ToString(nfi)}\"");
            builder.AppendLine($"\tY=\"{bounds.Y.ToString(nfi)}\"");

            builder.AppendLine($"\tWidthRequest=\"{bounds.Width.ToString(nfi)}\"");
            builder.AppendLine($"\tHeightRequest=\"{bounds.Height.ToString(nfi)}\"");

            if (frameNode.opacity != 1)
                builder.AppendLine($"\tOpacity=\"{frameNode.opacity.ToString(nfi)}\"");

            if (!frameNode.visible)
                builder.AppendLine($"\tIsVisible=\"{frameNode.visible}\"");

            var cornerRadius = frameNode.cornerRadius;
            builder.AppendLine($"\tCornerRadius=\"{cornerRadius.ToString(nfi)}\"");
            
            if (frameNode.HasStrokes)
            {
                var strokePaint = frameNode.strokes.FirstOrDefault();

                if (strokePaint.color != null)
                {
                    builder.AppendLine($"\tStroke=\"{strokePaint.color.ToCodeString()}\"");
                }

                if (frameNode.strokeWeight != 0)
                {
                    var strokeSize = frameNode.strokeWeight;
                    builder.AppendLine($"\tStrokeThickness=\"{strokeSize}\"");
                }
            }

            builder.Append("\t>");

            if (frameNode.HasFills)
            {
                var backgroundPaint = frameNode.fills.FirstOrDefault();

                if (backgroundPaint != null && backgroundPaint.visible)
                {
                    builder.AppendLine("\n\t<alohakit:RoundRectangle.Fill>");

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

                    builder.AppendLine("\t</alohakit:RoundRectangle.Fill>");
                }
            }

            if (frameNode.effects != null && frameNode.effects.Length > 0)
            {
                var dropShadow = frameNode.effects
                    .Where(e => e.type.Equals("DROP_SHADOW", StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

                if (dropShadow != null)
                {
                    builder.AppendLine("\t<alohakit:RoundRectangle.Shadow>");

                    builder.AppendLine($"{dropShadow.ToShadow()}");

                    builder.AppendLine("\t</alohakit:RoundRectangle.Shadow>");
                }
            }

            builder.AppendLine("</alohakit:RoundRectangle>");

            return builder.ToString();
        }

        public override FigmaSharp.Views.IView ConvertToView(FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService)
        {
            throw new NotImplementedException();
        }

        public override Type GetControlType(FigmaNode currentNode)
            => typeof(View);

        public override bool ScanChildren(FigmaNode currentNode)
            => true;
    }
}

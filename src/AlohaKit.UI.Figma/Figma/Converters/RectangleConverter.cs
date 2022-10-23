using AlohaKit.UI.Figma.Extensions;
using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace AlohaKit.UI.Figma.Converters
{
    internal class RectangleConverter : RectangleVectorConverterBase
    {
        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not RectangleVector rectangleVector)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine("<alohakit:Rectangle");

            var bounds = rectangleVector.absoluteBoundingBox;

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            builder.AppendLine($"\tX=\"{bounds.X.ToString(nfi)}\"");
            builder.AppendLine($"\tY=\"{bounds.Y.ToString(nfi)}\"");

            builder.AppendLine($"\tWidthRequest=\"{bounds.Width.ToString(nfi)}\"");
            builder.AppendLine($"\tHeightRequest=\"{bounds.Height.ToString(nfi)}\"");

            if (rectangleVector.HasStrokes)
            {
                var strokePaint = rectangleVector.strokes.FirstOrDefault();

                if (strokePaint.color != null)
                {
                    builder.AppendLine($"\tStroke=\"{strokePaint.color.ToCodeString()}\"");
                }

                if (rectangleVector.strokeWeight != 0)
                {
                    var strokeSize = rectangleVector.strokeWeight;
                    builder.AppendLine($"\tStrokeThickness=\"{strokeSize}\"");
                }
            }

            builder.Append("\t>");

            if (rectangleVector.HasFills)
            {
                var backgroundPaint = rectangleVector.fills.FirstOrDefault();

                if (backgroundPaint != null && backgroundPaint.visible)
                {
                    builder.AppendLine("\n\t<alohakit:Rectangle.Fill>");

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

                    builder.AppendLine("\t</alohakit:Rectangle.Fill>");
                }
            }

            builder.AppendLine("</alohakit:Rectangle>");

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

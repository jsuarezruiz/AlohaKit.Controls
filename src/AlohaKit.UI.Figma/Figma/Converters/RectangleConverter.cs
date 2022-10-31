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

            string rectangle = "Rectangle";

            if (rectangleVector.cornerRadius > 0)
                rectangle = "RoundRectangle";

            builder.AppendLine($"<alohakit:{rectangle}");

            var bounds = rectangleVector.absoluteBoundingBox;

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            builder.AppendLine($"\tX=\"{bounds.X.ToString(nfi)}\"");
            builder.AppendLine($"\tY=\"{bounds.Y.ToString(nfi)}\"");

            builder.AppendLine($"\tWidthRequest=\"{bounds.Width.ToString(nfi)}\"");
            builder.AppendLine($"\tHeightRequest=\"{bounds.Height.ToString(nfi)}\"");

            if (rectangleVector.opacity != 1)
                builder.AppendLine($"\tOpacity=\"{rectangleVector.opacity.ToString(nfi)}\"");

            if (!rectangleVector.visible)
                builder.AppendLine($"\tIsVisible=\"{rectangleVector.visible}\"");
           
            if (rectangleVector.cornerRadius > 0)
                builder.AppendLine($"\tCornerRadius=\"{rectangleVector.cornerRadius.ToString(nfi)}\"");

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

            if (rectangleVector.effects != null && rectangleVector.effects.Length > 0)
            {
                var dropShadow = rectangleVector.effects
                    .Where(e => e.type.Equals("DROP_SHADOW", StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

                if (dropShadow != null)
                {
                    builder.AppendLine($"\t<alohakit:{rectangle}.Shadow>");

                    builder.AppendLine($"{dropShadow.ToShadow()}");

                    builder.AppendLine($"\t</alohakit:{rectangle}.Shadow>");
                }
            }

            builder.Append("\t>");

            if (rectangleVector.HasFills)
            {
                var backgroundPaint = rectangleVector.fills.FirstOrDefault();

                if (backgroundPaint != null && backgroundPaint.visible)
                {
                    builder.AppendLine($"\n\t<alohakit:{rectangle}.Fill>");

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

                    builder.AppendLine($"\t</alohakit:{rectangle}.Fill>");
                }
            }

            builder.AppendLine($"</alohakit:{rectangle}> ");

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

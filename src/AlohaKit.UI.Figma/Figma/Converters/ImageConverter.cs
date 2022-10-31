using AlohaKit.UI.Figma.Extensions;
using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.Services;
using System.Globalization;
using System.Text;

namespace AlohaKit.UI.Figma.Converters
{
    internal class ImageConverter : NodeConverter
    {
        public override bool CanConvert(FigmaNode currentNode)
            => currentNode.GetType() == typeof(FigmaVector);

        public override string ConvertToCode(CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService)
        {
            if (currentNode.Node is not FigmaVector figmaVector)
            {
                return string.Empty;
            }

            if (figmaVector.fillGeometry == null || figmaVector.fillGeometry.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            NumberFormatInfo nfi = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            builder.AppendLine("<alohakit:Path");

            var bounds = figmaVector.absoluteBoundingBox;

            builder.AppendLine($"\tX=\"{bounds.X.ToString(nfi)}\"");
            builder.AppendLine($"\tY=\"{bounds.Y.ToString(nfi)}\"");

            builder.AppendLine($"\tWidthRequest=\"{bounds.Width.ToString(nfi)}\"");
            builder.AppendLine($"\tHeightRequest=\"{bounds.Height.ToString(nfi)}\"");

            if (figmaVector.opacity != 1)
                builder.AppendLine($"\tOpacity=\"{figmaVector.opacity.ToString(nfi)}\"");

            if (!figmaVector.visible)
                builder.AppendLine($"\tIsVisible=\"{figmaVector.visible}\"");

            if (figmaVector.fillGeometry.Length > 0)
            {
                var geometry = figmaVector.fillGeometry[0];
                builder.AppendLine($"\tData=\"{geometry.path}\"");
            }

            if (figmaVector.HasStrokes)
            {
                var strokePaint = figmaVector.strokes.FirstOrDefault();

                if (strokePaint.color != null)
                {
                    builder.AppendLine($"\tStroke=\"{strokePaint.color.ToCodeString()}\"");
                }

                if (figmaVector.strokeWeight != 0)
                {
                    var strokeSize = figmaVector.strokeWeight;
                    builder.AppendLine($"\tStrokeThickness=\"{strokeSize}\"");
                }
            }

            builder.Append("\t>");

            if (figmaVector.HasFills)
            {
                var backgroundPaint = figmaVector.fills.FirstOrDefault();

                if (backgroundPaint != null && backgroundPaint.visible)
                {
                    builder.AppendLine("\n\t<alohakit:Path.Fill>");

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

                    builder.AppendLine("\t</alohakit:Path.Fill>");
                }
            }

            if (figmaVector.effects != null && figmaVector.effects.Length > 0)
            {
                var dropShadow = figmaVector.effects
                    .Where(e => e.type.Equals("DROP_SHADOW", StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

                if (dropShadow != null)
                {
                    builder.AppendLine("\t<alohakit:Path.Shadow>");

                    builder.AppendLine($"{dropShadow.ToShadow()}");

                    builder.AppendLine("\t</alohakit:Path.Shadow>");
                }
            }

            builder.AppendLine("</alohakit:Path>");

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

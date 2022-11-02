using FigmaSharp.Models;
using System.Text;

namespace AlohaKit.UI.Figma.Extensions
{
    public static class FigmaExtensions
    {
        public static string ToCodeString(this FigmaSharp.Color color)
        {
            int red = Convert.ToInt32(color.R * 255);
            int green = Convert.ToInt32(color.G * 255);
            int blue = Convert.ToInt32(color.B * 255);

            return "#" + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2");
        }

        public static string ToLinearGradientPaint(this ColorStop[] colorStops)
        {
            StringBuilder builder = new StringBuilder();
             
            builder.Append("\t\t<LinearGradientBrush>");
            builder.AppendLine("\n\t\t\t<LinearGradientBrush.GradientStops>");

            foreach (var colorStop in colorStops)
            {
                var color = colorStop.color;
                string hexColor = color.ToCodeString();
        
                builder.AppendLine($"\t\t\t\t<GradientStop Offset=\"{colorStop.position}\" Color= \"{hexColor}\" />");
            }

            builder.AppendLine("\t\t\t</LinearGradientBrush.GradientStops>");

            builder.Append("\t\t</LinearGradientBrush>");

            return builder.ToString();
        }
        
        public static string ToRadialGradientPaint(this ColorStop[] colorStops)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("\t\t<RadialGradientBrush>");
            builder.AppendLine("\n\t\t\t<RadialGradientBrush.GradientStops>");

            foreach (var colorStop in colorStops)
            {
                var color = colorStop.color;
                string hexColor = color.ToCodeString();

                builder.AppendLine($"\t\t\t\t<GradientStop Offset=\"{colorStop.position}\" Color= \"{hexColor}\" />");
            }

            builder.AppendLine("\t\t\t</RadialGradientBrush.GradientStops>");

            builder.Append("\t\t</RadialGradientBrush>");

            return builder.ToString();
        }

        public static string ToShadow(this FigmaEffect dropShadow)
        {
            StringBuilder builder = new StringBuilder();

            var offset = dropShadow.offset;

            var radius = dropShadow.radius;

            var color = dropShadow.color;
            string hexColor = color.ToCodeString();

            builder.AppendLine($"\t\t<alohakit:Shadow Offset=\"{offset.x}, {offset.y}\" Radius=\"{radius}\" Color= \"{hexColor}\" />");

            return builder.ToString();
        }

        public static string ToCodeString(this float[] values)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("new float[] {");
            var separator = ",";
            int i = 0;

            foreach (var value in values)
            {
                builder.Append($"{ToCodeString(value)}{(i < values.Length ? separator : string.Empty)}");
                i++;
            }

            builder.Append("}");

            return builder.ToString();
        }

        public static string ToCodeString(this float value)
        {
            return string.Concat(value.ToString(), "f");
        }

        public static string ToCodeString(this double value)
        {
            return string.Concat(value.ToString(), "f");
        }

        public static string ToCodeString(this FigmaTypeStyle style)
        {
            var fontFamily = style.fontPostScriptName ?? style.fontFamily;
            var fontWeight = style.fontWeight;

            string fontStyleType = "FontStyleType.Normal";

            if (!string.IsNullOrEmpty(fontFamily) && fontFamily.Contains("Italic", StringComparison.CurrentCultureIgnoreCase))
                fontStyleType = "FontStyleType.Italic";

            return $"new Microsoft.Maui.Graphics.Font(\"{fontFamily}\", {fontWeight}, {fontStyleType})";
        }
   
        public static string ToHorizontalAignment(this string value)
        {
            switch(value)
            {
                case "LEFT":
                    return "HorizontalAlignment.Left";
                case "CENTER":
                    return "HorizontalAlignment.Center";
                case "RIGHT":
                    return "HorizontalAlignment.Right";
                case "SCALE":
                    return "HorizontalAlignment.Justified";
                default:
                    return "HorizontalAlignment.Left";
            }
        }

        public static string ToVerticalAlignment(this string value)
        {
            switch (value)
            {
                case "TOP":
                    return "VerticalAlignment.Top";
                case "CENTER":
                case "SCALE":
                    return "VerticalAlignment.Center";
                case "BOTTOM":
                    return "VerticalAlignment.Bottom";
                default:
                    return "VerticalAlignment.Top";
            }
        }
    }
}
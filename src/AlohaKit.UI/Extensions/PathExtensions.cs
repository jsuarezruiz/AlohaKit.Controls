using Microsoft.Maui.Controls.Shapes;

namespace AlohaKit.UI.Extensions
{
    public static class PathExtensions
    {
        public static T Data<T>(this T path, Geometry data) where T : Path
        {
            path.Data = data;

            return path;
        }
    }
}
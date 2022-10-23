using AlohaKit.UI.Figma.Extensions;

namespace AlohaKit.UI.Figma.Helpers
{
    public static class CodeGenerationHelpers
    {
        public static string GetConstructor(string viewName, Type type, bool includesVar = true)
        {
            return GetConstructor(viewName, type.FullName, includesVar);
        }

        public static string GetConstructor(string viewName, string typeFullName, bool includesVar = true)
        {
            return $"{(includesVar ? "var " : string.Empty)}{viewName} = new {typeFullName}();";
        }

        public static string GetPropertyEquality(string viewName, string propertyName, Enum value)
        {
            return GetPropertyEquality(viewName, propertyName, value.GetFullName());
        }

        public static string GetPropertyEquality(string viewName, string propertyName, bool value)
        {
            return GetPropertyEquality(viewName, propertyName, value.ToString());
        }

        public static string GetPropertyEquality(string viewName, string propertyName, string value, bool inQuotes = false, bool instanciate = false)
        {
            string fullPropertyName;
            if (string.IsNullOrEmpty(propertyName))
                fullPropertyName = viewName;
            else
                fullPropertyName = $"{viewName}.{propertyName}";
            return GetEquality(fullPropertyName, value, inQuotes, instanciate);
        }

        public static string GetEquality(string viewName, string value, bool inQuotes = false, bool instanciate = false)
        {
            if (inQuotes)
            {
                value = string.Format("\"{0}\"", value.Replace("\n", "\\n"));
            }

            var instanciateText = instanciate ? "var " : "";
            return $"{instanciateText}{viewName} = {value};";
        }

        public static string GetMethod(string viewName, string methodName, Enum parameter)
        {
            return GetMethod(viewName, methodName, parameter.GetFullName());
        }

        public static string GetMethod(string viewName, string methodName, bool value)
        {
            return GetMethod(viewName, methodName, value.ToString());
        }

        public static string GetMethod(string viewName, string methodName, string parameters, bool inQuotes = false, bool includesSemicolon = true)
        {
            parameters = inQuotes ? $"\"{parameters}\"" : parameters;
            var semicolon = includesSemicolon ? ";" : "";
            return $"{viewName}.{methodName} ({parameters}){semicolon}";
        }
    }
}

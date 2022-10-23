// Authors:
//   Jose Medrano <josmed@microsoft.com>
//
// Copyright (C) 2018 Microsoft, Corp
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit
// persons to whom the Software is furnished to do so, subject to the
// following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN
// NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE
// USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;

using FigmaSharp.Models;
using FigmaSharp.Services;
using FigmaSharp.Views;

namespace FigmaSharp.Converters
{
	public abstract class NodeConverter 
	{
		public abstract Type GetControlType (FigmaNode currentNode);

		public virtual bool IsLayer { get; }

        public virtual string Name { get; } = CodeRenderService.DefaultViewName;

        public virtual bool ScanChildren (FigmaNode currentNode)
        {
            return true;
            //return !(currentNode is FigmaInstance);
        }

        protected T ToEnum<T> (string value)
		{
			try {
				foreach (T suit in (T[])Enum.GetValues (typeof (T))) {
					if (suit.ToString ().ToLower ().Equals (value, StringComparison.InvariantCultureIgnoreCase)) {
						return suit;
					}
				}
			} catch (System.Exception ex) {
				LoggingService.LogError("[FIGMA] Error", ex);

			}
			return default (T);
		}

		protected Dictionary<string, string> GetKeyValues (FigmaNode currentNode)
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();
			var index = currentNode.name.IndexOf ($"type:", System.StringComparison.InvariantCultureIgnoreCase);
			if (index > -1) {
				var properties = currentNode.name.Split (' ');
				foreach (var property in properties) {
					var data = property.Split (':');
					if (data.Length != 2) {
						LoggingService.LogError ($"Error format in parameter: '{property}'");
						continue;
					}
					ids.Add (data[0], data[1]);
				}
			} else {
				ids.Add ("type", currentNode.name);
			}
			return ids;
        }

		string RemoveQuotes(string data, bool enable = false)
		{
            if (enable)
            return data.Replace("\"", "");
            return data;
        }

        protected string GetIdentifierValue (string data, string parameter, bool removeQuotes = false)
        {
            var index = data.IndexOf($"{parameter}:", System.StringComparison.InvariantCultureIgnoreCase);
            if (index > -1)
            {
                var delta = data.Substring(index + $"{parameter}=".Length);
                var endIndex = delta.IndexOf(" ", System.StringComparison.InvariantCultureIgnoreCase);

                if (endIndex == -1)
                    return RemoveQuotes (delta, removeQuotes);
                return RemoveQuotes (delta.Substring(0, endIndex), removeQuotes);
            }
			return null;
        }

        internal virtual bool HasWidthConstraint ()
        {
            return true;
        }

        internal virtual bool HasHeightConstraint()
        {
            return true;
        }

        internal virtual bool IsFlexibleHorizontal(FigmaNode node)
        {
            if (node is IConstraints constrainedNode)
                return constrainedNode != null && constrainedNode.constraints.IsFlexibleHorizontal;
            return false;
        }

        internal virtual bool IsFlexibleVertical(FigmaNode node)
        {
            if (node is IConstraints constrainedNode)
                return constrainedNode != null && constrainedNode.constraints.IsFlexibleVertical;
            return false;
        }

        protected bool ContainsType (FigmaNode currentNode, string name)
        {
			var identifier = GetIdentifierValue (currentNode.name, "type") ?? currentNode.name;
			return identifier == name;
		}

        public abstract bool CanConvert (FigmaNode currentNode);

        public virtual bool CanCodeConvert(FigmaNode currentNode) => CanConvert(currentNode);

        public abstract IView ConvertToView (FigmaNode currentNode, ViewNode parent, ViewRenderService rendererService);

        public abstract string ConvertToCode (CodeNode currentNode, CodeNode parentNode, ICodeRenderService rendererService);
    }
}

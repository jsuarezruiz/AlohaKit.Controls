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
using System.Linq;
using System.Text;

using FigmaSharp.Models;

namespace FigmaSharp
{
    public static class ServiceExtensions
	{
        public static bool HasChildrenVisible (this IFigmaNodeContainer optionsNode, string layerName)
        {
            return optionsNode.children.FirstOrDefault(s => s.name == layerName)
                ?.visible ?? false;
        }

        public static IEnumerable<T> GetChildren<T>(this FigmaNode node)
        {
            if (node is IFigmaNodeContainer container)
                return container.children.OfType<T>();
            return Enumerable.Empty<T>();
        }

        public static bool HasChildrenVisible(this FigmaNode figmaNode, string layerName)
        {
            return figmaNode is IFigmaNodeContainer nodeContainer && nodeContainer.HasChildrenVisible(layerName);
        }

        public static IEnumerable<FigmaFileVersion> GroupByCreatedAt (this IEnumerable<FigmaFileVersion> sender)
        {
            return sender.GroupBy(x => x.created_at)
                        .Select(group => group.First());
        }

        public static Rectangle GetCurrentBounds (this FigmaCanvas canvas)
        {
            Rectangle contentRect = Rectangle.Zero;
            for (int i = 0; i < canvas.children.Length; i++) {
                if (canvas.children[i] is IAbsoluteBoundingBox box) {
                    if (i == 0) {
                        contentRect = box.absoluteBoundingBox;
                    } else {
                        contentRect = contentRect.UnionWith (box.absoluteBoundingBox);
                    }
                }
            }
            return contentRect;
        }

        public static FigmaCanvas GetCurrentCanvas (this FigmaNode node)
        {
            if (node.Parent is FigmaCanvas figmaCanvas)
                return figmaCanvas;

            if (node.Parent == null)
                return null;

            return GetCurrentCanvas (node.Parent);
        }

        public static void AppendLineIfValue (this StringBuilder sender, string value)
        {
            if (!string.IsNullOrEmpty (value))
                sender.AppendLine (value);
        }

        public static bool ContainsSourceImage (this FigmaNode node)
		{
            FigmaPaint[] fills = null;
            if (node is FigmaFrame frame) {
                fills = frame.fills;
            }
            else if (node is FigmaVector vector)
            {
                fills = vector.fills;
            }
            if (fills != null)
            {
                return fills.OfType<FigmaPaint>()
                .Any(s => s.type == "IMAGE" && !string.IsNullOrEmpty(s.imageRef));
            }

            return false;
		}

        public static string FilterName(string name)
        {
            if (name.StartsWith("!") || name.StartsWith("#"))
                return name.Substring(1);
            return name;
        }

        public static string GetNodeTypeName (this FigmaNode node)
		{
            if (string.IsNullOrEmpty(node.name))
                return string.Empty;

            var name = FilterName (node.name);
            var index = name.IndexOf (' ');
            if (index > -1 && index < name.Length - 1) {
                name = name.Substring (0,index);
            }
            return name;
        }

		static string RemoveIllegalCharacters (string name)
		{
            name = name.Replace ("-", "");
            return name;
		}

        public static bool TryGetCodeViewName (this FigmaNode node, out string customName)
        {
            if (node.TryGetNodeCustomName (out customName)) {
                customName = RemoveIllegalCharacters (customName);
                return true;
            };
            customName = RemoveIllegalCharacters (node.name);
            return false;
        }

        public static string GetClassName (this FigmaNode node)
        {
            var name = node.name;
            var index = name.IndexOf ('\"');
            if (index > -1 && index < name.Length - 1) {
                name = name.Substring (index + 1);
                index = name.IndexOf ('\"');
                if (index > -1 && index < name.Length) {
                    name = name.Substring (0, index);

                    return name
                .Replace (" ", string.Empty)
                .Replace (".", string.Empty);
                }
            }

            name = node.name;
            //HACK: we need to fix this
            index = name.IndexOf (' ');
            if (index > -1 && index < name.Length - 1) {
                name = name.Substring (index + 1);

				//names cannot be only integers, we get default name
				if (int.TryParse (name, out _))
                    name = node.name;
            }

            return name
                .Replace (" ", string.Empty)
                .Replace (".", string.Empty);
        }

        public static T FindNativeViewByName<T>(this Services.ViewRenderService rendererService, string name)
		{
			foreach (var node in rendererService.NodesProcessed)
			{
				if (node.View.NativeObject is T && node.Node.name == name)
				{
					return (T)node.View.NativeObject;
				}
			}
			return default(T);
		}

		public static IEnumerable<T> FindNativeViewsByName<T>(this Services.ViewRenderService rendererService, string name)
		{
			foreach (var node in rendererService.NodesProcessed)
			{
				if (node.View.NativeObject is T && node.Node.name == name)
				{
					yield return (T)node.View.NativeObject;
				}
			}
		}

		public static IEnumerable<T> FindNativeViewsStartsWith<T>(this Services.ViewRenderService rendererService, string name, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
		{
			foreach (var node in rendererService.NodesProcessed)
			{
				if (node.View.NativeObject is T && node.Node.name.StartsWith(name, stringComparison))
				{
					yield return (T)node.View.NativeObject;
				}
			}
		}
	}
}

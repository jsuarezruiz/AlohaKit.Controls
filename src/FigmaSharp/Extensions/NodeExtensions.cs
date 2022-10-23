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

using FigmaSharp.Models;

namespace FigmaSharp
{
    public static class NodeExtensions
    {
        public static bool TryGetAttributeValue(this FigmaNode node, string parameter, out string value)
        {
            //we want remove special chars
            parameter = FigmaSharp.ServiceExtensions.FilterName(parameter);

            value = node.name;
            try
            {
                var index = value.IndexOf(parameter);
                if (index > -1 && index < value.Length)
                {
                    value = value.Substring(index + parameter.Length);
                    index = value.IndexOf("\"");
                    if (index > -1 && index < value.Length)
                    {
                        value = value.Substring(index + 1);
                        index = value.IndexOf("\"");
                        if (index > -1 && index < value.Length)
                        {
                            value = value.Substring(0, index);
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            value = null;
            return false;
        }

        public static bool TryGetChildPropertyValue(this FigmaNode node, string property, out string value)
        {
            if (node is IFigmaNodeContainer container && container.children != null)
            {
                foreach (var item in container.children)
                {
                    if (TryGetAttributeValue(item, property, out value))
                    {
                        return true;
                    }
                }
            }
            value = null;
            return false;
        }

        public static bool IsStackView (this FigmaNode node)
        {
            if (node is FigmaFrame frame)
            {
                if (frame.LayoutMode == FigmaLayoutMode.Horizontal ||
                    frame.LayoutMode == FigmaLayoutMode.Vertical)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool TryGetNodeCustomName(this FigmaNode node, out string customName)
        {
            customName = node.name;
            var index = customName.IndexOf('\"');

            if (index > 2 && (customName[index - 1] == ':' || customName[index - 1] == '='))
            {
                return false;
            }

            if (index > -1 && index < customName.Length - 1)
            {
                customName = customName.Substring(index + 1);

                index = customName.IndexOf('\"');
                if (index > -1 && index < customName.Length)
                {
                    customName = customName.Substring(0, index);
                    //customName = RemoveIllegalCharacters (customName);
                    return true;
                }
            }
            customName = node.name;
            //customName = RemoveIllegalCharacters (node.name);
            return false;
        }

        public static void CalculateBounds(this IFigmaNodeContainer figmaNodeContainer)
        {
            if (figmaNodeContainer is IAbsoluteBoundingBox calculatedBounds)
            {
                calculatedBounds.absoluteBoundingBox = Rectangle.Zero;
                foreach (var item in figmaNodeContainer.children)
                {
                    if (item is IAbsoluteBoundingBox itmBoundingBox)
                    {
                        calculatedBounds.absoluteBoundingBox.X = Math.Min(calculatedBounds.absoluteBoundingBox.X, itmBoundingBox.absoluteBoundingBox.X);
                        calculatedBounds.absoluteBoundingBox.Y = Math.Min(calculatedBounds.absoluteBoundingBox.Y, itmBoundingBox.absoluteBoundingBox.Y);

                        if (itmBoundingBox.absoluteBoundingBox.X + itmBoundingBox.absoluteBoundingBox.Width > calculatedBounds.absoluteBoundingBox.X + calculatedBounds.absoluteBoundingBox.Width)
                        {
                            calculatedBounds.absoluteBoundingBox.Width += (itmBoundingBox.absoluteBoundingBox.X + itmBoundingBox.absoluteBoundingBox.Width) - (calculatedBounds.absoluteBoundingBox.X + calculatedBounds.absoluteBoundingBox.Width);
                        }

                        if (itmBoundingBox.absoluteBoundingBox.Y + itmBoundingBox.absoluteBoundingBox.Height > calculatedBounds.absoluteBoundingBox.Y + calculatedBounds.absoluteBoundingBox.Height)
                        {
                            calculatedBounds.absoluteBoundingBox.Height += (itmBoundingBox.absoluteBoundingBox.Y + itmBoundingBox.absoluteBoundingBox.Height) - (calculatedBounds.absoluteBoundingBox.Y + calculatedBounds.absoluteBoundingBox.Height);
                        }
                    }
                }
            }
        }

        ////TODO: Change to async multithread
        //public static async Task SaveFigmaImageFiles(this FigmaPaint[] paints, string fileId, string directoryPath, string format = ".png")
        //{
        //    var ids = paints.Select(s => s.ID).ToArray();
        //    var query = new FigmaImageQuery(AppContext.Current.Token, fileId, ids);
        //    var images = FigmaApiHelper.GetFigmaImage(query);
        //    if (images != null)
        //    {
        //        var urls = paints.Select(s => images.images[s.ID]).ToArray();
        //        FileHelper.SaveFiles(directoryPath, format, urls);
        //    }
        //}

        public static IEnumerable<FigmaNode> FindImageNodes(this FigmaNode sender, Func<FigmaNode, bool> condition = null)
        {
            if (sender is Models.IFigmaImage && (condition == null || condition(sender)))
            {
                yield return sender;
            }

            if (sender is IFigmaNodeContainer container)
            {
                foreach (var child in container.children)
                {
                    foreach (var image in child.FindImageNodes())
                    {
                        yield return image;
                    }
                }
            }
            else if (sender is FigmaDocument document)
            {
                foreach (var child in document.children)
                {
                    foreach (var image in child.FindImageNodes())
                    {
                        yield return image;
                    }
                }
            }
        }

        public static IEnumerable<FigmaNode> FindNode(this FigmaNode sender, Func<FigmaNode, bool> condition)
        {
            if (condition(sender))
            {
                yield return sender;
            }

            if (sender is IFigmaNodeContainer container)
            {
                foreach (var child in container.children)
                {
                    foreach (var image in child.FindNode(condition))
                    {
                        yield return image;
                    }
                }
            }
            else if (sender is FigmaDocument document)
            {
                foreach (var child in document.children)
                {
                    foreach (var image in child.FindNode(condition))
                    {
                        yield return image;
                    }
                }
            }
        }

        public static Rectangle GetBoundRectangle(this IEnumerable<FigmaNode> customViews, bool includeHidden = false)
        {
            Rectangle point = null;
            FigmaNode item;
            for (int i = 0; i < customViews.Count(); i++)
            {
                item = customViews.ElementAt(i);
                if (!includeHidden && !item.visible)
                    continue;
                if (item is IAbsoluteBoundingBox boundingBox)
                {
                    if (point == null)
                    {
                        point = boundingBox.absoluteBoundingBox.Copy();
                    }
                    else
                    {
                        if (boundingBox.absoluteBoundingBox.Left < point.X)
                            point.X = boundingBox.absoluteBoundingBox.Left;
                        if (boundingBox.absoluteBoundingBox.Top < point.Y)
                            point.Y = boundingBox.absoluteBoundingBox.Top;

                        if (boundingBox.absoluteBoundingBox.Right > point.Right)
                            point.Width = boundingBox.absoluteBoundingBox.Right - point.Left;
                        if (boundingBox.absoluteBoundingBox.Bottom > point.Bottom)
                            point.Height = boundingBox.absoluteBoundingBox.Bottom - point.Top;
                    }
                }
            }
            return point;
        }

        public static void Recursively(this FigmaNode[] customViews, string filter, List<FigmaNode> viewsFound)
        {
            foreach (var item in customViews)
            {
                Recursively(item, filter, viewsFound);
            }
        }

        public static void Recursively(this FigmaNode customView, string filter, List<FigmaNode> viewsFound)
        {
            if (customView.name == filter)
            {
                viewsFound.Add(customView);
            }

            if (customView is IFigmaNodeContainer container)
            {
                foreach (var item in container.children)
                {
                    Recursively(item, filter, viewsFound);
                }
            }
        }

        public static bool IsRenderSkipped(this FigmaNode figmaNode)
        {
            return figmaNode.name.StartsWith("//");
        }
    }
}

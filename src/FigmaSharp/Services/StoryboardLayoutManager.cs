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

using System.Linq;

using FigmaSharp.Models;
using FigmaSharp.PropertyConfigure;
using FigmaSharp.Views;

namespace FigmaSharp.Services
{
    public class StoryboardLayoutManager
    {
        public bool UsesConstraints { get; set; }
        public int HorizontalMargins { get; set; } = 64;
        public int VerticalMargins { get; set; } = 64;

        public void Run(IView contentView, ViewRenderService rendererService)
        {
            var mainNodes = rendererService.NodesProcessed.Where(s => s.Node.Parent is FigmaCanvas)
                .ToArray ();
            Run(mainNodes, contentView, rendererService);
        }

        public void Run (ViewNode[] mainViews, IView contentView, ViewRenderService rendererService)
        {
            var orderedNodes = mainViews
        .OrderBy(s => ((IAbsoluteBoundingBox)s.Node).absoluteBoundingBox.Left)
        .ThenBy(s => ((IAbsoluteBoundingBox)s.Node).absoluteBoundingBox.Top)
        .ToArray();

            //We want know the background color of the figma camvas and apply to our scrollview
            var firstNode = orderedNodes.FirstOrDefault();
            if (firstNode == null)
                return;

            var canvas = firstNode.ParentView?.Node as FigmaCanvas;
            if (canvas != null) {
                //contentView.BackgroundColor = canvas.backgroundColor;
                if (contentView.Parent is IScrollView scrollview) {
                    //scrollview.BackgroundColor = canvas.backgroundColor;

                    //we need correct current initial positioning
                    var rectangle = orderedNodes
                        .Select(s => s.Node)
                        .GetBoundRectangle();
                    scrollview.SetContentSize(rectangle.Width + VerticalMargins * 2, rectangle.Height + HorizontalMargins * 2);
                }
            }

            if (UsesConstraints) {
                foreach (var node in orderedNodes)
                {
                    Converters.NodeConverter converter = rendererService.GetConverter(node.Node);
                    rendererService.PropertySetter.Configure(PropertyNames.Constraints,
                        node, node.ParentView, converter, rendererService);
                }
            } else {
                var rectangle = orderedNodes
                       .Select(s => s.Node)
                       .GetBoundRectangle();

                foreach (var node in orderedNodes)
                {
                    //we need correct current initial positioning
                    if (node.Node is IAbsoluteBoundingBox box)
                    {
                        node.View.SetPosition(-rectangle.X + box.absoluteBoundingBox.X + VerticalMargins, -rectangle.Y + box.absoluteBoundingBox.Y + HorizontalMargins);
                    }
                } 
            }
        }
    }
}

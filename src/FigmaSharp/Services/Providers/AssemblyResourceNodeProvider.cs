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
using System.Reflection;

using FigmaSharp.Helpers;
using FigmaSharp.Views;

namespace FigmaSharp.Services
{
    public class AssemblyResourceNodeProvider : NodeProvider
    {
        public Assembly Assembly { get; set; }

        public AssemblyResourceNodeProvider(Assembly assembly, string file)
        {
            Assembly = assembly;
            File = file;
        }

        public override string GetContentTemplate(string file)
        {
            return AppContext.Current.GetManifestResource(Assembly, file);
        }

        public override void OnStartImageLinkProcessing(List<ViewNode> imageFigmaNodes)
        {
            LoggingService.LogInfo($"Loading images..");

            if (imageFigmaNodes.Count > 0)
            {
                foreach (var vector in imageFigmaNodes)
                {
                    var recoveredKey = ResourceHelper.FromLocalResourceNameToUrlResourceName(vector.Node.id);
                    var image = AppContext.Current.GetImageFromManifest(Assembly, recoveredKey);
                    if (image != null && vector.View is IImageView imageView)
                    {
                        imageView.Image = image;
                    }
                }
            }

            LoggingService.LogInfo("Ended image link processing");
            OnImageLinkProcessed();
        }
    }
}

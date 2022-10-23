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
using System.IO;

using FigmaSharp.Helpers;
using FigmaSharp.Views;

namespace FigmaSharp.Services
{
    public class FileNodeProvider : NodeProvider
    {
        public FileNodeProvider(string resourcesDirectory)
        {
            ResourcesDirectory = resourcesDirectory;
        }

        public override string GetContentTemplate(string file)
        {
            return System.IO.File.ReadAllText(file);
        }

        public string ResourcesDirectory { get; set; }

        public string ImageFormat { get; set; } = ".png";

        public override void OnStartImageLinkProcessing(List<ViewNode> imageFigmaNodes)
        {
            //not needed in local files
            LoggingService.LogInfo($"Loading images..");

            if (imageFigmaNodes.Count > 0)
            {
                foreach (var vector in imageFigmaNodes)
                {
                    try
                    {
                        var recoveredKey = ResourceHelper.FromLocalResourceNameToUrlResourceName(vector.Node.id);
                        string filePath = Path.Combine(ResourcesDirectory, string.Concat(recoveredKey, ImageFormat));

                        if (!System.IO.File.Exists(filePath))
                        {
                            throw new FileNotFoundException(filePath);
                        }

                        if (vector.View is IImageView imageView)
                        {
                            var image = AppContext.Current.GetImageFromFilePath(filePath);
                            imageView.Image = image;
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        LoggingService.LogError("[FIGMA.RENDERER] Resource '{0}' not found.", ex);
                    }
                    catch (Exception ex)
                    {
                        LoggingService.LogError("[FIGMA.RENDERER] Error.", ex);
                    }
                }
            }

            LoggingService.LogInfo("Ended image link processing");
            OnImageLinkProcessed();
        }
    }
}

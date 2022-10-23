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
using System.Threading.Tasks;

using FigmaSharp.Models;
using FigmaSharp.Views;

namespace FigmaSharp.Services
{
    public class RemoteNodeProvider : NodeProvider
    {
        public FigmaFileVersion Version { get; set; }

        public override string GetContentTemplate(string file)
        {
            return AppContext.Api.GetContentFileAsync(new FigmaFileQuery(file, Version)).GetAwaiter().GetResult();
        }

        public IEnumerable<string> GetKeys(List<FigmaImageResponse> responses, string image)
        {
            foreach (var item in responses)
            {
                foreach (var keys in item.images.Where(s => s.Value == image))
                {
                    yield return keys.Key;
                }
            }
        }

        void ProcessRemoteImages(List<ViewNode> imageFigmaNodes, ImageFormat imageFormat)
        {
            try
            {
                var totalImages = imageFigmaNodes.Count();
                //TODO: figma url has a limited character in urls we fixed the limit to 10 ids's for each call
                var numberLoop = (totalImages / CallNumber) + 1;

                //var imageCache = new Dictionary<string, List<string>>();
                List<Tuple<string, List<string>>> imageCacheResponse = new List<Tuple<string, List<string>>>();
                LoggingService.LogInfo("Detected a total of {0} possible {1} images.  ", totalImages, imageFormat);

                var images = new List<string>();
                for (int i = 0; i < numberLoop; i++)
                {
                    var vectors = imageFigmaNodes.Skip(i * CallNumber).Take(CallNumber);
                    LoggingService.LogInfo("[{0}/{1}] Processing Images ... {2} ", i, numberLoop, vectors.Count());
                    var ids = vectors.Select(s => CreateEmptyImageNodeRequest(s.Node))
                        .ToArray();

                    var figmaImageResponse = AppContext.Api.GetImagesAsync(File, ids, imageFormat).GetAwaiter().GetResult();
                    if (figmaImageResponse != null)
                    {
                        foreach (var image in figmaImageResponse.images)
                        {
                            if (image.Value == null)
                            {
                                continue;
                            }

                            var img = imageCacheResponse.FirstOrDefault(s => image.Value == s.Item1);
                            if (img?.Item1 != null)
                            {
                                img.Item2.Add(image.Key);
                            }
                            else
                            {
                                imageCacheResponse.Add(new Tuple<string, List<string>>(image.Value, new List<string>() { image.Key }));
                            }
                        }
                    }
                }

                //get images not dupplicates
                LoggingService.LogInfo("Finished image to download {0}", images.Count);

                if (imageFormat == ImageFormat.svg)
                {
                    throw new NotImplementedException("svg not implemented");
                    //with all the keys now we get the dupplicated images
                    foreach (var imageUrl in imageCacheResponse)
                    {
                        var image = Helpers.WebApiHelper.GetUrlContent(imageUrl.Item1, null);

                        foreach (var figmaNodeId in imageUrl.Item2)
                        {
                            var vector = imageFigmaNodes.FirstOrDefault(s => s.Node.id == figmaNodeId);
                            LoggingService.LogInfo("[{0}:{1}:{2}] {3}...", vector.Node.GetType(), vector.Node.id, vector.Node.name, imageUrl);

                            if (vector != null && vector.View is ISvgView imageView)
                            {
                                AppContext.Current.BeginInvoke(() =>
                                {
                                    imageView.Load(image);
                                });
                            }
                            LoggingService.LogInfo("OK \n");
                        }
                    }
                }
                else
                {
                    //with all the keys now we get the dupplicated images
                    foreach (var imageUrl in imageCacheResponse)
                    {
                        var Image = AppContext.Current.GetImage(imageUrl.Item1);
                        foreach (var figmaNodeId in imageUrl.Item2)
                        {
                            var vector = imageFigmaNodes.FirstOrDefault(s => s.Node.id == figmaNodeId);
                            LoggingService.LogInfo("[{0}:{1}:{2}] {3}...", vector.Node.GetType(), vector.Node.id, vector.Node.name, imageUrl);

                            if (vector != null)
                            {
                                AppContext.Current.BeginInvoke(() =>
                                {
                                    if (vector.View is IImageView imageView)
                                    {
                                        imageView.Image = Image;
                                    }
                                    else if (vector.View is IImageButton imageButton)
                                    {
                                        imageButton.Image = Image;
                                    }
                                    else
                                    {
                                        LoggingService.LogInfo("[{0}:{1}:{2}] Error cannot assign the image to the current view {3}", vector.Node.GetType(), vector.Node.id, vector.Node.name, vector.View.GetType().FullName);
                                    }
                                });
                            }
                            LoggingService.LogInfo("OK \n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.LogError("[FIGMA] Error.", ex);
            }
        }

        public override void OnStartImageLinkProcessing(List<ViewNode> imageFigmaNodes)
        {
            if (imageFigmaNodes.Count == 0)
            {
                OnImageLinkProcessed();
                return;
            }

            Task.Run(() =>
            {
                //var svgImages = imageFigmaNodes.Where(s => s.View is ISvgView).ToList();
                //if (svgImages.Count > 0)
                //    ProcessRemoteImages(svgImages, ImageFormat.svg);
                var images = imageFigmaNodes.Where(s => !(s.View is ISvgView)).ToList();
                if (images.Count > 0)
                    ProcessRemoteImages(images, ImageFormat.png);
                OnImageLinkProcessed();
            });
        }
        const int CallNumber = 250;
    }
}

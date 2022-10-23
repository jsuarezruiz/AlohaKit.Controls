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
using System.Threading.Tasks;

using FigmaSharp.Models;

namespace FigmaSharp.Services
{
    public interface INodeProvider
    {
        string File { get; }
        event EventHandler ImageLinksProcessed;
        List<FigmaNode> Nodes { get; }
        FigmaFileResponse Response { get; }

        string GetContentTemplate(string file);

        Task LoadAsync(string file);
        void Load(string file);
        void Save(string filePath);
        void OnStartImageLinkProcessing(List<ViewNode> imageVectors);

        FigmaNode[] GetMainGeneratedLayers();

        FigmaNode FindByFullPath(string fullPath);
        FigmaNode FindByPath(params string[] path);
        FigmaNode FindByName(string nodeName);

        bool TryGetMainInstance (FigmaInstance figmaInstance, out FigmaInstance outInstance);
        bool TryGetMainComponent (FigmaInstance figmaInstance, out FigmaComponentEntity outInstance);
        bool TryGetStyle(string fillStyleValue, out FigmaStyle style);

        bool RendersAsImage(FigmaNode figmaNode);
        void SaveResourceFiles(string destinationDirectory, string format, IImageNodeRequest[] downloadImages);
    }
}
 
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

using FigmaSharp.Converters;
using FigmaSharp.Models;
using FigmaSharp.PropertyConfigure;
using FigmaSharp.Services;
using FigmaSharp.Views;

namespace FigmaSharp
{
    /// <summary>
    /// Use FigmaFile to load a FigmaDocument from a .figma file bundled in your project.
    /// </summary>
    [Obsolete ("Not used anymore")]
    public class FigmaFile : IFigmaFile
    {
        public const string FigmaPackageId = "FigmaPackageId";
        public const string FigmaNodeCustomName = "FigmaNodeCustomName";

        /// <summary>
        /// Gets the figma images.
        /// </summary>
        /// <value>The figma images.</value>
        public List<IImageView> FigmaImages { get; private set; }

        /// <summary>
        /// Gets the document.
        /// </summary>
        /// <value>The document.</value>
        public FigmaFileResponse Document => figmaLocalFileProvider.Response;

        /// <summary>
        /// Gets the content view.
        /// </summary>
        /// <value>The content view.</value>
        public IView ContentView { get; private set; }

        readonly ViewRenderService rendererService;
        readonly AssemblyResourceNodeProvider figmaLocalFileProvider;

        string file;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FigmaSharp.FigmaFile"/> class.
        /// </summary>
        /// <param name="file">File.</param>
        /// <param name="figmaViewConverters">Figma view converters.</param>
        public FigmaFile (string file, NodeConverter[] figmaViewConverters, ViewPropertyConfigureBase propertyConfigure)
        {
            this.file = file;

            ContentView = AppContext.Current.CreateEmptyView();
            FigmaImages = new List<IImageView>();

            if (propertyConfigure == null)
                propertyConfigure = AppContext.Current.GetViewPropertyConfigure();

            var assembly = System.Reflection.Assembly.GetCallingAssembly();
            figmaLocalFileProvider = new AssemblyResourceNodeProvider(assembly, file);
            rendererService = new ViewRenderService(figmaLocalFileProvider, figmaViewConverters, propertyConfigure);
    
        }

        /// <summary>
        /// Reload the specified includeImages.
        /// </summary>
        public void Reload ()
        {
            LoggingService.LogInfo($"Loading views..");
            try
            {
                FigmaImages.Clear();
                rendererService. Start(file, ContentView);

                var mainNodes = rendererService.NodesProcessed
                    .Where(s => s.ParentView == null)
                    .ToArray();

                new StoryboardLayoutManager().Run(ContentView, rendererService);
            }
            catch (Exception ex)
            {
                LoggingService.LogError($"[FIGMA.FILE] Error reading resource", ex);
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        public void InitializeComponent ()
        {
            Reload ();
        }
    }
}

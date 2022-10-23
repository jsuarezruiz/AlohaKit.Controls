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
using System.Linq;
using System.Threading.Tasks;

using FigmaSharp.Helpers;
using FigmaSharp.Models;

namespace FigmaSharp.Services
{
    public abstract class NodeProvider : INodeProvider
	{
		public event EventHandler ImageLinksProcessed;

		public FigmaFileResponse Response { get; protected set; }
		public List<FigmaNode> Nodes { get; } = new List<FigmaNode> ();

		public bool ImageProcessed;

		internal void OnImageLinkProcessed ()
		{
			ImageProcessed = true;
			ImageLinksProcessed?.Invoke (this, new EventArgs ());
		}

		public string File { get; set; }

		public Task LoadAsync(string file) => Task.Run(() => Load(file));

		public void Load (string file)
		{
			this.File = file;

			ImageProcessed = false;
			try {
				Nodes.Clear ();

				var contentTemplate = GetContentTemplate (file);

				//parse the json into a model format
				Response = WebApiHelper.GetFigmaResponseFromFileContent (contentTemplate);

				//proceses all the views recursively
				foreach (var item in Response.document.children)
					ProcessNodeRecursively (item, null);
			} catch (System.Net.WebException ex) {
				if (!AppContext.Current.IsApiConfigured)
					LoggingService.LogError ($"Cannot connect to Figma server: TOKEN not configured.", ex);
				else
					LoggingService.LogError ($"Cannot connect to Figma server: wrong TOKEN?", ex);
			} catch (Exception ex) {
				LoggingService.LogError ($"Error reading remote resources. Ensure you added NewtonSoft nuget or cannot parse the to json?", ex);
			}
		}

		public FigmaNode[] GetMainGeneratedLayers ()
		{
			return GetMainLayers (s => s.TryGetNodeCustomName (out var customName) && !s.name.StartsWith("#") && !s.name.StartsWith("//")).ToArray();
		}

		public IEnumerable<FigmaNode> GetMainLayers (Func<FigmaNode, bool> action = null)
		{
			return Nodes.Where(s => s.Parent is FigmaCanvas && (action?.Invoke (s) ?? true));
		}

		public FigmaNode FindByFullPath (string fullPath)
		{
			return FindByPath (fullPath.Split ('/'));
		}

		public FigmaNode GetParentNode (FigmaNode node)
			=> Nodes.FirstOrDefault(s => s is IFigmaNodeContainer container && container.children.Any(c => c == node));

		/// <summary>
		/// Finds a node using the path of the views, returns null in case of no data
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public FigmaNode FindByPath (params string[] path)
		{
			if (path.Length == 0) {
				return null;
			}

			FigmaNode figmaNode = null;
			for (int i = 0; i < path.Length; i++) {
				if (i == 0)
					figmaNode = Nodes.FirstOrDefault (s => s.name == path[i]);
				else
					figmaNode = Nodes.FirstOrDefault (s => s.name == path[i] && s.Parent.id == figmaNode.id);

				if (figmaNode == null)
					return null;
			}
			return figmaNode;
		}

		public FigmaNode FindById(string id)
		{
			return Nodes.FirstOrDefault(s => s.id == id);
		}

		public FigmaNode FindByName (string name)
		{
			var quotedName = string.Format ("\"{0}\"", name);
			var found = Nodes.FirstOrDefault (s => s.name.Contains (quotedName));
			if (found != null) {
				return found;
			}
			return Nodes.FirstOrDefault (s => s.name == name);
		}

		public FigmaNode FindByCustomName(string name)
		{
			var found = Nodes.FirstOrDefault(s => s.TryGetNodeCustomName (out var customName) && customName == name);
			if (found != null)
			{
				return found;
			}
			return Nodes.FirstOrDefault(s => s.name == name);
		}

		void ProcessNodeRecursively (FigmaNode node, FigmaNode parent)
		{
			node.Parent = parent;
			Nodes.Add (node);

			if (node is FigmaInstance instance) {
				if (Response.components.TryGetValue (instance.componentId, out var figmaComponent))
					instance.Component = figmaComponent;
			}

			if (node is IFigmaNodeContainer nodeContainer) {
				foreach (var item in nodeContainer.children)
					ProcessNodeRecursively (item, node);
			}
		}

		public abstract string GetContentTemplate (string file);

		public abstract void OnStartImageLinkProcessing (List<ViewNode> imageFigmaNodes);

		public void Save (string filePath)
		{
			Response.Save (filePath);
		}

        public bool TryGetMainInstance(FigmaInstance nodeInstance, out FigmaInstance result)
        {
			//Get the instance
			var componentNode = GetMainGeneratedLayers();
			foreach (var item in componentNode)
			{
				if (item is FigmaInstance figmaInstance && figmaInstance.id == nodeInstance.componentId) {
					result = figmaInstance;
					return true;
                }
			}
			result = null;
			return false;
		}

		public bool TryGetMainComponent(FigmaInstance nodeInstance, out FigmaComponentEntity result)
		{
			//Get the instance
			var componentNode = GetMainGeneratedLayers();
			foreach (var item in componentNode)
			{
				if (item is FigmaComponentEntity figmaInstance && figmaInstance.id == nodeInstance.componentId)
				{
					result = figmaInstance;
					return true;
				}
			}
			result = null;
			return false;
		}

		public bool TryGetStyle(string fillStyleValue, out FigmaStyle style)
		{
			return Response.styles.TryGetValue(fillStyleValue, out style);
		}

        #region Image Resources

        public virtual void SaveResourceFiles(string destinationDirectory, string format, IImageNodeRequest[] downloadImages)
        {
			if (!Directory.Exists(destinationDirectory))
				throw new DirectoryNotFoundException(destinationDirectory);

			foreach (var downloadImage in downloadImages)
			{
                foreach (var imageScale in downloadImage.Scales)
                {
					if (string.IsNullOrEmpty(imageScale.Url))
						continue;

					string customNodeName = downloadImage.GetOutputFileName(imageScale.Scale);
					var fileName = string.Concat(customNodeName, format);
					var fullPath = Path.Combine(destinationDirectory, fileName);

					if (System.IO.File.Exists(fullPath))
						System.IO.File.Delete(fullPath);

					try
					{
						using (System.Net.WebClient client = new System.Net.WebClient())
							client.DownloadFile(new Uri(imageScale.Url), fullPath);
					}
					catch (Exception ex)
					{
						LoggingService.LogError("[FIGMA] Error.", ex);
					}
				}
			};
		}

        public virtual bool RendersAsImage (FigmaNode figmaNode)
		{
			if (figmaNode.ContainsSourceImage ())
				return true;

			if (figmaNode is IFigmaImage figmaImage && figmaImage.HasImage())
				return true;

			return false;
		}

		public virtual bool SearchImageChildren(FigmaNode figmaNode) => true;

		public IEnumerable<FigmaNode> SearchImageNodes (FigmaNode mainNode)
		{
			if (mainNode is FigmaInstance)
				yield break;

			if (RendersAsImage (mainNode))
            {
                yield return mainNode;
				yield break;
            }

			//we don't want iterate on children
			if (!SearchImageChildren (mainNode))
				yield break;

			if (mainNode is IFigmaNodeContainer nodeContainer)
            {
                foreach (var item in nodeContainer.children)
                {
                    foreach (var resultItems in SearchImageNodes (item))
                    {
                        yield return resultItems;
                    }
                }
            } else if (mainNode is FigmaDocument document)
            {
				foreach (var item in document.children)
				{
					foreach (var resultItems in SearchImageNodes(item))
					{
						yield return resultItems;
					}
				}
			}
		}

		public IEnumerable<FigmaNode> SearchImageNodes() => SearchImageNodes(Response.document);

		public virtual IImageNodeRequest CreateEmptyImageNodeRequest(FigmaNode node) => new ImageNodeRequest(node);

		#endregion
	}
}

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

using System.Collections.Generic;
using System.Linq;

using FigmaSharp.Converters;
using FigmaSharp.Models;

namespace FigmaSharp.Services
{
    public abstract class RenderService
    {
        protected readonly List<NodeConverter> DefaultConverters;
        public readonly List<NodeConverter> CustomConverters;

        protected RenderServiceOptions baseOptions;
        public ITranslationService TranslationService { get; internal set; }

        protected INodeProvider nodeProvider;
        public INodeProvider NodeProvider => nodeProvider;

        protected void SetOptions (RenderServiceOptions options)
        {
            baseOptions = options;
        }

        public RenderService(INodeProvider nodeProvider, NodeConverter[] nodeConverters, ITranslationService translationService = null)
        {
            this.TranslationService = translationService ?? new DefaultTranslationService ();
            this.nodeProvider = nodeProvider;
            DefaultConverters = nodeConverters.Where(s => s.IsLayer).ToList();
            CustomConverters = nodeConverters.Where(s => !s.IsLayer).ToList();
        }

        public FigmaNode FindNodeByName(string name)
        {
            return nodeProvider.Nodes.FirstOrDefault(s => s.name == name);
        }

        public FigmaNode FindNodeById(string id)
        {
            return nodeProvider.Nodes.FirstOrDefault(s => s.id == id);
        }

        FigmaNode GetRecursively(string name, FigmaNode figmaNode)
        {
            if (figmaNode.name == name)
            {
                return figmaNode;
            }

            if (figmaNode is IFigmaNodeContainer container)
            {
                foreach (var item in container.children)
                {
                    var node = GetRecursively(name, item);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        const string HasConstraintsParameter = "hasConstraints";
        internal virtual bool HasConstraints(FigmaNode currentNode, NodeConverter converter)
        {
            if (currentNode.TryGetAttributeValue(HasConstraintsParameter, out var value))
            {
                if (value == "true")
                    return true;
                if (value == "false")
                    return false;
            }
            return true;
        }

        internal virtual bool HasWidthConstraint(FigmaNode currentNode, NodeConverter converter)
        {
            return converter.HasWidthConstraint();
        }

        internal virtual bool HasHeightConstraint(FigmaNode currentNode, NodeConverter converter)
        {
            return converter.HasHeightConstraint();
        }

        internal virtual bool IsFlexibleVertical(ContainerNode currentNode, NodeConverter converter)
        {
            return converter.IsFlexibleVertical(currentNode.Node);
        }

        internal virtual bool IsFlexibleHorizontal(ContainerNode currentNode, NodeConverter converter)
        {
            return converter.IsFlexibleHorizontal(currentNode.Node);
        }

        public string GetTranslatedText(FigmaText text)
            => GetTranslatedText(text.characters, text.visible);

        public string GetTranslatedText(string text, bool textCondition = true)
        {
            //translation false 
            if (!textCondition)
                return string.Empty;

            text = text ?? string.Empty;
            text = text.Replace(System.Environment.NewLine, "\\n");
            if (baseOptions.TranslateLabels && TranslationService != null)
            {
                return TranslationService.GetTranslatedStringText(text);
            }
            return text;
        }

        public NodeConverter GetConverter(FigmaNode node)
        {
            var converter = GetProcessedConverter(node, CustomConverters);
            if (converter == null)
                converter = GetProcessedConverter(node, DefaultConverters);
            return converter;
        }

        protected NodeConverter GetProcessedConverter(FigmaNode currentNode, IEnumerable<NodeConverter> customViewConverters)
        {
            foreach (var customViewConverter in customViewConverters)
            {
                if (customViewConverter.CanConvert(currentNode))
                {
                    return customViewConverter;
                }
            }
            return null;
        }
    }
}

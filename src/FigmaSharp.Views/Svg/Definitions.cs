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
using System.Xml;
using System.Xml.Serialization;

namespace FigmaSharp.Views.Graphics
{
    [Serializable()]
    public class Definitions : HtmlElement
    {
    }

    [Serializable()]
    public class StyleDefinition : Definitions
    {
        [XmlAttribute(attributeName: "type")]
        public string ContentType { get; set; }

        public string Content { get; set; }

        [XmlText]
        public XmlNode[] CDataContent
        {
            get
            {
                var dummy = new XmlDocument();
                return new XmlNode[] { dummy.CreateCDataSection(Content) };
            }
            set
            {
                if (value == null)
                {
                    Content = null;
                    return;
                }

                if (value.Length != 1)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "Invalid array length {0}", value.Length));
                }

                Content = value[0].Value;
            }
        }
    }

    [Serializable()]
    public class MarkerDefinition : Definitions
    {
        [XmlAttribute(attributeName: "type")]
        public float StyleType { get; set; }

        [XmlAttribute(attributeName: "markerWidth")]
        public float MarkerWidth { get; set; }

        [XmlAttribute(attributeName: "markerHeight")]
        public float MarkerHeight { get; set; }

        [XmlAttribute(attributeName: "orient")]
        public string Orient { get; set; }

        [XmlAttribute(attributeName: "viewBox")]
        public string viewBox { get; set; }

        [XmlIgnore()]
        public ViewBoxRectangle ViewBox
        {
            get
            {
                if (!string.IsNullOrEmpty(viewBox))
                {
                    var splitt = viewBox.Split(' ');
                    if (splitt.Length == 4)
                    {
                        var rectangle = new ViewBoxRectangle()
                        {
                            X = float.Parse(splitt[0]),
                            Y = float.Parse(splitt[1]),
                            Width = float.Parse(splitt[2]),
                            Height = float.Parse(splitt[3]),
                        };

                        return rectangle;
                    }
                }
                return null;
            }
        }

        [XmlElement("g", Type = typeof(GPath))]
        [XmlElement("path", Type = typeof(Path))]
        [XmlElement("circle", Type = typeof(CirclePath))]
        public HtmlElement[] Vectors { get; set; }
    }
}

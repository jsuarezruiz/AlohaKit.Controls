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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FigmaSharp.Views.Graphics
{
    [Serializable()]
    [XmlRoot("svg")]
    public class Svg
    {
        [XmlAttribute(attributeName: "id")]
        public string Id { get; set; }

        [XmlAttribute(attributeName: "width")]
        public float Width { get; set; }

        [XmlAttribute(attributeName: "height")]
        public float Height { get; set; }

        [XmlAttribute(attributeName: "version")]
        public float Version { get; set; }

        #region Titles

        [XmlElement(elementName: "title")]
        public string Title { get; set; }
        [XmlElement(elementName: "drawing-type")]
        public string DrawingType { get; set; }

        #endregion

        [XmlElement("mask")]
        public Mask[] Mask { get; set; }

        [XmlElement("svg")]
        public Svg[] Svgs { get; set; }

        [XmlElement("g", Type = typeof(GPath))]
        [XmlElement("path", Type = typeof(Path))]
        [XmlElement("circle", Type = typeof(CirclePath))]
        [XmlElement("rect", Type = typeof(RectanglePath))]
        [XmlElement("line", Type = typeof(LinePath))]
        [XmlElement("text", Type = typeof(TextPath))]
        public HtmlElement[] Vectors { get; set; }

        [XmlArray("defs")]
        [XmlArrayItem("style", typeof(StyleDefinition))]
        [XmlArrayItem("marker", typeof(MarkerDefinition))]
        public Definitions[] Definitions { get; set; }

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

        public static Svg FromData (string data)
        {
            data = data
                .Replace("xmlns=\"http://www.w3.org/2000/svg\"", "")
                .Replace("xmlns=\"http://www.w3.org/1999/xlink\"", "");

            XmlSerializer serializer = new XmlSerializer(typeof(Svg));

            Svg result;
            using (TextReader r = new StringReader(data))
                result = (Svg)serializer.Deserialize(r);
            return result;
            //Parse(xml.Root);
        }
    }
}
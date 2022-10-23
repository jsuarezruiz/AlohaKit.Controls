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
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace FigmaSharp
{
    public class FigmaManifest
	{
		[ManifestDescription ("File Id")]
		public string FileId { get; set; }

		[ManifestDescription("Namespace")]
		public string Namespace { get; set; }

		[ManifestDescription ("Document Title")]
		public string DocumentTitle { get; set; }

		[ManifestDescription ("Document Version")]
		public string DocumentVersion { get; set; }

		[ManifestDescription("Document Last Modified")]
		public string DocumentLastModified { get; set; }

		[ManifestDescription ("Conversion Date")]
		public DateTime Date { get; set; }

		[ManifestDescription ("Remote Api Version")]
		public string RemoteApiVersion { get; set; }

		[ManifestDescription ("FigmaSharp Api Version")]
		public string ApiVersion { get; set; }

		ManifestDescription GetManifestDescription (string propertyName)
		{
			var attribute = this.GetType ().GetProperty (propertyName).GetCustomAttributes (true).OfType<ManifestDescription> ().FirstOrDefault ();
			return attribute;
		}

		public void ToComment (StringBuilder builder)
		{
			string timestamp = Date.ToString("r");

			builder.AppendLine ($"// This file was auto-generated using");
			builder.AppendLine ($"// FigmaSharp {ApiVersion} and Figma API {RemoteApiVersion} on {timestamp}");
			builder.AppendLine ($"//");
			builder.AppendLine ($"// Document title:   {DocumentTitle}");
			builder.AppendLine ($"// Document version: {DocumentVersion}");
			builder.AppendLine ($"// Document URL:     https://figma.com/file/{FileId}");
			builder.AppendLine ($"// Namespace:        {Namespace}");
			builder.AppendLine ($"//");
			builder.AppendLine ($"// Changes to this file may cause incorrect behavior");
			builder.AppendLine ($"// and will be lost if the code is regenerated.");
		}

		public static FigmaManifest FromFilePath (string filePath)
		{
			return JsonConvert.DeserializeObject<FigmaManifest> (File.ReadAllText (filePath));
		}

		public void Save (string filePath)
		{
			File.WriteAllText (filePath, JsonConvert.SerializeObject (this, Formatting.Indented));
		}
	}
}

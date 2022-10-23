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
using System.Reflection;

namespace FigmaSharp.Views.Helpers
{
	public static class FileHelper
	{
		public static string GetFileDataFromBundle (string fileName, Assembly assembly = null)
		{
			if (assembly == null)
				assembly = Assembly.GetEntryAssembly();

			var path = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(assembly.Location));
			path = System.IO.Path.Combine(path, "Resources", "svg", fileName);
			var data = System.IO.File.ReadAllText(path);
			return data;
		}

        public static string GetManifestResource(Assembly assembly, string resource)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly();
            }
            try
            {
                var resources = assembly.GetManifestResourceNames();
                var fullResourceName = string.Format("{0}.{1}", assembly.GetName().Name, resource);

                foreach (var item in new string[] { resource,  fullResourceName })
                {
                    //TODO: not safe
                    using (var stream = assembly.GetManifestResourceStream(item))
                    {
                        if (stream == null)
                            continue;
                        using (TextReader tr = new StreamReader(stream))
                            return tr.ReadToEnd();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Cannot read resource '{0}' in assembly '{1}'", resource, ex);
                return null;
            }

            Console.WriteLine("Resource '{0}' not found in assembly '{1}'", resource, assembly.FullName); ;
            return null;
        }
    }
}

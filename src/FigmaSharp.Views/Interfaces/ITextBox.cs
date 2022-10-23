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

namespace FigmaSharp.Views
{
	public enum TextAlignment
	{
		/// <summary>Indicates that text will be aligned to the left or top of horizontally or vertically aligned text, respectively.</summary>
		Start,
		/// <summary>Indicates that text will be aligned in the middle of either horizontally or vertically aligned text.</summary>
		Center,
		/// <summary>Indicates that text will be aligned to the right or bottom of horizontally or vertically aligned text, respectively.</summary>
		End
	}

	public interface ITextBox : IView
	{
		string Text { get; set; }
		string PlaceHolderString { get; set; }

		Color ForegroundColor { get; set; }
	}

    public interface ITextView : IView
    {
        //string Text { get; set; }
        //Color ForegroundColor { get; set; }
    }
}

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

namespace FigmaSharp.Views
{
	public interface IView : IObject
	{
		float Width { get; set; }
		float Height { get; set; }

		bool Hidden { get; set; }

		string Identifier { get; set; }
		string NodeName { get; set; }

		float CornerRadius { get; set; }

		bool MovableByWindowBackground { get; set; }

		Size Size { get; set; }
		Rectangle Allocation { get; }
		Size IntrinsicContentSize { get; }

		IView Parent { get; set; }
		Color BackgroundColor { get; set; }
		AnchorStyles Anchor { get; set; }

		IReadOnlyList<IView> Children { get; }

		void Focus ();
		void SetPosition (float x, float y);
		void SetAllocation (float x, float y, float width, float height);
		void SetAlignmentRect (float x, float y, float width, float height);

		void SetPosition (Point origin);
		void SetAllocation (Point origin, Size size);

		void AddChild (IView view);
		void RemoveChild (IView view);
		void ClearSubviews ();

		void OnChangeFrameSize (Size newSize);
	}
}

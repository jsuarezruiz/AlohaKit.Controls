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

namespace FigmaSharp
{
	public class Point : IEquatable<Point>
	{
		public Point () { }
		public Point (float x, float y)
		{
			X = x;
			Y = y;
		}

		public float X { get; set; }
		public float Y { get; set; }

		public bool Equals (Point other)
		{
			if (X != other.X || Y != other.Y) {
				return false;
			}
			return true;
		}

		public override bool Equals (object obj)
		{
			if (obj == null) return false;
			if (!(obj is Size)) return false;
			return Equals ((Size)obj);
		}

		public override string ToString ()
		{
			return string.Format ("{{{0},{1}}}", X, Y);
		}

		public static Point Zero { get; set; } = new Point ();

        public Point Substract(Point center)
        {
			return new Point(X - center.X, Y - center.Y);
        }

		public Point UnionWith (Point center)
		{
			return new Point(X + center.X, Y + center.Y);
		}
	}

	public class Size : IEquatable<Size>
	{
		public Size () { }
		public Size (float width, float height)
		{
			Width = width;
			Height = height;
		}

		public float Width { get; set; }
		public float Height { get; set; }

		public bool Equals (Size other)
		{
			if (Width != other.Width || Height != other.Height) {
				return false;
			}
			return true;
		}

		public override bool Equals (object obj)
		{
			if (obj == null) return false;
			if (!(obj is Size)) return false;
			return Equals ((Size)obj);
		}

		public override string ToString ()
		{
			return string.Format ("{{{0},{1}}}", Width, Height);
		}

		public static Size Zero { get; set; } = new Size ();
	}

	public class Rectangle : IEquatable<Rectangle>
	{

		readonly Size size = new Size ();
		public Size Size {
			get => size;
			set {
				size.Width = value.Width;
				size.Height = value.Height;
			}
		}

		readonly Point origin = new Point ();
		public Point Origin {
			get => origin;
			set {
				origin.X = value.X;
				origin.Y = value.Y;
			}
		}

		public float X {
			get => origin.X;
			set => origin.X = value;
		}

		public float Y {
			get => origin.Y;
			set => origin.Y = value;
		}

		public float Width {
			get => size.Width;
			set => size.Width = value;
		}

		public float Height {
			get => size.Height;
			set => size.Height = value;
		}

		public float Left => X;
		public float Right => X + Width;
		public float Top => Y;
		public float Bottom => Y + Height;

		public static Rectangle Zero { get; set; } = new Rectangle ();

		public Rectangle ()
		{

		}

		public Rectangle (Point origin, Size size)
		{
			this.origin = origin;
			this.size = size;
		}

		public Rectangle (float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		public Rectangle UnionWith (Rectangle allocation)
		{
			//TODO: improve
			float xMin = Math.Min (X, allocation.X);
			float yMin = Math.Min (Y, allocation.Y);
			float xMax = Math.Max (X + Width, allocation.X + allocation.Width);
			float yMax = Math.Max (Y + Height, allocation.Y + allocation.Height);
			return new Rectangle (xMin, yMin, xMax - xMin, yMax - yMin);
		}

		public bool Equals (Rectangle other)
		{
			if (size.Equals (other.Size) && origin.Equals (other.Origin)) {
				return true;
			}
			return false;
		}

		public override bool Equals (object obj)
		{
			if (obj == null) return false;
			if (!(obj is Rectangle)) return false;
			return Equals ((Rectangle)obj);
		}

		public bool IntersectsWith (Rectangle allocation)
		{
			return (Left < allocation.Right && Right > allocation.Left &&
	 Top < allocation.Bottom && Bottom > allocation.Top);
		}

		public bool IntersectsWith (Rectangle allocation, bool reversed)
		{
			return (Left < allocation.Right && Right > allocation.Left &&
	 Top <= allocation.Bottom && Bottom >= allocation.Top);
		}

		public override string ToString ()
		{
			return string.Format ("{{{0},{1},{2},{3}}}", X, Y, Width, Height);
		}

		public Point Center => new Point (Width / 2f, Height / 2f);

		public Rectangle Copy() => new Rectangle(X, Y, Width, Height);
	}
}

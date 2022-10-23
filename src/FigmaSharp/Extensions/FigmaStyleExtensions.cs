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

using FigmaSharp.Models; 

namespace FigmaSharp.Extensions
{
    public static class FigmaStyleExtensions
    {
        public static void FillEmptyStylePropertiesWithDefaults(this FigmaTypeStyle style, FigmaText text)
        {
            if (style.fills == default)
                style.fills = text.fills;
            if (style.fontFamily == default)
                style.fontFamily = text.style.fontFamily;
            if (style.fontPostScriptName == default)
                style.fontPostScriptName = text.style.fontPostScriptName;
            if (style.fontSize == default)
                style.fontSize = text.style.fontSize;
            if (style.fontWeight == default)
                style.fontWeight = text.style.fontWeight;
            if (style.letterSpacing == default)
                style.letterSpacing = text.style.letterSpacing;
            if (style.lineHeightPercent == default)
                style.lineHeightPercent = text.style.lineHeightPercent;
            if (style.lineHeightPx == default)
                style.lineHeightPx = text.style.lineHeightPx;
            if (style.textAlignHorizontal == default)
                style.textAlignHorizontal = text.style.textAlignHorizontal;
            if (style.textAlignVertical == default)
                style.textAlignVertical = text.style.textAlignVertical;
        }
    }
}

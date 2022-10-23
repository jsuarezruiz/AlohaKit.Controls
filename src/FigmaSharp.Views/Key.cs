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
	[Flags]
	public enum Key : ulong
	{
		A = 0uL,
		S = 1uL,
		D = 2uL,
		F = 3uL,
		H = 4uL,
		G = 5uL,
		Z = 6uL,
		X = 7uL,
		C = 8uL,
		V = 9uL,
		B = 11uL,
		Q = 12uL,
		W = 13uL,
		E = 14uL,
		R = 0xF,
		Y = 0x10,
		T = 17uL,
		D1 = 18uL,
		D2 = 19uL,
		D3 = 20uL,
		D4 = 21uL,
		D6 = 22uL,
		D5 = 23uL,
		Equal = 24uL,
		D9 = 25uL,
		D7 = 26uL,
		Minus = 27uL,
		D8 = 28uL,
		D0 = 29uL,
		RightBracket = 30uL,
		O = 0x1F,
		U = 0x20,
		LeftBracket = 33uL,
		I = 34uL,
		P = 35uL,
		L = 37uL,
		J = 38uL,
		Quote = 39uL,
		K = 40uL,
		Semicolon = 41uL,
		Backslash = 42uL,
		Comma = 43uL,
		Slash = 44uL,
		N = 45uL,
		M = 46uL,
		Period = 47uL,
		Grave = 50uL,
		KeypadDecimal = 65uL,
		KeypadMultiply = 67uL,
		KeypadPlus = 69uL,
		KeypadClear = 71uL,
		KeypadDivide = 75uL,
		KeypadEnter = 76uL,
		KeypadMinus = 78uL,
		KeypadEquals = 81uL,
		Keypad0 = 82uL,
		Keypad1 = 83uL,
		Keypad2 = 84uL,
		Keypad3 = 85uL,
		Keypad4 = 86uL,
		Keypad5 = 87uL,
		Keypad6 = 88uL,
		Keypad7 = 89uL,
		Keypad8 = 91uL,
		Keypad9 = 92uL,
		Return = 36uL,
		Tab = 48uL,
		Space = 49uL,
		Delete = 51uL,
		Escape = 53uL,
		Command = 55uL,
		Shift = 56uL,
		CapsLock = 57uL,
		Option = 58uL,
		Control = 59uL,
		RightShift = 60uL,
		RightOption = 61uL,
		RightControl = 62uL,
		Function = 0x3F,
		VolumeUp = 72uL,
		VolumeDown = 73uL,
		Mute = 74uL,
		ForwardDelete = 117uL,
		ISOSection = 10uL,
		JISYen = 93uL,
		JISUnderscore = 94uL,
		JISKeypadComma = 95uL,
		JISEisu = 102uL,
		JISKana = 104uL,
		F18 = 79uL,
		F19 = 80uL,
		F20 = 90uL,
		F5 = 96uL,
		F6 = 97uL,
		F7 = 98uL,
		F3 = 99uL,
		F8 = 100uL,
		F9 = 101uL,
		F11 = 103uL,
		F13 = 105uL,
		F16 = 106uL,
		F14 = 107uL,
		F10 = 109uL,
		F12 = 111uL,
		F15 = 113uL,
		Help = 114uL,
		Home = 115uL,
		PageUp = 116uL,
		F4 = 118uL,
		End = 119uL,
		F2 = 120uL,
		PageDown = 121uL,
		F1 = 122uL,
		LeftArrow = 123uL,
		RightArrow = 124uL,
		DownArrow = 125uL,
		UpArrow = 126uL
	}
}


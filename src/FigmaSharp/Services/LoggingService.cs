// Authors:
//   Matt Ward <matt.ward@microsoft.com>
//
// Copyright (C) 2021 Microsoft, Corp
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
using System.Text;

namespace FigmaSharp.Services
{
    public enum LogLevel
    {
        Fatal = 1,
        Error = 2,
        Warn = 4,
        Info = 8,
        Debug = 16,
    }

    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }

    public static class LoggingService
    {
        static ILogger logger = new DefaultLogger();

        public static void RegisterDefaultLogger(ILogger logger)
        {
            LoggingService.logger = logger;
        }

        public static void LogInfo(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        public static void LogInfo(string format, object arg0)
        {
            logger.Log(LogLevel.Info, string.Format(format, arg0));
        }

        public static void LogInfo(string format, object arg0, object arg1)
        {
            logger.Log(LogLevel.Info, string.Format(format, arg0, arg1));
        }

        public static void LogInfo(string format, object arg0, object arg1, object arg2)
        {
            logger.Log(LogLevel.Info, string.Format(format, arg0, arg1, arg2));
        }

        public static void LogInfo(string format, params object[] args)
        {
            logger.Log(LogLevel.Info, string.Format(format, args));
        }

        public static void LogError(string message)
        {
            logger.Log(LogLevel.Error, message);
        }

        public static void LogError(string format, object arg0)
        {
            logger.Log(LogLevel.Error, string.Format(format, arg0));
        }

        public static void LogError(string message, Exception ex)
        {
            if (ex == null)
            {
                logger.Log(LogLevel.Error, message);
            }
            else
            {
                var exceptionText = new StringBuilder();
                exceptionText.AppendLine(message);
                exceptionText.Append(ex);

                logger.Log(LogLevel.Error, exceptionText.ToString());
            }
        }

        public static void LogWarning(string message)
        {
            logger.Log(LogLevel.Warn, message);
        }

        public static void LogWarning(string format, object arg0)
        {
            logger.Log(LogLevel.Warn, string.Format(format, arg0));
        }

        public static void LogWarning(string format, object arg0, object arg1)
        {
            logger.Log(LogLevel.Warn, string.Format(format, arg0, arg1));
        }

        class DefaultLogger : ILogger
        {
            public void Log(LogLevel level, string message)
            {
                switch (level)
                {
                    case LogLevel.Info:
                        Console.WriteLine(message);
                        break;
                    default:
                        Console.WriteLine("{0}: {1}", level.ToString().ToUpper(), message);
                        break;
                }
            }
        }
    }
}

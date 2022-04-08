using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace FunWithLogger.Services
{
    public class FileLogger : ILogger
    {
        class NullDispasable : IDisposable
        {
            public void Dispose()
            {
            }
        }

        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new NullDispasable();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var exc = "";
            if (exception is not null)
            {
                exc = Environment.NewLine +
                    exception.GetType() + ": " + exception.Message + Environment.NewLine +
                    exception.StackTrace + Environment.NewLine;
            }

            File.AppendAllText(_filePath, logLevel.ToString() + ": " + DateTime.Now.ToString() + " " + formatter(state, exception) + Environment.NewLine + exc);
        }
    }
}

using System;
using System.IO;
using Serilog.Core;
using Serilog.Events;

namespace FunWithSerilog.Services
{
    public class FileSink : ILogEventSink
    {
        private readonly string _filePath;

        public FileSink(string filePath)
        {
            _filePath = filePath;
        }

        public void Emit(LogEvent logEvent)
        {
            File.AppendAllText(_filePath, $"[{logEvent.Level}] [{logEvent.Timestamp}] -- {logEvent.RenderMessage()} {Environment.NewLine}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Serilog;
using Serilog.Events;

namespace FunWithSerilog.Services
{
    [InterpolatedStringHandler]
    public ref struct StructuredLoggingInterpolatedStringHandler
    {
        private readonly StringBuilder _builder = null!;
        private readonly List<object?> _arguments = null!;

        public bool IsEnabled { get; }

        public StructuredLoggingInterpolatedStringHandler(
            int literalLength,
            int formattedCount,
            ILogger logger,
            LogEventLevel logLevel,
            out bool isEnabled)
        {
            IsEnabled = isEnabled = logger.IsEnabled(logLevel);
            if (isEnabled)
            {
                _builder = new StringBuilder(literalLength);
                _arguments = new List<object?>(formattedCount);
            }
        }

        public void AppendLiteral(string s)
        {
            if (!IsEnabled)
            {
                return;
            }

            _builder.Append(s.Replace("{", "{{", StringComparison.Ordinal).Replace("}", "}}", StringComparison.Ordinal));
        }

        public void AppendFormatted<T>(T value, [CallerArgumentExpression("value")] string name = "")
            where T : IFormattable
        {
            if (!IsEnabled)
            {
                return;
            }

            _arguments.Add(value);
            _builder.Append($"{{@{name}}}");
        }

        public (string, object?[]) GetTemplateAndArguments() => (_builder.ToString(), _arguments.ToArray());
    }
}

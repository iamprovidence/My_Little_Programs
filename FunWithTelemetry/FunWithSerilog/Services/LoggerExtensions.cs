using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Events;

namespace FunWithSerilog.Services
{
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, LogEventLevel logEventLevel,
            [InterpolatedStringHandlerArgument("logger", "logEventLevel")] ref StructuredLoggingInterpolatedStringHandler handler)
        {
            if (handler.IsEnabled)
            {
                var (template, arguments) = handler.GetTemplateAndArguments();
                logger.Write(logEventLevel, template, arguments);
            }
        }
    }
}

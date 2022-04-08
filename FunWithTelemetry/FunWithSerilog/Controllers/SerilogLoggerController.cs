using System;
using FunWithSerilog.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;

namespace FunWithSerilog.Controllers
{
    [ApiController]
    [Route("serilog-logger")]
    public class SerilogLoggerController
    {
        private readonly ILogger _logger;

        public SerilogLoggerController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            _logger.Verbose("Hello world");
            _logger.Debug("Hello world");
            _logger.Information("Hello world");
            _logger.Warning("Hello world");
            _logger.Error("Hello world");
            _logger.Fatal("Hello world");

            _logger.Write(LogEventLevel.Information, "Message {id} - {test} - {id}", 1); // 1 {test} 1
            _logger.Write(LogEventLevel.Information, "Message {id} - {test} - {id}", 1, 2); // 1 2 1
            Log.Information("Message {id} - {test} - {id}", 1, 2, 3); // 3 2 3
            Log.Information("Message {0} - {1} - {0}", 1, 2, 3); // 1 2 1

            // different color, parameters are treated as object depending on sink
            // optimized memory allocation, since format is performed when LogLevel enabled
            _logger.Information($"Hello world {1 + 1}");
            _logger.Information("Hello world {0}", 1 + 1);
            _logger.Log(LogEventLevel.Information, $"Hello world {2}");
            _logger.InterpolatedInformation($"Hello world {1 + 1}");

            var arr = new[] { 1, 2, 3 };
            Log.Information("Message {Data}", arr); // [1, 2, 3]
            Log.Information("Message {$Data}", arr); // "System.Int32[]"

            // Standard ILogger interface
            var logger = Microsoft.Extensions.Logging.LoggerFactory.Create(config =>
            {
                config.AddSerilog(dispose: true);
            }).CreateLogger(categoryName: "Application");

            Microsoft.Extensions.Logging.LoggerExtensions.LogInformation(logger, "Message {id} - {test} - {id}", 1, 2, 3); // 3 2 3

            try
            {
                throw new Exception("Hello exception");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred");
            }

            throw new Exception("Hello exception");
        }
    }
}

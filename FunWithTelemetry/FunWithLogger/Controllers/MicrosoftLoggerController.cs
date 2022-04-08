using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FunWithLogger.Controllers
{
    [ApiController]
    [Route("microsoft-logger")]
    public class MicrosoftLoggerController : ControllerBase
    {
        private readonly ILogger<MicrosoftLoggerController> _logger;

        public MicrosoftLoggerController(ILogger<MicrosoftLoggerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            _logger.LogTrace("[Trace] Hello world");
            _logger.LogDebug("[Debug] Hello world");
            _logger.LogInformation("[Info] Hello world");
            _logger.LogWarning("[Warning] Hello world");
            _logger.LogError("[Error] Hello world");
            _logger.LogCritical("[Critical] Hello world");

            _logger.LogInformation(eventId: 1, "[Info] Hello world");
            _logger.LogInformation(eventId: new EventId(1, "Test"), "[Info] Hello world");

            using (_logger.BeginScope("User {User} logged in from {Address}", "Test", HttpContext.Connection.RemoteIpAddress))
            {
                // Error:_logger.Log(LogLevel.Information, "Message {id} - {test} - {id}", 1);
                // Error: _logger.Log(LogLevel.Information, "Message {id} - {test} - {id}", 1, 2);
                _logger.Log(LogLevel.Information, "Message {id} - {test} - {id}", 1, 2, 3); // 1 2 1
                _logger.Log(LogLevel.Information, "Message {0} {1} {0}", 1, 2, 3); // 1 2 1
            }

            try
            {
                throw new Exception("Hello exception");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred");
            }

            throw new Exception("Hello exception");
        }
    }
}

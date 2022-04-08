using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FunWithLogger.Controllers
{
    [ApiController]
    [Route("console-logger")]
    public class ConsoleLoggerController
    {
        [HttpGet]
        public void Get()
        {
            Trace.WriteLine("[Trace] Hello world");
            Debug.WriteLine("[Debug] Hello world");
            Console.WriteLine("[Info] Hello world");
            Console.Error.WriteLine("[Error] Hello world");
        }
    }
}

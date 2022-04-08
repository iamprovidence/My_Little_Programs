using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FunWithLogger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // Not handled by ASP, so can be caught by AppDomain
            // throw new Exception("Hello exception");

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) =>
                {
                    builder
                        .AddExceptionDemystifyer();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var logger = LoggerFactory.Create(config =>
            {
                config.AddConsole(opts =>
                {
                    opts.LogToStandardErrorThreshold = LogLevel.Trace;
                });
            }).CreateLogger(categoryName: "Application");

            logger.LogError((Exception)e.ExceptionObject, "Unhandled exception caught. Runtime is terminating : {IsTerminating}", e.IsTerminating);
        }
    }
}

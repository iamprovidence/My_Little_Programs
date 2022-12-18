using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;

namespace FunWithSerilog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .Enrich.WithDemystifiedStackTraces()
                        .WriteTo.Sink(new Services.FileSink("mylogs.txt"))
                        .WriteTo.File(new CompactJsonFormatter(), "cleflogs.clef")
                        .WriteTo.File(new JsonFormatter(), "jsonlogs.json")
                        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200/"))
                        {
                            IndexFormat = $"{hostingContext.HostingEnvironment.ApplicationName}~logs~{hostingContext.HostingEnvironment.EnvironmentName}~{DateTimeOffset.UtcNow:yyyy-MM}",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1,
                        })
                        .WriteTo.Seq("http://localhost:5341")
                        .ReadFrom.Configuration(hostingContext.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

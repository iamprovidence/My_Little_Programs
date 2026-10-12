using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Endpoints.gRPC.Ui
{
    public static class GrpcUiEndpointRouteBuilderExtensions
    {
        // browsers can't make gRPC calls, so instead of a "try it" page this one explains which clients to use
        public static IEndpointConventionBuilder MapGrpcUi(this IEndpointRouteBuilder endpoints, string pattern)
        {
            var configuration = endpoints.ServiceProvider.GetRequiredService<IConfiguration>();

            // gRPC works only on the HTTP/2 endpoint
            var address = new Uri(configuration["Kestrel:Endpoints:Http:Url"]).Authority;

            var page = ReadResource("grpc-ui.html")
                .Replace("{{ADDRESS}}", address)
                .Replace("{{PROTO}}", WebUtility.HtmlEncode(ReadResource("Platforms.proto")));

            return endpoints.MapGet(pattern, context =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                return context.Response.WriteAsync(page, context.RequestAborted);
            });
        }

        private static string ReadResource(string name)
        {
            using var stream = typeof(GrpcUiEndpointRouteBuilderExtensions).Assembly.GetManifestResourceStream(name)!;
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}

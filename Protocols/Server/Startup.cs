using System.Diagnostics;
using System.Linq;
using Autofac;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using Server.Application;
using Server.Endpoints.GraphQL;
using Server.Endpoints.GraphQL.Infrastructure;
using Server.Endpoints.gRPC;
using Server.Endpoints.gRPC.Infrastructure;
using Server.Endpoints.gRPC.Ui;
using Server.Endpoints.MCP.Prompts;
using Server.Endpoints.MCP.Resources;
using Server.Endpoints.MCP.Tools;
using Server.Endpoints.MCP.Ui;
using Server.Infrastructure;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers();

            services
                .AddSwaggerGen();

            services
                .AddHostedServices();

            services
                .AddGrpc();

            services
                .AddGrpcReflection();

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                // .AddType<PlatformType>() custom projection
                .AddProjections() // to pull child objects
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions();

            services
                .AddMcpServer()
                .WithHttpTransport()
                .WithTools<PlatformTools>()
                .WithPrompts<PlatformPrompts>()
                .WithResources<PlatformResources>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<GraphQlModule>();
            builder.RegisterModule<GrpcModule>();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment environment,
            IHostApplicationLifetime lifetime,
            IConfiguration configuration)
        {
            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger("/openapi/{documentName}.json"); // the route Scalar reads the document from
                endpoints.MapScalarApiReference();
                endpoints.MapGraphQL();
                endpoints.MapGrpcService<PlatformGrpcService>();
                endpoints.MapGrpcUi("/ui/grpc");
                endpoints.MapMcp("/mcp");
                endpoints.MapMcpUi("/ui/mcp");

                if (environment.IsDevelopment())
                {
                    // lets Postman / grpcui / grpcurl discover services without the .proto file
                    endpoints.MapGrpcReflectionService();
                }
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
            }, path: "/ui/vayager");

            if (environment.IsDevelopment())
            {
                // browsers speak HTTP/1.1 only over plain http, so open the pages on the Http1 endpoint
                var baseUrl = configuration["Kestrel:Endpoints:Http1:Url"];

                lifetime.ApplicationStarted.Register(() =>
                {
                    foreach (var path in new[] { "/ui/vayager", "/graphql/", "/scalar", "/ui/mcp", "/ui/grpc" }) // Voyager, Banana Cake Pop, Scalar, MCP Explorer, gRPC guide
                    {
                        Process.Start(new ProcessStartInfo($"{baseUrl}{path}")
                        {
                            UseShellExecute = true,
                        });
                    }
                });
            }
        }
    }

    public static class ServicesExtensions
    {
        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            var addHostedServiceMathod = typeof(ServiceCollectionHostedServiceExtensions)
                .GetMethod(nameof(ServiceCollectionHostedServiceExtensions.AddHostedService), new[] { typeof(IServiceCollection) })!;

            foreach (var hostedServiceType in typeof(Program).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(IHostedService))))
            {
                addHostedServiceMathod.MakeGenericMethod(hostedServiceType).Invoke(null, new[] { services });
            }

            return services;
        }
    }
}

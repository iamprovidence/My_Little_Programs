using System.Linq;
using Autofac;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Application;
using Server.Endpoints.GraphQL;
using Server.Endpoints.GraphQL.Infrastructure;
using Server.Endpoints.gRPC;
using Server.Endpoints.gRPC.Infrastructure;
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
                .AddHostedServices();

            services
                .AddGrpc();

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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<GraphQlModule>();
            builder.RegisterModule<GrpcModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
                endpoints.MapGrpcService<PlatformGrpcService>();
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
            }, path: "/ui/vayager");
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

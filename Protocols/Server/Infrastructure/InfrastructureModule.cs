using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Common;

namespace Server.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<JobService>()
                .As<IJobService>()
                .SingleInstance();

            var serviceCollection = new ServiceCollection();
            serviceCollection
                .AddDbContextPool<AppDbContext>(ConfigureOptions)
                .AddPooledDbContextFactory<AppDbContext>(ConfigureOptions);
            builder
                .Populate(serviceCollection);

            /*
            builder
                .Register(componentContext =>
                {
                    var serviceProvider = componentContext.Resolve<IServiceProvider>();

                    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                        .UseApplicationServiceProvider(serviceProvider);
                    ConfigureOptions(optionsBuilder);

                    return optionsBuilder.Options;
                })
                .As<DbContextOptions<AppDbContext>>()
                .InstancePerLifetimeScope();

            builder
                .Register(context => context.Resolve<DbContextOptions<AppDbContext>>())
                .As<DbContextOptions>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<AppDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
            */
        }

        private void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options
                .UseInMemoryDatabase(databaseName: "in-mem-db");
        }
    }
}

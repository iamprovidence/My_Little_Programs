using Autofac;
using Server.Application.Common;

namespace Server.Endpoints.GraphQL.Infrastructure
{
    public class GraphQlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GraphQlEventSender>()
                .As<IEventSender>()
                .InstancePerLifetimeScope();
        }
    }
}

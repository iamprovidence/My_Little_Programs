using Autofac;
using Server.Application.Common;

namespace Server.Endpoints.gRPC.Infrastructure
{
    public class GrpcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GrpcEventSender>()
                .As<IEventSender>()
                .As<IGrpcEventQueue>()
                .SingleInstance();
        }
    }
}

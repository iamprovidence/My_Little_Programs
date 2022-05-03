﻿using Autofac;

namespace Diver.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("AppService"))
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}

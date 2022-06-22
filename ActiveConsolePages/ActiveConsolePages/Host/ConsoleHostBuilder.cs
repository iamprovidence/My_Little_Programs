using System;
using System.Linq;
using System.Reflection;
using ActiveConsolePages.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace ActiveConsolePages.Host
{
    public interface IConsoleHostConfigurationBuilder
    {
        IConsoleHostServiceBuilder ConfigureHost(Action<HostConfiguration> configureDelegate);
    }

    public interface IConsoleHostServiceBuilder
    {
        IConsoleHostEntryPointBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);
    }

    public interface IConsoleHostEntryPointBuilder
    {
        IConsoleHostBuilder SetEntryPoint<TPage>()
            where TPage : PageBase;
    }

    public interface IConsoleHostBuilder
    {
        IConsoleHost Build();
    }

    internal class ConsoleHostBuilder : IConsoleHostConfigurationBuilder, IConsoleHostServiceBuilder, IConsoleHostEntryPointBuilder, IConsoleHostBuilder
    {
        private readonly IServiceCollection _services;
        private readonly HostConfiguration _hostConfiguration;
        private Type _entryPointType;

        internal ConsoleHostBuilder()
        {
            _services = new ServiceCollection();
            _hostConfiguration = new HostConfiguration();
            _entryPointType = null;
        }

        public IConsoleHostServiceBuilder ConfigureHost(Action<HostConfiguration> configureDelegate)
        {
            configureDelegate(_hostConfiguration);

            return this;
        }

        public IConsoleHostEntryPointBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
        {
            AddPages(_services);

            configureDelegate(_services);

            return this;

            void AddPages(IServiceCollection services)
            {
                var pageTypes = Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsAssignableTo(typeof(PageBase))).ToList();

                foreach (var pageType in pageTypes)
                {
                    services.AddTransient(pageType);
                }
            }
        }

        public IConsoleHostBuilder SetEntryPoint<TPage>()
            where TPage : PageBase
        {
            _entryPointType = typeof(TPage);

            return this;
        }
        public IConsoleHost Build()
        {
            var serviceProvider = _services.BuildServiceProvider();
            var entryPoint = serviceProvider.GetRequiredService(_entryPointType) as PageBase;

            var pageContext = new PageContext
            {
                ServiceProvider = serviceProvider,
                EnableBreadcrumbs = _hostConfiguration.EnableBreadcrumbs,
            };
            pageContext.History.Push(entryPoint);

            entryPoint.Initialize(pageContext);

            return new ConsoleHost(pageContext);
        }
    }
}

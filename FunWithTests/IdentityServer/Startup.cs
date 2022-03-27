using System.Linq;
using IdentityServer.Configurations;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews();

            services
                .AddTransient<IRedirectUriValidator, AcceptAllRedirectUrlValidator>()
                .AddIdentityServer()
                .AddInMemoryClients(IdentityServerConfigurations.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfigurations.GetIdentityResources())
                .AddInMemoryApiResources(IdentityServerConfigurations.GetApiResources())
                .AddInMemoryApiScopes(IdentityServerConfigurations.GetApiScopes())
                .AddTestUsers(IdentityServerConfigurations.GetUsers().ToList())
                .AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

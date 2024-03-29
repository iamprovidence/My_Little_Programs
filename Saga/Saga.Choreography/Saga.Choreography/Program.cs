using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Saga.Choreography
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.UseDefaultServiceProvider((context, options) =>
				{
					options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
					options.ValidateOnBuild = true;
				});
	}
}

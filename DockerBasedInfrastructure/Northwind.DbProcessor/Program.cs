using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Northwind.DbProcessor.Configuration;
using Northwind.DbProcessor.Migration;
using System.IO;
using System.Threading.Tasks;

namespace Northwind.DbProcessor
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var configuration = BuildConfiguration(args);
			var settings = BuildDbMigrationSettings(configuration);
			var loggerFactory = BuildLoggerFactory();

			loggerFactory.CreateLogger<Program>().LogWarning($"Runs: {settings.RunModes}");

			await MigrationFacadeService.Create(settings, loggerFactory).Run();
		}

		private static IConfiguration BuildConfiguration(string[] args)
		{
			return new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true)
				.AddEnvironmentVariables()
				.AddCommandLine(args)
				.Build();
		}

		private static Settings BuildDbMigrationSettings(IConfiguration configuration)
		{
			return new Settings
			{
				RunModes = configuration.GetValue("RunModes", RunModes.ListChanges),

				NorthwindDbConnection = configuration.GetConnectionString("NorthwindDbConnection"),
			};
		}

		private static ILoggerFactory BuildLoggerFactory()
		{
			return LoggerFactory.Create(config =>
			{
				config.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
				config.AddConsole();
			});
		}
	}
}

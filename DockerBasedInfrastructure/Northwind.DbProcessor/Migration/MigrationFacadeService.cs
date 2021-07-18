using Microsoft.Extensions.Logging;
using Northwind.DbProcessor.Configuration;
using Northwind.DbProcessor.Migration.Abstract;
using Northwind.DbProcessor.Migration.Strategies;
using System.Threading.Tasks;

namespace Northwind.DbProcessor.Migration
{
	internal class MigrationFacadeService
	{
		private readonly Settings _settings;
		private readonly ILoggerFactory _loggerFactory;

		public static MigrationFacadeService Create(Settings settings, ILoggerFactory loggerFactory)
		{
			return new MigrationFacadeService(settings, loggerFactory);
		}

		private MigrationFacadeService(Settings settings, ILoggerFactory loggerFactory)
		{
			_settings = settings;
			_loggerFactory = loggerFactory;
		}

		public async Task Run()
		{
			var migrationStrategies = new IDbMigrator[]
			{
				new NorthwindDbMigrator(_settings, _loggerFactory),
			};

			foreach (var migrator in migrationStrategies)
			{
				await migrator.Migrate();
			}
		}
	}
}

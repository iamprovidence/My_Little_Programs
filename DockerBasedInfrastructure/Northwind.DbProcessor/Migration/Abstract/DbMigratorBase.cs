using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.DbProcessor.Configuration;
using Northwind.DbProcessor.Utilities;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.DbProcessor.Migration.Abstract
{
	internal abstract class DbMigratorBase<T> : IDbMigrator
		where T : DbContext
	{
		protected readonly Settings _settings;
		protected readonly ILoggerFactory _loggerFactory;
		protected readonly ILogger<DbMigratorBase<T>> _logger;

		protected abstract string DbName { get; }
		protected abstract string DbConnectionString { get; }

		protected DbMigratorBase(Settings settings, ILoggerFactory loggerFactory)
		{
			_settings = settings;
			_loggerFactory = loggerFactory;
			_logger = loggerFactory.CreateLogger<DbMigratorBase<T>>();
		}

		public async Task Migrate()
		{
			_logger.LogInformation($"Starting {DbName} processing");
			_logger.LogInformation(DbConnectionString);

			using (var context = GetDbContext())
			{
				while (!context.Database.Exists())
				{
					_logger.LogInformation($"{DbName} database cannot be connected");
					await Task.Delay(1000);
				}

				await DoMigrate(context);
			}

			_logger.LogInformation($"Finishing {DbName} processing");
		}

		protected abstract T GetDbContext();

		public async Task DoMigrate(T context)
		{
			if (_settings.RunModes.HasFlag(RunModes.ListChanges))
			{
				await ListChanges(context);
			}

			if (_settings.RunModes.HasFlag(RunModes.ApplyMigration))
			{
				await Migrate(context);
			}
		}

		protected virtual async Task ListChanges(T context)
		{
			var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

			if (!pendingMigrations.Any())
			{
				_logger.LogInformation("No pending migrations");

				return;
			}

			_logger.LogInformation("Pending migrations:");

			foreach (var migration in pendingMigrations)
			{
				_logger.LogInformation(migration);
			}
		}

		protected virtual Task Migrate(T context)
		{
			return context.Database.MigrateAsync();
		}
	}
}

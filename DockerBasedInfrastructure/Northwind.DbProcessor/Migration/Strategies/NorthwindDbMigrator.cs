using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Northwind.DbProcessor.Configuration;
using Northwind.DbProcessor.Migration.Abstract;
using Northwind.Infrastructure.Persistence;

namespace Northwind.DbProcessor.Migration.Strategies
{
	internal class NorthwindDbMigrator : DbMigratorBase<NorthwindDbContext>
	{
		protected override string DbName => "NorthwindDb";
		protected override string DbConnectionString => _settings.NorthwindDbConnection;

		public NorthwindDbMigrator(Settings settings, ILoggerFactory loggerFactory)
			: base(settings, loggerFactory) { }

		protected override NorthwindDbContext GetDbContext()
		{
			var builder = new DbContextOptionsBuilder<NorthwindDbContext>();
			builder
				.UseLoggerFactory(_loggerFactory)
				.UseSqlServer(
					_settings.NorthwindDbConnection,
					b => b.MigrationsAssembly("Northwind"));

			return new NorthwindDbContext(builder.Options);
		}
	}
}

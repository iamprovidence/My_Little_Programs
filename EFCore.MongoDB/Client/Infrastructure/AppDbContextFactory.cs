using EFCore.MongoDB;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure
{
	internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var options = new MongoDbConfiguration
			{
				ConnectionString = "mongodb://localhost:27017",
				DatabaseName = "EfMongoDb",
			};

			var dbContext = new AppDbContext(Options.Create(options));

			return dbContext;
		}
	}
}

using MongoDB.Driver;
using SimpleMigrations;

namespace EFCore.MongoDB.Migrations
{
	internal class MongoDbDatabaseProvider : IDatabaseProvider<MongoDbConfiguration>
	{
		private readonly MongoDbConfiguration _mongoDbConfiguration;
		private readonly IMongoDatabase _database;

		public MongoDbDatabaseProvider(MongoDbConfiguration mongoDbConfiguration, IMongoDatabase database)
		{
			_mongoDbConfiguration = mongoDbConfiguration;
			_database = database;
		}

		public MongoDbConfiguration BeginOperation()
		{
			return _mongoDbConfiguration;
		}

		public void EndOperation()
		{

		}

		public long EnsurePrerequisitesCreatedAndGetCurrentVersion()
		{
			return GetCurrentVersion();
		}

		public long GetCurrentVersion()
		{
			var collection = GetMigrationHistoryCollection();

			return collection
				.Find(FilterDefinition<MigrationHistory>.Empty)
				.SortByDescending(x => x.Version)
				.Project(x => x.Version)
				.FirstOrDefault();
		}

		public void UpdateVersion(long oldVersion, long newVersion, string newDescription)
		{
			var collection = GetMigrationHistoryCollection();

			collection.InsertOne(new MigrationHistory
			{
				Version = newVersion,
				Description = newDescription,
			});
		}

		private IMongoCollection<MigrationHistory> GetMigrationHistoryCollection()
		{
			return _database.GetCollection<MigrationHistory>("_EFMigrationHistory");
		}
	}
}

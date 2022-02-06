using MongoDB.Driver;
using SimpleMigrations;

namespace EFCore.MongoDB.Migrations
{
	public abstract class MongoDbMigrationBase : IMigration<MongoDbConfiguration>
	{
		protected abstract void Up(IMongoDatabase database);
		protected abstract void Down(IMongoDatabase database);

		void IMigration<MongoDbConfiguration>.RunMigration(MigrationRunData<MongoDbConfiguration> data)
		{
			if (data.Direction == MigrationDirection.Up)
			{
				Up(data.Connection.Open());
			}
			else
			{
				Down(data.Connection.Open());
			}
		}
	}
}

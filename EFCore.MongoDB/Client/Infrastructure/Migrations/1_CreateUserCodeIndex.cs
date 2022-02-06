using Client.Domain;
using EFCore.MongoDB.Migrations;
using MongoDB.Driver;
using SimpleMigrations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Client.Infrastructure.Migrations
{
	[Migration(1, nameof(CreateUserCodeIndex))]
	internal class CreateUserCodeIndex : MongoDbMigrationBase
	{
		protected override void Up(IMongoDatabase database)
		{
			var collectionName = typeof(User).GetCustomAttribute<TableAttribute>().Name;

			var collection = database.GetCollection<User>(collectionName);

			var indexOptions = new CreateIndexOptions()
			{
				Name = "UserCodeIndex",
				Unique = false,
				Sparse = true,
			};
			var indexKeys = Builders<User>.IndexKeys.Ascending(x => x.Code);
			var indexModel = new CreateIndexModel<User>(indexKeys, indexOptions);

			collection.Indexes.CreateOne(indexModel);
		}

		protected override void Down(IMongoDatabase database)
		{
			var collectionName = typeof(User).GetCustomAttribute<TableAttribute>().Name;

			var collection = database.GetCollection<User>(collectionName);

			collection.Indexes.DropOne(name: "UserCodeIndex");
		}
	}
}

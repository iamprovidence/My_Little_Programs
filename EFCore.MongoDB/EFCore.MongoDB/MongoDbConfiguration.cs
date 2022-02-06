using MongoDB.Driver;

namespace EFCore.MongoDB
{
	public class MongoDbConfiguration
	{
		public string DatabaseName { get; set; }
		public string ConnectionString { get; set; }

		internal IMongoDatabase Open()
		{
			var mongoClient = new MongoClient(ConnectionString);
			
			return mongoClient.GetDatabase(DatabaseName);
		}
	}
}

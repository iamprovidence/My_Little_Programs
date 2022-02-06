using Client.Domain;
using Client.Infrastructure;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Client
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			var factory = new AppDbContextFactory();

			var dbContext = factory.CreateDbContext(args);

			dbContext.Migrate(typeof(AppDbContext).Assembly);

			dbContext.GetCollection<User>().InsertOne(new User()
			{
				Id = 1,
				Name = "John Doe",
				Code = Guid.Parse("6a07892f-ef03-4bf9-870e-1893d0f06950"),
			});

			foreach (var user in dbContext.GetCollection<User>().FindSync(FilterDefinition<User>.Empty).ToList())
			{
				Console.WriteLine($"{user.Id} {user.Name} {user.Code}");
			}
		}
	}
}

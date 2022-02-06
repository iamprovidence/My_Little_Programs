using EFCore.MongoDB;
using Microsoft.Extensions.Options;

namespace Client.Infrastructure
{
	internal class AppDbContext : MongoDbContext
	{
		public AppDbContext(IOptions<MongoDbConfiguration> options)
			: base(options) { }

		protected override void OnModelCreating()
		{
			ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}
	}
}

using WebApplication.Application.Persistence.Abstractions;

namespace WebApplication.IntegrationTests.PersistenceHelpers
{
	internal static class ApplicationDbContextExtensions
	{
		public static void MockDbSet<T>(this IApplicationDbContext dbContext, params T[] entities)
			where T : class
		{
			var dbSet = dbContext.GetDbSet<T>();

			dbSet.RemoveRange(dbSet);

			dbSet.AddRange(entities);

			dbContext.SaveChanges();
		}
	}
}

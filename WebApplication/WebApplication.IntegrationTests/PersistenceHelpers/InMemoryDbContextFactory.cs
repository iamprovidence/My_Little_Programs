using Microsoft.EntityFrameworkCore;
using System;
using WebApplication.Application.Persistence.Abstractions;
using WebApplication.Infrastructure.Persistence.MsSql.Services;

namespace WebApplication.IntegrationTests.PersistenceHelpers
{
	internal static class InMemoryDbContextFactory
	{
		public static IApplicationDbContext BuildDbContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
				.Options;

			var context = new ApplicationDbContext(options);
			context.Database.EnsureCreated();
			IdValueGeneratorHelper.SetIdValueGenerator(context);

			return context;
		}
	}
}

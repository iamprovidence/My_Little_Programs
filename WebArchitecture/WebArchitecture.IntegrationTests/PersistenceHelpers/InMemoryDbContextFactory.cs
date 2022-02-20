using Microsoft.EntityFrameworkCore;
using System;
using WebArchitecture.Application.Persistence.Abstractions;
using WebArchitecture.Infrastructure.Persistence.MsSql.Services;

namespace WebArchitecture.IntegrationTests.PersistenceHelpers
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

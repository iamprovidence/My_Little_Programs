using Microsoft.EntityFrameworkCore;
using WebApplication.Application.Persistence.Abstractions;
using WebApplication.Domain.Entities;

namespace WebApplication.Infrastructure.Persistence.MsSql.Services
{
	internal class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		public static readonly string DEFAULT_SCHEMA = "WebApplicationDbSchema";

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<T> GetDbSet<T>() where T : class
		{
			return Set<T>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

			modelBuilder
				.Entity<TodoItem>()
				.HasData(new TodoItem
				{
					Id = 1,
					Description = "Lorem ipsum",
				});
		}
	}
}

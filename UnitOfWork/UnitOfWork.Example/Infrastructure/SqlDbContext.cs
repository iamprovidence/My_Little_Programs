using Microsoft.EntityFrameworkCore;
using UnitOfWork.Example.Domains;

namespace UnitOfWork.Example.Infrastructure
{
	public class SqlDbContext : DbContext
	{
		public SqlDbContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder
				.Entity<User>()
				.HasKey(u => u.Id);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UOW;Trusted_Connection=True;");
		}
	}
}

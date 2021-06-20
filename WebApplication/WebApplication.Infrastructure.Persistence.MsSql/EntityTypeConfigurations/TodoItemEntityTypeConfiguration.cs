using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication.Domain.Entities;
using WebApplication.Infrastructure.Persistence.MsSql.Services;

namespace WebApplication.Infrastructure.Persistence.MsSql.EntityConfigurations
{
	internal sealed class TodoItemEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
	{
		public void Configure(EntityTypeBuilder<TodoItem> builder)
		{
			builder
				.ToTable("TodoItem", ApplicationDbContext.DEFAULT_SCHEMA);

			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Description)
				.HasMaxLength(250);
		}
	}
}

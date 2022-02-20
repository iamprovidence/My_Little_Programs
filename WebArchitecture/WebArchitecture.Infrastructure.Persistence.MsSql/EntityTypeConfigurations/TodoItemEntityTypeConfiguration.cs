using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebArchitecture.Domain.Entities;
using WebArchitecture.Infrastructure.Persistence.MsSql.Services;

namespace WebArchitecture.Infrastructure.Persistence.MsSql.EntityTypeConfigurations
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

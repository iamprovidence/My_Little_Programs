using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Entities;

namespace Northwind.Infrastructure.Persistence.Configurations
{
	internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder
				.ToTable("Categories", NorthwindDbContext.Schema);

			builder
				.Property(e => e.CategoryId)
				.HasColumnName("CategoryID");

			builder
				.Property(e => e.CategoryName)
				.IsRequired()
				.HasMaxLength(15);

			builder
				.Property(e => e.Description)
				.HasColumnType("ntext");

			builder
				.Property(e => e.Picture)
				.HasColumnType("image");
		}
	}
}

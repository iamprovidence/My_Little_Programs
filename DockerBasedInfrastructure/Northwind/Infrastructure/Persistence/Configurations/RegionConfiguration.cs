using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Entities;

namespace Northwind.Infrastructure.Persistence.Configurations
{
	internal sealed class RegionConfiguration : IEntityTypeConfiguration<Region>
	{
		public void Configure(EntityTypeBuilder<Region> builder)
		{
			builder
				.ToTable("Regions", NorthwindDbContext.Schema);

			builder
				.HasKey(e => e.RegionId)
				.IsClustered(false);

			builder
				.Property(e => e.RegionId)
				.HasColumnName("RegionID")
				.ValueGeneratedNever();

			builder
				.Property(e => e.RegionDescription)
				.IsRequired()
				.HasMaxLength(50);
		}
	}
}

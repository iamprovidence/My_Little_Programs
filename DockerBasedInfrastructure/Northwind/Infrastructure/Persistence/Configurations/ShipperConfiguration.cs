using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Entities;

namespace Northwind.Infrastructure.Persistence.Configurations
{
	internal sealed class ShipperConfiguration : IEntityTypeConfiguration<Shipper>
	{
		public void Configure(EntityTypeBuilder<Shipper> builder)
		{
			builder
				.ToTable("Shippers", NorthwindDbContext.Schema);

			builder
				.Property(e => e.ShipperId)
				.HasColumnName("ShipperID");

			builder
				.Property(e => e.CompanyName)
				.IsRequired()
				.HasMaxLength(40);

			builder
				.Property(e => e.Phone)
				.HasMaxLength(24);
		}
	}
}

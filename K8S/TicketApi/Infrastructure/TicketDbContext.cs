using Common.Structs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using TicketApi.Domain;

namespace TicketApi.Infrastructure
{
    public class TicketDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        public TicketDbContext(DbContextOptions<TicketDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Ticket>()
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<Ticket>()
                .Property(t => t.DisplayText);

            modelBuilder
                .Entity<Ticket>()
                .Property(t => t.FormattedDisplayText)
                .HasConversion(
                    domainValue => JsonConvert.SerializeObject(domainValue),
                    dbValue => JsonConvert.DeserializeObject<MultilingualString>(dbValue),
                    new ValueComparer<MultilingualString>((a, b) => false, (a) => a.GetHashCode()));

            modelBuilder
                .Entity<Ticket>()
                .Property(t => t.Type)
                .HasConversion(
                    domainValue => domainValue.Value,
                    dbValue => TicketType.FromValue(dbValue));

            modelBuilder
                .Entity<Ticket>()
                .Property(t => t.Code)
                .HasMaxLength(4);

            modelBuilder
                .Entity<Ticket>()
                .Property(t => t.Number);
        }
    }
}

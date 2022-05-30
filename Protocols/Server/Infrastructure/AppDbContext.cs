using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Server.Domain;

namespace Server.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Command> Commands { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Platform>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Platform>()
                .HasMany(p => p.Commands)
                .WithOne(c => c.Platform)
                .HasForeignKey(c => c.PlatformId);

            modelBuilder
                .Entity<Command>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<Platform>()
                .HasData(new Platform
                {
                    Id = 1,
                    Name = ".Net",
                });

            modelBuilder
                .Entity<Command>()
                .HasData(new List<Command>
                {
                    new Command
                    {
                        Id = 1,
                        CommandLine = "dotnet --help",
                        Help = "",
                        PlatformId = 1,
                    },
                    new Command
                    {
                        Id = 2,
                        CommandLine = "dotnet add",
                        Help = "Add a package or reference to a .NET project",
                        PlatformId = 1,
                    },
                });
        }
    }
}

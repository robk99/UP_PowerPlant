using Domain.PowerPlants;
using Domain.PowerProductions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PowerPlant> PowerPlants { get; set; }
        public DbSet<PowerProduction> PowerProductions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PowerPlant>()
                .OwnsOne(p => p.Location, loc =>
                {
                    loc.Property(l => l.Latitude)
                        .IsRequired()
                        .HasColumnType("double")
                        .HasColumnName("Latitude");

                    loc.Property(l => l.Longitude)
                        .IsRequired()
                        .HasColumnType("double")
                        .HasColumnName("Longitude");
                });

            modelBuilder.Entity<PowerProduction>()
                 .HasOne(p => p.PowerPlant)
                 .WithMany(pp => pp.PowerProductions)
                 .HasForeignKey(p => p.PowerPlantId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PowerProduction>()
                .HasIndex(p => new { p.PowerPlantId, p.Timestamp })
                .IsUnique();

            // TODO: Add all check constraints
        }

    }
}

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
                        .HasColumnType("DECIMAL(9,6)")
                        .HasColumnName("Latitude");

                    loc.Property(l => l.Longitude)
                        .IsRequired()
                        .HasColumnType("DECIMAL(9,6)")
                        .HasColumnName("Longitude");
                })
                .ToTable(t =>
                {
                    t.HasCheckConstraint("CK_PowerPlant_InstalledPower", "InstalledPower >= 0.1 AND InstalledPower <= 100");
                    t.HasCheckConstraint("CK_PowerPlant_Latitude", "Latitude >= -90 AND Latitude <= 90");
                    t.HasCheckConstraint("CK_PowerPlant_Longitude", "Longitude >= -180 AND Longitude <= 180");
                })
                .Property(p => p.InstallationDate)
                .HasColumnType("datetime2(3)");

            modelBuilder.Entity<PowerProduction>()
                 .HasOne(p => p.PowerPlant)
                 .WithMany(pp => pp.PowerProductions)
                 .HasForeignKey(p => p.PowerPlantId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PowerProduction>()
                .HasIndex(p => new { p.PowerPlantId, p.Timestamp })
                .IsUnique();
        }

    }
}

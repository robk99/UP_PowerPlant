using Domain.PowerPlants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.PowerPlants
{
    internal class PowerPlantModelConfiguration : IEntityTypeConfiguration<PowerPlant>
    {
        public void Configure(EntityTypeBuilder<PowerPlant> builder)
        {
            builder
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
        }
    }
}

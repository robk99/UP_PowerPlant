using Domain.PowerProductions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.PowerProductions
{
    internal class PowerProductionsModelConfiguration : IEntityTypeConfiguration<PowerProduction>
    {
        public void Configure(EntityTypeBuilder<PowerProduction> builder)
        {
            builder
                 .HasOne(p => p.PowerPlant)
                 .WithMany(pp => pp.PowerProductions)
                 .HasForeignKey(p => p.PowerPlantId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(p => new { p.PowerPlantId, p.Timestamp })
                .IsUnique();
        }
    }
}

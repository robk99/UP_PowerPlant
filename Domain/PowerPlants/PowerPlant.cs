using Domain.Locations;
using Domain.PowerProductions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.PowerPlants
{
    public class PowerPlant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public float InstalledPower { get; set; }
        [Required]
        public DateTime InstallationDate { get; set; }
        [Required]
        public Location Location { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<PowerProduction>  PowerProductions { get; set; } = new List<PowerProduction>();

        public PowerPlant(float installedPower, DateTime installationDate, Location location, string? name)
        {
            InstalledPower = installedPower;
            InstallationDate = installationDate;
            Location = location;
            Name = name;
        }

        // Parameterless constructor for EF Core
        private PowerPlant() { }
    }
}

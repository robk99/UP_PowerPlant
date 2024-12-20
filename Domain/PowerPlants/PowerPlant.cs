using Domain.Locations;
using Domain.PowerProductions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.PowerPlants
{
    public class PowerPlant(float installedPower, DateTime installationDate, Location location, string? name)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; } = name;
        [Required]
        [Range(0.1, 100, ErrorMessage = "Installed Power must be positive and less than 100.")]
        public required float InstalledPower { get; set; } = installedPower;
        [Required]
        public required DateTime InstallationDate { get; set; } = installationDate;
        [Required]
        public required Location Location { get; set; } = location;
        public required bool IsDeleted { get; set; } = false;
        public virtual ICollection<PowerProduction>  PowerProductions { get; set; } = new List<PowerProduction>();
    }
}


using Domain.PowerPlants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.PowerProductions
{
    public class PowerProduction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int PowerPlantId { get; set; }
        public PowerPlant? PowerPlant { get; set; }
        [Required]
        public float PowerProduced { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }

        public PowerProduction(float powerProduced, DateTime timestamp, PowerPlant powerPlant)
        {
            PowerProduced = powerProduced;
            Timestamp = timestamp;
            PowerPlant = powerPlant;
        }

        // Parameterless constructor for EF Core
        private PowerProduction() { }
    }
}

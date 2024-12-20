
using Domain.PowerPlants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.PowerProductions
{
    public class PowerProduction(float powerProduced, DateTime timestamp, PowerPlant powerPlant)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int PowerPlantId { get; set; }
        public PowerPlant? PowerPlant { get; set; } = powerPlant;
        [Required]
        public required float PowerProduced { get; set; } = powerProduced;
        [Required]
        public required DateTime Timestamp { get; set; } = timestamp;
    }
}

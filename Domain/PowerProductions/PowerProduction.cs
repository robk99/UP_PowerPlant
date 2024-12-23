
using Domain.PowerPlants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.PowerProductions
{
    public class PowerProduction : AuditDetails
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

        public PowerProduction(float powerProduced, DateTime timestamp, int powerPlantId)
        {
            PowerProduced = powerProduced;
            Timestamp = timestamp;
            PowerPlantId = powerPlantId;
        }

        // Parameterless constructor for EF Core
        private PowerProduction() { }
    }
}

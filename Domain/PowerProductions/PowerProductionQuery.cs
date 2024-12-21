

namespace Domain.PowerProductions
{
    public record PowerProductionQuery
    {
        public int PowerPlantId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

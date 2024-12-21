namespace Domain.PowerProductions.Queries
{
    public record PowerProductionTimeseriesQuery
    {
        public int PowerPlantId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

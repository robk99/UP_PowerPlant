namespace API.PowerProductions.Responses
{
    public record PowerProductionData
    {
        public string Timestamp { get; set; }
        public double PowerProduced { get; set; }
    }
    public record PowerProductionTimeseriesResponse
    {
        public int PowerPlantId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public List<PowerProductionData> ProductionTimeseries { get; set; } = new();
    }
}
